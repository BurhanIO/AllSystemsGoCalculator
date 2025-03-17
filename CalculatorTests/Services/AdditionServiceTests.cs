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
        var addends = new List<int>();
        var additionService = CreateAdditionService();
        
        var result = additionService.AddIntegers(addends);

        result.Should().Be(0);
    }
    
    [TestMethod]
    public void AddIntegers_OneAddend_ShouldReturnTheAddend()
    {
        var addends = new List<int>{1};
        var additionService = CreateAdditionService();
        
        var result = additionService.AddIntegers(addends);

        result.Should().Be(1);
    }
    
    [TestMethod]
    public void AddIntegers_TwoAddends_ShouldReturnTheSumOfTheTwo()
    {
        var addends = new List<int>{1, 2};
        var additionService = CreateAdditionService();
        
        var result = additionService.AddIntegers(addends);

        result.Should().Be(3);
    }
    
    [TestMethod]
    public void AddIntegers_MoreThanTwoAddends_ShouldThrowArgumentOutOfRangeException()
    {
        var addends = new List<int> {1,2,3};
        var additionService = CreateAdditionService();
        
        var result = () => additionService.AddIntegers(addends);

        result.Should().Throw<ArgumentOutOfRangeException>().WithMessage("Only two addends are supported (Parameter 'addends')");
    }
}