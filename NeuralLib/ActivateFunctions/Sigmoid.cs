
//  Determina o namespace do código.
namespace NeuralLib;

/// <summary>
/// Função de ativação sigmoide.
/// </summary>
public class Sigmoid : IActivateFunction
{
    /// <summary>
    /// Aplica a função de ativação.
    /// </summary>
    /// <param name="input">Entrada da função.</param>
    /// <returns>Retorna a saída.</returns>
    public double Apply (double input)
    {
        return 1 / (1 + Math.Pow(Math.E, -input));
    }
}