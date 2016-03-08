using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Data;

using SA_lab_5.Additional_Windows;
using System.Windows;

namespace SA_lab_5.Converters
{
    class SituationBackgroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var row = values[0] as DataRow;
            var dtgrdIS = (values[1] as DataGrid).ItemsSource as DataView;
            int rowId = (dtgrdIS.Table).Rows.IndexOf(row);
            int columnId = (values[2] as DataGridCell).Column.DisplayIndex;
            DataTable classification = values[3] as DataTable;
            var situation = classification.Rows[rowId][columnId];
            if (situation == DBNull.Value)
            {
                return new SolidColorBrush(Colors.Gray);
            }
            switch ((SituationClass)situation)
            {
                case SituationClass.Regular: return new SolidColorBrush(Colors.YellowGreen);
                case SituationClass.Critical: return new SolidColorBrush(Colors.Orange);
                case SituationClass.Dangerous: return new SolidColorBrush(Colors.Red);

            }
            return new SolidColorBrush(Colors.Yellow);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
