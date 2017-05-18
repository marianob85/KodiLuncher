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
private const int WM_SYSKEYDOWN = 0x0104;
private	const int VK_LSHIFT = 0xA0;
private	const int VK_RSHIFT = 0xA1;
private	const int VK_LCONTROL = 0xA2;
private	const int VK_RCONTROL = 0xA3;
private	const int VK_LMENU = 0xA4;
private	const int VK_RMENU = 0xA5;

private	static LowLevelKeyboardProc m_proc = HookCallback;
private	static IntPtr m_hookID = IntPtr.Zero;

public KeyboardHook()
{
   // m_hookID = SetHook(m_proc);
}

public void Dispose()
{
   // UnhookWindowsHookEx(m_hookID);
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

    //Console.WriteLine("Code:   " +  nCode);
    //Console.WriteLine("lParam: " + Marshal.ReadInt32(lParam));
    //Console.WriteLine("wParam: " + (IntPtr)wParam );
    //Console.WriteLine("----");

    if (nCode >= 0 && wParam == (IntPtr)WM_SYSKEYDOWN)
    {
        int vkCode = Marshal.ReadInt32(lParam);

        if ( GetAsyncKeyState(Keys.LWin) !=0 && GetAsyncKeyState (Keys.Enter)!=0)
        {
            Console.WriteLine( "Triggered" );
            return IntPtr.Zero;
        }

       //Console.WriteLine(GetAsyncKeyState(Keys.LControlKey));
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

[DllImport("User32.dll")]
public static extern short GetAsyncKeyState(Keys ArrowKeys);
}
}

