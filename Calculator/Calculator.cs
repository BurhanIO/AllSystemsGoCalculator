using Calculator.Services;
using Calculator.Services.BasicMath;
using Calculator.Utils;

namespace Calculator;

public class Calculator : ICalculator
{
    private readonly IUserInputService _userInputService;
    private readonly IBasicMathService _basicMathService;
    private readonly IConsoleWrapper _consoleWrapper;
    
    public Calculator(
        IUserInputService userInputService,
        IBasicMathService basicMathService,
        IConsoleWrapper consoleWrapper)
    {
        _userInputService = userInputService;
        _basicMathService = basicMathService;
        _consoleWrapper = consoleWrapper;
    }
    
    public void Run()
    {
        var alternativeDelimiter = _userInputService.GetChar("Enter an alternative delimiter: ");
        var basicMathOptions = new BasicMathOptions
        {
            AllowNegativeNumbers = _userInputService.GetBoolean("Allow negative numbers?"),
            MaxNumber = _userInputService.GetNumber("Max number?"),
        };
        
        while (true)
        {
            var operationTypeInt = -1;
            while (!Enum.IsDefined(typeof(MathOperationType), operationTypeInt))
            {
                operationTypeInt = (int)_userInputService.GetNumber(
                    "Operation type? (0 for Add, 1 for Subtract, 2 for Multiply, 3 for Divide)");   
            }
            basicMathOptions.Operation = (MathOperationType)operationTypeInt;
            
            var inputNumbers = _userInputService.GetNumbers(
                "Enter numbers and optionally delimiters: ",
                alternativeDelimiter.ToString());
            
            var (numbers, sum) = _basicMathService.PerformOperation(inputNumbers, basicMathOptions);
        
            _consoleWrapper.WriteLine($"{string.Join("+", numbers)} = {sum}");   
        }
    }
}