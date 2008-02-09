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

using System.Collections.Generic;
using System.Reflection;
using Gallio.Collections;
using Gallio.Reflection;
using JetBrains.ReSharper.Psi;

namespace Gallio.ReSharperRunner.Reflection.Impl
{
    internal abstract class PsiConstructedTypeWrapper<TTarget> : PsiTypeWrapper<TTarget>
        where TTarget : class, IType
    {
        public PsiConstructedTypeWrapper(PsiReflector reflector, TTarget target)
            : base(reflector, target)
        {
        }

        public abstract ITypeInfo EffectiveClassType { get; }

        public override ITypeInfo DeclaringType
        {
            get { return null; }
        }

        public override IAssemblyInfo Assembly
        {
            get { return ElementType.Assembly; }
        }

        public override INamespaceInfo Namespace
        {
            get { return ElementType.Namespace; }
        }

        public override ITypeInfo BaseType
        {
            get { return null; }
        }

        public override TypeAttributes TypeAttributes
        {
            get { return EffectiveClassType.TypeAttributes; }
        }

        public override IList<ITypeInfo> Interfaces
        {
            get { return EffectiveClassType.Interfaces; }
        }

        public override bool ContainsGenericParameters
        {
            get { return ElementType.ContainsGenericParameters; }
        }

        public override IList<IConstructorInfo> GetConstructors(BindingFlags bindingFlags)
        {
            return EffectiveClassType.GetConstructors(bindingFlags);
        }

        public override IMethodInfo GetMethod(string methodName, BindingFlags bindingFlags)
        {
            return EffectiveClassType.GetMethod(methodName, bindingFlags);
        }

        public override IList<IMethodInfo> GetMethods(BindingFlags bindingFlags)
        {
            return EffectiveClassType.GetMethods(bindingFlags);
        }

        public override IList<IPropertyInfo> GetProperties(BindingFlags bindingFlags)
        {
            return EffectiveClassType.GetProperties(bindingFlags);
        }

        public override IList<IFieldInfo> GetFields(BindingFlags bindingFlags)
        {
            return EffectiveClassType.GetFields(bindingFlags);
        }

        public override IList<IEventInfo> GetEvents(BindingFlags bindingFlags)
        {
            return EffectiveClassType.GetEvents(bindingFlags);
        }

        public override IEnumerable<IAttributeInfo> GetAttributeInfos(ITypeInfo attributeType, bool inherit)
        {
            return EmptyArray<IAttributeInfo>.Instance;
        }
    }
}