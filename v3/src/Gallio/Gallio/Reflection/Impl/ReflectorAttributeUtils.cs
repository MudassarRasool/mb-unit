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
using System.Reflection;
using Gallio.Reflection;

namespace Gallio.Reflection.Impl
{
    /// <summary>
    /// <para>
    /// Provides helpers for enumerating attributes taking into account
    /// the attribute inheritance structure.
    /// </para>
    /// <para>
    /// This class is intended to assist with the implementation of new
    /// reflection policies.  It should not be used directly by clients of the
    /// reflection API.
    /// </para>
    /// </summary>
    public static class ReflectorAttributeUtils
    {
        private delegate IEnumerable<T> InheritedMemberProvider<T>(T member);

        /// <summary>
        /// Provides the attributes declared by a member.
        /// Does not include inherited members.
        /// </summary>
        /// <typeparam name="T">The member type</typeparam>
        /// <param name="member">The member</param>
        /// <returns>The enumeration of attributes declared by the member</returns>
        public delegate IEnumerable<IAttributeInfo> AttributeProvider<T>(T member) where T : ICodeElementInfo;

        /// <summary>
        /// Creates an attribute instance from an <see cref="IAttributeInfo" />.
        /// </summary>
        /// <remarks>
        /// This method may be used by <see cref="IAttributeInfo.Resolve"/> to construct
        /// an attribute instance from its raw description.  Client code should
        /// call <see cref="IAttributeInfo.Resolve" /> instead of using this method
        /// directly.
        /// </remarks>
        /// <param name="attribute">The attribute description</param>
        /// <returns>The attribute instance</returns>
        public static object CreateAttribute(IAttributeInfo attribute)
        {
            ConstructorInfo constructor = attribute.Constructor.Resolve(true);
            object instance = constructor.Invoke(attribute.InitializedArgumentValues);

            foreach (KeyValuePair<IFieldInfo, object> initializer in attribute.InitializedFieldValues)
                initializer.Key.Resolve(true).SetValue(instance, initializer.Value);

            foreach (KeyValuePair<IPropertyInfo, object> initializer in attribute.InitializedPropertyValues)
                initializer.Key.Resolve(true).SetValue(instance, initializer.Value, null);

            return instance;
        }

        /// <summary>
        /// Returns true if the field is assignable as a named attribute parameter.
        /// </summary>
        /// <param name="field">The field</param>
        /// <returns>True if the field is assignable</returns>
        public static bool IsAttributeField(IFieldInfo field)
        {
            return !field.IsLiteral && !field.IsInitOnly && !field.IsStatic;
        }

        /// <summary>
        /// Returns true if the property is assignable as a named attribute parameter.
        /// </summary>
        /// <param name="property">The property</param>
        /// <returns>True if the property is assignable</returns>
        public static bool IsAttributeProperty(IPropertyInfo property)
        {
            IMethodInfo setMethod = property.SetMethod;
            return setMethod != null && setMethod.IsPublic && ! setMethod.IsAbstract && ! setMethod.IsStatic;
        }

        /// <summary>
        /// Enumerates all assembly attributes.
        /// </summary>
        /// <param name="assembly">The assembly</param>
        /// <param name="attributeType">The attribute type, or null to search for attributes of all types</param>
        /// <param name="inherit">True to include inherited attributes (ignored)</param>
        /// <param name="attributeProvider">The attribute provider</param>
        /// <returns>The enumeration of attributes</returns>
        public static IEnumerable<IAttributeInfo> EnumerateAssemblyAttributes(IAssemblyInfo assembly, ITypeInfo attributeType, bool inherit, AttributeProvider<IAssemblyInfo> attributeProvider)
        {
            return FilterAttributesOfType(attributeProvider(assembly), attributeType);
        }

        /// <summary>
        /// Enumerates all constructor attributes.
        /// </summary>
        /// <param name="constructor">The constructor</param>
        /// <param name="attributeType">The attribute type, or null to search for attributes of all types</param>
        /// <param name="inherit">True to include inherited attributes (ignored)</param>
        /// <param name="attributeProvider">The attribute provider</param>
        /// <returns>The enumeration of attributes</returns>
        public static IEnumerable<IAttributeInfo> EnumerateConstructorAttributes(IConstructorInfo constructor, ITypeInfo attributeType, bool inherit, AttributeProvider<IConstructorInfo> attributeProvider)
        {
            return FilterAttributesOfType(attributeProvider(constructor), attributeType);
        }

