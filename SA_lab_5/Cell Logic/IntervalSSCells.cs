using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SA_lab_5.Cell_Logic
{
    class IntervalsSSCells<T> where T: BaseCell
    {
        public List<String> columns { get; set; }
        public T[,] cell { get; set; }
        public List<Tuple<double, double>>[,] interval{get; private set;}
        public int N, M;
        public double n_right { get; set; } //eta(n with tail) crucial for intervals
        public double n_left { get; set; }
        public IntervalsSSCells(ExpertDataModel model)
        {
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
                else res.Add($"[{Math.Round(ob.Item1, 1)}; {Math.Round(ob.Item2, 1)}];");
            }
            return res;
        }
    }
}
