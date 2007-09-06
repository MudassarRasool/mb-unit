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

extern alias MbUnit2;
using System;
using MbUnit.Core.ConsoleSupport.CommandLine;
using MbUnit2::MbUnit.Framework;

namespace MbUnit.Core.Tests.ConsoleSupport.CommandLine
{
    [TestFixture]
    public class CommandLineArgumentParserTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void OnlyOneDefaultCommandLineArgumentIsAllowed()
        {
            new CommandLineArgumentParser(new MainArgumentsDuplicateDefaultCommandLineArgumentStub().GetType(), null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CommandLineArgumentOnlyWithUniqueShortNameIsAllowed()
        {
            new CommandLineArgumentParser(new MainArgumentsDuplicateShortNameStub().GetType(), null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CommandLineArgumentOnlyWithUniqueLongNameIsAllowed()
        {
            new CommandLineArgumentParser(new MainArgumentsDuplicateLongNameStub().GetType(), null);
        }

        [RowTest]
        [Row("/invalid")]
        [Row("-invalid")]
        public void ParseInvalidArgument(string arg)
        {
            string errorMsg = string.Empty;
            MainArguments arguments = new MainArguments();
            CommandLineArgumentParser parser = new CommandLineArgumentParser(arguments.GetType(), delegate(string message)
            { errorMsg = message; });
            Assert.AreEqual(false, parser.Parse(new string[] { arg }, arguments));
            Assert.AreEqual(string.Format("Unrecognized command line argument '{0}'", arg), errorMsg);
        }

        [Test]
        public void ParseInvalidValueForBooleanArgument()
        {
            string errorMsg = string.Empty;
            MainArguments arguments = new MainArguments();
            CommandLineArgumentParser parser = new CommandLineArgumentParser(arguments.GetType(), delegate(string message)
            { errorMsg = message; });
            Assert.AreEqual(false, parser.Parse(new string[] { "/help:bad" }, arguments));
            Assert.AreEqual("'bad' is not a valid value for the 'help' command line option", errorMsg);
        }

        [RowTest]
        [Row("/help", "/help")]
        [Row("/help+", "/help")]
        public void ParseDuplicatedArgument(string arg1, string arg2)
        {
            string errorMsg = string.Empty;
            MainArguments arguments = new MainArguments();
            CommandLineArgumentParser parser = new CommandLineArgumentParser(arguments.GetType(), delegate(string message)
            { errorMsg = message; });
            Assert.AreEqual(false, parser.Parse(new string[] { arg1, arg2 }, arguments));
            Assert.AreEqual("Duplicate 'help' argument", errorMsg);
        }
    }

    public class MainArguments
    {
        [DefaultCommandLineArgument(
            CommandLineArgumentFlags.MultipleUnique,
            LongName = "assemblies",
            Description = "List of assemblies containing the tests."
            )]
        public string[] Assemblies;

        [CommandLineArgument(
         CommandLineArgumentFlags.AtMostOnce,
         ShortName = "h",
         LongName = "help",
         Description = "Display this help text"
         )]
        public bool Help = false;

        [CommandLineArgument(
        CommandLineArgumentFlags.MultipleUnique,
        ShortName = "hd",
        LongName = "hint-directories",
        Description = "The list of directories used for loading assemblies and other dependent resources."
        )]
        public string[] HintDirectories;
    }

    public class MainArgumentsDuplicateDefaultCommandLineArgumentStub : MainArguments
    {
        [DefaultCommandLineArgument(
            CommandLineArgumentFlags.MultipleUnique,
            LongName = "duplicate",
            Description = "duplicated default command line argument"
            )]
        public string[] DuplicateDefault = null;
    }

    public class MainArgumentsDuplicateLongNameStub : MainArguments
    {
        [CommandLineArgument(
            CommandLineArgumentFlags.MultipleUnique,
            ShortName = "unique",
           LongName = "help",
           Description = "Duplicated long name."
         )]
        public string[] DuplicateLongName = null;
    }

    public class MainArgumentsDuplicateShortNameStub : MainArguments
    {
        [CommandLineArgument(
            CommandLineArgumentFlags.MultipleUnique,
           ShortName = "h",
            LongName = "long name",
            Description = "Duplicated short name."
         )]
        public string[] DuplicateShortName = null;
    }
}