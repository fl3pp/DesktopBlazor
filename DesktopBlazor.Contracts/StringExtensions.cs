using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopBlazor
{
    public static class StringExtensions
    {
        public static string ToBase64(this string str)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string From64(this string str)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

    }
}
