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

using System;
using Gallio.Collections;
using Gallio.Model;
using Gallio.Reflection;
using Gallio.Framework.Pattern;

namespace Gallio.Framework.Pattern
{
    /// <summary>
    /// <para>
    /// Declares that an assembly generates an assembly-level test.
    /// Subclasses of this attribute can customize how test enumeration takes place within
    /// the assembly.
    /// </para>
    /// <para>
    /// At most one attribute of this type may appear on any given assembly.
    /// </para>
    /// </summary>
    /// <seealso cref="TestAssemblyDecoratorPatternAttribute"/>
    [AttributeUsage(PatternAttributeTargets.TestAssembly, AllowMultiple = false, Inherited = true)]
    public abstract class TestAssemblyPatternAttribute : PatternAttribute
    {
        /// <summary>
        /// Gets a default instance of the assembly pattern attribute to use
        /// when no other pattern consumes an assembly.
        /// </summary>
        public static readonly TestAssemblyPatternAttribute DefaultInstance = new DefaultImpl();

        /// <inheritdoc />
        public override bool IsPrimary
        {
            get { return true; }
        }

        /// <inheritdoc />
        public override bool IsTest(IPatternEvaluator evaluator, ICodeElementInfo codeElement)
        {
            return true;
        }

        /// <inheritdoc />
        public override void Consume(IPatternScope containingScope, ICodeElementInfo codeElement, bool skipChildren)
        {
            IAssemblyInfo assembly = codeElement as IAssemblyInfo;
            Validate(containingScope, assembly);

            IPatternScope assemblyScope = containingScope.CreateChildTestScope(assembly.Name, assembly);
            assemblyScope.TestBuilder.Kind = TestKinds.Assembly;

            InitializeAssemblyTest(assemblyScope, assembly);
            SetTestSemantics(assemblyScope.TestBuilder, assembly);

            if (skipChildren)
                PrepareToPopulateChildrenOnDemand(assemblyScope, assembly);
            else
                PopulateChildrenImmediately(assemblyScope, assembly);

            assemblyScope.TestBuilder.ApplyDeferredActions();
        }

        /// <summary>
        /// Verifies that the attribute is being used correctly.
        /// </summary>
        /// <param name="containingScope">The containing scope</param>
        /// <param name="assembly">The assembly</param>
        /// <exception cref="PatternUsageErrorException">Thrown if the attribute is being used incorrectly</exception>
        protected virtual void Validate(IPatternScope containingScope, IAssemblyInfo assembly)
        {
            if (!containingScope.CanAddChildTest || assembly == null)
                ThrowUsageErrorException("This attribute can only be used on a test assembly.");
        }

        /// <summary>
        /// Initializes a test for an assembly after it has been added to the test model.
        /// </summary>
        /// <param name="assemblyScope">The assembly scope</param>
        /// <param name="assembly">The assembly</param>
        protected virtual void InitializeAssemblyTest(IPatternScope assemblyScope, IAssemblyInfo assembly)
        {
            MetadataMap metadata = new MetadataMap();
            ModelUtils.PopulateMetadataFromAssembly(assembly, metadata);
            foreach (var pair in metadata.Pairs)
                assemblyScope.TestBuilder.AddMetadata(pair.Key, pair.Value);

            assemblyScope.Process(assembly);
        }

        /// <summary>
        /// <para>
        /// Applies semantic actions to the assembly-level test to estalish its runtime behavior.
        /// </para>
        /// <para>
        /// This method is called after <see cref="InitializeAssemblyTest" />.
        /// </para>
        /// </summary>
        /// <remarks>
        /// <para>
        /// The default behavior for a <see cref="TestAssemblyPatternAttribute" />
        /// is to configure the test actions as follows:
        /// <list type="bullet">
        /// <item><see cref="IPatternTestHandler.InitializeTest" />: Reset the <see cref="TestAssemblyExecutionParameters" />
        /// to defaults.</item>
        /// </list>
        /// </para>
        /// <para>
        /// You can override this method to change the semantics as required.
        /// </para>
        /// </remarks>
        /// <param name="testBuilder">The test builder</param>
        /// <param name="assembly">The assembly</param>
        protected virtual void SetTestSemantics(ITestBuilder testBuilder, IAssemblyInfo assembly)
        {
            testBuilder.TestActions.InitializeTestChain.After(
                delegate(PatternTestState testInstanceState)
                {
                    TestAssemblyExecutionParameters.Reset();
                });
        }

        /// <summary>
        /// Populates the children of the assembly test all at once.
        /// </summary>
        /// <remarks>
        /// The default implementation processes all public and non-public types within the assembly.
        /// </remarks>
        /// <param name="assemblyScope">The assembly scope</param>
        /// <param name="assembly">The assembly</param>
        protected virtual void PopulateChildrenImmediately(IPatternScope assemblyScope, IAssemblyInfo assembly)
        {
            foreach (ITypeInfo type in assembly.GetTypes())
            {
                if (!type.IsNested)
                    assemblyScope.Consume(type, false, DefaultTypePattern);
            }
        }

        /// <summary>
        /// Prepares to populate the children of the assembly test on demand by
        /// adding a deferred populator with <see cref="IPatternScope.AddDeferredComponentPopulator" />.
        /// </summary>
        /// <param name="assemblyScope">The assembly scope</param>
        /// <param name="assembly">The assembly</param>
        protected virtual void PrepareToPopulateChildrenOnDemand(IPatternScope assemblyScope, IAssemblyInfo assembly)
        {
            HashSet<ITypeInfo> populatedTypes = new HashSet<ITypeInfo>();
            assemblyScope.AddDeferredComponentPopulator(childCodeElementHint =>
            {
                ITypeInfo type = childCodeElementHint as ITypeInfo;
                if (type != null && ! type.IsNested && !populatedTypes.Contains(type) && assembly.Equals(type.Assembly))
                {
                    populatedTypes.Add(type);
                    assemblyScope.Consume(type, false, DefaultTypePattern);
                }
            });
        }

        /// <summary>
        /// Gets the default pattern to apply to types that do not have a primary pattern, or null if none.
        /// </summary>
        /// <remarks>
        /// The default implementation returns <see cref="TestTypePatternAttribute.AutomaticInstance"/>.
        /// </remarks>
        protected virtual IPattern DefaultTypePattern
        {
            get { return TestTypePatternAttribute.AutomaticInstance; }
        }

        private sealed class DefaultImpl : TestAssemblyPatternAttribute
        {
        }
    }
}