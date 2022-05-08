using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPFArchive.ViewModel;

namespace WPFArchive.ViewModel
{
    class SetOfDocsViewModel : INotifyPropertyChanged
    {
        private RelayCommand saveCommand;
        private int idOrg;
        private int idSet;
        private int numberOfSet;
        private int copies;
        private DateTime startDate;
        private DateTime endDate;
        private int idCategory;
        private int idAccess;
        private string reasonOfClosedAccess;
        private string nameOfSet;
        private List<organization> org;
        public event Action OnSuccess;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public int IdOrg
        {
            get { return idOrg; }
            set { idOrg = value; }
        }
        public int NumberOfSet
        {
            get { return numberOfSet; }
            set { numberOfSet = value; }
        }
        public int Copies
        {
            get { return copies; }
            set { copies = value; }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public int Category
        {
            get { return idCategory; }
            set { idCategory = value; }
        }
        public int Access
        {
            get { return idAccess; }
            set { idAccess = value; }
        }
        public string ReasonOfClosedAccess
        {
            get { return reasonOfClosedAccess; }
            set { reasonOfClosedAccess = value; }
        }
        public string NameOfSet
        {
            get { return nameOfSet; }
            set { nameOfSet = value; }
        }
        public List<organization> Organizations 
        { 
            get { return org; }
            private set { org = value; }
        }
        public List<categoryOfSet> Categories { get; private set; }
        public List<access> Accesses { get; private set; }
        public List<SetOfDocs_Select_Result> SetOfDocs { get; private set; }

        public SetOfDocsViewModel()
        {
            startDate = DateTime.Now.Date;
            endDate = DateTime.Now.Date;
            GetEntities();
            using (var db = new archiveEntities())
            {
                SetOfDocs = db.SetOfDocs_Select().ToList();
            }
        }
        public SetOfDocsViewModel(int idSet)
        {
            this.idSet = idSet;
            startDate = DateTime.Now.Date;
            endDate = DateTime.Now.Date;
            GetEntities();
            using (var db = new archiveEntities())
            {
                SetOfDocs = db.SetOfDocs_Select().ToList();
                var set = db.setOfDocs.Where(x => x.idSet == idSet).First();
                IdOrg = set.organization_idOrg;
                Category = set.categoryOfSet_idCatOfSet;
                Access = set.access_idAccess;
                StartDate = (DateTime)set.startDate;
                EndDate = (DateTime)set.endDate;
                NumberOfSet = (int)set.numberSet;
                Copies = (int)set.copies;
                NameOfSet = set.nameOfSet;
                ReasonOfClosedAccess = set.reasonOfClosedAccess;
            }
        }
        async private void GetEntities() 
        {
            using (var db = new archiveEntities())
            {
                Organizations = db.organization.ToList();
                Categories = db.categoryOfSet.ToList();
                Accesses = db.access.ToList();
            }
        }

        //добавление описи
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand((o) =>
                    {
                        using (var db = new archiveEntities())
                        {
                            if (idSet != 0) 
                            {/////обновление
                                var set = db.setOfDocs.Where(x => x.idSet == idSet).First();
                                set.organization_idOrg = IdOrg;
                                set.categoryOfSet_idCatOfSet = Category;
                                set.access_idAccess = Access;
                                set.startDate = StartDate;
                                set.endDate = EndDate;
                                set.numberSet = NumberOfSet;
                                set.copies = Copies;
                                set.nameOfSet = NameOfSet;
                                set.reasonOfClosedAccess = ReasonOfClosedAccess;
                                if (set.organization_idOrg == 0 ||
                                    set.categoryOfSet_idCatOfSet == 0 || set.access_idAccess == 0 ||
                                    set.startDate == null || set.endDate == null || set.numberSet == null ||
                                    set.copies == 0 || set.copies == null || set.copies == 0)
                                {
                                    MessageBox.Show("Только поле ''Наименование описи'' может быть пустым. Остальные поля необходимо заполнить.");
                                }
                                else if (StartDate > EndDate)
                                {
                                    MessageBox.Show("Начальная дата не может быть позднее конечной даты");
                                }
                                else if (SetOfDocs.Exists(x => x.numberSet == NumberOfSet && x.idSet != idSet && x.nameOrg == (Organizations.Find(y => y.idOrg == IdOrg).nameOrg) && x.categoryOfSet_idCatOfSet == Category) == true)
                                {
                                    MessageBox.Show("Опись с номером " + NumberOfSet + " и категорией ''" + SetOfDocs.Find(x => x.categoryOfSet_idCatOfSet == Category).nameCatOfSet + "'' для данной организации уже добавлена");
                                }
                                else
                                {
                                    db.SaveChanges();
                                    ArchiveProxyModel.getInstance().emitUpdateSetOfDocs();
                                    MessageBox.Show("Информация об описи успешно отредактирована");
                                    OnSuccess?.Invoke(); //закрытие формы добавления
                                }
                            }
                            else
                            {////добавление
                                setOfDocs newSet = new setOfDocs
                                {
                                    organization_idOrg = IdOrg,
                                    categoryOfSet_idCatOfSet = Category,
                                    access_idAccess = Access,
                                    startDate = StartDate,
                                    endDate = EndDate,
                                    numberSet = NumberOfSet,
                                    copies = Copies,
                                    nameOfSet = NameOfSet,
                                    reasonOfClosedAccess = ReasonOfClosedAccess
                                };
                                if (newSet.organization_idOrg == 0 ||
                                    newSet.categoryOfSet_idCatOfSet == 0 || newSet.access_idAccess == 0 ||
                                    newSet.startDate == null || newSet.endDate == null || newSet.numberSet == null ||
                                    newSet.copies == 0 || newSet.copies == null || newSet.copies == 0)
                                {
                                    MessageBox.Show("Только поле ''Наименование описи'' может быть пустым. Остальные поля необходимо заполнить.");
                                }
                                else if (StartDate > EndDate)
                                {
                                    MessageBox.Show("Начальная дата не может быть позднее конечной даты");
                                }
                                else if (SetOfDocs.Exists(x => x.numberSet == NumberOfSet && x.idSet != idSet && x.nameOrg == (Organizations.Find(y => y.idOrg == IdOrg).nameOrg) && x.categoryOfSet_idCatOfSet == Category) == true)
                                {
                                    MessageBox.Show("Опись с номером " + NumberOfSet + " и категорией ''" + SetOfDocs.Find(x => x.categoryOfSet_idCatOfSet == Category).nameCatOfSet + "'' для данной организации уже добавлена");
                                }
                                else
                                {
                                    ArchiveProxyModel.getInstance().addSetOfDocs(newSet);
                                    OnSuccess?.Invoke();
                                    MessageBox.Show("Опись успешно добавлена");
                                }
                            }
                        }   
                    }));
            }
        }
    }
}
