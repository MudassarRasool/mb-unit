// MbUnit Test Framework
// 
// Copyright (c) 2004 Jonathan de Halleux
//
// This software is provided 'as-is', without any express or implied warranty. 
// 
// In no event will the authors be held liable for any damages arising from 
// the use of this software.
// Permission is granted to anyone to use this software for any purpose, 
// including commercial applications, and to alter it and redistribute it 
// freely, subject to the following restrictions:
//
//		1. The origin of this software must not be misrepresented; 
//		you must not claim that you wrote the original software. 
//		If you use this software in a product, an acknowledgment in the product 
//		documentation would be appreciated but is not required.
//
//		2. Altered source versions must be plainly marked as such, and must 
//		not be misrepresented as being the original software.
//
//		3. This notice may not be removed or altered from any source 
//		distribution.
//		
//		MbUnit HomePage: http://www.mbunit.org
//		Author: Jonathan de Halleux



namespace MbUnit.Core.Cons.CommandLine
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Collections;
    using System.IO;
    using System.Text;
    
    /// <summary>
    /// Allows control of command line parsing.
    /// Attach this attribute to instance fields of types used
    /// as the destination of command line argument parsing.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Command line parsing code from Peter Halam, 
    /// http://www.gotdotnet.com/community/usersamples/details.aspx?sampleguid=62a0f27e-274e-4228-ba7f-bc0118ecc41e
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field,AllowMultiple=false,Inherited=true)]
    public class CommandLineArgumentAttribute : Attribute
    {
        /// <summary>
        /// Allows control of command line parsing.
        /// </summary>
        /// <param name="type"> Specifies the error checking to be done on the argument. </param>
        public CommandLineArgumentAttribute(CommandLineArgumentType type)
        {
            this.type = type;
        }
        
        /// <summary>
        /// The error checking to be done on the argument.
        /// </summary>
        public CommandLineArgumentType Type
        {
            get { return this.type; }
        }
        /// <summary>
        /// Returns true if the argument did not have an explicit short name specified.
        /// </summary>
        public bool DefaultShortName    { get { return null == this.shortName; } }
        
        /// <summary>
        /// The short name of the argument.
        /// </summary>
        public string ShortName
        {
            get { return this.shortName; }
            set { this.shortName = value; }
        }

        /// <summary>
        /// Returns true if the argument did not have an explicit long name specified.
        /// </summary>
        public bool DefaultLongName     { get { return null == this.longName; } }
        
        /// <summary>
        /// The long name of the argument.
        /// </summary>
        public string LongName
        {
            get { Debug.Assert(!this.DefaultLongName); return this.longName; }
            set { this.longName = value; }
        }

		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description=value;
			}
		}
        
        private string shortName;
        private string longName;
		private string description="";
        private CommandLineArgumentType type;
    }
}

