namespace NeuralLib;

public interface IActivateFunction
{
    public decimal GetResult(decimal input);
    public decimal GetGradient(decimal error, decimal output);
    public decimal GetOutputGradient(decimal output, decimal expected);
}