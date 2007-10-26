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
using System.Globalization;
using System.Reflection;
using MbUnit.Collections;
using MbUnit.Core.ProgressMonitoring;
using MbUnit.Hosting;
using MbUnit.Runner;
using MbUnit.Model;
using MbUnit.Model.Filters;
using MbUnit.Runner.Reports;
using MbUnit.Tasks.NAnt.Properties;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Types;

namespace MbUnit.Tasks.NAnt
{
    /// <summary>
    /// A NAnt task that provides support for running MbUnit tests.
    /// </summary>
    /// <remarks>
    /// In order for NAnt to find this task, either the MbUnit.Tasks.NAnt.dll assembly needs
    /// to be put in NAnt's bin folder, or it must be loaded with the loadtasks directive:
    /// <code>
    /// <![CDATA[
    ///    <loadtasks assembly="[pathtoassembly]\MbUnit.Tasks.NAnt.dll" />
    /// ]]>
    /// </code>
    /// </remarks>
    /// <example>
    /// The following code is an example build file that shows how to load the task, specify the test assemblies
    /// and set some of the task's properties:
    /// <code>
    /// <![CDATA[
    ///    <?xml version="1.0" ?>
    ///    <project name="TestProject" default="RunMbUnit">
    ///    <!-- This is needed by NAnt to locate the MbUnit task -->
    ///    <loadtasks assembly="[pathtoassembly]\MbUnit.Tasks.NAnt.dll" />
    ///    <target name="RunMbUnit">
    ///     <mbunit result-property="ExitCode" failonerror="false" filter="Type=SomeFixture" >
    ///      <assemblies>
    ///        <!-- Specify the tests assemblies -->
    ///        <include name="[Path-to-test-assembly1]/TestAssembly1.dll" />
    ///        <include name="[Path-to-test-assembly2]/TestAssembly2.dll" />
    ///      </assemblies>
    ///     </mbunit>
    ///     <fail if="${ExitCode != '0'}" >The return code should have been 0!</fail>
    ///    </target>
    ///
    ///    </project>
    /// ]]>
    /// </code>
    /// </example>
    [TaskName(@"mbunit")]
    public class MbUnitTask : Task, INAntLogger
    {
        #region Private Members

        private FileSet[] assemblies;
        private DirSet[] pluginDirectories;
        private DirSet[] hintDirectories;
        private string filter;
        private string reportTypes = @"";
        private string reportNameFormat = Resources.DefaultReportNameFormat;
        private string reportDirectory = String.Empty;
        private string resultProperty;
        private string resultPropertiesPrefix;
        private readonly INAntLogger nantLogger;

        #endregion

