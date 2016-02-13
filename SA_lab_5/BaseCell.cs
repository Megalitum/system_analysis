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
        double Fullness { get; } // returns delegate that calculates fullness
        double Reliability { get; } // returns delegate that calculates reliability
        double Timeliness { get; } // returns delegate that calculates timeliness
        DoubleInterval[] FindTimeInterval(double lowerbound, double upperbound);
    }

    abstract class BaseCell : IBaseCell
    {
        private double fullness_expert;
        private double reliablility_expert;
        private double timeliness_expert;
        private double alpha_expert;
        private double beta_expert;
        private double gamma_expert;
        public double Alpha { get; protected set; }
        public double Beta { get; protected set; }
        public double Gamma { get; protected set; }

        public BaseCell(double fe, double re, double te, double ae, double be, double ge)
        {
            this.fullness_expert = fe;
            this.reliablility_expert = re;
            this.timeliness_expert = te;
            this.alpha_expert = ae;
            this.beta_expert = be;
            this.gamma_expert = ge;
            CalculateCoefficients();
        }
        protected abstract void CalculateCoefficients();

        public abstract DoubleInterval[] FindTimeInterval(double lowerbound, double upperbound);
        public abstract double Fullness { get; }

        public abstract double Reliability { get; }

        public abstract double Timeliness { get; }
    }
}
