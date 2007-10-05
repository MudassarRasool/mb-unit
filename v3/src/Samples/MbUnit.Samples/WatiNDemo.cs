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
using System.Text;
using System.Text.RegularExpressions;
using MbUnit.Framework;
using WatiN.Core;
using WatiN.Core.Interfaces;
using WatiN.Core.Logging;

namespace MbUnit.Samples
{
    [TestFixture]
    public class WatiNDemo
    {
        private IE ie;

        [SetUp]
        public void CreateBrowser()
        {
            ie = new IE();

            Logger.LogWriter = new WatiNStreamLogger();
        }

        [TearDown]
        public void DisposeBrowser()
        {
            Snapshot("Final Screen");

            if (ie != null)
                ie.Dispose();
        }

        [Test]
        public void Demo()
        {
            using (Log.BeginSection("Go to home page and sign in."))
            {
                ie.GoTo("http://www.ether.com");

                //ie.Link(Find.ByText("Sign In")).Click();
            }

            using (Log.BeginSection("Sign out."))
            {
                //ie.Link("Sign Out").Click();
            }
        }

        private void Snapshot(string attachmentName)
        {
            Log.EmbedImage(attachmentName, new CaptureWebPage(ie).CaptureWebPageImage(false, false, 100));
        }

        private class WatiNStreamLogger : ILogWriter
        {
            public void LogAction(string message)
            {
                Log.WriteLine(message);
            }
        }
    }
}
