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
    /// 
    public partial class SettingsWindow : Window
    {
        private int[] width = new int[4] { 600, 800, 1000, 1200 };
        private int[] height = new int[4] { 400, 600, 800, 1000 }; //could be calculated -> width[index] - 200;
        private int[] fontSize = new int[11] { 12, 14, 16, 18, 20, 22, 24, 26, 28, 30, 32 };

        public bool isColorPickerOpen;
        public static SettingsWindow settingsWin;

        private ColorPickerWindow ColorPickerWindow;
        Color fontButtonColor;
        public Color FontButtonColor
        {
            get { return fontButtonColor; }
            set
            {
                fontButtonColor = value;
            }
        }

        Color bgButtonColor;
        public Color BgButtonColor
        {
            get { return bgButtonColor; }
            set
            {
                bgButtonColor = value;
            }
        }

        public SettingsWindow()
        {
            settingsWin = this;
            MainWindow.mainWindow.isSeetingsOpen = true;
            InitializeComponent();
            WpmTxtbox.Text = MainWindow.mainWindow.ReadSpeed.ToString();
            WinSizeCmbBox.SelectedIndex = 0;
            FontSizeCmbBox.SelectedIndex = 0;

            //this.Deactivated += new EventHandler(SettingsWindow_FocusLost);
            this.Closed += new EventHandler(SettingsWindow_Closed);
        }

        private void FontColorBtnBackground_Changed(object sender, EventArgs e)
        {

        }

        private void SettingsWindow_Closed(object sender, EventArgs e)
        {
            MainWindow.mainWindow.isSeetingsOpen = false;
        }

        private void SettingsWindow_FocusLost(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WinSizeCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Point resolution = new Point(0, 0);
            resolution.X = width[WinSizeCmbBox.SelectedIndex];
            resolution.Y = height[WinSizeCmbBox.SelectedIndex];
            MainWindow.mainWindow.Resolution = resolution;
        }

        private void FontSizeCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow.mainWindow.TextSize = fontSize[FontSizeCmbBox.SelectedIndex];
        }

        private void FontColorBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isColorPickerOpen)
            {
                ColorPickerWindow.Close();
            } else
            {
                ColorPickerWindow = new ColorPickerWindow(FontColorBtn);
                Point position = MainWindow.mainWindow.GetScreenPointOfControl(FontColorBtn);
                ColorPickerWindow.Left = position.X + 20;
                ColorPickerWindow.Top = position.Y;
                ColorPickerWindow.Show();
                ColorPickerWindow.Activate();
            }            
        }

        private void BgColorBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isColorPickerOpen)
            {
                ColorPickerWindow.Close();
            }
            else
            {
                ColorPickerWindow = new ColorPickerWindow(BgColorBtn);
                Point position = MainWindow.mainWindow.GetScreenPointOfControl(BgColorBtn);
                ColorPickerWindow.Left = position.X + 20;
                ColorPickerWindow.Top = position.Y;
                ColorPickerWindow.Show();
                ColorPickerWindow.Activate();
            }
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

        public void TransferColorToControl(Color color, Control changedControl)
        {
            SolidColorBrush scb = new SolidColorBrush(color);
            changedControl.Background = scb;
            changedControl.BorderBrush = scb;
            changedControl.Foreground = scb;

            if (changedControl == FontColorBtn)
            {
                MainWindow.mainWindow.SpeedReaderPoint.Foreground = scb;
            } else
            {
                MainWindow.mainWindow.SpeedReaderPoint.Background = scb;
            }
        }
    }
}
