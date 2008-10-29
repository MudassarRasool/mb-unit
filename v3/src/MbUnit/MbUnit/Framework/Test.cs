﻿using System;
using System.Collections.Generic;
using Gallio;
using Gallio.Collections;
using Gallio.Framework.Pattern;
using Gallio.Model;
using Gallio.Model.Diagnostics;
using Gallio.Reflection;

namespace MbUnit.Framework
{
    /// <summary>
    /// Describes a test generated either at test exploration time or at test
    /// execution time by a test factory.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Tests cam be nested to form test suites and other aggregates.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// Produces a suite of static tests as part of a containing test fixture.
    /// The suite includes some custom set-up and tear-down behavior, metadata,
    /// a timeout, an order relative to the other tests in the fixture, and a list
    /// of children.  There are two test cases defined in line, and one reference
    /// to another statically defined test elsewhere.
    /// </para>
    /// <code>
    /// [StaticTestFactory]
    /// public static IEnumerable&lt;Test&gt; TestSuite()
    /// {
    ///     yield return new TestSuite("My Suite")
    ///     {
    ///         Description = "An example test suite.",
    ///         Metadata =
    ///         {
    ///             { MetadataKeys.AuthorName, "Me" },
    ///             { MetadataKeys.AuthorEmail, "me@mycompany.com" }
    ///         },
    ///         SuiteSetUp = () => DatabaseUtils.SetUpDatabase(),
    ///         SuiteTearDown = () => DatabaseUtils.TearDownDatabase(),
    ///         Timeout = TimeSpan.FromMinutes(2),
    ///         Children =
    ///         {
    ///             new TestCase("Test 1", () => {
    ///                 // first test in suite
    ///             }),
    ///             new TestCase("Test 2", () => {
    ///                // second test in suite
    ///             }),
    ///             new TestFixtureReference(typeof(OtherFixtureToIncludeInSuite))
    ///         }
    ///     };
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="StaticTestFactoryAttribute"/>
    /// <seealso cref="DynamicTestFactoryAttribute"/>
    /// <seealso cref="TestCase"/>
    /// <seealso cref="TestSuite"/>
    /// <seealso cref="TestFixtureReference"/>
    public abstract class Test
    {
        /// <summary>
        /// Builds a collection of static tests during test exploration.
        /// </summary>
        /// <param name="tests">The enumeration of tests to build as children of the containing scope</param>
        /// <param name="containingScope">The containing pattern evaluation scope</param>
        /// <param name="declaringCodeElement">The code element that represents the scope in which the test was defined</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="containingScope"/>, <paramref name="declaringCodeElement"/>
        /// or <paramref name="tests"/> is null or contains a null</exception>
        /// <seealso cref="StaticTestFactoryAttribute" />
        public static void BuildStaticTests(IEnumerable<Test> tests, PatternEvaluationScope containingScope, ICodeElementInfo declaringCodeElement)
        {
            if (containingScope == null)
                throw new ArgumentNullException("containingScope");
            if (declaringCodeElement == null)
                throw new ArgumentNullException("declaringCodeElement");
            if (tests == null)
                throw new ArgumentNullException("tests");
            if (GenericUtils.Find(tests, test => test == null) != null)
                throw new ArgumentNullException("tests", "Test enumeration should not contain null.");

            foreach (Test test in tests)
                test.BuildStaticTest(containingScope, declaringCodeElement);
        }

        /// <summary>
        /// Runs a collection of dynamic tests during test execution.
        /// </summary>
        /// <param name="tests">The enumeration of tests to run</param>
        /// <param name="declaringCodeElement">The code element that represents the scope in which the test was defined</param>
        /// <param name="setUp">Optional set-up code to run before the test, or null if none</param>
        /// <param name="tearDown">Optional tear-down code to run after the test, or null if none</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="declaringCodeElement"/> or
        /// <paramref name="tests"/> is null or contains a null</exception>
        /// <seealso cref="DynamicTestFactoryAttribute" />
        [TestFrameworkInternal]
        public static TestOutcome RunDynamicTests(IEnumerable<Test> tests, ICodeElementInfo declaringCodeElement, Action setUp, Action tearDown)
        {
            if (declaringCodeElement == null)
                throw new ArgumentNullException("declaringCodeElement");
            if (tests == null)
                throw new ArgumentNullException("tests");
            if (GenericUtils.Find(tests, test => test == null) != null)
                throw new ArgumentNullException("tests", "Test enumeration should not contain null.");

            TestOutcome combinedOutcome = TestOutcome.Passed;
            foreach (Test test in tests)
                combinedOutcome = combinedOutcome.CombineWith(test.RunDynamicTest(declaringCodeElement, setUp, tearDown));

            return combinedOutcome;
        }

        /// <summary>
        /// Builds a static test during test exploration.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Subclasses may override this behavior to change how the static test
        /// gets added to the test model.
        /// </para>
        /// </remarks>
        /// <param name="containingScope">The containing pattern evaluation scope</param>
        /// <param name="declaringCodeElement">The code element that represents the scope in which the test was defined</param>
        /// <seealso cref="StaticTestFactoryAttribute" />
        protected abstract void BuildStaticTest(PatternEvaluationScope containingScope, ICodeElementInfo declaringCodeElement);

        /// <summary>
        /// Runs a dynamic test during test execution.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Subclasses may override this behavior to change how the dynamic test
        /// is executed as a test step.
        /// </para>
        /// </remarks>
        /// <param name="declaringCodeElement">The code element that represents the scope in which the test was defined</param>
        /// <param name="setUp">Optional set-up code to run before the test, or null if none</param>
        /// <param name="tearDown">Optional tear-down code to run after the test, or null if none</param>
        /// <seealso cref="DynamicTestFactoryAttribute" />
        [TestFrameworkInternal]
        protected abstract TestOutcome RunDynamicTest(ICodeElementInfo declaringCodeElement, Action setUp, Action tearDown);
    }
}
