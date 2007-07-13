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
using System.Xml.Serialization;
using MbUnit.Core.Serialization;

namespace MbUnit.Core.Services.Reports.Xml
{
    /// <summary>
    /// Specifies the Xml-encoding of an embedded report attachment.
    /// </summary>
    [XmlType(Namespace=SerializationUtils.XmlNamespace)]
    public enum XmlContentEncoding
    {
        /// <summary>
        /// The attachment is encoded as a text string.
        /// </summary>
        [XmlEnum("text")]
        Text,

        /// <summary>
        /// The attachment is encoded in XML as child elements.
        /// </summary>
        [XmlEnum("xml")]
        Xml,

        /// <summary>
        /// The attachment is encoded as base 64 text string.
        /// </summary>
        [XmlEnum("base64")]
        Base64
    }
}
