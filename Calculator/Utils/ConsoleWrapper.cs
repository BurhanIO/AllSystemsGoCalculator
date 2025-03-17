namespace Calculator.Utils;

public class ConsoleWrapper : IConsoleWrapper
{
    public void WriteLine(string? value)
    {
        Console.WriteLine(value);
    }
    
    public void WriteLine(int value)
    {
        Console.WriteLine(value);
    }
    
    public string? ReadLine()
    {
        return Console.ReadLine();
    }
}