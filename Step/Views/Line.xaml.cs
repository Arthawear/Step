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
using System.IO.Packaging;
using Step.Views;
using System.IO.Compression;
using System.Diagnostics;

namespace Step.Views
{
    /// <summary>
    /// Interaction logic for Line.xaml
    /// </summary>
    public partial class Line : UserControl
    {
        Microsoft.Office.Interop.Excel.Application app;

        public Line()
        {
            InitializeComponent();
        }
        private const long BUFFER_SIZE = 4096;
        private void Button_COMPRESS(object sender, RoutedEventArgs e)
        {
            //AddFileToZip("Calendar Fullcalendar Manifest und Skript.zip", @"C:\Users\hkovacs\Source\Repos\navCalendarAddin\Calendar Fullcalendar Manifest und Skript\Script");
            //AddFileToZip("Calendar Fullcalendar Manifest und Skript.zip", @"C:\Users\hkovacs\Source\Repos\navCalendarAddin\Calendar Fullcalendar Manifest und Skript\StyleSheet");
            //AddFileToZip("Calendar Fullcalendar Manifest und Skript.zip", @"C:\Users\hkovacs\Source\Repos\navCalendarAddin\Calendar Fullcalendar Manifest und Skript\Manifest.xml");
            //compressButon.Content = "cliccked";
            string startPath = @"C:\Users\hkovacs\Source\Repos\navCalendarAddin\Calendar Fullcalendar Manifest und Skript";
            string zipPath = @"C:\Users\hkovacs\Source\Repos\navCalendarAddin\Calendar Fullcalendar Manifest und Skript\Calendar Fullcalendar Manifest und Skript.zip";
            string extractPath = @"C:\Users\hkovacs\Source\Repos\navCalendarAddin\Calendar Fullcalendar Manifest und Skript";

            System.IO.Compression.ZipFile.CreateFromDirectory(startPath, zipPath);
            System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);

           

        }
        private static void AddFileToZip(string zipFilename, string fileToAdd)
        {
            using (Package zip = System.IO.Packaging.Package.Open(zipFilename, FileMode.OpenOrCreate))
            {
                string destFilename = ".\\" + System.IO.Path.GetFileName(fileToAdd);
                Uri uri = PackUriHelper.CreatePartUri(new Uri(destFilename, UriKind.Relative));
                if (zip.PartExists(uri))
                {
                    zip.DeletePart(uri);
                }
                PackagePart part = zip.CreatePart(uri, "", CompressionOption.Normal);
                using (FileStream fileStream = new FileStream(fileToAdd, FileMode.Open, FileAccess.Read))
                {
                    using (Stream dest = part.GetStream())
                    {
                        CopyStream(fileStream, dest);
                    }
                }
            }
        }
        private static void CopyStream(System.IO.FileStream inputStream, System.IO.Stream outputStream)
        {
            long bufferSize = inputStream.Length < BUFFER_SIZE ? inputStream.Length : BUFFER_SIZE;
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;
            long bytesWritten = 0;
            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
                bytesWritten += bufferSize;
            }
        }
    }
}
