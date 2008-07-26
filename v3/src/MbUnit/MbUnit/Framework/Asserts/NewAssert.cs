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

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Gallio;
using Gallio.Framework.Utilities;

namespace MbUnit.Framework
{
    /// <summary>
    /// Defines a set of assertions.
    /// </summary>
    [TestFrameworkInternal]
    public abstract class NewAssert
    {
        #region Private stuff
        /// <summary>
        /// Always throws a <see cref="InvalidOperationException" />.
        /// Use <see cref="NewAssert.AreEqual{T}(T, T)" /> instead.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static new void Equals(object a, object b)
        {
            throw new InvalidOperationException("Assert.Equals should not be used for assertions.  Use Assert.AreEqual instead.");
        }

        /// <summary>
        /// Always throws a <see cref="InvalidOperationException" />.
        /// Use <see cref="NewAssert.AreSame{T}(T, T)" /> instead.
        /// </summary>
        public static new void ReferenceEquals(object a, object b)
        {
            throw new InvalidOperationException("Assert.ReferenceEquals should not be used for assertions.  Use Assert.AreSame instead.");
        }
        #endregion

        #region Syntax Extensions
        /// <summary>
        /// Provides methods for composing assertions to map over complex data structures.
        /// </summary>
        public static AssertOverSyntax Over
        {
            get { return AssertOverSyntax.Instance; }
        }
        #endregion

        #region AreEqual
        /// <summary>
        /// Verifies that an actual value equals some expected value.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreEqual<T>(T expectedValue, T actualValue)
        {
            AreEqual<T>(expectedValue, actualValue, (string) null, null);
        }

        /// <summary>
        /// Verifies that an actual value equals some expected value.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreEqual<T>(T expectedValue, T actualValue, string messageFormat, params object[] messageArgs)
        {
            AreEqual<T>(expectedValue, actualValue, (Func<T, T, bool>) null, messageFormat, messageArgs);
        }

        /// <summary>
        /// Verifies that an actual value equals some expected value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreEqual<T>(T expectedValue, T actualValue, IEqualityComparer<T> comparer)
        {
            AreEqual<T>(expectedValue, actualValue, comparer, null, null);
        }

        /// <summary>
        /// Verifies that an actual value equals some expected value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreEqual<T>(T expectedValue, T actualValue, IEqualityComparer<T> comparer, string messageFormat, params object[] messageArgs)
        {
            AreEqual<T>(expectedValue, actualValue, comparer != null ? comparer.Equals : (Func<T, T, bool>) null, messageFormat, messageArgs);
        }

        /// <summary>
        /// Verifies that an actual value equals some expected value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreEqual<T>(T expectedValue, T actualValue, Func<T, T, bool> comparer)
        {
            AreEqual<T>(expectedValue, actualValue, comparer, null, null);
        }

        /// <summary>
        /// Verifies that an actual value equals some expected value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreEqual<T>(T expectedValue, T actualValue, Func<T, T, bool> comparer, string messageFormat, params object[] messageArgs)
        {
            AssertHelper.Verify(delegate
            {
                if (comparer == null)
                    comparer = DefaultEqualityComparer;

                if (comparer(expectedValue, actualValue))
                    return null;

                return new AssertionFailureBuilder("Expected values to be equal.")
                    .SetMessage(messageFormat, messageArgs)
                    .SetExpectedValue(expectedValue)
                    .SetActualValue(actualValue)
                    .ToAssertionFailure();
            });
        }
        #endregion

        #region AreNotEqual
        /// <summary>
        /// Verifies that an actual value does not equal some expected value.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreNotEqual<T>(T expectedValue, T actualValue)
        {
            AreNotEqual<T>(expectedValue, actualValue, (string)null, null);
        }

        /// <summary>
        /// Verifies that an actual value does not equal some expected value.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreNotEqual<T>(T expectedValue, T actualValue, string messageFormat, params object[] messageArgs)
        {
            AreNotEqual<T>(expectedValue, actualValue, (Func<T, T, bool>)null, messageFormat, messageArgs);
        }

