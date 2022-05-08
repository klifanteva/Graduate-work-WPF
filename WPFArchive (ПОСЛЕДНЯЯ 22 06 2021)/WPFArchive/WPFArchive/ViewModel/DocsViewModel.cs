using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace WPFArchive.ViewModel
{
    class DocsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private int idSet;
        private int idDoc;
        private int idDocType;
        private DateTime dateDoc;
        private int idNom;
        private int numberDoc;
        private string setOfDocs;
        private RelayCommand saveCommand;
        private RelayCommand saveFileCommand;
        private RelayCommand deleteFileCommand;
        
        public event Action OnSuccess;
        public List<Nomenclature_ForOrgAndYear_Result> nom;
        private int sheetsCount;
        public int DocType
        {
            get { return idDocType; }
            set { idDocType = value; }
        }
        public List<documentType> DocTypes { get; private set; }
        public DateTime DateDoc
        {
            get { return dateDoc; }
            set 
            { 
                dateDoc = value;
                OnPropertyChanged("Nomenclatures");
            }
        }
        public int Nomenclature {  get { return idNom; } set { idNom = value; } }
        public List<Nomenclature_ForOrgAndYear_Result> Nomenclatures
        {
            get { return nom; }
            private set 
            { 
                nom = value;
            }
        }
        public int NumberDoc { get { return numberDoc; } set { numberDoc = value; } }
        public int SheetsCount { get { return sheetsCount; } set { sheetsCount = value; } }
        public string SetOfDocs
        {
            get { return setOfDocs; }
            set {
                setOfDocs = "Опись " + "№" + SetsOfDocs.Find(x => x.idSet == idSet).numberSet + " ''"
                  + SetsOfDocs.Find(x => x.idSet == idSet).nameOfSet + "'' от организации "
                  + Organizations.Find(x => x.idOrg == (SetsOfDocs.Find(y => y.idSet == idSet).organization_idOrg)).nameOrg;
            }
        }
        public List<setOfDocs> SetsOfDocs { get; private set; }
        public List<organization> Organizations { get; private set; }
        public List<document> Documents { get; private set; }
        public List<DocumentsOfSet_Select_Result> Docs { get; private set; }

        public DocsViewModel(int idSet)
        {
            this.idSet = idSet;
            GetEntities();
            SetOfDocs = "";
            //обновление таблицы с документами
            ArchiveProxyModel.getInstance().OnDocsUpdate += () =>
            {
                using (var db = new archiveEntities())
                {
                    Docs = db.DocumentsOfSet_Select(idSet).ToList();
                }
            };
        }
        public DocsViewModel(int idDoc, int idSet)
        {
            this.idDoc = idDoc;
            this.idSet = idSet;
            GetEntities();
            SetOfDocs = "";
            //обновление таблицы с документами
            ArchiveProxyModel.getInstance().OnDocsUpdate += () =>
            {
                using (var db = new archiveEntities())
                {
                    Docs = db.DocumentsOfSet_Select(idSet).ToList();
                }
            };
            using (var db = new archiveEntities())
            {
                var doc = db.document.Where(x => x.idDoc == idDoc).First();
                DocType = doc.documentType_idDocType;
                Nomenclature = doc.nomenclature_idNom;
                DateDoc = (DateTime)doc.dateDoc;
                NumberDoc = (int)doc.numberDoc;
                if (doc.sheetsCount!=0) SheetsCount = (int)doc.sheetsCount;
            }
        }
        async private void GetEntities()
        {
            using (var db = new archiveEntities())
            {
                DocTypes = db.documentType.ToList();
                SetsOfDocs = db.setOfDocs.ToList();
                dateDoc = (DateTime)SetsOfDocs.Find(x => x.idSet == idSet).startDate;
                Organizations = db.organization.ToList();
                int idOrg = Organizations.Find(x => x.idOrg == (SetsOfDocs.Find(y => y.idSet == idSet).organization_idOrg)).idOrg;
                Documents = db.document.ToList();
                Nomenclatures = db.Nomenclature_ForOrgAndYear(idOrg, dateDoc.Year).ToList();
            }
        }
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand((o) =>
                    {
                        using (var db = new archiveEntities())
                        {
                            if (idDoc != 0) ////обновление
                            {
                                var doc = db.document.Where(x => x.idDoc == idDoc).First();
                                doc.documentType_idDocType = DocType;
                                doc.setOfDocs_idSet = idSet;
                                doc.nomenclature_idNom = Nomenclature;
                                doc.dateDoc = DateDoc;
                                doc.numberDoc = NumberDoc;
                                doc.sheetsCount = SheetsCount;
                                if (doc.nomenclature_idNom == 0 ||
                                    doc.documentType_idDocType == 0 || doc.dateDoc == null ||
                                    doc.numberDoc == null)
                                {
                                    System.Windows.Forms.MessageBox.Show("Необходимо заполнить все поля");
                                }
                                else if (Documents.Exists(x => x.numberDoc == doc.numberDoc && x.setOfDocs_idSet == doc.setOfDocs_idSet && x.idDoc != idDoc && x.nomenclature_idNom == doc.nomenclature_idNom) == true)
                                {
                                    System.Windows.Forms.MessageBox.Show("Документ с номером " + doc.numberDoc + " и номенклатурой " + Docs.Find(x => x.idNom == Nomenclature).actIndex + " уже добавлен в текущую опись.");
                                }
                                else if (Documents.Exists(x => (x.nomenclature_idNom == doc.nomenclature_idNom && x.documentType_idDocType == doc.documentType_idDocType && x.setOfDocs_idSet == doc.setOfDocs_idSet && x.dateDoc == doc.dateDoc) && x.idDoc != idDoc && x.numberDoc != doc.numberDoc) == true)
                                {
                                    System.Windows.Forms.MessageBox.Show("Такой документ уже добавлен в текущую опись.");
                                }
                                else
                                {
                                    db.SaveChanges();
                                    ArchiveProxyModel.getInstance().emitUpdateDocument();
                                    System.Windows.Forms.MessageBox.Show("Информация о документе успешно отредактирована");
                                    OnSuccess?.Invoke(); //закрытие формы добавления
                                }
                            }
                            else
                            {////добавление
                                document newDoc = new document
                                {
                                    documentType_idDocType = DocType,
                                    setOfDocs_idSet = idSet,
                                    nomenclature_idNom = Nomenclature,
                                    dateDoc = DateDoc,
                                    numberDoc = NumberDoc,
                                    sheetsCount = SheetsCount
                                };
                                if (newDoc.nomenclature_idNom == 0 ||
                                    newDoc.documentType_idDocType == 0 || newDoc.dateDoc == null ||
                                    newDoc.numberDoc == null)
                                {
                                    System.Windows.Forms.MessageBox.Show("Необходимо заполнить все поля");
                                }
                                else if (Documents.Exists(x => x.numberDoc == newDoc.numberDoc && x.setOfDocs_idSet == newDoc.setOfDocs_idSet && x.nomenclature_idNom == newDoc.nomenclature_idNom) == true)
                                {
                                    System.Windows.Forms.MessageBox.Show("Документ с номером " + newDoc.numberDoc + " и номенклатурой " + Docs.Find(x => x.idNom == Nomenclature).actIndex + " уже добавлен в текущую опись.");
                                }
                                else if (Documents.Exists(x => x.numberDoc == newDoc.numberDoc && x.nomenclature_idNom == newDoc.nomenclature_idNom && x.documentType_idDocType == newDoc.documentType_idDocType && x.setOfDocs_idSet == newDoc.setOfDocs_idSet && x.dateDoc == newDoc.dateDoc) == true)
                                {
                                    System.Windows.Forms.MessageBox.Show("Такой документ уже добавлен в текущую опись.");
                                }
                                else
                                {
                                    ArchiveProxyModel.getInstance().addDocument(newDoc);
                                    OnSuccess?.Invoke(); //закрытие формы добавления
                                    System.Windows.Forms.MessageBox.Show("Документ успешно добавлен.");
                                }
                            }
                        }
                    }));
            }
        }

        /// прикрепление файла
        public RelayCommand SaveFileCommand
        {
            get
            {
                return saveFileCommand ??
                    (saveFileCommand = new RelayCommand((o) =>
                    {
                        OpenFileDialog saveFileDialog = new OpenFileDialog();
                        saveFileDialog.FileName = "путь к файлу, который надо прикрепить";
                        saveFileDialog.Title = "Выберите файл";
                        if (idDoc != 0)
                        {
                            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                using (var db = new archiveEntities())
                                {
                                    string sourceFilename = saveFileDialog.FileName; //директория выбранного файла
                                    string extension = Path.GetExtension(sourceFilename); //расширение файла
                                    string destFilename;

                                    List<categoryOfSet> Categories = db.categoryOfSet.ToList();
                                    //путь (только каталоги) куда поместить выбранный файл
                                    destFilename = "D:/Архив/" + Organizations.Find(x => x.idOrg == SetsOfDocs.Find(y => y.idSet == idSet).organization_idOrg).idOrg
                                    + "/" + DateDoc.Year
                                + "/" + SetsOfDocs.Find(y => y.idSet == idSet).numberSet + "_" + Categories.Find(x => x.idCatOfSet == SetsOfDocs.Find(y => y.idSet == idSet).categoryOfSet_idCatOfSet).nameCatOfSet;

                                    Directory.CreateDirectory(destFilename);//создание каталогов, если их нет
                                    destFilename += "/" + NumberDoc + "_" + Nomenclatures.Find(x => x.idNom == Nomenclature).actIndex + extension;//директория файла для перемещения в нее
                                    try
                                    {
                                        File.Copy(sourceFilename, destFilename);
                                        var doc = db.document.Where(x => x.idDoc == idDoc).First();
                                        doc.filePath = destFilename;
                                        db.SaveChanges();
                                        ArchiveProxyModel.getInstance().emitUpdateDocument();
                                        System.Windows.Forms.MessageBox.Show("Файл документа был успешно добавлен.");
                                    }
                                    catch (IOException e)
                                    {
                                        if (System.Windows.Forms.MessageBox.Show("Файл для этого документа уже прикреплен. Вы хотите прикрепить новый?", "Прикрепление файла к документу", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        { 
                                            File.Copy(sourceFilename, destFilename, true);
                                            var doc = db.document.Where(x => x.idDoc == idDoc).First();
                                            doc.filePath = destFilename;
                                            db.SaveChanges();
                                            ArchiveProxyModel.getInstance().emitUpdateDocument();
                                            System.Windows.Forms.MessageBox.Show("Файл документа был успешно заменен новым."); }
                                        else
                                            try { File.Copy(sourceFilename, destFilename, false); }
                                            catch (IOException ioe) { System.Windows.Forms.MessageBox.Show("Файл документа не был изменен."); }
                                    }
                                }
                            }
                        } else System.Windows.Forms.MessageBox.Show("Сначала добавьте информацию о документе и сохраните её.");
                    }));    
            }
        }
        /// удаление файла
        public RelayCommand DeleteFileCommand
        {
            get
            {
                return deleteFileCommand ??
                    (deleteFileCommand = new RelayCommand((o) =>
                    {
                        if (System.Windows.Forms.MessageBox.Show("Файл будет удален из папки полностью. Удалить файл?", "Удаление файла документа", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            using (var db = new archiveEntities())
                            {
                                var doc = db.document.Where(x => x.idDoc == idDoc).First();
                                File.Delete(doc.filePath);
                                doc.filePath = null;
                                db.SaveChanges();
                                ArchiveProxyModel.getInstance().emitUpdateDocument();
                                System.Windows.Forms.MessageBox.Show("Файл документа был удален.");
                            }
                        }
                    }));
            }
        }
    }
} 
