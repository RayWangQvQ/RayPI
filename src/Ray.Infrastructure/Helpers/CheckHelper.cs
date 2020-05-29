using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Autofac;
using Ray.Infrastructure.Extensions;

namespace Ray.Infrastructure.Helpers
{
    [DebuggerStepThrough]
    public static class CheckHelper
    {
        public static T NotNull<T>(
            T value,
            string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static T NotNull<T>(
            T value,
            string parameterName,
            string message)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }

            return value;
        }

        public static string NotNull(
            string value,
            string parameterName,
            int maxLength = int.MaxValue,
            int minLength = 0)
        {
            if (value == null)
            {
                throw new ArgumentException($"{parameterName} can not be null!", parameterName);
            }

            if (value.Length > maxLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or lower than {maxLength}!", parameterName);
            }

            if (minLength > 0 && value.Length < minLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or bigger than {minLength}!", parameterName);
            }

            return value;
        }

        public static string NotNullOrWhiteSpace(
            string value,
            string parameterName,
            int maxLength = int.MaxValue,
            int minLength = 0)
        {
            if (value.IsNullOrWhiteSpace())
            {
                throw new ArgumentException($"{parameterName} can not be null, empty or white space!", parameterName);
            }

            if (value.Length > maxLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or lower than {maxLength}!", parameterName);
            }

            if (minLength > 0 && value.Length < minLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or bigger than {minLength}!", parameterName);
            }

            return value;
        }

        public static string NotNullOrEmpty(
            string value,
            string parameterName,
            int maxLength = int.MaxValue,
            int minLength = 0)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException($"{parameterName} can not be null or empty!", parameterName);
            }

            if (value.Length > maxLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or lower than {maxLength}!", parameterName);
            }

            if (minLength > 0 && value.Length < minLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or bigger than {minLength}!", parameterName);
            }

            return value;
        }

        public static ICollection<T> NotNullOrEmpty<T>(ICollection<T> value, string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);
            }

            return value;
        }

        public static Type AssignableTo<TBaseType>(
            Type type,
            string parameterName)
        {
            NotNull(type, parameterName);

            if (!type.IsAssignableTo<TBaseType>())
            {
                throw new ArgumentException($"{parameterName} (type of {type.AssemblyQualifiedName}) should be assignable to the {typeof(TBaseType).GetFullNameWithAssemblyName()}!");
            }

            return type;
        }

        public static string Length(
            string value,
            string parameterName,
            int maxLength,
            int minLength = 0)
        {
            if (minLength > 0)
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);
                }

                if (value.Length < minLength)
                {
                    throw new ArgumentException($"{parameterName} length must be equal to or bigger than {minLength}!", parameterName);
                }
            }

            if (value != null && value.Length > maxLength)
            {
                throw new ArgumentException($"{parameterName} length must be equal to or lower than {maxLength}!", parameterName);
            }

            return value;
        }
    }
}
