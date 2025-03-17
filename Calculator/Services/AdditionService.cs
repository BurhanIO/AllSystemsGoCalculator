namespace Calculator.Services;

public class AdditionService : IAdditionService
{
    public int AddIntegers(List<int> addends)
    {
        return addends.Count switch
        {
            0 => 0,
            1 => addends[0],
            2 => addends[0] + addends[1],
            _ => throw new ArgumentOutOfRangeException(nameof(addends), "Only two addends are supported")
        };
    }
}