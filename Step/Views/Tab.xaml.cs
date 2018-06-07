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
       
        public Tab()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            string cellName = this.DataContext.ToString();
            string fileName = "C:\\Temp\\" + cellName + ".xlsx";
            FileInfo fi = new FileInfo(@fileName);
            if (!fi.Exists)
            {
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
            }
            else
            {
                string myPath = (@fileName);
                app.Workbooks.Open(myPath);
                wb = app.ActiveWorkbook;
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
                wb.Save();
                wb.Close();
                app.Quit();
                app.DisplayAlerts = true;
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            string cellName = this.DataContext.ToString();
            string fileName = "C:\\Temp\\" + cellName + ".xlsx";
            FileInfo fi = new FileInfo(@fileName);
            if (!fi.Exists)
            {
                wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                ws = wb.Worksheets[1];
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
                wb.SaveAs(fileName);
                wb.Close();
                app.Quit();
                app.DisplayAlerts = true;
            }
            else
            {
                string myPath = (fileName);
                app.Workbooks.Open(myPath);
                wb = app.ActiveWorkbook;
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
                wb.Save();
                wb.Close();
                app.Quit();
                app.DisplayAlerts = true;
            }

        }

        private void Table_Click(object sender, RoutedEventArgs e)
        {
            string cellName = this.DataContext.ToString();
            string fileName = "C:\\Temp\\" + cellName + ".xlsx";
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
            string newName = nameTxtBox.Text;
            this.DataContext = newName;
            popLink.IsOpen = false;
        }

    }
}
