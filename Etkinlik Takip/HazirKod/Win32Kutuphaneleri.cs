// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/HazirKod>

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace ArgeMup.HazirKod
{
    public static class W32_1
    {
        public const string Sürüm = "V0.0";

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string lpFileName);
    }

    public static class W32_2
    {
        public const string Sürüm = "V0.0";

        public delegate int Win32HookProcHandler(int nCode, IntPtr wParam, IntPtr lParam);
      
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, Win32HookProcHandler lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);
    }

    public static class W32_3
    {
        public const string Sürüm = "V0.0";

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }

    public static class W32_4
    {
        public const string Sürüm = "V0.0";

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);
    }

    public static class W32_5
    {
        public const string Sürüm = "V0.0";

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    }

    public static class W32_6
    {
        public const string Sürüm = "V0.0";

        public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);
    }

    public static class W32_7
    {
        public const string Sürüm = "V0.0";

        public struct WINDOWPLACEMENT
        {
            public uint length;
            public uint flags;
            public uint showCmd; //0 gizli, 1 normal, 2 mini, 3 maxi, 5 olduğu gibi
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        };

        [DllImport("user32.dll")]
        public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
    }

    public static class W32_8
    {
        public const string Sürüm = "V0.0";

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool DestroyIcon(IntPtr handle);
    }
}