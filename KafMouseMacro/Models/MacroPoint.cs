namespace KafMouseMacro.Models;

public sealed class MacroPoint
{
    public int X { get; set; }
    public int Y { get; set; }

    public MacroPoint() { }

    public MacroPoint(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"({X}, {Y})";
}
