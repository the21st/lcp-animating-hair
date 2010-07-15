using System;
using System.Globalization;
using System.Windows.Forms;
using AnimatingHair.GUI;

namespace AnimatingHair
{
    static class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.CurrentCulture = new CultureInfo( "en-US" );
            Application.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";

            using ( ControlsWindow window = new ControlsWindow() )
            {
                Application.EnableVisualStyles();
                window.ShowDialog();
            }
        }
    }
}
