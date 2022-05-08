using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPFArchive.View;

namespace WPFArchive.ViewModel
{
    class OrganizationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnSuccess;
        public int idCurrentOrg; //организация, сведения о которой открыты на форме

        public OrganizationViewModel(int idOrg)
        {
            foundationDate = DateTime.Now.Date;
            GetEntities();
            this.idCurrentOrg = idOrg;

            if (idOrg != 0)
            {
                //заполнение полей данными для редактирования
                using (var db = new archiveEntities())
                {
                    var org = db.organization.Where(x => x.idOrg == idOrg).First();
                    NameOrg = org.nameOrg;
                    IdRecForm = org.receivingForm_idRecForm;
                    IdColProf = org.collectionProfile_idColProf;
                    IdOwnForm = org.ownForm_idOwnForm;
                    FoundationDate = (DateTime)org.foundationDate;
                    Adress = org.adress;
                    Curator = org.curator;
                    NameDirector = org.nameDirector;
                    PhoneDirector = (long)org.phoneDirector;
                    EmailDirector = org.emailDirector;
                    NameRespPers = org.nameRespPers;
                    PhoneRespPers = (long)org.phoneRespPers;
                    EmailRespPers = org.emailRespPers;
                    IsSource = (bool)org.isSource;
                    if (OrganizationArchives.Exists(x => x.organization_idOrg == idOrg) == true) 
                    {
                        var orgArchive = db.organizationArchive.Where(x => x.organization_idOrg == idOrg).First();
                        if (orgArchive.storageExistence == true)
                        {
                            Heating = (int)orgArchive.heating_idHeating;
                            Shelving = (int)orgArchive.shelving_idShelving;
                            Cupboard = (int)orgArchive.cupboard_idCupboard;
                            FundCount = (int)orgArchive.fundCount;
                            StorageSquare = (float)orgArchive.storageSquare;
                            FilledCapacityPercent = (float)orgArchive.filledCapacityPercent;
                            EmployeeCount = (int)orgArchive.employeeCount;
                            TimeOfStorage = (int)orgArchive.timeOfStorage;
                            StorageDescription = orgArchive.storageDescription;
                            StorageExistence = orgArchive.storageExistence;
                            FireAlarm = (bool)orgArchive.fireAlarm;
                            SecurityAlarm = (bool)orgArchive.securityAlarm;
                            TempAndCoolConditions = (bool)orgArchive.tempAndCoolConditions;
                        }  
                    }
                }
            }
        }


        public OrganizationViewModel() : this(0)
        {
            //обновление таблицы с мероприятиями
            /*ArchiveProxyModel.getInstance().OnEventsUpdate += () =>
            {
                using (var db = new archiveEntities())
                {
                    Events = db.SelectEventLogbookForOrg(5010).ToList();
                }
            };*/
        }

