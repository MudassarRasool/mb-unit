// Copyright 2005-2008 Gallio Project - http://www.gallio.org/
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
using Gallio.Hosting.ProgressMonitoring;

namespace Gallio.Model.Execution
{
    /// <summary>
    /// Base implementation of <see cref="ITestController" /> that does nothing.
    /// </summary>
    public abstract class BaseTestController : ITestController
    {
        /// <inheritdoc />
        public virtual void Dispose()
        {
        }

        /// <inheritdoc />
        public void RunTests(ITestCommand rootTestCommand, ITestInstance parentTestInstance,
            TestExecutionOptions options, IProgressMonitor progressMonitor)
        {
            if (rootTestCommand == null)
                throw new ArgumentNullException("rootTestCommand");
            if (progressMonitor == null)
                throw new ArgumentNullException("progressMonitor");
            if (options == null)
                throw new ArgumentNullException("options");

            RunTestsInternal(rootTestCommand, parentTestInstance, options, progressMonitor);
        }

        /// <summary>
        /// Implementation of <see cref="RunTests" /> called after argument validation has taken place.
        /// </summary>
        /// <param name="rootTestCommand">The root test command, not null</param>
        /// <param name="parentTestInstance">The parent test instance, or null if none</param>
        /// <param name="options">The test execution options, not null</param>
        /// <param name="progressMonitor">The progress monitor, not null</param>
        protected abstract void RunTestsInternal(ITestCommand rootTestCommand, ITestInstance parentTestInstance,
            TestExecutionOptions options, IProgressMonitor progressMonitor);

        /// <summary>
        /// Recursively generates single test instances for each <see cref="ITestCommand" /> and
        /// sets the final outcome to <see cref="TestOutcome.Skipped" />.
        /// </summary>
        /// <remarks>
        /// This is useful for implementing fallback behavior when <see cref="TestExecutionOptions.SkipTestInstanceExecution" />
        /// is true.
        /// </remarks>
        /// <param name="rootTestCommand">The root test command</param>
        /// <param name="parentTestInstance">The parent test instance</param>
        protected static void SkipAll(ITestCommand rootTestCommand, ITestInstance parentTestInstance)
        {
            ITestContext context = rootTestCommand.StartRootStep(parentTestInstance);

            foreach (ITestCommand child in rootTestCommand.Children)
                SkipAll(child, context.TestStep.TestInstance);

            context.FinishStep(TestOutcome.Skipped, null);
        }
    }
}
