using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace  Mini.Social.Media.Domain.Extensions
{
    public static class StringExtensions
    {

        public static int ToInt32(this string str)
        {
            int i;
            _ = int.TryParse(str, out i);
            return i;
        }
        public static int ToInt32(this object str)
        {
            int i;
            _ = int.TryParse(str.ToString(), out i);
            return i;
        }
        public static decimal ToDecimal(this string str)
        {
            return Convert.ToDecimal(str == string.Empty ? null : str);
        }
        public static double ToDouble(this string str)
        {
            return Convert.ToDouble(str == string.Empty ? null : str);
        }
        public static DateTime ToDateTime(this string str)
        {
            return Convert.ToDateTime(str == string.Empty ? null : str);
        }
    }
}