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
using System.Reflection;
using Gallio.Hosting;

namespace Gallio.Reflection.Impl
{
    internal abstract class NativeFunctionWrapper<TTarget> : NativeMemberWrapper<TTarget>, IFunctionInfo
        where TTarget : MethodBase
    {
        public NativeFunctionWrapper(TTarget target)
            : base(target)
        {
        }

        public MethodAttributes MethodAttributes
        {
            get { return Target.Attributes; }
        }

        public bool IsAbstract
        {
            get { return Target.IsAbstract; }
        }

        public bool IsPublic
        {
            get { return Target.IsPublic; }
        }

        public bool IsStatic
        {
            get { return Target.IsStatic; }
        }

        public IList<IParameterInfo> GetParameters()
        {
            ParameterInfo[] parameters = Target.GetParameters();
            return Array.ConvertAll<ParameterInfo, IParameterInfo>(parameters, Reflector.Wrap);
        }

        public IList<IGenericParameterInfo> GetGenericParameters()
        {
            Type[] parameters = Target.GetGenericArguments();
            return Array.ConvertAll<Type, IGenericParameterInfo>(parameters, Reflector.WrapAsGenericParameter);
        }

        new public MethodBase Resolve()
        {
            return Target;
        }

        public override string GetXmlDocumentation()
        {
            return Loader.XmlDocumentationResolver.GetXmlDocumentation(Target);
        }

        public bool Equals(IFunctionInfo other)
        {
            return Equals((object)other);
        }
    }
}