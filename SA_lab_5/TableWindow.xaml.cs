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
        private dynamic dataModel;
        public TableWindow()
        {
            InitializeComponent();
        }

        public TableWindow(bool classification, object dataModel)
        {
            InitializeComponent();
            this.dataModel = dataModel;

        }
    }
}
