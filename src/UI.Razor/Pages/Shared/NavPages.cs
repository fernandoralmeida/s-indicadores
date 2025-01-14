using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI.Razor.Pages.Shared;
public class NavPages
{
    public static string MapCompanies => "MapEmpresas";
    public static string MapSetores => "MapSetores";
    public static string Info => "Info";
    public static string Privacy => "Privacy";

    public static string MapCompaniesNavClass(ViewContext viewContext) => PageNavClass(viewContext, MapCompanies)!;
    public static string MapSetoresNavClass(ViewContext viewContext) => PageNavClass(viewContext, MapSetores)!;
    public static string PrivacyNavClass(ViewContext viewContext) => PageNavClass(viewContext, Privacy)!;
    public static string InfoNavClass(ViewContext viewContext) => PageNavClass(viewContext, Info)!;

    private static string? PageNavClass(ViewContext viewContext, string page)
    {
        var activePage = viewContext.ViewData["ActivePage"] as string
                         ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);

        return string.Equals(activePage, page!, StringComparison.OrdinalIgnoreCase) ? "active" : null;
    }

}