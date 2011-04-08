using System;

namespace alexn.Extensions
{
    public static class IntegerExtensions
    {
        public static DateTime January(this int instance, int year)
        {
            return new DateTime(year, 1, instance);
        }
        
        public static DateTime February(this int instance, int year)
        {
            return new DateTime(year, 2, instance);
        }
        
        public static DateTime March(this int instance, int year)
        {
            return new DateTime(year, 3, instance);
        }
        
        public static DateTime April(this int instance, int year)
        {
            return new DateTime(year, 4, instance);
        }
        
        public static DateTime May(this int instance, int year)
        {
            return new DateTime(year, 5, instance);
        }
        
        public static DateTime June(this int instance, int year)
        {
            return new DateTime(year, 6, instance);
        }
        
        public static DateTime July(this int instance, int year)
        {
            return new DateTime(year, 7, instance);
        }
        
        public static DateTime August(this int instance, int year)
        {
            return new DateTime(year, 8, instance);
        }

        public static DateTime Septeber(this int instance, int year)
        {
            return new DateTime(year, 9, instance);
        }

        public static DateTime October(this int instance, int year)
        {
            return new DateTime(year, 10, instance);
        }

        public static DateTime November(this int instance, int year)
        {
            return new DateTime(year, 11, instance);
        }

        public static DateTime December(this int instance, int year)
        {
            return new DateTime(year, 12, instance);
        }

        public static void Times<T>(this int instance, Func<T> action)
        {
            for(var i = 0; i < instance; i++)
            {
                action();
            }
        }
    }
}