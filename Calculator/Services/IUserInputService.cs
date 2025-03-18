namespace Calculator.Services;

public interface IUserInputService
{
    bool GetBoolean(string userPrompt);
    double GetNumber(string userPrompt, bool allowNegative = true);
    char GetChar(string userPrompt);
    List<double> GetNumbers(string userInput, string alternativeDelimiter);
}