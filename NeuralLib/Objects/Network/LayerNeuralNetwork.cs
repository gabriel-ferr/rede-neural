//  Determina o namespace do código.
namespace NeuralLib;

/// <summary>
/// Interface para uma função de ativação.
/// </summary>
public class LayerNeuralNetwork : INetwork<Layer<NeuralNetwork, Neuron>>
{

    #region Variables
#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    //  Valor de entrada da rede.
    private decimal?[] input;
    //  Valor de saída da rede.
    private decimal?[] output;
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
                              //  Lista de elementos da rede.
    private readonly List<Layer<NeuralNetwork, Neuron>> layers = new();
    //  Lista de conexões.
    private readonly List<Connection<Layer<NeuralNetwork, Neuron>>> connections = new();

    #endregion

    #region Proprieties

    /// <summary> Valor de entrada da rede. </summary>
    public decimal?[] Input { get => input; set => input = value; }

    /// <summary> Valor de saída da rede. </summary>
    public decimal?[] Output { get => output; set => output = value; }

    /// <summary> Lista de elementos da rede. (O set está desabilitado). </summary>
    public List<Layer<NeuralNetwork, Neuron>> Elements { get => layers; set { } }

    /// <summary> Lista de conexões da rede. (O set está desabilitado). </summary>
    public List<Connection<Layer<NeuralNetwork, Neuron>>> Connections { get => connections; set { } }

    #endregion

    #region Methods

    /// <summary>
    /// Reseta a entrada e saída da rede.
    /// </summary>
    public void Refresh()
    {
        input = Array.Empty<decimal?>();
        output = Array.Empty<decimal?>();
    }

    /// <summary>
    /// Propaga um pulso na rede de camadas.
    /// </summary>
    /// <param name="inputLayer">Camada de entrada.</param>
    /// <param name="outputLayer">Camada de saída.</param>
    /// <returns>Retorna ao terminar a propagação.</returns>
    /// <exception cref="Exception">Pode retornar uma exceção quando há problemas nos dados de entrada.</exception>
    public async Task Pulse(Layer<NeuralNetwork, Neuron> inputLayer, Layer<NeuralNetwork, Neuron> outputLayer)
    {
        //  Verifica se a quantidade de dados de entrada é igual ao de neurônios na camada.
        if (input == null) throw new Exception("A matriz de entrada é nula.");
        if (inputLayer.Elements.Count != input.Length) throw new Exception("O número de entradas deve ser equivalente ao de neurônios na camada de entrada.");

        //  Envia um pulso pelos neurônios da camada de entrada.
        int i = 0;
        foreach(Neuron neuron in inputLayer.Elements)
        {
            neuron.Input = input[i];
            neuron.Output = input[i];
            await inputLayer.Reference.SendPulse(neuron);
            i++;
        }

        //  Pega as camadas conectadas e vai gerando um efeito cascata.
        await PulseNextLayer(inputLayer, outputLayer);

        // Pega os valores de saída.
        List<decimal?> outputs = new();
        foreach (Neuron neuron in outputLayer.Elements)
            outputs.Add(neuron.Output);

        //  Por fim, salva as saídas.
        output = outputs.ToArray();

    }

    /// <summary>
    /// Propaga o evento em camadas seguindo um callback.
    /// </summary>
    /// <param name="reference">Referência para a propagação.</param>
    /// <param name="output">Camada de saída que encerrará a propagação.</param>
    /// <returns>Retorna ao completar a propagação.</returns>
    public async Task PulseNextLayer (Layer<NeuralNetwork, Neuron> reference, Layer<NeuralNetwork, Neuron> output)
    {
        //  Pega as conexões da camada de referência.
        List<Connection<Layer<NeuralNetwork, Neuron>>> connections = Connections.FindAll(x => x.Sender == reference);
        List<Task> pulses = new();

        //  Lista as conexões e vai propagando.
        foreach (Connection<Layer<NeuralNetwork, Neuron>> connection in connections)
        {
            //  Gera o pulso na camada que recebe a conexão.
            foreach (Neuron neuron in connection.Receiver.Elements)
            {
                await neuron.Active(connection.Receiver.Reference);
            }
        }

        //  Se a referência for a camada de saída, encerra a propagação.
        if (reference == output) return;

        //  Ativado os neurônios das conexões, propaga o pulso para as próximas camadas
        foreach (Connection<Layer<NeuralNetwork, Neuron>> connection in connections)
        {
            pulses.Add(PulseNextLayer(connection.Receiver, output));
        }

        //  Aguarda a próxima camada ter encerrado a propagação.
        foreach (Task pulse in pulses) await pulse;
    }

    /// <summary>
    /// Instância uma nova conexão manual.
    /// </summary>
    /// <param name="sender">ID da camada que deve enviar a informação.</param>
    /// <param name="receiver">ID da camada que deve receber a informação.</param>
    /// <param name="weight">Peso da conexão (se já setado, se não, gera um peso)</param>
    public void CreateConnection(Layer<NeuralNetwork, Neuron> sender, Layer<NeuralNetwork, Neuron> receiver, decimal? weight = null)
    {
        if (sender.Reference != receiver.Reference) throw new Exception("A rede de referência para as camadas deve ser a mesma.");

        //  Verifica se a conexão já existe.
        List<Connection<Layer<NeuralNetwork, Neuron>>> _connections = Connections.FindAll(x => x.Sender == sender);
        _connections = _connections.FindAll(x => x.Receiver == receiver);

        //  Retorna pois a conexão já existe.
        if (_connections.Count() > 0) return;

        //  Cria a conexão.
        Connection<Layer<NeuralNetwork, Neuron>> connection = new()
        {
            Sender = sender,
            Receiver = receiver,
            Weight = 0
        };

        //  Adiciona a conexão.
        Connections.Add(connection);

        //  Cria as conexões entre os neurônios.
        foreach(Neuron neuronSender in sender.Elements)
        {
            foreach (Neuron neuronReceiver in receiver.Elements)
            {
                sender.Reference.CreateConnection(neuronSender, neuronReceiver);
            }
        }
    }

    #endregion

}