        #region Public Instance Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MbUnitTask" /> class.
        /// </summary>
        public MbUnitTask()
        {
            // This must be confusing, but in the unit tests for some reason the
            // Log method will fail with a NullReferenceException, so in order to
            // avoid that we make the task implement the INAntLogger interface and
            // also to contain a INAntLogger member. This way in the unit tests we
            // will log to a stubbed logger, and in the default case the calls will
            // be redirected to the task itself.
            nantLogger = this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MbUnitTask" /> class using
        /// a custom INAntLogger instance.
        /// </summary>
        /// <param name="nantLogger">The <see cref="INAntLogger" /> object that will be used
        /// to log messages.</param>
        public MbUnitTask(INAntLogger nantLogger)
        {
            if (nantLogger == null)
                throw new ArgumentNullException("nantLogger");
            // See the comments in the default constructor
            this.nantLogger = nantLogger;
        }

        #endregion

        #region Public Properties

        ///<summary>
        /// The list of test assemblies to execute. This is required.
        ///</summary>
        ///<example>The following example shows how to specify the test assemblies (for a more complete example
        /// please see the <see cref="MbUnitTask"/> task documentation):
        /// <code>
        /// <![CDATA[
        /// <mbunit>
        ///     <assemblies>
        ///         <!-- Specify the tests assemblies -->
        ///         <include name="[Path-to-test-assembly1]/TestAssembly1.dll" />
        ///         <include name="[Path-to-test-assembly2]/TestAssembly2.dll" />
        ///     </assemblies>
        /// </mbunit>
        /// ]]>
        /// </code>
        /// </example>
        [BuildElementArray("assemblies", Required = true, ElementType = typeof(FileSet))]
        public FileSet[] Assemblies
        {
            set { assemblies = value; }
        }

        /// <summary>
        /// The list of directories used for loading assemblies and other dependent resources.
        /// </summary>
        /// <example>The following example shows how to specify the hint directories:
        /// <code>
        /// <![CDATA[
        /// <mbunit>
        ///     <hint-directories>
        ///         <include name="C:\SomeFolder\AnotherFolder" />
        ///         <include name="../somefolder" />
        ///     </hint-directories>
        /// </mbunit>
        /// ]]>
        /// </code>
        /// </example>
        [BuildElementArray("hint-directories", ElementType = typeof(DirSet))]
        public DirSet[] HintDirectories
        {
            set { hintDirectories = value; }
        }

        /// <summary>
        /// Additional MbUnit plugin directories to search recursively.
        /// </summary>
        /// <example>The following example shows how to specify the plugins directories:
        /// <code>
        /// <![CDATA[
        /// <mbunit>
        ///     <plugin-directories>
        ///         <include name="C:\SomeFolder\AnotherFolder" />
        ///         <include name="../somefolder" />
        ///     </plugin-directories>
        /// </mbunit>
        /// ]]>
        /// </code>
        /// </example>
        [BuildElementArray("plugin-directories", ElementType = typeof(DirSet))]
        public DirSet[] PluginDirectories
        {
            set { pluginDirectories = value; }
        }

        /// <summary>
        /// A list of the types of reports to generate, separated by semicolons. 
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>The types supported "out of the box" are: Html, Html-Inline, Text, XHtml,
        /// XHtml-Inline, Xml, and Xml-Inline, but more types could be available as plugins.</item>
        /// <item>The report types are not case sensitives.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// In the following example reports will be generated in both HTML and XML format.
        /// <code>
        /// <![CDATA[
        /// <mbunit report-types="html;xml">
        ///     <!-- More options -->
        /// </mbunit>
        /// ]]>
        /// </code>
        /// </example>
        [TaskAttribute("report-types")]
        public string ReportTypes
        {
            set { reportTypes = value; }
        }

        /// <summary>
        /// Sets the format string to use to generate the reports filenames.
        /// </summary>
        /// <remarks>
        /// Any occurence of {0} will be replaced by the date, and any occurrence of {1} by the time.
        /// The default format string is mbunit-{0}-{1}.
        /// </remarks>
        [TaskAttribute("report-name-format", Required = false)]
        public string ReportNameFormat
        {
            set { reportNameFormat = value; }
        }

        /// <summary>
        /// Sets the name of the directory where the reports will be put.
        /// </summary>
        /// <remarks>
        /// The directory will be created if it doesn't exist. Existing files will be overwrited.
        /// </remarks>
        [TaskAttribute("report-directory", Required = false)]
        public string ReportDirectory
        {
            set { reportDirectory = value; }
        }

        /// <summary>
        /// Sets the name of a NAnt property in which the exit code of the tests execution
        /// should be stored.
        /// </summary>
        /// <remarks>
        /// Only of interest if FailOnError is set to false.
        /// </remarks>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// <target name="RunMbUnit">
        ///     <mbunit result-property="MbUnitExitCode" failonerror="false">
        ///         <!-- Include test assemblies -->
        ///     </mbunit>
        ///     <fail if="${MbUnitExitCode != 0}" >The return code should have been 0!</fail>
        /// </target>
        /// ]]>
        /// </code>
        /// </example>
        [TaskAttribute("result-property")]
        [StringValidator(AllowEmpty = false)]
        public string ResultProperty
        {
            set { resultProperty = value; }
        }

        /// <summary>
        /// Sets the prefix that will be used for the statistics result properties.
        /// </summary>
        /// <remarks>
        /// The following properties are available:
        /// <list type="bullet">
        /// <item><term>AssertCount</term><description>Gets the number of assertions evaluated.</description></item>
        /// <item><term>FailureCount</term><description>Gets the total number of test cases that were run and failed.</description></item>
        /// <item><term>IgnoreCount</term><description>Gets the total number of test cases that did not run because they were ignored.</description></item>
        /// <item><term>InconclusiveCount</term><description>Gets the total number of test cases that ran and were inconclusive.</description></item>
        /// <item><term>PassCount</term><description>Gets the total number of test cases that were run and passed.</description></item>
        /// <item><term>RunCount</term><description>Gets the total number of test cases that were run.</description></item>
        /// <item><term>SkipCount</term><description>Gets the total number of test cases that did not run because they were skipped.</description></item>
        /// <item><term>TestCount</term><description>Gets the total number of test cases.</description></item>
        /// </list>
        /// </remarks>
        /// <example>The following example shows how to use the result-properties-prefix property :
        /// <code>
        /// <![CDATA[
        /// <target name="RunMbUnit">
        ///     <mbunit result-properties-prefix="mbunit.">
        ///         <assemblies>
        ///             <include name="SomeAssembly.dll" />
        ///         </assemblies>
        ///     </mbunit>
        ///     <echo message="AssertCount = ${mbunit.AssertCount}" />
        ///     <echo message="FailureCount = ${mbunit.FailureCount}" />
        ///     <echo message="IgnoreCount = ${mbunit.IgnoreCount}" />
        ///     <echo message="InconclusiveCount = ${mbunit.InconclusiveCount}" />
        ///     <echo message="PassCount = ${mbunit.PassCount}" />
        ///     <echo message="RunCount = ${mbunit.RunCount}" />
        ///     <echo message="SkipCount = ${mbunit.SkipCount}" />
        ///     <echo message="TestCount = ${mbunit.TestCount}" />
        /// </target>
        /// ]]>
        /// </code>
        /// </example>
        [TaskAttribute("result-properties-prefix")]
        [StringValidator(AllowEmpty = false)]
        public string ResultPropertiesPrefix
        {
            set { resultPropertiesPrefix = value; }
        }

        /// <summary>
        /// Sets the filter to apply in the format "filterkey1=value1,value2;filterkey2=value3;...".
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <item>
        /// Currently the following filter keys are recognized:
        /// <list type="bullet">
        /// <item>Id: Filter by id</item>
        /// <item>Assembly: Filter by assembly name</item>
        /// <item>Namespace: Filter by namespace name</item>
        /// <item>Type: Filter by type name</item>
        /// <item>Member: Filter by member name</item>
        /// <item>*: All other names are assumed to correspond to the members defined in the <see cref="MetadataKeys" /> class.</item>
        /// </list>
        /// </item>
        /// <item>If this property is left empty then the "Any" filter will be applied.</item>
        /// </list>
        /// </remarks>
        /// <example>
        /// Assuming the following fixtures have been defined:
        /// <code>
        /// [TestFixture]
        /// [TestCategory("UnitTest")]
        /// [Author("AlbertEinstein")]
        /// public class Fixture1
        /// {
        ///     [Test]
        ///     public void Test1()
        ///     {
        ///     }
        ///     [Test]
        ///     public void Test2()
        ///     {
        ///     }
        /// }
        /// 
        /// [TestFixture]
        /// [TestCategory("IntegrationTest")]
        /// public class Fixture2
        /// {
        ///     [Test]
        ///     public void Test1()
        ///     {
        ///     }
        ///     [Test]
        ///     public void Test2()
        ///     {
        ///     }
        /// }
        /// </code>
        /// <para>The following filters could be applied:</para>
        /// <list type="bullet">
        /// <item>
        /// <term>filter="Type=Fixture1"</term>
        /// <description>Only tests within Fixture1 will be run (that is, Fixture1.Test1 and Fixture1.Test2).</description>
        /// </item>
        /// <item>
        /// <term>filter="Member=Test1"</term>
        /// <description>Only Fixture1.Test1 and Fixture2.Test1 will be run.</description>
        /// </item>
        /// <item>
        /// <term>filter="Type=Fixture1,Fixture2"</term>
        /// <description>All the tests within Fixture1 and Fixture2 will be run.</description>
        /// </item>
        /// <item>
        /// <term>filter="Type=Fixture1,Fixture2;Member=Test2"</term>
        /// <description>Only Fixture1.Test2 and Fixture2.Test2 will be run.</description>
        /// </item>
        /// <item>
        /// <term>filter="AuthorName=AlbertEinstein"</term>
        /// <description>All the tests within Fixture1 will be run (because its author attribute is set to "AlbertEinstein").</description>
        /// </item>
        /// </list>  
        /// </example>
        [TaskAttribute("filter")]
        public string Filter
        {
            set { filter = value; }
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        protected override void ExecuteTask()
        {
            // We don't catch exceptions here because NAnt takes care of that job,
            // and decides whether to let them through based on the value of the
            // FailOnError
            NAntLogger logger = new NAntLogger(nantLogger);

            DisplayVersion();

            using (TestLauncher launcher = new TestLauncher())
            {
                launcher.Logger = logger;
                launcher.ProgressMonitorProvider = new LogProgressMonitorProvider(logger);
                launcher.Filter = GetFilter();
                launcher.RuntimeSetup = new RuntimeSetup();

                AddAssemblies(launcher);
                AddHintDirectories(launcher);
                AddPluginDirectories(launcher);

                if (reportDirectory != null)
                    launcher.ReportDirectory = reportDirectory;
                if (!String.IsNullOrEmpty(reportNameFormat))
                    launcher.ReportNameFormat = reportNameFormat;
                if (reportTypes != null)
                    GenericUtils.AddAll(reportTypes.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries),
                        launcher.ReportFormats);

                TestLauncherResult result = RunLauncher(launcher);

                SetResultProperty(result.ResultCode);
                PopulateStatistics(result);

                if (FailOnError)
                {
                    if (result.ResultCode != ResultCode.Success && result.ResultCode != ResultCode.NoTests)
                    {
                        // The only way to make the task fail is to throw an exception
                        throw new BuildException(Resources.TestExecutionFailed);
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Provided so that the unit tests can override test execution behavior.
        /// </summary>
        protected virtual TestLauncherResult RunLauncher(TestLauncher launcher)
        {
            return launcher.Run();
        }

        private void PopulateStatistics(TestLauncherResult result)
        {
            PackageRunStatistics stats = result.Statistics;

            Properties[resultPropertiesPrefix + @"TestCount"] = stats.TestCount.ToString();
            Properties[resultPropertiesPrefix + @"PassCount"] = stats.PassCount.ToString();
            Properties[resultPropertiesPrefix + @"FailureCount"] = stats.FailureCount.ToString();
            Properties[resultPropertiesPrefix + @"IgnoreCount"] = stats.IgnoreCount.ToString();
            Properties[resultPropertiesPrefix + @"InconclusiveCount"] = stats.InconclusiveCount.ToString();
            Properties[resultPropertiesPrefix + @"RunCount"] = stats.RunCount.ToString();
            Properties[resultPropertiesPrefix + @"SkipCount"] = stats.SkipCount.ToString();
            Properties[resultPropertiesPrefix + @"Duration"] = stats.Duration.ToString();
            Properties[resultPropertiesPrefix + @"AssertCount"] = stats.AssertCount.ToString();
        }

        private Filter<ITest> GetFilter()
        {
            if (String.IsNullOrEmpty(filter))
            {
                return new AnyFilter<ITest>();
            }
            return FilterParser.ParseFilterList<ITest>(filter);
        }

        /// <summary>
        /// Checks the result code of the tests execution and performs the
        /// corresponding action.
        /// </summary>
        /// <param name="resultCode">The result code returned by the Run method of the
        /// TestRunnerHelper class.</param>
        private void SetResultProperty(IConvertible resultCode)
        {
            if (!String.IsNullOrEmpty(resultProperty))
            {
                Properties[resultProperty] = resultCode.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void DisplayVersion()
        {
            Version appVersion = Assembly.GetExecutingAssembly().GetName().Version;
            nantLogger.Log(Level.Info, String.Format(Resources.TaskNameAndVersion,
                appVersion.Major, appVersion.Minor, appVersion.Build));
        }

        private void AddAssemblies(TestLauncher launcher)
        {
            if (assemblies != null)
            {
                foreach (FileSet fs in assemblies)
                {
                    foreach (string f in fs.FileNames)
                        launcher.TestPackage.AssemblyFiles.Add(f);
                }
            }
        }

        private void AddHintDirectories(TestLauncher launcher)
        {
            if (hintDirectories != null)
            {
                foreach (DirSet ds in hintDirectories)
                {
                    foreach (string d in ds.DirectoryNames)
                        launcher.TestPackage.HintDirectories.Add(d);
                }
            }
        }

        private void AddPluginDirectories(TestLauncher launcher)
        {
            if (pluginDirectories != null)
            {
                foreach (DirSet ds in pluginDirectories)
                {
                    foreach (string d in ds.DirectoryNames)
                        launcher.RuntimeSetup.PluginDirectories.Add(d);
                }
            }
        }

        #endregion
    }
}
