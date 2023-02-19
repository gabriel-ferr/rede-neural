namespace NeuralLib;

public class Neuron
{
    public Neuron(int id, IActivateFunction activateFunction, decimal bias = decimal.Zero)
    {
        if (bias == decimal.Zero)
        {
            Random rnd = new Random();
            bias = (decimal)rnd.NextDouble();
        }

        this.id = id;
        Output = decimal.Zero;
        receiveBuffer = decimal.Zero;

        Bias = bias;
        ActivateFunction = activateFunction;
    }

    private int id;
    private decimal output;
    private decimal receiveBuffer;

    public decimal Bias;
    public IActivateFunction ActivateFunction;

    public int Id { get => id; private set => id = value; }
    public decimal Output { get => output; private set => output = value; }

    public void Activate ()
    {
        output = ActivateFunction.GetResult(receiveBuffer + Bias);
        //Console.WriteLine("ID {3}; buffer: {0} ; bias: {1}; output: {2}", receiveBuffer, Bias, output, id);
    }

    public void Receive(decimal value)
    {
        receiveBuffer += value;
    }

    public void Refresh ()
    {
        this.output = decimal.Zero;
        this.receiveBuffer = decimal.Zero;
    }

}