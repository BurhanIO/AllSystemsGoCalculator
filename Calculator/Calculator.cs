using Calculator.Services;
using Calculator.Utils;

namespace Calculator;

public class Calculator
{
    private readonly IUserInputService _userInputService;
    private readonly IAdditionService _additionService;
    private readonly IConsoleWrapper _consoleWrapper;
    
    public Calculator(
        IUserInputService userInputService,
        IAdditionService additionService,
        IConsoleWrapper consoleWrapper)
    {
        _userInputService = userInputService;
        _additionService = additionService;
        _consoleWrapper = consoleWrapper;
    }
    
    public void Run()
    {
        var alternativeDelimiter = _userInputService.GetChar("Enter an alternative delimiter: ");
        var allowNegativeNumbers = _userInputService.GetBoolean("Allow negative numbers?");
        var maxAddend = _userInputService.GetInteger("Max addend?");
        while (true)
        {
            var inputAddends = _userInputService.GetIntegers(
                "Enter addends and optionally delimiters: ",
                alternativeDelimiter.ToString());
            var (addends, sum) = _additionService.AddIntegers(inputAddends, allowNegativeNumbers, maxAddend);
        
            _consoleWrapper.WriteLine($"{string.Join("+", addends)} = {sum}");   
        }
    }
}