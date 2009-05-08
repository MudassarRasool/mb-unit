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

using Gallio.Common.Collections;
using Gallio.Framework;
using MbUnit.Framework;

namespace Gallio.Tests.Framework
{
    [TestFixture]
    [TestsOn(typeof(TestContext))]
    public class TestContextTest
    {
        [Test]
        public void CurrentTestHasCorrectTestName()
        {
            Assert.AreEqual("CurrentTestHasCorrectTestName", TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CanStoreDataInTestContext()
        {
            Key<string> key = new Key<string>("key");
            TestContext.CurrentContext.Data.SetValue(key, "value");
            Assert.AreEqual("value", TestContext.CurrentContext.Data.GetValue(key));
        }
    }
}