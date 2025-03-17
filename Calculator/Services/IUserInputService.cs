namespace Calculator.Services;

public interface IUserInputService
{
    bool GetBoolean(string userPrompt);
    int GetInteger(string userPrompt);
    char GetChar(string userPrompt);
    List<int> GetIntegers(string userInput, string alternativeDelimiter);
}