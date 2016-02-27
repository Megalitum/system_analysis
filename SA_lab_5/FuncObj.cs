using System;
using System.Collections.Generic;
using MathNet.Numerics.RootFinding;
using MathNet.Numerics;

public class FuncObj
{
    Func<double, double> f;
    double a { get; set; }
    double b { get; set; }
    double lb { get; set; } //lower bound in constraints
    double ub { get; set; } //uper ...
    Func<double, double> fa;
    Func<double, double> fb;
    double eps { get; set; }
    int maxiter { get; set; }
    List<double> point;
    List<Tuple<double, double>> root;

    public FuncObj(Func<double, double> f, double a, double b, double lb, double ub, double eps, int maxiter = 100)
    {
        this.f = f;
        this.a = a < b ? a : b;
        this.b = a < b ? b : a;
        this.lb = lb < ub ? lb : ub;
        this.ub = lb < ub ? ub : lb;
        this.fa = delegate(double i) { return f(i) - this.lb; };
        this.fb = delegate(double i) { return f(i) - this.ub; };
        this.eps = eps;
        this.point = new List<double>();
        this.maxiter = maxiter;
        this.root = new List<Tuple<double, double>>();
    }

    public void FindPoints()
    {
        //intervals with crucial points
        List<Tuple<double, double>> interval = new List<Tuple<double, double>>();
        bool flag = membership(this.a);
        double past = this.a; // last point interval

        for (double i = this.a; i <= this.b; )
        {
            if ((membership(i) && flag == false) || (!membership(i) && flag == true))
            {
                interval.Add(new Tuple<double, double>(past, i));
                flag = flag == false ? true : false;
                //Console.WriteLine("[{0} {1}]", past, i);
            }
            past = i;
            i += eps;
            if (i > b && i < b + eps) i = b;
        }

        //if (interval.Count() == 0) ;
        for (int i = 0; i < interval.Count; i++)
        {
            try
            {
                if (this.constraint(interval[i])) this.point.Add(Brent.FindRoot(this.fb, interval[i].Item1, interval[i].Item2, eps, maxiter));
                else this.point.Add(Brent.FindRoot(this.fa, interval[i].Item1, interval[i].Item2, eps, maxiter));
                //Console.WriteLine(point[i]);
            }
            catch (NonConvergenceException e)
            {
                //Console.WriteLine("Not converge. Exception: {0}", e.ToString());
            }
        }
        if (point.Count > 0)
        {
            if (!point.Contains(a)) point.Insert(0, a);
            if (!point.Contains(b)) point.Add(b);
        }
    }

    public void FindRoot()
    {
        for (int i = 1; i < point.Count; i++)
        {
            if (membership((point[i - 1] + point[i]) / 2)) this.root.Add(new Tuple<double, double>(point[i - 1], point[i]));
        }
    }

    public override string ToString()
    {
        String res = "";
        foreach (Tuple<double, double> i in this.root)
        {
            res += String.Format("[{0}; {1}];\n", i.Item1, i.Item2);
        }
        return res;
    }

    public bool membership(double i)
    {

        return (this.f(i) <= this.ub && this.f(i) >= this.lb) ? true : false;
    }

    public bool constraint(Tuple<double, double> interval)
    {
        // where f not satisfy constraint (in a or b)
        if (this.f(interval.Item1) < this.lb || this.f(interval.Item2) < this.lb) return false;
        else return true;
    }
}

/*
class Program
    {
        static void Main(string[] args)
        {
            Func<double, double> func = function;
            FuncObj a = new FuncObj(func, 0, 600, 0.1, 0.6, 0.0001, 10000);
            a.FindPoints();
            a.FindRoot();
            Console.WriteLine(a);
            Console.ReadLine();
        }

        public static double function(double x)
        {
            //return (x - 1) * (x - 1) - 1;
            //return (x - 0.2) * (x - 0.1)*(x-0.1) * (x) * (x - 0.11111) * (x - 0.11112) * (x - 0.5);
            return 1 - Math.Log((1 + 0.5 * 0.6 * 0.8 * 0.7 * (1 + 0.5 * x) * (1 + 0.05 * x) * (1 - 0.000003 * x * x)), 2); [1.534; 5.9] [577. ; 577. ]
        }

    }
*/