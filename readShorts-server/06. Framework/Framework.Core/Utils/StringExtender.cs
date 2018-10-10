using System.Linq;
using System.Text.RegularExpressions;

namespace Framework.Core.Utils
{
    public static class StringExtender
    {
        public static int NthIndexOf(this string target, string value, int n)
        {
            Match m = Regex.Match(target, "((" + value + ").*?){" + n + "}");

            if (m.Success)
                return m.Groups[2].Captures[n - 1].Index;

            return -1;
        }

        public static string[] SplitByWidth(string s, int[] widths)
        {
            string[] ret = new string[widths.Length];
            char[] c = s.ToCharArray();
            int startPos = 0;
            for (int i = 0; i < widths.Length; i++)
            {
                int width = widths[i];
                ret[i] = new string(c.Skip(startPos).Take(width).ToArray<char>());
                startPos += width;
            }
            return ret;
        }

        public static string GetRandomString()
        {
            string path = System.Guid.NewGuid().ToString();
            return path;
        }

        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}