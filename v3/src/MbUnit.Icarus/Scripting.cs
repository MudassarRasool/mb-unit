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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Actions;
using ICSharpCode.TextEditor;

namespace MbUnit.Icarus
{
    public partial class Scripting : Form
    {
        public Scripting()
        {
            InitializeComponent();

            textEditorControl1.ShowEOLMarkers = false;
            textEditorControl1.ShowInvalidLines = false;
            textEditorControl1.ShowSpaces = false;
            textEditorControl1.ShowTabs = false;

            textEditorControl1.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("C#");

            textEditorControl1.Text = @"
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MbUnit.GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}";

        }
    }
}