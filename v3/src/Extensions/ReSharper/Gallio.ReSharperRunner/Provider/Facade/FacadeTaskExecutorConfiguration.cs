// Copyright 2005-2008 Gallio Project - http://www.gallio.org/
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

namespace Gallio.ReSharperRunner.Provider.Facade
{
    /// <summary>
    /// Specifies task execution configuration information.
    /// </summary>
    /// <remarks>
    /// This type is part of a facade that decouples the Gallio test runner from the ReSharper interfaces.
    /// </remarks>
    [Serializable]
    public class FacadeTaskExecutorConfiguration
    {
        /// <summary>
        /// Gets or sets whether task assemblies should be shadow-copied.
        /// </summary>
        public bool ShadowCopy { get; set; }

        /// <summary>
        /// Gets or sets the folder to use as the base directory for task assemblies.
        /// Or null if the project's assembly should be used.
        /// </summary>
        public string AssemblyFolder { get; set; }
    }
}
