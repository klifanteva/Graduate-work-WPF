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
    class EventViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action OnSuccess;
        private int idEvent;
        private string nameOrg;
        private int typeOfEvent_idTypeOfEvent;
        public int organization_idOrg;
        private DateTime dateOfEvent;
        private string result;
        private string responsibleOfEvent;
        private bool done;
        private RelayCommand saveEventCommand;
        public List<event_logbook> Events { get; private set; }
        public List<organization> Organizations { get; private set; }
        public List<typeOfEvent> TypesOfEvent { get; private set; }
        public int IdTypeOfEvent { get { return typeOfEvent_idTypeOfEvent; } set { typeOfEvent_idTypeOfEvent = value; } }
        public DateTime DateOfEvent { get { return dateOfEvent; } set { dateOfEvent = value; } }
        public string Result { get { return result; } set { result = value; } }
        public string ResponsibleOfEvent { get { return responsibleOfEvent; } set { responsibleOfEvent = value; } }
        public bool Done { get { return done; } set { done = value; } }
        public string NameOrg
        {
            get { return nameOrg; }
            set {
                nameOrg = Organizations.Find(x => x.idOrg == organization_idOrg).nameOrg; 
            }
        }
        public EventViewModel(int idOrg)
        {
            organization_idOrg = idOrg;
            dateOfEvent = DateTime.Now.Date;
            GetEntities();
            NameOrg="";
        }
        public EventViewModel(int idOrg, int idEvent)
        {
            this.idEvent = idEvent;
            organization_idOrg = idOrg;
            dateOfEvent = DateTime.Now.Date;
            GetEntities();
            NameOrg = "";
            using (var db = new archiveEntities())
            {
                var event1 = db.event_logbook.Where(x => x.idEvent_logbook == idEvent).First();
                ResponsibleOfEvent = event1.responsibleOfEvent;
                DateOfEvent = event1.dateOfEvent;
                Result = event1.result;
                Done = (bool)event1.done;
                IdTypeOfEvent = event1.typeOfEvent_idTypeOfEvent;
            }
        }
        async private void GetEntities()
        {
            using (var db = new archiveEntities())
            {
                TypesOfEvent = db.typeOfEvent.ToList();
                Organizations = db.organization.ToList();
                Events = db.event_logbook.ToList();
            }
        }
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        //добавление и редактирование информации о мероприятии
        public RelayCommand SaveEventCommand
        {
            get
            {
                return saveEventCommand ??
                  (saveEventCommand = new RelayCommand(obj =>
                  {
                      using (var db = new archiveEntities())
                      {
                          if (idEvent != 0) 
                          {///обновление
                              var event1 = db.event_logbook.Where(x => x.idEvent_logbook == idEvent).First();
                              event1.typeOfEvent_idTypeOfEvent = IdTypeOfEvent;
                              event1.organization_idOrg = organization_idOrg;
                              event1.dateOfEvent = DateOfEvent;
                              event1.result = Result;
                              event1.responsibleOfEvent = ResponsibleOfEvent;
                              event1.done = Done;
                              if (event1.typeOfEvent_idTypeOfEvent == 0 || event1.responsibleOfEvent == null)
                              {
                                  MessageBox.Show("Необходимо заполнить поля 'Тип мероприятия' и 'Ответственное лицо'.");
                              }
                              else if (Events.Exists(x => x.organization_idOrg == organization_idOrg && x.dateOfEvent == DateOfEvent && x.typeOfEvent_idTypeOfEvent == IdTypeOfEvent && x.idEvent_logbook != idEvent) == true)
                              {
                                  MessageBox.Show("Информация о таком мероприятии уже добавлена.");
                              }
                              else
                              {
                                  db.SaveChanges();
                                  ArchiveProxyModel.getInstance().emitUpdateEvent();
                                  MessageBox.Show("Информация о мероприятии успешно отредактирована");
                                  OnSuccess?.Invoke(); //закрытие формы добавления
                              } 
                          }
                          else { ////добавление
                              event_logbook newEvent = new event_logbook
                              {
                                  typeOfEvent_idTypeOfEvent = IdTypeOfEvent,
                                  organization_idOrg = organization_idOrg,
                                  dateOfEvent = DateOfEvent,
                                  result = Result,
                                  responsibleOfEvent = ResponsibleOfEvent,
                                  done = Done
                              };
                              if (newEvent.typeOfEvent_idTypeOfEvent == 0 || newEvent.responsibleOfEvent == null)
                              {
                                  MessageBox.Show("Необходимо заполнить поля 'Тип мероприятия' и 'Ответственное лицо'.");
                              }
                              else if (Events.Exists(x => x.organization_idOrg == organization_idOrg && x.dateOfEvent == DateOfEvent && x.typeOfEvent_idTypeOfEvent == IdTypeOfEvent) == true)
                              {
                                  MessageBox.Show("Информация о таком мероприятии уже добавлена.");
                              }
                              else
                              {
                                  ArchiveProxyModel.getInstance().addEvent(newEvent);
                                  MessageBox.Show("Информация о мероприятии успешно добавлена.");
                                  OnSuccess?.Invoke(); //закрытие формы добавления
                              }
                          }
                      }
                  }));
            }
        }
    }
}
