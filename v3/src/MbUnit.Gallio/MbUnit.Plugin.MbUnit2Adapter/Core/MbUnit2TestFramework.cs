using System;
using System.Collections.Generic;
using System.Reflection;
using MbUnit.Framework.Collections;
using MbUnit.Framework.Model.Metadata;
using MbUnit.Framework.Model;
using MbUnit.Core.Utilities;
using MbUnit.Framework.Utilities;

namespace MbUnit.Plugin.MbUnit2Adapter.Core
{
    /// <summary>
    /// Builds a test object model based on reflection against MbUnit v2 framework attributes.
    /// </summary>
    public class MbUnit2TestFramework : ITestFramework
    {
        /// <inheritdoc />
        public string Name
        {
            get { return "MbUnit v2"; }
        }

        /// <inheritdoc />
        public void BuildTemplates(TestTemplateTreeBuilder builder, ITestTemplate parent)
        {
            MultiMap<AssemblyName, Assembly> map = ReflectionUtils.GetReverseAssemblyReferenceMap(builder.Project.Assemblies, "MbUnit.Framework");
            foreach (KeyValuePair<AssemblyName, IList<Assembly>> entry in map)
            {
                // Add a framework template with suitable rules to populate tests using the
                // MbUnit v2 test enumerator.  We don't actually represent each test as a
                // template because we can't perform any interesting meta-operations
                // on them like binding test parameters or composing tests.
                Version frameworkVersion = entry.Key.Version;
                TestTemplateGroup frameworkTemplate = new TestTemplateGroup("MbUnit v" + frameworkVersion, CodeReference.Unknown);
                frameworkTemplate.Kind = TemplateKind.Framework;
                parent.AddChild(frameworkTemplate);

                foreach (Assembly assembly in entry.Value)
                {
                    TestTemplateGroup assemblyTemplate = new TestTemplateGroup(assembly.FullName, CodeReference.CreateFromAssembly(assembly));
                    assemblyTemplate.Kind = TemplateKind.Assembly;
                    frameworkTemplate.AddChild(assemblyTemplate);

                    // TODO: Add rules to populate the test graph using MbUnit v2.
                }
            }
        }

        /// <inheritdoc />
        public void InitializeTestAssembly(Assembly assembly)
        {
        }

        /*
        private void BuildTests(Assembly assembly)
        {
            FixtureExplorer explorer = new FixtureExplorer(assembly);
            explorer.Explore();
            // Use Fixture.Load on each Fixture in the FixtureGraph to
            // populate the run pipes.

            // Use FixtureFilter to select test fixtures by type.
            // Use RunPipeFilter to select tests by members.
            // Use GetDependentAssemblies() to handle assembly dependencies
        }
         */
    }
}
