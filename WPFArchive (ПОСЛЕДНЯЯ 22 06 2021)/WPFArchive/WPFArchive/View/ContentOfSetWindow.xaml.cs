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
    /// Логика взаимодействия для ContentOfSetWindow.xaml
    /// </summary>
    public partial class ContentOfSetWindow : Window
    {
        private int idSet;
        public ContentOfSetWindow(int idSet)
        {
            this.idSet = idSet;
            InitializeComponent();
            var viewModel = new ContentOfSetViewModel(idSet);
            DataContext = viewModel;
        }
        //открытие формы для добавления документа
        private void addDocToSet_Click(object sender, RoutedEventArgs e)
        {
            DocsWindow docsWindow = new DocsWindow(idSet);
            docsWindow.ShowDialog();
        }

        //переименовывание автоматически загружаемых заголовков столбцов
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var col = e.Column as DataGridTextColumn;
            var style = new Style(typeof(TextBlock));
            style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
            style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));
            col.ElementStyle = style;

            if (e.PropertyName.StartsWith("idDoc")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("idNom")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("filePath")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("setOfDocs_idSet")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("numberDoc")) { e.Column.Header = "Номер\nдокумента"; e.Column.Width = 70; }
            if (e.PropertyName.StartsWith("dateDoc")) { e.Column.Header = "Дата\nформирования"; e.Column.ClipboardContentBinding.StringFormat = "dd/MM/yyyy"; }
            if (e.PropertyName.StartsWith("nameDocType")) { e.Column.Header = "Тип документа"; e.Column.Width = 200; }
            if (e.PropertyName.StartsWith("actIndex")) e.Column.Header = "Индекс дела";
            if (e.PropertyName.StartsWith("actHeading")) { e.Column.Header = "Заголовок дела"; e.Column.Width = 480; }
            if (e.PropertyName.StartsWith("sheetsCount")) e.Column.Header = "Число листов";
            }
        //открытие формы "Редактирование документа"
        private void EditDocument_Button_Click(object sender, RoutedEventArgs e)
        {
            List<document> Docs;
            DocumentsOfSet_Select_Result classObj = docsGrid.SelectedItem as DocumentsOfSet_Select_Result;
            if (classObj != null) {
                using (var db = new archiveEntities())
                {
                    Docs = db.document.ToList();
                }
                DocsWindow vm = new DocsWindow(classObj.idDoc, Docs.Find(x=> x.idDoc == classObj.idDoc).setOfDocs_idSet);
                vm.Title = "Редактирование документа";
                vm.ShowDialog();
            }
            else { MessageBox.Show("Выберите документ из списка"); }
            
        }
        //////открытие формы "Редактирование описи"
        private void EditSet_Button_Click(object sender, RoutedEventArgs e)
        {
            SetOfDocsWindow vm = new SetOfDocsWindow(idSet);
            vm.Title = "Редактирование описи";
            vm.ShowDialog();
        }
    }
}
