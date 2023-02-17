
//  Determina o namespace do código.
namespace NeuralLib;

/// <summary>
/// Interface para uma função de ativação.
/// </summary>
public interface IActivateFunction
{
    /// <summary>
    /// Aplica a função de ativação.
    /// </summary>
    /// <param name="input">Entrada da função.</param>
    /// <returns>Resultado do cálculo.</returns>
    public double Apply(double input);
}