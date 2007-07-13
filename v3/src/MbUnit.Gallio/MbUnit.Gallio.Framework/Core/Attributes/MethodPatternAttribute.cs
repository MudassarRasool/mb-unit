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
using MbUnit.Framework.Model;
using MbUnit.Framework.Core.Model;
using MbUnit.Framework.Utilities;

namespace MbUnit.Framework.Core.Attributes
{
    /// <summary>
    /// <para>
    /// Declares that a method in a fixture class represents an MbUnit test method.
    /// Subclasses of this attribute can customize how test template enumeration takes
    /// place within a fixture.
    /// </para>
    /// <para>
    /// At most one attribute of this type may appear on any given class.
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=true)]
    public abstract class MethodPatternAttribute : PatternAttribute
    {
        /// <summary>
        /// Gets a default instance of the test pattern attribute to use
        /// when none was specified.
        /// </summary>
        public static readonly MethodPatternAttribute DefaultInstance = new DefaultImpl();

        /// <summary>
        /// Creates a test method template.
        /// This method is called when a test method is discovered via reflection to
        /// create a new model object to represent it.
        /// </summary>
        /// <param name="builder">The test template tree builder</param>
        /// <param name="fixtureTemplate">The containing fixture template</param>
        /// <param name="methodInfo">The test method</param>
        /// <returns>The test method template</returns>
        public virtual MbUnitTestMethodTemplate CreateTemplate(TestTemplateTreeBuilder builder,
            MbUnitTestFixtureTemplate fixtureTemplate, MethodInfo methodInfo)
        {
            return new MbUnitTestMethodTemplate(fixtureTemplate, methodInfo);
        }

        /// <summary>
        /// Applies contributions to a method template.
        /// This method is called after the test method template is linked to the template tree.
        /// </summary>
        /// <remarks>
        /// A typical use of this method is to apply additional metadata to model
        /// objects in the test template tree and to further expand the tree using
        /// declarative metadata derived via reflection.
        /// </remarks>
        /// <param name="builder">The test template tree builder</param>
        /// <param name="methodTemplate">The method template</param>
        public virtual void Apply(TestTemplateTreeBuilder builder, MbUnitTestMethodTemplate methodTemplate)
        {
            // FIXME: Should we apply decorators in two separate sorted batches or merge them
            //        together to produce a single ordered collection?
            MethodDecoratorPatternAttribute.ProcessDecorators(builder, methodTemplate, methodTemplate.Method);
            MethodDecoratorPatternAttribute.ProcessDecorators(builder, methodTemplate, methodTemplate.Method.ReflectedType);
            MetadataPatternAttribute.ProcessMetadata(builder, methodTemplate, methodTemplate.Method);

            ProcessParameters(builder, methodTemplate);
        }

        /// <summary>
        /// Processes a method using reflection to populate tests and other executable components.
        /// </summary>
        /// <param name="builder">The test template tree builder</param>
        /// <param name="fixtureTemplate">The fixture template</param>
        /// <param name="method">The method to process</param>
        public static void ProcessMethod(TestTemplateTreeBuilder builder, MbUnitTestFixtureTemplate fixtureTemplate, MethodInfo method)
        {
            MethodPatternAttribute methodPatternAttribute = ReflectionUtils.GetAttribute<MethodPatternAttribute>(method);
            if (methodPatternAttribute == null)
                return;

            MbUnitTestMethodTemplate methodTemplate = methodPatternAttribute.CreateTemplate(builder, fixtureTemplate, method);
            fixtureTemplate.AddMethodTemplate(methodTemplate);
            methodPatternAttribute.Apply(builder, methodTemplate);
        }

        /// <summary>
        /// Processes all parameters using reflection to populate method parameters.
        /// </summary>
        /// <param name="builder">The test template tree builder</param>
        /// <param name="methodTemplate">The method template</param>
        protected virtual void ProcessParameters(TestTemplateTreeBuilder builder, MbUnitTestMethodTemplate methodTemplate)
        {
            MbUnitTestParameterSet parameterSet = methodTemplate.CreateAnonymousParameterSet();
            foreach (ParameterInfo parameter in methodTemplate.Method.GetParameters())
            {
                ProcessParameter(builder, methodTemplate, parameterSet, parameter);
            }
        }

        /// <summary>
        /// Processes a parameter using reflection to populate method parameters.
        /// </summary>
        /// <param name="builder">The test template tree builder</param>
        /// <param name="methodTemplate">The method template</param>
        /// <param name="parameterSet">The default parameter set to use unless overridden</param>
        /// <param name="parameter">The parameter</param>
        protected virtual void ProcessParameter(TestTemplateTreeBuilder builder, MbUnitTestMethodTemplate methodTemplate,
            MbUnitTestParameterSet parameterSet, ParameterInfo parameter)
        {
            ParameterPatternAttribute.ProcessSlot(builder, parameterSet, new Slot(parameter));
        }

        private class DefaultImpl : MethodPatternAttribute
        {
        }
    }
}
