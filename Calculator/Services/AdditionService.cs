using Calculator.Exceptions;

namespace Calculator.Services;

public class AdditionService : IAdditionService
{
    public int AddIntegers(List<int> addends)
    {
        var negativeAddends = addends.Where(a => a < 0).ToList();
        if (negativeAddends.Any())
        {
            throw new NegativeNumbersNotSupportedException($"Negative numbers are not supported. [{string.Join( ",", negativeAddends)}]");
        }
        return addends.Count switch
        {
            0 => 0,
            1 => addends[0],
            _ => addends.Sum()
        };
    }
}