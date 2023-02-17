
//  Determina o namespace do código.
namespace NeuralLib;

/// <summary>
/// Função de ativação linear.
/// </summary>
public class Linear : IActivateFunction
{
    /// <summary>
    /// Aplica a função de ativação.
    /// </summary>
    /// <param name="input">Entrada da função.</param>
    /// <returns>Retorna a saída.</returns>
    public double Apply(double input)
    {
        return input;
    }
}