using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using WPFArchive.ViewModel;
using WPFArchive.View;
using System.Windows.Forms;
using System.Windows.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace WPFArchive.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        List<SetOfDocs_Select_Result> _SetOfDocs;
        List<SelectOrgForMainForm_Result> _Organizations;
        private RelayCommand deleteOrgCommand;
        private RelayCommand deleteSetCommand;
        private RelayCommand filterCommandForOrg;
        private RelayCommand filterCommandForSet;
        private RelayCommand toExcelCommand;
        private SelectOrgForMainForm_Result selectedOrg;
        private SetOfDocs_Select_Result selectedSet;
        public event Action OnSuccess;
        private string searchByNameForOrg;
        private string searchByNameForSet;
        private bool munFilterOwnFormMun;
        private bool oblFilterOwnFormMun;
        private bool fedFilterOwnForm;
        private bool notGosFilterOwnForm;
        private bool catPersFilter;
        private bool catStoreFilter;
        private DateTime startFilterDate;
        private DateTime endFilterDate;
        private Excel.Application excelapp;
        private Excel.Workbook excelapp_workbook;
        private Excel.Worksheet excelworksheet;
        private Excel.Range excelcells;
        public string SearchByNameForOrg { get { return searchByNameForOrg; } set { searchByNameForOrg = value; } }
        public string SearchByNameForSet { get { return searchByNameForSet; } set { searchByNameForSet = value; } }
        public bool MunFilterOwnForm { get { return munFilterOwnFormMun; } set { munFilterOwnFormMun = value; } }//муниципальная форма собственности
        public bool OblFilterOwnForm { get { return oblFilterOwnFormMun; } set { oblFilterOwnFormMun = value; } }//областная форма собственности
        public bool FedFilterOwnForm { get { return fedFilterOwnForm; } set { fedFilterOwnForm = value; } }//федеральаная форма собственности
        public bool NotGosFilterOwnForm { get { return notGosFilterOwnForm; } set { notGosFilterOwnForm = value; } } //негосуд форма собственности
        public bool CatPersFilter { get { return catPersFilter; } set { catPersFilter = value; } } //категория "опись личного состава"
        public bool CatStoreFilter { get { return catStoreFilter; } set { catStoreFilter = value; } } //категория "опись постоянного хранения"
        public DateTime StartFilterDate { get { return startFilterDate; } set { startFilterDate = value; } }
        public DateTime EndFilterDate { get { return endFilterDate; } set { endFilterDate = value; } }

        public List<SetOfDocs_Select_Result> SetOfDocs
        {
            get { return _SetOfDocs; }
            private set
            {
                _SetOfDocs = value;
                OnPropertyChanged("SetOfDocs");
            }
        }
        public List<SelectOrgForMainForm_Result> Organizations
        {
            get { return _Organizations; }
            private set
            {
                _Organizations = value;
                OnPropertyChanged("Organizations");
            }
        }
        public List<organization> Orgs { get; private set; }
        public SelectOrgForMainForm_Result SelectedOrg
        {
            get { return selectedOrg; }
            set
            {
                selectedOrg = value;
                OnPropertyChanged("SelectedOrg");
            }
        }
        public SetOfDocs_Select_Result SelectedSet
        {
            get { return selectedSet; }
            set
            {
                selectedSet = value;
                OnPropertyChanged("SelectedSet");
            }
        }
        public List<setOfDocs> Sets { get; private set; }

        public MainViewModel()
        {
            GetEntities();
            //обновление таблицы с описями
            ArchiveProxyModel.getInstance().OnSetOfDocsUpdate += () =>
            {
                using (var db = new archiveEntities())
                {
                    SetOfDocs = db.SetOfDocs_Select().ToList();
                }
            };
            //обновление таблицы с описями
            ArchiveProxyModel.getInstance().OnDocsUpdate += () =>
            {
                using (var db = new archiveEntities())
                {
                    SetOfDocs = db.SetOfDocs_Select().ToList();
                }
            };
            //обновление таблицы с организациями
            ArchiveProxyModel.getInstance().OnOrganizationUpdate += () =>
            {
                using (var db = new archiveEntities())
                {
                    Organizations = db.SelectOrgForMainForm().ToList();
                }
            };
            //обновление таблицы с организациями
            ArchiveProxyModel.getInstance().OnDocsUpdate += () =>
            {
                using (var db = new archiveEntities())
                {
                    Organizations = db.SelectOrgForMainForm().ToList();
                }
            };
            //обновление таблицы с организациями
            ArchiveProxyModel.getInstance().OnEventsUpdate += () =>
            {
                using (var db = new archiveEntities())
                {
                    Organizations = db.SelectOrgForMainForm().ToList();
                }
            };
            startFilterDate = new DateTime(1900, 01, 01);
            endFilterDate = DateTime.Now;
            CatStoreFilter = true;
            CatPersFilter = true;
            MunFilterOwnForm = true;
            OblFilterOwnForm = true;
            FedFilterOwnForm = true;
            NotGosFilterOwnForm = true;
            Organizations.Sort((x, y) => x.nameOrg.CompareTo(y.nameOrg));
            SetOfDocs.Sort((x, y) => x.nameOrg.CompareTo(y.nameOrg));
        }

        async private void GetEntities()
        {
            using (var db = new archiveEntities())
            {
                Organizations = db.SelectOrgForMainForm().ToList();
                SetOfDocs = db.SetOfDocs_Select().ToList();
                Orgs = db.organization.ToList();
                Sets = db.setOfDocs.ToList();
            }
        }

        //удаление организации
        public RelayCommand DeleteOrgCommand
        {
            get
            {
                return deleteOrgCommand ??
                  (deleteOrgCommand = new RelayCommand(obj =>
                  {
                      if (SelectedOrg != null)
                      {
                          if (MessageBox.Show("Будут удалены все записи об этой организации (включая описи). Удалить ''" + Orgs.Find(x => x.idOrg == SelectedOrg.idOrg).nameOrg + "''?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                          {
                              if (MessageBox.Show("Вы уверены, что хотите удалить организацию?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                                  organization orgToDelete = Orgs.Find(x => x.idOrg == SelectedOrg.idOrg);
                                  ArchiveProxyModel.getInstance().deleteOrganization(orgToDelete);
                                  OnSuccess?.Invoke();
                              }
                                  
                          }
                      }
                      else MessageBox.Show("Выберите организацию из списка.");
                  }));
            }
        }
        //удаление описи
        public RelayCommand DeleteSetCommand
        {
            get
            {
                return deleteSetCommand ??
                  (deleteSetCommand = new RelayCommand(obj =>
                  {
                      if (SelectedSet != null)
                      {
                          if (MessageBox.Show("Выбранная опись будет полностью удалена (включая все документы, занесенные в нее). Удалить опись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                          {
                              if (MessageBox.Show("Вы уверены, что хотите удалить опись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                                  setOfDocs setToDelete = Sets.Find(x => x.idSet == SelectedSet.idSet);
                                  ArchiveProxyModel.getInstance().deleteSetOfDocs(setToDelete);
                                  OnSuccess?.Invoke();
                              }
                          }
                      }
                      else MessageBox.Show("Выберите опись из списка.");
                  }));
            }
        }

        //фильтры для организаций
        public RelayCommand FilterCommandForOrg
        {
            get
            {
                return filterCommandForOrg ??
                  (filterCommandForOrg = new RelayCommand(obj =>
                  {
                      CollectionViewSource.GetDefaultView(Organizations).Filter = FilterForOrg;
                  }));
            }
        }
        private bool FilterForOrg(object o)
        {
            SelectOrgForMainForm_Result org = (o as SelectOrgForMainForm_Result);
            bool munOwnForm = ((org.nameOwnForm == "муниципальная") == true && (MunFilterOwnForm == true));
            bool oblOwnForm = ((org.nameOwnForm == "областная") == true && (OblFilterOwnForm == true));
            bool fedOwnForm = ((org.nameOwnForm == "федеральная") == true && (FedFilterOwnForm == true));
            bool notGosFilterOwnForm = ((org.nameOwnForm == "негосударственная") == true && (NotGosFilterOwnForm == true));
            var result = true;
            if (org == null) { return false; }
            else if (!(munOwnForm || oblOwnForm || fedOwnForm || notGosFilterOwnForm)) { return false; }
            else if (SearchByNameForOrg != null) { if (!(org.nameOrg.Contains(SearchByNameForOrg))) return false; }
            return result;
        }


        /////Фильтрация для описей
        public RelayCommand FilterCommandForSet
        {
            get
            {
                return filterCommandForSet ??
                  (filterCommandForSet = new RelayCommand(obj =>
                  {
                      CollectionViewSource.GetDefaultView(SetOfDocs).Filter = FilterForSet;
                  }));
            }
        }
        private bool FilterForSet(object o)
        {
            SetOfDocs_Select_Result set = (o as SetOfDocs_Select_Result);
            bool catPers = ((set.nameCatOfSet == "опись дел по личному составу") == true && (CatPersFilter == true));
            bool catStore = ((set.nameCatOfSet == "опись дел постоянного хранения") == true && (CatStoreFilter == true));
            var result = true;
            if (set == null) { return false; }
            else if (!(set.startDate >= StartFilterDate && set.startDate <= EndFilterDate)) { return false; }
            else if (!(catPers || catStore)) { return false; }
            else if (SearchByNameForSet != null) { if (!(set.nameOrg.Contains(SearchByNameForSet))) return false; }
            return result;
        }
        /////выгрузка в excel
        public RelayCommand ToExcelCommand
        {
            get
            {
                return toExcelCommand ??
                  (toExcelCommand = new RelayCommand(obj =>
                  {
                      if (Organizations.Count != 0)
                      {
                          Organizations.Sort((x, y) => x.nameOrg.CompareTo(y.nameOrg));
                          excelapp = new Excel.Application();//Объявляем приложение
                          excelapp.SheetsInNewWorkbook = 1;// Количество листов в рабочей книге
                          excelapp_workbook = excelapp.Workbooks.Add(Type.Missing);//Добавить рабочую книгу
                          excelapp.DisplayAlerts = true;//Отключить отображение окон с сообщениями
                          excelworksheet = (Excel.Worksheet)excelapp.Worksheets.get_Item(1);//Получаем первый лист документа (счет начинается с 1)
                          excelworksheet.Columns[1].ColumnWidth = 5;
                          excelworksheet.Columns[2].ColumnWidth = 12;
                          excelworksheet.Columns[3].ColumnWidth = 45;
                          excelworksheet.Columns[4].ColumnWidth = 19;
                          excelworksheet.Columns[5].ColumnWidth = 15;
                          excelworksheet.Columns[6].ColumnWidth = 10;
                          excelworksheet.Columns[7].ColumnWidth = 12;
                          excelworksheet.Cells.Style.WrapText = true;
                          excelworksheet.get_Range("A1", "G1").Merge();
                          excelcells = excelworksheet.get_Range("A1", Type.Missing);
                          excelcells.set_Value(Type.Missing, "Список организаций-источников комплектования Архивного отдела администрации Ольхонского районного муниципального образования ");

                          excelcells = excelworksheet.get_Range("A3", Type.Missing);
                          excelcells.set_Value(Type.Missing, "№ пп");
                          excelcells = excelworksheet.get_Range("B3", Type.Missing);
                          excelcells.set_Value(Type.Missing, "Индекс организации");
                          excelcells = excelworksheet.get_Range("C3", Type.Missing);
                          excelcells.set_Value(Type.Missing, "Наименование организации");
                          excelcells = excelworksheet.get_Range("D3", Type.Missing);
                          excelcells.set_Value(Type.Missing, "Форма собственности (федеральная, областная, муниципальная, негосударственная)");
                          excelcells = excelworksheet.get_Range("E3", Type.Missing);
                          excelcells.set_Value(Type.Missing, "Форма приема документов");
                          excelcells = excelworksheet.get_Range("F3", Type.Missing);
                          excelcells.set_Value(Type.Missing, "Прием НТД КФФД");
                          excelcells = excelworksheet.get_Range("G3", Type.Missing);
                          excelcells.set_Value(Type.Missing, "Примечания");

                          excelcells = excelworksheet.get_Range("A4", Type.Missing);
                          excelcells.set_Value(Type.Missing, "1");
                          excelcells = excelworksheet.get_Range("B4", Type.Missing);
                          excelcells.set_Value(Type.Missing, "2");
                          excelcells = excelworksheet.get_Range("C4", Type.Missing);
                          excelcells.set_Value(Type.Missing, "3");
                          excelcells = excelworksheet.get_Range("D4", Type.Missing);
                          excelcells.set_Value(Type.Missing, "4");
                          excelcells = excelworksheet.get_Range("E4", Type.Missing);
                          excelcells.set_Value(Type.Missing, "5");
                          excelcells = excelworksheet.get_Range("F4", Type.Missing);
                          excelcells.set_Value(Type.Missing, "6");
                          excelcells = excelworksheet.get_Range("G4", Type.Missing);
                          excelcells.set_Value(Type.Missing, "7");
                          
                          int irow = 6;
                          int iorg = 1;
                          Organizations.ForEach(
                                  x =>
                                  {
                                      excelcells = excelworksheet.get_Range("A" + Convert.ToString(irow), Type.Missing);
                                      excelcells.set_Value(Type.Missing, String.Format("{0}", iorg));
                                      excelcells = excelworksheet.get_Range("B" + Convert.ToString(irow), Type.Missing);
                                      excelcells.set_Value(Type.Missing, String.Format("{0}", x.idOrg));
                                      excelcells = excelworksheet.get_Range("C" + Convert.ToString(irow), Type.Missing);
                                      excelcells.set_Value(Type.Missing, String.Format("{0}", x.nameOrg));
                                      excelcells = excelworksheet.get_Range("D" + Convert.ToString(irow), Type.Missing);
                                      excelcells.set_Value(Type.Missing, String.Format("{0}", x.nameOwnForm));
                                      excelcells = excelworksheet.get_Range("E" + Convert.ToString(irow), Type.Missing);
                                      excelcells.set_Value(Type.Missing, String.Format("{0}", x.nameRecForm));
                                      irow++;
                                      iorg++;
                                  });
                          excelcells = excelworksheet.get_Range("A3", "G" + Convert.ToString(irow-1));
                          excelcells.Borders.Color = 000000;
                          excelapp.Visible = true;//Отобразить Excel
                      }
                      else MessageBox.Show("Список источников комплектования пуст.");
                  }));
            }
        }

    }

}
