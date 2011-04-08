using System;
using System.Collections.Generic;
using alexn.Extensions;

namespace alexn
{
    public static class Guard
    {
        public static class Against
        {
            public static void Null(object instance)
            {
                if(instance == null)
                    throw new ArgumentNullException();
            }

            public static void NullOrEmpty(string instance)
            {
                if (instance.IsNullOrEmpty())
                    throw new ArgumentNullException();
            }

            public static void NullOrEmpty<T>(IEnumerable<T> instance)
            {
                if (instance.IsNullOrEmpty())
                    throw new ArgumentNullException();
            }
        }
    }
}