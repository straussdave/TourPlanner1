using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner1.Model.Messages
{
    public class LogCreatedMessage : ValueChangedMessage<List<Log>>
    {
        public LogCreatedMessage(List<Log> value) : base(value)
        {
            AllTours = value;
        }

        public List<Log> AllTours { get; set; }
    }
}
