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

using System.Data;

namespace SA_lab_5.Additional_Windows
{
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        public dynamic Intvscell { get; private set; }
        public bool Classification
        {
            get; private set;
        }
        private DataTable intervalSource { get; set; }

        public TableWindow()
        {
            InitializeComponent();
        }

        public TableWindow(dynamic obj, bool classification)
        {
            Intvscell = obj;
            InitializeComponent();
            this.Classification = classification;
            if (classification)
            {
                lowerBound.Text = "0.1";
            }
            else
            {
                lowerBound.Text = "0.0";
            }
            intervalTable.ItemsSource = intervalSource.DefaultView;
        }

        private DataTable GenerateTable()
        {
            DataTable dt = new DataTable();
            foreach (var column in Intvscell.columns)
            {
                dt.Columns.Add(column, typeof(string));
            }
            for (int i = 0; i < Intvscell.N; i++)
            {
                dt.Rows.Add();
            }
            return dt;
        }

        private void UpdateDatagrid()
        {
            if (intervalSource == null)
            {
                intervalSource = GenerateTable();
            }
            double eta_left = Classification ? .1 : .0;
            double eta_right = Double.Parse(upperBound.SelectedValue.ToString());
            // perform calculations and data source change
            // exampli gratia
            intervalSource.Rows[0][0] = $"{new Random().Next()}";
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDatagrid();
        }

        private void Interval_Loaded(object sender, RoutedEventArgs e)
        {
            List<String[]> tb = new List<string[]>();
            for (int i = 0; i < this.Intvscell.N; i++)
            {
                String[] st = new String[this.Intvscell.M];
                for (int j = 0; j < this.Intvscell.M; j++)
                {
                    st[j] = this.Intvscell.interval[i, j].cellIntervalToString();
                }
                tb.Add(st);
            }
            var grid = sender as DataGrid;
            grid.ItemsSource = tb;
        }

        private void intervalTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var selected = dataGrid.SelectedCells.First();
        }
    }
}
