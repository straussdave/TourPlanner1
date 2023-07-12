using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using log4net.Appender;
using TourPlanner1.Model;
using TourPlanner1.Utility;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using TourPlanner1.View;
using System.Windows;
using System.Collections.Generic;
using TourPlanner1.Model.Messages;
using System.Linq;
using CommunityToolkit.Mvvm.Messaging;

namespace TourPlanner1.ViewModel
{
    public partial class MainViewModel : ObservableRecipient
    {
        IConfig config = new Model.Config();
        ReportGenerator reportGenerator = new();

        [ObservableProperty]
        string routeImage;

        [ObservableProperty]
        private Tour selectedTour;

        [ObservableProperty]
        private List<Tour> tourList;

        public MainViewModel()
        {
            DatabaseHandler databaseHandler = new DatabaseHandler(new TourPlannerDbContext(), config);
            TourList = databaseHandler.ReadTours();
            SelectedTour = TourList.FirstOrDefault(defaultValue: null);
            Messenger.Register<MainViewModel, TourSelectedMessage>(this, (r, m) => {
                SelectedTour = m.Selected;
                RouteImage = PathHelper.GetBasePath() + "\\Images\\" + SelectedTour.RouteImage;
            });
        }

        [RelayCommand]
        void CloseApplication()
        {
            Application.Current.MainWindow.Close();
        }

        void OpenErrorWindow(string message)
        {
            ErrorWindow errorWindow = new(message);
            errorWindow.Show();
        }

        [RelayCommand]
        void GenerateReport()
        {
            reportGenerator.GenerateReport(SelectedTour);
        }

        [RelayCommand]
        void GenerateSummaryReport()
        {
            reportGenerator.GenerateSummaryReport();
        }
    }
}