        /// <summary>
        /// Verifies that an actual value does not equal some expected value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreNotEqual<T>(T expectedValue, T actualValue, IEqualityComparer<T> comparer)
        {
            AreNotEqual<T>(expectedValue, actualValue, comparer, null, null);
        }

        /// <summary>
        /// Verifies that an actual value does not equal some expected value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreNotEqual<T>(T expectedValue, T actualValue, IEqualityComparer<T> comparer, string messageFormat, params object[] messageArgs)
        {
            AreNotEqual<T>(expectedValue, actualValue, comparer != null ? comparer.Equals : (Func<T, T, bool>)null, messageFormat, messageArgs);
        }

        /// <summary>
        /// Verifies that an actual value does not equal some expected value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreNotEqual<T>(T expectedValue, T actualValue, Func<T, T, bool> comparer)
        {
            AreNotEqual<T>(expectedValue, actualValue, comparer, null, null);
        }

        /// <summary>
        /// Verifies that an actual value does not equal some expected value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreNotEqual<T>(T expectedValue, T actualValue, Func<T, T, bool> comparer, string messageFormat, params object[] messageArgs)
        {
            AssertHelper.Verify(delegate
            {
                if (comparer == null)
                    comparer = DefaultEqualityComparer;

                if (! comparer(expectedValue, actualValue))
                    return null;

                return new AssertionFailureBuilder("Expected values to be non-equal.")
                    .SetMessage(messageFormat, messageArgs)
                    .SetExpectedValue(expectedValue)
                    .SetActualValue(actualValue)
                    .ToAssertionFailure();
            });
        }
        #endregion

        #region AreSame
        /// <summary>
        /// Verifies that an actual value is referentially identical to some expected value.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreSame<T>(T expectedValue, T actualValue)
            where T : class
        {
            AreSame<T>(expectedValue, actualValue, (string)null, null);
        }

        /// <summary>
        /// Verifies that an actual value is referentially identical to some expected value.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreSame<T>(T expectedValue, T actualValue, string messageFormat, params object[] messageArgs)
            where T : class
        {
            AssertHelper.Verify(delegate
            {
                if (Object.ReferenceEquals(expectedValue, actualValue))
                    return null;

                return new AssertionFailureBuilder("Expected values to be referentially identical.")
                    .SetMessage(messageFormat, messageArgs)
                    .SetExpectedValue(expectedValue)
                    .SetActualValue(actualValue)
                    .ToAssertionFailure();
            });
        }
        #endregion

        #region AreNotSame
        /// <summary>
        /// Verifies that an actual value is not referentially identical to some expected value.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreNotSame<T>(T expectedValue, T actualValue)
            where T : class
        {
            AreNotSame<T>(expectedValue, actualValue, (string)null, null);
        }

        /// <summary>
        /// Verifies that an actual value is not referentially identical to some expected value.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="expectedValue">The expected value</param>
        /// <param name="actualValue">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void AreNotSame<T>(T expectedValue, T actualValue, string messageFormat, params object[] messageArgs)
            where T : class
        {
            AssertHelper.Verify(delegate
            {
                if (! Object.ReferenceEquals(expectedValue, actualValue))
                    return null;

                return new AssertionFailureBuilder("Expected values to be referentially different.")
                    .SetMessage(messageFormat, messageArgs)
                    .SetExpectedValue(expectedValue)
                    .SetActualValue(actualValue)
                    .ToAssertionFailure();
            });
        }
        #endregion

        #region Contains
        /// <summary>
        /// Verifies that a string contains some expected value.
        /// </summary>
        /// <param name="actualValue">The actual value</param>
        /// <param name="expectedSubstring">The expected substring</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void Contains(string actualValue, string expectedSubstring)
        {
            Contains(actualValue, expectedSubstring, null);
        }

