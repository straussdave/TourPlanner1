using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner1.Utility;

namespace TourPlannerTests
{
    internal class ReportGeneratorTests
    {
        [Test]
        public void TestMinutesConverison()
        {
            int seconds = 632;
            Assert.That(ReportGenerator.ConvertToHoursAndMinutes(seconds), Is.EqualTo("11 Minutes"));
        }

        [Test]
        public void TestHoursAndMinutesConverison()
        {
            int seconds = 123475;
            Assert.That(ReportGenerator.ConvertToHoursAndMinutes(seconds), Is.EqualTo("34 Hours and 18 Minutes"));
        }

        [Test]
        public void TestCapitalizeFirstLetter()
        {
            Assert.That(ReportGenerator.CapitalizeFirstLetter("teststring"), Is.EqualTo("Teststring"));
        }

        [Test]
        public void TestCapitalizeFirstCapitalLetter()
        {
            Assert.That(ReportGenerator.CapitalizeFirstLetter("Teststring"), Is.EqualTo("Teststring"));
        }

        [Test]
        public void TestCapitalizeInteger()
        {
            Assert.That(ReportGenerator.CapitalizeFirstLetter("1"), Is.EqualTo("1"));
        }
    }
}
