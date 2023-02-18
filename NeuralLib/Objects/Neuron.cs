
//  Determina o namespace do código.
namespace NeuralLib;

/// <summary>
/// Estrutura de um neurônio.
/// </summary>
public class Neuron
{
    #region Construtores

    /// <summary>
    /// Instância um novo neurônio.
    /// </summary>
    public Neuron(int id, IActivateFunction activateFunction, decimal bias = 0)
    {
        this.bias = bias;
        this.id = id;
        this.activateFunction = activateFunction;
    }

    #endregion

    #region Variables

    //  Valor de identificação do neurônio.
    private int id;
    //  Valor de entrada armazenado no neurônio.
    private decimal? input = null;
    //  Valor de saída armazenado no neurônio.
    private decimal? output = null;
    //  Valor fixo de BIAS.
    private decimal bias;
    //  Função de ativação.
    private IActivateFunction activateFunction;

    #endregion

    #region Proprieties

    /// <summary> Valor de entrada do neurônio. </summary>
    public int Id { get => id; set => id = value; }

    /// <summary> Valor de entrada do neurônio. </summary>
    public decimal? Input { get => input; set => input = value; }

    /// <summary> Valor de saída do neurônio. </summary>
    public decimal? Output { get => output; set => output = value; }

    /// <summary> Valor de saída do neurônio. </summary>
    public decimal Bias { get => bias; set => bias = value; }

    /// <summary> Função de ativação do neurônio.. </summary>
    public IActivateFunction ActivateFunction { get => activateFunction; set => activateFunction = value; }

    #endregion

    #region Methods

    /// <summary>
    /// Recebe um pulso e registra o valor recebido na entrada.
    /// Esse valor já deve ser multiplicado pelo peso da sinapse!
    /// </summary>
    /// <param name="value">Valor de entrada relacionado ao pulso.</param>
    public void ReceivePulse (decimal? value)
    {
        if (value == null) return;
        if (input == null) input = 0;
        input += value;
    }

    /// <summary>
    /// Ativa o neurônio, gerando um valor de saída em output.
    /// </summary>
    /// <returns>Retorna quando a ativação tiver sido finalizada.</returns>
    public Task<Task> Active (NeuralNetwork network)
    {
        if (input != null)
            output = activateFunction.Apply((decimal) input + bias);
        //  Envia o pulso para os neurônios conectados
        return Task.FromResult(network.SendPulse(this));
    }

    #endregion
}