        /// <summary>
        /// Verifies that a string contains some expected value.
        /// </summary>
        /// <param name="actualValue">The actual value</param>
        /// <param name="expectedSubstring">The expected substring</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void Contains(string actualValue, string expectedSubstring, string messageFormat, params object[] messageArgs)
        {
            AssertHelper.Verify(delegate
            {
                if (actualValue.Contains(expectedSubstring))
                    return null;

                return new AssertionFailureBuilder("Expected string to contain a particular substring.")
                    .SetMessage(messageFormat, messageArgs)
                    .SetLabeledValue("Expected Substring", expectedSubstring)
                    .SetActualValue(actualValue)
                    .ToAssertionFailure();
            });
        }
        #endregion

        #region DoesNotContain
        /// <summary>
        /// Verifies that a string does not contain some expected value.
        /// </summary>
        /// <param name="actualValue">The actual value</param>
        /// <param name="expectedSubstring">The expected substring</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void DoesNotContain(string actualValue, string expectedSubstring)
        {
            DoesNotContain(actualValue, expectedSubstring, null);
        }

        /// <summary>
        /// Verifies that a string does not contain some expected value.
        /// </summary>
        /// <param name="actualValue">The actual value</param>
        /// <param name="expectedSubstring">The expected substring</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void DoesNotContain(string actualValue, string expectedSubstring, string messageFormat, params object[] messageArgs)
        {
            AssertHelper.Verify(delegate
            {
                if (! actualValue.Contains(expectedSubstring))
                    return null;

                return new AssertionFailureBuilder("Expected string to not contain a particular substring.")
                    .SetMessage(messageFormat, messageArgs)
                    .SetLabeledValue("Expected Substring", expectedSubstring)
                    .SetActualValue(actualValue)
                    .ToAssertionFailure();
            });
        }
        #endregion

        #region IsTrue
        /// <summary>
        /// Verifies that an actual value is true.
        /// </summary>
        /// <param name="actualValue">The actual value</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void IsTrue(bool actualValue)
        {
            IsTrue(actualValue, null, null);
        }

        /// <summary>
        /// Verifies that an actual value is true.
        /// </summary>
        /// <param name="actualValue">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void IsTrue(bool actualValue, string messageFormat, params object[] messageArgs)
        {
            AssertHelper.Verify(delegate
            {
                if (actualValue)
                    return null;

                return new AssertionFailureBuilder("Expected value to be true.")
                    .SetMessage(messageFormat, messageArgs)
                    .SetActualValue(actualValue)
                    .ToAssertionFailure();
            });
        }
        #endregion

        #region IsFalse
        /// <summary>
        /// Verifies that an actual value is false.
        /// </summary>
        /// <param name="actualValue">The actual value</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void IsFalse(bool actualValue)
        {
            IsFalse(actualValue, null, null);
        }

        /// <summary>
        /// Verifies that an actual value is false.
        /// </summary>
        /// <param name="actualValue">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void IsFalse(bool actualValue, string messageFormat, params object[] messageArgs)
        {
            AssertHelper.Verify(delegate
            {
                if (! actualValue)
                    return null;

                return new AssertionFailureBuilder("Expected value to be false.")
                    .SetMessage(messageFormat, messageArgs)
                    .SetActualValue(actualValue)
                    .ToAssertionFailure();
            });
        }
        #endregion

        #region IsNull
        /// <summary>
        /// Verifies that an actual value is null.
        /// </summary>
        /// <param name="actualValue">The actual value</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void IsNull(object actualValue)
        {
            IsNull(actualValue, null, null);
        }

        /// <summary>
        /// Verifies that an actual value is null.
        /// </summary>
        /// <param name="actualValue">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void IsNull(object actualValue, string messageFormat, params object[] messageArgs)
        {
            AssertHelper.Verify(delegate
            {
                if (actualValue == null)
                    return null;

                return new AssertionFailureBuilder("Expected value to be null.")
                    .SetMessage(messageFormat, messageArgs)
                    .SetActualValue(actualValue)
                    .ToAssertionFailure();
            });
        }
        #endregion

        #region IsNotNull
        /// <summary>
        /// Verifies that an actual value is not null.
        /// </summary>
        /// <param name="actualValue">The actual value</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void IsNotNull(object actualValue)
        {
            IsNotNull(actualValue, null, null);
        }

