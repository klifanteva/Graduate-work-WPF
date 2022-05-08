using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace WPFArchive.ViewModel
{
    class ContentOfSetViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private int idSet;
        private DocumentsOfSet_Select_Result selectedDocument;
        private RelayCommand deleteDocCommand;
        private RelayCommand deleteFileDocCommand;
        private RelayCommand filterCommand;
        private RelayCommand openFileCommand;
        private RelayCommand toExcelCommand;
        private string searchByActHead;
        private DateTime startFilterDate;
        private DateTime endFilterDate;
        private Excel.Application excelapp;
        private Excel.Workbook excelapp_workbook;
        private Excel.Worksheet excelworksheet;
        private Excel.Range excelcells;

        List<DocumentsOfSet_Select_Result> _Documents;
        private string setOfDocs;
        public List<DocumentsOfSet_Select_Result> Documents
        {
            get { return _Documents; }
            private set
            {
                _Documents = value;
                OnPropertyChanged("Documents");
            }
        }
        public List<SetOfDocs_Select_Result> SetsOfDocs { get; private set; }
        public List<organization> Organizations { get; private set; }
        public DocumentsOfSet_Select_Result SelectedDocument
        {
            get { return selectedDocument; }
            set
            {
                selectedDocument = value;
                OnPropertyChanged("DocTextBox");
                OnPropertyChanged("SelectedDocument");
            }
        }
        public List<document> Docs { get; private set; }
        public string DocTextBox
        {
            get { 
                if (SelectedDocument != null) 
                { 
                    return $"''{SelectedDocument?.actHeading}'' № {SelectedDocument?.numberDoc} от {SelectedDocument?.dateDoc.Value.Day}.{SelectedDocument?.dateDoc.Value.Month}.{SelectedDocument?.dateDoc.Value.Year}"; 
                } 
                else 
                    return null; 
            }
        }
        public string NameOfSet
        {
            get { return setOfDocs; }
            set
            {
                setOfDocs = "Опись " + "№" + SetsOfDocs.Find(x => x.idSet == idSet).numberSet + " ''"
                  + SetsOfDocs.Find(x => x.idSet == idSet).nameCatOfSet + "'' от организации "
                  + SetsOfDocs.Find(y => y.idSet == idSet).nameOrg + " за " + SetsOfDocs.Find(x => x.idSet == idSet).startDate.Value.Year + " год";
            }
        }
        public string SearchByActHead { get { return searchByActHead; } set { searchByActHead = value; } }
        public DateTime StartFilterDate { get { return startFilterDate; } set { startFilterDate = value; } }
        public DateTime EndFilterDate { get { return endFilterDate; } set { endFilterDate = value; } }
        public ContentOfSetViewModel(int idSet)
        {
            this.idSet = idSet;
            using (var db = new archiveEntities())
            {
                Documents = db.DocumentsOfSet_Select(idSet).ToList();
                Docs = db.document.ToList();
                SetsOfDocs = db.SetOfDocs_Select().ToList();
                Organizations = db.organization.ToList();
            }
            //обновление таблицы с документами
            ArchiveProxyModel.getInstance().OnDocsUpdate += () =>
            {
                using (var db = new archiveEntities())
                {
                    Documents = db.DocumentsOfSet_Select(idSet).ToList();
                }
            };
            NameOfSet = "";
            if (Documents.Count > 0)
            {
                startFilterDate = (DateTime)SetsOfDocs.Find(x => x.idSet == Documents.FirstOrDefault().setOfDocs_idSet).startDate;
                endFilterDate = (DateTime)SetsOfDocs.Find(x => x.idSet == Documents.FirstOrDefault().setOfDocs_idSet).endDate;
            }
        }
        //удаление файла выбранного документа
        public RelayCommand DeleteFileDocCommand
        {
            get
            {
                return deleteDocCommand ??
                  (deleteDocCommand = new RelayCommand(obj =>
                  {
                      if (SelectedDocument != null)
                      {
                          if (SelectedDocument.filePath != null)
                          {
                              if (System.Windows.Forms.MessageBox.Show("Файл будет удален из папки полностью. Удалить файл?", "Удаление файла документа", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                              {
                                  using (var db = new archiveEntities())
                                  {
                                      var doc = db.document.Where(x => x.idDoc == SelectedDocument.idDoc).First();
                                      File.Delete(doc.filePath);
                                      doc.filePath = null;
                                      db.SaveChanges();
                                      ArchiveProxyModel.getInstance().emitUpdateDocument();
                                      System.Windows.Forms.MessageBox.Show("Файл документа был удален.");
                                  }
                              }
                          }
                          else MessageBox.Show("Для этого документа файл еще не был прикреплен.");
                      }
                      else MessageBox.Show("Выберите документ из списка.");
                  }));
            }
        }

        //удаление  выбранного документа
        public RelayCommand DeleteDocCommand
        {
            get
            {
                return deleteFileDocCommand ??
                  (deleteFileDocCommand = new RelayCommand(obj =>
                  {
                      if (SelectedDocument != null)
                      {
                          if (MessageBox.Show("Документ будет полностью удален, включая файл. Удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                          {
                              if (MessageBox.Show("Вы уверены, что хотите удалить?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                              {
                                  document docToDelete = Docs.Find(x => x.idDoc == SelectedDocument.idDoc);
                                  if (docToDelete.filePath != null) { File.Delete(docToDelete.filePath); }
                                  ArchiveProxyModel.getInstance().deleteDocument(docToDelete);
                                  MessageBox.Show("Документ был удален.");
                              }
                          }
                      }
                      else MessageBox.Show("Выберите документ из списка.");
                  }));
            }
        }
        //открытие файла  выбранного документа
        public RelayCommand OpenFileCommand
        {
            get
            {
                return openFileCommand ??
                  (openFileCommand = new RelayCommand(obj =>
                  {
                      if (SelectedDocument != null)
                      {
                          if(SelectedDocument.filePath != null)
                            Process.Start(SelectedDocument.filePath);
                          else MessageBox.Show("Для данного документа не был добавлен файл.");
                      }
                      else MessageBox.Show("Выберите документ из списка.");
                  }));
            }
        }
        //фильтры
        public RelayCommand FilterCommand
        {
            get
            {
                return filterCommand ??
                  (filterCommand = new RelayCommand(obj =>
                  {
                      CollectionViewSource.GetDefaultView(Documents).Filter = Filter;
                  }));
            }
        }
        private bool Filter(object o)
        {
            DocumentsOfSet_Select_Result doc = (o as DocumentsOfSet_Select_Result);
            var result = true;

            if (doc == null)
            {
                return false;
            }
            else if (!(doc.dateDoc >= StartFilterDate && doc.dateDoc <= EndFilterDate))
            {
                return false;
            }
            else if (SearchByActHead != null)
            {
                if (!(doc.actHeading.Contains(SearchByActHead))) return false;
            }
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
                      if (Documents.Count != 0)
                      {
                          Documents.Sort((x, y) => x.actHeading.CompareTo(y.actHeading));
                          excelapp = new Excel.Application();//Объявляем приложение
                          excelapp.SheetsInNewWorkbook = 1;// Количество листов в рабочей книге
                          excelapp_workbook = excelapp.Workbooks.Add(Type.Missing);//Добавить рабочую книгу
                          excelapp.DisplayAlerts = true;//Отключить отображение окон с сообщениями
                          excelworksheet = (Excel.Worksheet)excelapp.Worksheets.get_Item(1);//Получаем первый лист документа (счет начинается с 1)
                          excelworksheet.Columns[1].ColumnWidth = 5;
                          excelworksheet.Columns[2].ColumnWidth = 12;
                          excelworksheet.Columns[3].ColumnWidth = 45;
                          excelworksheet.Columns[4].ColumnWidth = 9;
                          excelworksheet.Columns[5].ColumnWidth = 11;
                          excelworksheet.Columns[6].ColumnWidth = 12;
                          excelworksheet.Cells.Style.WrapText = true;

                          excelworksheet.get_Range("A1", "F1").Merge();
                          excelcells = excelworksheet.get_Range("A1", Type.Missing);
                          excelcells.set_Value(Type.Missing, SetsOfDocs.Find(x => x.idSet == idSet).nameOrg);

                          excelworksheet.get_Range("A2", "F2").Merge();
                          excelcells = excelworksheet.get_Range("A2", Type.Missing);
                          excelcells.set_Value(Type.Missing, "Опись №" + SetsOfDocs.Find(x => x.idSet == idSet).numberSet + " " + SetsOfDocs.Find(x => x.idSet == idSet).nameCatOfSet + " за " + SetsOfDocs.Find(x => x.idSet == idSet).startDate.Value.Year + " год");

                          excelcells = excelworksheet.get_Range("A4", Type.Missing);
                          excelcells.set_Value(Type.Missing, "№ пп");
                          excelcells = excelworksheet.get_Range("B4", Type.Missing);
                          excelcells.set_Value(Type.Missing, "Индекс дела");
                          excelcells = excelworksheet.get_Range("C4", Type.Missing);
                          excelcells.set_Value(Type.Missing, "Заголовок дела");
                          excelcells = excelworksheet.get_Range("D4", Type.Missing);
                          excelcells.set_Value(Type.Missing, "Дата");
                          excelcells = excelworksheet.get_Range("E4", Type.Missing);
                          excelcells.set_Value(Type.Missing, "Количество листов");
                          excelcells = excelworksheet.get_Range("F4", Type.Missing);
                          excelcells.set_Value(Type.Missing, "Примечания");

                          excelcells = excelworksheet.get_Range("A5", Type.Missing);
                          excelcells.set_Value(Type.Missing, "1");
                          excelcells = excelworksheet.get_Range("B5", Type.Missing);
                          excelcells.set_Value(Type.Missing, "2");
                          excelcells = excelworksheet.get_Range("C5", Type.Missing);
                          excelcells.set_Value(Type.Missing, "3");
                          excelcells = excelworksheet.get_Range("D5", Type.Missing);
                          excelcells.set_Value(Type.Missing, "4");
                          excelcells = excelworksheet.get_Range("E5", Type.Missing);
                          excelcells.set_Value(Type.Missing, "5");
                          excelcells = excelworksheet.get_Range("F5", Type.Missing);
                          excelcells.set_Value(Type.Missing, "6");

                          int irow = 7;
                          int idoc = 1;
                          Documents.ForEach(
                                  x =>
                                  {
                                      excelcells = excelworksheet.get_Range("A" + Convert.ToString(irow), Type.Missing);
                                      excelcells.set_Value(Type.Missing, String.Format("{0}", idoc));
                                      excelcells = excelworksheet.get_Range("B" + Convert.ToString(irow), Type.Missing);
                                      excelcells.set_Value(Type.Missing, String.Format("{0}", x.actIndex));
                                      excelcells = excelworksheet.get_Range("C" + Convert.ToString(irow), Type.Missing);
                                      excelcells.set_Value(Type.Missing, String.Format("{0}", x.actHeading) + " №" + String.Format("{0}", x.numberDoc));
                                      excelcells = excelworksheet.get_Range("D" + Convert.ToString(irow), Type.Missing);
                                      excelcells.set_Value(Type.Missing, String.Format("{0}", x.dateDoc.Value.Day + "." + x.dateDoc.Value.Month + "." + x.dateDoc.Value.Year));
                                      excelcells = excelworksheet.get_Range("E" + Convert.ToString(irow), Type.Missing);
                                      excelcells.set_Value(Type.Missing, String.Format("{0}", x.sheetsCount));
                                      irow++;
                                      idoc++;
                                  });
                          excelcells = excelworksheet.get_Range("A3", "F" + Convert.ToString(irow - 1));
                          excelcells.Borders.Color = 000000;
                          excelapp.Visible = true;//Отобразить Excel
                      }
                      else MessageBox.Show("Список документов для данной описи пуст.");
                  }));
            }
        }

    }
}
