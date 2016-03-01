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

        public ExpertDataModel(string fileName)
        {
            Excel.Application excel_app = new Excel.Application();
            dataset = new DataSet();
            bool first_sheet = true;
            Excel.Workbook wb = excel_app.Workbooks.Open(fileName);
            foreach (Excel._Worksheet excelWorkSheet in wb.Sheets)
            {
                //Excel._Worksheet excelWorkSheet = wb.Sheets[1];
                Excel.Range excelRange = excelWorkSheet.UsedRange;
                DataTable dt = new DataTable();
                dt.TableName = excelWorkSheet.Name;


                for (int j = 2; j <= excelRange.Columns.Count; j++)
                {
                    dt.Columns.Add(excelRange.Cells[1, j].Value2.ToString(), type: first_sheet ? typeof(string) : typeof(double));
                }
                DataRow dataRow = null;

                for (int row = 2; row <= excelRange.Rows.Count; row++)
                {
                    dataRow = dt.NewRow();
                    

                    for (int col = 2; col <= excelRange.Columns.Count; col++)
                    {
                        if (first_sheet || excelRange.Cells[row, col].Value2 != null)
                        {
                            dataRow[col - 2] = excelRange.Cells[row, col].Value2;
                        }
                        else
                        {
                            dataRow[col - 2] = DBNull.Value;
                        }
                    }
                    dt.Rows.Add(dataRow);
                }
                first_sheet = false;
                dataset.Tables.Add(dt);
            }
            wb.Close();
            excel_app.Quit();

        }

        
    }
}