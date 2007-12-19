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

using System.Reflection;
using Gallio.Model.Reflection;

namespace Gallio.ReSharperRunner.Reflection
{
    /// <summary>
    /// Resolves assembly references to loaded assemblies.
    /// </summary>
    internal interface IAssemblyResolver
    {
        /// <summary>
        /// Resolves an assembly reflection wrapper to an assembly by loading it from disk
        /// if necessary.
        /// </summary>
        /// <param name="assemblyInfo">The assembly info</param>
        /// <returns>The resolved assembly</returns>
        Assembly ResolveAssembly(IAssemblyInfo assemblyInfo);
    }
}
