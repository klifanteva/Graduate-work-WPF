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
    /// Логика взаимодействия для RenameWindow.xaml
    /// </summary>
    public partial class RenameWindow : Window
    {
        public RenameWindow(int idOrg)
        {
            InitializeComponent();
            var viewModel = new RenameViewModel(idOrg);
            viewModel.OnSuccess += () =>
            {
                this.DialogResult = true;
            };
            DataContext = viewModel;
        }

        public RenameWindow(int idOrg, int idRename)
        {
            InitializeComponent();
            var viewModel = new RenameViewModel(idOrg, idRename);
            viewModel.OnSuccess += () =>
            {
                this.DialogResult = true;
            };
            DataContext = viewModel;
        }
    }
}
