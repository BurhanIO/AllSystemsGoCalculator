using Calculator.Services;
using Calculator.Utils;
using FluentAssertions;
using Moq;

namespace CalculatorTests.Services;

[TestClass]
public sealed class UserInputServiceTests
{
    Mock<IConsoleWrapper> _mockConsoleWrapper;
    
    private UserInputService CreateUserInputService()
    {
        return new UserInputService(_mockConsoleWrapper.Object);
    }

    [TestInitialize]
    public void TestInitialize()
    {
        _mockConsoleWrapper = new Mock<IConsoleWrapper>();
    }

    [TestMethod]
    public void GetIntegers_NullInput_ShouldReturnAnEmptyList()
    {
        string? userInput = null;
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
            
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetIntegers();
        
        result.Should().BeEmpty();
    }

    
    [TestMethod]
    public void GetIntegers_EmptyInput_ShouldReturnAnEmptyList()
    {
        var userInput = string.Empty;
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
            
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetIntegers();
        
        result.Should().BeEmpty();
    }
    
    [TestMethod]
    public void GetIntegers_WhitespaceInput_ShouldReturnAnEmptyList()
    {
        var userInput = "   ";
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
            
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetIntegers();
        
        result.Should().BeEmpty();
    }
    
    [TestMethod]
    public void GetIntegers_TwoNonNumbersSeperatedByAComma_ShouldReturnTwoElementsBothZero()
    {
        var userInput = "ab,cd";
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
            
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetIntegers();
        
        result.Count.Should().Be(2);
        result[0].Should().Be(0);
        result[1].Should().Be(0);
    }
    
    [TestMethod]
    public void GetIntegers_OneNumberAndOneNonNumbersSeperatedByAComma_ShouldReturnAZeroAndTheNumberAddedToTheList()
    {
        var userInput = "ab,10";
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
            
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetIntegers();
        
        result.Count.Should().Be(2);
        result[0].Should().Be(0);
        result[1].Should().Be(10);
    }
    
    [TestMethod]
    public void GetIntegers_OneNumberAndOneNonNumbersSeperatedByACommaAndContainingWhiteSpace_ShouldReturnAZeroAndTheNumberAddedToTheList()
    {
        var userInput = " ab  ,   10  ";
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
            
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetIntegers();
        
        result.Count.Should().Be(2);
        result[0].Should().Be(0);
        result[1].Should().Be(10);
    }
    
    [TestMethod]
    public void GetIntegers_TwoNumbersSeparatedByAComma_ShouldReturnBothNumbersInTheList()
    {
        var userInput = "10,20";
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
            
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetIntegers();
        
        result.Count.Should().Be(2);
        result[0].Should().Be(10);
        result[1].Should().Be(20);
    }
    
    [TestMethod]
    public void GetIntegers_OnePositiveOneNegativeNumberSeparatedByAComma_ShouldReturnBothNumbersInTheList()
    {
        var userInput = "10,-20";
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
            
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetIntegers();
        
        result.Count.Should().Be(2);
        result[0].Should().Be(10);
        result[1].Should().Be(-20);
    }
    
    [TestMethod]
    public void GetIntegers_FiveNumbersSeparatedByAComma_ShouldReturnAllNumbersInTheList()
    {
        var userInput = "10,20,33,54,5";
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
            
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetIntegers();
        
        result.Count.Should().Be(5);
        result[0].Should().Be(10);
        result[1].Should().Be(20);
        result[2].Should().Be(33);
        result[3].Should().Be(54);
        result[4].Should().Be(5);
    }
}