        ///////////////////ОБЩИЕ СВЕДЕНИЯ
        public List<ownForm> OwnForms { get; private set; }
        public List<receivingForm> RecForms { get; private set; }
        public List<collectionProfile> ColProfs { get; private set; }
        public List<organization> Organizations { get; private set; }
        private RelayCommand saveOrgCommand;
        private string nameOrg;
        private DateTime foundationDate;
        private string adress;
        private string nameDirector;
        private string emailDirector;
        private long phoneDirector;
        private string nameRespPers;
        private string emailRespPers;
        private long phoneRespPers;
        private string curator;
        private int ownForm_idOwnForm;
        private int receivingForm_idRecForm;
        private int collectionProfile_idColProf;
        private bool isSource;
        public string NameOrg
        {
            get { return nameOrg; }
            set
            {
                nameOrg = value;
                OnPropertyChanged("NameOrg");
            }
        }
        public DateTime FoundationDate { get { return foundationDate; } set { foundationDate = value; } }
        public string Adress { get { return adress; } set { adress = value; } }
        public string NameDirector { get { return nameDirector; } set { nameDirector = value; } }
        public string EmailDirector { get { return emailDirector; } set { emailDirector = value; } }
        public long PhoneDirector { get { return phoneDirector; } set { phoneDirector = value; } }
        public string NameRespPers { get { return nameRespPers; } set { nameRespPers = value; } }
        public string EmailRespPers { get { return emailRespPers; } set { emailRespPers = value; } }
        public long PhoneRespPers { get { return phoneRespPers; }  set { phoneRespPers = value; } }
        public string Curator { get { return curator; } set { curator = value; } }
        public int IdOwnForm { get { return ownForm_idOwnForm; } set { ownForm_idOwnForm = value; } }
        public int IdRecForm { get { return receivingForm_idRecForm; } set { receivingForm_idRecForm = value; } }
        public int IdColProf { get { return collectionProfile_idColProf; } set { collectionProfile_idColProf = value; } }
        public bool IsSource { get { return isSource; } set { isSource = value; } }
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        async private void GetEntities()
        {
            using (var db = new archiveEntities())
            {
                OwnForms = db.ownForm.ToList();
                RecForms = db.receivingForm.ToList();
                ColProfs = db.collectionProfile.ToList();
                Organizations = db.organization.ToList();
                Heatings = db.heating.ToList();
                Shelvings = db.shelving.ToList();
                Cupboards = db.cupboard.ToList();
                OrganizationArchives = db.organizationArchive.ToList();
                Events = db.event_logbook.ToList();
                Renames = db.renamesOfOrganization.ToList();
            }
        }
        //добавление и обновление организации
        public RelayCommand SaveOrgCommand
        {
            get
            {
                return saveOrgCommand ??
                  (saveOrgCommand = new RelayCommand(obj =>
                  {
                      using (var db = new archiveEntities())
                      {
                          //обновление
                          if (Organizations.Exists(x => x.idOrg == idCurrentOrg) == true)
                          {
                              var org = db.organization.Where(x => x.idOrg == idCurrentOrg).First();
                              org.nameOrg = NameOrg;
                              org.receivingForm_idRecForm = IdRecForm;
                              org.collectionProfile_idColProf = IdColProf;
                              org.ownForm_idOwnForm = IdOwnForm;
                              org.foundationDate = FoundationDate;
                              org.adress = Adress;
                              org.curator = Curator;
                              org.nameDirector = NameDirector;
                              org.phoneDirector = PhoneDirector;
                              org.emailDirector = EmailDirector;
                              org.nameRespPers = NameRespPers;
                              org.phoneRespPers = PhoneRespPers;
                              org.emailRespPers = EmailRespPers;
                              org.isSource = IsSource;
                              if (org.nameOrg == null || org.receivingForm_idRecForm == 0 ||
                                    org.collectionProfile_idColProf == 0 || org.ownForm_idOwnForm == 0 ||
                                    org.adress == null || org.curator == null || org.nameDirector == null ||
                                    org.phoneDirector == null || org.emailDirector == null || org.nameRespPers == null
                                    || org.phoneRespPers == null || org.emailRespPers == null)
                              {
                                  MessageBox.Show("Необходимо заполнить все поля");
                              }
                              else if (Organizations.Exists(x => x.nameOrg == NameOrg && x.idOrg != org.idOrg) == true)
                              {
                                  MessageBox.Show("Такая организация уже добавлена.");
                              }
                              else
                              {
                                  db.SaveChanges();
                                  ArchiveProxyModel.getInstance().emitUpdateOrganization();
                                  MessageBox.Show("Информация об организации " + NameOrg + " успешно отредактирована");
                              }
                          }
                          else
                          { //добавление
                              organization newOrg = new organization
                              {
                                  nameOrg = NameOrg,
                                  receivingForm_idRecForm = IdRecForm,
                                  collectionProfile_idColProf = IdColProf,
                                  ownForm_idOwnForm = IdOwnForm,
                                  foundationDate = FoundationDate,
                                  adress = Adress,
                                  curator = Curator,
                                  nameDirector = NameDirector,
                                  phoneDirector = PhoneDirector,
                                  emailDirector = EmailDirector,
                                  nameRespPers = NameRespPers,
                                  phoneRespPers = PhoneRespPers,
                                  emailRespPers = EmailRespPers,
                                  isSource = IsSource
                              };
                              if (newOrg.nameOrg == null || newOrg.receivingForm_idRecForm == 0 ||
                                    newOrg.collectionProfile_idColProf == 0 || newOrg.ownForm_idOwnForm == 0 ||
                                    newOrg.adress == null || newOrg.curator == null || newOrg.nameDirector == null ||
                                    newOrg.phoneDirector == null || newOrg.emailDirector == null || newOrg.nameRespPers == null
                                    || newOrg.phoneRespPers == null || newOrg.emailRespPers == null)
                              {
                                  MessageBox.Show("Необходимо заполнить все поля");
                              }
                              else if (Organizations.Exists(x => x.nameOrg == NameOrg) == true)
                              {
                                  MessageBox.Show("Организация " + NameOrg + " уже добавлена.");
                              }
                              else
                              {
                                  ArchiveProxyModel.getInstance().addOrganization(newOrg);
                                  //OnSuccess?.Invoke(); //закрытие формы добавления
                                  MessageBox.Show("Организация " + NameOrg + " успешно добавлена.");
                              }
                          }
                      }
                  }));
            }
        }

