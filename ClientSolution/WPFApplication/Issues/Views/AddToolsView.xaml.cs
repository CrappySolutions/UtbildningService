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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFApplication.Issues.Views
{
    /// <summary>
    /// Interaction logic for AddToolsView.xaml
    /// </summary>
    public partial class AddToolsView : UserControl
    {
        public AddToolsView(ViewModels.IAddViewModel addViewModel)
        {
            InitializeComponent();
            DataContext = addViewModel;
        }
    }
}
