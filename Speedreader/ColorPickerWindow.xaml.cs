using Speedreader.Properties;
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
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace Speedreader
{
    /// <summary>
    /// Interaction logic for ColorPickerWindow.xaml
    /// </summary>
    public partial class ColorPickerWindow : Window
    {
        Control modifiableControl;
        public ColorPickerWindow(Control activeControl)
        {
            modifiableControl = activeControl;
            InitializeComponent();
            
            this.Deactivated += new EventHandler(ColorPickerWindow_LostFocus);
            this.Closed += new EventHandler(ColorPickerWindow_Closed);
            SettingsWindow.settingsWin.isColorPickerOpen = true;
        }

        private void ColorPickerWindow_LostFocus(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ColorPickerWindow_Closed(object sender, EventArgs e)
        {
            SettingsWindow.settingsWin.isColorPickerOpen = false;
        }
        private void ClrPcker_Background_SelectedColorChanged(object sender, EventArgs e)
        {
            SettingsWindow.settingsWin.TransferColorToControl(ClrPcker_Background.SelectedColor.Value, modifiableControl);
        }
    }
}
