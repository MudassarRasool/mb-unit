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

extern alias MbUnit2;
using MbUnit2::MbUnit.Framework;

using System.Reflection;
using MbUnit.Framework.Kernel.Metadata;
using MbUnit.Framework.Kernel.Model;
using MbUnit.Plugin.NUnitAdapter.Core;

using System;

namespace MbUnit.Plugin.NUnitAdapter.Tests.Core
{
    [TestFixture]
    [TestsOn(typeof(NUnitTestFramework))]
    [Author("Jeff", "jeff@ingenio.com")]
    public class NUnitTestFrameworkTest
    {
        private NUnitTestFramework framework;
        private TestTemplateTreeBuilder builder;

        [SetUp]
        public void SetUp()
        {
            framework = new NUnitTestFramework();
        }

        [Test]
        public void NameIsNUnit()
        {
            Assert.AreEqual("NUnit", framework.Name);
        }

        [Test]
        public void PopulateTree_WhenAssemblyDoesNotReferenceMbUnit_IsEmpty()
        {
            PopulateTree(typeof(Int32).Assembly);

            Assert.AreEqual(0, builder.Root.ChildrenList.Count);
        }

        [Test]
        public void PopulateTree_WhenAssemblyReferencesNUnit_ContainsJustTheFrameworkTemplate()
        {
            Type fixtureType = typeof(MbUnit.TestResources.NUnit.SimpleTest);
            Assembly assembly = fixtureType.Assembly;
            Version expectedVersion = typeof(NUnit.Framework.Assert).Assembly.GetName().Version;

            PopulateTree(assembly);
            Assert.IsNull(builder.Root.Parent);
            Assert.AreEqual(TemplateKind.Root, builder.Root.Kind);
            Assert.AreEqual(CodeReference.Unknown, builder.Root.CodeReference);
            Assert.AreEqual(1, builder.Root.ChildrenList.Count);

            TestTemplateGroup frameworkTemplate = (TestTemplateGroup)builder.Root.ChildrenList[0];
            Assert.AreSame(builder.Root, frameworkTemplate.Parent);
            Assert.AreEqual(TemplateKind.Framework, frameworkTemplate.Kind);
            Assert.AreEqual(CodeReference.Unknown, frameworkTemplate.CodeReference);
            Assert.AreEqual("NUnit v" + expectedVersion, frameworkTemplate.Name);
            Assert.AreEqual(1, frameworkTemplate.ChildrenList.Count);

            TestTemplateGroup assemblyTemplate = (TestTemplateGroup) frameworkTemplate.ChildrenList[0];
            Assert.AreSame(frameworkTemplate, assemblyTemplate.Parent);
            Assert.AreEqual(TemplateKind.Assembly, assemblyTemplate.Kind);
            Assert.AreEqual(CodeReference.CreateFromAssembly(assembly), assemblyTemplate.CodeReference);
            Assert.AreEqual(0, assemblyTemplate.ChildrenList.Count);
        }

        private void PopulateTree(Assembly assembly)
        {
            TestProject project = new TestProject();
            project.Assemblies.Add(assembly);

            builder = new TestTemplateTreeBuilder(project);
            framework.BuildTemplates(builder, builder.Root);
        }
    }
}
