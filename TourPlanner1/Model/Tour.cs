
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using static TourPlanner1.Model.RouteResponse;
using System;

namespace TourPlanner1.Model
{
    public partial class Tour
    {
        public Tour(string fromLocation, string toLocation, RouteRoot routeRoot, string uniqueFilename, string description, string name)
        {
            FromLocation = fromLocation;
            ToLocation = toLocation;
            TransportType = routeRoot.route.legs[0].maneuvers[0].transportMode;
            TourDistance = (int)Math.Round(routeRoot.route.distance * 1.609);
            EstimatedTime = routeRoot.route.realTime;
            RouteImage = uniqueFilename;
            Description = description;
            Name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string   FromLocation { get; set; }

        public string ToLocation { get; set; }

        public string TransportType { get; set; }

        public int TourDistance { get; set; }

        public int EstimatedTime { get; set; }

        public string RouteImage { get; set; }

        public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
    }
}