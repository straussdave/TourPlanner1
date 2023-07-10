using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner1.ViewModel
{
    public partial class ErrorViewModel : ObservableObject
    {
        public ErrorViewModel(string message)
        {
            errorMessage = message;
        }

        [ObservableProperty]
        string errorMessage;
    }
}
