// Copyright 2007 MbUnit Project - http://www.mbunit.com/
// Portions Copyright 2000-2004 Jonathan De Halleux, Jamie Cansdale
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MbUnit.Framework.Kernel.Events;
using MbUnit.Framework.Kernel.Model;
using MbUnit.Framework.Kernel.Results;
using MbUnit.Framework.Kernel.ExecutionLogs;
using NUnit.Core;

using ITest = MbUnit.Framework.Kernel.Model.ITest;
using TestResult = MbUnit.Framework.Kernel.Results.TestResult;

namespace MbUnit.Plugin.NUnitAdapter.Core
{
    /// <summary>
    /// Controls the execution of NUnit tests.
    /// </summary>
    public class NUnitTestController : ITestController
    {
        private TestRunner runner;

        /// <summary>
        /// Creates a test controller.
        /// </summary>
        /// <param name="runner">The test runner</param>
        public NUnitTestController(TestRunner runner)
        {
            this.runner = runner;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            runner = null;
        }

        /// <inheritdoc />
        public void Run(IProgressMonitor progressMonitor,
            TestExecutionOptions options, IEventListener listener, IList<ITest> tests)
        {
            ThrowIfDisposed();

            using (progressMonitor)
            {
                progressMonitor.BeginTask("Running NUnit tests.", tests.Count);

                using (RunMonitor monitor = new RunMonitor(progressMonitor, runner, options, listener, tests))
                {
                    monitor.Run();
                }
            }
        }

        private void ThrowIfDisposed()
        {
            if (runner == null)
                throw new ObjectDisposedException("The test controller has been disposed.");
        }

        private class RunMonitor : EventListener, ITestFilter, IDisposable
        {
            private IProgressMonitor progressMonitor;
            private TestRunner runner;
            private TestExecutionOptions options;
            private IEventListener listener;
            private IList<ITest> tests;

            private Dictionary<TestName, NUnitTest> testsByTestName;
            private Stack<IStep> stepStack;

            public RunMonitor(IProgressMonitor progressMonitor, TestRunner runner,
                TestExecutionOptions options, IEventListener listener, IList<ITest> tests)
            {
                this.progressMonitor = progressMonitor;
                this.runner = runner;
                this.options = options;
                this.listener = listener;
                this.tests = tests;

                Initialize();
            }

            public void Dispose()
            {
                progressMonitor.Canceled -= HandleCanceled;
            }

            public void Run()
            {
                try
                {
                    // NUnit does not seem to catch unhandled exceptions at the app-domain level
                    // itself so they bubble up to the test runner and get printed to the console
                    // (due to our use of the legacyExceptionPolicy).  This is very bizarre.
                    // So we handle these exceptions here and try to log them with the test results.
                    AppDomain.CurrentDomain.UnhandledException += HandleUnhandledException;

                    runner.Run(this, this);
                }
                catch (ThreadAbortException)
                {
                    // NUnit cancelation is nasty!  It does a Thread.Abort on the test runner's
                    // thread.  If we were aborted due to cancelation then we try to recover here.
                    // This happened when we were using the SimpleTestRunner but it looks like the
                    // RemoteTestRunner is immune.  I'm leaving this in just in case.
                    if (progressMonitor.IsCanceled)
                        Thread.ResetAbort();
                }
                finally
                {
                    AppDomain.CurrentDomain.UnhandledException -= HandleUnhandledException;
                }
            }

            private void Initialize()
            {
                progressMonitor.Canceled += HandleCanceled;

                // Build a reverse mapping from NUnit tests.
                testsByTestName = new Dictionary<TestName, NUnitTest>();
                foreach (NUnitTest test in tests)
                {
                    if (test.Test != null)
                        testsByTestName[test.Test.TestName] = test;
                }

                stepStack = new Stack<IStep>();
            }

            #region EventListener Members
            void EventListener.RunStarted(string name, int testCount)
            {
                // Note: This handles a possible race condition involving Cancel wherein the
                //       cancelation may not occur if we abort too soon.
                CheckCanceled();
            }

            void EventListener.RunFinished(NUnit.Core.TestResult result)
            {
            }

            void EventListener.RunFinished(Exception exception)
            {
            }

            void EventListener.TestStarted(TestName testName)
            {
                HandleTestOrSuiteStarted(testName);
            }

            void EventListener.TestFinished(TestCaseResult nunitResult)
            {
                HandleTestOrSuiteFinished(nunitResult);
            }

            void EventListener.TestOutput(TestOutput testOutput)
            {
                if (stepStack.Count == 0)
                    return;

                IStep step = stepStack.Peek();

                string streamName;
                switch (testOutput.Type)
                {
                    case TestOutputType.Out:
                        streamName = ExecutionLogStreamName.ConsoleOutput;
                        break;
                    case TestOutputType.Error:
                        streamName = ExecutionLogStreamName.ConsoleError;
                        break;
                    case TestOutputType.Trace:
                        streamName = ExecutionLogStreamName.Trace;
                        break;
                    default:
                        streamName = "NUnit TestOutputType(" + testOutput.Type + ")";
                        break;
                }

                listener.NotifyExecutionLogEvent(ExecutionLogEventArgs.CreateWriteTextEvent(step.Id, streamName, testOutput.Text));
            }

            void EventListener.SuiteStarted(TestName testName)
            {
                HandleTestOrSuiteStarted(testName);
            }

