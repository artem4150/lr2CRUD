using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// ������������ �������
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// ������������ ����������� HttpClient ��� ������� API
builder.Services.AddHttpClient("Api", client =>
{
    // ������ ������� URL �� appsettings.json (��� ���������� �������� �� ���������)
    var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5057/";
    client.BaseAddress = new Uri(apiBaseUrl);
});

// ����������� HttpClient �� ���������, ��������� ����������� ������ "Api"
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
