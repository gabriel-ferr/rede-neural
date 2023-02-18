
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
    }

    #endregion

    //  Variáveis internas da rede.
    #region Variables

#pragma warning disable CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
    //  Valor de entrada da rede.
    private double?[] input = null;
        //  Valor de saída da rede.
    private double?[] output = null;
#pragma warning restore CS8625 // Não é possível converter um literal nulo em um tipo de referência não anulável.
    //  Lista de elementos da rede.
    private readonly List<Neuron> neurons = new ();
    //  Lista de sinapses.
    private readonly List<Connection<Neuron>> synapses = new ();

    //  Pega um neurônio a partir da index.
    public Neuron this[int i] { get => neurons[i]; protected set => neurons[i] = value; }

    #endregion

    #region Proprieties

    /// <summary> Valor de entrada da rede. </summary>
    public double?[] Input { get => input; set => input = value; }

    /// <summary> Valor de saída da rede. </summary>
    public double?[] Output { get => output; set => output = value; }

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
    public void CreateConnection (Neuron sender, Neuron receiver, double? weigth = null)
    {
        Random rnd = new(Convert.ToInt32(DateTime.Now.Ticks));

        //  Gera um novo peso se ele não tiver sido setado.
        if (weigth == null) weigth = rnd.NextDouble();

        //  Verifica se a conexão já existe.
        List<Connection<Neuron>> _connections = Connections.FindAll(x => x.Sender.Id == sender.Id);
        _connections = _connections.FindAll(x => x.Receiver.Id == receiver.Id);

        //  Retorna pois a conexão já existe.
        if (_connections.Count() > 0) return;

        //  Cria a conexão.
        Connection<Neuron> connection = new() {
            Sender = sender,
            Receiver = receiver,
            Weight = (double) weigth
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
        {
            if (sender.Output != null) connection.Receiver.ReceivePulse(sender.Output * connection.Weight);
            else connection.Receiver.ReceivePulse(sender.Output);
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Treina a rede neural (é uma função pronta, mas é recomendável fazer a sua própria função de treinamento).
    /// </summary>
    /// <returns>Retorna o sucesso quando o treinamento tiver sido concluido.</returns>
    public Task Train (List<Neuron> output, double[] expected, double learningRate)
    {
        //  Se o index das matrizes for diferente, deu problema.
        if (output.Count() != expected.Length) throw new ArgumentException($"O tamanho da matriz de neurônios de saída não coincide com o da matriz de valores para treino.");

        //  Cria um dicionário associando um neurônio de saída ao seu erro.
        Dictionary<Neuron, double> errors = new ();

        //  Calcula os erros de saída.
        int e_index = 0;
        foreach(Neuron neuron in output)
        {
            if (neuron.Output == null) { e_index++; continue; }
            double error = (expected[e_index] - ((double) neuron.Output)) * ((double)neuron.Output) * (1.0F - ((double)neuron.Output));
            errors.Add(neuron, error);
        }

        //  Lista as conexões e vai corrigindo.
        foreach(Connection<Neuron> connection in Connections)
        {
            //  Se não retornou valor, ignora.
            if (connection.Receiver.Output == null) continue;

            //  Valor do erro calculado.
            double errorValue = 0;

            //  Verifica se o neurônio que recebeu os dados está na saída.
            if (output.Contains(connection.Receiver))
            {
                connection.Weight += learningRate * errors[connection.Receiver] * (double) (connection.Receiver.Output);
                connection.Receiver.Bias = learningRate * errors[connection.Receiver];
                continue;
            }

            //  Caso não seja camada de saída, calcula o erro.
            foreach (double error in errors.Values)
                errorValue += error * connection.Weight * (double) connection.Receiver.Output * (1.0F - (double) connection.Receiver.Output);

            //  Calcula o novo peso do neurônio.
            connection.Weight += learningRate * errorValue * (double) connection.Receiver.Output;
            connection.Receiver.Bias += learningRate * errorValue;
        }

        //  Finaliza.
        return Task.CompletedTask;
    }

    #endregion
}