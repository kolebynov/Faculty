using System;
using System.Collections.Generic;
using System.Text;

namespace Faculty.EFCore.Extensions
{
    public static class ExceptionExtensions
    {
        public static void CheckArgumentNull<T>(this T argument, string name = null) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
