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
    /// A NAnt task for launching MbUnit.
    /// </summary>
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
            // See the comments in the default constructor
            this.nantLogger = nantLogger;
        }

        #endregion

        #region Public Properties

        ///<summary>
        /// The list of test assemblies to execute. This is required.
        ///</summary>
        [BuildElementArray("assemblies", Required = true, ElementType = typeof(FileSet))]
        public FileSet[] Assemblies
        {
            get { return assemblies; }
            set { assemblies = value; }
        }

        /// <summary>
        /// The list of directories used for loading assemblies and other dependent resources.
        /// </summary>
        [BuildElementArray("hint-directories", ElementType = typeof(DirSet))]
        public DirSet[] HintDirectories
        {
            get { return hintDirectories; }
            set { hintDirectories = value; }
        }

        /// <summary>
        /// Additional MbUnit plugin directories to search recursively.
        /// </summary>
        [BuildElementArray("plugin-directories", ElementType = typeof(DirSet))]
        public DirSet[] PluginDirectories
        {
            get { return pluginDirectories; }
            set { pluginDirectories = value; }
        }

        ///<summary>
        /// An array of report types to generate.
        ///</summary>
        [TaskAttribute("report-types")]
        public string ReportTypes
        {
            get { return reportTypes; }
            set { reportTypes = value; }
        }

        ///<summary>
        /// A format string to use to generate the reports filename.
        ///</summary>
        [TaskAttribute("report-name-format", Required = false)]
        public string ReportNameFormat
        {
            get { return reportNameFormat; }
            set { reportNameFormat = value; }
        }

        ///<summary>
        /// The directory where the reports will be put.
        ///</summary>
        [TaskAttribute("report-directory", Required = false)]
        public string ReportDirectory
        {
            get { return reportDirectory; }
            set { reportDirectory = value; }
        }

        /// <summary>
        /// The name of a property in which the exit code of the tests execution
        /// should be stored. Only of interest if FailOnError is false.
        /// </summary>
        [TaskAttribute("result-property")]
        [StringValidator(AllowEmpty = false)]
        public string ResultProperty
        {
            get { return resultProperty; }
            set { resultProperty = value; }
        }

        /// <summary>
        /// The prefix that will be used for the statistics result properties.
        /// </summary>
        [TaskAttribute("result-properties-prefix")]
        [StringValidator(AllowEmpty = false)]
        public string ResultPropertiesPrefix
        {
            get { return resultPropertiesPrefix; }
            set { resultPropertiesPrefix = value; }
        }

        /// <summary>
        /// The filter to apply in the format "property=value;property=value;..."
        /// If left empty the "Any" filter will be applied.
        /// </summary>
        [TaskAttribute("filter")]
        public string Filter
        {
            get { return filter; }
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
            if (!String.IsNullOrEmpty(ResultProperty))
            {
                Properties[ResultProperty] = resultCode.ToString(CultureInfo.InvariantCulture);
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

            if (Assemblies != null)
            {
                foreach (FileSet fs in Assemblies)
                {
                    foreach (string f in fs.FileNames)
                        launcher.TestPackage.AssemblyFiles.Add(f);
                }
            }
        }

        private void AddHintDirectories(TestLauncher launcher)
        {
            if (HintDirectories != null)
            {
                foreach (DirSet ds in HintDirectories)
                {
                    foreach (string d in ds.FileNames)
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
                    foreach (string d in ds.FileNames)
                        launcher.RuntimeSetup.PluginDirectories.Add(d);
                }
            }
        }

        #endregion
    }
}
