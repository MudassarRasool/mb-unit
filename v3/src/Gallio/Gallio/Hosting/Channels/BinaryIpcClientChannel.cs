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
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace Gallio.Hosting.Channels
{
    /// <summary>
    /// A client channel based on an <see cref="IpcClientChannel" /> that uses a
    /// <see cref="BinaryClientFormatterSinkProvider" />.
    /// </summary>
    public class BinaryIpcClientChannel : BaseClientChannel
    {
        private const int ConnectionTimeoutMillis = 30000;

        /// <summary>
        /// Creates a channel.
        /// </summary>
        /// <param name="portName">The ipc port name to connect to</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="portName"/> is null</exception>
        public BinaryIpcClientChannel(string portName)
            : base(CreateChannel(portName), new Uri("ipc://" + portName))
        {
        }

        private static IChannel CreateChannel(string portName)
        {
            if (portName == null)
                throw new ArgumentNullException("portName");

            IDictionary formatterProperties = new Hashtable();
            formatterProperties[@"includeVersions"] = false;

            BinaryClientFormatterSinkProvider sinkProvider = new BinaryClientFormatterSinkProvider(formatterProperties, null);

            IDictionary channelProperties = new Hashtable();
            channelProperties[@"connectionTimeout"] = ConnectionTimeoutMillis;
            channelProperties[@"name"] = @"ipc-client:" + portName;
            channelProperties[@"secure"] = true;

            return new IpcClientChannel(channelProperties, sinkProvider);
        }
    }
}