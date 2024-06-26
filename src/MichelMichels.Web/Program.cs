using MichelMichels.BpostSharp.Excel;
using MichelMichels.BpostSharp.Services;
using MichelMichels.ViesSharp;
using MichelMichels.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<IViesSharpClient, ViesSharpClient>()
    .AddSingleton(services =>
    {
        return new BelgianCityDataService(new ExcelCacheBuilder("wwwroot/data/bpost.xls"));
    });

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
