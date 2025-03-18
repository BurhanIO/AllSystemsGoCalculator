namespace Calculator.Utils;

public interface IConsoleWrapper
{
    public void WriteLine(string? value);
    public string? ReadLine();
}