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
    public enum SituationClass
    {
        //
        Dangerous = 1,
        Critical = 2,
        Regular = 3
    };  
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
        private dynamic intvscell;
        public DataTable ClassificationMatrix { get; private set; }
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
            intvscell = obj;
            this.Classification = classification;
            InitializeComponent();
            if (classification)
            {
                lowerBound.Text = "0.1";
                Title = "Классификация ситуаций";
            }
            else
            {
                lowerBound.Text = "0.0";
                Title = "Вычисление интервалов принятия решения";
            }
            intervalTable.ItemsSource = intervalSource.DefaultView;
        }

        private DataTable GenerateTable<T>()
        {
            DataTable dt = new DataTable();
            if (Classification) dt.Columns.Add("S", typeof(T));
            foreach (var column in intvscell.columns)
            {
                dt.Columns.Add(column, typeof(T));
            }
            for (int i = 0; i < intvscell.N; i++)
            {
                dt.Rows.Add();
            }
            return dt;
        }

        private void UpdateDatagrid()
        {
            if (intervalSource == null)
            {
                intervalSource = GenerateTable<string>();
                ClassificationMatrix = Classification ? GenerateTable<SituationClass>() : null;
            }
            double eta_left = Classification ? .1 : .0;
            double eta_right = Double.Parse(upperBound.SelectedValue.ToString());
            // perform calculations and data source change
            // exampli gratia
            intvscell.n_left = eta_left;
            intvscell.n_right = eta_right;
            intvscell.FindInterval();
            if (!Classification)
            {
                for (int i = 0; i < intervalSource.Rows.Count; i++)
                {
                    for (int j = 0; j < intervalSource.Columns.Count; j++)
                    {
                        intervalSource.Rows[i][j] = intvscell.cellIntervalToString(intvscell.interval[i, j]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < intervalSource.Rows.Count; i++)
                {
                    for (int j = 1; j < intervalSource.Columns.Count; j++)
                    {
                        intervalSource.Rows[i][j] = intvscell.cellIntervalToString(intvscell.interval[i, j-1]);
                    }
                }

                List<string> hint;
                var interval = intvscell.unioninterval();
                hint = intvscell.uninterval_tostring(interval);
                for (int i = 0; i < intervalSource.Rows.Count; i++) intervalSource.Rows[i][0] = hint[i];
                List<int> level = intvscell.determine_class(interval);
                //intervalSource.Rows[0][0] = $"{new Random().Next()}";
                //intervalSource.Rows[0][1] = $"{new Random().Next()}";
                //intervalSource.Rows[0][2] = $"{new Random().Next()}";

                // determine class and update corresponing table
                // exampli gratia (all fields must be filled)
                var rndGenerator = new Random();
                for (int i = 0; i < ClassificationMatrix.Rows.Count; i++)
                {
                    var rowIndex = rndGenerator.Next() % 3 + 1;
                    for (int j = 0; j < ClassificationMatrix.Columns.Count; j++)
                    {
                        ClassificationMatrix.Rows[i][j] = rowIndex;
                    }
                }
                //ClassificationMatrix.Rows[0][0] = SituationClass.Regular;
                //ClassificationMatrix.Rows[0][1] = SituationClass.Critical;
                //ClassificationMatrix.Rows[0][2] = SituationClass.Dangerous;
            }
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

        private void intervalTable_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var row = (dataGrid.CurrentItem as DataRowView).Row;
            var column = dataGrid.CurrentColumn;
            int rowId = intervalSource.Rows.IndexOf(row);
            int columnId = column.DisplayIndex;
            var cell = intvscell.cell[rowId, columnId];
            if (cell != null)
            {
                (new InformationFunctionPlot(cell, rowId, columnId) { Owner = this }).Show();
            }
        }
    }
}
