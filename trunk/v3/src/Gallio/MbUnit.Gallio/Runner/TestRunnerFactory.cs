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
using MbUnit.Hosting;
using MbUnit.Runner.Domains;

namespace MbUnit.Runner
{
    /// <summary>
    /// Creates test runners.
    /// </summary>
    public static class TestRunnerFactory
    {
        /// <summary>
        /// Creates a test runner that runs tests locally within the current <see cref="AppDomain" /> using
        /// a <see cref="LocalTestDomain" />.
        /// </summary>
        /// <remarks>
        /// The <see cref="Runtime" /> must be initialized prior to calling this method.
        /// </remarks>
        /// <returns>The test runner</returns>
        public static ITestRunner CreateLocalTestRunner()
        {
            return new DefaultTestRunner(new LocalTestDomainFactory());
        }

        /// <summary>
        /// Creates a test runner that runs tests in an isolated <see cref="AppDomain" /> using
        /// an <see cref="IsolatedTestDomain" />.
        /// </summary>
        /// <remarks>
        /// The <see cref="Runtime" /> must be initialized prior to calling this method.
        /// </remarks>
        /// <returns>The test runner</returns>
        public static ITestRunner CreateIsolatedTestRunner()
        {
            return new DefaultTestRunner(new IsolatedTestDomainFactory());
        }
    }
}
