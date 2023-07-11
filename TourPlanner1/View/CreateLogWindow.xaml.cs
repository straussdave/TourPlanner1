using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TourPlanner1.ViewModel;

namespace TourPlanner1.View
{
    /// <summary>
    /// Interaction logic for CreateLogWindow.xaml
    /// </summary>
    public partial class CreateLogWindow : Window
    {
        public CreateLogWindow()
        {
            InitializeComponent();
            this.DataContext = new CreateLogViewModel();
        }
    }
}
