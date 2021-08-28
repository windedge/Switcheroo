using Switcheroo.Properties;
using System;
using System.Windows.Media;

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
                    Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    break;
                case Mode.Dark:
                    Background = new SolidColorBrush(Color.FromRgb(30, 30, 30));
                    Foreground = new SolidColorBrush(Color.FromRgb(248, 248, 248));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }

            SetUpTheme();
        }

        private static void SetUpTheme()
        {
            mainWindow.Border.Background =
                mainWindow.tb.Background = mainWindow.lb.Background
                = mainWindow.Border.BorderBrush = Background;

            mainWindow.tb.Foreground = mainWindow.lb.Foreground = Foreground;
        }
    }
}
