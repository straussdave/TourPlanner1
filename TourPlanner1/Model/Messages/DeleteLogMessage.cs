using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner1.Model.Messages
{
    internal class DeleteLogMessage : ValueChangedMessage<List<Log>>
    {
        public DeleteLogMessage(List<Log> value, Log selected) : base(value)
        {
            AllLogs = value;
            Selected = selected;
        }

        public List<Log> AllLogs { get; set; }
        public Log Selected { get; set; }

    }
}
