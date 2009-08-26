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
using System.Collections.Generic;
using System.Text;
using Gallio;
using Gallio.Framework.Assertions;
using MbUnit.Framework.Xml;
using System.Xml;
using System.IO;

namespace MbUnit.Framework
{
    public abstract partial class Assert
    {
        /// <summary>
        /// Assertions for XML data.
        /// </summary>
        public abstract class Xml
        {
            /// <summary>
            /// Asserts that two XML fragments have the same content.
            /// </summary>
            /// <param name="expectedXmlReader">A reader to get the expected XML fragment.</param>
            /// <param name="actualXmlReader">A reader to get the actual XML fragment.</param>
            /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise.</exception>
            public static void AreEqual(TextReader expectedXmlReader, TextReader actualXmlReader)
            {
                AreEqual(expectedXmlReader, actualXmlReader, XmlEqualityOptions.Default, null, null);
            }

            /// <summary>
            /// Asserts that two XML fragments have the same content.
            /// </summary>
            /// <param name="expectedXmlReader">A reader to get the expected XML fragment.</param>
            /// <param name="actualXmlReader">A reader to get the actual XML fragment.</param>
            /// <param name="options">Equality options.</param>
            /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise.</exception>
            public static void AreEqual(TextReader expectedXmlReader, TextReader actualXmlReader, XmlEqualityOptions options)
            {
                AreEqual(expectedXmlReader, actualXmlReader, options, null, null);
            }

            /// <summary>
            /// Asserts that two XML fragments have the same content.
            /// </summary>
            /// <param name="expectedXmlReader">A reader to get the expected XML fragment.</param>
            /// <param name="actualXmlReader">A reader to get the actual XML fragment.</param>
            /// <param name="messageFormat">The custom assertion message format, or null if none.</param>
            /// <param name="messageArgs">The custom assertion message arguments, or null if none.</param>
            /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise.</exception>
            public static void AreEqual(TextReader expectedXmlReader, TextReader actualXmlReader, string messageFormat, params object[] messageArgs)
            {
                AreEqual(expectedXmlReader, actualXmlReader, XmlEqualityOptions.Default, messageFormat, messageArgs);
            }

            /// <summary>
            /// Asserts that two XML fragments have the same content.
            /// </summary>
            /// <param name="expectedXmlReader">A reader to get the expected XML fragment.</param>
            /// <param name="actualXmlReader">A reader to get the actual XML fragment.</param>
            /// <param name="options">Equality options.</param>
            /// <param name="messageFormat">The custom assertion message format, or null if none.</param>
            /// <param name="messageArgs">The custom assertion message arguments, or null if none.</param>
            /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise.</exception>
            public static void AreEqual(TextReader expectedXmlReader, TextReader actualXmlReader, XmlEqualityOptions options, string messageFormat, params object[] messageArgs)
            {
                if (expectedXmlReader == null)
                    throw new ArgumentNullException("expectedXmlReader");
                if (actualXmlReader == null)
                    throw new ArgumentNullException("actualXmlReader");

                AreEqual(expectedXmlReader.ReadToEnd(), actualXmlReader.ReadToEnd(), options, messageFormat, messageArgs);
            }

            /// <summary>
            /// Asserts that two XML fragments have the same content.
            /// </summary>
            /// <param name="expectedXml">The expected XML fragment.</param>
            /// <param name="actualXml">The actual XML fragment.</param>
            /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise.</exception>
            public static void AreEqual(string expectedXml, string actualXml)
            {
                AreEqual(expectedXml, actualXml, XmlEqualityOptions.Default, null, null);
            }

            /// <summary>
            /// Asserts that two XML fragments have the same content.
            /// </summary>
            /// <param name="expectedXml">The expected XML fragment.</param>
            /// <param name="actualXml">The actual XML fragment.</param>
            /// <param name="options">Equality options.</param>
            /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise.</exception>
            public static void AreEqual(string expectedXml, string actualXml, XmlEqualityOptions options)
            {
                AreEqual(expectedXml, actualXml, options, null, null);
            }

            /// <summary>
            /// Asserts that two XML fragments have the same content.
            /// </summary>
            /// <param name="expectedXml">The expected XML fragment.</param>
            /// <param name="actualXml">The actual XML fragment.</param>
            /// <param name="messageFormat">The custom assertion message format, or null if none.</param>
            /// <param name="messageArgs">The custom assertion message arguments, or null if none.</param>
            /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise.</exception>
            public static void AreEqual(string expectedXml, string actualXml, string messageFormat, params object[] messageArgs)
            {
                AreEqual(expectedXml, actualXml, XmlEqualityOptions.Default, messageFormat, messageArgs);
            }

            /// <summary>
            /// Asserts that two XML fragments have the same content.
            /// </summary>
            /// <param name="expectedXml">The expected XML fragment.</param>
            /// <param name="actualXml">The actual XML fragment.</param>
            /// <param name="options">Equality options.</param>
            /// <param name="messageFormat">The custom assertion message format, or null if none.</param>
            /// <param name="messageArgs">The custom assertion message arguments, or null if none.</param>
            /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise.</exception>
            public static void AreEqual(string expectedXml, string actualXml, XmlEqualityOptions options, string messageFormat, params object[] messageArgs)
            {
                if (expectedXml == null)
                    throw new ArgumentNullException("expectedXml");
                if (actualXml == null)
                    throw new ArgumentNullException("actualXml");

                AssertionHelper.Verify(() =>
                {
                    Document actualDocument;
                    Document expectedDocument;

                    try
                    {
                        expectedDocument = new Parser(expectedXml).Run();
                    }
                    catch (XmlException exception)
                    {
                        return new AssertionFailureBuilder("Cannot parse the actual XML fragment.")
                            .AddException(exception)
                            .ToAssertionFailure();
                    }

                    try
                    {
                        actualDocument = new Parser(actualXml).Run();
                    }
                    catch (XmlException exception)
                    {
                        return new AssertionFailureBuilder("Cannot parse the expected XML fragment.")
                            .AddException(exception)
                            .ToAssertionFailure();
                    }

                    DiffSet diffSet = actualDocument.Diff(expectedDocument, MbUnit.Framework.Xml.Path.Empty, options);

                    if (diffSet.IsEmpty)
                        return null;

                    return new AssertionFailureBuilder("Expected XML fragments to be equal according to the specified options.")
                        .SetMessage(messageFormat, messageArgs)
                        .AddRawLabeledValue("Equality Options", options)
                        .AddInnerFailures(diffSet.ToAssertionFailures())
                        .ToAssertionFailure();
                });
            }
        }
    }
}
