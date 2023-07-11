using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner1.Model;
using TourPlanner1.Model.Messages;
using TourPlanner1.View;

namespace TourPlanner1.ViewModel
{
    public partial class TourListViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private List<Tour> tourList;

        [ObservableProperty]
        private Tour selectedTour;

        public TourListViewModel()
        {
            IConfig config = new Config();
            DatabaseHandler databaseHandler = new DatabaseHandler(new TourPlannerDbContext(), config);
            TourList = databaseHandler.ReadTours();
            SelectedTour = TourList.FirstOrDefault(defaultValue: null);
        }

        partial void OnSelectedTourChanged(Tour oldValue, Tour newValue)
        {
            Messenger.Send(new TourSelectedMessage(newValue));
        }

        [RelayCommand]
        void OpenCreateTourWindow()
        {
            CreateTourWindow createTourWindow = new();
            createTourWindow.ShowDialog();
        }
    }
}
