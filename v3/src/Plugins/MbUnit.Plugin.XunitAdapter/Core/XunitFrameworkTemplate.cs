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
using System.Reflection;
using MbUnit.Framework.Kernel.DataBinding;
using MbUnit.Framework.Kernel.Model;
using MbUnit.Plugin.XunitAdapter.Properties;

namespace MbUnit.Plugin.XunitAdapter.Core
{
    /// <summary>
    /// The Xunit framework template.  Since Xunit does not support test
    /// parameters, we do not fully decompose the tests into a template tree.
    /// Instead, this template takes care of building all of the tests as needed.
    /// </summary>
    public class XunitFrameworkTemplate : BaseTemplate
    {
        private readonly IList<Assembly> assemblies;

        /// <summary>
        /// Initializes a template initially without a parent.
        /// </summary>
        /// <param name="frameworkVersion">The framework version</param>
        /// <param name="assemblies">The list of assemblies</param>
        public XunitFrameworkTemplate(Version frameworkVersion, IList<Assembly> assemblies)
            : base(String.Format(Resources.XunitFrameworkTemplate_FrameworkTemplateName, frameworkVersion), CodeReference.Unknown)
        {
            this.assemblies = assemblies;

            Kind = ComponentKind.Framework;
            IsGenerator = true;
        }

        /// <summary>
        /// Gets the list of assemblies.
        /// </summary>
        public IList<Assembly> Assemblies
        {
            get { return assemblies; }
        }

        /// <inheritdoc />
        public override ITemplateBinding Bind(TemplateBindingScope scope, IDictionary<ITemplateParameter, IDataFactory> arguments)
        {
            return new XunitFrameworkTemplateBinding(this, scope, arguments);
        }
    }
}
