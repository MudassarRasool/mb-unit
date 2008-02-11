﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Gallio.Collections;

namespace Gallio.Reflection.Impl
{
    /// <summary>
    /// A <see cref="StaticReflectionPolicy"/> type wrapper.
    /// </summary>
    public abstract class StaticTypeWrapper : StaticMemberWrapper, IResolvableTypeInfo
    {
        /// <summary>
        /// Creates a wrapper.
        /// </summary>
        /// <param name="policy">The reflection policy</param>
        /// <param name="handle">The underlying reflection object</param>
        /// <param name="declaringType">The declaring type, or null if none</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="policy"/> or <paramref name="handle"/> is null</exception>
        public StaticTypeWrapper(StaticReflectionPolicy policy, object handle, StaticDeclaredTypeWrapper declaringType)
            : base(policy, handle, declaringType)
        {
        }

        /// <inheritdoc />
        public override CodeElementKind Kind
        {
            get { return CodeElementKind.Type; }
        }

        /// <inheritdoc />
        public override CodeReference CodeReference
        {
            get
            {
                CodeReference reference = Assembly.CodeReference;
                reference.NamespaceName = Namespace.Name;
                reference.TypeName = FullName;
                return reference;
            }
        }

        /// <inheritdoc />
        public abstract IAssemblyInfo Assembly { get; }

        /// <inheritdoc />
        public abstract INamespaceInfo Namespace { get; }

        /// <inheritdoc />
        public abstract ITypeInfo BaseType { get; }

        /// <inheritdoc />
        public virtual string AssemblyQualifiedName
        {
            get { return FullName + @", " + Assembly.FullName; }
        }

        /// <inheritdoc />
        public abstract string FullName { get; }

        /// <inheritdoc />
        public abstract TypeAttributes TypeAttributes { get; }

        /// <summary>
        /// Gets the element type, or null if none.
        /// </summary>
        public virtual StaticTypeWrapper ElementType
        {
            get { return null; }
        }

        ITypeInfo ITypeInfo.ElementType
        {
            get { return ElementType; }
        }

        /// <inheritdoc />
        public virtual int ArrayRank
        {
            get { throw new InvalidOperationException("The type is not an array type."); }
        }

        /// <inheritdoc />
        public virtual bool IsArray
        {
            get { return false; }
        }

        /// <inheritdoc />
        public virtual bool IsPointer
        {
            get { return false; }
        }

        /// <inheritdoc />
        public virtual bool IsByRef
        {
            get { return false; }
        }

        /// <inheritdoc />
        public virtual bool IsGenericParameter
        {
            get { return false; }
        }

        /// <inheritdoc />
        public virtual bool IsGenericType
        {
            get { return false; }
        }

        /// <inheritdoc />
        public virtual bool IsGenericTypeDefinition
        {
            get { return false; }
        }

        /// <inheritdoc />
        public virtual bool ContainsGenericParameters
        {
            get { return false; }
        }

        /// <inheritdoc />
        public virtual IList<ITypeInfo> GenericArguments
        {
            get { return EmptyArray<ITypeInfo>.Instance; }
        }

        /// <inheritdoc />
        public virtual ITypeInfo GenericTypeDefinition
        {
            get { throw new InvalidOperationException("The type is not a generic type."); }
        }

        /// <inheritdoc />
        public virtual TypeCode TypeCode
        {
            get { return ReflectorTypeUtils.GetTypeCode(this); }
        }

        /// <inheritdoc />
        public abstract IList<ITypeInfo> Interfaces { get; }

        /// <inheritdoc />
        public abstract IList<IConstructorInfo> GetConstructors(BindingFlags bindingFlags);

        /// <inheritdoc />
        public virtual IMethodInfo GetMethod(string methodName, BindingFlags bindingFlags)
        {
            if (methodName == null)
                throw new ArgumentNullException("methodName");

            return GetMemberByName(GetMethods(bindingFlags), methodName);
        }

        /// <inheritdoc />
        public abstract IList<IMethodInfo> GetMethods(BindingFlags bindingFlags);

