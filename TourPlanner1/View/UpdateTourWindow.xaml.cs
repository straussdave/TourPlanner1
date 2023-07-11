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
    /// Interaction logic for UpdateTourWindow.xaml
    /// </summary>
    public partial class UpdateTourWindow : Window
    {
        public UpdateTourWindow()
        {
            InitializeComponent();
            this.DataContext = new UpdateTourViewModel();
        }
    }
}
