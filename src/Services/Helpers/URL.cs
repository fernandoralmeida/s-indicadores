using System.Web;
namespace IDN.Services.Helpers;

public static class URL
{
    public static string EncryptURL(this string value)
    {
        return HttpUtility.UrlEncode(Core.Helpers.URL.EncryptString(value));
    }

    public static string DecryptURL(this string value)
    {
        return Core.Helpers.URL.DecryptString(HttpUtility.UrlDecode(value));
    }
}