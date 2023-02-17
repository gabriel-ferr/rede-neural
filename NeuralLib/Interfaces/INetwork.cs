
//  Determina o namespace do código.
namespace NeuralLib;

/// <summary>
/// Interface que representa uma rede no contexto da biblioteca.
/// Em resumo, uma rede de tipo <T> relaciona um valor de entrada, saída e um conjunto de elementos internos.
/// </summary>
public interface INetwork<T>
{
    //  Propriedades da interface.
    #region Proprieties

    /// <summary> Valor de entrada da rede. </summary>
    public double[]? Input { get; protected set; }

    /// <summary> Valor de saída da rede. </summary>
    public double[]? Output { get; protected set; }

    /// <summary> Lista de elementos da rede. </summary>
    public List<T> Elements { get; protected set; }

    /// <summary> Lista de conexões da rede. (O set está desabilitado). </summary>
    public List<Connection<T>> Connections { get; protected set; }

    #endregion

    #region Methods

    /// <summary>
    /// Instância uma nova conexão manual.
    /// </summary>
    /// <param name="sender">ID do neurônio que deve enviar a informação.</param>
    /// <param name="receiver">ID do neurônio que deve receber a informação.</param>
    /// <param name="weigth">Peso da conexão (se já setado, se não, gera um peso)</param>
    public void CreateConnection(int sender, int receiver, double? weigth = null);

    #endregion
}

