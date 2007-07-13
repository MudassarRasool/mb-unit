using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using MbUnit.Core.Model;
using MbUnit.Core.Runtime;
using MbUnit.Core.Serialization;

namespace MbUnit.Core.Runner
{
    /// <summary>
    /// A local implementation of a test domain that performs all processing
    /// with the current app-domain including loading assemblies.
    /// </summary>
    public class LocalTestDomain : BaseTestDomain
    {
        private IAssemblyResolverManager resolverManager;
        private TestProject modelProject;

        /// <summary>
        /// Creates a local test domain using the specified resolver manager.
        /// </summary>
        /// <param name="resolverManager">The assembly resolver manager</param>
        public LocalTestDomain(IAssemblyResolverManager resolverManager)
        {
            this.resolverManager = resolverManager;
        }

        /// <inheritdoc />
        protected override void InternalDispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void InternalLoadProject(TestProjectInfo project)
        {
            modelProject = new TestProject();

            foreach (string path in project.HintDirectories)
                resolverManager.AddHintDirectory(path);

            foreach (string assemblyFile in project.AssemblyFiles)
                resolverManager.AddHintDirectoryContainingFile(assemblyFile);

            foreach (string assemblyFile in project.AssemblyFiles)
            {
                modelProject.Assemblies.Add(LoadTestAssembly(assemblyFile));
            }
        }

        /// <inheritdoc />
        protected override void InternalBuildTestTemplates()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void InternalBuildTests()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void InternalRunTests()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        protected override void InternalUnloadProject()
        {
            modelProject = null;
        }

        private Assembly LoadTestAssembly(string assemblyFile)
        {
            try
            {
                return Assembly.LoadFrom(assemblyFile);
            }
            catch (Exception ex)
            {
                throw new FatalRunnerException(String.Format(CultureInfo.CurrentCulture,
                    "Could not load test assembly from '{0}'.", assemblyFile), ex);
            }
        }
    }
}
