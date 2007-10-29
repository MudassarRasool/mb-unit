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
using System.Reflection;
using Gallio.Plugin.NUnitAdapter.Model;
using MbUnit.Framework;
using Gallio.Model;
using Gallio.TestResources.NUnit;
using Gallio.TestResources.NUnit.Metadata;
using Gallio.Tests.Model;

namespace Gallio.Plugin.NUnitAdapter.Tests.Model
{
    [TestFixture]
    [TestsOn(typeof(NUnitTestFramework))]
    [Author("Jeff", "jeff@ingenio.com")]
    public class NUnitTestFrameworkTest : BaseTestFrameworkTest
    {
        protected override Assembly GetSampleAssembly()
        {
            return typeof(SimpleTest).Assembly;
        }

        protected override ITestFramework CreateFramework()
        {
            return new NUnitTestFramework();
        }

        [Test]
        public void NameIsNUnit()
        {
            Assert.AreEqual("NUnit", framework.Name);
        }

        [Test]
        public void PopulateTemplateTree_WhenAssemblyDoesNotReferenceFramework_IsEmpty()
        {
            sampleAssembly = typeof(Int32).Assembly;
            PopulateTemplateTree();

            Assert.AreEqual(0, rootTemplate.Children.Count);
        }

        [Test]
        public void PopulateTemplateTree_WhenAssemblyReferencesNUnit_ContainsJustTheFrameworkTemplate()
        {
            Version expectedVersion = typeof(NUnit.Framework.Assert).Assembly.GetName().Version;
            PopulateTemplateTree();

            Assert.IsNull(rootTemplate.Parent);
            Assert.AreEqual(ComponentKind.Root, rootTemplate.Kind);
            Assert.AreEqual(CodeReference.Unknown, rootTemplate.CodeReference);
            Assert.IsTrue(rootTemplate.IsGenerator);
            Assert.AreEqual(1, rootTemplate.Children.Count);

            BaseTemplate frameworkTemplate = (BaseTemplate)rootTemplate.Children[0];
            Assert.AreSame(rootTemplate, frameworkTemplate.Parent);
            Assert.AreEqual(ComponentKind.Framework, frameworkTemplate.Kind);
            Assert.AreEqual(CodeReference.Unknown, frameworkTemplate.CodeReference);
            Assert.AreEqual("NUnit v" + expectedVersion, frameworkTemplate.Name);
            Assert.IsTrue(frameworkTemplate.IsGenerator);
            Assert.AreEqual(0, frameworkTemplate.Children.Count);
        }

        [Test]
        public void PopulateTestTree_CapturesTestStructureAndBasicMetadata()
        {
            Version expectedVersion = typeof(NUnit.Framework.Assert).Assembly.GetName().Version;
            PopulateTestTree();

            Assert.IsNull(rootTest.Parent);
            Assert.AreEqual(ComponentKind.Root, rootTest.Kind);
            Assert.AreEqual(CodeReference.Unknown, rootTest.CodeReference);
            Assert.IsFalse(rootTest.IsTestCase);
            Assert.AreEqual(1, rootTest.Children.Count);

            BaseTest frameworkTest = (BaseTest)rootTest.Children[0];
            Assert.AreSame(rootTest, frameworkTest.Parent);
            Assert.AreEqual(ComponentKind.Framework, frameworkTest.Kind);
            Assert.AreEqual(CodeReference.Unknown, frameworkTest.CodeReference);
            Assert.AreEqual("NUnit v" + expectedVersion, frameworkTest.Name);
            Assert.IsFalse(frameworkTest.IsTestCase);
            Assert.AreEqual(1, frameworkTest.Children.Count);

            NUnitTest assemblyTest = (NUnitTest)frameworkTest.Children[0];
            Assert.AreSame(frameworkTest, assemblyTest.Parent);
            Assert.AreEqual(ComponentKind.Assembly, assemblyTest.Kind);
            Assert.AreEqual(CodeReference.CreateFromAssembly(sampleAssembly), assemblyTest.CodeReference);
            Assert.AreEqual(sampleAssembly.GetName().Name, assemblyTest.Name);
            Assert.IsFalse(assemblyTest.IsTestCase);
            Assert.GreaterEqualThan(assemblyTest.Children.Count, 1);

            NUnitTest fixtureTest = (NUnitTest)GetDescendantByName(assemblyTest, "SimpleTest");
            Assert.AreSame(assemblyTest, fixtureTest.Parent);
            Assert.AreEqual(ComponentKind.Fixture, fixtureTest.Kind);
            Assert.AreEqual(new CodeReference(sampleAssembly.FullName, "Gallio.TestResources.NUnit", "Gallio.TestResources.NUnit.SimpleTest", null, null), fixtureTest.CodeReference);
            Assert.AreEqual("SimpleTest", fixtureTest.Name);
            Assert.IsFalse(fixtureTest.IsTestCase);
            Assert.AreEqual(2, fixtureTest.Children.Count);

            NUnitTest passTest = (NUnitTest)GetDescendantByName(fixtureTest, "Pass");
            NUnitTest failTest = (NUnitTest)GetDescendantByName(fixtureTest, "Fail");
 
            Assert.AreSame(fixtureTest, passTest.Parent);
            Assert.AreEqual(ComponentKind.Test, passTest.Kind);
            Assert.AreEqual(new CodeReference(sampleAssembly.FullName, "Gallio.TestResources.NUnit", "Gallio.TestResources.NUnit.SimpleTest", "Pass", null), passTest.CodeReference);
            Assert.AreEqual("Pass", passTest.Name);
            Assert.IsTrue(passTest.IsTestCase);
            Assert.AreEqual(0, passTest.Children.Count);

            Assert.AreSame(fixtureTest, failTest.Parent);
            Assert.AreEqual(ComponentKind.Test, failTest.Kind);
            Assert.AreEqual(new CodeReference(sampleAssembly.FullName, "Gallio.TestResources.NUnit", "Gallio.TestResources.NUnit.SimpleTest", "Fail", null), failTest.CodeReference);
            Assert.AreEqual("Fail", failTest.Name);
            Assert.IsTrue(failTest.IsTestCase);
            Assert.AreEqual(0, failTest.Children.Count);
        }

