namespace Calculator.Services.BasicMath;

public interface IBasicMathService
{
    (List<double>, double) PerformOperation(List<double> numbers, BasicMathOptions options);
}