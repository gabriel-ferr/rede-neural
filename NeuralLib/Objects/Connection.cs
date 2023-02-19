namespace NeuralLib;

public sealed class Connection<T>
{
    public Connection (T sender, T receiver, decimal weight = decimal.Zero)
    {
        if (weight == decimal.Zero)
        {
            Random rnd = new Random();
            weight = (decimal) rnd.NextDouble();
        }

        this.sender = sender;
        this.receiver = receiver;

        this.Weight = weight;
    }

    private T sender;
    private T receiver;

    public decimal Weight;

    public T Sender { get => sender; private set => sender = value; }
    public T Receiver { get => receiver; private set => receiver = value; }
}