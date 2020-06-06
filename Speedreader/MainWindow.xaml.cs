using Speedreader.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
using System.Windows.Threading;

namespace Speedreader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private static SettingsWindow settingsWin;

        bool isInReadMode;
        public bool isSeetingsOpen;

        private static int position = 0;
        List<string> words = new List<string>();

        public static MainWindow mainWindow;

        private int readSpeed = 200;
        public int ReadSpeed
        {
            get { return readSpeed; }
            set
            {
                readSpeed = value;
                SetReadingSpeed(readSpeed);
            }
        }

        private Point resolution;
        public Point Resolution
        {
            get { return resolution; }
            set {
                resolution = value;
                SetWindowResolution(resolution);
            }
        }

        private int textSize;
        public int TextSize
        {
            get { return textSize; }
            set
            {
                textSize = value;
                SetTextSize(textSize);
            }
        }

        public MainWindow()
        {
            mainWindow = this;            
            InitializeComponent();           
            
            StartStopBtn.Content = "Start";
            MenuBtn.Content = "Settings";

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            mainWindow.Closed += new EventHandler(Mainwindow_Close);
        }

        public void SetReadingSpeed(int value)
        {
            int speed = 60000 / value;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, speed);
        }

        private void ReadMode ()
        {
            StartStopBtn.Content = "Stop";
            InputTextBox.Visibility = Visibility.Hidden;
            isInReadMode = true;
        }

        private void InputMode()
        {
            StartStopBtn.Content = "Start";
            InputTextBox.Visibility = Visibility.Visible;
            isInReadMode = false;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            ReadMode();
            if (position < words.Count)
            {
                SpeedReaderPoint.Content = words[position];
                position++;
            }
            else
            {
                InputMode();
                dispatcherTimer.Stop();
            }
            
        }

        private void StartStopBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isInReadMode)
            {
                dispatcherTimer.Stop();
                InputMode();
            } else
            {
                position = 0;
                string text = GetStringFromRichTextBox(InputTextBox);
                text = text.Replace(System.Environment.NewLine, "");

                SetReadingSpeed(ReadSpeed);
                words = text.Split(' ').ToList<string>();
                dispatcherTimer.Start();
            }            
        }

        private string GetStringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }

        private void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!isSeetingsOpen)
            {
                settingsWin = new SettingsWindow();

                Point targetPoint = GetScreenPointOfControl(MenuBtn);
                settingsWin.WindowStartupLocation = WindowStartupLocation.Manual;
                settingsWin.Left = targetPoint.X+20;
                settingsWin.Top = targetPoint.Y;

                settingsWin.Show();
                settingsWin.Activate();
            } 
            else
            {
                settingsWin.Close();
                settingsWin = null;
            }
        }

        public Point GetScreenPointOfControl(Control control)
        {
            PresentationSource source = PresentationSource.FromVisual(this);
            System.Windows.Point targetPoints = source.CompositionTarget.TransformFromDevice.Transform(control.PointToScreen(new Point(0, 0)));
            return targetPoints;
        }

        private void SetWindowResolution(Point resolution)
        {
            this.Width = resolution.X;
            this.Height = resolution.Y;

            //InputTextBox.Margin = new Thickness(15, 0, 0, 0);
            InputTextBox.Width = resolution.X - 35;
            InputTextBox.Height = resolution.Y - 100;    
            //.w
        }

        private void Mainwindow_Close(object sender, EventArgs e)
        {
            settingsWin.Close();
        }

        private void SetTextSize(int textSize)
        {
            SpeedReaderPoint.FontSize = textSize;
            InputTextBox.FontSize = textSize;
        }
    }
}
