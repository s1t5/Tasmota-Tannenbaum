// Program.cs
using Microsoft.Extensions.Configuration;
using WeihnachtsTannenbaum.Models;
using WeihnachtsTannenbaum.Services;

var builder = WebApplication.CreateBuilder(args);

// Konfiguriere Kestrel, um auf allen Netzwerkinterfaces auf Port 5000 (HTTP) zu lauschen
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // HTTP
});

// Füge Dienste zum Container hinzu.
builder.Services.AddControllersWithViews();

// Binde die Tasmota-Einstellungen aus der Konfiguration
builder.Services.Configure<TasmotaSettings>(builder.Configuration.GetSection("Tasmota"));

// Registriere den TasmotaService
builder.Services.AddTransient<TasmotaService>();

var app = builder.Build();

// Konfiguriere die HTTP-Anforderungs-Pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // HSTS wird hier nicht verwendet, da keine HTTPS-Verwendung
}

// Verwende statische Dateien
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Definiere die Standardroute
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();