using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using SA_lab_5.Cell_Logic;

namespace SA_lab_5.Additional_Windows
{
    public class InformationViewModel
    {
        public PlotModel FullnessModel { get; private set; }
        public PlotModel ReliabilityModel { get; private set; }
        public PlotModel TimelinessModel { get; private set; }
        private double DetermineLimit(Func<double, double> f, double target, double step, double eps = 1E-6)
        {
            double x = 0;
            while(Math.Abs(f(x) - target) >= eps)
            {
                x += step;
            }
            return x * 1.2;
        }
        public InformationViewModel(BaseCell cell)
        {
            FullnessModel = new PlotModel { Title = "Полнота" };
            var fullnessFunc = cell.Fullness;
            FullnessModel.Series.Add(new FunctionSeries(fullnessFunc, 0, DetermineLimit(fullnessFunc, 1, 0.1, 5), 0.01)
            { Color = OxyColors.Red });
            FullnessModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -0.1, Maximum = 1.1 });
            FullnessModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "t" });

            this.ReliabilityModel = new PlotModel { Title = "Достоверность" };
            var reliabilityFunc = cell.Reliability;
            this.ReliabilityModel.Series.Add(new FunctionSeries(reliabilityFunc, 0, DetermineLimit(reliabilityFunc, 1, 0.1), 0.01)
            { Color = OxyColors.Blue });
            ReliabilityModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -0.1, Maximum = 1.1 });
            ReliabilityModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "t" });

            this.TimelinessModel = new PlotModel { Title = "Своевременность" };
            var timelinessFunc = cell.Timeliness;
            this.TimelinessModel.Series.Add(new FunctionSeries(timelinessFunc, 0, DetermineLimit(timelinessFunc, 0, 0.1), 0.1)
            { Color = OxyColors.Magenta });
            TimelinessModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -0.1, Maximum = 1.1 });
            TimelinessModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "t" });
        }

    }
}
