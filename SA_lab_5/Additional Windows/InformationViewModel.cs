using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OxyPlot;
using OxyPlot.Series;
using SA_lab_5.Cell_Logic;

namespace SA_lab_5.Additional_Windows
{
    public class InformationViewModel
    {
        public PlotModel FullnessModel { get; private set; }
        public PlotModel ReliabilityModel { get; private set; }
        public PlotModel TimelinessModel { get; private set; }
        public InformationViewModel(BaseCell cell)
        {
            this.FullnessModel = new PlotModel { Title = "Example 1" };
            this.FullnessModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            this.ReliabilityModel = new PlotModel { Title = "Example 2" };
            this.ReliabilityModel.Series.Add(new FunctionSeries(Math.Sin, 0, 10, 0.1, "sin(x)"));
            this.TimelinessModel = new PlotModel { Title = "Example 3" };
            this.TimelinessModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
        }

    }
}
