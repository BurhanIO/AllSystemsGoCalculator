namespace Calculator.Utils;

public interface IConsoleWrapper
{
    public void WriteLine(string? value);
    public void WriteLine(int value);
    public string? ReadLine();
}