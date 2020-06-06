using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Speedreader
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            WpmTxtbox.Text = MainWindow.mainWindow.ReadSpeed.ToString();
            
        }

        private void WinSizeCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FontSizeCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FontColorBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BgColorBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WpmTxtbox_OnFocusLost(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex("[0-9]+");
            if (regex.IsMatch(WpmTxtbox.Text))
            {
                MainWindow.mainWindow.ReadSpeed = int.Parse(WpmTxtbox.Text);
            }
            else
            {
                WpmTxtbox.Text = MainWindow.mainWindow.ReadSpeed.ToString();
            }
        }
    }
}
