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

namespace MbUnit.Framework.Kernel.Utilities
{
    /// <summary>
    /// Utility functions for manipulating lists.
    /// </summary>
    public static class ListUtils
    {
        /// <summary>
        /// Converts each element of the input collection and stores the result in the
        /// output list using the same index.  The output list must be at least as
        /// large as the input list.
        /// </summary>
        /// <typeparam name="TInput">The input type</typeparam>
        /// <typeparam name="TOutput">The output type</typeparam>
        /// <param name="input">The input list</param>
        /// <param name="output">The output list</param>
        /// <param name="converter">The conversion function to apply to each element</param>
        public static void ConvertAndCopyAll<TInput, TOutput>(ICollection<TInput> input, IList<TOutput> output,
                                                              Converter<TInput, TOutput> converter)
        {
            int i = 0;
            foreach (TInput value in input)
                output[i++] = converter(value);
        }

        /// <summary>
        /// Converts each element of the input collection and returns the collected results as an array
        /// of the same size.
        /// </summary>
        /// <typeparam name="TInput">The input type</typeparam>
        /// <typeparam name="TOutput">The output type</typeparam>
        /// <param name="input">The input collection</param>
        /// <param name="converter">The conversion function to apply to each element</param>
        /// <returns>The output array</returns>
        public static TOutput[] ConvertAllToArray<TInput, TOutput>(ICollection<TInput> input,
                                                                   Converter<TInput, TOutput> converter)
        {
            TOutput[] output = new TOutput[input.Count];
            ConvertAndCopyAll(input, output, converter);
            return output;
        }

        /// <summary>
        /// Copies all of the elements of the input collection to an array.
        /// </summary>
        /// <typeparam name="T">The input type</typeparam>
        /// <param name="input">the input collection</param>
        /// <returns>The output array</returns>
        public static T[] CopyAllToArray<T>(ICollection<T> input)
        {
            T[] output = new T[input.Count];

            int i = 0;
            foreach (T value in input)
                output[i++] = value;

            return output;
        }

        /// <summary>
        /// Returns the first element of the input enumeration for which the specified
        /// predicate returns true.
        /// </summary>
        /// <typeparam name="T">The input type</typeparam>
        /// <param name="input">The input collection</param>
        /// <param name="predicate">The predicate</param>
        /// <returns>The first matching value or the default for the type if not found</returns>
        public static T Find<T>(IEnumerable<T> input, Predicate<T> predicate)
        {
            foreach (T value in input)
                if (predicate(value))
                    return value;

            return default(T);
        }
    }
}