using Calculator.Exceptions;
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
    public void GetNumbers_NullInput_ShouldReturnAnEmptyList()
    {
        var userPrompt = "Enter Value";
        string? userInput = null;
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumbers(userPrompt);
        result.Should().BeEmpty();
    }

    
    [TestMethod]
    public void GetNumbers_EmptyInput_ShouldReturnAnEmptyList()
    {
        var userInput = string.Empty;
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumbers(userPrompt);
        result.Should().BeEmpty();
    }
    
    [TestMethod]
    public void GetNumbers_WhitespaceInput_ShouldReturnAnEmptyList()
    {
        var userInput = "   ";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumbers(userPrompt);
        result.Should().BeEmpty();
    }
    
    [TestMethod]
    public void GetNumbers_TwoNonNumbersSeperatedByAComma_ShouldReturnTwoElementsBothZero()
    {
        var userInput = "ab,cd";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumbers(userPrompt);
        result.Count.Should().Be(2);
        result[0].Should().Be(0);
        result[1].Should().Be(0);
    }
    
    [TestMethod]
    public void GetNumbers_OneNumberAndOneNonNumbersSeperatedByAComma_ShouldReturnAZeroAndTheNumberAddedToTheList()
    {
        var userInput = "ab,10";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumbers(userPrompt);
        result.Count.Should().Be(2);
        result[0].Should().Be(0);
        result[1].Should().Be(10);
    }
    
    [TestMethod]
    public void GetNumbers_OneNumberAndOneNonNumbersSeperatedByACommaAndContainingWhiteSpace_ShouldReturnAZeroAndTheNumberAddedToTheList()
    {
        var userPrompt = "Enter Value";
        var userInput = " ab  ,   10  ";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumbers(userPrompt);
        result.Count.Should().Be(2);
        result[0].Should().Be(0);
        result[1].Should().Be(10);
    }
    
    [TestMethod]
    public void GetNumbers_TwoNumbersSeparatedByAComma_ShouldReturnBothNumbersInTheList()
    {
        var userPrompt = "Enter Value";
        var userInput = "10,20";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumbers(userPrompt);
        result.Count.Should().Be(2);
        result[0].Should().Be(10);
        result[1].Should().Be(20);
    }
    
    [TestMethod]
    public void GetNumbers_OnePositiveOneNegativeNumberSeparatedByAComma_ShouldReturnBothNumbersInTheList()
    {
        var userInput = "10,-20";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumbers(userPrompt);
        
        result.Count.Should().Be(2);
        result[0].Should().Be(10);
        result[1].Should().Be(-20);
    }
    
    [TestMethod]
    public void GetNumbers_FiveNumbersSeparatedByAComma_ShouldReturnAllNumbersInTheList()
    {
        var userInput = "10,20,33,54,5";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumbers(userPrompt);
        result.Count.Should().Be(5);
        result[0].Should().Be(10);
        result[1].Should().Be(20);
        result[2].Should().Be(33);
        result[3].Should().Be(54);
        result[4].Should().Be(5);
    }
    
    [TestMethod]
    public void GetNumbers_FiveNumbersSeparatedByANewLineChar_ShouldReturnAllNumbersInTheList()
    {
        var userInput = "10\\n20\\n33\\n54\\n5";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumbers(userPrompt);
        result.Count.Should().Be(5);
        result[0].Should().Be(10);
        result[1].Should().Be(20);
        result[2].Should().Be(33);
        result[3].Should().Be(54);
        result[4].Should().Be(5);
    }
    
    [TestMethod]
    public void GetNumbers_FiveNumbersSeparatedByANewLineCharsAComma_ShouldReturnAllNumbersInTheList()
    {
        var userInput = "10,20\\n33,54\\n5";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumbers(userPrompt);
        result.Count.Should().Be(5);
        result[0].Should().Be(10);
        result[1].Should().Be(20);
        result[2].Should().Be(33);
        result[3].Should().Be(54);
        result[4].Should().Be(5);
    }
    
    [TestMethod]
    public void GetNumbers_NumbersSeparatedByOneCustomDelimiter_ShouldReturnAllNumbersInTheList()
    {
        var userInput = "//[###]\\n10###20###20";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumbers(userPrompt);
        result.Count.Should().Be(3);
        result[0].Should().Be(10);
        result[1].Should().Be(20);
        result[2].Should().Be(20);
    }
    
    [TestMethod]
    public void GetNumbers_NumbersSeparatedByMultipleCustomDelimiter_ShouldReturnAllNumbersInTheList()
    {
        var userInput = "//[*][!!][r9r]\\n11r9r22*hh*33!!44";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumbers(userPrompt);
        result.Count.Should().Be(5);
        result[0].Should().Be(11);
        result[1].Should().Be(22);
        result[2].Should().Be(0);
        result[3].Should().Be(33);
        result[4].Should().Be(44);
    }
    
    [TestMethod]
    public void GetNumbers_CustomDelimiterIsEmpty_ShouldThrowInvalidDelimiterException()
    {
        var userInput = "//[]\\n102020";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = () => userInputService.GetNumbers(userPrompt);
        result.Should().Throw<InvalidDelimiterException>().WithMessage("No delimiters found.");
    }
    
    [TestMethod]
    public void GetNumbers_AlternativeDelimiterIsUsed_ShouldReturnValidNumbersInTheList()
    {
        var userInput = "1#2";
        var alternativeDelimiter = "#";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumbers(userPrompt, alternativeDelimiter);
        result.Count.Should().Be(2);
        result[0].Should().Be(1);
        result[1].Should().Be(2);
    }
    
    [TestMethod]
    public void GetBooleanInput_UppercaseYValueProvided_ShouldReturnTrue()
    {
        var userInput = "Y";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetBoolean(userPrompt);
        result.Should().BeTrue();
    }
    
    [TestMethod]
    public void GetBooleanInput_LowercaseYValueProvided_ShouldReturnTrue()
    {
        var userInput = "y";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetBoolean(userPrompt);
        result.Should().BeTrue();
    }
    
    [TestMethod]
    public void GetBooleanInput_UppercaseNValueProvided_ShouldReturnFalse()
    {
        var userInput = "N";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetBoolean(userPrompt);
        result.Should().BeFalse();
    }
    
    [TestMethod]
    public void GetBooleanInput_LowercaseNValueProvided_ShouldReturnFalse()
    {
        var userInput = "n";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetBoolean(userPrompt);
        result.Should().BeFalse();
    }
    
    [TestMethod]
    public void GetCharInput_OneCharProvided_ShouldReturnChar()
    {
        var userInput = "#";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetChar(userPrompt);
        result.ToString().Should().Be(userInput);
    }
    
    [TestMethod]
    public void GetNumber_ValidIntegerProvided_ShouldReturnInteger()
    {
        var userInput = "10";
        var userPrompt = "Enter Value";
        
        _mockConsoleWrapper.Setup(x => x.ReadLine()).Returns(userInput);
        var userInputService = CreateUserInputService();
        
        var result = userInputService.GetNumber(userPrompt);
        result.ToString().Should().Be(userInput);
    }
}