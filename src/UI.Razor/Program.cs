using System.Globalization;
using IDN.Coupler;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddLocalization();
//var cultureInfoBR = new[] { new CultureInfo("pt-BR") };

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.Register();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
//CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture("pt-BR");

app.UseStaticFiles();
app.UseForwardedHeaders();
app.UseRouting();
app.MapRazorPages();
app.MapControllers();
app.UseAuthorization();

await app.RunAsync();
