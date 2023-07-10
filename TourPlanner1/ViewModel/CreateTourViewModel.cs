﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner1.Model;
using TourPlanner1.Utility;
using TourPlanner1.View;

namespace TourPlanner1.ViewModel
{
    public partial class CreateTourViewModel : ObservableObject
    {
        DatabaseHandler dbHandler = new();

        [ObservableProperty]
        string tourName;
        [ObservableProperty]
        string description;
        [ObservableProperty]
        string fromLocation;
        [ObservableProperty]
        string toLocation;

        [RelayCommand]
        void CreateTour()
        {
            if (ValidateInput())
            {
                dbHandler.CreateTour(FromLocation, ToLocation, Description, TourName);
            }
            else
            {
                OpenErrorWindow("Invalid Input! Every field needs a value.\nYou can close this Window.");
            }
            
        }

        bool ValidateInput()
        {
            TourName = InputValidator.SanitizeString(TourName);
            Description = InputValidator.SanitizeString(Description);
            ToLocation = InputValidator.SanitizeString(ToLocation);
            FromLocation = InputValidator.SanitizeString(FromLocation);
            if(String.IsNullOrEmpty(TourName) || String.IsNullOrEmpty(Description) || String.IsNullOrEmpty(ToLocation) || String.IsNullOrEmpty(FromLocation))
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