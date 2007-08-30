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
using MbUnit.Framework.Kernel.Metadata;
using MbUnit.Framework.Kernel.Model;

namespace MbUnit.Framework.Kernel.Model
{
    /// <summary>
    /// Represents a template derived from an MbUnit assembly.
    /// </summary>
    public class MbUnitAssemblyTemplate : MbUnitTemplate
    {
        private readonly MbUnitFrameworkTemplate frameworkTemplate;
        private readonly Assembly assembly;

        /// <summary>
        /// Initializes an MbUnit assembly template model object.
        /// </summary>
        /// <param name="frameworkTemplate">The containing framework template</param>
        /// <param name="assembly">The assembly from which the template was derived</param>
        public MbUnitAssemblyTemplate(MbUnitFrameworkTemplate frameworkTemplate, Assembly assembly)
            : base(assembly.GetName().Name, CodeReference.CreateFromAssembly(assembly))
        {
            this.frameworkTemplate = frameworkTemplate;
            this.assembly = assembly;

            Kind = ComponentKind.Assembly;
            IsGenerator = true;
        }

        /// <summary>
        /// Gets the containing framework template.
        /// </summary>
        public MbUnitFrameworkTemplate FrameworkTemplate
        {
            get { return frameworkTemplate; }
        }

        /// <summary>
        /// Gets the assembly from which the template was derived.
        /// </summary>
        public Assembly Assembly
        {
            get { return assembly; }
        }

        /// <summary>
        /// Gets the list of type templates that are children of this template.
        /// </summary>
        public IList<MbUnitTypeTemplate> TypeTemplates
        {
            get { return ModelUtils.FilterChildrenByType<ITemplate, MbUnitTypeTemplate>(this); }
        }

        /// <summary>
        /// Adds a type template as a child of this template.
        /// </summary>
        /// <param name="typeTemplate">The type template</param>
        public void AddTypeTemplate(MbUnitTypeTemplate typeTemplate)
        {
            AddChild(typeTemplate);
        }
    }
}
