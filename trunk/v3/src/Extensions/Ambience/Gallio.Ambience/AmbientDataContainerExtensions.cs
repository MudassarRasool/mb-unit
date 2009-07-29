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
using System.Linq;
using System.Text;
using Db4objects.Db4o.Linq;
using Gallio.Ambience.Impl;

namespace Gallio.Ambience
{
    /// <summary>
    /// Extension methods for LINQ syntax over Ambient data containers.
    /// </summary>
    public static class AmbientDataContainerExtensions
    {
        /// <summary>
        /// Obtains a query over a data container.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Client code will not call this method directly.  However, it turns out that
        /// the C# compiler will add an implicit call to this method when it attempts to
        /// select a value from the container using LINQ syntax.
        /// </para>
        /// </remarks>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="container">The container.</param>
        /// <returns>The query object.</returns>
        public static IAmbientDataQuery<T> Cast<T>(this IAmbientDataContainer container)
        {
            var db4oContainer = (Db4oAmbientDataContainer)container;
            return new Db4oAmbientDataQuery<T>(db4oContainer.Inner.Cast<T>());
        }
    }
}