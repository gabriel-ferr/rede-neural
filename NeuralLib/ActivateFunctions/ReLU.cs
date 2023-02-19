using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NeuralLib.Activate;

public class ReLU : IActivateFunction
{

    public decimal GetResult(decimal input)
    {
        return Math.Max(0, input);
    }

    public decimal GetGradient(decimal error, decimal output)
    {
        int d = output > 0 ? 1 : 0;
        return error * d;
    }

    public decimal GetOutputGradient(decimal output, decimal expected)
    {
        int d = output > 0 ? 1 : 0;
        return (expected - output) * d;
    }

}