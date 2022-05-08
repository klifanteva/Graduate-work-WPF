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
    /// Логика взаимодействия для NomenclatureWindow.xaml
    /// </summary>
    public partial class NomenclatureWindow : Window
    {
        public NomenclatureWindow()
        {
            InitializeComponent();
            var viewModel = new NomenclatureViewModel();
            DataContext = viewModel;
        }
        //переименовывание автоматически загружаемых заголовков столбцов
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var col = e.Column as DataGridTextColumn;
            var style = new Style(typeof(TextBlock));
            style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
            style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));
            col.ElementStyle = style;

            //таблица номенклатура
            if (e.PropertyName.StartsWith("idNom")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("organization_idOrg")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("actIndex")) { e.Column.Header = "Индекс дела"; e.Column.Width = 100; }
            if (e.PropertyName.StartsWith("actHeading")) { e.Column.Header = "Заголовок дела"; e.Column.Width = 700; }
            if (e.PropertyName.StartsWith("yearNom")) {e.Column.Header = "Год"; e.Column.Width = 50; }
            if (e.PropertyName.StartsWith("nameOrg")) { e.Column.Header = "Источник комплектования"; e.Column.Width = 450; }
        }
        //кнопка убрать выделение
        private void removeSelection_Button_Click(object sender, RoutedEventArgs e)
        {
            actIndex_textBox.Text = null;
            actHeading_textBox.Text = null;
            yearNom_textBox.Text = null;
            org_comboBox.SelectedItem = null;
            nom_dataGrid.SelectedItem = null;
            saveButtonContent.Text = "Сохранить";
        }

        private void nom_dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (nom_dataGrid.SelectedItem != null) 
            {
                Nomenclature_Select_Result classObj = nom_dataGrid.SelectedItem as Nomenclature_Select_Result;
            }
        }

        private void toExcel_Click(object sender, RoutedEventArgs e)
        {
            NomToExcelWindow vm = new NomToExcelWindow();
            vm.ShowDialog();
            //vm.DataContext = new NomToExcelViewModel();
        }
    }
}
