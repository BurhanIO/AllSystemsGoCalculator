using Calculator.Exceptions;
using Calculator.Services;
using FluentAssertions;

namespace CalculatorTests.Services;

[TestClass]
public sealed class AdditionServiceTests
{
    private AdditionService CreateAdditionService()
    {
        return new AdditionService();
    }
    
    [TestInitialize]
    public void TestInitialize(){}

    [TestMethod]
    public void AddIntegers_NoAddends_ShouldReturnZero()
    {
        var addendsInput = new List<int>();
        var additionService = CreateAdditionService();
        
        var (addends, sum) = additionService.AddIntegers(addendsInput);

        addends.Count.Should().Be(0);
        sum.Should().Be(0);
    }
    
    [TestMethod]
    public void AddIntegers_OneAddend_ShouldReturnTheAddend()
    {
        var addendsInput = new List<int>{1};
        var additionService = CreateAdditionService();
        
        var (addends, sum) = additionService.AddIntegers(addendsInput);

        sum.Should().Be(1);
    }
    
    [TestMethod]
    public void AddIntegers_TwoAddends_ShouldReturnTheSumOfTheTwo()
    {
        var addendsInput = new List<int>{1, 2};
        var additionService = CreateAdditionService();
        
        var (addends, sum) = additionService.AddIntegers(addendsInput);

        addends.Count.Should().Be(2);
        sum.Should().Be(3);
    }
    
    [TestMethod]
    public void AddIntegers_MoreThanTwoAddends_ShouldReturnTheSumOfAll()
    {
        var addendsInput = new List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ,11 ,12};
        var additionService = CreateAdditionService();
        
        var (addends, sum) = additionService.AddIntegers(addendsInput);

        addends.Count.Should().Be(12);
        sum.Should().Be(78);
    }
    
    [TestMethod]
    public void AddIntegers_AddendsGreaterThanOneThousandAreNotUsedInTheCalculation_ShouldReturnTheSumOfNumberNotOverOneThousand()
    {
        var addendsInput = new List<int>{1, 2, 3, 1000, 1001, 20000};
        var additionService = CreateAdditionService();
        
        var (addends, sum) = additionService.AddIntegers(addendsInput);

        addends.Count.Should().Be(6);
        sum.Should().Be(1006);
    }
    
    [TestMethod]
    public void AddIntegers_NegativeNumbersInList_ShouldthrowANegativeNumbersNotSupportedException()
    {
        var addendsInput = new List<int> {1,-2,-3};
        var additionService = CreateAdditionService();
         
        var result = () => additionService.AddIntegers(addendsInput);
 
        result.Should().Throw<NegativeNumbersNotSupportedException>().WithMessage("Negative numbers are not supported. [-2,-3]");
    }
}