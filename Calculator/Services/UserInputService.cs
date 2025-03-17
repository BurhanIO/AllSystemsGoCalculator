using Calculator.Exceptions;
using Calculator.Utils;

namespace Calculator.Services;

public class UserInputService : IUserInputService
{
    private const string CustomDelimitersStartStr = "//";
    private const string CustomDelimitersEndStr = "\\n";
    private const string SingleCustomDelimiterStartStr = "[";
    private const string SingleCustomDelimiterEndStr = "]";
    
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

        if (HasCustomDelimiters(userInput))
        {
            AddCustomDelimitersToList(userInput);
            userInput = RemoveCustomDelimiters(userInput);
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

    private bool HasCustomDelimiters(string userInput)
    {
        return userInput.StartsWith(CustomDelimitersStartStr) && userInput.Contains(CustomDelimitersEndStr);
    }
    
    private void AddCustomDelimitersToList(string userInput)
    {
        var delimiters = userInput.Substring(CustomDelimitersStartStr.Length, userInput.IndexOf(CustomDelimitersEndStr)-CustomDelimitersStartStr.Length);
        if (delimiters.Length < 1)
        {
            throw new InvalidDelimiterException("No delimiters found.");
        }

        var delimitersList = delimiters.Split([SingleCustomDelimiterStartStr, SingleCustomDelimiterEndStr], StringSplitOptions.RemoveEmptyEntries);
        if (delimitersList.Length < 1)
        {
            throw new InvalidDelimiterException("No delimiters found.");
        }

        _integerDelimiters = _integerDelimiters.Concat(delimitersList).ToArray();
    }
    
    private string RemoveCustomDelimiters(string userInput)
    {
        var delimiterEndStEnd = userInput.IndexOf(CustomDelimitersEndStr) + CustomDelimitersEndStr.Length;
       return userInput.Substring(delimiterEndStEnd, userInput.Length-delimiterEndStEnd);
    }
}