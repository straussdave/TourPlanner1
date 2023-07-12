using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner1.Model.Messages
{
    public class UpdateLogMessage : ValueChangedMessage<Log>
    {
        public UpdateLogMessage(Log value, int tourId) : base(value)
        {
            Selected = value;
            this.tourId = tourId;
        }

        public Log Selected { get; set; }
        public int tourId { get; set; }

    }
}
