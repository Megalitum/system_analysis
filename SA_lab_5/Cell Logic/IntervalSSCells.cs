using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SA_lab_5.Cell_Logic
{
    class IntervalsSSCells<T> where T : BaseCell
    {
        public double[,] t_plus_w, t_minus_w;
        public List<String> columns { get; set; }
        public T[,] cell { get; set; }
        public List<Tuple<double, double>>[,] interval{get; private set;}
        public int N, M;
        public double n_right { get; set; } //eta(n with tail) crucial for intervals
        public double n_left { get; set; }
        public IntervalsSSCells(ExpertDataModel model)
        {
            tw_init();

            M = model.dataset.Tables[0].Columns.Count;
            N = model.dataset.Tables[0].Rows.Count;

            interval = new List<Tuple<double,double>>[N, M];

            columns = new List<String>();
            foreach (DataColumn col in model.dataset.Tables[0].Columns)
            {
                columns.Add(col.ColumnName);
            }
            
            cell = new T[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (model.dataset.Tables[1].Rows[i][j] == DBNull.Value) cell[i, j] = null;
                    else cell[i, j] = (T)Activator.CreateInstance(typeof(T),
                        model.dataset.Tables["If"].Rows[i][j], model.dataset.Tables["Ir"].Rows[i][j],
                        model.dataset.Tables["It"].Rows[i][j], model.dataset.Tables["a"].Rows[i][j]);
                }
            }
        }

        public void FindInterval()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (this.cell[i, j] == null) interval[i, j] = null;
                    else
                    {
                        this.interval[i, j] = this.cell[i, j].FindTimeInterval(this.n_left, this.n_right);
                    }
                }
            }
        }

        public String cellIntervalToString(List<Tuple<double, double>> interval)
        {
            if (interval == null) return "-";
            String ret = "";
            if(interval.Count == 0) return "empty set";
            else
            {
                foreach (Tuple<double, double> i in interval)
                {
                    //ret += $"[{i.Item1}; {i.Item2}];";
                    ret += $"[{Math.Round(i.Item1, 1)}; {Math.Round(i.Item2, 1)}];";
                }
                return ret;
            }
        }

        public List<Tuple<double, double>> unioninterval()
        {
            List<Tuple<double, double>>uninterval = new List<Tuple<double, double>>();
            for (int i = 0; i < N; i++)
            {
                double a = 1000000, b = -1;
                for (int j = 0; j < M; j++)
                {
                    if (interval[i, j] == null) continue;
                    foreach (Tuple<double, double> l in interval[i, j])
                    {
                        if (l.Item1 < a) a = l.Item1;
                        if (l.Item2 > b) b = l.Item2;
                    }
                }
                if (b == -1 || a > b) uninterval.Add(new Tuple<double, double>(-1, -1));
                else uninterval.Add(new Tuple<double, double>(a, b));
            }
            return uninterval;
        }

        public List<string> uninterval_tostring(List<Tuple<double, double>> union)
        {
            List<string> res = new List<string>();
            foreach (var ob in union)
            {
                if (ob.Item1 < 0 || ob.Item2 < 0) res.Add("empty set");

                else res.Add($"[{Math.Round(ob.Item1, 3)}; {Math.Round(ob.Item2, 3)}];");
                //else res.Add($"[{ob.Item1}; {ob.Item2}];");
            }
            return res;
        }

        public bool classify(double[,] tw, double t0, double w0 = 0.5)
        {
            //t - T0; w - weights; 
            double sum = 0;
            double w = 0; // weights more tw weights
            for (int i = 0; i < tw.GetLength(0); i++)
            {
                if (t0 >= tw[i,0]) w += tw[i,1];
                sum += tw[i,1];
            }
                        
            return w/sum >= w0 ? true : false;
        }

        public List<int> determine_class(List<Tuple<double, double>> a)
        {
            //3 - regular
            //2 - critial
            //1 - dangerous
            List<int> res = new List<int>();
            foreach (var ob in a)
            {
                if (classify(t_plus_w, ob.Item2 - ob.Item1, 0.5)) res.Add(3);
                else if (!classify(t_minus_w, ob.Item2 - ob.Item1, 0.5)) res.Add(1);
                else res.Add(2);
            }
            return res;
        }

        public void tw_init()
        {
            int n = 11;
            //const for t_plus_w
            double c1 = 50;
            double h1 = 5;
            double prob1 = 0.1;
            //const for t_minus_w
            double c2 = 5;
            double h2 = 5;
            double prob2 = 0.1;
            t_plus_w = new double[n, 2];
            t_minus_w = new double[n, 2];
            for (int i = 0; i < n; i++)
            {
                t_plus_w[i, 0] = c1+i*h1;
                t_plus_w[i, 1] = 1 - prob1 * i;

                t_minus_w[i, 0] = c2 + i * h2;
                t_minus_w[i, 1] = prob2 * i;

            }

        }
    }
}
