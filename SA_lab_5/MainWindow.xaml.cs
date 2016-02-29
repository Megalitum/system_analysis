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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Data;

namespace SA_lab_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private dynamic dataModel = null;
        private string structureName = "Default";
        private string filename = null;
        private bool dataModified = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog()
            {
                Filter = "Spreadsheet |*.xlsx;*.xls",
                InitialDirectory = Environment.CurrentDirectory
            };
            if (openDlg.ShowDialog() == true)
            {
                filename = openDlg.FileName;
                LoadModel();
                tabControl.IsEnabled = true;
            }
        }

        private void LoadModel()
        {
            if (filename == null)
            {
                return;
            }
            switch (structureName)
            {
                case "Default":
                    {
                        dataModel = new ExpertDataModel<DefaultCell>(filename);
                        break;
                    }
                case "Variant":
                    {
                        dataModel = new ExpertDataModel<VariantCell>(filename);
                        break;
                    }
                case "Custom":
                    {
                        dataModel = new ExpertDataModel<CustomCell>(filename);
                        break;
                    }
            }
            influenceGrid.ItemsSource = dataModel.dataset.Tables[1].DefaultView;
            fullnessGrid.ItemsSource = dataModel.dataset.Tables[2].DefaultView;
            reliabilityGrid.ItemsSource = dataModel.dataset.Tables[3].DefaultView;
            timelinessGrid.ItemsSource = dataModel.dataset.Tables[4].DefaultView;
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog()
            {
                Filter = "Text file | *.txt",
                InitialDirectory = Environment.CurrentDirectory
            };
            if (saveDlg.ShowDialog() == true)
            {
                dataModified = false;
                // save generated data 
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = dataModified; // to be fixed
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Structure_Type_Click(object sender, RoutedEventArgs e)
        {
            MenuItem s = sender as MenuItem;
            if ((string) s.Tag == structureName)
            {
                return;
            }
            foreach (var item in structureMenu.Items)
            {
                MenuItem menuitem = item as MenuItem;
                if (menuitem != null)
                {
                    menuitem.IsChecked = false;
                }
            }
            s.IsChecked = true;
            structureName = (string) s.Tag;
            LoadModel();
            e.Handled = true;
        }

        private void Grid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
                dataModified = true;
        }

        private void IntervalSearch_Click(object sender, RoutedEventArgs e)
        {
            TableWindow tblWindow = new TableWindow(false);
            tblWindow.Show();
        }

        private void Classification_Click(object sender, RoutedEventArgs e)
        {
            TableWindow tblWindow = new TableWindow(true);
            tblWindow.Show();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Window about program will appear here");
        }
    }
}
