using System.Runtime.InteropServices;

namespace KafMouseMacro.Services;

internal static class HotkeyManager
{
    private const int WmHotkey = 0x0312;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    public static bool Register(IntPtr hWnd, int id, uint vk, uint modifiers = 0)
    {
        return RegisterHotKey(hWnd, id, modifiers, vk);
    }

    public static bool Unregister(IntPtr hWnd, int id)
    {
        return UnregisterHotKey(hWnd, id);
    }

    public static void UnregisterAll(IntPtr hWnd, params int[] ids)
    {
        foreach (var id in ids)
        {
            UnregisterHotKey(hWnd, id);
        }
    }

    public static bool TryProcessMessage(ref Message m, out int hotkeyId)
    {
        hotkeyId = 0;
        if (m.Msg == WmHotkey)
        {
            hotkeyId = (int)m.WParam;
            return true;
        }
        return false;
    }
}
