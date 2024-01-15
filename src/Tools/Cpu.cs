namespace IDN.Tools;

public static class Cpu
{
    /// <summary>
    /// Variavel estática tipo inteira somente leitura.
    /// </summary>
    /// <returns>Número de processadores do Sistema Operacional.</returns>
    public static readonly int Count = System.Environment.ProcessorCount;
}