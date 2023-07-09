using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace TourPlanner1.Model
{
    public partial class Log
    {
        public Log(int tourId, DateTime tourDate, string comment, int difficulty, int totalTime, int rating) 
        {
            TourId = tourId;
            TourDate = tourDate;
            Comment = comment;
            Difficulty = difficulty;
            TotalTime = totalTime;
            Rating = rating;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TourId { get; set; }

        public DateTime TourDate { get; set; }

        public string Comment { get; set; }

        public int Difficulty { get; set; }

        public int TotalTime { get; set; }

        public int Rating { get; set; }

        public virtual Tour Tour { get; set; }
    }
}