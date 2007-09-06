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
using MbUnit.Core.Harness;
using MbUnit.Framework.Kernel.Collections;
using MbUnit.Framework.Kernel.Utilities;
using MbUnit.Plugin.NUnitAdapter.Properties;

namespace MbUnit.Plugin.NUnitAdapter.Core
{
    /// <summary>
    /// Builds a test object model based on reflection against NUnit framework attributes.
    /// </summary>
    public class NUnitTestFramework : ITestFramework
    {
        /// <inheritdoc />
        public string Name
        {
            get { return Resources.NUnitTestFramework_NUnitFrameworkName; }
        }

        /// <inheritdoc />
        public void Apply(ITestHarness harness)
        {
            harness.BuildingTemplates += harness_BuildingTemplates;
        }

        private static void harness_BuildingTemplates(ITestHarness harness, EventArgs e)
        {
            MultiMap<Version, Assembly> map = ReflectionUtils.GetReverseAssemblyReferenceMap(harness.Assemblies, @"nunit.framework");
            foreach (KeyValuePair<Version, IList<Assembly>> entry in map)
            {
                // Add a framework template with suitable rules to populate tests using the
                // NUnit test enumerator.  We don't actually represent each test as a
                // template because we can't perform any interesting meta-operations
                // on them like binding test parameters or composing tests.
                Version frameworkVersion = entry.Key;
                NUnitFrameworkTemplate frameworkTemplate = new NUnitFrameworkTemplate(frameworkVersion, entry.Value);
                harness.TemplateTreeBuilder.Root.AddChild(frameworkTemplate);
            }
        }
    }
}
