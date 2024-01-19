using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI.Razor.Pages.Shared;
public class NavPages
{
    public static string MapCompanies => "MapEmpresas";
    public static string MapSetores => "MapSetores";

    public static string MapCompaniesNavClass(ViewContext viewContext) => PageNavClass(viewContext, MapCompanies)!;
    public static string MapSetoresNavClass(ViewContext viewContext) => PageNavClass(viewContext, MapSetores)!;

    private static string? PageNavClass(ViewContext viewContext, string page)
    {
        var activePage = viewContext.ViewData["ActivePage"] as string
                         ?? Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
        
        return string.Equals(activePage, page!, StringComparison.OrdinalIgnoreCase) ? "active" : null;
    }

}