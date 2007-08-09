using Castle.Core.Logging;
using MbUnit.Core.Runner;

namespace MbUnit.Core.Runner
{
    /// <summary>
    /// Implementation of a IProgressMonitor that logs messages to a ConsoleLogger
    /// instance.
    /// </summary>
    public class RunnerProgressMonitor : TextualProgressMonitor
    {
        private readonly ConsoleLogger tddLogger = null;

        /// <summary>
        /// Initializes a new instance of the RunnerProgressMonitor class.
        /// </summary>
        /// <param name="logger">A ConsoleLogger instance where log messages will be
        /// channeled to.</param>
        public RunnerProgressMonitor(ConsoleLogger logger)
        {            
            tddLogger = logger;
        }

        private string previousTaskName = string.Empty;
        protected override void UpdateDisplay()
        {
            // We can't show progress in a convenient way when running 
            // within Visual Studio, so just inform when a new task
            // has begun.
            if (previousTaskName.CompareTo(TaskName) != 0)
            {
                previousTaskName = TaskName;
                tddLogger.Info(TaskName);
            }
        }
    }
}
