using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NameInverter.Test
{
    public class NameInverter
    {
        public string InvertName(string name)
        {
            if (name == null || name.Length <= 0)
                return "";
            else
                return FormatName(RemoveHonorifics(SplitNames(name)));
        }

        private static string FormatName(List<string> names)
        {
            if (names.Count == 1)
            {
                return names[0];
            }
            else
            {
                return FormatMultiElementName(names);
            }
        }

        private static string FormatMultiElementName(List<string> names)
        {
            var postNominal = GetPostNominals(names);
            string firstName = names[0];
            string lastName = names[1];
            return String.Format("{0} {1} {2}", lastName, firstName, postNominal).Trim();
        }

        private static string GetPostNominals(List<string> names)
        {
            string postNominalString = "";
            if (names.Count > 2)
            {
                var postNominals = names.GetRange(2, names.Count - 2);
                
                foreach (var pn in postNominals)
                {
                    postNominalString += pn + " ";
                }
            }

            return postNominalString;
        }

        private static List<string> RemoveHonorifics(List<string> names)
        {
            if (names.Count > 1 && IsHonorific(names[0])) names.RemoveAt(0);
            return names;
        }

        private static bool IsHonorific(string word)
        {
            string[] str = {"Mr.", "Mrs."};
            return str.Any(c => c.Contains(word));
        }

        private static List<string> SplitNames(string name)
        {
            var regex = new Regex(" +");
            return new List<string>(regex.Split(name.Trim()));
        }
    }
}