using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner1.Model.Messages;
using TourPlanner1.Model;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using static TourPlanner1.Model.RouteResponse;
using TourPlanner1.Utility;
using TourPlanner1.View;

namespace TourPlanner1.ViewModel
{
    public partial class UpdateLogViewModel : ObservableRecipient
    {
        public UpdateLogViewModel()
        {
            Messenger.Register<UpdateLogViewModel, UpdateLogMessage>(this, (r, m) =>
            {
                SelectedLog = m.Selected;
                Date = SelectedLog.TourDate;
                Comment = SelectedLog.Comment;
                Difficulty = SelectedLog.Difficulty.ToString();
                Rating = SelectedLog.Rating.ToString();
                TotalTime = (SelectedLog.TotalTime / 60).ToString();
                tourId = m.tourId;
            });
        }

        static IConfig config = new Config();
        DatabaseHandler dbHandler = new(new TourPlannerDbContext(), config);
        int tourId;

        [ObservableProperty]
        private Log selectedLog;

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

        [RelayCommand]
        public void UpdateLog()
        {
            if (ValidateInput())
            {
                dbHandler.UpdateLog(SelectedLog.Id, Date, Comment, Int32.Parse(Difficulty), Int32.Parse(TotalTime) * 60, Int32.Parse(Rating));
                OpenErrorWindow("Log Updated.");
                Messenger.Send(new LogCreatedMessage(dbHandler.ReadLogs()));
            }
            else
            {
                OpenErrorWindow("Invalid Input!\nYou can close this Window.");
            }
        }

        bool ValidateInput()
        {
            try
            {
                Int32.Parse(Rating);
            }
            catch
            {
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
            else if (!(Int32.Parse(Difficulty) > 0) || !(Int32.Parse(Difficulty)! < 6))
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
