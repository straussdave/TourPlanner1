using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner1.Model.Messages
{
    internal class CreateLogMessage : ValueChangedMessage<int>
    {
        public CreateLogMessage(int value) : base(value)
        {
            TourId = value;
        }

        public int TourId { get; set; }
    }
}
