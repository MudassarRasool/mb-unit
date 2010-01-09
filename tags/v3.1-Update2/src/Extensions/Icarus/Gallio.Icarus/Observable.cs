// Copyright 2005-2009 Gallio Project - http://www.gallio.org/
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

using System.ComponentModel;
using Gallio.UI.Common.Synchronization;

namespace Gallio.Icarus
{
    // Thanks Oren!
    // http://ayende.com/Blog/archive/2009/08/08/an-easier-way-to-manage-inotifypropertychanged.aspx
    public class Observable<T> : INotifyPropertyChanged
    {
        private T value;

        public Observable()
        { }

        public Observable(T value)
        {
            this.value = value;
        }

        public T Value
        {
            get { return value; }
            set
            {
                this.value = value;
             
                if (PropertyChanged == null)
                    return;

                SynchronizationContext.Send(delegate
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                }, this);
            }
        }

        public static implicit operator T(Observable<T> val)
        {
            return val.value;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}