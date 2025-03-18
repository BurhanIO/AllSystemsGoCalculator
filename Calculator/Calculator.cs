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
        var basicMathOptions = new BasicMathOptions();

        var alternativeDelimiter = _userInputService.GetChar("Enter an alternative delimiter: ");
        basicMathOptions.AllowNegativeNumbers = _userInputService.GetBoolean("Allow negative numbers?");
        basicMathOptions.MaxNumber = _userInputService.GetNumber(
            $"Max number?{(basicMathOptions.AllowNegativeNumbers ? "" : " (Negatives not allowed)")}",
            basicMathOptions.AllowNegativeNumbers
        );
        
        while (true)
        {
            try
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

                _consoleWrapper.WriteLine($"{string.Join(GetOperationSymbol(basicMathOptions.Operation), numbers)} = {sum}");
            }
            catch(Exception e)
            {
                _consoleWrapper.WriteLine(e.Message);
            }
        }
    }

    private static char GetOperationSymbol(MathOperationType mathOperationType)
    {
        return mathOperationType switch
        {
            MathOperationType.Addition => '+',
            MathOperationType.Subtraction => '-',
            MathOperationType.Multiplication => '*',
            MathOperationType.Division => '/',
            _ => '?'
        };
    }
}