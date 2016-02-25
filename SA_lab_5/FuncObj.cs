using System;
using System.Collections.Generic;
using MathNet.Numerics.RootFinding;
using MathNet.Numerics;

public class FuncObj
{
    Func<double, double> f;
    double a { get; set; }
    double b { get; set; }
    double eps { get; set; }
    int maxiter { get; set; }
    List<double> point;
    List<Tuple<double, double>> root;

    public FuncObj(Func<double, double> f, double a, double b, double eps, int maxiter = 100)
    {
        this.f = f;
        this.a = a;
        this.b = b;
        this.eps = eps;
        this.point = new List<double>();
        this.maxiter = maxiter;
        this.root = new List<Tuple<double, double>>();
    }

    public void FindPoints()
    {
        //intervals with crucial points
        List<Tuple<double, double>> interval = new List<Tuple<double, double>>();
        bool flag = this.f(a) < 0 ? false : true;
        double past = this.a; // past point interval
        for (double i = this.a; i <= this.b;)
        {

            if ((f(i) > 0 && flag == false) || (f(i) < 0 && flag == true))
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
                this.point.Add(Brent.FindRoot(f, interval[i].Item1, interval[i].Item2, eps, maxiter));
                //Console.WriteLine(point[i]);
            }
            catch (NonConvergenceException e)
            {
                Console.WriteLine("Not converge. Exception: {0}", e.ToString());
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
            if (f((point[i - 1] + point[i]) / 2) <= 0) this.root.Add(new Tuple<double, double>(point[i - 1], point[i]));
        }
    }

    public override string ToString()
    {
        String res = "";
        foreach (Tuple<double, double> i in this.root)
        {
            res += String.Format("[{0},{1}];", i.Item1, i.Item2);
        }
        return res;
    }
}