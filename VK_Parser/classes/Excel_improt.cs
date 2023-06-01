using System;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace Excel_improt
{
    class Excel_improt : IDisposable
    {
        private Excel.Application _excel;
        private Excel.Workbook _workbook;
        private string _filePath;

        public Excel_improt()
        {
            _excel = new Excel.Application();
        }

        internal void delete (string filePath)
        {
            
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception e)
                {
                }
            }
            else
            {
            }
        }

        internal bool Open(string filePath)
        {
            try
            {
                _workbook = _excel.Workbooks.Add();

                if (File.Exists(filePath))
                {
                    _workbook = _excel.Workbooks.Open(filePath);
                    //_workbook = _excel.Workbooks.Add();

                }
                else
                {
                    _workbook = _excel.Workbooks.Add();
                    _filePath = filePath;
                }

                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }

        internal void Save()
        {
            if (!string.IsNullOrEmpty(_filePath))
            {
                _workbook.SaveAs(_filePath);
                _filePath = null;
            }
            else
            {
                _workbook.Save();
            }
        }

        internal bool Set(string column, int row, object data)
        {
            try
            {
                // var val = ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, column].Value2;

                ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, column] = data;
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }

        internal object Get(string column, int row)
        {
            try
            {
                return ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, column].Value2;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null;
        }

        public void Dispose()
        {
            try
            {
                _workbook.Close();
                _excel.Quit();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
