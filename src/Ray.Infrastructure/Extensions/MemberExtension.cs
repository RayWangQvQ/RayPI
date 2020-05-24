using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Ray.Infrastructure.Extensions
{
    public static class MemberExtension
    {
        private static MemberExpression RemoveUnary(Expression toUnwrap)
        {
            if (toUnwrap is UnaryExpression)
            {
                return ((UnaryExpression)toUnwrap).Operand as MemberExpression;
            }

            return toUnwrap as MemberExpression;
        }

        public static MemberInfo GetMember<T, TProperty>(this Expression<Func<T, TProperty>> expression)
        {
            var memberExp = RemoveUnary(expression.Body);
            return memberExp?.Member;
        }
    }
}
