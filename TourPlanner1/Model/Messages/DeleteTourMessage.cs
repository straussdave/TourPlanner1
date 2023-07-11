using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner1.Model.Messages
{
    public class DeleteTourMessage : ValueChangedMessage<List<Tour>>
    {
        public DeleteTourMessage(List<Tour> value, Tour selected) : base(value)
        {
            AllTours = value;
            Selected = selected;
        }

        public List<Tour> AllTours { get; set; }
        public Tour Selected { get; set; }

    }
}
