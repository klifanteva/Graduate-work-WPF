using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;

namespace WPFArchive.ViewModel
{
    class NomenclatureViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public event Action OnSuccess;
        List<Nomenclature_Select_Result> _Nomenclatures;
        List<nomenclature> nomenclatures;
        public int idOrg;
        public List<organization> org;
        public string actIndex;
        public string actHeading;
        public int yearNom;
        private RelayCommand saveCommand;
        private RelayCommand deleteNomCommand;
        private RelayCommand searchCommand;
        private Nomenclature_Select_Result selectedNom;
        public CollectionView ItemView { get; set; }
        public string SearchByHeading 
        { 
            get 
            { return searchByHeading; } 
            set 
            { 
                searchByHeading = value;
                NotifyPropertyChanged("SearchByHeading");
            } 
        }
        private string searchByHeading;
        public string SearchByOrg { get { return searchByOrg; } 
            set 
            { 
                searchByOrg = value;
                NotifyPropertyChanged("SearchByOrg");
            } 
        }
        private string searchByOrg;
        public int SearchByYear
        {
            get
            { return searchByYear; }
            set
            {
                searchByYear = value;
                NotifyPropertyChanged("SearchByYear");
            }
        }
        private int searchByYear;
        public List<Nomenclature_Select_Result> NomenclaturesForDataGrid
        {
            get { return _Nomenclatures; }
            private set
            {
                _Nomenclatures = value;
                OnPropertyChanged("NomenclaturesForDataGrid");
            }
        }
        public List<nomenclature> Nomenclatures
        {
            get { return nomenclatures; }
            private set
            {
                nomenclatures = value;
                OnPropertyChanged("Nomenclatures");
            }
        }
        public Nomenclature_Select_Result SelectedNom
        {
            get { return selectedNom; }
            set
            {
                selectedNom = value;
                OnPropertyChanged("SelectedNom");
            }
        }
        public int IdOrg { get { return idOrg; } set { idOrg = value; } }
        public List<organization> Organizations { get { return org; } private set { org = value; } }
        public string ActIndex { get { return actIndex; } set { actIndex = value;  } }
        public string ActHeading {  get { return actHeading; } set { actHeading = value;  } }
        public int YearNom { get { return yearNom; } set { yearNom = value;  } }

        public NomenclatureViewModel()
        {
            GetEntities();
            ItemView = (CollectionView)CollectionViewSource.GetDefaultView(NomenclaturesForDataGrid);
            //обновление таблицы с документами
            ArchiveProxyModel.getInstance().OnNomenclatureUpdate += () =>
            {
                GetEntities();
            };
        }

        async private void GetEntities()
        {
            using (var db = new archiveEntities())
            {
                NomenclaturesForDataGrid = db.Nomenclature_Select().ToList();
                Organizations = db.organization.ToList();
                Nomenclatures = db.nomenclature.ToList();
            }
        }

        //кнопка сохранения
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand((o) =>
                    {
                        using (var db = new archiveEntities())
                        {
                                ////добавление
                                nomenclature newNom = new nomenclature
                                {
                                    organization_idOrg = IdOrg,
                                    actIndex = ActIndex,
                                    actHeading = ActHeading,
                                    yearNom = YearNom
                                };
                                if (newNom.actIndex == null || newNom.actHeading == null ||
                                    newNom.yearNom == 0)
                                {
                                    MessageBox.Show("Необходимо заполнить все поля");
                                }
                                else if (Nomenclatures.Exists(x => x.actIndex == ActIndex && x.yearNom == YearNom && x.organization_idOrg == IdOrg && x.actHeading == ActHeading) == true)
                                {
                                    MessageBox.Show("Такая номенклатура уже добавлена");
                                }
                                else if (Nomenclatures.Exists(x => x.actIndex == ActIndex && x.yearNom == YearNom && x.organization_idOrg == IdOrg) == true)
                                {
                                    MessageBox.Show("Номенклатура с индексом " + ActIndex + " для выбранной организации на " + YearNom + " год уже добавлена");
                                }
                                else if (newNom.yearNom <= 1500 && newNom.yearNom >= DateTime.Now.Year + 5)
                                {
                                    MessageBox.Show("Год должен быть указан не ранее 1500 и не позднее текущего + 5 лет.");
                                }
                                else
                                {
                                if (MessageBox.Show("Добавленную номенклатуру нельзя будет изменить. Добавить номенклатуру?", "Добавление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    ArchiveProxyModel.getInstance().addNomenclature(newNom);
                                    OnSuccess?.Invoke();
                                    MessageBox.Show("Номенклатура успешно добавлена");
                                }
                                }
                            }
                    }));
            }
        }
        //удаление 
        public RelayCommand DeleteNomCommand
        {
            get
            {
                return deleteNomCommand ??
                  (deleteNomCommand = new RelayCommand(obj =>
                  {
                      if (SelectedNom != null)
                      {
                          if (MessageBox.Show("Данная номенклатура может использоваться в записях о документах. Если ее удалить, то все документы, привязанные к этой номенклатуре, полностью удалятся. Удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                          {
                              if (MessageBox.Show("Вы действительно хотите удалить выбранную номенклатуру?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                              {
                                  nomenclature nomToDelete = Nomenclatures.Find(x => x.idNom == SelectedNom.idNom);
                                  ArchiveProxyModel.getInstance().deleteNomenclature(nomToDelete);
                                  OnSuccess?.Invoke();
                                  MessageBox.Show("Номенклатура успешно удалена.");
                              }
                          }
                      }
                      else MessageBox.Show("Выберите номенклатуру из списка.");
                  }));
            }
        }

        //поиск
        public RelayCommand SearchCommand
        {
            get
            {
                return searchCommand ??
                  (searchCommand = new RelayCommand(obj =>
                  {
                      CollectionViewSource.GetDefaultView(NomenclaturesForDataGrid).Filter = SearchFilter;
                  }));
            }
        }
        private bool SearchFilter(object o)
        {
            Nomenclature_Select_Result nom = (o as Nomenclature_Select_Result);
            var result = true;

            if (nom == null)
            {
                return false;
            }
            else if (SearchByHeading != null)
            {
                if (!(nom.actHeading.Contains(SearchByHeading))) return false;
            }
            else if (SearchByOrg != null)
            {
                if (!(nom.nameOrg.Contains(SearchByOrg))) return false;
            }
            else if (SearchByYear != 0)
            {
                if (!(nom.yearNom.Value.Equals(SearchByYear))) return false;
            }
            return result;
        }
    }
}
