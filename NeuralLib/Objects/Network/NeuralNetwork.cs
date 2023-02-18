
//  Determina o namespace do código.
namespace NeuralLib;

/// <summary>
/// Objeto que representa uma rede de neurônios.
/// </summary>
public class NeuralNetwork : INetwork<Neuron>
{
    //  Construtores do objeto.
    #region Constructors

    /// <summary>
    /// Instância uma nova rede de neurônios.
    /// </summary>
    /// <param name="amount">Quantidade de neurônios pertencentes a rede.</param>
    public NeuralNetwork (int amount, IActivateFunction standerd)
    {
        //  Instância os neurônios da rede.
        for (int i = 0; i < amount; i++)
            neurons.Add(new Neuron(i, standerd));

        input = new decimal [1];
        output = new decimal[1];
    }

    #endregion

    //  Variáveis internas da rede.
    #region Variables

    //  Valor de entrada da rede.
    private decimal[] input;
        //  Valor de saída da rede.
    private decimal[] output;
    //  Lista de elementos da rede.
    private readonly List<Neuron> neurons = new ();
    //  Lista de sinapses.
    private readonly List<Connection<Neuron>> synapses = new ();

    //  Pega um neurônio a partir da index.
    public Neuron this[int i] { get => neurons[i]; protected set => neurons[i] = value; }

    #endregion

    #region Proprieties

    /// <summary> Valor de entrada da rede. </summary>
    public decimal[] Input { get => input; set => input = value; }

    /// <summary> Valor de saída da rede. </summary>
    public decimal[] Output { get => output; set => output = value; }

    /// <summary> Lista de elementos da rede. (O set está desabilitado). </summary>
    public List<Neuron> Elements { get => neurons; set { } }

    /// <summary> Lista de conexões da rede. (O set está desabilitado). </summary>
    public List<Connection<Neuron>> Connections { get => synapses; set { } }

    #endregion

    #region Methods

    /// <summary>
    /// Instância uma nova conexão manual.
    /// </summary>
    /// <param name="sender">ID do neurônio que deve enviar a informação.</param>
    /// <param name="receiver">ID do neurônio que deve receber a informação.</param>
    /// <param name="weigth">Peso da conexão (se já setado, se não, gera um peso)</param>
    public void CreateConnection (Neuron sender, Neuron receiver, decimal weigth = 0)
    {
        Random rnd = new();

        //  Gera um novo peso se ele não tiver sido setado.
        if (weigth == 0) weigth = (decimal) rnd.NextDouble();

        //  Verifica se a conexão já existe.
        List<Connection<Neuron>> _connections = Connections.FindAll(x => x.Sender.Id == sender.Id);
        _connections = _connections.FindAll(x => x.Receiver.Id == receiver.Id);

        //  Retorna pois a conexão já existe.
        if (_connections.Count > 0) return;

        //  Cria a conexão.
        Connection<Neuron> connection = new() {
            Sender = sender,
            Receiver = receiver,
            Weight = weigth
        };

        //  Adiciona a conexão.
        Connections.Add(connection);
    }

    /// <summary>
    /// Envia o pulso pela rede a partir de um neurônio específico.
    /// </summary>
    /// <param name="sender">Neurônio que deverá enviar o pulso.</param>
    /// <returns></returns>
    public Task SendPulse (Neuron sender)
    {
        //  Pega todas as conexões que o neurônio que vai enviar a informação possuí.
        List<Connection<Neuron>> _connections = synapses.FindAll(x => x.Sender.Id == sender.Id);
        //  Propaga o pulso para os neurônios conectados.
        foreach (Connection<Neuron> connection in _connections)
            connection.Receiver.ReceivePulse(sender.Output * connection.Weight);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Treina a rede neural (é uma função pronta, mas é recomendável fazer a sua própria função de treinamento).
    /// </summary>
    /// <returns>Retorna o sucesso quando o treinamento tiver sido concluido.</returns>
    public Task Train (List<Neuron> output, decimal[] expected, decimal learningRate)
    {
        

        //  Finaliza.
        return Task.CompletedTask;
    }

    #endregion
}