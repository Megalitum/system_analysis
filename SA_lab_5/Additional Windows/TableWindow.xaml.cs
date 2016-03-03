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
        private dynamic intvscell;
        private bool classification;

        public TableWindow()
        {
            InitializeComponent();
        }

        public TableWindow(dynamic obj, bool classification)
        {
            InitializeComponent();
            intvscell = obj;
            this.classification = classification;
            if (classification)
            {
                lowerBound.Text = "0.1";
            }
            else
            {
                lowerBound.Text = "0.0";
            }
            //Resources.Add("intervalSource", null);
        }

        private void UpdateDatagrid()
        {
            double eta_left = classification ? .1 : .0;
            double eta_right = Double.Parse(upperBound.SelectedValue.ToString());
            // perform calculations and data source change
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn(columnName: "test_col", dataType: typeof(string)));
            dt.Rows.Add("abracadabra");
            //Resources["intervalSource"] = dt; does not work
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDatagrid();
        }

        private void Interval_Loaded(object sender, RoutedEventArgs e)
        {
            List<String[]> tb = new List<string[]>();
            for (int i = 0; i < this.intvscell.N; i++)
            {
                String[] st = new String[this.intvscell.M];
                for (int j = 0; j < this.intvscell.M; j++)
                {
                    st[j] = this.intvscell.interval[i, j].cellIntervalToString();
                }
                tb.Add(st);
            }
            var grid = sender as DataGrid;
            grid.ItemsSource = tb;
        }
    }
}
