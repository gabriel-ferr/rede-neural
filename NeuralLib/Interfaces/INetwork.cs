namespace NeuralLib;

public interface INetwork<T>
{
    public List<T> Elements { get; protected set; }
    public List<Connection<T>> Connections { get; protected set; }
}