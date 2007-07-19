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
using System.Globalization;
using System.Reflection;
using System.Text;
using MbUnit.Framework.Kernel.Collections;
using MbUnit.Framework.Kernel.Harness;
using MbUnit.Framework.Kernel.Model;
using MbUnit.Framework.Kernel.Utilities;
using MbUnit.Framework.Services.Runtime;

namespace MbUnit.Core.Harness
{
    /// <summary>
    /// Default implementation of a test harness.
    /// </summary>
    public class DefaultTestHarness : ITestHarness
    {
        private bool isDisposed;
        private IRuntime runtime;
        private IList<Assembly> assemblies;
        private TemplateTreeBuilder templateTreeBuilder;
        private TestTreeBuilder testTreeBuilder;

        /// <summary>
        /// Creates a test harness.
        /// </summary>
        /// <param name="runtime">The runtime</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="runtime"/> is null</exception>
        public DefaultTestHarness(IRuntime runtime)
        {
            if (runtime == null)
                throw new ArgumentNullException("runtime");

            this.runtime = runtime;

            assemblies = new List<Assembly>();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (!isDisposed)
            {
                if (Disposing != null)
                    Disposing(this, EventArgs.Empty);

                isDisposed = true;

                Initialized = null;
                BuildingTemplates = null;
                Disposing = null;

                templateTreeBuilder = null;
                testTreeBuilder = null;
                assemblies = null;
                runtime = null;
            }
        }

        /// <inheritdoc />
        public IRuntime Runtime
        {
            get
            {
                ThrowIfDisposed();
                return runtime;
            }
        }

        /// <inheritdoc />
        public IAssemblyResolverManager AssemblyResolverManager
        {
            get { return runtime.Resolve<IAssemblyResolverManager>(); }
        }

        /// <inheritdoc />
        public IList<Assembly> Assemblies
        {
            get
            {
                ThrowIfDisposed();
                return assemblies;
            }
        }

        /// <inheritdoc />
        public TemplateTreeBuilder TemplateTreeBuilder
        {
            get
            {
                ThrowIfDisposed();

                if (templateTreeBuilder == null)
                    throw new InvalidOperationException("Templates have not been built yet.");
                return templateTreeBuilder;
            }
        }

        /// <inheritdoc />
        public TestTreeBuilder TestTreeBuilder
        {
            get
            {
                ThrowIfDisposed();

                if (testTreeBuilder == null)
                    throw new InvalidOperationException("Tests have not been built yet.");
                return testTreeBuilder;
            }
        }

        /// <inheritdoc />
        public event TypedEventHandler<ITestHarness, EventArgs> Initialized;

        /// <inheritdoc />
        public event TypedEventHandler<ITestHarness, EventArgs> BuildingTemplates;

        /// <inheritdoc />
        public event TypedEventHandler<ITestHarness, EventArgs> BuildingTests;

        /// <inheritdoc />
        public event TypedEventHandler<ITestHarness, EventArgs> Disposing;

        /// <inheritdoc />
        public void AddContributor(ITestHarnessContributor contributor)
        {
            if (contributor == null)
                throw new ArgumentNullException("contributor");

            contributor.Apply(this);
        }

        /// <inheritdoc />
        public void AddAssembly(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            if (!assemblies.Contains(assembly))
                assemblies.Add(assembly);
        }

        /// <inheritdoc />
        public Assembly LoadAssemblyFrom(string assemblyFile)
        {
            if (assemblyFile == null)
                throw new ArgumentNullException("assemblyFile");

            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyFile);
                AddAssembly(assembly);
                return assembly;
            }
            catch (Exception ex)
            {
                throw new TestHarnessException(String.Format(CultureInfo.CurrentCulture,
                    "Could not load test assembly from '{0}'.", assemblyFile), ex);
            }
        }

        /// <inheritdoc />
        public void Initialize()
        {
            ThrowIfDisposed();

            if (Initialized != null)
                Initialized(this, EventArgs.Empty);
        }

        /// <inheritdoc />
        public void BuildTemplates()
        {
            ThrowIfDisposed();

            testTreeBuilder = null;
            templateTreeBuilder = new TemplateTreeBuilder(this);

            if (BuildingTemplates != null)
                BuildingTemplates(this, EventArgs.Empty);

            templateTreeBuilder.FinishBuilding();
        }

        /// <inheritdoc />
        public void BuildTests()
        {
            ThrowIfDisposed();

            if (templateTreeBuilder == null)
                throw new InvalidOperationException("The template tree has not been built yet.");

            testTreeBuilder = new TestTreeBuilder(this);

            ITemplateBinding rootBinding = templateTreeBuilder.Root.Bind(testTreeBuilder.Root.Scope, EmptyDictionary<ITemplateParameter, object>.Instance);
            rootBinding.BuildTests(testTreeBuilder);

            if (BuildingTests != null)
                BuildingTests(this, EventArgs.Empty);

            testTreeBuilder.FinishBuilding();
        }

        private void ThrowIfDisposed()
        {
            if (isDisposed)
                throw new ObjectDisposedException("The test harness has been disposed.");
        }
    }
}
