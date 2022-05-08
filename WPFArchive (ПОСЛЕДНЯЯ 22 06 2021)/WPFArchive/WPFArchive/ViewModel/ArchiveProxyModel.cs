using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFArchive.ViewModel
{
    class ArchiveProxyModel
    {
        private static ArchiveProxyModel instance;
        private ArchiveProxyModel() {}
        public static ArchiveProxyModel getInstance()
        {
            if (instance == null) 
            {
                instance = new ArchiveProxyModel();
            }
            return instance;
        }
        public event Action OnSetOfDocsUpdate;
        public event Action OnOrganizationUpdate;
        public event Action OnOrgArchiveUpdate;
        public event Action OnEventsUpdate;
        public event Action OnRenamesUpdate;
        public event Action OnDocsUpdate;
        public event Action OnNomenclatureUpdate;

        //////////ДОБАВЛЕНИЕ
        public void addSetOfDocs(setOfDocs entity) 
        {
            using (var db = new archiveEntities()) 
            {
                db.setOfDocs.Add(entity);
                db.SaveChanges();
                OnSetOfDocsUpdate?.Invoke();
            }
        }
        public void addOrganization(organization entity)
        {
            using (var db = new archiveEntities())
            {
                db.organization.Add(entity);
                db.SaveChanges();
                OnOrganizationUpdate?.Invoke();
            }
        }
        public void addArchiveOrganization(organizationArchive entity)
        {
            using (var db = new archiveEntities())
            {
                db.organizationArchive.Add(entity);
                db.SaveChanges();
                OnOrgArchiveUpdate?.Invoke();
            }
        }
        public void addEvent(event_logbook entity)
        {
            using (var db = new archiveEntities())
            {
                db.event_logbook.Add(entity);
                db.SaveChanges();
                OnEventsUpdate?.Invoke();
            }
        }
        public void addRename(renamesOfOrganization entity)
        {
            using (var db = new archiveEntities())
            {
                db.renamesOfOrganization.Add(entity);
                db.SaveChanges();
                OnRenamesUpdate?.Invoke();
            }
        }
        public void addDocument(document entity)
        {
            using (var db = new archiveEntities())
            {
                db.document.Add(entity);
                db.SaveChanges();
                OnDocsUpdate?.Invoke();
            }
        }
        public void addNomenclature(nomenclature entity)
        {
            using (var db = new archiveEntities())
            {
                db.nomenclature.Add(entity);
                db.SaveChanges();
                OnNomenclatureUpdate?.Invoke();
            }
        }
        //////////УДАЛЕНИЕ
        public void deleteSetOfDocs(setOfDocs entity)
        {
            using (var db = new archiveEntities())
            {
                db.setOfDocs.Attach(entity);
                db.setOfDocs.Remove(entity);
                db.SaveChanges();
                OnSetOfDocsUpdate?.Invoke();
            }
        }
        public void deleteOrganization(organization entity)
        {
            using (var db = new archiveEntities())
            {
                db.organization.Attach(entity);
                db.organization.Remove(entity);
                db.SaveChanges();
                OnOrganizationUpdate?.Invoke();
                OnSetOfDocsUpdate?.Invoke();
            }
        }
        //удаления архива организации нет, потому что он удаляется вместе с организацией или можно отметить при редактировании "нет архива"
        public void deleteEvent(event_logbook entity)
        {
            using (var db = new archiveEntities())
            {
                db.event_logbook.Attach(entity);
                db.event_logbook.Remove(entity);
                db.SaveChanges();
                OnEventsUpdate?.Invoke();
            }
        }
        public void deleteRename(renamesOfOrganization entity)
        {
            using (var db = new archiveEntities())
            {
                db.renamesOfOrganization.Attach(entity);
                db.renamesOfOrganization.Remove(entity);
                db.SaveChanges();
                OnRenamesUpdate?.Invoke();
            }
        }
        public void deleteDocument(document entity)
        {
            using (var db = new archiveEntities())
            {
                db.document.Attach(entity);
                db.document.Remove(entity);
                db.SaveChanges();
                OnDocsUpdate?.Invoke();
            }
        }
        public void deleteNomenclature(nomenclature entity)
        {
            using (var db = new archiveEntities())
            {
                db.nomenclature.Attach(entity);
                db.nomenclature.Remove(entity);
                db.SaveChanges();
                OnNomenclatureUpdate?.Invoke();
            }
        }
        ////////////ОБНОВЛЕНИЕ 
        public void emitUpdateSetOfDocs()
        {
            OnSetOfDocsUpdate?.Invoke();
        }
        public void emitUpdateOrganization()
        {
            OnOrganizationUpdate?.Invoke();
        }
        public void emitUpdateOrganizationArchive()
        {
            OnOrgArchiveUpdate?.Invoke();
        }
        public void emitUpdateEvent()
        {
            OnEventsUpdate?.Invoke();
        }
        public void emitUpdateRename()
        {
            OnRenamesUpdate?.Invoke();
        }
        public void emitUpdateDocument()
        {
            OnDocsUpdate?.Invoke();
        }
    }
}
