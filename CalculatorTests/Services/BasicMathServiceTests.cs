using Calculator.Exceptions;
using Calculator.Services;
using Calculator.Services.BasicMath;
using FluentAssertions;

namespace CalculatorTests.Services;

[TestClass]
public sealed class BasicMathServiceTests
{
    private BasicMathService CreateAdditionService()
    {
        return new BasicMathService();
    }
    
    [TestInitialize]
    public void TestInitialize(){}

    [TestMethod]
    public void PerformOperation_NoNumbers_ShouldReturnZero()
    {
        var numbersInput = new List<double>();
        var options = new BasicMathOptions
        {
            AllowNegativeNumbers = false,
            MaxNumber = 1000,
            Operation = MathOperationType.Addition
        };
        
        var additionService = CreateAdditionService();
        
        var (numbers, sum) = additionService.PerformOperation(numbersInput, options);
        numbers.Count.Should().Be(0);
        sum.Should().Be(0);
    }
    
    [TestMethod]
    public void PerformOperation_OneNumber_ShouldReturnTheNumber()
    {
        var numbersInput = new List<double>{1};
        var options = new BasicMathOptions
        {
            AllowNegativeNumbers = false,
            MaxNumber = 1000,
            Operation = MathOperationType.Addition
        };
        
        var additionService = CreateAdditionService();
        
        var (numbers, sum) = additionService.PerformOperation(numbersInput, options);
        numbers.Count.Should().Be(1);
        sum.Should().Be(1);
    }
    
    [TestMethod]
    public void PerformOperation_TwoNumbers_ShouldReturnTheSumOfTheTwo()
    {
        var numbersInput = new List<double>{1, 2};
        var options = new BasicMathOptions
        {
            AllowNegativeNumbers = false,
            MaxNumber = 1000,
            Operation = MathOperationType.Addition
        };
        
        var additionService = CreateAdditionService();
        
        var (numbers, sum) = additionService.PerformOperation(numbersInput, options);
        numbers.Count.Should().Be(2);
        sum.Should().Be(3);
    }
    
    [TestMethod]
    public void PerformOperation_MoreThanTwoNumbers_ShouldReturnTheResultOfAll()
    {
        var numbersInput = new List<double>{1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ,11 ,12};
        var options = new BasicMathOptions
        {
            AllowNegativeNumbers = false,
            MaxNumber = 1000,
            Operation = MathOperationType.Addition
        };
        
        var additionService = CreateAdditionService();
        
        var (numbers, sum) = additionService.PerformOperation(numbersInput, options);
        numbers.Count.Should().Be(12);
        sum.Should().Be(78);
    }
    
    [TestMethod]
    public void PerformOperation_NumbersGreaterThanOneThousandAreNotUsedInTheCalculation_ShouldReturnTheSumOfNumberNotOverOneThousand()
    {
        var numbersInput = new List<double>{1, 2, 3, 1000, 1001, 20000};
        var options = new BasicMathOptions
        {
            AllowNegativeNumbers = false,
            MaxNumber = 1000,
            Operation = MathOperationType.Addition
        };
        
        var additionService = CreateAdditionService();
        
        var (numbers, sum) = additionService.PerformOperation(numbersInput, options);
        sum.Should().Be(1006.0);
    }
    
    [TestMethod]
    public void PerformOperation_NegativeNumbersInListWithAllowNegativeNumbersSetToFalse_ShouldThrowANegativeNumbersNotSupportedException()
    {
        var numbersInput = new List<double> {1,-2,-3};
        var options = new BasicMathOptions
        {
            AllowNegativeNumbers = false,
            MaxNumber = 1000,
            Operation = MathOperationType.Addition
        };
        
        var additionService = CreateAdditionService();
         
        var result = () => additionService.PerformOperation(numbersInput, options);
        result.Should().Throw<NegativeNumbersNotSupportedException>().WithMessage("Negative numbers are not supported. [-2,-3]");
    }
    
    [TestMethod]
    public void PerformOperation_MaxNumberIsRespected_ResultFactorsInMaxNumber()
    {
        var numbersInput = new List<double> {1, 2, 3, 1000, 1001, 20000};
        var options = new BasicMathOptions
        {
            AllowNegativeNumbers = false,
            MaxNumber = 1000,
            Operation = MathOperationType.Addition
        };
        
        var additionService = CreateAdditionService();
         
        var (numbers, sum) = additionService.PerformOperation(numbersInput, options);
        numbers.Count.Should().Be(6);
        numbers[0].Should().Be(1);
        numbers[1].Should().Be(2);
        numbers[2].Should().Be(3);
        numbers[3].Should().Be(1000);
        numbers[4].Should().Be(0);
        numbers[5].Should().Be(0);
        sum.Should().Be(1006);
    }
    
    [TestMethod]
    public void PerformOperation_SubtractNumbers_ShouldReturnTheResultOfAll()
    {
        var numbersInput = new List<double>{3, 2, 1};
        var options = new BasicMathOptions
        {
            AllowNegativeNumbers = false,
            MaxNumber = 1000,
            Operation = MathOperationType.Subtraction
        };
        
        var additionService = CreateAdditionService();
        
        var (numbers, sum) = additionService.PerformOperation(numbersInput, options);
        numbers.Count.Should().Be(3);
        sum.Should().Be(0);
    }
    
    [TestMethod]
    public void PerformOperation_MultiplyNumbers_ShouldReturnTheResultOfAll()
    {
        var numbersInput = new List<double>{3, 2, 1};
        var options = new BasicMathOptions
        {
            AllowNegativeNumbers = false,
            MaxNumber = 1000,
            Operation = MathOperationType.Multiplication
        };
        
        var additionService = CreateAdditionService();
        
        var (numbers, sum) = additionService.PerformOperation(numbersInput, options);
        numbers.Count.Should().Be(3);
        sum.Should().Be(6);
    }
    
    [TestMethod]
    public void PerformOperation_DivideNumbers_ShouldReturnTheResultOfAll()
    {
        var numbersInput = new List<double>{1,2,2};
        var options = new BasicMathOptions
        {
            AllowNegativeNumbers = false,
            MaxNumber = 1000,
            Operation = MathOperationType.Division
        };
        
        var additionService = CreateAdditionService();
        
        var (numbers, sum) = additionService.PerformOperation(numbersInput, options);
        numbers.Count.Should().Be(3);
        sum.Should().Be(.25);
    }
}