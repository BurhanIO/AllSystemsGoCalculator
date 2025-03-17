using Calculator.Exceptions;
using Calculator.Utils;

namespace Calculator.Services;

public class UserInputService : IUserInputService
{
    private const string CustomDelimiterStartStr = "//[";
    private const string CustomDelimiterEndStr = "]\\n";
    private string[] _integerDelimiters = [",", "\\n"];
    
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

        if (HasCustomDelimiter(userInput))
        {
            AddCustomDelimiterToList(userInput);
            userInput = RemoveCustomDelimiter(userInput);
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
        return _consoleWrapper.ReadLine()?.Trim();
    }

    private List<string> SplitAndTrimUserInput(string userInput)
    {
        return userInput.Split(_integerDelimiters, StringSplitOptions.TrimEntries).ToList();
    }

    private bool HasCustomDelimiter(string userInput)
    {
        return userInput.StartsWith(CustomDelimiterStartStr) && userInput.Contains(CustomDelimiterEndStr);
    }
    
    private void AddCustomDelimiterToList(string userInput)
    {
        var delimiter = userInput.Substring(3, userInput.IndexOf(CustomDelimiterEndStr)-3);
        if (delimiter.Length < 1)
        {
            throw new InvalidDelimiterException("Delimiter is empty.");
        }
        _integerDelimiters = _integerDelimiters.Append(delimiter).ToArray();
    }
    
    private string RemoveCustomDelimiter(string userInput)
    {
        var delimiterEndStEnd = userInput.IndexOf(CustomDelimiterEndStr) + 3;
       return userInput.Substring(delimiterEndStEnd, userInput.Length-delimiterEndStEnd);
    }
}