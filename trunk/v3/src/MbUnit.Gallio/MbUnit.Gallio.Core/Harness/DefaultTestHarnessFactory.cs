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
using Castle.Core;
using MbUnit.Framework.Kernel.Harness;
using MbUnit.Framework.Services.Runtime;

namespace MbUnit.Core.Harness
{
    /// <summary>
    /// The default test harness factory creates a <see cref="DefaultTestHarness" />
    /// and 
    /// </summary>
    [Singleton]
    public class DefaultTestHarnessFactory : ITestHarnessFactory
    {
        private IRuntime runtime;
        private List<ITestHarnessContributor> contributors;

        /// <summary>
        /// Creates a default test harness factory that adds all registered
        /// <see cref="ITestFramework" /> and <see cref="ITestHarnessContributor" />
        /// services to the test harness as contributors.
        /// </summary>
        /// <param name="runtime">The runtime</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="runtime"/> is null</exception>
        public DefaultTestHarnessFactory(IRuntime runtime)
        {
            if (runtime == null)
                throw new ArgumentNullException("runtime");

            this.runtime = runtime;

            contributors = new List<ITestHarnessContributor>();
            contributors.AddRange(runtime.ResolveAll<ITestFramework>());
            contributors.AddRange(runtime.ResolveAll<ITestHarnessContributor>());
        }

        /// <inheritdoc />
        public ITestHarness CreateHarness()
        {
            DefaultTestHarness harness = new DefaultTestHarness(runtime);

            foreach (ITestHarnessContributor contributor in contributors)
                harness.AddContributor(contributor);

            return harness;
        }
    }
}
