using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFArchive.ViewModel;

namespace WPFArchive.View
{
    /// <summary>
    /// Логика взаимодействия для DocsWindow.xaml
    /// </summary>
    public partial class DocsWindow : Window
    {
        int idSet;
        public int idDoc;
        public DocsWindow(int idSet)
        {
            InitializeComponent();
            this.idSet = idSet;
            var viewModel = new DocsViewModel(idSet);
            viewModel.OnSuccess += () =>
            {
                this.DialogResult = true;
            };
            DataContext = viewModel;
        }

        public DocsWindow(int idDoc, int idSet)
        {
            this.idDoc = idDoc;
            this.idSet = idSet;
            InitializeComponent();
            var viewModel = new DocsViewModel(idDoc, idSet);
            viewModel.OnSuccess += () =>
            {
                this.DialogResult = true;
            };
            DataContext = viewModel;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            using (var db = new archiveEntities())
            {
                List<DocumentsOfSet_Select_Result> Docs = db.DocumentsOfSet_Select(idSet).ToList();
                if (Docs.Exists(x => x.idDoc == idDoc))
                {
                    if (Docs.Find(x => x.idDoc == idDoc).filePath != null)
                    {
                        deleteFileButton.Visibility = Visibility.Visible;
                        saveFileButton.Text = "Заменить прикрепленный файл";
                        fileExsists.Text = "файл для этого документа уже прикреплен. Вы можете заменить его";
                    }
                    else
                    {
                        deleteFileButton.Visibility = Visibility.Hidden;
                        saveFileButton.Text = "Прикрепить новый файл";
                        fileExsists.Text = "для этого документа еще не был добавлен файл";
                    }
                }
            }
        }
    }
}
