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
        private ExpertDataModel<DefaultCell> dataModel = null; // model to store Cells
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
                dataModel = new ExpertDataModel<DefaultCell>(openDlg.FileName);
                influenceGrid.ItemsSource = dataModel.dataset.Tables[1].DefaultView;
                fullnessGrid.ItemsSource = dataModel.dataset.Tables[2].DefaultView;
                reliabilityGrid.ItemsSource = dataModel.dataset.Tables[3].DefaultView;
                timelinessGrid.ItemsSource = dataModel.dataset.Tables[4].DefaultView;
                tabControl.IsEnabled = true;
                //TODO: refresh tables if needed
            }
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
                // save generated data 
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.dataModified; // to be fixed
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

    }
}
