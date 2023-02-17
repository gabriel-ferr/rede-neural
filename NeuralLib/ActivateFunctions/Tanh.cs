
//  Determina o namespace do código.
namespace NeuralLib;

/// <summary>
/// Função de ativação tanh.
/// </summary>
public class Tanh : IActivateFunction
{
    /// <summary>
    /// Aplica a função de ativação.
    /// </summary>
    /// <param name="input">Entrada da função.</param>
    /// <returns>Retorna a saída.</returns>
    public double Apply(double input)
    {
        return (2 / (1 + Math.Pow(Math.E, -2 * input))) - 1;
    }
}