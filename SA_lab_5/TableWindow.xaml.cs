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

namespace SA_lab_5
{
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {
       // public IntervalsSSCells<T> intvscell; 
        private dynamic intvscell;
        public bool isInervalsShow { get; set; }

        public TableWindow()
        {
            InitializeComponent();
        }

        //public TableWindow(bool classification)
        //{
        //    InitializeComponent();
        //}

        public TableWindow(dynamic obj)
        {
            InitializeComponent();
            intvscell = obj;
            isInervalsShow = false;
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> data = new List<string>();
            data.Add("Book");
            data.Add("Computer");
            data.Add("Chair");
            data.Add("Mug");

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

            // ... Make the first item selected.
            comboBox.SelectedIndex = 0;
            //List<String> cb = new List<String>{"0.1", "0.2", "0.3", "0.4", "0.5", "0.6", "0.7", "0.8", "0.9"};
            //var comboBox = sender as ComboBox;
            //comboBox.ItemsSource = cb;
            //comboBox.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isInervalsShow = false;
            var cb = sender as ComboBox;
            double n = (double)cb.SelectedValue;
            intvscell.n_acces = n;
            this.intvscell.FindInterval();
            isInervalsShow = true;
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
