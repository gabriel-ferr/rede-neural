
//  Determina o namespace do código.
namespace NeuralLib;

/// <summary>
/// Objeto que estrutura uma camada dentro de uma rede.
/// </summary>

public class Layer<G, T>
{
    #region Constructors

    /// <summary>
    /// Constroí uma nova camada.
    /// </summary>
    /// <param name="reference">Referência ao qual a camada deverá se referir.</param>
    public Layer(G reference) 
    {
        this.reference = reference;
    }

    #endregion

    #region Variables

    //  Elementos da camada.
    private readonly List<T> elements = new ();

    //  Referência de rede.
    private readonly G reference;


    #endregion

    #region Proprieties

    /// <summary> Elementos da camada. </summary>
    public List<T> Elements { get => elements; private set { } }

    /// <summary> Referência da camada. </summary>
    public G Reference { get => reference; private set { } }

    #endregion
}
