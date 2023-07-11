using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner1.Utility;

namespace TourPlanner1.Model
{
    public interface IDatabaseHandler
    {
        public List<Tour> ReadTours();

        public Tour ReadTour(int id);

        public int CreateTour(string fromLocation, string toLocation, string description, string name);

        public Tour UpdateTour(int id, string fromLocation, string toLocation, string description, string name);

        public Tour UpdateTour(Tour oldTour, string fromLocation, string toLocation, string description, string name);

        public void DeleteTour(int id);

        public void DeleteTour(Tour tour);

        public List<Log> ReadLogs();

        public Log ReadLog(int id);

        public Log CreateLog(int tourId, DateTime tourDate, string comment, int difficulty, int TotalTime, int Rating);

        public void UpdateLog(int id, DateTime tourDate, string comment, int difficulty, int totalTime, int rating);

        public void UpdateLog(Log oldLog, DateTime tourDate, string comment, int difficulty, int totalTime, int rating);

        public void DeleteLog(int id);

        public void DeleteLog(Log log);
    }
}
