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

namespace Gallio.Runner.Domains
{
    /// <summary>
    /// <para>
    /// An isolated test domain contribution applies contributions to
    /// a <see cref="IsolatedTestDomain" /> prior to loading test
    /// projects.
    /// </para>
    /// <para>
    /// An example of a contribution is one that registers additional
    /// bootstrap assemblies with binding redirects for use inside the
    /// isolated test domain.
    /// </para>
    /// </summary>
    public interface IIsolatedTestDomainContributor
    {
        /// <summary>
        /// Applies contributions to the specified domain.
        /// </summary>
        /// <param name="domain">The domain to which contributions are applied</param>
        void Apply(IsolatedTestDomain domain);
    }
}