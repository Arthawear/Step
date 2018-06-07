using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using System.IO;
using Step.Models;

namespace Step.Views
{
    /// <summary>
    /// Interaction logic for Tab.xaml
    /// </summary>
    public partial class Tab : UserControl
    {
        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel._Workbook wb;
        Microsoft.Office.Interop.Excel._Worksheet ws;
        ////private TabModel tabModel;

        //Microsoft.Office.Interop.Excel.Range oRng;
        public Tab()
        {
            InitializeComponent();
            //tabModel = new TabModel();
            //this.DataContext = tabModel;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            string cellName = this.DataContext.ToString();
            string fileName = "C:\\Temp\\" + cellName + ".xlsx";
            //FileInfo fi = new FileInfo(@"C:\\Temp\\proba4.xlsx");
            FileInfo fi = new FileInfo(@fileName);
            if (!fi.Exists)
            {
                //oXL = new Microsoft.Office.Interop.Excel.Application();
                //oXL.Visible = true;
                //oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(System.Reflection.Missing.Value));
                //oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;
                //Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //app.Visible = true;
                //app.WindowState = XlWindowState.xlMaximized;
                wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                ws = wb.Worksheets[1];
                ws.Range["A1"].Value = cellName;
                DateTime currentDate = DateTime.Now;
                for (int i = 2; i <= 1000; i++)
                {
                    if (ws.Range["A" + i].Value == null)
                    {
                        ws.Range["A" + i].Value = currentDate;
                        break;
                    }

                }
                app.DisplayAlerts = false;
                wb.SaveAs(fileName);
                wb.Close();
                app.Quit();
                app.DisplayAlerts = true;
                //Marshal.ReleaseComObject(ws);
                //Marshal.ReleaseComObject(wb);
                //Marshal.ReleaseComObject(app);
            }
            else
            {
                //Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                string myPath = (@fileName);
                app.Workbooks.Open(myPath);
                //app.Visible = true;
                //wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                wb = app.ActiveWorkbook;
                //ws = wb.Worksheets[1];
                ws = wb.ActiveSheet;
                DateTime currentDate = DateTime.Now;
                for (int i = 2; i <= 1000; i++)
                {
                    if (ws.Range["A" + i].Value == null)
                    {
                        ws.Range["A" + i].Value = currentDate;
                        break;
                    }
                }
                app.DisplayAlerts = false;
                //wb.SaveAs("C:\\Temp\\proba4.xlsx");
                wb.Save();
                wb.Close();
                app.Quit();
                app.DisplayAlerts = true;
                //Marshal.ReleaseComObject(ws);
                //Marshal.ReleaseComObject(wb);
                //Marshal.ReleaseComObject(app);
            }



            //ws.Range["A1:A3"].Value = "Who is number one? :)";
            //ws.Range["A4"].Value = "vitoshacademy.com";
            //ws.Range["A5"].Value = currentDate;
            //ws.Range["B6"].Value = "Tommorow's date is: =>";
            //ws.Range["C6"].FormulaLocal = "= A5 + 1";
            //ws.Range["A7"].FormulaLocal = "=SUM(D1:D10)";
            //for (int i = 1; i <= 10; i++)
            //    ws.Range["D" + i].Value = i * 2;

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            string cellName = this.DataContext.ToString();
            string fileName = "C:\\Temp\\" + cellName + ".xlsx";
            //string fileName = "C:\\Temp\\table.xlsx";
            //FileInfo fi = new FileInfo(@"C:\\Temp\\proba4.xlsx");
            FileInfo fi = new FileInfo(@fileName);
            if (!fi.Exists)
            {
                //oXL = new Microsoft.Office.Interop.Excel.Application();
                //oXL.Visible = true;
                //oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(System.Reflection.Missing.Value));
                //oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;
                //Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                //app.Visible = true;
                //app.WindowState = XlWindowState.xlMaximized;
                wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                ws = wb.Worksheets[1];
                //ws.Range["A1"].Value = "Name";
                DateTime currentDate = DateTime.Now;
                for (int i = 2; i <= 1000; i++)
                {
                    if (ws.Range["B" + i].Value == null)
                    {
                        ws.Range["B" + i].Value = currentDate;
                        break;
                    }
                }
                app.DisplayAlerts = false;
                //wb.SaveAs("C:\\Temp\\proba4.xlsx");
                wb.SaveAs(fileName);
                wb.Close();
                app.Quit();
                app.DisplayAlerts = true;
                //Marshal.ReleaseComObject(ws);
                //Marshal.ReleaseComObject(wb);
                //Marshal.ReleaseComObject(app);
            }
            else
            {
                //Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                string myPath = (fileName);
                app.Workbooks.Open(myPath);
                //app.Visible = true;
                //wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                wb = app.ActiveWorkbook;
                //ws = wb.Worksheets[1];
                ws = wb.ActiveSheet;
                DateTime currentDate = DateTime.Now;
                for (int i = 2; i <= 1000; i++)
                {
                    if (ws.Range["B" + i].Value == null)
                    {
                        ws.Range["B" + i].Value = currentDate;
                        break;
                    }

                }
                app.DisplayAlerts = false;
                //wb.SaveAs("C:\\Temp\\proba4.xlsx");
                wb.Save();
                wb.Close();
                app.Quit();
                app.DisplayAlerts = true;
                //Marshal.ReleaseComObject(ws);
                //Marshal.ReleaseComObject(wb);
                //Marshal.ReleaseComObject(app);
            }

        }

        private void Table_Click(object sender, RoutedEventArgs e)
        {
            string cellName = this.DataContext.ToString();
            string fileName = "C:\\Temp\\" + cellName + ".xlsx";
            //string fileName = "C:\\Temp\\table.xlsx";
            FileInfo fi = new FileInfo(fileName);
            if (fi.Exists)
            {
                if (app.Visible == true)
                {
                    app.Workbooks.Close();
                    app.Quit();
                }
                else
                {
                    string myPath = (@fileName);
                    app.Workbooks.Open(myPath);
                    app.Visible = true;
                }

            }
        }

        private void Setbutton_Click(object sender, RoutedEventArgs e)
        {
            if (popLink.IsOpen == false)
            {
                popLink.IsOpen = true;
            }
            else
            {
                popLink.IsOpen = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var screen = this.Parent as Screen;
            //TabModel tabModel = screen.DataContext as TabModel;
            //string oldName = this.DataContext.ToString();

            string newName = nameTxtBox.Text;

            //TabName.Text = newName;
            this.DataContext = newName;
            //string[] oldTabNames = tabModel.TabNames;
            //string[] newTabNames = oldTabNames.Select(x => x.Replace(oldName, newName)).ToArray();
            //tabModel.TabNames = newTabNames;


            //ws.Range["A1"].Value = newName;

            popLink.IsOpen = false;
        }

    }
}
