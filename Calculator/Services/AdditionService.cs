using Calculator.Exceptions;

namespace Calculator.Services;

public class AdditionService : IAdditionService
{
    public (List<int>, int) AddIntegers(List<int> addends, bool allowNegativeNumbers, int maxAddend)
    {
        var negativeAddends = addends.Where(a => a < 0).ToList();
        if (!allowNegativeNumbers && negativeAddends.Count > 0)
        {
            throw new NegativeNumbersNotSupportedException($"Negative numbers are not supported. [{string.Join( ",", negativeAddends)}]");
        }

        for (var i = 0; i < addends.Count; i++)
        {
            if (addends[i] > maxAddend)
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