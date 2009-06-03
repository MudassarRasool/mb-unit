// Copyright 2005-2009 Gallio Project - http://www.gallio.org/
// Portions Copyright 2000-2004 Jonathan de Halleux
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
using System.IO;
using System.Text;
using Gallio.Common.Collections;
using Gallio.Common.Reflection;

namespace Gallio.Runtime.Extensibility
{
    /// <summary>
    /// Provides a view of registered plugins along with methods for resolving them.
    /// </summary>
    public interface IPlugins : IEnumerable<IPluginDescriptor>
    {
        /// <summary>
        /// Gets a plugin descriptor by its id, or null if not found.
        /// </summary>
        /// <param name="pluginId">The plugin id.</param>
        /// <returns>The plugin descriptor, or null if not found</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="pluginId"/> is null.</exception>
        IPluginDescriptor this[string pluginId] { get; }
    }
}
