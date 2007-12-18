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
using System.Threading;
using Gallio.Model.Execution;
using Gallio.Core.ProgressMonitoring;
using Gallio.Logging;
using Gallio.Model;
using Gallio.Plugin.NUnitAdapter.Properties;
using NUnit.Core;
using NUnitTestResult=NUnit.Core.TestResult;
using NUnitTestName = NUnit.Core.TestName;

namespace Gallio.Plugin.NUnitAdapter.Model
{
    /// <summary>
    /// Controls the execution of NUnit tests.
    /// </summary>
    internal class NUnitTestController : ITestController
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
        public void RunTests(IProgressMonitor progressMonitor, ITestMonitor rootTestMonitor)
        {
            ThrowIfDisposed();

            using (progressMonitor)
            {
                IList<ITestMonitor> testMonitors = rootTestMonitor.GetAllMonitors();

                progressMonitor.BeginTask(Resources.NUnitTestController_RunningNUnitTests, testMonitors.Count);

                using (RunMonitor monitor = new RunMonitor(runner, testMonitors, progressMonitor))
                {
                    monitor.Run();
                }
            }
        }

        private void ThrowIfDisposed()
        {
            if (runner == null)
                throw new ObjectDisposedException(Resources.NUnitTestController_ControllerWasDisposedException);
        }

        private class RunMonitor : EventListener, ITestFilter, IDisposable
        {
            private readonly IProgressMonitor progressMonitor;
            private readonly TestRunner runner;
            private readonly IList<ITestMonitor> testMonitors;

            private Dictionary<TestName, ITestMonitor> testMonitorsByTestName;
            private Stack<ITestStepMonitor> stepMonitorStack;

            public RunMonitor(TestRunner runner, IList<ITestMonitor> testMonitors,
                IProgressMonitor progressMonitor)
            {
                this.progressMonitor = progressMonitor;
                this.runner = runner;
                this.testMonitors = testMonitors;

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
            }

            private void Initialize()
            {
                progressMonitor.Canceled += HandleCanceled;

                // Build a reverse mapping from NUnit tests.
                testMonitorsByTestName = new Dictionary<TestName, ITestMonitor>();
                foreach (ITestMonitor testMonitor in testMonitors)
                {
                    NUnitTest test = (NUnitTest) testMonitor.Test;
                    test.ProcessTestNames(delegate(NUnitTestName testName)
                    {
                        testMonitorsByTestName[testName] = testMonitor;
                    }); 
                }

                stepMonitorStack = new Stack<ITestStepMonitor>();
            }

            #region EventListener Members
            void EventListener.RunStarted(string name, int testCount)
            {
                // Note: This handles a possible race condition involving Cancel wherein the
                //       cancelation may not occur if we abort too soon.
                CheckCanceled();
            }

            void EventListener.RunFinished(NUnitTestResult result)
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
                if (stepMonitorStack.Count == 0)
                    return;

                ITestStepMonitor stepMonitor = stepMonitorStack.Peek();

                string streamName;
                switch (testOutput.Type)
                {
                    default:
                    case TestOutputType.Out:
                        streamName = LogStreamNames.ConsoleOutput;
                        break;
                    case TestOutputType.Error:
                        streamName = LogStreamNames.ConsoleError;
                        break;
                    case TestOutputType.Trace:
                        streamName = LogStreamNames.DebugTrace;
                        break;
                }

                stepMonitor.LogWriter[streamName].Write(testOutput.Text);
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
                if (stepMonitorStack.Count == 0)
                    return;

                ITestStepMonitor stepMonitor = stepMonitorStack.Peek();

                stepMonitor.LogWriter[LogStreamNames.Failures].WriteException(exception, Resources.NUnitTestController_UnhandledExceptionSectionName);
            }

            private void HandleTestOrSuiteStarted(TestName testName)
            {
                ITestMonitor testMonitor;
                if (!testMonitorsByTestName.TryGetValue(testName, out testMonitor))
                    return;

                progressMonitor.SetStatus(String.Format(Resources.NUnitTestController_StatusMessages_RunningTest, testMonitor.Test.Name));

                ITestStepMonitor stepMonitor = testMonitor.StartTestInstance();
                stepMonitorStack.Push(stepMonitor);
            }

            private void HandleTestOrSuiteFinished(NUnitTestResult nunitResult)
            {
                if (stepMonitorStack.Count == 0)
                    return;

                ITestStepMonitor stepMonitor = stepMonitorStack.Peek();
                NUnitTest test = (NUnitTest) stepMonitor.Step.TestInstance.Test;
                if (test.Test.TestName != nunitResult.Test.TestName)
                    return;

                stepMonitorStack.Pop();

                progressMonitor.Worked(1);

                if (nunitResult.Message != null)
                {
                    stepMonitor.LogWriter[LogStreamNames.Failures].BeginSection(Resources.NUnitTestController_FailureMessageSectionName);
                    stepMonitor.LogWriter[LogStreamNames.Failures].Write(nunitResult.Message);
                    stepMonitor.LogWriter[LogStreamNames.Failures].EndSection();
                }
                if (nunitResult.StackTrace != null)
                {
                    stepMonitor.LogWriter[LogStreamNames.Failures].BeginSection(Resources.NUnitTestController_FailureStackTraceSectionName);
                    stepMonitor.LogWriter[LogStreamNames.Failures].Write(nunitResult.StackTrace);
                    stepMonitor.LogWriter[LogStreamNames.Failures].EndSection();
                }

                TestOutcome outcome;
                switch (nunitResult.ResultState)
                {
                    case ResultState.Success:
                        outcome = TestOutcome.Passed;
                        break;

                    default:
                    case ResultState.Failure:
                    case ResultState.Error:
                        outcome = TestOutcome.Failed;
                        break;
                }

                TestStatus status;
                switch (nunitResult.RunState)
                {
                    case RunState.Executed:
                        status = TestStatus.Executed;
                        break;

                    case RunState.Skipped:
                    case RunState.Explicit:
                        status = TestStatus.Skipped;
                        outcome = TestOutcome.Inconclusive;
                        break;

                    case RunState.Ignored:
                        status = TestStatus.Ignored;
                        break;

                    default:
                    case RunState.NotRunnable:
                    case RunState.Runnable:
                        status = TestStatus.NotRun;
                        break;
                }

                stepMonitor.Context.AddAssertCount(nunitResult.AssertCount);
                stepMonitor.FinishStep(status, outcome, null);
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
                return testMonitorsByTestName.ContainsKey(test.TestName);
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
        }
    }
}
