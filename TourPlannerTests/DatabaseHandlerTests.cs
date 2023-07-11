using Autofac.Extras.Moq;
using Bogus;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TourPlanner1.Model;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TourPlannerTests
{
    internal class DatabaseHandlerTests
    {
        [Test]
        public void CreateTourTest()
        {
            var stub = GenerateTourData(1);
            var data = stub.AsQueryable();
            var mockTourPlannerDbContext = new Mock<TourPlannerDbContext>();
            var mockSet = new Mock<DbSet<Tour>>();
            mockSet.As<IQueryable<Tour>>().Setup(x => x.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Tour>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Tour>>().Setup(x => x.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Tour>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            mockTourPlannerDbContext.Setup(x => x.Tours).Returns(mockSet.Object);
            mockTourPlannerDbContext.Setup(x => x.SaveChanges()).Returns(1);

            var tourObject = stub.FirstOrDefault();
            var mockdbHandler = new Mock<DatabaseHandler>();
            IConfig mockConfig = new MockConfig();
            IDatabaseHandler dbHandler = new DatabaseHandler(mockTourPlannerDbContext.Object, mockConfig);
            
            var response = dbHandler.CreateTour(tourObject.FromLocation, tourObject.ToLocation, tourObject.Description, tourObject.Name);

            Assert.That(response, Is.AtLeast(0));
        }

        private List<Tour> GenerateTourData(int count)
        {
            var faker = new Faker<Tour>()
                        .RuleFor(c => c.Name, f => f.Lorem.Word())
                        .RuleFor(c => c.Description, f => f.Lorem.Text())
                        .RuleFor(c => c.FromLocation, f => "Pöchlarn")
                        .RuleFor(c => c.ToLocation, f => "Wien")
                        .RuleFor(c => c.TransportType, f => "car")
                        .RuleFor(c => c.TourDistance, f => f.Random.Int(5, 5000))
                        .RuleFor(c => c.EstimatedTime, f => f.Random.Int(100, 50000))
                        .RuleFor(c => c.RouteImage, f => "image.jpg");

            return faker.Generate(count);
        }

        public class MockConfig : IConfig
        {
            public string ConnectionString => "ConnectionString";
            public string MapHandlerKey => "SaHz8hpWGbAFt6NqaZvr7GhQnFd8cTEK";
            public string MapHandlerWidth => "800";
            public string MapHandlerHeight => "400";
            public string Mode => "unitTest";
        }
    }
}
