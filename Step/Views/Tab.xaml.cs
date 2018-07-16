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
using Step.Storage;

namespace Step.Views
{
    /// <summary>
    /// Interaction logic for Tab.xaml
    /// </summary>
    public partial class Tab : UserControl
    {
        Microsoft.Office.Interop.Excel.Application app;

        public Tab()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook wb;
            Microsoft.Office.Interop.Excel._Worksheet ws;
            this.Background =Brushes.Green;
            string cellName = this.DataContext.ToString();
            string fileName = "C:\\Temp\\" + cellName + ".xlsx";
            FileInfo fi = new FileInfo(@fileName);
            if (!fi.Exists)
            {
                var wbs = app.Workbooks;
                wb = wbs.Add(XlWBATemplate.xlWBATWorksheet);
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
                Marshal.ReleaseComObject(ws);
                Marshal.ReleaseComObject(wb);
                Marshal.ReleaseComObject(wbs);
                Marshal.ReleaseComObject(app);
            }
            else
            {
                string myPath = (@fileName);
                var wbs = app.Workbooks;
                wbs.Open(myPath);
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
                Marshal.ReleaseComObject(ws);
                Marshal.ReleaseComObject(wb);
                Marshal.ReleaseComObject(wbs);
                Marshal.ReleaseComObject(app);
            }
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook wb;
            Microsoft.Office.Interop.Excel._Worksheet ws;
            this.Background = null;
            string cellName = this.DataContext.ToString();
            string fileName = "C:\\Temp\\" + cellName + ".xlsx";
            FileInfo fi = new FileInfo(@fileName);
            if (!fi.Exists)
            {
                var wbs = app.Workbooks;
                wb = wbs.Add(XlWBATemplate.xlWBATWorksheet);
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
                Marshal.ReleaseComObject(ws);
                Marshal.ReleaseComObject(wb);
                Marshal.ReleaseComObject(wbs);
                Marshal.ReleaseComObject(app);
            }
            else
            {
                string myPath = (fileName);
                var wbs = app.Workbooks;
                wbs.Open(myPath);
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
                Marshal.ReleaseComObject(ws);
                Marshal.ReleaseComObject(wb);
                Marshal.ReleaseComObject(wbs);
                Marshal.ReleaseComObject(app);
            }

        }

        private void Table_Click(object sender, RoutedEventArgs e)
        {
            app = new Microsoft.Office.Interop.Excel.Application();
            string cellName = this.DataContext.ToString();
            string fileName = "C:\\Temp\\" + cellName + ".xlsx";
            FileInfo fi = new FileInfo(fileName);
            string myPath = (@fileName);
            
            if (fi.Exists)
            {
                var wbs = app.Workbooks;
                wbs.Open(myPath);
                app.Visible = true;
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
            string buttonName = this.Name;
            int buttonNumber = Convert.ToInt32(buttonName.Substring(3));
            string newName = nameTxtBox.Text;
            this.DataContext = newName;
            string savedSettingsFilePath = "SavedNames.json";
            try
            {
                var fileStorage = new FileStorage<object[]>();
                object[] settingsSaved = fileStorage.GetModel(savedSettingsFilePath);
                settingsSaved[buttonNumber - 1] = newName;
                fileStorage.SetModel(savedSettingsFilePath, settingsSaved);
            }
            catch (Exception)
            {
                var fileStorage = new FileStorage<object[]>();
                object[] settingsSaved = new object[18];
                for (int i = 0; i < settingsSaved.Length; i++)
                {
                    settingsSaved[i] = "Name";
                }
                settingsSaved[buttonNumber - 1] = newName;
                fileStorage.SetModel(savedSettingsFilePath, settingsSaved);
            }
            popLink.IsOpen = false;
        }

    }
}
