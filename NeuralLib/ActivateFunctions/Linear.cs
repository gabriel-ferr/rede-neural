namespace NeuralLib.Activate;

public class Linear : IActivateFunction
{
    public Linear(decimal a, decimal b)
    {
        A = a;
        B = b;
    }

    public decimal A;
    public decimal B;

    public decimal GetResult(decimal input)
    {
        return (A * input) + B;
    }

    public decimal GetGradient(decimal error, decimal output = decimal.Zero)
    {
        return A * error;
    }

    public decimal GetOutputGradient(decimal output, decimal expected)
    {
        return A * (expected - output);
    }

}