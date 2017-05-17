using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

namespace KLuncher
{
class KeyboardHook : IDisposable
{
private const int WH_KEYBOARD_LL = 13;
private	const int WM_KEYDOWN = 0x0100;
private	static LowLevelKeyboardProc m_proc = HookCallback;
private	static IntPtr m_hookID = IntPtr.Zero;

public KeyboardHook()
{
    m_hookID = SetHook(m_proc);
}

public void Dispose()
{
    UnhookWindowsHookEx(m_hookID);
}

private	static IntPtr SetHook( LowLevelKeyboardProc proc )
{
    using( Process curProcess = Process.GetCurrentProcess() )

    using (ProcessModule curModule = curProcess.MainModule)
    {
        return SetWindowsHookEx(WH_KEYBOARD_LL,
                                 proc,
                                 GetModuleHandle(curModule.ModuleName),
                                 0);
    }
}

private	delegate IntPtr LowLevelKeyboardProc( int nCode, IntPtr wParam, IntPtr lParam );
private static IntPtr HookCallback( int nCode, IntPtr wParam, IntPtr lParam )
{
    if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
    {
        int vkCode = Marshal.ReadInt32(lParam);

        //Console.WriteLine( ( Keys )vkCode );
    }

	return CallNextHookEx( m_hookID, nCode, wParam, lParam );
}

[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
[return: MarshalAs(UnmanagedType.Bool)]
private static extern bool UnhookWindowsHookEx(IntPtr hhk);

[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
private static extern IntPtr GetModuleHandle(string lpModuleName);
}
}
