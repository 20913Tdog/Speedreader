using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
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

        bool isInReadMode;

        int readingSpeed = 400;
        private static int position = 0;
        List<string> words = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            //speedTextBox.Text = readingSpeed.ToString();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            StartStopBtn.Content = "Start";
        }

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    Regex regex = new Regex("[0-9]+");
        //    if(regex.IsMatch(speedTextBox.Text))
        //    {
        //        readingSpeed = int.Parse(speedTextBox.Text);
        //    } else
        //    {
        //        speedTextBox.Text = readingSpeed.ToString();
        //    }
        //}

        private void SetReadingSpeed(int valueChange)
        {
            readingSpeed = readingSpeed + valueChange;
            int speed = 60000 / readingSpeed;
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
            SpeedReaderPoint.Content = words[position];
            if (position < words.Count)
            {
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

                words = text.Split(' ').ToList<string>();
                SetReadingSpeed(0);
                dispatcherTimer.Start();
            }            
        }

        private string GetStringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }
    }
}
