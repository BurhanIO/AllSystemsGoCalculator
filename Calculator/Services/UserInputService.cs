using Calculator.Utils;

namespace Calculator.Services;

public class UserInputService : IUserInputService
{
    private const char ListDelimiter = ',';
    
    private readonly IConsoleWrapper _consoleWrapper;

    public UserInputService(IConsoleWrapper consoleWrapper)
    {
        _consoleWrapper = consoleWrapper;
    }
    
    public List<int> GetIntegers()
    {
        var integers = new List<int>();
        
        var userInput = GetUserInput("Enter integers separated by a comma: ");
        if (string.IsNullOrWhiteSpace(userInput))
        {
            return integers;
        }

        var inputList = SplitAndTrimUserInput(userInput);
        inputList.ForEach(input =>
        {
            int.TryParse(input, out var integer);
            integers.Add(integer); 
        });
        
        return integers;
    }
    
    private string? GetUserInput(string userPrompt)
    {
        _consoleWrapper.WriteLine(userPrompt);
        return _consoleWrapper.ReadLine();
    }

    private static List<string> SplitAndTrimUserInput(string userInput)
    {
        return userInput.Split(ListDelimiter).Select(a => a.Trim()).ToList();
    }
}