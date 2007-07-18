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
using System.Reflection;
using MbUnit.Framework.Kernel.Model;

namespace MbUnit.Framework.Kernel.Model
{
    /// <summary>
    /// Represents a template parameter.
    /// </summary>
    public class MbUnitTemplateParameter : BaseTemplateParameter
    {
        private Slot slot;

        /// <summary>
        /// Initializes an MbUnit test parameter model object.
        /// </summary>
        /// <param name="parameterSet">The parameter set</param>
        /// <param name="slot">The slot, non-null</param>
        public MbUnitTemplateParameter(MbUnitTemplateParameterSet parameterSet, Slot slot)
            : base(slot.Name, slot.CodeReference, parameterSet, slot.ValueType)
        {
            this.slot = slot;

            Index = slot.Position;
        }

        /// <inheritdoc />
        public new MbUnitTemplateParameterSet ParameterSet
        {
            get { return (MbUnitTemplateParameterSet) base.ParameterSet; }
            set { base.ParameterSet = value; }
        }

        /// <summary>
        /// Gets the associated slot.
        /// </summary>
        public Slot Slot
        {
            get { return slot; }
        }
    }
}