        /// <summary>
        /// Verifies that an actual value is not null.
        /// </summary>
        /// <param name="actualValue">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void IsNotNull(object actualValue, string messageFormat, params object[] messageArgs)
        {
            AssertHelper.Verify(delegate
            {
                if (actualValue != null)
                    return null;

                return new AssertionFailureBuilder("Expected value to be non-null.")
                    .SetMessage(messageFormat, messageArgs)
                    .SetActualValue(actualValue)
                    .ToAssertionFailure();
            });
        }
        #endregion

        #region GreaterThan

        /// <summary>
        /// Verifies that an left value is greater than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        public static void GreaterThan<T>(T left, T right)
            where T : IComparable<T>
        {
            GreaterThan(left, right, (string)null, null);
        }

        /// <summary>
        /// Verifies that an left value is greater than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void GreaterThan<T>(T left, T right, string messageFormat)
            where T : IComparable<T>
        {
            GreaterThan(left, right, messageFormat, null);
        }

        /// <summary>
        /// Verifies that an left value is greater than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void GreaterThan<T>(T left, T right, string messageFormat, params object[] messageArgs)
            where T : IComparable<T>
        {
            GreaterThan(left, right, null, messageFormat, messageArgs);
        }

        /// <summary>
        /// Verifies that an left value is greater than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void GreaterThan<T>(T left, T right, Func<T, T, int> comparer)
            where T : IComparable<T>
        {
            GreaterThan(left, right, comparer, null, null);
        }

        /// <summary>
        /// Verifies that an left value is greater than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void GreaterThan<T>(T left, T right, Func<T, T, int> comparer, string messageFormat)
            where T : IComparable<T>
        {
            GreaterThan(left, right, comparer, messageFormat, null);
        }

        /// <summary>
        /// Verifies that an left value is greater than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void GreaterThan<T>(T left, T right, Func<T, T, int> comparer, string messageFormat, params object[] messageArgs)
            where T : IComparable<T>
        {
            AssertHelper.Verify(
                () => VerifyComparable(left, right, comparer
                    , "Expected left to be greater than right."
                    , compareResult => compareResult > 0
                    , messageFormat, messageArgs));
        }
        #endregion

        #region GreaterEqualThan
        /// <summary>
        /// Verifies that an left value is greater or equal than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        public static void GreaterEqualThan<T>(T left, T right)
            where T : IComparable<T>
        {
            GreaterEqualThan(left, right, (string)null, null);
        }

        /// <summary>
        /// Verifies that an left value is greater or equal than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void GreaterEqualThan<T>(T left, T right, string messageFormat)
            where T : IComparable<T>
        {
            GreaterEqualThan(left, right, messageFormat, null);
        }

        /// <summary>
        /// Verifies that an left value is greater or equal than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void GreaterEqualThan<T>(T left, T right, string messageFormat, params object[] messageArgs)
            where T : IComparable<T>
        {
            GreaterEqualThan(left, right, null, messageFormat, messageArgs);
        }

        /// <summary>
        /// Verifies that an left value is greater or equal than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void GreaterEqualThan<T>(T left, T right, Func<T, T, int> comparer)
            where T : IComparable<T>
        {
            GreaterEqualThan(left, right, comparer, null, null);
        }

        /// <summary>
        /// Verifies that an left value is greater or equal than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void GreaterEqualThan<T>(T left, T right, Func<T, T, int> comparer, string messageFormat)
            where T : IComparable<T>
        {
            GreaterEqualThan(left, right, comparer, messageFormat, null);
        }

        /// <summary>
        /// Verifies that an left value is greater or equal than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void GreaterEqualThan<T>(T left, T right, Func<T, T, int> comparer, string messageFormat, params object[] messageArgs)
            where T : IComparable<T>
        {
            AssertHelper.Verify(
                () => VerifyComparable(left, right, comparer
                    , "Expected left to be greater or equal than right."
                    , compareResult => compareResult >= 0
                    , messageFormat, messageArgs));
        }

