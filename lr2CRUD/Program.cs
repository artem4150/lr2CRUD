using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Регистрируем сервисы
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Регистрируем именованный HttpClient для вызовов API
builder.Services.AddHttpClient("Api", client =>
{
    // Читаем базовый URL из appsettings.json (или используем значение по умолчанию)
    var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5057/";
    client.BaseAddress = new Uri(apiBaseUrl);
});

// Настраиваем HttpClient по умолчанию, используя именованный клиент "Api"
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
