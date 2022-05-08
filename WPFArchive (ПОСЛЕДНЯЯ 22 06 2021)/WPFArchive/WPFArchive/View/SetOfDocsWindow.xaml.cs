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
    /// Логика взаимодействия для SetOfDocsWindow.xaml
    /// </summary>
    public partial class SetOfDocsWindow : Window
    {
        private string idOrg;
        public SetOfDocsWindow()
        {
            InitializeComponent();
            var viewModel = new SetOfDocsViewModel();
            viewModel.OnSuccess += () =>
            {
                this.DialogResult = true;
            };
            DataContext = viewModel;
        }
        public SetOfDocsWindow(int idSet)
        {
            InitializeComponent();
            var viewModel = new SetOfDocsViewModel(idSet);
            viewModel.OnSuccess += () =>
            {
                this.DialogResult = true;
            };
            DataContext = viewModel;
        }

    }
}
