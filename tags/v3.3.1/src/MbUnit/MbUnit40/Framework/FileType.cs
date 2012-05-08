﻿// Copyright 2005-2011 Gallio Project - http://www.gallio.org/
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
using System.Linq;
using System.Text;

namespace MbUnit.Framework
{
    /// <summary>
    /// Specifies the type of File user by TabularDataAttribute
    /// </summary>
    public enum TabularDataFileType
    {
        /// <summary>
        /// CSV File
        /// </summary>
        CsvFile = 1,
        /// <summary>
        /// Tab-delimited File
        /// </summary>
        TabFile = 2,
    }
}