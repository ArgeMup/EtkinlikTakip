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
     
        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);
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

        //showCmd Değerleri
        //SW_HIDE             0 Hides the window and activates another window.
        //SW_NORMAL           1	Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
        //SW_SHOWMINIMIZED    2	Activates the window and displays it as a minimized window.
        //SW_MAXIMIZE         3	Activates the window and displays it as a maximized window.
        //SW_SHOWNOACTIVATE   4	Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except that the window is not activated.
        //SW_SHOW             5	Activates the window and displays it in its current size and position.
        //SW_MINIMIZE         6	Minimizes the specified window and activates the next top-level window in the Z order.
        //SW_SHOWMINNOACTIVE  7	Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.
        //SW_SHOWNA           8	Displays the window in its current size and position. This value is similar to SW_SHOW, except that the window is not activated.
        //SW_RESTORE          9	Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.
        //SW_SHOWDEFAULT      10 Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application.
        //SW_FORCEMINIMIZE    11 Minimizes a window, even if the thread that owns the window is not responding. This flag should only be used when minimizing windows from a different thread.
        
        public struct WINDOWPLACEMENT
        {
            public uint length;
            public uint flags;
            public uint showCmd;
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

    public static class W32_9
    {
        public const string Sürüm = "V0.0";

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
    }

    public static class W32_Konsol
    {
        public const string Sürüm = "V0.0";

        // [DllImport("kernel32.dll")]
        // public static extern bool AllocConsole(); //Doğrudan açmak için

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void AyrıPenceredeGöster(bool Evet)
        {
            const int SW_HIDE = 0;
            const int SW_SHOW = 5;
            var handle = GetConsoleWindow();

            if (Evet) ShowWindow(handle, SW_SHOW);
            else ShowWindow(handle, SW_HIDE);
        }
    }
}