﻿using System;
using System.Diagnostics;
using System.IO;
using Gallio.Concurrency;
using Gallio.Hosting;
using NCover.Framework;

namespace Gallio.NCoverIntegration
{
    /// <summary>
    /// A <see cref="ProcessTask" /> that uses the NCover framework to
    /// start the process with NCover attached.
    /// </summary>
    /// <todo author="jeff">
    /// Support NCover configuration settings using the test runner options collection.
    /// </todo>
    public class NCoverProcessTask : ProcessTask
    {
        private ProfilerDriver driver;
        private ThreadTask waitForExitTask;
        
        /// <summary>
        /// Creates a process task.
        /// </summary>
        /// <param name="executablePath">The path of the executable executable</param>
        /// <param name="arguments">The arguments for the executable</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="executablePath"/>
        /// or <paramref name="arguments"/> is null</exception>
        public NCoverProcessTask(string executablePath, string arguments)
            : base(executablePath, arguments)
        {
        }

        /// <inheritdoc />
        protected override Process StartProcess(ProcessStartInfo startInfo)
        {
            string outputDirectory = startInfo.WorkingDirectory;

            ProfilerSettings settings = new ProfilerSettings();
            settings.CommandLineExe = startInfo.FileName;
            settings.CommandLineArgs = startInfo.Arguments;
            settings.WorkingDirectory = startInfo.WorkingDirectory;
            settings.NoLog = true;
            //settings.LogFile = Path.Combine(outputDirectory, "Coverage.log");
            settings.CoverageXml = Path.Combine(outputDirectory, "Coverage.xml");
            //settings.CoverageHtmlPath = Path.Combine(outputDirectory, "Coverage.html");
            settings.RegisterForUser = true;

            return RegisterAndStartProfiler(settings, startInfo.RedirectStandardOutput | startInfo.RedirectStandardError);
        }

        private Process RegisterAndStartProfiler(ProfilerSettings settings, bool redirectOutput)
        {
            driver = new ProfilerDriver(settings);

            if (settings.RegisterForUser)
                driver.RegisterProfilerForUser();

            driver.Start(redirectOutput);
            if (!driver.MessageCenter.WaitForProfilerReadyEvent())
                throw new HostException("Timed out waiting for the NCover profiler to become ready.");

            driver.ConfigureProfiler();
            driver.MessageCenter.SetDriverReadyEvent();

            if (!settings.NoLog)
                driver.StartLogging();

            waitForExitTask = new ThreadTask("NCover Profiler Wait for Exit", (Block) WaitForExit);
            waitForExitTask.Start();

            return driver.Process;
        }

        protected override void OnTerminated()
        {
            try
            {
                if (driver != null)
                {
                    if (driver.Process != null)
                    {
                        try
                        {
                            driver.Stop();
                        }
                        catch (InvalidOperationException)
                        {
                            // An invalid operation exception may occur when attempting to kill a process
                            // that has already stopped so we ignore it.
                        }
                    }

                    if (driver.Settings.RegisterForUser)
                        driver.UnregisterProfilerForUser();
                }
            }
            catch (Exception ex)
            {
                Panic.UnhandledException("An exception occurred while shutting down the NCover profiler.", ex);
            }
            finally
            {
                if (waitForExitTask != null)
                    waitForExitTask.Abort();

                driver = null;
                waitForExitTask = null;
                base.OnTerminated();
            }
        }

        private void WaitForExit()
        {
            try
            {
                ProfilerDriver cachedDriver = driver;
                if (cachedDriver == null)
                    return;

                cachedDriver.WaitForExit();
            }
            catch (Exception ex)
            {
                Panic.UnhandledException("An exception occurred while waiting for the NCover profiler to exit.", ex);
            }
        }
    }
}
