using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace IDN.Core.Helpers;

public static class Extensions
{

    /// <summary>
    /// 20230101 -> 2023-01-01
    /// </summary>
    /// <param name="valor"></param>
    /// <returns>000-00-00</returns>
    public static string StringDateTime(this string valor)
    {
        if (string.IsNullOrEmpty(valor) || valor.Length < 8)
            return "";
        else
            return valor
                .Insert(4, "-")
                .Insert(7, "-");
    }

    public static string NormalizeText(this string text)
    {
        return new string(text
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
    }

    public static string DateDiference(this DateTime date)
    {
        var yearsSinceDate = (DateTime.Now - date).TotalDays / 365;

        foreach (var interval in Dictionaries.AgeGroupCorp)
            if (yearsSinceDate < interval.Key)
                return interval.Value;

        return string.Empty;
    }

    public static string RemoveNumbers(this string str)
    {
        // Cria um novo StringBuilder para armazenar a string resultante
        var sb = new StringBuilder();

        // Itera sobre cada caractere da string original
        for (int i = 0; i < str.Length; i++)
        {
            // Verifica se o caractere é um número
            if (!char.IsDigit(str[i]))
            {
                // Adiciona o caractere ao StringBuilder
                sb.Append(str[i]);
            }
        }

        // Retorna a string resultante
        return sb.ToString();
    }
}