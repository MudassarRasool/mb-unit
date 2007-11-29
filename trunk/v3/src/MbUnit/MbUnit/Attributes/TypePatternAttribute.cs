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
using Gallio.Model.Reflection;
using MbUnit.Model;

namespace MbUnit.Attributes
{
    /// <summary>
    /// <para>
    /// Generates a type template from the annotated class.
    /// Subclasses of this attribute can control what happens with the type.
    /// The type might not necessarily represent a test fixture.
    /// </para>
    /// <para>
    /// At most one attribute of this type may appear on any given class.
    /// </para>
    /// </summary>
    /// <seealso cref="TypeDecoratorPatternAttribute"/>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class TypePatternAttribute : PatternAttribute
    {
        /// <summary>
        /// Creates a type template.
        /// This method is called when a type is discovered via reflection to
        /// create a new model object to represent it.
        /// </summary>
        /// <remarks>
        /// A typical use of this method is to apply additional metadata to model
        /// objects in the template tree and to further expand the tree using
        /// declarative metadata derived via reflection.
        /// </remarks>
        /// <param name="builder">The builder</param>
        /// <param name="assemblyTemplate">The containing assembly template</param>
        /// <param name="type">The annotated type</param>
        /// <returns>The type template</returns>
        public virtual MbUnitTypeTemplate CreateTemplate(MbUnitTestBuilder builder,
            MbUnitAssemblyTemplate assemblyTemplate, ITypeInfo type)
        {
            return new MbUnitTypeTemplate(assemblyTemplate, type);
        }

        /// <summary>
        /// <para>
        /// Applies contributions to a type template.
        /// This method is called after the type template is linked to the template tree.
        /// </para>
        /// <para>
        /// Contributions are applied in a very specific order:
        /// <list type="bullet">
        /// <item>Type decorator attributes declared by the containing assembly and the type sorted by order</item>
        /// <item>Metadata attributes declared by the type</item>
        /// <item>Fields, properties, methods and events declared by supertypes before
        /// those declared by subtypes</item>
        /// <item>Constructors</item>
        /// </list>
        /// </para>
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="typeTemplate">The type template</param>
        public virtual void Apply(MbUnitTestBuilder builder, MbUnitTypeTemplate typeTemplate)
        {
            builder.ProcessTypeDecorators(typeTemplate);
            builder.ProcessMetadata(typeTemplate, typeTemplate.Type);

            ProcessMembers(builder, typeTemplate);
            ProcessConstructors(builder, typeTemplate);
        }

        /// <summary>
        /// Processes all public constructors using reflection to populate the
        /// type template's parameters derived from constructor parameters.
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="typeTemplate">The type template</param>
        protected virtual void ProcessConstructors(MbUnitTestBuilder builder, MbUnitTypeTemplate typeTemplate)
        {
            foreach (IConstructorInfo constructor in typeTemplate.Type.GetConstructors(BindingFlags.Instance | BindingFlags.Public))
            {
                ProcessConstructor(builder, typeTemplate, constructor);

                // FIXME: Currently we arbitrarily choose the first constructor and throw away the rest.
                //        This should be replaced by a more intelligent mechanism that can
                //        handle optional or alternative dependencies.  We might benefit from
                //        using an existing inversion of control framework like Castle
                //        to handle stuff like this.
                break;
            }
        }

        /// <summary>
        /// Processes a constructor using reflection to populate the
        /// type template's parameters derived from constructor parameters.
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="typeTemplate">The type template</param>
        /// <param name="constructor">The constructor to process</param>
        protected virtual void ProcessConstructor(MbUnitTestBuilder builder, MbUnitTypeTemplate typeTemplate, IConstructorInfo constructor)
        {
            foreach (IParameterInfo parameter in constructor.GetParameters())
            {
                ProcessConstructorParameter(builder, typeTemplate, parameter);
            }
        }

        /// <summary>
        /// Processes a constructor parameter using reflection to populate template parameters.
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="typeTemplate">The type template</param>
        /// <param name="parameter">The parameter</param>
        protected virtual void ProcessConstructorParameter(MbUnitTestBuilder builder, MbUnitTypeTemplate typeTemplate,
            IParameterInfo parameter)
        {
            builder.ProcessParameter(typeTemplate, parameter);
        }

        /// <summary>
        /// Processes all public fields, properties, methods and events using reflection in order
        /// such that those declared by supertypes are processed before those declared by subtypes
        /// </summary>
        /// <param name="builder">The template tree builder</param>
        /// <param name="typeTemplate">The type template</param>
        protected virtual void ProcessMembers(MbUnitTestBuilder builder, MbUnitTypeTemplate typeTemplate)
        {
            foreach (IFieldInfo field in typeTemplate.Type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                ProcessField(builder, typeTemplate, field);

            foreach (IPropertyInfo property in typeTemplate.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                ProcessProperty(builder, typeTemplate, property);

            foreach (IMethodInfo method in typeTemplate.Type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                ProcessMethod(builder, typeTemplate, method);

            foreach (IEventInfo @event in typeTemplate.Type.GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static))
                ProcessEvent(builder, typeTemplate, @event);
        }

        /// <summary>
        /// Processes a field using reflection to populate the
        /// type template's parameters derived from fields.
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="typeTemplate">The type template</param>
        /// <param name="field">The field to process</param>
        protected virtual void ProcessField(MbUnitTestBuilder builder, MbUnitTypeTemplate typeTemplate, IFieldInfo field)
        {
            builder.ProcessParameter(typeTemplate, field);
        }

        /// <summary>
        /// Processes a property using reflection to populate the
        /// type template's parameters derived from properties.
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="typeTemplate">The type template</param>
        /// <param name="property">The property to process</param>
        protected virtual void ProcessProperty(MbUnitTestBuilder builder, MbUnitTypeTemplate typeTemplate, IPropertyInfo property)
        {
            builder.ProcessParameter(typeTemplate, property);
        }

        /// <summary>
        /// Processes an event using reflection.  The default implementation does nothing.
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="typeTemplate">The type template</param>
        /// <param name="event">The event to process</param>
        protected virtual void ProcessEvent(MbUnitTestBuilder builder, MbUnitTypeTemplate typeTemplate, IEventInfo @event)
        {
        }

        /// <summary>
        /// Processes a method using reflection to populate tests and other executable components.
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="typeTemplate">The type template</param>
        /// <param name="method">The method to process</param>
        protected virtual void ProcessMethod(MbUnitTestBuilder builder, MbUnitTypeTemplate typeTemplate, IMethodInfo method)
        {
            builder.ProcessMethod(typeTemplate, method);
        }

        private T[] SortMembers<T>(T[] members)
            where T : IMemberInfo
        {
            Array.Sort(members, DeclaringTypeComparer<T>.Instance);
            return members;
        }
    }
}