namespace NeuralLib;

public class NeuralNetwork : INetwork<Neuron>
{
    public NeuralNetwork ()
    {
        neurons = new ();
        synapses = new();
    }

    private readonly List<Neuron> neurons;
    private readonly List<Connection<Neuron>> synapses;

    public List<Neuron> Elements { get => neurons; set { throw new NotImplementedException("O método `set` não é válido para este elemento."); } }
    public List<Connection<Neuron>> Connections { get => synapses; set { throw new NotImplementedException("O método `set` não é válido para este elemento."); } }

    public Neuron this[int i]
    {
        get
        {
            Neuron? result = neurons.Find(x => x.Id == i);
            if (result == null) throw new IndexOutOfRangeException("O ID informado no index não existe na matriz de neurônios.");
            return result;
        }

        set
        {
            Neuron? result = neurons.Find(x => x.Id == i);
            if (result == null) neurons.Add(value);
            else result = value;
        }
    }

    public Task Pulse(Neuron[] senders)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            List<Connection<Neuron>> connections = Connections.FindAll(x => x.Sender == senders[i]);
            foreach (Connection<Neuron> connection in connections)
            {
                //Console.WriteLine("{0} -> {1}: {2}", senders[i].Id, connection.Receiver.Id, senders[i].Output * connection.Weight);
                connection.Receiver.Receive(senders[i].Output * connection.Weight);
            }
        }

        return Task.CompletedTask;
    }

    public void Refresh()
    {
        foreach (Neuron neuron in neurons) neuron.Refresh();
    }
}