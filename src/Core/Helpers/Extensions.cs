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
}