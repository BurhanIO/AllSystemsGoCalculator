using Calculator.Services;
using Calculator.Utils;

namespace Calculator;

public class Program
{
    public static void Main(string[] args)
    {
        var consoleWrapper = new ConsoleWrapper();
        var userInputService = new UserInputService(consoleWrapper);
        var additionService = new AdditionService();
        
        var calculator = new Calculator(userInputService, additionService, consoleWrapper);
        
        calculator.Run();
    }
}