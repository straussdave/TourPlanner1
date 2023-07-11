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
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace TourPlanner1.ViewModel
{
    public partial class TourListViewModel : ObservableRecipient
    {
        IConfig config = new Model.Config();

        [ObservableProperty]
        private List<Tour> tourList;

        [ObservableProperty]
        private Tour selectedTour;

        public TourListViewModel()
        {
            DatabaseHandler databaseHandler = new DatabaseHandler(new TourPlannerDbContext(), config);
            TourList = databaseHandler.ReadTours();
            SelectedTour = TourList.FirstOrDefault(defaultValue: null);
            Messenger.Register<TourListViewModel, TourCreatedMessage>(this, (r, m) =>
            {
                TourList = m.AllTours;
            });
            Messenger.Register<TourListViewModel, DeleteTourMessage>(this, (r, m) =>
            {
                TourList = m.AllTours;
                SelectedTour = m.Selected;
            });
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

        [RelayCommand]
        void RemoveTour()
        {
            DatabaseHandler dbHandler = new(new TourPlannerDbContext(), config);
            dbHandler.DeleteTour(SelectedTour.Id);
            Messenger.Send(new DeleteTourMessage(dbHandler.ReadTours(), SelectedTour));
        }

        [RelayCommand]
        void OpenUpdateTourWindow()
        {
            UpdateTourWindow updateTourWindow = new();
            Messenger.Send(new UpdateTourMessage(SelectedTour));
            updateTourWindow.ShowDialog();
        }
    }
}
