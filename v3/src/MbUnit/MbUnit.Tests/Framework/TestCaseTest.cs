﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using Gallio.Model;
using Gallio.Model.Serialization;
using Gallio.Reflection;
using Gallio.Runner.Reports;
using Gallio.Tests;
using MbUnit.Framework;

namespace MbUnit.Tests.Framework
{
    [TestsOn(typeof(TestCase))]
    [RunSample(typeof(DynamicSample))]
    [RunSample(typeof(StaticSample))]
    public class TestCaseTest : BaseTestWithSampleRunner
    {
        [Test]
        public void ConstructorRequiresNameAndExecuteAction()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCase(null, delegate { }));
            Assert.Throws<ArgumentNullException>(() => new TestCase("Foo", null));

            Gallio.Action execute = delegate { };
            TestCase testCase = new TestCase("Name", execute);
            Assert.AreEqual("Name", testCase.Name);
            Assert.AreSame(execute, testCase.Execute);
        }

        [Test]
        public void DescriptionStoredInMetadata()
        {
            TestCase testCase = new TestCase("Name", delegate { });
            Assert.IsNull(testCase.Description);
            Assert.IsFalse(testCase.Metadata.ContainsKey(MetadataKeys.Description));

            testCase.Description = "Description";
            Assert.AreEqual("Description", testCase.Description);
            Assert.AreEqual("Description", testCase.Metadata.GetValue(MetadataKeys.Description));

            testCase.Description = null;
            Assert.IsNull(testCase.Description);
            Assert.IsFalse(testCase.Metadata.ContainsKey(MetadataKeys.Description));
        }

        [Test]
        public void TimeoutMustBeNullOrPositive()
        {
            TestCase testCase = new TestCase("Name", delegate { });
            Assert.IsNull(testCase.Timeout);

            testCase.Timeout = TimeSpan.FromSeconds(5);
            Assert.AreEqual(TimeSpan.FromSeconds(5), testCase.Timeout);

            testCase.Timeout = null;
            Assert.IsNull(testCase.Timeout);

            Assert.Throws<ArgumentOutOfRangeException>(() => testCase.Timeout = TimeSpan.FromSeconds(-1));
        }

        [Test]
        public void CodeElement()
        {
            TestCase testCase = new TestCase("Name", delegate { });
            Assert.IsNull(testCase.CodeElement);

            ITypeInfo type = Reflector.Wrap(typeof(TestCase));
            testCase.CodeElement = type;
            Assert.AreSame(type, testCase.CodeElement);
        }

        [Test]
        public void DynamicRun()
        {
            TestData factoryData = Runner.GetTestData(CodeReference.CreateFromMember(
                typeof(DynamicSample).GetMethod("Factory")));
            Assert.AreEqual(0, factoryData.Children.Count);

            TestStepRun factoryRun = Runner.GetPrimaryTestStepRun(CodeReference.CreateFromMember(
                typeof(DynamicSample).GetMethod("Factory")));
            Assert.AreEqual(1, factoryRun.Children.Count);

            TestStepRun testRun = factoryRun.Children[0];
            Assert.AreEqual("Test", testRun.Step.Name);
            Assert.IsTrue(testRun.Step.IsDynamic);
            Assert.IsFalse(testRun.Step.IsPrimary);
            Assert.IsTrue(testRun.Step.IsTestCase);
            Assert.AreEqual("System.Int32", testRun.Step.CodeReference.TypeName);
            Assert.AreEqual("Me", testRun.Step.Metadata.GetValue(MetadataKeys.AuthorName));
            Assert.AreEqual(TestKinds.Test, testRun.Step.Metadata.GetValue(MetadataKeys.TestKind));
            Assert.AreEqual("*** Log ***\n\nExecute\n", testRun.TestLog.ToString());
            Assert.AreEqual(0, testRun.Children.Count);
        }

        [Test]
        public void StaticRun()
        {
            TestData fixtureData = Runner.GetTestData(CodeReference.CreateFromType(typeof(StaticSample)));
            Assert.AreEqual(1, fixtureData.Children.Count);

            TestData testData = fixtureData.Children[0];
            Assert.AreEqual("Test", testData.Name);
            Assert.IsTrue(testData.IsTestCase);
            Assert.AreEqual("System.Int32", testData.CodeReference.TypeName);
            Assert.AreEqual("Me", testData.Metadata.GetValue(MetadataKeys.AuthorName));
            Assert.AreEqual(TestKinds.Test, testData.Metadata.GetValue(MetadataKeys.TestKind));
            Assert.AreEqual(0, testData.Children.Count);

            TestStepRun fixtureRun = Runner.GetPrimaryTestStepRun(CodeReference.CreateFromType(typeof(StaticSample)));
            Assert.AreEqual(1, fixtureRun.Children.Count);

            TestStepRun testRun = fixtureRun.Children[0];
            Assert.AreEqual("Test", testRun.Step.Name);
            Assert.IsFalse(testRun.Step.IsDynamic);
            Assert.IsTrue(testRun.Step.IsPrimary);
            Assert.IsTrue(testRun.Step.IsTestCase);
            Assert.AreEqual("System.Int32", testRun.Step.CodeReference.TypeName);
            Assert.AreEqual("Me", testRun.Step.Metadata.GetValue(MetadataKeys.AuthorName));
            Assert.AreEqual(TestKinds.Test, testRun.Step.Metadata.GetValue(MetadataKeys.TestKind));
            Assert.AreEqual("*** Log ***\n\nExecute\n", testRun.TestLog.ToString());
            Assert.AreEqual(0, testRun.Children.Count);
        }

        private static readonly Test[] tests = new Test[]
        {
            new TestCase("Test", () => TestLog.WriteLine("Execute"))
            {
                CodeElement = Reflector.Wrap(typeof(Int32)),
                Description = "Description",
                Metadata = { { MetadataKeys.AuthorName, "Me" }},
            }
        };

        [Explicit("Sample")]
        public class DynamicSample
        {
            [DynamicTestFactory]
            public IEnumerable<Test> Factory()
            {
                return tests;
            }
        }

        [Explicit("Sample")]
        public class StaticSample
        {
            [StaticTestFactory]
            public static IEnumerable<Test> Factory()
            {
                return tests;
            }
        }

        [Explicit("Sample")]
        public class ReferencedFixture
        {
            [Test]
            public void Test()
            {
                TestLog.WriteLine("Test Ran");
            }
        }
    }
}