        #endregion

        #region LowerThan

        /// <summary>
        /// Verifies that an left value is less than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        public static void LowerThan<T>(T left, T right)
            where T : IComparable<T>
        {
            LowerThan(left, right, (string)null, null);
        }

        /// <summary>
        /// Verifies that an left value is less than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void LowerThan<T>(T left, T right, string messageFormat)
            where T : IComparable<T>
        {
            LowerThan(left, right, messageFormat, null);
        }

        /// <summary>
        /// Verifies that an left value is less than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void LowerThan<T>(T left, T right, string messageFormat, params object[] messageArgs)
            where T : IComparable<T>
        {
            LowerThan(left, right, null, messageFormat, messageArgs);
        }

        /// <summary>
        /// Verifies that an left value is less than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void LowerThan<T>(T left, T right, Func<T, T, int> comparer)
            where T : IComparable<T>
        {
            LowerThan(left, right, comparer, null, null);
        }

        /// <summary>
        /// Verifies that an left value is less than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void LowerThan<T>(T left, T right, Func<T, T, int> comparer, string messageFormat)
            where T : IComparable<T>
        {
            LowerThan(left, right, comparer, messageFormat, null);
        }

        /// <summary>
        /// Verifies that an left value is less than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void LowerThan<T>(T left, T right, Func<T, T, int> comparer, string messageFormat, params object[] messageArgs)
            where T : IComparable<T>
        {
            AssertHelper.Verify(
                () => VerifyComparable(left, right, comparer
                    , "Expected left to be less than right."
                    , compareResult => compareResult < 0
                    , messageFormat, messageArgs));
        }
        #endregion

        #region LowerEqualThan

        /// <summary>
        /// Verifies that an left value is less or equal than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        public static void LowerEqualThan<T>(T left, T right)
            where T : IComparable<T>
        {
            LowerEqualThan(left, right, (string)null, null);
        }

        /// <summary>
        /// Verifies that an left value is less or equal than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void LowerEqualThan<T>(T left, T right, string messageFormat)
            where T : IComparable<T>
        {
            LowerEqualThan(left, right, messageFormat, null);
        }

        /// <summary>
        /// Verifies that an left value is less or equal than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void LowerEqualThan<T>(T left, T right, string messageFormat, params object[] messageArgs)
            where T : IComparable<T>
        {
            LowerEqualThan(left, right, null, messageFormat, messageArgs);
        }

        /// <summary>
        /// Verifies that an left value is less or equal than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void LowerEqualThan<T>(T left, T right, Func<T, T, int> comparer)
            where T : IComparable<T>
        {
            LowerEqualThan(left, right, comparer, null, null);
        }

        /// <summary>
        /// Verifies that an left value is less or equal than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void LowerEqualThan<T>(T left, T right, Func<T, T, int> comparer, string messageFormat)
            where T : IComparable<T>
        {
            LowerEqualThan(left, right, comparer, messageFormat, null);
        }

        /// <summary>
        /// Verifies that an left value is less or equal than right value according to a particular comparer.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="left">The expected value</param>
        /// <param name="right">The actual value</param>
        /// <param name="comparer">The comparer to use, or null to use the default one</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void LowerEqualThan<T>(T left, T right, Func<T, T, int> comparer, string messageFormat, params object[] messageArgs)
            where T : IComparable<T>
        {
            AssertHelper.Verify(
                () => VerifyComparable(left, right, comparer
                    , "Expected left to be less or equal than right."
                    , compareResult => compareResult <= 0
                    , messageFormat, messageArgs));
        }
        #endregion

        #region Throws
        /// <summary>
        /// Evaluates an action delegate and verifies that it throws an exception of a particular type.
        /// </summary>
        /// <typeparam name="TExpectedException">The expected type of exception</typeparam>
        /// <param name="action">The action delegate to evaluate</param>
        /// <returns>The exception that was thrown</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null</exception>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static TExpectedException Throws<TExpectedException>(Action action)
            where TExpectedException : Exception
        {
            return Throws<TExpectedException>(action, null, null);
        }

