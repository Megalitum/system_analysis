using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;

namespace SA_lab_5
{
    class ExpertDataModel
    {
        public DataSet dataset;
        protected DefaultCell[,] array;
        int N, M; //number of rows and columns;

        public ExpertDataModel(string fileName)
        {
            Excel.Application excel_app = new Excel.Application();
            Excel.Workbook wb = excel_app.Workbooks.Open(fileName);
            dataset = new DataSet();

            foreach (Excel._Worksheet excelWorkSheet in wb.Sheets)
            {
                //Excel._Worksheet excelWorkSheet = wb.Sheets[1];
                Excel.Range excelRange = excelWorkSheet.UsedRange;
                DataTable dt1 = new DataTable();
                dt1.TableName = excelWorkSheet.Name;


                for (int j = 1; j <= excelRange.Columns.Count; j++)
                {
                    dt1.Columns.Add(excelRange.Cells[1, j].Value2.ToString());
                }

                DataRow dataRow = null;

                for (int row = 2; row <= excelRange.Rows.Count; row++)
                {
                    dataRow = dt1.NewRow();

                    for (int col = 1; col <= excelRange.Columns.Count; col++)
                    {
                        dataRow[col - 1] = (excelRange.Cells[row, col] as Excel.Range).Value2;
                    }
                    dt1.Rows.Add(dataRow);
                }
                dataset.Tables.Add(dt1);
            }
            wb.Close(true, Type.Missing, Type.Missing);
            excel_app.Quit();


            this.M = dataset.Tables[0].Columns.Count;
            this.N = dataset.Tables[0].Rows.Count;
            array = new DefaultCell[N, M];
            for (int i = 0; i < N; i++)
            {
                for (int j = 1; j < M; j++)
                {
                    if (dataset.Tables[0].Rows[i][j].ToString() == "–") array[i, j] = null;
                    else array[i, j - 1] = new DefaultCell(
                        double.Parse(dataset.Tables["If"].Rows[i][j].ToString(), System.Globalization.CultureInfo.InvariantCulture),
                        double.Parse(dataset.Tables["Ir"].Rows[i][j].ToString(), System.Globalization.CultureInfo.InvariantCulture),
                        double.Parse(dataset.Tables["It"].Rows[i][j].ToString(), System.Globalization.CultureInfo.InvariantCulture),
                        double.Parse(dataset.Tables["a"].Rows[i][j].ToString(), System.Globalization.CultureInfo.InvariantCulture)
                        );
                }
            }

        }
    }
}