using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class TextExtensions
    {
        public static string Pluralize(this string value, int count = 0)
        {
            return count == 1 ? value : value + "s";
        }
        public static string Dot(this string value)
        {
            return value += ".";
        }
        public static string Exclamation(this string value)
        {
            return value += "!";
        }
        public static string Question(this string value)
        {
            return value += "?";
        }
        public static string Repeat(this string value, int count)
        {
            if (value == string.Empty)
                return value;
            if (count > 2)
            {
                count -= 3;
                string newStr = value + value + value;
                for (var x = 0; x < count; x++)
                {
                    newStr += value;
                }
                return newStr;
            }
            return count == 2 ? value + value
                : count == 1 ? value
                : string.Empty;
        }

        public static string Repeat(this string value, string target, int count)
        {
            if (target == string.Empty || count == 0)
                return value;
            return value += target.Repeat(count);
        }

        public static string Fill(this string value)
        {
            return value += "?";
        }
    }
}
