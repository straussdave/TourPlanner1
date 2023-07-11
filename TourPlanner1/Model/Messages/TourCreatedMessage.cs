using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner1.Model.Messages
{
    public class TourCreatedMessage : ValueChangedMessage<List<Tour>>
    {
        public TourCreatedMessage(List<Tour> value) : base(value)
        {
            AllTours = value;
        }

        public List<Tour> AllTours { get; set; }

    }
}