        //////////////////АРХИВ ОРГАНИЗАЦИИ
        ///
        private int heating_idHeating;
        private int shelving_idShelving;
        private int cupboard_idCupboard;
        private int fundCount;
        private float storageSquare;
        private float filledCapacityPercent;
        private int employeeCount;
        private float timeOfStorage;
        private string storageDescription;
        private bool storageExistence;
        private bool fireAlarm;
        private bool securityAlarm;
        private bool tempAndCoolConditions;
        private RelayCommand saveArchiveOrgCommand, addAboutArchiveCommand, openAboutArchiveCommand, deleteAboutArchiveCommand, addExpertFileCommand, addInstructionFileCommand, openExpertFileCommand, openInstructionFileCommand, deleteExpertFileCommand, deleteInstructionFileCommand;
        public List<organizationArchive> OrganizationArchives { get; private set; }
        public List<heating> Heatings { get; private set; }
        public int Heating { get { return heating_idHeating; } set { heating_idHeating = value; } }
        public List<shelving> Shelvings { get; private set; }
        public int Shelving { get { return shelving_idShelving; } set { shelving_idShelving = value; } }
        public List<cupboard> Cupboards { get; private set; }
        public int Cupboard { get { return cupboard_idCupboard; } set { cupboard_idCupboard = value; } }
        public int FundCount { get { return fundCount; } set { fundCount = value; } }
        public float StorageSquare { get { return storageSquare; } set { storageSquare = value; } }
        public float FilledCapacityPercent { get { return filledCapacityPercent; } set { filledCapacityPercent = value; } }
        public int EmployeeCount { get { return employeeCount; } set { employeeCount = value; } }
        public float TimeOfStorage { get { return timeOfStorage; } set { timeOfStorage = value; } }
        public string StorageDescription { get { return storageDescription; } set { storageDescription = value; } }
        public bool StorageExistence { get { return storageExistence; } set { storageExistence = value; } }
        public bool FireAlarm { get { return fireAlarm; } set { fireAlarm = value; } }
        public bool SecurityAlarm { get { return securityAlarm; } set { securityAlarm = value; } }
        public bool TempAndCoolConditions { get { return tempAndCoolConditions; } set { tempAndCoolConditions = value; } }
        //добавление и обновление информации об архиве организации
        public RelayCommand SaveArchiveOrgCommand
        {
            get
            {
                return saveArchiveOrgCommand ??
                  (saveArchiveOrgCommand = new RelayCommand(obj =>
                  {
                      
                      using (var db = new archiveEntities())
                      {
                          Organizations = db.organization.ToList();
                          idCurrentOrg = Organizations.Find(x => x.nameOrg == NameOrg).idOrg;
                          //обновление
                          if (OrganizationArchives.Exists(x => x.organization_idOrg == idCurrentOrg) == true)
                          {
                              var orgArchive = db.organizationArchive.Where(x => x.organization_idOrg == idCurrentOrg).First();
                              orgArchive.heating_idHeating = Heating;
                              orgArchive.shelving_idShelving = Shelving;
                              orgArchive.cupboard_idCupboard = Cupboard;
                              orgArchive.organization_idOrg = idCurrentOrg;
                              orgArchive.fundCount = FundCount;
                              orgArchive.storageSquare = StorageSquare;
                              orgArchive.filledCapacityPercent = FilledCapacityPercent;
                              orgArchive.employeeCount = EmployeeCount;
                              orgArchive.timeOfStorage = TimeOfStorage;
                              orgArchive.storageDescription = StorageDescription;
                              orgArchive.storageExistence = StorageExistence;
                              orgArchive.fireAlarm = FireAlarm;
                              orgArchive.securityAlarm = SecurityAlarm;
                              orgArchive.tempAndCoolConditions = TempAndCoolConditions;
                              if (orgArchive.storageExistence == true && (orgArchive.heating_idHeating == 0 || orgArchive.shelving_idShelving == 0 || orgArchive.cupboard_idCupboard == 0 || orgArchive.fundCount == null || orgArchive.storageSquare == null || orgArchive.filledCapacityPercent == null || orgArchive.employeeCount == null || orgArchive.timeOfStorage == null || orgArchive.storageDescription == null))
                              {
                                  MessageBox.Show("Необходимо заполнить все поля");
                              }
                              else
                              { 
                                  if (orgArchive.storageExistence == false)
                                  {//если отмечено что архива в организации нет, все равно добалвяется запись в бд, в которой все значения null
                                      orgArchive.heating_idHeating = null;
                                      orgArchive.shelving_idShelving = null;
                                      orgArchive.cupboard_idCupboard = null;
                                      orgArchive.fundCount = null;
                                      orgArchive.storageSquare = null;
                                      orgArchive.filledCapacityPercent = null;
                                      orgArchive.employeeCount = null;
                                      orgArchive.timeOfStorage = null;
                                      orgArchive.storageDescription = null;
                                      orgArchive.fireAlarm = null;
                                      orgArchive.securityAlarm = null;
                                      orgArchive.tempAndCoolConditions = null;
                                  }
                                  db.SaveChanges();
                                  ArchiveProxyModel.getInstance().emitUpdateOrganizationArchive();
                                  MessageBox.Show("Информация об Архиве организации " + NameOrg + " успешно отредактирована");
                              }
                          }
                          else
                          { //добавление
                              organizationArchive newOrgArchive = new organizationArchive
                              {
                                  heating_idHeating = Heating,
                                  shelving_idShelving = Shelving,
                                  cupboard_idCupboard = Cupboard,
                                  organization_idOrg = idCurrentOrg,
                                  fundCount = FundCount,
                                  storageSquare = StorageSquare,
                                  filledCapacityPercent = FilledCapacityPercent,
                                  employeeCount = EmployeeCount,
                                  timeOfStorage = TimeOfStorage,
                                  storageDescription = StorageDescription,
                                  storageExistence = StorageExistence,
                                  fireAlarm = FireAlarm,
                                  securityAlarm = SecurityAlarm,
                                  tempAndCoolConditions = TempAndCoolConditions
                              };
                              if (idCurrentOrg == 0)
                              {
                                  MessageBox.Show("Сначала заполните 'Общие сведения' об организации.");
                              }
                              else if (newOrgArchive.storageExistence == true && (newOrgArchive.heating_idHeating == 0 || newOrgArchive.shelving_idShelving == 0 || newOrgArchive.cupboard_idCupboard == 0 || newOrgArchive.fundCount == null || newOrgArchive.storageSquare == null || newOrgArchive.filledCapacityPercent == null || newOrgArchive.employeeCount == null || newOrgArchive.timeOfStorage == null || newOrgArchive.storageDescription == null))
                              {
                                  MessageBox.Show("Необходимо заполнить все поля");
                              }
                              else 
                              {
                                  if (newOrgArchive.storageExistence == false)
                                  {
                                      newOrgArchive.heating_idHeating = null;
                                      newOrgArchive.shelving_idShelving = null;
                                      newOrgArchive.cupboard_idCupboard = null;
                                      newOrgArchive.fundCount = null;
                                      newOrgArchive.storageSquare = null;
                                      newOrgArchive.filledCapacityPercent = null;
                                      newOrgArchive.employeeCount = null;
                                      newOrgArchive.timeOfStorage = null;
                                      newOrgArchive.storageDescription = null;
                                      newOrgArchive.fireAlarm = null;
                                      newOrgArchive.securityAlarm = null;
                                      newOrgArchive.tempAndCoolConditions = null;
                                  }
                                  ArchiveProxyModel.getInstance().addArchiveOrganization(newOrgArchive);
                                  OnSuccess?.Invoke(); //закрытие формы добавления
                                  MessageBox.Show("Информация об Архиве организации " + NameOrg + " успешно добавлена."  + "Внимание! Для того, чтобы изменения вступили в силу, необходимо закрыть форму.");
                              }
                          }
                      }
                  }));
            }
        }
        //добавление файла положения об архиве
        public RelayCommand AddAboutArchiveCommand
        {
            get
            {
                return addAboutArchiveCommand ??
                  (addAboutArchiveCommand = new RelayCommand(obj =>
                  {
                      OpenFileDialog saveFileDialog = new OpenFileDialog();
                      saveFileDialog.FileName = "путь к файлу, который надо прикрепить";
                      saveFileDialog.Title = "Выберите файл";
                      if (OrganizationArchives.Exists(x => x.organization_idOrg == idCurrentOrg) == true)
                      {
                          if (saveFileDialog.ShowDialog() == DialogResult.OK)
                          {
                              using (var db = new archiveEntities())
                              {
                                  string sourceFilename = saveFileDialog.FileName; //директория выбранного файла
                                  string extension = Path.GetExtension(sourceFilename); //расширение файла
                                  string destFilename;
                                  //путь (только каталоги) куда поместить выбранный файл
                                  destFilename = "D:/Архив/" + Organizations.Find(x => x.idOrg == idCurrentOrg).idOrg;

                                  Directory.CreateDirectory(destFilename);//создание каталогов, если их нет
                                  destFilename += "/" + "Положение об архиве_" + Organizations.Find(x => x.idOrg == idCurrentOrg).idOrg + extension;//директория файла для перемещения в нее
                                  try
                                  {
                                      File.Copy(sourceFilename, destFilename);
                                      var orgArch = db.organizationArchive.Where(x => x.organization_idOrg == idCurrentOrg).First();
                                      orgArch.pathAboutArchive = destFilename;
                                      db.SaveChanges();
                                      ArchiveProxyModel.getInstance().emitUpdateOrganizationArchive();
                                      System.Windows.Forms.MessageBox.Show("Файл Положения об архиве был успешно добавлен.");
                                  }
                                  catch (IOException e)
                                  {
                                      if (System.Windows.Forms.MessageBox.Show("Файл Положения об архиве для этой организации уже прикреплен. Вы хотите заменить его?", "Прикрепление файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                      {
                                          File.Copy(sourceFilename, destFilename, true);
                                          var orgArch = db.organizationArchive.Where(x => x.organization_idOrg == idCurrentOrg).First();
                                          orgArch.pathAboutArchive = destFilename;
                                          db.SaveChanges();
                                          ArchiveProxyModel.getInstance().emitUpdateOrganizationArchive();
                                          System.Windows.Forms.MessageBox.Show("Файл Положения об архиве успешно заменен новым.");
                                      }
                                      else
                                          try { File.Copy(sourceFilename, destFilename, false); }
                                          catch (IOException ioe) { System.Windows.Forms.MessageBox.Show("Файл Положения об архиве не был изменен."); }
                                  }
                              }
                          }
                      }
                      else System.Windows.Forms.MessageBox.Show("Сначала добавьте информацию об архиве организации и сохраните её.");
                  }));
            }
        }
        //открытие файла положения об архиве
        public RelayCommand OpenAboutArchiveCommand
        {
            get
            {
                return openAboutArchiveCommand ??
                  (openAboutArchiveCommand = new RelayCommand(obj =>
                  {
                          string path = OrganizationArchives.Find(x => x.organization_idOrg == idCurrentOrg).pathAboutArchive;
                          if (path != null)
                              Process.Start(path);
                          else MessageBox.Show("Файл не был добавлен. (или попробуйте перезапустить окно)");
                  }));
            }
        }
        //удаление файла положения об архиве
        public RelayCommand DeleteAboutArchiveCommand
        {
            get
            {
                return deleteAboutArchiveCommand ??
                  (deleteAboutArchiveCommand = new RelayCommand(obj =>
                  {
                      if (System.Windows.Forms.MessageBox.Show("Файл будет удален из папки полностью. Удалить файл?", "Удаление файла документа", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                      {
                          using (var db = new archiveEntities())
                          {
                              var orgArch = db.organizationArchive.Where(x => x.organization_idOrg == idCurrentOrg).First();
                              File.Delete(orgArch.pathAboutArchive);
                              orgArch.pathAboutArchive = null;
                              db.SaveChanges();
                              ArchiveProxyModel.getInstance().emitUpdateOrganizationArchive();
                              System.Windows.Forms.MessageBox.Show("Файл Положения об архиве был удален.");
                          }
                      }
                  }));
            }
        }

        //добавление файла положения об ЭК
        public RelayCommand AddExpertFileCommand
        {
            get
            {
                return addExpertFileCommand ??
                  (addExpertFileCommand = new RelayCommand(obj =>
                  {
                      OpenFileDialog saveFileDialog = new OpenFileDialog();
                      saveFileDialog.FileName = "путь к файлу, который надо прикрепить";
                      saveFileDialog.Title = "Выберите файл";
                      if (OrganizationArchives.Exists(x => x.organization_idOrg == idCurrentOrg) == true)
                      {
                          if (saveFileDialog.ShowDialog() == DialogResult.OK)
                          {
                              using (var db = new archiveEntities())
                              {
                                  string sourceFilename = saveFileDialog.FileName; //директория выбранного файла
                                  string extension = Path.GetExtension(sourceFilename); //расширение файла
                                  string destFilename;
                                  //путь (только каталоги) куда поместить выбранный файл
                                  destFilename = "D:/Архив/" + Organizations.Find(x => x.idOrg == idCurrentOrg).idOrg;

                                  Directory.CreateDirectory(destFilename);//создание каталогов, если их нет
                                  destFilename += "/" + "Положение об ЭК_" + Organizations.Find(x => x.idOrg == idCurrentOrg).idOrg + extension;//директория файла для перемещения в нее
                                  try
                                  {
                                      File.Copy(sourceFilename, destFilename);
                                      var orgArch = db.organizationArchive.Where(x => x.organization_idOrg == idCurrentOrg).First();
                                      orgArch.pathExpertCommission = destFilename;
                                      db.SaveChanges();
                                      ArchiveProxyModel.getInstance().emitUpdateOrganizationArchive();
                                      System.Windows.Forms.MessageBox.Show("Файл Положения об ЭК был успешно добавлен.");
                                  }
                                  catch (IOException e)
                                  {
                                      if (System.Windows.Forms.MessageBox.Show("Файл Положения об ЭК для этой организации уже прикреплен. Вы хотите заменить его?", "Прикрепление файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                      {
                                          File.Copy(sourceFilename, destFilename, true);
                                          var orgArch = db.organizationArchive.Where(x => x.organization_idOrg == idCurrentOrg).First();
                                          orgArch.pathExpertCommission = destFilename;
                                          db.SaveChanges();
                                          ArchiveProxyModel.getInstance().emitUpdateOrganizationArchive();
                                          System.Windows.Forms.MessageBox.Show("Файл Положения об ЭК успешно заменен новым.");
                                      }
                                      else
                                          try { File.Copy(sourceFilename, destFilename, false); }
                                          catch (IOException ioe) { System.Windows.Forms.MessageBox.Show("Файл Положения об ЭК не был изменен."); }
                                  }
                              }
                          }
                      }
                      else System.Windows.Forms.MessageBox.Show("Сначала добавьте информацию об архиве организации и сохраните её.");
                  }));
            }
        }
        //открытие файла положения об ЭК
        public RelayCommand OpenExpertFileCommand
        {
            get
            {
                return openExpertFileCommand ??
                  (openExpertFileCommand = new RelayCommand(obj =>
                  {
                      string path = OrganizationArchives.Find(x => x.organization_idOrg == idCurrentOrg).pathExpertCommission;
                      if (path != null)
                          Process.Start(path);
                      else MessageBox.Show("Файл не был добавлен. (или попробуйте перезапустить окно)");
                  }));
            }
        }
        //удаление файла положения об ЭК
        public RelayCommand DeleteExpertFileCommand
        {
            get
            {
                return deleteExpertFileCommand ??
                  (deleteExpertFileCommand = new RelayCommand(obj =>
                  {
                      if (System.Windows.Forms.MessageBox.Show("Файл будет удален из папки полностью. Удалить файл?", "Удаление файла документа", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                      {
                          using (var db = new archiveEntities())
                          {
                              var orgArch = db.organizationArchive.Where(x => x.organization_idOrg == idCurrentOrg).First();
                              File.Delete(orgArch.pathExpertCommission);
                              orgArch.pathExpertCommission = null;
                              db.SaveChanges();
                              ArchiveProxyModel.getInstance().emitUpdateOrganizationArchive();
                              System.Windows.Forms.MessageBox.Show("Файл Положения об ЭК был удален.");
                          }
                      }
                  }));
            }
        }

        //добавление файла инструкции
        public RelayCommand AddInstructionFileCommand
        {
            get
            {
                return addInstructionFileCommand ??
                  (addInstructionFileCommand = new RelayCommand(obj =>
                  {
                      OpenFileDialog saveFileDialog = new OpenFileDialog();
                      saveFileDialog.FileName = "путь к файлу, который надо прикрепить";
                      saveFileDialog.Title = "Выберите файл";
                      if (OrganizationArchives.Exists(x => x.organization_idOrg == idCurrentOrg) == true)
                      {
                          if (saveFileDialog.ShowDialog() == DialogResult.OK)
                          {
                              using (var db = new archiveEntities())
                              {
                                  string sourceFilename = saveFileDialog.FileName; //директория выбранного файла
                                  string extension = Path.GetExtension(sourceFilename); //расширение файла
                                  string destFilename;
                                  //путь (только каталоги) куда поместить выбранный файл
                                  destFilename = "D:/Архив/" + Organizations.Find(x => x.idOrg == idCurrentOrg).idOrg;

                                  Directory.CreateDirectory(destFilename);//создание каталогов, если их нет
                                  destFilename += "/" + "Инструкция по делопроизводству_" + Organizations.Find(x => x.idOrg == idCurrentOrg).idOrg + extension;//директория файла для перемещения в нее
                                  try
                                  {
                                      File.Copy(sourceFilename, destFilename);
                                      var orgArch = db.organizationArchive.Where(x => x.organization_idOrg == idCurrentOrg).First();
                                      orgArch.pathInstruction = destFilename;
                                      db.SaveChanges();
                                      ArchiveProxyModel.getInstance().emitUpdateOrganizationArchive();
                                      System.Windows.Forms.MessageBox.Show("Файл Интрукции по делопроизводству был успешно добавлен.");
                                  }
                                  catch (IOException e)
                                  {
                                      if (System.Windows.Forms.MessageBox.Show("Файл Интрукции по делопроизводству для этой организации уже прикреплен. Вы хотите заменить его?", "Прикрепление файла", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                      {
                                          File.Copy(sourceFilename, destFilename, true);
                                          var orgArch = db.organizationArchive.Where(x => x.organization_idOrg == idCurrentOrg).First();
                                          orgArch.pathInstruction = destFilename;
                                          db.SaveChanges();
                                          ArchiveProxyModel.getInstance().emitUpdateOrganizationArchive();
                                          System.Windows.Forms.MessageBox.Show("Файл Интрукции по делопроизводству успешно заменен новым.");
                                      }
                                      else
                                          try { File.Copy(sourceFilename, destFilename, false); }
                                          catch (IOException ioe) { System.Windows.Forms.MessageBox.Show("Файл Интрукции по делопроизводству не был изменен."); }
                                  }
                              }
                          }
                      }
                      else System.Windows.Forms.MessageBox.Show("Сначала добавьте информацию об архиве организации и сохраните её.");
                  }));
            }
        }
        //открытие файла инструкции
        public RelayCommand OpenInstructionFileCommand
        {
            get
            {
                return openInstructionFileCommand ??
                  (openInstructionFileCommand = new RelayCommand(obj =>
                  {
                      string path = OrganizationArchives.Find(x => x.organization_idOrg == idCurrentOrg).pathInstruction;
                      if (path != null)
                          Process.Start(path);
                      else MessageBox.Show("Файл не был добавлен. (или попробуйте перезапустить окно)");
                  }));
            }
        }

        //удаление файла инструкции
        public RelayCommand DeleteInstructionFileCommand
        {
            get
            {
                return deleteInstructionFileCommand ??
                  (deleteInstructionFileCommand = new RelayCommand(obj =>
                  {
                      if (System.Windows.Forms.MessageBox.Show("Файл будет удален из папки полностью. Удалить файл?", "Удаление файла документа", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                      {
                          using (var db = new archiveEntities())
                          {
                              var orgArch = db.organizationArchive.Where(x => x.organization_idOrg == idCurrentOrg).First();
                              File.Delete(orgArch.pathInstruction);
                              orgArch.pathInstruction = null;
                              db.SaveChanges();
                              ArchiveProxyModel.getInstance().emitUpdateOrganizationArchive();
                              System.Windows.Forms.MessageBox.Show("Файл Инструкции по делопроизводству был удален.");
                          }
                      }
                  }));
            }
        }

        /////////МЕРОПРИЯТИЯ
        private RelayCommand deleteEventCommand;
        List<SelectEventLogbookForOrg_Result> events;
        private SelectEventLogbookForOrg_Result selectedEvent;
        public List<event_logbook> Events { get; set; }
        public SelectEventLogbookForOrg_Result SelectedEvent
        {
            get { return selectedEvent; }
            set
            {
                selectedEvent = value;
                OnPropertyChanged("SelectedEvent");
            }
        }
        //удаление информации о мероприятии
        public RelayCommand DeleteEventCommand
        {
            get
            {
                return deleteEventCommand ??
                  (deleteEventCommand = new RelayCommand(obj =>
                  {
                      if (SelectedEvent != null)
                      {
                          int id = SelectedEvent.idEvent_logbook;
                          if (MessageBox.Show("Запись о выбранном мероприятии будет удалена. Удалить мероприятие?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                          {
                              event_logbook eventToDelete = Events.Find(x => x.idEvent_logbook == id);
                              ArchiveProxyModel.getInstance().deleteEvent(eventToDelete);
                              OnSuccess?.Invoke();
                              MessageBox.Show("Мероприятие было удалено.");
                          }
                      }
                      else MessageBox.Show("Выберите мероприятие из списка.");
                  }));
            }
        }


        ////////////////////////////////ПЕРЕИМЕНОВАНИЯ
        private RelayCommand deleteRenameCommand;
        private SelectRenameForOrg_Result selectedRename;
        public List<renamesOfOrganization> Renames { get; set; }
        public SelectRenameForOrg_Result SelectedRename
        {
            get { return selectedRename; }
            set
            {
                selectedRename = value;
                OnPropertyChanged("SelectedRename");
            }
        }
        //удаление информации переименовании
        public RelayCommand DeleteRenameCommand
        {
            get
            {
                return deleteRenameCommand ??
                  (deleteRenameCommand = new RelayCommand(obj =>
                  {
                      if (SelectedRename != null)
                      {
                          int id = SelectedRename.idNameOfOrganization;
                          if (MessageBox.Show("Запись о выбранном предыдущем названии организации будет удалена. Удалить запись?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                          {
                              renamesOfOrganization renameToDelete = Renames.Find(x => x.idNameOfOrganization == id);
                              ArchiveProxyModel.getInstance().deleteRename(renameToDelete);
                              OnSuccess?.Invoke();
                              MessageBox.Show("Запись о предыдущем названии была удалена.");
                          }
                      }
                      else MessageBox.Show("Выберите запись из списка.");
                  }));
            }
        }
    }
}
