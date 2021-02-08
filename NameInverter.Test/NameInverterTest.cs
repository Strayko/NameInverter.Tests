using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace NameInverter.Test
{
    public class Tests
    {
        private static void AssertInverted(string invertedName, string originalName)
        {
            Assert.AreEqual(invertedName, InvertName(originalName));
        }

        [Test]
        public void GivenNull_ReturnsEmptyString()
        {
            AssertInverted("", null);
        }

        [Test]
        public void GivenEmptyString_ReturnEmptyString()
        {
            AssertInverted("", "");
        }

        [Test]
        public void GivenSimpleName_ReturnSimpleName()
        {
            AssertInverted("Name", "Name");
        }

        [Test]
        public void GivenFirstLast_ReturnLastFirst()
        {
            AssertInverted("First Last", "Last First");
        }

        [Test]
        public void GivenASimpleNameWithSpaces_ReturnSimpleNameWithoutSpaces()
        {
            AssertInverted("Name", " Name ");
        }

        [Test]
        public void GivenFirstLastWithExtraSpaces_ReturnLastFirst()
        {
            AssertInverted("Last First", "  First  Last  ");
        }

        [Test]
        public void IgnoreHonorific()
        {
            AssertInverted("Last First", "Mr. First Last");
        }

        private static string InvertName(string name)
        {
            if (name == null || name.Length <= 0)
            {
                return "";
            }
            else
            {
                var regex = new Regex(" +");
                List<string> names = SplitNames(name, regex);
                if (names.Count > 1 && names[0].Equals("Mr.")) names.RemoveAt(0);
                
                if (names.Count == 1) return names[0];
                return String.Format("{0} {1}", names[1], names[0]);
            }
        }

        private static List<string> SplitNames(string name, Regex regex)
        {
            return new List<string>(regex.Split(name.Trim()));
        }
    }
}