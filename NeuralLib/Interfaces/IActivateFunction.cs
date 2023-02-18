
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
    public decimal Apply(decimal input);

    /// <summary>
    /// Retorna o erro da camada de saída.
    /// </summary>
    /// <param name="input">Valor de entrada.</param>
    /// <param name="e">Erro relativo.</param>
    public decimal OutputError(decimal input, decimal e);
}