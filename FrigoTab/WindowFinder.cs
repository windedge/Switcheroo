using Serilog;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FrigoTab {

    public class WindowFinder {

        public readonly IList<WindowHandle> Windows = new List<WindowHandle>();
        public readonly IList<WindowHandle> ToolWindows = new List<WindowHandle>();

        public WindowFinder () => EnumWindows(EnumWindowCallback, IntPtr.Zero);

        private bool EnumWindowCallback (WindowHandle handle, IntPtr lParam) {
            var wndType = GetWindowType(handle);
            switch( wndType ) {
                case WindowType.AppWindow:
                    Windows.Add(handle);
                    break;
                case WindowType.ToolWindow:
                    ToolWindows.Add(handle);
                    break;
                case WindowType.Hidden:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return true;
        }

        private enum WindowType {

            Hidden,
            AppWindow,
            ToolWindow

        }

        private enum WindowAttribute {

            Cloaked = 0xe

        }

        private delegate bool EnumWindowsProc (WindowHandle handle, IntPtr lParam);

        private static WindowType GetWindowType (WindowHandle handle) {
            int cloaked = IsCloaked(handle);
            if( cloaked > 0 ) {
                return WindowType.Hidden;
            }

            WindowStyles style = handle.GetWindowStyles();
            if( style.HasFlag(WindowStyles.Disabled) ) {
                return WindowType.Hidden;
            }
            if( !style.HasFlag(WindowStyles.Visible) ) {
                return WindowType.Hidden;
            }

            WindowExStyles ex = handle.GetWindowExStyles();
            if( ex.HasFlag(WindowExStyles.NoActivate) ) {
                return WindowType.Hidden;
            }
            if( ex.HasFlag(WindowExStyles.AppWindow) ) {
                return WindowType.AppWindow;
            }
            if( ex.HasFlag(WindowExStyles.ToolWindow) ) {
                return WindowType.ToolWindow;
            }
            var title = GetWindowTitle(handle.handle);
            return IsAltTabWindow(handle) ? WindowType.AppWindow : WindowType.Hidden;
        }

        private static int IsCloaked (WindowHandle window) {
            DwmGetWindowAttribute(window, WindowAttribute.Cloaked, out int cloaked, Marshal.SizeOf(typeof(bool)));
            return cloaked;
        }

        private static bool IsAltTabWindow(WindowHandle hwnd) {
            var lastActivePop = GetLastActiveVisiblePopup(GetAncestor(hwnd, 3));
            return lastActivePop == hwnd;
        }

        private static WindowHandle GetLastActiveVisiblePopup (WindowHandle root) {
            WindowHandle hwndWalk = WindowHandle.Null;
            WindowHandle hwndTry = root;
            while( hwndWalk != hwndTry ) {
                hwndWalk = hwndTry;
                hwndTry = GetLastActivePopup(hwndWalk);
                if( IsWindowVisible(hwndTry) ) {
                    return hwndTry;
                }
            }
            return WindowHandle.Null;
        }

        public static string GetWindowTitle(IntPtr hWnd)
        {
            var length = GetWindowTextLength(hWnd) + 1;
            var title = new StringBuilder(length);
            GetWindowText(hWnd, title, length);
            return title.ToString();
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows (EnumWindowsProc enumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern WindowHandle GetAncestor (WindowHandle hWnd, int gaFlags);

        [DllImport("user32.dll")]
        private static extern WindowHandle GetLastActivePopup (WindowHandle hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible (WindowHandle hWnd);

        [DllImport("dwmapi.dll")]
        private static extern int DwmGetWindowAttribute (WindowHandle hWnd, WindowAttribute dwAttribute, out int pvAttribute, int cbAttribute);

    }

}
