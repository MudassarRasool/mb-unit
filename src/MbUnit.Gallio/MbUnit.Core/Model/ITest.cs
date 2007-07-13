using System;
using System.Collections.Generic;
using System.Text;
using MbUnit.Core.Serialization;

namespace MbUnit.Core.Model
{
    /// <summary>
    /// <para>
    /// A test object represents a single instance of a test that has been
    /// generated from a <see cref="ITestTemplate" /> using particular
    /// bindings in a particular scope.
    /// </para>
    /// <para>
    /// A test may depend on one or more other tests.  When a test
    /// fails, the tests that depend on it are also automatically
    /// considered failures.  Moreover, the test runner ensures
    /// that a test will only run once all of its dependencies have
    /// completed execution successfully.  A run-time error will
    /// occur when the system detects the presence of circular test dependencies
    /// or attempts to execute a test concurrently with its dependencies.
    /// </para>
    /// <para>
    /// A test may be decomposed into a tree of subtests.  The subtests
    /// encapsulate logical units of processing within a test.  This feature
    /// makes it easier to isolate individual verification activities that
    /// are performed as part of some larger scenario.  Subtests are executed
    /// in dependency order just like ordinary tests.
    /// </para>
    /// <para>
    /// A test is executed in isolation of other tests only insofar as they belong
    /// to disjoint (and mutually exclusive) scopes.  Thus tests belonging to the same
    /// assembly-level scope will not be executed in pure isolation; instead, they
    /// will share the environment established by their common ancestor as part of
    /// setup/teardown activities.  For example, if the common ancestor includes rules to
    /// set up and tear down a temporary database, then descendent tests may share the
    /// same database whereas tests in different assembly-level scopes generally will
    /// not (unless there are side-effects).
    /// </para>
    /// </summary>
    public interface ITest
    {
        /// <summary>
        /// Gets or sets the name of the test.
        /// </summary>
        /// <remarks>
        /// The name does not need to be globally unique.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null</exception>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent of this test, or null if this test
        /// is at the root of the test tree.
        /// </summary>
        ITest Parent { get; set; }

        /// <summary>
        /// Gets the children of this test.
        /// The children are considered subordinate to the parent.
        /// </summary>
        IEnumerable<ITest> Children { get; }

        /// <summary>
        /// Gets the list of the dependencies of this test.
        /// </summary>
        IList<ITest> Dependencies { get; }

        /// <summary>
        /// Gets the metadata of the test.
        /// </summary>
        MetadataMap Metadata { get; }

        /// <summary>
        /// Gets or sets a reference to the point of definition of this test in the code.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null</exception>
        CodeReference CodeReference { get; set; }

        /// <summary>
        /// Adds a child test.
        /// Sets the child's parent to this test as part of the addition process.
        /// </summary>
        /// <param name="test">The test to add</param>
        /// <exception cref="NotSupportedException">Thrown if the test does not support
        /// the addition of arbitrary children (because it has some more specific internal structure)</exception>
        void AddChild(ITest test);

        /// <summary>
        /// Gets a serializable description of the test.
        /// </summary>
        /// <returns>The test info</returns>
        TestInfo ToInfo();


        /*
        string Name { get; }

        ITestTemplateBinding TemplateBinding { get; }

        MetadataMap Metadata { get; }

        IList<ITest> Dependencies { get; }

        ITestPart SetUp { get; }

        ITestPart TearDown { get; }

        ITestPart...
         */
    }
}
