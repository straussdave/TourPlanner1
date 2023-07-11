using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner1.Model.Messages
{
    public class TourSelectedMessage : ValueChangedMessage<Tour>
    {
        public TourSelectedMessage(Tour value) : base(value)
        {
            Selected = value;
        }

        public Tour Selected { get; set; }

    }
}
