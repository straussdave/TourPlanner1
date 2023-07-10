using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner1.Utility;

namespace TourPlannerTests
{
    internal class InputValidationTest
    {
        [Test]
        public void TestCleanInput()
        {
            Assert.That(InputValidator.SanitizeString("Hello World!"), Is.EqualTo("Hello World!"));
        }

        [Test]
        public void TestDirtyInput()
        {
            Assert.That(InputValidator.SanitizeString("§$%&H^^e\"ll@o Wo\\rl*+d!"), Is.EqualTo("Hello World!"));
        }

        [Test]
        public void TestInputLengthNull()
        {
            Assert.That(InputValidator.CheckLength(""), Is.EqualTo(false));
        }

        [Test]
        public void TestInputLengthPositive()
        {
            Assert.That(InputValidator.CheckLength("test"), Is.EqualTo(true));
        }
    }
}
