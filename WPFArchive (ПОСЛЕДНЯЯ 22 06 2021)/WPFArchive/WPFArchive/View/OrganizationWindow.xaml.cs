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
    /// Логика взаимодействия для OrganizationWindow.xaml
    /// </summary>
    public partial class OrganizationWindow : Window
    {
        private string idOrg;
        public OrganizationWindow():this(null)
        {
            
        }
        public OrganizationWindow(string idOrg)
        {
            InitializeComponent();
            this.idOrg = idOrg;
            var viewModel = new OrganizationViewModel(this.idOrg == null ? 0 : int.Parse(this.idOrg));
            /*viewModel.OnSuccess += () =>
            {
                this.DialogResult = true;
            };*/
            DataContext = viewModel;
            item2.IsEnabled = false;
            item3.IsEnabled = false;
            item4.IsEnabled = false;
        }
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var col = e.Column as DataGridTextColumn;
            var style = new Style(typeof(TextBlock));
            style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
            style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));
                
            col.ElementStyle = style;

            //таблица мероприятия
            if (e.PropertyName.StartsWith("done")) { e.Column.Header = "Статус \n (проведено/\nпланируется)"; e.Column.Width = 100; e.Column.ClipboardContentBinding.StringFormat = "Yes, No";  }
            if (e.PropertyName.StartsWith("idEvent_logbook")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("typeOfEvent_idTypeOfEvent")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("nameTypeOfEvent")) { e.Column.Header = "Тип мероприятия"; e.Column.Width = 200; }
            if (e.PropertyName.StartsWith("organization_idOrg")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("nameOrg")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("dateOfEvent")) { e.Column.Header = "Дата проведения"; e.Column.ClipboardContentBinding.StringFormat = "dd/MM/yyyy"; }
            if (e.PropertyName.StartsWith("result")) { e.Column.Header = "Результат"; e.Column.Width = 200; }
            if (e.PropertyName.StartsWith("responsibleOfEvent")) { e.Column.Header = "Ответственное лицо"; e.Column.Width = 150; }
            //таблица переименования
            if (e.PropertyName.StartsWith("idNameOfOrganization")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("organization_idOrg")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("nameOrg")) e.Column.Visibility = Visibility.Hidden;
            if (e.PropertyName.StartsWith("nameRenamed")) { e.Column.Header = "Предыдущее название организации"; e.Column.Width = 650; }
            if (e.PropertyName.StartsWith("startDate")) { e.Column.Header = "Начальная дата"; e.Column.ClipboardContentBinding.StringFormat = "dd/MM/yyyy"; }
            if (e.PropertyName.StartsWith("endDate")) { e.Column.Header = "Конечная дата"; e.Column.ClipboardContentBinding.StringFormat = "dd/MM/yyyy"; }
            }

        //открытие формы "добавление мероприятия"
        private void AddNewEvent_Click(object sender, RoutedEventArgs e)
        {
            int id;
            using (var db = new archiveEntities())
            {
                List<organization> Org = db.organization.ToList();
                id = (Org.Find(x => x.nameOrg == nameOrg_textBox.Text)).idOrg;
            }
            EventWindow vm = new EventWindow(id);
            vm.Title = "Добавление нового мероприятия";
            vm.ShowDialog();
        }
        //открытие формы "добавление переименования"
        private void AddNewRename_Click(object sender, RoutedEventArgs e)
        {
            int id;
            using (var db = new archiveEntities())
            {
                List<organization> Org = db.organization.ToList();
                id = (Org.Find(x => x.nameOrg == nameOrg_textBox.Text)).idOrg;
            }
            RenameWindow vm = new RenameWindow(id);
            vm.Title = "Добавление записи о предыдущем названии организации";
            vm.ShowDialog();
        }
        //метод, который запускается, если форма активна
        private void Window_Activated_1(object sender, EventArgs e)
        {
            if (nameOrg_textBox.Text != "")
            {
                StorageExistenceCheckBox_Changed(sender,  e);
                using (var db = new archiveEntities())
                {
                    List<organization> Org = db.organization.ToList();
                    List<organizationArchive> OrgArchive = db.organizationArchive.ToList();
                    if (Org.Exists(x => x.nameOrg == nameOrg_textBox.Text))
                    {
                        int id = (Org.Find(x => x.nameOrg == nameOrg_textBox.Text)).idOrg;
                        eventsGrid.ItemsSource = db.SelectEventLogbookForOrg(id);
                        renamesGrid.ItemsSource = db.SelectRenameForOrg(id);
                        new OrganizationViewModel(id);
                        item2.IsEnabled = true;
                        item3.IsEnabled = true;
                        item4.IsEnabled = true;
                        if (OrgArchive.Exists(x => x.organization_idOrg == id))
                        {
                            if (OrgArchive.Find(x => x.organization_idOrg == id).pathAboutArchive != null)
                            {
                                aboutArchDelete_Button.Visibility = Visibility.Visible;
                                aboutArchOpen_Button.Visibility = Visibility.Visible;
                                aboutArchAdd_textBlock.Text = "Заменить файл";
                            }
                            else
                            {
                                aboutArchDelete_Button.Visibility = Visibility.Hidden;
                                aboutArchOpen_Button.Visibility = Visibility.Hidden;
                                aboutArchAdd_textBlock.Text = "Прикрепить файл";
                            }
                            if (OrgArchive.Find(x => x.organization_idOrg == id).pathExpertCommission != null)
                            {
                                expertCommDelete_Button.Visibility = Visibility.Visible;
                                expertCommOpen_Button.Visibility = Visibility.Visible;
                                expertCommAdd_textBlock.Text = "Заменить файл";
                            }
                            else
                            {
                                expertCommDelete_Button.Visibility = Visibility.Hidden;
                                expertCommOpen_Button.Visibility = Visibility.Hidden;
                                expertCommAdd_textBlock.Text = "Прикрепить файл";
                            }
                            if (OrgArchive.Find(x => x.organization_idOrg == id).pathInstruction != null)
                            {
                                instructionDelete_Button.Visibility = Visibility.Visible;
                                instructionOpen_Button.Visibility = Visibility.Visible;
                                instructionAdd_textBlock.Text = "Заменить файл";
                            }
                            else
                            {
                                instructionDelete_Button.Visibility = Visibility.Hidden;
                                instructionOpen_Button.Visibility = Visibility.Hidden;
                                instructionAdd_textBlock.Text = "Прикрепить файл";
                            }
                        }
                        else
                        {
                            aboutArchDelete_Button.Visibility = Visibility.Hidden;
                            aboutArchOpen_Button.Visibility = Visibility.Hidden;
                            expertCommDelete_Button.Visibility = Visibility.Hidden;
                            expertCommOpen_Button.Visibility = Visibility.Hidden;
                            instructionDelete_Button.Visibility = Visibility.Hidden;
                            instructionOpen_Button.Visibility = Visibility.Hidden;
                        }
                    }
                }
            } 
        }
        //нажатие на галочку "наличие архива"
        private void StorageExistenceCheckBox_Changed(object sender, EventArgs e)
        {
            if (storageExistenceCheckBox.IsChecked == false) storageExistsPanel.IsEnabled = false;
            if (storageExistenceCheckBox.IsChecked == true) storageExistsPanel.IsEnabled = true;
        }
        //открытие формы "редактирование мероприятия"
        private void EventRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectEventLogbookForOrg_Result classObj = eventsGrid.SelectedItem as SelectEventLogbookForOrg_Result;
            EventWindow vm = new EventWindow(classObj.organization_idOrg, classObj.idEvent_logbook);
            vm.Title = "Редактирование мероприятия";
            vm.ShowDialog();
        }
        //открытие формы "редактирование переименования"
        private void RenameRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectRenameForOrg_Result classObj = renamesGrid.SelectedItem as SelectRenameForOrg_Result;
            RenameWindow vm = new RenameWindow(classObj.organization_idOrg, classObj.idNameOfOrganization);
            vm.Title = "Редактирование предыдущего названия организации";
            vm.ShowDialog();
        }
    }
}
