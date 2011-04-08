using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace alexn.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string instance)
        {
            return string.IsNullOrEmpty(instance);
        }

        /// <summary>
        /// Format a string (using string.format)
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string With(this string instance, string format)
        {
            return string.Format(instance, format);
        }

        public static string UppercaseFirst(this string instance)
        {
            if (instance.IsNullOrEmpty())
                return instance;

            var chars = instance.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);

            return new string(chars);
        }

        public static string ToSlug(this string instance, int length = 50)
        {
            Guard.Against.NullOrEmpty(instance);

            var slug = instance.ToLower().Trim();

            slug = Regex.Replace(slug, @"\s|\.|://", "-");
            slug = Regex.Replace(slug, @"[^a-z0-9\s\-]", "");
            slug = Regex.Replace(slug, @"\s+", " ").Trim();

            if (slug.Length > length)
                slug = slug.Substring(0, length);

            return slug;
        }

        /// <summary>
        /// Remove all accents from a string
        /// </summary>
        /// <example>
        /// å = a
        /// è = e
        /// </example>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static string RemoveAccent(this string instance)
        {
            Guard.Against.NullOrEmpty(instance);

            var buffer = Encoding.GetEncoding("Cyrillic").GetBytes(instance);
            return Encoding.ASCII.GetString(buffer);
        }

        public static string Md5(this string instance)
        {
            var md5 = MD5.Create();
            var encoding = Encoding.ASCII;

            var buffer = encoding.GetBytes(instance);
            var hash = md5.ComputeHash(buffer);

            var sb = new StringBuilder();
            for(var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}