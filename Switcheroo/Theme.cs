using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Controls;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Switcheroo.Properties;

namespace Switcheroo
{
    public static class Theme
    {
        private static SolidColorBrush Background;
        private static SolidColorBrush Foreground;
        private static MainWindow mainWindow;

        private enum Mode
        {
            Light,
            Dark
        }

        public static void SuscribeWindow(MainWindow main)
        {
            mainWindow = main;
        }

        public static void LoadTheme()
        {
            Mode mode;
            Enum.TryParse(Settings.Default.Theme, out mode);
            switch (mode)
            {
                case Mode.Light:
                    Background = new SolidColorBrush(Color.FromRgb(248, 248, 248));
                    Foreground = new SolidColorBrush(Color.FromRgb(0,0,0));
                    break;
                case Mode.Dark:
                    Background = new SolidColorBrush(Color.FromRgb(30, 30, 30));
                    Foreground = new SolidColorBrush(Color.FromRgb(240, 240, 240));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }

            SetUpTheme();
        }

        private static void SetUpTheme()
        {
            var BackgroundList = new ArrayList()
            {
                mainWindow.Border,
                mainWindow.txtSearch,
                mainWindow.lblProgramName,
            };

            var ForegroundList = new ArrayList() // Build a flattened ArrayList out of nested elements using InsertRange
            {
                mainWindow.txtSearch,
                mainWindow.lblProgramName
            };
            ForegroundList.InsertRange(0, mainWindow.HelpPanel.Children.OfType<TextBlock>().ToList()); 
            ForegroundList.InsertRange(0, mainWindow.HelpSearchDetails.Children.OfType<TextBlock>().ToArray()); 

            foreach (dynamic fgElement in ForegroundList) // Using dynamic instead of var allows calling property on multiple data types
            {
                fgElement.Foreground = Foreground;
            }

            foreach (dynamic bgElement in BackgroundList)
            {
                if (bgElement is Border)
                {
                    bgElement.Background = Background;
                    bgElement.BorderBrush = Background;
                } 
                else
                {
                    bgElement.Background = Background;
                }
            }

        }

    }
}
