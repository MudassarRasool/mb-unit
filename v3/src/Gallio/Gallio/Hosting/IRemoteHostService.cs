// Copyright 2008 MbUnit Project - http://www.mbunit.com/
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
using System.Runtime.Remoting;
using System.Text;
using Castle.Core.Logging;

namespace Gallio.Hosting
{
    /// <summary>
    /// <para>
    /// A remote host service enables a local client to interact with a remotely
    /// executing hosting process.
    /// </para>
    /// <para>
    /// A remote host service implementation may choose to implement a keep-alive
    /// mechanism to automatically shut itself down when the service is disposed or
    /// when it has not received a ping within a set period of time.
    /// </para>
    /// </summary>
    public interface IRemoteHostService : IDisposable
    {
        /// <summary>
        /// Pings the remote host service to verify and maintain connectivity.
        /// </summary>
        void Ping();

        /// <summary>
        /// Creates an instance of a remote object of the specified type
        /// using the type's default constructor.
        /// </summary>
        /// <param name="assemblyName">The name of assembly that contains the type</param>
        /// <param name="typeName">The full name of the type</param>
        /// <returns>The object handle that must be unwrapped to obtain the instance</returns>
        ObjectHandle Activate(string assemblyName, string typeName);

        /// <summary>
        /// Initializes the runtime.
        /// </summary>
        /// <param name="runtimeSetup">The runtime setup</param>
        /// <param name="logger">The logger</param>
        void InitializeRuntime(RuntimeSetup runtimeSetup, ILogger logger);

        /// <summary>
        /// Shuts down the runtime.
        /// </summary>
        void ShutdownRuntime();
    }
}
