// Copyright 2005-2008 Gallio Project - http://www.gallio.org/
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

using System.Collections.Generic;
using Gallio;
using Gallio.Framework.Assertions;

namespace MbUnit.Framework
{
    public abstract partial class Assert
    {
        /// <summary>
        /// Asserts that two strings are equal, ignoring the case
        /// </summary>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreEqualIgnoreCase(string expectedValue, string actualValue)
        {
            AreEqualIgnoreCase(expectedValue, actualValue, (Func<string, string, bool>)null, null, null);
        }

        /// <summary>
        /// Verifies that an actual value equals some expected value.
        /// </summary>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreEqualIgnoreCase(string expectedValue, string actualValue, string messageFormat, params object[] messageArgs)
        {
            AreEqualIgnoreCase(expectedValue, actualValue, (Func<string, string, bool>)null, messageFormat, messageArgs);
        }

        /// <summary>
        /// Verifies that an actual value equals some expected value according to a particular comparer.
        /// </summary>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreEqualIgnoreCase(string expectedValue, string actualValue, IEqualityComparer<string> comparer)
        {
            AreEqual(expectedValue, actualValue, comparer, null, null);
        }

        /// <summary>
        /// Asserts that two strings are equal, ignoring the case
        /// </summary>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreEqualIgnoreCase(string expectedValue, string actualValue, IEqualityComparer<string> comparer, string messageFormat, params object[] messageArgs)
        {
            AreEqualIgnoreCase(expectedValue, actualValue, comparer != null ? comparer.Equals : (Func<string, string, bool>)null, messageFormat, messageArgs);
        }

        /// <summary>
        /// Asserts that two strings are equal, ignoring the case
        /// </summary>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreEqualIgnoreCase(string expectedValue, string actualValue, Func<string, string, bool> comparer)
        {
            AreEqualIgnoreCase(expectedValue, actualValue, comparer, null, null);
        }

        /// <summary>
        /// Asserts that two strings are equal, ignoring the case
        /// </summary>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreEqualIgnoreCase(string expectedValue, string actualValue, Func<string, string, bool> comparer, string messageFormat, params object[] messageArgs)
        {
            if (expectedValue == null || actualValue == null)
                AreEqual(expectedValue, actualValue, comparer, messageFormat, messageArgs);
            else
                AssertionHelper.Verify(delegate
                {
                    if (comparer == null)
                        comparer = DefaultEqualityComparer;

                    if (comparer(expectedValue.ToLower(), actualValue.ToLower()))
                        return null;

                    return new AssertionFailureBuilder("Expected values to be equal.")
                        .SetMessage(messageFormat, messageArgs)
                        .AddRawExpectedAndActualValuesWithDiffs(expectedValue, actualValue)
                        .ToAssertionFailure();
                });
        }
    }
}