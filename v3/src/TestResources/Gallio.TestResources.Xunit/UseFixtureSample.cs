// Copyright 2008 MbUnit Project - http://www.mbunit.com/
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
using Xunit;

namespace Gallio.TestResources.Xunit
{
    /// <summary>
    /// A test fixture with before/after sections.
    /// </summary>
    public class UseFixtureSample : IUseFixture<UseFixtureSample.Fixture>
    {
        [Fact]
        public void Test1()
        {
            Console.WriteLine("Test1");
        }

        [Fact]
        public void Test2()
        {
            Console.WriteLine("Test2");
        }

        public void SetFixture(Fixture data)
        {
            Assert.NotNull(data);
        }

        public class Fixture : IDisposable
        {
            public Fixture()
            {
                Console.WriteLine("Before");
            }

            public void Dispose()
            {
                Console.WriteLine("After");
            }
        }
    }
}
