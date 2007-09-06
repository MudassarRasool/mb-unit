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
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("MbUnit.Framework")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("MbUnit.Framework")]
[assembly: AssemblyCopyright("Copyright © 2007 MbUnit Project - http://www.mbunit.com/")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("e59e2250-578a-44a0-a947-c46a2c5b003f")]

// Ensure CLS compliance for as much of the framework as possible.
// We will individually mark certain constructs non-compliant as needed
// but setting this attribute on the assembly lets the compiler help us.
[assembly: CLSCompliant(true)]

// Allow partially trusted callers to use the MbUnit framework.
// This isn't enough to ensure that we properly support partially trusted
// contexts but it's a beginning.  We also need to carefully review security
// demands throughout the framework and especially calls back into core services.
[assembly: AllowPartiallyTrustedCallers]

// The neutral resources language is US English.
// Telling the system that this is the case yields a small performance improvement during startup.
[assembly: NeutralResourcesLanguage("en-US")]
