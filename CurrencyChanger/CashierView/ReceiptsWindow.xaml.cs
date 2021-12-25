using CurrencyChanger.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CurrencyChanger
{
    /// <summary>
    /// Interaction logic for ReceiptsWindow.xaml
    /// </summary>
    public partial class ReceiptsWindow : Window
    {
        private readonly string filesPath = "C:/Users/Artyom Yankin/Desktop/WPF/CurrencyChanger/Receipts/";
        public ReceiptsWindow()
        {
            InitializeComponent();
        }
        public void LoadPhisicalFiles(object c, RoutedEventArgs a)
        {
            var files = new List<Files>();
            var phisicalFiles = Directory.GetFiles(filesPath);
            foreach (var fl in phisicalFiles)
            {
                var finfo = new System.IO.FileInfo(fl);
                int fileSize = (int)finfo.Length;
                string fileName = finfo.Name;
                Files f = new Files();
                f.Name = fileName;
                bListFiles.Items.Add(new CheckBox()
                {
                    Content = f.Name
                });
                files.Add(f);
            }
        }
        private void mnPreview_Click(object c, RoutedEventArgs a)
        {
            var fileName = ((CheckBox)bListFiles.SelectedItem).Content.ToString();
            new Process { StartInfo = new ProcessStartInfo($"{filesPath}{fileName}") { UseShellExecute = true } }.Start();
        }
    }
}
