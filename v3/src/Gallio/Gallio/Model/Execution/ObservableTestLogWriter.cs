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
using Gallio.Model.Logging;

namespace Gallio.Model.Execution
{
    /// <summary>
    /// A log writer that sends messages to a <see cref="ITestListener" />.
    /// </summary>
    public class ObservableTestLogWriter : UnifiedTestLogWriter
    {
        private ITestListener listener;
        private readonly string stepId;

        /// <summary>
        /// Creates a log writer.
        /// </summary>
        /// <param name="listener">The test listener to which notifications are dispatched</param>
        /// <param name="stepId">The step id</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="listener"/> or
        /// <paramref name="stepId"/> is null</exception>
        public ObservableTestLogWriter(ITestListener listener, string stepId)
        {
            if (listener == null)
                throw new ArgumentNullException(@"listener");
            if (stepId == null)
                throw new ArgumentNullException(@"stepId");

            this.listener = listener;
            this.stepId = stepId;
        }

        /// <inheritdoc />
        protected override void CloseImpl()
        {
            listener = null;
        } 

        /// <inheritdoc />
        protected override void AttachTextImpl(string attachmentName, string contentType, string text)
        {
            listener.NotifyTestStepLogAttachText(stepId, attachmentName, contentType, text);
        }

        /// <inheritdoc />
        protected override void AttachBytesImpl(string attachmentName, string contentType, byte[] bytes)
        {
            listener.NotifyTestStepLogAttachBytes(stepId, attachmentName, contentType, bytes);
        }

        /// <inheritdoc />
        protected override void WriteImpl(string streamName, string text)
        {
            listener.NotifyTestStepLogStreamWrite(stepId, streamName, text);
        }

        /// <inheritdoc />
        protected override void EmbedImpl(string streamName, string attachmentName)
        {
            listener.NotifyTestStepLogStreamEmbed(stepId, streamName, attachmentName);
        }

        /// <inheritdoc />
        protected override void BeginSectionImpl(string streamName, string sectionName)
        {
            listener.NotifyTestStepLogStreamBeginSection(stepId, streamName, sectionName);
        }

        /// <inheritdoc />
        protected override void BeginMarkerImpl(string streamName, string @class)
        {
            listener.NotifyTestStepLogStreamBeginMarker(stepId, streamName, @class);
        }

        /// <inheritdoc />
        protected override void EndImpl(string streamName)
        {
            listener.NotifyTestStepLogStreamEnd(stepId, streamName);
        }
    }
}
