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
        var addends = _userInputService.GetIntegers();
        var sum = _additionService.AddIntegers(addends);
        
        _consoleWrapper.WriteLine(sum);
    }
}