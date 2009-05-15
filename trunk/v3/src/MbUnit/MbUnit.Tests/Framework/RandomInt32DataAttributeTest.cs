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

using System;
using System.Transactions;
using Gallio.Framework;
using Gallio.Common.Reflection;
using Gallio.Runner.Reports;
using Gallio.Tests;
using MbUnit.Framework;
using System.Linq;
using Gallio.Common.Markup;
using System.Text.RegularExpressions;

namespace MbUnit.Tests.Framework
{
    [TestsOn(typeof(RandomInt32DataAttribute))]
    [RunSample(typeof(RandomInt32DataSample))]
    public class RandomInt32DataAttributeTest : BaseTestWithSampleRunner
    {
        [Test]
        [Row("Single", -10, 10, 100)]
        public void EnumData(string testMethod, double expectedMinimum, double expectedMaximum, int expectedCount)
        {
            var run = Runner.GetPrimaryTestStepRun(CodeReference.CreateFromMember(typeof(RandomInt32DataSample).GetMethod(testMethod)));
            string[] lines = run.Children.Select(x => x.TestLog.GetStream(MarkupStreamNames.Default).ToString()).ToArray();
            Assert.AreEqual(expectedCount, lines.Length);

            foreach(string line in lines)
            {
                var match = Regex.Match(line, @"\[(?<value>-?\d+)\]");
                Assert.IsTrue(match.Success);
                int value = Int32.Parse(match.Groups["value"].Value);
                Assert.Between(value, expectedMinimum, expectedMaximum);
            }
        }

        [TestFixture, Explicit("Sample")]
        public class RandomInt32DataSample
        {
            [Test]
            public void Single([RandomInt32Data(Minimum = -10, Maximum = 10, Count = 100)] double value)
            {
                TestLog.WriteLine("[{0}]", value);
            }
        }
    }
}
