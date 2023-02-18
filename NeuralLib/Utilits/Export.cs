//  Determina o namespace do código.
namespace NeuralLib.Utilits;

/// <summary>
/// Exporta um conjunto de dados.
/// </summary>
public class Export
{
    /// <summary>
    /// Exporta o conjunto de dados como arquivo csv.
    /// </summary>
    /// <param name="x_axis">Valores para o eixo x</param>
    /// <param name="y_axis">Valores para o eixo y</param>
    public static void AsCsv (List<double> x_axis, List<double> y_axis, string? path = null, string? filename = null)
    {
        if (x_axis.Count != y_axis.Count) throw new ArgumentException("O número de elementos em ambos os eixos deve ser igual.");

        //  Gera o conteúdo do arquivo.
        System.Text.StringBuilder builder = new ();
        for(int i = 0; i < x_axis.Count; i++)
            builder.Append($"{x_axis[i]};{y_axis[i]}\n");

        //  Diretório onde o arquivo será salvo.
        path ??= Path.Combine(Directory.GetCurrentDirectory(), "exports");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);

        //  Cria o arquivo onde será salvo.
        filename ??= $"{DateTime.Now.ToLocalTime():yyyy-MM-dd-hh-mm-ss}.csv";
        if (!File.Exists(Path.Combine(path, filename))) File.Create(Path.Combine(path, filename)).Close();

        //  Escreve o conteúdo do arquivo.
        StreamWriter writer = new(Path.Combine(path, filename));
        writer.Write(builder.ToString());
        writer.Flush();
        writer.Close();
    }
}