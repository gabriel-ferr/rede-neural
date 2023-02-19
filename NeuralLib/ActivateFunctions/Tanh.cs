namespace NeuralLib.Activate;

public class Tanh : IActivateFunction
{
    public Tanh(decimal a, decimal b)
    {
        A = a;
        B = b;
    }

    public decimal A;
    public decimal B;

    public decimal GetResult(decimal input)
    {
        return A * (decimal) Math.Tanh((double) (B * input));
    }

    public decimal GetGradient(decimal error, decimal output)
    {
        return (B / A) * (A - output) * (A + output) * error;
    }

    public decimal GetOutputGradient(decimal output, decimal expected)
    {
        return (B / A) * (expected - output) * (A - output) * (A + output);
    }

}