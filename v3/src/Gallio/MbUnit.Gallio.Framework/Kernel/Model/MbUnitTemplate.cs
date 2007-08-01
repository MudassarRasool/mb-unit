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
using MbUnit.Framework.Kernel.Model;

namespace MbUnit.Framework.Kernel.Model
{
    /// <summary>
    /// Abstract base class for MbUnit-derived test method templates.
    /// </summary>
    public abstract class MbUnitTemplate : BaseTemplate
    {
        /// <summary>
        /// Initializes a template initially without a parent.
        /// </summary>
        /// <param name="name">The name of the component</param>
        /// <param name="codeReference">The point of definition</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/>
        /// or <paramref name="codeReference"/> is null</exception>
        public MbUnitTemplate(string name, CodeReference codeReference)
            : base(name, codeReference)
        {
        }

        /// <summary>
        /// This event is fired when a new template binding is created to allow
        /// MbUnit framework attributes to contribute to the test construction
        /// process performed by the binding.  Framework attributes will generally
        /// hook this event to capture the binding process then hook additional
        /// events on the bound template itself which is passed in as the parameter.
        /// </summary>
        public event Action<ITemplateBinding> ProcessBinding;

        /// <summary>
        /// Creates an anonymous parameter set associated with this template.
        /// </summary>
        /// <returns>The parameter set</returns>
        public MbUnitTemplateParameterSet CreateAnonymousParameterSet()
        {
            MbUnitTemplateParameterSet parameterSet = new MbUnitTemplateParameterSet(this, "", CodeReference);
            ParameterSets.Add(parameterSet);
            return parameterSet;
        }

        /// <summary>
        /// Sets the parameter set of a parameter by name.
        /// Automatically creates a parameter set if none with the specified name exists.
        /// Automatically deletes empty anonymous parameter sets.
        /// </summary>
        /// <param name="parameter">The parameter to move to a different parameter set</param>
        /// <param name="parameterSetName">The parameter set name</param>
        public void SetParameterSetName(MbUnitTemplateParameter parameter, string parameterSetName)
        {
            if (parameter.ParameterSet.Name == parameterSetName)
                return;

            if (!ParameterSets.Contains(parameter.ParameterSet))
                throw new InvalidOperationException("The parameter does not belong to any of this template's parameter sets.");

            // Remote the parameter from its old set.
            if (!parameter.ParameterSet.Parameters.Remove(parameter))
                throw new InvalidOperationException("The parameter does not belong to its own parameter set!");

            // Remove old empty anonymous parameter sets created during intermediate
            // stages of test enumeration.
            if (parameter.ParameterSet.Name.Length == 0 && parameter.ParameterSet.Parameters.Count == 0)
                ParameterSets.Remove(parameter.ParameterSet);

            // Add the parameter to its new set.
            ITemplateParameterSet parameterSet = GetParameterSetByName(parameterSetName);
            if (parameterSet == null)
            {
                parameterSet = new MbUnitTemplateParameterSet(this, parameterSetName, CodeReference);
                ParameterSets.Add(parameterSet);
            }

            parameterSet.Parameters.Add(parameter);
        }

        /// <inheritdoc />
        public override ITemplateBinding Bind(TestScope scope, IDictionary<ITemplateParameter, object> arguments)
        {
            MbUnitTemplateBinding binding = new MbUnitTemplateBinding(this, scope, arguments);

            if (ProcessBinding != null)
                ProcessBinding(binding);

            return binding;
        }
    }
}
