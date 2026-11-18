using Step.Models;
using Step.Storage;
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

namespace Step.Views
{
    /// <summary>
    /// Interaction logic for Screen.xaml
    /// </summary>
    public partial class Screen : UserControl
    {
        string savedSettingsFilePath = "SavedNames.json";
        private TabModel tabModel;
        public Screen()
        {
            InitializeComponent();
            tabModel = new TabModel();
            try
            {
                var fileStorage = new FileStorage<object[]>();
                var settingsSaved = fileStorage.GetModel(savedSettingsFilePath);
                for (int i = 0; i < tabModel.TabNames.Length; i++)
                {
                    tabModel.TabNames[i] = (string)settingsSaved[i];
                }
            }
            catch (Exception)
            {
                
            }
            this.DataContext = tabModel;
        }
    }
}
