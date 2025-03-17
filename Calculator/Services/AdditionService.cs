using Calculator.Exceptions;

namespace Calculator.Services;

public class AdditionService : IAdditionService
{
    private const int MaxAddend = 1000;
    public int AddIntegers(List<int> addends)
    {
        var negativeAddends = addends.Where(a => a < 0).ToList();
        if (negativeAddends.Any())
        {
            throw new NegativeNumbersNotSupportedException($"Negative numbers are not supported. [{string.Join( ",", negativeAddends)}]");
        }

        addends = addends.Where(a => a <= MaxAddend).ToList();
        
        return addends.Count switch
        {
            0 => 0,
            1 => addends[0],
            _ => addends.Sum()
        };
    }
}