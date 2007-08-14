using System;
using System.Collections.Generic;
using System.Text;
using MbUnit.Framework.Kernel.Events;

namespace MbUnit.Core.Reporting
{
    /// <summary>
    /// The report manager provides services for manipulating reports.
    /// </summary>
    public interface IReportManager
    {
        /// <summary>
        /// Gets the names of all available report formatters.
        /// </summary>
        IList<string> GetFormatterNames();

        /// <summary>
        /// Gets a report formatter by name.
        /// </summary>
        /// <param name="name">The name of the report formatter, matched case-insensitively</param>
        /// <returns>The report formatter, or null if none exist with the specified name</returns>
        IReportFormatter GetFormatter(string name);

        /// <summary>
        /// Formats a report and saves it to a file.
        /// Overwrites the file and replaces associated resources if they exist.
        /// </summary>
        /// <seealso cref="IReportFormatter.Format"/> for important remarks.
        /// <param name="formatterName">The formatter name</param>
        /// <param name="report">The report to format</param>
        /// <param name="outputFilename">The name of the file to write including its extension</param>
        /// <param name="options">Custom options for the report formatter</param>
        /// <param name="progressMonitor">The progress monitor</param>
        /// <exception cref="ArgumentNullException">Thrown if any of the arguments are null</exception>
        /// <returns>The complete list of files that were written</returns>
        IList<string> Format(string formatterName, Report report, string outputFilename,
            Dictionary<string, string> options, IProgressMonitor progressMonitor);

        /// <summary>
        /// Saves the report as XML to the specified file.
        /// </summary>
        /// <param name="report">The report</param>
        /// <param name="filename">The filename to save</param>
        /// <param name="progressMonitor">The progress monitor</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="report"/>, <paramref name="filename"/>
        /// or <paramref name="progressMonitor"/> is null</exception>
        void SaveReport(Report report, string filename, IProgressMonitor progressMonitor);

        /// <summary>
        /// Loads the report from XML from the specified file.
        /// </summary>
        /// <param name="filename">The filename to save</param>
        /// <param name="progressMonitor">The progress monitor</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="filename"/> or <paramref name="progressMonitor "/> is null</exception>
        Report LoadReport(string filename, IProgressMonitor progressMonitor);
    }
}
