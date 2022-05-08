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
    /// Логика взаимодействия для NomToExcelWindow.xaml
    /// </summary>
    public partial class NomToExcelWindow : Window
    {
        //public List<nomenclature> Nomenclatures { get; private set; }
        public NomToExcelWindow()
        {
            InitializeComponent();
            var viewModel = new NomToExcelViewModel();
            viewModel.OnSuccess += () =>
            {
                this.DialogResult = true;
            };
            DataContext = viewModel;
        }
    }
}
