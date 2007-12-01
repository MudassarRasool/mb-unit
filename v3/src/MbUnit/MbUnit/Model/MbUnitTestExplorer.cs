﻿using System.Collections.Generic;
using System.Reflection;
using Gallio.Collections;
using Gallio.Model;
using Gallio.Model.Data;
using Gallio.Model.Reflection;
using MbUnit.Attributes;
using MbUnit.Framework;

namespace MbUnit.Model
{
    /// <summary>
    /// A test explorer for MbUnit.
    /// </summary>
    public class MbUnitTestExplorer : ITestExplorer
    {
        private MbUnitTestBuilder builder;

        private MbUnitTestBuilder Builder
        {
            get
            {
                if (builder == null)
                    builder = new MbUnitTestBuilder(
                        new TemplateTreeBuilder(new RootTemplate(), new TemplateEnumerationOptions()));
                return builder;
            }
        }

        /// <inheritdoc />
        public void Reset()
        {
            builder = null;
        }

        /// <inheritdoc />
        public bool IsTest(ICodeElementInfo element)
        {
            // FIXME: Dummy implementation
            return element.HasAttribute(typeof(TestFixtureAttribute), true)
                || element.HasAttribute(typeof(TestAttribute), true);
        }

        /// <inheritdoc />
        public IEnumerable<ITest> ExploreType(ITypeInfo type)
        {
            // FIXME: Dummy implementation
            if (type.HasAttribute(typeof(FixturePatternAttribute), true))
            {
                ITest fixtureTest = CreateTest(type);

                foreach (IMethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    if (method.HasAttribute(typeof(TestPatternAttribute), true))
                        fixtureTest.AddChild(CreateTest(method));
                }

                yield return fixtureTest;
            }
        }

        /// <inheritdoc />
        public IEnumerable<ITest> ExploreAssembly(IAssemblyInfo assembly)
        {
            ITest assemblyTest = CreateTest(assembly);

            foreach (ITypeInfo type in assembly.GetExportedTypes())
                foreach (ITest childTest in ExploreType(type))
                    assemblyTest.AddChild(childTest);

            if (assemblyTest.Children.Count != 0)
                yield return assemblyTest;
        }

        private static ITest CreateTest(ICodeElementInfo codeElement)
        {
            return new BaseTest(codeElement.Name, codeElement,
                new BaseTemplateBinding(new BaseTemplate(codeElement.Name, codeElement),
                    new TemplateBindingScope(null), EmptyDictionary <ITemplateParameter, IDataFactory>.Instance));
        }
    }
}
