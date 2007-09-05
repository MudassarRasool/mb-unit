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
using System.IO;
using System.Text;

namespace MbUnit.Core.ConsoleSupport.CommandLine
{
    ///<summary>
    /// Responsible for creating output.
    ///</summary>
    public class CommandLineOutput
    {
        private readonly TextWriter _output;
        private int _lineLength;

        ///<summary>
        /// Initializes new instance of CommandLineOutput.
        ///</summary>
        ///<param name="console">The console</param>
        public CommandLineOutput(IRichConsole console)
            : this(console.Out, console.Width)
        {
        }

        ///<summary>
        /// Initializes new instance of CommandLineOutput that outputs to specified stream.
        ///</summary>
        ///<param name="output"></param>
        public CommandLineOutput(TextWriter output)
        {
            _output = output;
            _lineLength = 80;
        }

        ///<summary>
        ///</summary>
        ///<param name="output"></param>
        ///<param name="width"></param>
        public CommandLineOutput(TextWriter output, int width)
        {
            _output = output;
            _lineLength = width;
        }

        ///<summary>
        /// Output Stream
        ///</summary>
        public TextWriter Output
        {
            get { return _output; }
        }

        ///<summary>
        /// Maximum line length allowed before the text will be wraped.
        ///</summary>
        public int LineLength
        {
            get { return _lineLength; }
            set { _lineLength = value; }
        }


        ///<summary>
        /// Prints out a new line.
        ///</summary>
        public void NewLine()
        {
            _output.WriteLine();
        }

        ///<summary>
        /// Outputs text with specified indentation.
        ///</summary>
        ///<param name="text">Text to output.</param>
        ///<param name="indentation">Number of blank spaces before the start of the text.</param>
        public void PrintText(string text, int indentation)
        {
            int maxLength = _lineLength - indentation - 1;
            while (text.Length > maxLength)
            {
                int pos = text.LastIndexOf(' ', maxLength);
                _output.Write(Space(indentation));
                _output.Write(text.Substring(0, pos));
                NewLine();
                text = text.Substring(pos + 1);
            }
            _output.Write(Space(indentation));
            _output.WriteLine(text);
        }

        ///<summary>
        /// Outputs text with specified indentation.
        ///</summary>
        ///<param name="text">Text to output.</param>
        ///<param name="firstLineIndent">Number of blank spaces before the start of the text.</param>
        ///<param name="indentation">Number of blank spaces before the start of the text.</param>
        private void PrintText(string text, int firstLineIndent, int indentation)
        {
            int maxLength = _lineLength - firstLineIndent - 1;
            if (text.Length > maxLength)
            {
                int pos = text.LastIndexOf(' ', maxLength);
                _output.Write(Space(firstLineIndent));
                _output.Write(text.Substring(0, pos));
                _output.WriteLine();
                text = text.Substring(pos + 1);
                PrintText(text, indentation);
            }
            else
                PrintText(text, firstLineIndent);
        }

        ///<summary>
        /// Output help for a specified argument.
        ///</summary>
        ///<param name="longName">Argument long name.</param>
        ///<param name="shortName">Argument short name.</param>
        ///<param name="description">Argument description.</param>
        ///<param name="valueType">Argument value type.</param>
        ///<param name="argumentType"></param>
        public void PrintArgumentHelp(string longName, string shortName, string description, string valueType, Type argumentType)
        {
            StringBuilder argumentHelp = new StringBuilder("/");
            argumentHelp.Append(longName);
            if (!string.IsNullOrEmpty(valueType))
            {
                argumentHelp.Append(":<");
                argumentHelp.Append(valueType);
                argumentHelp.Append(">");
            }
            if (argumentHelp.Length > 17)
            {
                PrintText(argumentHelp.ToString(), 2);
                PrintText(CreateDescriptionWithShortName(description, shortName, argumentType), 21);
            }
            else
            {
                argumentHelp.Append(Space(18 - longName.Length));
                argumentHelp.Append(CreateDescriptionWithShortName(description, shortName, argumentType));
                PrintText(argumentHelp.ToString(), 2, 21);
            }
        }

        private static string AddEnumerationValues(Type type)
        {
            StringBuilder enumDescription = new StringBuilder();
            if (type.IsArray)
                type = type.GetElementType();

            if (type.IsEnum)
            {
                enumDescription.Append(" The available options are ");
                string[] enumValues = Enum.GetNames(type);
                for (int ndx = 0; ndx < enumValues.Length; ndx++)
                {
                    enumDescription.Append("'");
                    enumDescription.Append(enumValues[ndx]);
                    enumDescription.Append("', ");
                }
                enumDescription.Replace(", ", ".", enumDescription.Length - 2, 2);
            }
            return enumDescription.ToString();
        }

        private static string Space(int spaceCount)
        {
            return new string(' ', spaceCount);
        }

        private static string CreateDescriptionWithShortName(string description, string shortName, Type argType)
        {
            return string.Format("{0}{1} (Short form: /{2})", description, AddEnumerationValues(argType), shortName);
        }
    }
}