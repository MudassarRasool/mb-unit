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
using System.Collections.Generic;
using System.Text;
using MbUnit.Framework.Collections;

namespace MbUnit.Framework.Services.Reports
{
    /// <summary>
    /// A lazily-populated collection of report streams for the report.
    /// Streams are automatically created on demand if no stream with the specified name
    /// exists at the time of the request.
    /// </summary>
    /// <remarks>
    /// The operations on this interface are thread-safe.
    /// </remarks>
    /// <seealso cref="IReport"/>, <seealso cref="IReportStream"/>
    public interface IReportStreamCollection
    {
        /// <summary>
        /// Gets the report stream with the specified name.  If the stream
        /// does not exist, it is created on demand.
        /// </summary>
        /// <remarks>
        /// This property may return different instances of <see cref="IReportStream" />
        /// each time it is called but they always represent the same stream just the same.
        /// </remarks>
        /// <param name="streamName">The name of the report stream</param>
        /// <returns>The report stream</returns>
        IReportStream this[string streamName] { get; }
    }
}
