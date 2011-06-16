using System;
using System.ComponentModel;

namespace alexn.Extensions {
    public static class EnumerationExtensions
    {
        /// <summary>
        /// Get description attribute for a enum
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum instance)
        {
            var type = instance.GetType();
            var member = type.GetMember(instance.ToString());

            if(member.Length > 0)
            {
                var attribute = member[0].GetCustomAttributes(typeof (DescriptionAttribute), false);
                if (attribute.Length > 0)
                    return ((DescriptionAttribute) attribute[0]).Description;
            }

            // No matching value was found
            return instance.ToString();
        }
    }
}