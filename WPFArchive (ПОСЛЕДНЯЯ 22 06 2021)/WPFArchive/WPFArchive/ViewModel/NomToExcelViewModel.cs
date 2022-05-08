using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace WPFArchive.ViewModel
{
    class NomToExcelViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<organization> org;
        List<Nomenclature_ForOrgAndYear_Result> nomenclatures;
        public int idOrg;
        public int yearNom;
        private RelayCommand toExcelCommand;
        private Excel.Application excelapp;
        private Excel.Workbook excelapp_workbook;
        private Excel.Worksheet excelworksheet;
        private Excel.Range excelcells;
        public event System.Action OnSuccess;
        public List<organization> Organizations { get { return org; } private set { org = value; } }
        public int IdOrg { get { return idOrg; } set { idOrg = value; } }
        public int YearNom { get { return yearNom; } set { yearNom = value; } }
        public List<Nomenclature_ForOrgAndYear_Result> Nomenclatures { get { return nomenclatures; } private set { nomenclatures = value; } }
        public NomToExcelViewModel()
        {
            GetEntities();
        }
        async private void GetEntities()
        {
            using (var db = new archiveEntities())
            {
                Organizations = db.organization.ToList();
            }
        }
        //выгрузка
        public RelayCommand ToExcelCommand
        {
            get
            {
                return toExcelCommand ??
                  (toExcelCommand = new RelayCommand(obj =>
                  {
                      if (IdOrg != 0 && YearNom != 0) 
                      {
                          using (var db = new archiveEntities())
                          {
                              Nomenclatures = db.Nomenclature_ForOrgAndYear(IdOrg, YearNom).ToList();
                              Nomenclatures.Sort((x, y) => x.actIndex.CompareTo(y.actIndex));
                          }
                          if (Nomenclatures.Count != 0)
                          {
                              excelapp = new Excel.Application();//Объявляем приложение
                              excelapp.SheetsInNewWorkbook = 1;// Количество листов в рабочей книге
                              excelapp_workbook = excelapp.Workbooks.Add(Type.Missing);//Добавить рабочую книгу
                              excelapp.DisplayAlerts = true;//Отключить отображение окон с сообщениями
                              excelworksheet = (Excel.Worksheet)excelapp.Worksheets.get_Item(1);//Получаем первый лист документа (счет начинается с 1)
                              excelworksheet.Columns[1].ColumnWidth = 12;
                              excelworksheet.Columns[2].ColumnWidth = 50;
                              excelworksheet.Columns[3].ColumnWidth = 15;
                              excelworksheet.Columns[4].ColumnWidth = 16;
                              excelworksheet.Columns[5].ColumnWidth = 20;
                              excelworksheet.Cells.Style.WrapText = true;
                              excelworksheet.get_Range("A1", "E1").Merge();
                              excelcells = excelworksheet.get_Range("A1", Type.Missing);
                              excelcells.set_Value(Type.Missing, Organizations.Find(y => y.idOrg == Nomenclatures.First(x => x.organization_idOrg == IdOrg).organization_idOrg).nameOrg);
                              excelworksheet.get_Range("A2", "E2").Merge();
                              excelcells = excelworksheet.get_Range("A2", Type.Missing);
                              excelcells.set_Value(Type.Missing, "Номенклатура дел на " + YearNom + "год");
                              excelcells = excelworksheet.get_Range("A4", Type.Missing);
                              excelcells.set_Value(Type.Missing, "Индекс дела");
                              excelcells = excelworksheet.get_Range("B4", Type.Missing);
                              excelcells.set_Value(Type.Missing, "Заголовок дела");
                              excelcells = excelworksheet.get_Range("C4", Type.Missing);
                              excelcells.set_Value(Type.Missing, "Количество дел");
                              excelcells = excelworksheet.get_Range("D4", Type.Missing);
                              excelcells.set_Value(Type.Missing, "Сроки хранения дел, номер статьи по перечню");
                              excelcells = excelworksheet.get_Range("E4", Type.Missing);
                              excelcells.set_Value(Type.Missing, "Примечание");
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
                              excelapp.Visible = true;//Отобразить Excel
                              int irow = 7;
                              Nomenclatures.ForEach(
                                  x => {
                                      excelcells = excelworksheet.get_Range("A" + Convert.ToString(irow), Type.Missing);
                                      excelcells.set_Value(Type.Missing, String.Format("{0}", x.actIndex));
                                      excelcells = excelworksheet.get_Range("B" + Convert.ToString(irow), Type.Missing);
                                      excelcells.set_Value(Type.Missing, String.Format("{0}", x.actHeading));
                                      irow++;
                                  });

                              excelcells = excelworksheet.get_Range("A4", "E" + Convert.ToString(irow));
                              excelcells.Borders.Color = 000000;
                              //excelapp_workbook.SaveAs(filePath);
                              //MessageBox.Show("Файл сохранен."); 
                              OnSuccess?.Invoke(); //закрытие формы добавления
                          }
                          else MessageBox.Show("Номенклатура для выбранных вами года и ИК не найдена");
                      }
                      else MessageBox.Show("Введите год и выберите ИК");
                  }));
            }
        }

    }
}
