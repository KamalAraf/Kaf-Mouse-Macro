using System.Runtime.InteropServices;

namespace KafMouseMacro.Services;

internal sealed class MacroEngine
{
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll")]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

    private const uint MouseEventLeftDown = 0x0002;
    private const uint MouseEventLeftUp = 0x0004;

    public async Task StartAsync(IReadOnlyList<Models.MacroPoint> points, int delayMs, CancellationToken cancellationToken)
    {
        if (points.Count == 0)
            return;

        while (!cancellationToken.IsCancellationRequested)
        {
            for (int i = 0; i < points.Count; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                SetCursorPos(points[i].X, points[i].Y);
                mouse_event(MouseEventLeftDown | MouseEventLeftUp, 0, 0, 0, UIntPtr.Zero);

                await Task.Delay(delayMs, cancellationToken);
            }
        }
    }
}
