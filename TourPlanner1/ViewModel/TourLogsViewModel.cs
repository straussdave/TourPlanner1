﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner1.Model.Messages;
using TourPlanner1.Model;
using System.Diagnostics;

namespace TourPlanner1.ViewModel
{
    public partial class TourLogsViewModel : ObservableRecipient
    {
        [ObservableProperty]
        private List<Log> tourLogList;

        [ObservableProperty]
        private Tour selectedTour;

        [ObservableProperty]
        private Log selectedLog;

        public TourLogsViewModel()
        {
            Messenger.Register<TourLogsViewModel, TourSelectedMessage>(this, (r, m) => {
                SelectedTour = m.Selected;
                IConfig config = new Config();
                DatabaseHandler databaseHandler = new DatabaseHandler(new TourPlannerDbContext(), config);
                List<Log> logs = databaseHandler.ReadLogs();
                //foreach (Log log in logs)
                //{
                //    if (log.TourId == SelectedTour.Id)
                //    {
                //        TourLogList.Add(log);
                //    }
                //}
                //SelectedLog = TourLogList.FirstOrDefault(defaultValue: null);
            });
        }
    }
}