        /// <summary>
        /// Enumerates all field attributes.
        /// </summary>
        /// <param name="field">The field</param>
        /// <param name="attributeType">The attribute type, or null to search for attributes of all types</param>
        /// <param name="inherit">True to include inherited attributes (ignored)</param>
        /// <param name="attributeProvider">The attribute provider</param>
        /// <returns>The enumeration of attributes</returns>
        public static IEnumerable<IAttributeInfo> EnumerateFieldAttributes(IFieldInfo field, ITypeInfo attributeType, bool inherit, AttributeProvider<IFieldInfo> attributeProvider)
        {
            return FilterAttributesOfType(attributeProvider(field), attributeType);
        }

        /// <summary>
        /// Enumerates all property attributes.
        /// </summary>
        /// <param name="property">The property</param>
        /// <param name="attributeType">The attribute type, or null to search for attributes of all types</param>
        /// <param name="inherit">True to include inherited attributes</param>
        /// <param name="attributeProvider">The attribute provider</param>
        /// <returns>The enumeration of attributes</returns>
        public static IEnumerable<IAttributeInfo> EnumeratePropertyAttributes(IPropertyInfo property, ITypeInfo attributeType, bool inherit, AttributeProvider<IPropertyInfo> attributeProvider)
        {
            if (!inherit)
                return attributeProvider(property);

            return EnumerateInheritedAttributes(property, attributeType, attributeProvider, ReflectorInheritanceUtils.EnumerateSuperProperties);
        }

        /// <summary>
        /// Enumerates all event attributes.
        /// </summary>
        /// <param name="event">The event</param>
        /// <param name="attributeType">The attribute type, or null to search for attributes of all types</param>
        /// <param name="inherit">True to include inherited attributes</param>
        /// <param name="attributeProvider">The attribute provider</param>
        /// <returns>The enumeration of attributes</returns>
        public static IEnumerable<IAttributeInfo> EnumerateEventAttributes(IEventInfo @event, ITypeInfo attributeType, bool inherit, AttributeProvider<IEventInfo> attributeProvider)
        {
            if (!inherit)
                return attributeProvider(@event);

            return EnumerateInheritedAttributes(@event, attributeType, attributeProvider, ReflectorInheritanceUtils.EnumerateSuperEvents);
        }

        /// <summary>
        /// Enumerates all method attributes.
        /// </summary>
        /// <param name="method">The method</param>
        /// <param name="attributeType">The attribute type, or null to search for attributes of all types</param>
        /// <param name="inherit">True to include inherited attributes</param>
        /// <param name="attributeProvider">The attribute provider</param>
        /// <returns>The enumeration of attributes</returns>
        public static IEnumerable<IAttributeInfo> EnumerateMethodAttributes(IMethodInfo method, ITypeInfo attributeType, bool inherit, AttributeProvider<IMethodInfo> attributeProvider)
        {
            if (!inherit)
                return attributeProvider(method);

            return EnumerateInheritedAttributes(method, attributeType, attributeProvider, ReflectorInheritanceUtils.EnumerateSuperMethods);
        }

        /// <summary>
        /// Enumerates all type attributes.
        /// </summary>
        /// <param name="type">The type</param>
        /// <param name="attributeType">The attribute type, or null to search for attributes of all types</param>
        /// <param name="inherit">True to include inherited attributes</param>
        /// <param name="attributeProvider">The attribute provider</param>
        /// <returns>The enumeration of attributes</returns>
        public static IEnumerable<IAttributeInfo> EnumerateTypeAttributes(ITypeInfo type, ITypeInfo attributeType, bool inherit, AttributeProvider<ITypeInfo> attributeProvider)
        {
            if (!inherit)
                return attributeProvider(type);

            return EnumerateInheritedAttributes(type, attributeType, attributeProvider, ReflectorInheritanceUtils.EnumerateSuperTypes);
        }

        /// <summary>
        /// Enumerates all parameter attributes.
        /// </summary>
        /// <param name="parameter">The parameter</param>
        /// <param name="attributeType">The attribute type, or null to search for attributes of all types</param>
        /// <param name="inherit">True to include inherited attributes</param>
        /// <param name="attributeProvider">The attribute provider</param>
        /// <returns>The enumeration of attributes</returns>
        public static IEnumerable<IAttributeInfo> EnumerateParameterAttributes(IParameterInfo parameter, ITypeInfo attributeType, bool inherit, AttributeProvider<IParameterInfo> attributeProvider)
        {
            if (!inherit)
                return attributeProvider(parameter);

            return EnumerateInheritedAttributes(parameter, attributeType, attributeProvider, ReflectorInheritanceUtils.EnumerateSuperParameters);
        }

