using System;
using MbUnit.Core.Serialization;

namespace MbUnit.Core.Runner
{
    /// <summary>
    /// <para>
    /// The test domain interface provides services for interacting with tests
    /// hosted in an isolated domain (presumably in its own AppDomain).  Where the
    /// domain is hosted is irrelvant.  It might be in an AppDomain within the same
    /// process or it might reside in another process altogether.
    /// </para>
    /// <para>
    /// Test domain implementations should be designed to permit interoperability of
    /// the host application with different versions of MbUnit linked to the code modules
    /// being tested.  For this reason, the test domain API only exposes objects that
    /// are serializable by value or interfaces for which proxies can be easily
    /// constructed with efficient interfaces that require few round-trips.
    /// </para>
    /// <para>
    /// Test domain implementations based on remote calls should implement the test
    /// domain as a proxy over the real remote interface rather than, for instance,
    /// subclassing <see cref="MarshalByRefObject" /> and supplying the application
    /// with a transparent proxy to be used directly.  The test domain implementation
    /// should protect the main application from configuration concerns and failure
    /// conditions resulting from the use of remoting internally.
    /// </para>
    /// <para>
    /// Calling <see cref="IDisposable.Dispose" /> should never throw an exception.
    /// </para>
    /// </summary>
    /// <remarks>
    /// This interface is not thread-safe.
    /// </remarks>
    public interface ITestDomain : IDisposable
    {
        /// <summary>
        /// Adds or removes a progress event handler to receive notifications
        /// of operations that are in progress.
        /// </summary>
        //event EventHandler<ProgressEventArgs> Progress;

        /// <summary>
        /// Gets the currently loaded test project, or null if none.
        /// </summary>
        /// <exception cref="TestDomainException">Thrown if an error occurs</exception>
        /// <exception cref="ObjectDisposedException">Thrown if the domain has been disposed</exception>
        TestProjectInfo TestProject { get; }

        /// <summary>
        /// Loads a test project into the test domain.
        /// </summary>
        /// <param name="project">The test project to load</param>
        /// <exception cref="TestDomainException">Thrown if an error occurs</exception>
        /// <exception cref="ObjectDisposedException">Thrown if the domain has been disposed</exception>
        void LoadProject(TestProjectInfo project);

        /// <summary>
        /// Unloads the current test project so that the test domain can
        /// be recycled for use with a different test project.
        /// </summary>
        /// <exception cref="TestDomainException">Thrown if an error occurs</exception>
        /// <exception cref="ObjectDisposedException">Thrown if the domain has been disposed</exception>
        void UnloadProject();

        /// <summary>
        /// Builds a tree of test templates and gets the root template.
        /// </summary>
        /// <remarks>
        /// The tree may be cached for use with subsequent operations until the project
        /// is unloaded.  Building the tree may be performed implicitly at other times also.
        /// </remarks>
        /// <returns>The root template</returns>
        /// <exception cref="TestDomainException">Thrown if an error occurs</exception>
        /// <exception cref="ObjectDisposedException">Thrown if the domain has been disposed</exception>
        TestTemplateInfo GetTestTemplateTreeRoot();
    }
}