        /// <summary>
        /// Evaluates an action delegate and verifies that it throws an exception of a particular type.
        /// </summary>
        /// <typeparam name="TExpectedException">The expected type of exception</typeparam>
        /// <param name="action">The action delegate to evaluate</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <returns>The exception that was thrown</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null</exception>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static TExpectedException Throws<TExpectedException>(Action action, string messageFormat, params object[] messageArgs)
            where TExpectedException : Exception
        {
            return (TExpectedException)Throws(typeof(TExpectedException), action, messageFormat, messageArgs);
        }

        /// <summary>
        /// Evaluates an action delegate and verifies that it throws an exception of a particular type.
        /// </summary>
        /// <param name="expectedExceptionType">The expected exception type</param>
        /// <param name="action">The action delegate to evaluate</param>
        /// <returns>The exception that was thrown</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null</exception>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static Exception Throws(Type expectedExceptionType, Action action)
        {
            return Throws(expectedExceptionType, action);
        }

        /// <summary>
        /// Evaluates an action delegate and verifies that it throws an exception of a particular type.
        /// </summary>
        /// <param name="expectedExceptionType">The expected exception type</param>
        /// <param name="action">The action delegate to evaluate</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <returns>The exception that was thrown</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null</exception>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static Exception Throws(Type expectedExceptionType, Action action, string messageFormat, params object[] messageArgs)
        {
            Exception result = null;
            AssertHelper.Verify(delegate
            {
                try
                {
                    action();
                    return new AssertionFailureBuilder("Expected the block to throw an exception.")
                        .SetMessage(messageFormat, messageArgs)
                        .SetLabeledValue("Expected Exception Type", expectedExceptionType)
                        .ToAssertionFailure();
                }
                catch (Exception actualException)
                {
                    if (expectedExceptionType.IsInstanceOfType(actualException))
                    {
                        result = actualException;
                        return null;
                    }

                    return new AssertionFailureBuilder("The block threw an exception of a different type than was expected.")
                        .SetMessage(messageFormat, messageArgs)
                        .SetLabeledValue("Expected Exception Type", expectedExceptionType)
                        .AddException(actualException)
                        .ToAssertionFailure();
                }
            });

            return result;
        }
        #endregion

        #region DoesNotThrow
        /// <summary>
        /// Evaluates an action delegate and verifies that it does not throw an exception of any type.
        /// </summary>
        /// <param name="action">The action delegate to evaluate</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null</exception>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void DoesNotThrow(Action action)
        {
            DoesNotThrow(action, null, null);
        }

        /// <summary>
        /// Evaluates an action delegate and verifies that it does not throw an exception of any type.
        /// </summary>
        /// <param name="action">The action delegate to evaluate</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null</exception>
        /// <exception cref="AssertionException">Thrown if the verification failed unless the current <see cref="AssertionContext.AssertionFailureBehavior" /> indicates otherwise</exception>
        public static void DoesNotThrow(Action action, string messageFormat, params object[] messageArgs)
        {
            AssertHelper.Verify(delegate
            {
                try
                {
                    action();
                    return null;
                }
                catch (Exception actualException)
                {
                    return new AssertionFailureBuilder("The block threw an exception but none was expected.")
                        .SetMessage(messageFormat, messageArgs)
                        .AddException(actualException)
                        .ToAssertionFailure();
                }
            });
        }
        #endregion

        #region Multiple
        /// <summary>
        /// <para>
        /// Executes an action delegate that contains multiple related assertions.
        /// </para>
        /// <para>
        /// While the delegate runs, the behavior of assertions is change such that
        /// failures are captured but do not cause a <see cref="AssertionFailureException" />
        /// to be throw.  When the delegate returns, the previous assertion failure behavior
        /// is restored and any captured assertion failures are reported.  The net effect
        /// of this change is that the test can continue to run even after an assertion failure
        /// occurs which can help to provide more information about the problem.
        /// </para>
        /// </summary>
        /// <remarks>
        /// A multiple assertion block is useful for verifying the state of a single
        /// component with many parts that require several assertions to check.
        /// This feature can accelerate debugging because more diagnostic information
        /// become available at once.
        /// </remarks>
        /// <param name="action">The action to invoke</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null</exception>
        public static void Multiple(Action action)
        {
            Multiple(action, null, null);
        }

