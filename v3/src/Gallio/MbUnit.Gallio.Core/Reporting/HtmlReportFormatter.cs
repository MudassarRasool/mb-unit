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

using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using Castle.Core;
using MbUnit.Core.Reporting.Resources;
using MbUnit.Core.Utilities;
using MbUnit.Framework.Kernel.Events;

namespace MbUnit.Core.Reporting
{
    /// <summary>
    /// <para>
    /// Formats reports as Html.
    /// </para>
    /// <para>
    /// Recognizes the following options:
    /// <list type="bullet">
    /// <listheader>
    /// <term>Option</term>
    /// <description>Description</description>
    /// </listheader>
    /// <item>
    /// <term>SaveAttachmentContents</term>
    /// <description>If <c>"true"</c>, saves the attachment contents.
    /// If <c>"false"</c>, discards the attachment altogether.
    /// Defaults to <c>"true"</c>.</description>
    /// </item>
    /// </list>
    /// </para>
    /// </summary>
    [Singleton]
    public class HtmlReportFormatter : XsltReportFormatter
    {
        /// <summary>
        /// Gets the name of this formatter.
        /// </summary>
        public const string FormatterName = @"HTML";

        /// <inheritdoc />
        public override string Name
        {
            get { return FormatterName; }
        }

        /// <inheritdoc />
        public override string PreferredExtension
        {
            get { return @"html"; }
        }

        /// <inheritdoc />
        protected override void ApplyTransform(Report report, string filename, NameValueCollection options, IList<string> filesWritten)
        {
            base.ApplyTransform(report, filename, options, filesWritten);

            string contentDirectory = ReportUtils.GetContentDirectoryPath(filename);
            if (! Directory.Exists(contentDirectory))
                Directory.CreateDirectory(contentDirectory);

            foreach (string imageResourceName in ReportingResources.Images)
            {
                using (Stream stream = ReportingResources.GetResource(imageResourceName))
                    FileUtils.CopyStreamToFile(stream, Path.Combine(contentDirectory, imageResourceName));
            }
        }

        /// <inheritdoc />
        protected override XmlReader GetStylesheetReader()
        {
            return XmlReader.Create(ReportingResources.GetResource(ReportingResources.HtmlTemplate));
        }
    }
}