        [Test]
        public void MetadataImport_XmlDocumentation()
        {
            PopulateTestTree();

            NUnitTest test = (NUnitTest)GetDescendantByName(rootTest, typeof(SimpleTest).Name);
            NUnitTest passTest = (NUnitTest)GetDescendantByName(test, "Pass");
            NUnitTest failTest = (NUnitTest)GetDescendantByName(test, "Fail");

            Assert.AreEqual("<summary>\nA simple test fixture.\n</summary>", test.Metadata.GetValue(MetadataKeys.XmlDocumentation));
            Assert.AreEqual("<summary>\nA passing test.\n</summary>", passTest.Metadata.GetValue(MetadataKeys.XmlDocumentation));
            Assert.AreEqual("<summary>\nA failing test.\n</summary>", failTest.Metadata.GetValue(MetadataKeys.XmlDocumentation));
        }

        [Test]
        public void MetadataImport_Description()
        {
            PopulateTestTree();

            NUnitTest test = (NUnitTest)GetDescendantByName(rootTest, typeof(DescriptionSample).Name);
            Assert.AreEqual("A sample description.", test.Metadata.GetValue(MetadataKeys.Description));
        }

        [Test]
        public void MetadataImport_Category()
        {
            PopulateTestTree();

            NUnitTest test = (NUnitTest)GetDescendantByName(rootTest, typeof(CategorySample).Name);
            Assert.AreEqual("samples", test.Metadata.GetValue(MetadataKeys.CategoryName));
        }

        [Test]
        public void MetadataImport_IgnoreReason()
        {
            PopulateTestTree();

            NUnitTest fixture = (NUnitTest)GetDescendantByName(rootTest, typeof(IgnoreReasonSample).Name);
            NUnitTest test = (NUnitTest)fixture.Children[0];
            Assert.AreEqual("For testing purposes.", test.Metadata.GetValue(MetadataKeys.IgnoreReason));
        }

        [Test]
        public void MetadataImport_Property()
        {
            PopulateTestTree();

            NUnitTest test = (NUnitTest)GetDescendantByName(rootTest, typeof(PropertySample).Name);
            Assert.AreEqual("customvalue-1", test.Metadata.GetValue("customkey-1"));
            Assert.AreEqual("customvalue-2", test.Metadata.GetValue("customkey-2"));
        }

        [Test]
        public void MetadataImport_AssemblyAttributes()
        {
            PopulateTestTree();

            BaseTest frameworkTest = (BaseTest)rootTest.Children[0];
            NUnitTest assemblyTest = (NUnitTest)frameworkTest.Children[0];

            Assert.AreEqual("MbUnit Project", assemblyTest.Metadata.GetValue(MetadataKeys.Company));
            Assert.AreEqual("Test", assemblyTest.Metadata.GetValue(MetadataKeys.Configuration));
            StringAssert.Contains(assemblyTest.Metadata.GetValue(MetadataKeys.Copyright), "MbUnit Project");
            Assert.AreEqual("A sample test assembly for NUnit.", assemblyTest.Metadata.GetValue(MetadataKeys.Description));
            Assert.AreEqual("Gallio", assemblyTest.Metadata.GetValue(MetadataKeys.Product));
            Assert.AreEqual("Gallio.TestResources.NUnit", assemblyTest.Metadata.GetValue(MetadataKeys.Title));
            Assert.AreEqual("Gallio", assemblyTest.Metadata.GetValue(MetadataKeys.Trademark));

            Assert.AreEqual("1.2.3.4", assemblyTest.Metadata.GetValue(MetadataKeys.InformationalVersion));
            StringAssert.IsNonEmpty(assemblyTest.Metadata.GetValue(MetadataKeys.FileVersion));
            StringAssert.IsNonEmpty(assemblyTest.Metadata.GetValue(MetadataKeys.Version));
        }
    }
}