        /// <summary>
        /// <para>
        /// Executes an action delegate that contains multiple related assertions.
        /// </para>
        /// <para>
        /// While the delegate runs, the behavior of assertions is change such that
        /// failures are captured but do not cause a <see cref="AssertionFailureException" />
        /// to be throw.  When the delegate returns, the previous assertion failure behavior
        /// is restored and any captured assertion failures are reported.  The net effect
        /// of this change is that the test can continue to run even after an assertion failure
        /// occurs which can help to provide more information about the problem.
        /// </para>
        /// <para>
        /// If the block throws an exception other than an assertion failure, then it is
        /// similarly recorded.
        /// </para>
        /// </summary>
        /// <remarks>
        /// A multiple assertion block is useful for verifying the state of a single
        /// component with many parts that require several assertions to check.
        /// This feature can accelerate debugging because more diagnostic information
        /// become available at once.
        /// </remarks>
        /// <param name="action">The action to invoke</param>
        /// <param name="messageFormat">The custom assertion message format, or null if none</param>
        /// <param name="messageArgs">The custom assertion message arguments, or null if none</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null</exception>
        public static void Multiple(Action action, string messageFormat, params object[] messageArgs)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            AssertHelper.Verify(delegate
            {
                AssertionFailure[] failures = AssertionContext.CurrentContext.CaptureFailures(action,
                    AssertionFailureBehavior.Log, true);
                if (failures.Length == 0)
                    return null;

                return new AssertionFailureBuilder(String.Format("There were {0} failures within the multiple assertion block.", failures.Length))
                    .SetMessage(messageFormat, messageArgs)
                    .ToAssertionFailure();
            });
        }
        #endregion

        // TODO: Make this extensible.
        private static bool DefaultEqualityComparer<T>(T expectedValue, T actualValue)
        {
            if (Object.ReferenceEquals(expectedValue, actualValue))
                return true;

            if (expectedValue == null || actualValue == null)
                return false;

            var expectedEnumerable = expectedValue as IEnumerable;
            var actualEnumerable = actualValue as IEnumerable;
            if (expectedEnumerable != null && actualEnumerable != null)
                return CompareEnumerables(expectedEnumerable, actualEnumerable);

            return expectedValue.Equals(actualValue);
        }

        private static bool CompareEnumerables(IEnumerable expectedEnumerable, IEnumerable actualEnumerable)
        {
            IEnumerator expectedEnumerator = expectedEnumerable.GetEnumerator();
            IEnumerator actualEnumerator = actualEnumerable.GetEnumerator();
            while (expectedEnumerator.MoveNext())
            {
                if (!actualEnumerator.MoveNext())
                    return false;

                if (! DefaultEqualityComparer(expectedEnumerator.Current, actualEnumerator.Current))
                    return false;
            }

            if (actualEnumerator.MoveNext())
                return false;

            return true;
        }

        private static int ComparableCompare<T>(T left, T right)
            where T: IComparable<T>
        {
            return left.CompareTo(right);
        }

        private static AssertionFailure VerifyComparable<T>(T left, T right, Func<T, T, int> comparer, string exceptionMessage, Func<int, bool> isCompareSuccesfull, string messageFormat, params object[] messageArgs)
            where T : IComparable<T>
        {
            if (comparer == null)
            {
                IsNotNull(left, AssertHelper.AppendCustomMessage("left value cannot be null.", messageFormat, messageArgs));
                comparer = ComparableCompare;
            }

            if (isCompareSuccesfull(comparer(left, right)))
                return null;

            return new AssertionFailureBuilder(exceptionMessage)
                .SetMessage(messageFormat, messageArgs)
                .SetExpectedValue(left)
                .SetActualValue(right)
                .ToAssertionFailure();

        }
    }
}