        /// <inheritdoc />
        public virtual IPropertyInfo GetProperty(string propertyName, BindingFlags bindingFlags)
        {
            if (propertyName == null)
                throw new ArgumentNullException("propertyName");

            return GetMemberByName(GetProperties(bindingFlags), propertyName);
        }

        /// <inheritdoc />
        public abstract IList<IPropertyInfo> GetProperties(BindingFlags bindingFlags);

        /// <inheritdoc />
        public virtual IFieldInfo GetField(string fieldName, BindingFlags bindingFlags)
        {
            if (fieldName == null)
                throw new ArgumentNullException("fieldName");

            return GetMemberByName(GetFields(bindingFlags), fieldName);
        }

        /// <inheritdoc />
        public abstract IList<IFieldInfo> GetFields(BindingFlags bindingFlags);

        /// <inheritdoc />
        public virtual IEventInfo GetEvent(string eventName, BindingFlags bindingFlags)
        {
            if (eventName == null)
                throw new ArgumentNullException("eventName");

            return GetMemberByName(GetEvents(bindingFlags), eventName);
        }

        /// <inheritdoc />
        public abstract IList<IEventInfo> GetEvents(BindingFlags bindingFlags);

        /// <inheritdoc />
        public bool IsAssignableFrom(ITypeInfo type)
        {
            throw new NotImplementedException("IsAssignableFrom not implemented for static types yet.");
        }

        /// <inheritdoc />
        public StaticArrayTypeWrapper MakeArrayType(int arrayRank)
        {
            return new StaticArrayTypeWrapper(Policy, this, arrayRank);
        }
        ITypeInfo ITypeInfo.MakeArrayType(int arrayRank)
        {
            return MakeArrayType(arrayRank);
        }

        /// <inheritdoc />
        public StaticPointerTypeWrapper MakePointerType()
        {
            return new StaticPointerTypeWrapper(Policy, this);
        }
        ITypeInfo ITypeInfo.MakePointerType()
        {
            return MakePointerType();
        }

        /// <inheritdoc />
        public StaticByRefTypeWrapper MakeByRefType()
        {
            return new StaticByRefTypeWrapper(Policy, this);
        }
        ITypeInfo ITypeInfo.MakeByRefType()
        {
            return MakeByRefType();
        }

        /// <inheritdoc />
        public virtual StaticDeclaredTypeWrapper MakeGenericType(IList<ITypeInfo> genericArguments)
        {
            throw new InvalidOperationException("The type is not a generic type definition.");
        }
        ITypeInfo ITypeInfo.MakeGenericType(IList<ITypeInfo> genericArguments)
        {
            return MakeGenericType(genericArguments);
        }

        /// <summary>
        /// Applies a type substitution and returns the resulting type.
        /// </summary>
        /// <param name="substitution">The substitution</param>
        /// <returns>The type after substitution has been performed</returns>
        protected internal virtual ITypeInfo ApplySubstitution(StaticTypeSubstitution substitution)
        {
            return this;
        }

        /// <inheritdoc />
        public bool Equals(ITypeInfo other)
        {
            return Equals((object)other);
        }

        /// <inheritdoc />
        public Type Resolve(bool throwOnError)
        {
            return Resolve(null, throwOnError);
        }

        /// <inheritdoc />
        public Type Resolve(MethodInfo methodContext, bool throwOnError)
        {
            return ReflectorResolveUtils.ResolveType(this, methodContext, throwOnError);
        }

        /// <inheritdoc />
        protected override MemberInfo ResolveMemberInfo(bool throwOnError)
        {
            return Resolve(null, throwOnError);
        }

        /// <inheritdoc />
        protected override IEnumerable<ICodeElementInfo> GetInheritedElements()
        {
            return ReflectorInheritanceUtils.EnumerateSuperTypes(this);
        }

        private static T GetMemberByName<T>(IEnumerable<T> members, string memberName)
            where T : class, IMemberInfo
        {
            T match = null;
            foreach (T member in members)
            {
                if (member.Name == memberName)
                {
                    if (match != null)
                        throw new AmbiguousMatchException(String.Format("Found two members named '{0}'.", memberName));

                    match = member;
                }
            }

            return match;
        }
    }
}
