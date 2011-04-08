using System;

namespace alexn.Extensions
{
    public static class DateTimeExtensions
    {
        public static long Timestap(this DateTime instance)
        {
            var ticks = instance.Ticks - new DateTime(1970, 1, 1).Ticks;
            ticks /= 10000000;
            return ticks;
        }

        public static DateTime FirstDayOfMonth(this DateTime instance)
        {
            return new DateTime(instance.Year, instance.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime instance)
        {
            return FirstDayOfMonth(instance).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Return instance with time set to 23:59:59
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static DateTime Midnight(this DateTime instance)
        {
            return new DateTime(instance.Year, instance.Month, instance.Day, 23, 59, 59);
        }
    }
}