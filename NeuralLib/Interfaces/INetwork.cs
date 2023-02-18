
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
    public decimal[] Input { get; protected set; }

    /// <summary> Valor de saída da rede. </summary>
    public decimal[] Output { get; protected set; }

    /// <summary> Lista de elementos da rede. </summary>
    public List<T> Elements { get; protected set; }

    /// <summary> Lista de conexões da rede. (O set está desabilitado). </summary>
    public List<Connection<T>> Connections { get; protected set; }

    #endregion

    #region Methods

    /// <summary>
    /// Instância uma nova conexão manual.
    /// </summary>
    /// <param name="sender">ID do objeto que deve enviar a informação.</param>
    /// <param name="receiver">ID do objeto que deve receber a informação.</param>
    /// <param name="weigth">Peso da conexão (se já setado, se não, gera um peso)</param>
    public void CreateConnection(T sender, T receiver, decimal? weigth = null);

    #endregion
}

