using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFArchive.ViewModel
{
    class RenameViewModel : INotifyPropertyChanged
    {
        
        public event Action OnSuccess;
        public event PropertyChangedEventHandler PropertyChanged;
        private string nameOrg;
        public string nameRenamed;
        public int organization_idOrg;
        public int idRename;
        public DateTime startDate;
        public DateTime endDate;
        private RelayCommand saveCommand;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public List<organization> Organizations { get; private set; }
        public List<renamesOfOrganization> Renames { get; private set; }
        public string NameOrg
        {
            get { return nameOrg; }
            set
            {
                nameOrg = Organizations.Find(x => x.idOrg == organization_idOrg).nameOrg;
            }
        }
        public string NameRenamed { get { return nameRenamed; } set { nameRenamed = value; } }
        public DateTime StartDate { get { return startDate; } set { startDate = value; } }
        public DateTime EndDate { get { return endDate; } set { endDate = value; } }
        async private void GetEntities()
        {
            using (var db = new archiveEntities())
            {
                Organizations = db.organization.ToList();
                Renames = db.renamesOfOrganization.ToList();
            }
        }
        public RenameViewModel(int idOrg)
        {
            organization_idOrg = idOrg;
            startDate = DateTime.Now.Date;
            endDate = DateTime.Now.Date;
            GetEntities();
            NameOrg = "";
        }
        public RenameViewModel(int idOrg, int idRename)
        {
            this.idRename = idRename;
            organization_idOrg = idOrg;
            startDate = DateTime.Now.Date;
            endDate = DateTime.Now.Date;
            GetEntities();
            NameOrg = "";
            using (var db = new archiveEntities())
            {
                var rename = db.renamesOfOrganization.Where(x => x.idNameOfOrganization == idRename).First();
                NameRenamed = rename.nameRenamed;
                StartDate = rename.startDate;
                if (rename.endDate != null) { EndDate = (DateTime)rename.endDate;  }
            }
        }

        //добавление информации о переименованиях
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      using (var db = new archiveEntities())
                      {
                          if (idRename!=0) 
                          {/////обновление
                              var rename = db.renamesOfOrganization.Where(x => x.idNameOfOrganization == idRename).First();
                              rename.organization_idOrg = organization_idOrg;
                              rename.nameRenamed = NameRenamed;
                              rename.startDate = StartDate;
                              rename.endDate = EndDate;
                              if (rename.nameRenamed == "" || rename.startDate == null || rename.endDate == null)
                              {
                                  MessageBox.Show("Необходимо заполнить все поля.");
                              }
                              else if (Renames.Exists(x => (x.organization_idOrg == organization_idOrg && x.nameRenamed == NameRenamed && x.startDate == StartDate && x.endDate == EndDate) && x.idNameOfOrganization != idRename) == true)
                              {
                                  MessageBox.Show("Информация о таком названии уже добавлена.");
                              }
                              else if (rename.endDate < rename.startDate)
                              {
                                  MessageBox.Show("Конечная дата переименования не может быть меньше начальной даты.");
                              }
                              else
                              {
                                  db.SaveChanges();
                                  ArchiveProxyModel.getInstance().emitUpdateRename();
                                  MessageBox.Show("Информация о предыдущем названии организации успешно отредактирована");
                                  OnSuccess?.Invoke(); //закрытие формы добавления
                              }
                          }
                          else
                          {////добавление
                              renamesOfOrganization newRename = new renamesOfOrganization
                              {
                                  organization_idOrg = organization_idOrg,
                                  nameRenamed = NameRenamed,
                                  startDate = StartDate,
                                  endDate = EndDate
                              };
                              if (newRename.nameRenamed == "" || newRename.startDate == null || newRename.endDate == null)
                              {
                                  MessageBox.Show("Необходимо заполнить все поля.");
                              }
                              else if (Renames.Exists(x => x.organization_idOrg == organization_idOrg && x.nameRenamed == NameRenamed && x.startDate == StartDate && x.endDate == EndDate) == true)
                              {
                                  MessageBox.Show("Информация о таком названии уже добавлена.");
                              }
                              else if (newRename.endDate < newRename.startDate)
                              {
                                  MessageBox.Show("Конечная дата переименования не может быть меньше начальной даты.");
                              }
                              else
                              {
                                  ArchiveProxyModel.getInstance().addRename(newRename);
                                  OnSuccess?.Invoke(); //закрытие формы добавления
                                  MessageBox.Show("Информация о мероприятии успешно добавлена.");
                              }
                          }
                      }
                  }));
            }
        }
    }
}
