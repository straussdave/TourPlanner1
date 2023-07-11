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

namespace TourPlanner1.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {

        }

        [RelayCommand]
        void CloseApplication()
        {
            Application.Current.MainWindow.Close();
        }
    }
}
