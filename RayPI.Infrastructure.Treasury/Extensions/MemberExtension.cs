using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace RayPI.Treasury.Extensions
{
    public static class MemberExtension
    {
        /// <summary>Removes the unary.</summary>
        /// <param name="toUnwrap">To unwrap.</param>
        /// <returns>MemberExpression.</returns>
        private static MemberExpression RemoveUnary(Expression toUnwrap)
        {
            if (toUnwrap is UnaryExpression)
                return ((UnaryExpression)toUnwrap).Operand as MemberExpression;
            return toUnwrap as MemberExpression;
        }

        /// <summary>Gets the member.</summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty">The type of the t property.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>MemberInfo.</returns>
        public static MemberInfo GetMember<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            return RemoveUnary(expression.Body)?.Member;
        }
    }
}