            void EventListener.SuiteFinished(TestSuiteResult nunitResult)
            {
                HandleTestOrSuiteFinished(nunitResult);
            }

            void EventListener.UnhandledException(Exception exception)
            {
                LogException(exception);
            }

            private void LogException(Exception exception)
            {
                if (stepStack.Count == 0)
                    return;

                IStep step = stepStack.Peek();

                listener.NotifyExecutionLogEvent(ExecutionLogEventArgs.CreateBeginSectionEvent(step.Id, ExecutionLogStreamName.Failures, "Unhandled Exception"));
                listener.NotifyExecutionLogEvent(ExecutionLogEventArgs.CreateWriteTextEvent(step.Id, ExecutionLogStreamName.Failures, exception.ToString()));
                listener.NotifyExecutionLogEvent(ExecutionLogEventArgs.CreateEndSectionEvent(step.Id, ExecutionLogStreamName.Failures));
            }

            private void HandleTestOrSuiteStarted(TestName testName)
            {
                NUnitTest test;
                if (!testsByTestName.TryGetValue(testName, out test))
                    return;

                progressMonitor.SetStatus("Run test: " + test.Name + ".");

                IStep step = BaseStep.CreateRootStep(test);
                stepStack.Push(step);

                listener.NotifyLifecycleEvent(LifecycleEventArgs.CreateStartEvent(new StepInfo(step)));
            }

            private void HandleTestOrSuiteFinished(NUnit.Core.TestResult nunitResult)
            {
                if (stepStack.Count == 0)
                    return;

                IStep step = stepStack.Peek();
                NUnitTest test = (NUnitTest) step.Test;
                if (test.Test.TestName != nunitResult.Test.TestName)
                    return;

                stepStack.Pop();

                progressMonitor.Worked(1);

                if (nunitResult.Message != null)
                {
                    listener.NotifyExecutionLogEvent(ExecutionLogEventArgs.CreateBeginSectionEvent(step.Id, ExecutionLogStreamName.Failures, "Failure Message"));
                    listener.NotifyExecutionLogEvent(ExecutionLogEventArgs.CreateWriteTextEvent(step.Id, ExecutionLogStreamName.Failures, nunitResult.Message));
                    listener.NotifyExecutionLogEvent(ExecutionLogEventArgs.CreateEndSectionEvent(step.Id, ExecutionLogStreamName.Failures));
                }
                if (nunitResult.StackTrace != null)
                {
                    listener.NotifyExecutionLogEvent(ExecutionLogEventArgs.CreateBeginSectionEvent(step.Id, ExecutionLogStreamName.Failures, "Failure Stack Trace"));
                    listener.NotifyExecutionLogEvent(ExecutionLogEventArgs.CreateWriteTextEvent(step.Id, ExecutionLogStreamName.Failures, nunitResult.StackTrace));
                    listener.NotifyExecutionLogEvent(ExecutionLogEventArgs.CreateEndSectionEvent(step.Id, ExecutionLogStreamName.Failures));
                }

                listener.NotifyExecutionLogEvent(ExecutionLogEventArgs.CreateCloseEvent(step.Id));

                TestResult result = CreateTestResultFromNUnitTestResult(nunitResult);
                listener.NotifyLifecycleEvent(LifecycleEventArgs.CreateFinishEvent(step.Id, result));
            }

            private TestResult CreateTestResultFromNUnitTestResult(NUnit.Core.TestResult nunitResult)
            {
                TestResult result = new TestResult();
                result.AssertCount = nunitResult.AssertCount;
                result.Duration = nunitResult.Time;

                switch (nunitResult.ResultState)
                {
                    case ResultState.Success:
                        result.Outcome = TestOutcome.Passed;
                        break;

                    case ResultState.Failure:
                    case ResultState.Error:
                        result.Outcome = TestOutcome.Failed;
                        break;
                }

                switch (nunitResult.RunState)
                {
                    case RunState.Executed:
                        result.State = TestState.Executed;
                        break;

                    case RunState.Skipped:
                    case RunState.Explicit:
                        result.State = TestState.Skipped;
                        result.Outcome = TestOutcome.Inconclusive;
                        break;

                    case RunState.Ignored:
                        result.State = TestState.Ignored;
                        break;

                    case RunState.NotRunnable:
                    case RunState.Runnable:
                        result.State = TestState.NotRun;
                        break;
                }

                return result;
            }
            #endregion

            #region ITestFilter Members
            bool ITestFilter.Pass(NUnit.Core.ITest test)
            {
                return FilterTest(test);
            }

            bool ITestFilter.Match(NUnit.Core.ITest test)
            {
                return FilterTest(test);
            }

            bool ITestFilter.IsEmpty
            {
                get { return false; }
            }

            private bool FilterTest(NUnit.Core.ITest test)
            {
                return testsByTestName.ContainsKey(test.TestName);
            }
            #endregion

            private void Cancel()
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    runner.CancelRun();
                });
            }

            private void CheckCanceled()
            {
                if (progressMonitor.IsCanceled)
                    Cancel();
            }

            private void HandleCanceled(object sender, EventArgs e)
            {
                Cancel();
            }

            private void HandleUnhandledException(object sender, UnhandledExceptionEventArgs e)
            {
                Exception ex = e.ExceptionObject as Exception;
                if (ex != null)
                    LogException(ex);
            }
        }
    }
}
