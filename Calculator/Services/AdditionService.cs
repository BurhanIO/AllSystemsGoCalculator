using Calculator.Exceptions;

namespace Calculator.Services;

public class AdditionService : IAdditionService
{
    private const int MaxAddend = 1000;
    public (List<int>, int) AddIntegers(List<int> addends)
    {
        var negativeAddends = addends.Where(a => a < 0).ToList();
        if (negativeAddends.Any())
        {
            throw new NegativeNumbersNotSupportedException($"Negative numbers are not supported. [{string.Join( ",", negativeAddends)}]");
        }

        for (int i = 0; i < addends.Count; i++)
        {
            if (addends[i] > MaxAddend)
            {
                addends[i] = 0;
            }
        }
        
        return addends.Count switch
        {
            0 => (addends, 0),
            1 => (addends, addends[0]),
            _ => (addends, addends.Sum())
        };
    }
}