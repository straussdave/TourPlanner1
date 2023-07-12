using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;
using System.Linq;
using TourPlanner1.Model.Messages;
using TourPlanner1.Model;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using TourPlanner1.View;
using Org.BouncyCastle.Utilities;

namespace TourPlanner1.ViewModel
{
    public partial class TourLogsViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private ObservableCollection<Log> tourLogList = new();

        [ObservableProperty]
        private Tour selectedTour;

        [ObservableProperty]
        private Log selectedLog;

        public TourLogsViewModel()
        {
            Messenger.Register<TourLogsViewModel, TourSelectedMessage>(this, (r, m) => {
                if(m.Selected != null)
                {
                    TourLogList.Clear();
                    SelectedTour = m.Selected;
                    IConfig config = new Model.Config();
                    DatabaseHandler databaseHandler = new DatabaseHandler(new TourPlannerDbContext(), config);
                    List<Log> logs = databaseHandler.ReadLogs();
                    foreach (Log log in logs)
                    {
                        if (log.TourId == SelectedTour.Id)
                        {
                            TourLogList.Add(log);
                        }
                    }
                    SelectedLog = TourLogList.FirstOrDefault(defaultValue: null);
                }
            });
            RegisterDeleteLogMessage();
            RegisterLogCreatedMessage();
        }

        [RelayCommand]
        public void OpenCreateLogWindow()
        {
            CreateLogWindow createLogWindow = new CreateLogWindow();
            Messenger.Send(new CreateLogMessage(SelectedTour.Id));
            createLogWindow.ShowDialog();
        }

        [RelayCommand]
        public void RemoveLog()
        {
            IConfig config = new Model.Config();
            DatabaseHandler dbHandler = new(new TourPlannerDbContext(), config);
            dbHandler.DeleteLog(SelectedLog.Id);
            Messenger.Send(new DeleteLogMessage(dbHandler.ReadLogs(), SelectedLog));
        }

        [RelayCommand]
        void OpenUpdateLogWindow()
        {
            UpdateLogWindow updatelogWindow = new();
            Messenger.Send(new UpdateLogMessage(SelectedLog, SelectedTour.Id));
            updatelogWindow.ShowDialog();
        }

        private void RegisterDeleteLogMessage()
        {
            Messenger.Register<TourLogsViewModel, DeleteLogMessage>(this, (r, m) =>
            {
                TourLogList.Clear();
                foreach (Log log in m.AllLogs)
                {
                    if (log.TourId == SelectedTour.Id)
                    {
                        TourLogList.Add(log);
                    }
                }
                SelectedLog = m.Selected;
            });
        }

        private void RegisterLogCreatedMessage()
        {
            Messenger.Register<TourLogsViewModel, LogCreatedMessage>(this, (r, m) =>
            {
                TourLogList.Clear();
                foreach (Log log in m.AllTours)
                {
                    if (log.TourId == SelectedTour.Id)
                    {
                        TourLogList.Add(log);
                    }
                }
            });
        }
    }
}
