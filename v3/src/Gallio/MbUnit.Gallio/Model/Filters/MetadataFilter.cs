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
using MbUnit.Model;

namespace MbUnit.Model.Filters
{
    /// <summary>
    /// A filter that matches objects whose <see cref="IModelComponent.Metadata" />
    /// contains the specified key and value.
    /// </summary>
    [Serializable]
    public class MetadataFilter<T> : Filter<T> where T : IModelComponent
    {
        private string key;
        private string value;

        /// <summary>
        /// Creates a metadata filter.
        /// </summary>
        /// <param name="key">The metadata key to look for</param>
        /// <param name="value">The metadata value to look for</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="key"/> is null</exception>
        public MetadataFilter(string key, string value)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            
            this.key = key;
            this.value = value;
        }

        /// <inheritdoc />
        public override bool IsMatch(T value)
        {
            return value.Metadata[key].Contains(this.value);
        }
    }
}
