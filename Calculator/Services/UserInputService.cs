using Calculator.Exceptions;
using Calculator.Utils;

namespace Calculator.Services;

public class UserInputService : IUserInputService
{
    private const string CustomDelimitersStartStr = "//";
    private const string CustomDelimitersEndStr = "\\n";
    private const string SingleCustomDelimiterStartStr = "[";
    private const string SingleCustomDelimiterEndStr = "]";
    private const string TrueString = "Y";
    private const string FalseString = "N";
    
    private string[] _integerDelimiters = [","];
    
    private readonly IConsoleWrapper _consoleWrapper;

    public UserInputService(IConsoleWrapper consoleWrapper)
    {
        _consoleWrapper = consoleWrapper;
    }

    public bool GetBoolean(string userPrompt)
    {
        bool? value = null;
        do
        {
            var userInput= GetUserInput($"{userPrompt} (Y/N)");
            
            if (string.IsNullOrWhiteSpace(userInput))
            {
                continue;
            }
            if (userInput.Equals(TrueString, StringComparison.InvariantCultureIgnoreCase))
            {
                value = true;
            }
            if (userInput.Equals(FalseString, StringComparison.InvariantCultureIgnoreCase))
            {
                value = false;
            }
        } while (value == null);

        return value.Value;
    }

    public int GetInteger(string userPrompt)
    {
        int? value = null;
        do
        {
            var userInput= GetUserInput(userPrompt);
            if (string.IsNullOrWhiteSpace(userInput))
            {
                continue;
            }

            if (int.TryParse(userInput, out var integer))
            {
                value = integer;
            }
        } while (value == null);

        return value.Value;
    }
    
    public char GetChar(string userPrompt)
    {
        char? value = null;
        do
        {
            var userInput= GetUserInput(userPrompt);
            if (string.IsNullOrWhiteSpace(userInput))
            {
                continue;
            }

            if (userInput.Length == 1)
            {
                value = userInput[0];
            }
        } while (value == null);

        return value.Value;
    }
    
    public List<int> GetIntegers(string userPrompt, string alternativeDelimiter = "\\n")
    {
        AddAlternativeDelimiter(alternativeDelimiter);
        
        var integers = new List<int>();
        
        var userInput = GetUserInput(userPrompt);
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
        var delimiters = userInput.Substring(
            CustomDelimitersStartStr.Length,
            userInput.IndexOf(CustomDelimitersEndStr)-CustomDelimitersStartStr.Length);
        if (delimiters.Length < 1)
        {
            throw new InvalidDelimiterException("No delimiters found.");
        }

        var delimitersList = delimiters.Split(
            [SingleCustomDelimiterStartStr, SingleCustomDelimiterEndStr],
            StringSplitOptions.RemoveEmptyEntries);
        if (delimitersList.Length < 1)
        {
            throw new InvalidDelimiterException("No delimiters found.");
        }

        _integerDelimiters = _integerDelimiters.Concat(delimitersList).ToArray();
    }
    
    private void AddAlternativeDelimiter(string alternativeDelimiter)
    {
        _integerDelimiters = _integerDelimiters.Append(alternativeDelimiter).ToArray();
    }
    
    private string RemoveCustomDelimiters(string userInput)
    {
        var delimiterEndStEnd = userInput.IndexOf(CustomDelimitersEndStr) + CustomDelimitersEndStr.Length;
       return userInput.Substring(delimiterEndStEnd, userInput.Length-delimiterEndStEnd);
    }
}