        /// <summary>
        /// Enumerates all generic parameter attributes.
        /// </summary>
        /// <param name="genericParameter">The generic parameter</param>
        /// <param name="attributeType">The attribute type, or null to search for attributes of all types</param>
        /// <param name="inherit">True to include inherited attributes (ignored)</param>
        /// <param name="attributeProvider">The attribute provider</param>
        /// <returns>The enumeration of attributes</returns>
        public static IEnumerable<IAttributeInfo> EnumerateGenericParameterAttributes(IGenericParameterInfo genericParameter, ITypeInfo attributeType, bool inherit, AttributeProvider<IGenericParameterInfo> attributeProvider)
        {
            return FilterAttributesOfType(attributeProvider(genericParameter), attributeType);
        }

        private static IEnumerable<IAttributeInfo> EnumerateInheritedAttributes<T>(T member, ITypeInfo attributeType, AttributeProvider<T> attributeProvider,
            InheritedMemberProvider<T> inheritedMemberProvider)
            where T : ICodeElementInfo
        {
            Dictionary<ITypeInfo, AttributeUsageAttribute> attributeUsages = new Dictionary<ITypeInfo, AttributeUsageAttribute>();
            string qualifiedTypeName = attributeType != null ? attributeType.FullName : null;

            // Yield all attributes declared by the member itself.
            // Keep track of which types were seen so we can resolve inherited but non-multiple attributes later.
            foreach (IAttributeInfo attribute in attributeProvider(member))
            {
                ITypeInfo type = attribute.Type;
                if (qualifiedTypeName == null || ReflectionUtils.IsDerivedFrom(type, qualifiedTypeName))
                {
                    yield return attribute;
                    attributeUsages[type] = null;
                }
            }

            // Now loop over the inherited member declarations to find inherited attributes.
            // If we see an attribute of a kind we have seen before, then we check whether
            // multiple instances of it are allowed and discard it if not.
            foreach (T inheritedMember in inheritedMemberProvider(member))
            {
                foreach (IAttributeInfo attribute in attributeProvider(inheritedMember))
                {
                    ITypeInfo type = attribute.Type;
                    if (qualifiedTypeName != null && !ReflectionUtils.IsDerivedFrom(type, qualifiedTypeName))
                        continue;

                    AttributeUsageAttribute attributeUsage;
                    bool seenBefore = attributeUsages.TryGetValue(type, out attributeUsage);

                    if (attributeUsage == null)
                    {
                        attributeUsage = GetAttributeUsage(type);
                        attributeUsages[type] = attributeUsage;
                    }

                    if (! attributeUsage.Inherited)
                        continue;

                    if (seenBefore && ! attributeUsage.AllowMultiple)
                        continue;

                    yield return attribute;
                }
            }
        }

        private static AttributeUsageAttribute GetAttributeUsage(ITypeInfo attributeType)
        {
            AttributeUsageAttribute attributeUsage;

            if (attributeType.FullName == "System.AttributeUsageAttribute")
            {
                // Note: Avoid indefinite recursion when determining whether AttributeUsageAttribute itself is inheritable.
                attributeUsage = new AttributeUsageAttribute(AttributeTargets.Class);
                attributeUsage.Inherited = true;
            }
            else
            {
                attributeUsage = AttributeUtils.GetAttribute<AttributeUsageAttribute>(attributeType, true);

                if (attributeUsage == null)
                {
                    attributeUsage = new AttributeUsageAttribute(AttributeTargets.All);
                    attributeUsage.Inherited = true;
                }
            }

            return attributeUsage;
        }

        private static IEnumerable<IAttributeInfo> FilterAttributesOfType(IEnumerable<IAttributeInfo> attributes, ITypeInfo attributeType)
        {
            if (attributeType == null)
                return attributes;

            return FilterAttributesOfTypeInternal(attributes, attributeType);
        }

        private static IEnumerable<IAttributeInfo> FilterAttributesOfTypeInternal(IEnumerable<IAttributeInfo> attributes, ITypeInfo attributeType)
        {
            string qualifiedTypeName = attributeType.FullName;

            foreach (IAttributeInfo attribute in attributes)
                if (ReflectionUtils.IsDerivedFrom(attribute.Type, qualifiedTypeName))
                    yield return attribute;
        }
    }
}
