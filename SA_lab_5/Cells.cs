using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_lab_5
{
    class DefaultCell : BaseCell
    {
        public DefaultCell(double fe, double re, double te, double ae, double be, double ge) : base(fe, re, te, ae, be, ge) { }
        public override double Fullness
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override double Reliability
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override double Timeliness
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override DoubleInterval[] FindTimeInterval(double lowerbound, double upperbound)
        {
            throw new NotImplementedException();
        }

        protected override void CalculateCoefficients()
        {
            throw new NotImplementedException();
        }
    }
    class VariantCell : BaseCell
    {
        public VariantCell(double fe, double re, double te, double ae, double be, double ge) : base(fe, re, te, ae, be, ge) { }
        public override double Fullness
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override double Reliability
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override double Timeliness
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override DoubleInterval[] FindTimeInterval(double lowerbound, double upperbound)
        {
            throw new NotImplementedException();
        }

        protected override void CalculateCoefficients()
        {
            throw new NotImplementedException();
        }
    }

    class CustomCell : BaseCell
    {
        public CustomCell(double fe, double re, double te, double ae, double be, double ge) : base(fe, re, te, ae, be, ge) { }
        public override double Fullness
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override double Reliability
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override double Timeliness
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override DoubleInterval[] FindTimeInterval(double lowerbound, double upperbound)
        {
            throw new NotImplementedException();
        }

        protected override void CalculateCoefficients()
        {
            throw new NotImplementedException();
        }
    }
}
