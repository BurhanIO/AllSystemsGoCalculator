namespace Calculator.Services;

public interface IAdditionService
{
    (List<int>, int) AddIntegers(List<int> addends);
}