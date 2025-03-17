using Calculator.Exceptions;

namespace Calculator.Services.BasicMath;

public class BasicMathService : IBasicMathService
{
    public (List<double>, double) PerformOperation(List<double> numbers, BasicMathOptions options)
    {
        numbers = ValidateAndPrepareNumbers(numbers, options);
        var result = Calculate(numbers, options);
        return (numbers, result);
    }
    
    private List<double> ValidateAndPrepareNumbers(List<double> numbers, BasicMathOptions options)
    {
        var negativeNumbers = numbers.Where(a => a < 0).ToList();
        if (!options.AllowNegativeNumbers && negativeNumbers.Count > 0)
        {
            throw new NegativeNumbersNotSupportedException($"Negative numbers are not supported. [{string.Join( ",", negativeNumbers)}]");
        }

        for (var i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] > options.MaxNumber)
            {
                numbers[i] = 0;
            }
        }
        
        return numbers;
    }
    
    private double Calculate(List<double> numbers, BasicMathOptions options)
    {
        switch (numbers.Count())
        {
            case 0:
                return 0;
            case 1:
                return numbers[0];
            default:
                switch (options.Operation)
                {
                    case MathOperationType.Addition:
                        var result = 0.0;
                        numbers.ForEach(number => result += number);
                        return result;
                    case MathOperationType.Subtraction:
                        result = numbers[0];
                        for (var i = 1; i < numbers.Count; i++)
                        {
                            result -= numbers[i];
                        }
                        return result;
                    case MathOperationType.Multiplication:
                        result = 1.0;
                        numbers.ForEach(number => result *= number);
                        return result;
                    case MathOperationType.Division:
                        result = numbers[0];
                        for (var i = 1; i < numbers.Count; i++)
                        {
                            result /= numbers[i];
                        }
                        return result;
                }
                break;
        }
        return 0.0;
    }
}