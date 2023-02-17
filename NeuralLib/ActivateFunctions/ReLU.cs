
//  Determina o namespace do código.
namespace NeuralLib;

/// <summary>
/// Função de ativação ReLU.
/// </summary>
public class ReLU : IActivateFunction
{
    /// <summary>
    /// Aplica a função de ativação.
    /// </summary>
    /// <param name="input">Entrada da função.</param>
    /// <returns>Retorna a saída.</returns>
    public double Apply(double input)
    {
        return Math.Max(0, input);
    }
}