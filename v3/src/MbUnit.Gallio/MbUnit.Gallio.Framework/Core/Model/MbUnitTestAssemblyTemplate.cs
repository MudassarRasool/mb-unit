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
using MbUnit.Framework.Model.Metadata;
using MbUnit.Framework.Model;

namespace MbUnit.Framework.Core.Model
{
    /// <summary>
    /// Represents a template derived from an MbUnit test assembly.
    /// </summary>
    public class MbUnitTestAssemblyTemplate : MbUnitTestTemplate
    {
        private MbUnitTestFrameworkTemplate frameworkTemplate;
        private List<MbUnitTestFixtureTemplate> fixtureTemplates;

        /// <summary>
        /// Initializes an MbUnit test assembly template model object.
        /// </summary>
        /// <param name="frameworkTemplate">The containing framework template</param>
        /// <param name="assembly">The test assembly</param>
        public MbUnitTestAssemblyTemplate(MbUnitTestFrameworkTemplate frameworkTemplate, Assembly assembly)
            : base(assembly.GetName().Name, CodeReference.CreateFromAssembly(assembly))
        {
            this.frameworkTemplate = frameworkTemplate;

            fixtureTemplates = new List<MbUnitTestFixtureTemplate>();
            Kind = TemplateKind.Assembly;
        }

        /// <inheritdoc />
        public override IEnumerable<ITestTemplate> Children
        {
            get
            {
                foreach (MbUnitTestFixtureTemplate fixtureTemplate in fixtureTemplates)
                    yield return fixtureTemplate;
            }
        }

        /// <summary>
        /// Gets the containing framework template.
        /// </summary>
        public MbUnitTestFrameworkTemplate FrameworkTemplate
        {
            get { return frameworkTemplate; }
        }

        /// <summary>
        /// Gets the test assembly.
        /// </summary>
        public Assembly Assembly
        {
            get { return CodeReference.Assembly; }
        }

        /// <summary>
        /// Gets the list of fixture templates.
        /// </summary>
        public IList<MbUnitTestFixtureTemplate> FixtureTemplates
        {
            get { return fixtureTemplates; }
        }

        /// <summary>
        /// Adds a test fixture template as a child of the assembly.
        /// </summary>
        /// <param name="fixtureTemplate">The test fixture template</param>
        public void AddFixtureTemplate(MbUnitTestFixtureTemplate fixtureTemplate)
        {
            ModelUtils.LinkTemplate(this, fixtureTemplates, fixtureTemplate);
        }

        /*
        public void AddAssemblySetUp()
        {
        }

        public void AddAssemblyTearDown()
        {
        }
         */
    }
}
