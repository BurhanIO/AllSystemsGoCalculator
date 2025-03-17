namespace Calculator.Services;

public interface IUserInputService
{
    bool GetBoolean(string userPrompt);
    double GetNumber(string userPrompt);
    char GetChar(string userPrompt);
    List<double> GetNumbers(string userInput, string alternativeDelimiter);
}