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
using System.Windows.Forms;

using MbUnit.Core.ConsoleSupport;
using MbUnit.Hosting;
using MbUnit.Icarus.Adapter;
using MbUnit.Icarus.AdapterModel;
using MbUnit.Icarus.Core.Model;
using MbUnit.Icarus.Core.Presenter;
using MbUnit.Runner;

namespace MbUnit.Icarus
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread, LoaderOptimization(LoaderOptimization.MultiDomain)]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Runtime.Initialize(new RuntimeSetup());
            try
            {
                // parse command line arguments
                CommandLineArgumentParser argumentParser = new CommandLineArgumentParser(typeof(MainArguments));
                MainArguments arguments = new MainArguments();
                TestPackage testPackage = new TestPackage();
                if (argumentParser.Parse(args, arguments, delegate { }))
                {
                    testPackage.AssemblyFiles.AddRange(arguments.Assemblies);
                }

                // wire up model
                Main main = new Main();
                ProjectPresenter projectPresenter = new ProjectPresenter(new ProjectAdapter(main, new ProjectAdapterModel(), testPackage), 
                    new TestRunnerModel());

                Application.Run(main);
            }
            finally
            {
                Runtime.Shutdown();
            }
        }
    }
}