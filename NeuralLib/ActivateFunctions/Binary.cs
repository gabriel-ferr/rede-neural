
//  Determina o namespace do código.
namespace NeuralLib;

/// <summary>
/// Função de ativação binária.
/// </summary>
public class Binary : IActivateFunction
{
    /// <summary>
    /// Aplica a função de ativação.
    /// </summary>
    /// <param name="input">Entrada da função.</param>
    /// <returns>Retorna a saída.</returns>
    public double Apply(double input)
    {
        if (input > 0) return 1;
        else return 0;
    }
}