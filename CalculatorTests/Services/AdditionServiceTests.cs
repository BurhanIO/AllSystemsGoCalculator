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

        var allowNegativeNumbers = false;
        var maxAddend = 1000;
        
        var additionService = CreateAdditionService();
        
        var (addends, sum) = additionService.AddIntegers(addendsInput, allowNegativeNumbers, maxAddend);
        addends.Count.Should().Be(0);
        sum.Should().Be(0);
    }
    
    [TestMethod]
    public void AddIntegers_OneAddend_ShouldReturnTheAddend()
    {
        var addendsInput = new List<int>{1};
        var allowNegativeNumbers = false;
        var maxAddend = 1000;
        
        var additionService = CreateAdditionService();
        
        var (addends, sum) = additionService.AddIntegers(addendsInput, allowNegativeNumbers, maxAddend);
        addends.Count.Should().Be(1);
        sum.Should().Be(1);
    }
    
    [TestMethod]
    public void AddIntegers_TwoAddends_ShouldReturnTheSumOfTheTwo()
    {
        var addendsInput = new List<int>{1, 2};
        var allowNegativeNumbers = false;
        var maxAddend = 1000;
        
        var additionService = CreateAdditionService();
        
        var (addends, sum) = additionService.AddIntegers(addendsInput, allowNegativeNumbers, maxAddend);
        addends.Count.Should().Be(2);
        sum.Should().Be(3);
    }
    
    [TestMethod]
    public void AddIntegers_MoreThanTwoAddends_ShouldReturnTheSumOfAll()
    {
        var addendsInput = new List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9, 10 ,11 ,12};
        var allowNegativeNumbers = false;
        var maxAddend = 1000;
        
        var additionService = CreateAdditionService();
        
        var (addends, sum) = additionService.AddIntegers(addendsInput, allowNegativeNumbers, maxAddend);
        addends.Count.Should().Be(12);
        sum.Should().Be(78);
    }
    
    [TestMethod]
    public void AddIntegers_AddendsGreaterThanOneThousandAreNotUsedInTheCalculation_ShouldReturnTheSumOfNumberNotOverOneThousand()
    {
        var addendsInput = new List<int>{1, 2, 3, 1000, 1001, 20000};
        var allowNegativeNumbers = false;
        var maxAddend = 1000;
        
        var additionService = CreateAdditionService();
        
        var (addends, sum) = additionService.AddIntegers(addendsInput, allowNegativeNumbers, maxAddend);
        addends.Count.Should().Be(6);
        sum.Should().Be(1006);
    }
    
    [TestMethod]
    public void AddIntegers_NegativeNumbersInListWithAllowNegativeNumbersSetToFalse_ShouldThrowANegativeNumbersNotSupportedException()
    {
        var addendsInput = new List<int> {1,-2,-3};
        var allowNegativeNumbers = false;
        var maxAddend = 1000;
        
        var additionService = CreateAdditionService();
         
        var result = () => additionService.AddIntegers(addendsInput, allowNegativeNumbers, maxAddend);
        result.Should().Throw<NegativeNumbersNotSupportedException>().WithMessage("Negative numbers are not supported. [-2,-3]");
    }
    
    [TestMethod]
    public void AddIntegers_MaxAddendIsRespected_ResultFactorsInMaxAddend()
    {
        var addendsInput = new List<int> {1, 2, 3, 1000, 1001, 20000};
        var allowNegativeNumbers = false;
        var maxAddend = 1000;
        
        var additionService = CreateAdditionService();
         
        var (addends, sum) = additionService.AddIntegers(addendsInput, allowNegativeNumbers, maxAddend);
        addends.Count.Should().Be(6);
        addends[0].Should().Be(1);
        addends[1].Should().Be(2);
        addends[2].Should().Be(3);
        addends[3].Should().Be(1000);
        addends[4].Should().Be(0);
        addends[5].Should().Be(0);
        sum.Should().Be(1006);
    }
}