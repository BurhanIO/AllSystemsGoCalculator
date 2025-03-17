namespace Calculator.Services.BasicMath;

public class BasicMathOptions
{
    public MathOperationType Operation { get; set; }
    public bool AllowNegativeNumbers { get; set; }
    public double MaxNumber { get; set; }
}