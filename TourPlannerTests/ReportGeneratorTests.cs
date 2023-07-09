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
        public void TestHoursAndMinutesConverison()
        {
            int seconds = 632;
            Assert.That(ReportGenerator.ConvertToHoursAndMinutes(seconds), Is.EqualTo("11 Minutes"));
        }
    }
}
