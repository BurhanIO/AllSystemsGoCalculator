namespace Calculator.Exceptions;

[Serializable]
public class NegativeNumbersNotSupportedException : Exception
{
    public NegativeNumbersNotSupportedException() { }
    public NegativeNumbersNotSupportedException(string message) : base(message) { }
    public NegativeNumbersNotSupportedException(string message, Exception inner) : base(message, inner) { }
}