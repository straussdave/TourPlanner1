using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using log4net.Appender;
using TourPlanner1.Model;
using TourPlanner1.Utility;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace TourPlanner1.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        Logger logger = new();
        ReportGenerator reportGenerator = new();
        DatabaseHandler dbHandler = new();

        int counter;
        [ObservableProperty] //this auto generates code in Dependencies/Analyzers/CommunityToolkin.Mvvm.SourceGenerators/CoomunityToolkit.MvvmSourceGenerators.ObservablePropertyGenerator
        string text;

        [RelayCommand] //example command
        void Clicked()
        {
            counter++;
            Text = counter.ToString();
        }

        public MainViewModel()
        {
            logger.log.Debug("MainViewModel created");
        }
    }
}
