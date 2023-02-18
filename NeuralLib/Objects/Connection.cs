
//  Determina o namespace do código.
namespace NeuralLib;

/// <summary>
/// Estrutura de conexão entre dois elementos iguais de uma rede.
/// </summary>
public class Connection<T>
{
    #region Variables

#pragma warning disable CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.
    //  Elemento de saída de dados.
    private T sender;
    //  Elemento que receberá os dados.
    private T receiver;
    //  Peso da conexão.
    private decimal weight;
#pragma warning restore CS8618 // O campo não anulável precisa conter um valor não nulo ao sair do construtor. Considere declará-lo como anulável.

    #endregion

    #region Proprieties

    /// <summary> Elemento de onde a informação deverá sair. </summary>
    public T Sender { get => sender; set => sender = value; }

    /// <summary> Elemento que receberá a informação. </summary>
    public T Receiver { get => receiver; set => receiver = value; }

    /// <summary> Peso da conexão. </summary>
    public decimal Weight { get => weight; set => weight = value; }

    #endregion
}