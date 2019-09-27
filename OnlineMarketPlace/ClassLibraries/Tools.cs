using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.ClassLibraries
{
    public class Tools
    {
        public static string LinkCorrection(string Text)
        {
            string newText = Text;
            if (Text.Length > 0 || Text != null)
            {
                char[] seperators = new char[] { ' ', ';', ',', '\r', '\t', '\n', '_', '\\', '.', '/', ':', '~', '!', '@', '#', '$', '%', '^', '&', '*', '(',')', '+', '=', '|'  };

                newText = Text.Replace(seperators, "-");
            }
            return newText;
        }
    }
    public static class ExtensionMethods
    {
        public static string Replace(this string s, char[] separators, string newVal)
        {
            string[] temp;

            temp = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return String.Join(newVal, temp);
        }
    }
}
