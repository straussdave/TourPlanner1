using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner1.Model;
using TourPlanner1.Model.Messages;
using TourPlanner1.Utility;
using static TourPlanner1.Model.RouteResponse;
using CommunityToolkit.Mvvm.Messaging;
using System.Linq.Expressions;
using TourPlanner1.View;
using log4net;

namespace TourPlanner1.ViewModel
{
    public partial class CreateLogViewModel : ObservableRecipient
    {
        public CreateLogViewModel()
        {
            Messenger.Register<CreateLogViewModel, CreateLogMessage>(this, (r, m) =>
            {
                tourId = m.TourId;
            });
        }

        static IConfig config = new Config();
        DatabaseHandler dbHandler = new(new TourPlannerDbContext(), config);
        private static readonly log4net.ILog log = new Logger().log;

        [ObservableProperty]
        DateTime date = DateTime.Now;
        [ObservableProperty]
        string comment;
        [ObservableProperty]
        string difficulty;
        [ObservableProperty]
        string rating;
        [ObservableProperty]
        string totalTime;

        int tourId;

        [RelayCommand]
        public void CreateLog()
        {
            if (ValidateInput())
            {
                if (dbHandler.CreateLog(tourId, Date, Comment, Int32.Parse(Difficulty), Int32.Parse(TotalTime)*60, Int32.Parse(Rating)) != null)
                {
                    log.Info("Log Created");
                    OpenErrorWindow("Log Created.");
                    Messenger.Send(new LogCreatedMessage(dbHandler.ReadLogs()));
                }
                else
                {
                    log.Info("Invalid Input in Tour Creation");
                    OpenErrorWindow("Invalid Input!");
                }
            }
            else
            {
                log.Info("Invalid Input in Tour Creation");
                OpenErrorWindow("Invalid Input! You can close this Window.");
            }
        }

        bool ValidateInput()
        {
            try{
                Int32.Parse(Rating);
            }
            catch{
                return false;
            }
            try
            {
                Int32.Parse(TotalTime);
            }
            catch
            {
                return false;
            }
            try
            {
                Int32.Parse(Difficulty);
            }
            catch
            {
                return false;
            }
            Comment = InputValidator.SanitizeString(Comment);
            Difficulty = InputValidator.SanitizeString(Difficulty);
            Rating = InputValidator.SanitizeString(Rating);
            TotalTime = InputValidator.SanitizeString(TotalTime);
            if (String.IsNullOrEmpty(Comment) || String.IsNullOrEmpty(Difficulty) || String.IsNullOrEmpty(TotalTime) || String.IsNullOrEmpty(Rating))
            {
                return false;
            }
            else if(!(Int32.Parse(Difficulty) > 0) || !(Int32.Parse(Difficulty) !< 6))
            {
                return false;
            }
            else if (!(Int32.Parse(Rating) > 0) || !(Int32.Parse(Rating)! < 6))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        void OpenErrorWindow(string message)
        {
            ErrorWindow errorWindow = new(message);
            errorWindow.Show();
        }
    }
}
