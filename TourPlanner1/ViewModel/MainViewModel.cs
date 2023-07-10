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

namespace TourPlanner1.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {

        }

        Logger logger = new();
        ReportGenerator reportGenerator = new();

        int counter;
        [ObservableProperty] //this auto generates code in Dependencies/Analyzers/CommunityToolkin.Mvvm.SourceGenerators/CoomunityToolkit.MvvmSourceGenerators.ObservablePropertyGenerator
        string text = "0";

        DatabaseHandler handler = new();

        [RelayCommand] //example command
        void Clicked()
        {
            counter++;
            Text = counter.ToString();
        }

        [RelayCommand]
        void OpenCreateTourWindow()
        {
            CreateTourWindow createTourWindow = new();
            createTourWindow.Show();
        }
    }
}
