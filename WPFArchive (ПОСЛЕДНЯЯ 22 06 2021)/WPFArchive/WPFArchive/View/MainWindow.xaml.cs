using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WPFArchive.View;
using WPFArchive.ViewModel;

namespace WPFArchive
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ICollectionView orgs;

        public MainWindow()
        {

                InitializeComponent();
            //OpenConnection();
                var viewModel = new MainViewModel();
                DataContext = viewModel;
                setOfDocsPanel.Visibility = Visibility.Hidden;
        
        }

        
        //открытие панели с описями
        private void setOfDocs_Button_Click(object sender, RoutedEventArgs e)
        {
            setOfDocsPanel.Visibility = Visibility.Visible;
            orgPanel.Visibility = Visibility.Hidden;
        }
        //открытие панели с организациями
        private void org_Button_Click(object sender, RoutedEventArgs e)
        {
            orgPanel.Visibility = Visibility.Visible;
            setOfDocsPanel.Visibility = Visibility.Hidden;
        }
        //открытие формы номенклатур
        private void nomenclature_Button_Click(object sender, RoutedEventArgs e)
        {
            NomenclatureWindow vm = new NomenclatureWindow();
            //vm.Title = "Добавление новой организации";
            vm.ShowDialog();
        }

        //переименовывание автоматически загружаемых заголовков столбцов
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var col = e.Column as DataGridTextColumn;
            var style = new Style(typeof(TextBlock));
            style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
            style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));
            
            col.ElementStyle = style;

            //таблица организации
            if (e.PropertyName.StartsWith("idOrg")) { e.Column.Header = "Код"; style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center)); }
                if (e.PropertyName.StartsWith("nameOrg") && sender == orgDataGrid) {  e.Column.Header = "Источник комплектования"; e.Column.Width = 450; }
            if (e.PropertyName.StartsWith("nameOwnForm")) { e.Column.Header = "Форма\n собственности"; e.Column.Width = 110; style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center)); }
            if (e.PropertyName.StartsWith("nameRecForm")) { e.Column.Header = "Форма приема\n документов"; e.Column.Width = 100; style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center)); }
            if (e.PropertyName.StartsWith("minDateUD")) { e.Column.Header = "Начальная\n дата (уд)"; e.Column.Width = 70; e.Column.ClipboardContentBinding.StringFormat = "dd/MM/yyyy"; style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center)); }
            if (e.PropertyName.StartsWith("minDateLS")) { e.Column.Header = "Начальная\n дата (лс)"; e.Column.Width = 70; e.Column.ClipboardContentBinding.StringFormat = "dd/MM/yyyy"; style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center)); }
            if (e.PropertyName.StartsWith("maxDateUD")) { e.Column.Header = "Конечная\n дата (уд)"; e.Column.Width = 70; e.Column.ClipboardContentBinding.StringFormat = "dd/MM/yyyy"; style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center)); }
            if (e.PropertyName.StartsWith("maxDateLS")) { e.Column.Header = "Конечная\n дата (лс)"; e.Column.Width = 70; e.Column.ClipboardContentBinding.StringFormat = "dd/MM/yyyy"; style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center)); }
            if (e.PropertyName.StartsWith("lastControl")) { e.Column.Header = "Последняя\n проверка"; style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center)); }
                if (e.PropertyName.StartsWith("isSource")) { e.Column.Header = "Статус \nорганизации"; e.Column.Width = 80;  }
            //таблица описи
            if (e.PropertyName.StartsWith("idSet")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("categoryOfSet_idCatOfSet")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("numberSet")) { e.Column.Header = "Номер\n описи"; style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center)); }
                if (e.PropertyName.StartsWith("nameOfSet")) { e.Column.Header = "Наименование описи"; e.Column.Width = 250; }
            if (e.PropertyName.StartsWith("nameOrg") && sender == setOfDocsDataGrid) { e.Column.Header = "Источник комплектования"; e.Column.Width = 250; }
            if (e.PropertyName.StartsWith("startDate")) { e.Column.Header = "Начальная\n дата";  e.Column.Width = 80; e.Column.ClipboardContentBinding.StringFormat = "dd/MM/yyyy"; style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center)); }
            if (e.PropertyName.StartsWith("endDate")) { e.Column.Header = "Конечная\n дата"; e.Column.Width = 80; e.Column.ClipboardContentBinding.StringFormat = "dd/MM/yyyy"; style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center)); }
            if (e.PropertyName.StartsWith("nameAccess")) e.Column.Header = "Доступ";
            if (e.PropertyName.StartsWith("reasonOfClosedAccess")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("docCount")) { e.Column.Header = "Число дел"; style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center)); }
            if (e.PropertyName.StartsWith("sheetsCount")) { e.Column.Header = "Число листов"; style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center)); }
                if (e.PropertyName.StartsWith("nameCatOfSet")) e.Column.Header = "Категория";
        }

        //открытие формы "добавление организации"
        private void addOrgButton_Click(object sender, RoutedEventArgs e)
        {
            OrganizationWindow vm = new OrganizationWindow();
            vm.Title = "Добавление новой организации";
            vm.ShowDialog();
        }
        //открытие формы "добавление описи"
        private void addSetButton_Click(object sender, RoutedEventArgs e)
        {
            SetOfDocsWindow vm = new SetOfDocsWindow();
            vm.ShowDialog();
        }
        //открытие формы "состав описи"
        private void SetRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            SetOfDocs_Select_Result classObj = setOfDocsDataGrid.SelectedItem as SetOfDocs_Select_Result;
            ContentOfSetWindow content = new ContentOfSetWindow(classObj.idSet);/////////
            content.Show();
        }
        //открытие формы "редактирование организации"
        private void OrgRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectOrgForMainForm_Result classObj = orgDataGrid.SelectedItem as SelectOrgForMainForm_Result;
            string idOrg = classObj.idOrg.ToString();
            OrganizationWindow orgWindow = new OrganizationWindow(idOrg);//////////
            orgWindow.ShowDialog();
        }
    }
}
