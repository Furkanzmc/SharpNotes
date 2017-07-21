using System;
using System.Runtime.InteropServices;

internal class NativeMethods
{
    public const int WM_COPYDATA = 0x4A;
    [DllImport("user32")]
    public static extern int SendMessage(int hwnd, int msg, int wparam, ref COPYDATASTRUCT lparam);


    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData;
    }

    public static int sendWindowsStringMessage(int hWnd, int wParam, string msg)
    {
        int result;
        byte[] sarr = System.Text.Encoding.Default.GetBytes(msg);
        int len = sarr.Length;
        COPYDATASTRUCT cds;
        cds.dwData = (IntPtr)100;
        cds.lpData = msg;
        cds.cbData = len + 1;
        result = SendMessage(hWnd, WM_COPYDATA, wParam, ref cds);
        return result;
    }
}
