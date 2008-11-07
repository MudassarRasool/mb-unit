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

using System.Collections.Generic;
using System.ComponentModel;
using Gallio.Icarus.Controllers.Interfaces;
using Gallio.Icarus.Mediator.Interfaces;
using Gallio.Runner.Projects;
using MbUnit.Framework;
using Rhino.Mocks;

namespace Gallio.Icarus.Tests
{
    [MbUnit.Framework.Category("Views")]
    class FiltersWindowTest : MockTest
    {
        [Test]
        public void Constructor_Test()
        {
            var projectController = mocks.StrictMock<IProjectController>();
            var mediator = mocks.StrictMock<IMediator>();
            Expect.Call(mediator.ProjectController).Return(projectController);
            Expect.Call(projectController.TestFilters).Return(new BindingList<FilterInfo>(new List<FilterInfo>()));
            ITestController testController = mocks.StrictMock<ITestController>();
            mocks.ReplayAll();
            FiltersWindow filtersWindow = new FiltersWindow(mediator);
        }
    }
}
