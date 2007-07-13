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

// MbUnit Test Framework
// 
// Copyright (c) 2004 Jonathan de Halleux
//
// This software is provided 'as-is', without any express or implied warranty. 
// 
// In no event will the authors be held liable for any damages arising from 
// the use of this software.
// Permission is granted to anyone to use this software for any purpose, 
// including commercial applications, and to alter it and redistribute it 
// freely, subject to the following restrictions:
//
//		1. The origin of this software must not be misrepresented; 
//		you must not claim that you wrote the original software. 
//		If you use this software in a product, an acknowledgment in the product 
//		documentation would be appreciated but is not required.
//
//		2. Altered source versions must be plainly marked as such, and must 
//		not be misrepresented as being the original software.
//
//		3. This notice may not be removed or altered from any source 
//		distribution.
//		
//		MbUnit HomePage: http://www.mbunit.org
//		Author: Jonathan de Halleux

using System;

namespace MbUnit.Core.Runner.CommandLine
{
    /// <summary>
    /// Useful Stuff.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Command line parsing code from Peter Halam, 
    /// http://www.gotdotnet.com/community/usersamples/details.aspx?sampleguid=62a0f27e-274e-4228-ba7f-bc0118ecc41e
    /// </para>
    /// </remarks>
    public sealed class CommandLineUtility
    {
        /// <summary>
        /// The System Defined new line string.
        /// </summary>
        public const string NewLine = "\r\n";
        
        /// <summary>
        /// Don't ever call this.
        /// </summary>
        private CommandLineUtility() {}
        
        /// <summary>
        /// Parses Command Line Arguments. 
        /// Errors are output on Console.Error.
        /// Use CommandLineArgumentAttributes to control parsing behaviour.
        /// </summary>
        /// <param name="arguments"> The actual arguments. </param>
        /// <param name="destination"> The resulting parsed arguments. </param>
        public static void ParseCommandLineArguments(string [] arguments, object destination)
        {
            ParseCommandLineArguments(arguments, destination, new ErrorReporter(Console.Error.WriteLine));
        }
        
        /// <summary>
        /// Parses Command Line Arguments. 
        /// Use CommandLineArgumentAttributes to control parsing behaviour.
        /// </summary>
        /// <param name="arguments"> The actual arguments. </param>
        /// <param name="destination"> The resulting parsed arguments. </param>
        /// <param name="reporter"> The destination for parse errors. </param>
        public static void ParseCommandLineArguments(string[] arguments, object destination, ErrorReporter reporter)
        {
            CommandLineArgumentParser parser = new CommandLineArgumentParser(destination.GetType(), reporter);
            if (!parser.Parse(arguments, destination))
				throw new Exception("Parsing failed");
        }

        /// <summary>
        /// Returns a Usage string for command line argument parsing.
        /// Use CommandLineArgumentAttributes to control parsing behaviour.
        /// </summary>
        /// <param name="argumentType"> The type of the arguments to display usage for. </param>
        /// <returns> Printable string containing a user friendly description of command line arguments. </returns>
        public static string CommandLineArgumentsUsage(Type argumentType)
        {
			CommandLineArgumentParser parser = new CommandLineArgumentParser(argumentType, null);
			return parser.Usage;
        }
    }
}
