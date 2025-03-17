using Calculator.Services;
using Calculator.Utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConsoleWrapper, ConsoleWrapper>()
            .AddSingleton<IUserInputService, UserInputService>()
            .AddSingleton<IAdditionService, AdditionService>()
            .AddSingleton<ICalculator, Calculator>()
            .BuildServiceProvider();
        
        var calculator = serviceProvider.GetService<ICalculator>();
        calculator?.Run();
    }
}