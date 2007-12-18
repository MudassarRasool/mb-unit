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
using MbUnit.Framework;
using Gallio.PowerShellCommands;

namespace Gallio.PowerShellCommands.Tests
{
    [TestFixture]
    [Author("Julian Hidalgo")]
    [TestsOn(typeof(CmdletLogger))]
    [Category("UnitTests")]
    public class CmdletLoggerTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InstantiateLoggerWithNullArgument()
        {
            new CmdletLogger(null);
        }

        [Test]
        public void InstantiateLogger()
        {
            new CmdletLogger(new RunGallioCommand());
        }

        [Test]
        public void CreateChildLogger()
        {
            CmdletLogger logger = new CmdletLogger(new RunGallioCommand());
            Assert.AreSame(logger.CreateChildLogger("child").GetType(), typeof(CmdletLogger));
        }
    }
}
