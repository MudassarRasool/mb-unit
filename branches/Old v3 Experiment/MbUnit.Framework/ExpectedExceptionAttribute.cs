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
using MbUnit.Framework;
using TestDriven.UnitTesting.Exceptions;
using TestDriven.UnitTesting;

namespace MbUnit.Framework
{
	
	/// <summary>
	/// Tags method that should throw an exception.
	/// </summary>
	/// <include file="MbUnit.Framework.Doc.xml" path="doc/remarkss/remarks[@name='ExpectedExceptionAttribute']"/>	
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ExpectedExceptionAttribute : TestDecoratorAttributeBase
    {
		Type exceptionType;
		
		public ExpectedExceptionAttribute(Type exceptionType)
		{
			if (exceptionType==null)
				throw new ArgumentNullException("exceptionType");
			this.exceptionType = exceptionType;
		}
	
		public Type ExceptionType
		{
			get
			{
				return this.exceptionType;
			}
		}

        public override ITestCase Decorate(ITestCase testCase)
        {
            return new ExplicitDecorator(testCase);
        }

        class ExplicitDecorator : DecoratorTestCaseBase
        {
            public ExplicitDecorator(ITestCase testCase)
                : base(testCase)
            {
            }

            public override bool IsExplicit
            {
                get { return true; }
            }

            public override void Run(object fixtureInstance)
            {
                try
                {

                }
                catch { }
            }
        }

        //public override IRunInvoker GetInvoker(IRunInvoker invoker)
        //{
        //    return new ExpectedExceptionRunInvoker(invoker, this.ExceptionType, this.Description);
        //}
	}
}
