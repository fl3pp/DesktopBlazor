using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace DesktopBlazor
{
    internal static class StringExtensions
    {
        public static string SubstringOrEmpty(this string str, int startIndex)
        {
            if (startIndex >= str.Length)
                return string.Empty;
            else
                return str.Substring(startIndex);
        }

    }
}
