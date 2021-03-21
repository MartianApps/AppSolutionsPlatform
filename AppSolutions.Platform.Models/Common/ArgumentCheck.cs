using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Platform.Models.Common
{
    public static class ArgumentCheck
    {
        public static void IsNotNull(object obj, string argumentName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"argument '{argumentName}' must not be null!");
            }
        }

        public static void IsNull(object obj, string argumentName)
        {
            if (obj != null)
            {
                throw new ArgumentNullException($"argument '{argumentName}' must not be null!");
            }
        }

        public static void IsNotNullOrEmpty(string obj, string argumentName)
        {
            if (string.IsNullOrEmpty(obj))
            {
                throw new ArgumentNullException($"string argument '{argumentName}' must not be null nor empty!");
            }
        }

        public static void IsTrue(bool value, string message)
        {
            if (!value)
            {
                throw new ArgumentException(message);
            }
        }

        public static void IsFalse(bool value, string message)
        {
            if (value)
            {
                throw new ArgumentException(message);
            }
        }
    }
}
