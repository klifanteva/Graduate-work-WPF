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
using WPFArchive.ViewModel;

namespace WPFArchive.View
{
    /// <summary>
    /// Логика взаимодействия для EventWindow.xaml
    /// </summary>
    public partial class EventWindow : Window
    {
        public EventWindow(int idOrg)
        {
            InitializeComponent();
            var viewModel = new EventViewModel(idOrg);
            viewModel.OnSuccess += () =>
            {
                this.DialogResult = true;
            };
            DataContext = viewModel;
            nameOrg_textBox.IsEnabled = false;
        }
        public EventWindow(int idOrg, int idEvent)
        {
            InitializeComponent();
            var viewModel = new EventViewModel(idOrg, idEvent);
            viewModel.OnSuccess += () =>
            {
                this.DialogResult = true;
            };
            DataContext = viewModel;
            nameOrg_textBox.IsEnabled = false;
        }
    }
}
