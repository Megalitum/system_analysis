using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_lab_5
{
    struct DoubleInterval
    {
        public DoubleInterval(double left, double right) : this()
        {
            Left = left;
            Right = right;
        }
        public double Left { get; private set; }
        public double Right { get; private set; }
    }
    interface IBaseCell
    {
        Func<double, double> Fullness { get; } // returns delegate that calculates fullness
        Func<double, double> Reliability { get; } // returns delegate that calculates reliability
        Func<double, double> Timeliness { get; } // returns delegate that calculates timeliness
        List<Tuple<double, double>> FindTimeInterval(double lowerbound, double upperbound);
    }

    abstract class BaseCell : IBaseCell
    {
        protected double fullness_expert;
        protected double reliability_expert;
        protected double timeliness_expert;
        protected double alpha_expert;
        public double Alpha { get; protected set; }
        public double Beta { get; protected set; }
        public double Gamma { get; protected set; }

        public BaseCell(double fe, double re, double te, double ae)
        {
            this.fullness_expert = fe;
            this.reliability_expert = re;
            this.timeliness_expert = te;
            this.alpha_expert = ae;
            CalculateCoefficients();
        }
        protected abstract void CalculateCoefficients();

        public abstract List<Tuple<double, double>> FindTimeInterval(double lowerbound, double upperbound);

        public abstract Func<double, double> Fullness { get; }

        public abstract Func<double, double> Reliability { get; }

        public abstract Func<double, double> Timeliness { get; }
    }
}