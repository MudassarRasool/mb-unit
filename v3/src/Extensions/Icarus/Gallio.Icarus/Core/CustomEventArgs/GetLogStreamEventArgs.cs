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
using System.Windows.Forms;

using Gallio.Model;
using Gallio.Model.Filters;

namespace Gallio.Icarus.Core.CustomEventArgs 
{
    public class GetLogStreamEventArgs : EventArgs
    {
        private readonly string logStream;
        private readonly string testId;

        public GetLogStreamEventArgs(string logStream, string testId)
        {
            this.logStream = logStream;
            this.testId = testId;
        }

        public string LogStream
        {
            get { return logStream; }
        }

        public string TestId
        {
            get { return testId; }
        }
    }
}
