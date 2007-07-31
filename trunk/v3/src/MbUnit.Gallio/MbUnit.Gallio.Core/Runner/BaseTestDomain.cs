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
using MbUnit.Core.Serialization;
using MbUnit.Core.Utilities;
using MbUnit.Framework.Kernel.Events;
using MbUnit.Framework.Kernel.Model;

namespace MbUnit.Core.Runner
{
    /// <summary>
    /// Base implementation of a test domain.
    /// </summary>
    /// <remarks>
    /// The base implementation inherits from <see cref="MarshalByRefObject" />
    /// so that test domain's services can be accessed remotely if needed.
    /// </remarks>
    public abstract class BaseTestDomain : LongLivingMarshalByRefObject, ITestDomain
    {
        private bool disposed;
        private IEventListener listener;
        private TestProject testProject;
        private TemplateModel templateModel;
        private TestModel testModel;

        /// <inheritdoc />
        public void Dispose()
        {
            if (!disposed)
            {
                InternalDispose();

                listener = null;
                testProject = null;
                templateModel = null;
                testModel = null;
                disposed = true;
            }
        }

        /// <inheritdoc />
        public TestProject TestProject
        {
            get
            {
                ThrowIfDisposed();
                return testProject;
            }
            protected set
            {
                testProject = value;
            }
        }

        /// <inheritdoc />
        public TemplateModel TemplateModel
        {
            get
            {
                ThrowIfDisposed();
                return templateModel;
            }
            protected set
            {
                templateModel = value;
            }
        }

        /// <inheritdoc />
        public TestModel TestModel
        {
            get
            {
                ThrowIfDisposed();
                return testModel;
            }
            protected set
            {
                testModel = value;
            }
        }

        /// <summary>
        /// Gets the event listener, or null if none was set.
        /// </summary>
        protected IEventListener Listener
        {
            get { return listener; }
        }

        /// <inheritdoc />
        public void SetEventListener(IEventListener listener)
        {
            this.listener = listener;
        }

        /// <inheritdoc />
        public void LoadProject(TestProject project)
        {
            if (project == null)
                throw new ArgumentNullException("project");
            ThrowIfDisposed();

            InternalUnloadProject();

            try
            {
                this.testProject = project;
                InternalLoadProject(project);
            }
            catch (Exception)
            {
                UnloadProject();
                throw;
            }
        }

        /// <inheritdoc />
        public void BuildTemplates(TemplateEnumerationOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            ThrowIfDisposed();
            InternalBuildTemplates(options);
        }

        /// <inheritdoc />
        public void BuildTests(TestEnumerationOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            ThrowIfDisposed();
            InternalBuildTests(options);
        }

        /// <inheritdoc />
        public void UnloadProject()
        {
            ThrowIfDisposed();
            InternalUnloadProject();

            testProject = null;
            testModel = null;
            templateModel = null;
        }

        /// <inheritdoc />
        public void RunTests(TestExecutionOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            ThrowIfDisposed();
            InternalRunTests(options);
        }

        /// <summary>
        /// Internal implementation of <see cref="Dispose" />.
        /// </summary>
        protected abstract void InternalDispose();

        /// <summary>
        /// Internal implementation of <see cref="LoadProject" />.
        /// </summary>
        /// <param name="project">The test project</param>
        protected abstract void InternalLoadProject(TestProject project);

        /// <summary>
        /// Internal implementation of <see cref="BuildTemplates" />.
        /// </summary>
        /// <param name="options">The template enumeration options</param>
        protected abstract void InternalBuildTemplates(TemplateEnumerationOptions options);

        /// <summary>
        /// Internal implementation of <see cref="BuildTests" />.
        /// </summary>
        /// <param name="options">The test enumeration options</param>
        protected abstract void InternalBuildTests(TestEnumerationOptions options);

        /// <summary>
        /// Internal implementation of <see cref="RunTests" />.
        /// </summary>
        /// <param name="options">The test execution options</param>
        protected abstract void InternalRunTests(TestExecutionOptions options);

        /// <summary>
        /// Internal implementation of <see cref="UnloadProject" />.
        /// </summary>
        protected abstract void InternalUnloadProject();

        /// <summary>
        /// Throws <see cref="ObjectDisposedException"/> if the domain has been disposed.
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException("Isolated test domain");
        }
    }
}
