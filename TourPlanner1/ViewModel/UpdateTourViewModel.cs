using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner1.Model;
using TourPlanner1.Model.Messages;
using TourPlanner1.Utility;
using TourPlanner1.View;
using CommunityToolkit.Mvvm.Messaging;

namespace TourPlanner1.ViewModel
{
    public partial class UpdateTourViewModel : ObservableRecipient
    {

        public UpdateTourViewModel() 
        {
            Messenger.Register<UpdateTourViewModel, UpdateTourMessage>(this, (r, m) =>
            {
                SelectedTour = m.Selected;
                TourName = SelectedTour.Name;
                Description = SelectedTour.Description;
                FromLocation = SelectedTour.FromLocation;
                ToLocation = SelectedTour.ToLocation;
            });
        }

        static IConfig config = new Config();
        DatabaseHandler dbHandler = new(new TourPlannerDbContext(), config);

        [ObservableProperty]
        private Tour selectedTour;

        [ObservableProperty]
        string tourName;
        [ObservableProperty]
        string description;
        [ObservableProperty]
        string fromLocation;
        [ObservableProperty]
        string toLocation;

        [RelayCommand]
        void UpdateTour()
        {
            if (ValidateInput())
            {
                if (dbHandler.UpdateTour(SelectedTour.Id, FromLocation, ToLocation, Description, TourName) != null)
                {
                    OpenErrorWindow("Tour Updated.");
                    Messenger.Send(new TourCreatedMessage(dbHandler.ReadTours()));
                }
                else
                {
                    OpenErrorWindow("Invalid Input!");
                }
            }
            else
            {
                OpenErrorWindow("Invalid Input! Every field needs a value.\nYou can close this Window.");
            }
        }

        bool ValidateInput()
        {
            SelectedTour.Name = InputValidator.SanitizeString(SelectedTour.Name);
            SelectedTour.Description = InputValidator.SanitizeString(SelectedTour.Description);
            SelectedTour.ToLocation = InputValidator.SanitizeString(SelectedTour.ToLocation);
            SelectedTour.FromLocation = InputValidator.SanitizeString(SelectedTour.FromLocation);
            if (String.IsNullOrEmpty(SelectedTour.Name) || String.IsNullOrEmpty(SelectedTour.Description) || String.IsNullOrEmpty(SelectedTour.ToLocation) || String.IsNullOrEmpty(SelectedTour.FromLocation))
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
