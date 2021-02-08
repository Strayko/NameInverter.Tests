using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using NUnit.Framework;

namespace NameInverter.Test
{
    public class Tests
    {
        private static NameInverter _nameInverter;
        
        [SetUp]
        public void SetUp()
        {
            _nameInverter = new NameInverter();
        }
        
        private static void AssertInverted(string invertedName, string originalName)
        {
            Assert.AreEqual(invertedName, _nameInverter.InvertName(originalName));
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
        public void IgnoreHonorifics()
        {
            AssertInverted("Last First", "Mr. First Last");
            AssertInverted("Last First", "Mrs. First Last");
        }

        [Test]
        public void PostNominals_StayAtEnd()
        {
            AssertInverted("Last First Sr.", "First Last Sr.");
            AssertInverted("Last First BS. Phd.", "First Last BS. Phd.");
        }

        [Test]
        public void Integration()
        {
            AssertInverted("Last First III esq.", "  First  Last  III   esq.   ");
        }
    }
}