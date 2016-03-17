using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_lab_5.Cell_Logic
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

    public abstract class BaseCell : IBaseCell
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

        public List<Tuple<double, double>> FindTimeInterval(double lowerbound, double upperbound)
        {
            int b = 1000;
            Func<double, double> nij = (double t) => 1 - Math.Log(1 + this.Alpha * Fullness(t) * Reliability(t) * Timeliness(t), 2);
            FuncObj Br_method = new FuncObj(nij, 0, b, lowerbound, upperbound);
            Br_method.FindPoints();
            Br_method.FindRoot();
            return Br_method.root;
        }

        public abstract Func<double, double> Fullness { get; }

        public abstract Func<double, double> Reliability { get; }

        public abstract Func<double, double> Timeliness { get; }
    }
}