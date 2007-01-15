﻿/// Refly License
/// 
/// Copyright (c) 2004 Jonathan de Halleux, http://www.dotnetwiki.org
///
/// This software is provided 'as-is', without any express or implied warranty. 
/// In no event will the authors be held liable for any damages arising from 
/// the use of this software.
/// 
/// Permission is granted to anyone to use this software for any purpose, 
/// including commercial applications, and to alter it and redistribute it 
/// freely, subject to the following restrictions:
///
/// 1. The origin of this software must not be misrepresented; 
/// you must not claim that you wrote the original software. 
/// If you use this software in a product, an acknowledgment in the product 
/// documentation would be appreciated but is not required.
/// 
/// 2. Altered source versions must be plainly marked as such, 
/// and must not be misrepresented as being the original software.
///
///3. This notice may not be removed or altered from any source distribution.

using System;
using System.CodeDom;

namespace Refly.CodeDom.Expressions
{
    using Refly.CodeDom.Collections;
    /// <summary>
    /// Summary description for ObjectCreationExpression.
    /// </summary>
    public class ArrayCreationWithInitializersExpression : Expression
    {
        private ITypeDeclaration type;
        private ExpressionCollection initializers = new ExpressionCollection();

        public ArrayCreationWithInitializersExpression(
            ITypeDeclaration type,
            params Expression[] initializers)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            this.type = type;
            this.initializers.AddRange(initializers);
        }

        public override System.CodeDom.CodeExpression ToCodeDom()
        {
            CodeExpression[] arguments = new CodeExpression[this.initializers.Count];
            int i = 0;
            foreach (Expression e in this.initializers)
            {
                arguments[i++] = e.ToCodeDom();
            }
            return new CodeArrayCreateExpression(
                this.type.TypeReference,
                arguments
                );
        }
    }
}
