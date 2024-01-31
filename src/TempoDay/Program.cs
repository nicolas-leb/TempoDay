using DataLayer.Auth;
using DataLayer.DayColor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient(AuthService.ClientName, c =>
{
    c.BaseAddress = new Uri("https://digital.iservices.rte-france.com");
    c.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", builder.Configuration["Authentication:Key"]);
});
builder.Services.AddHttpClient(DayColorService.ClientName, c =>
{
    c.BaseAddress = new Uri("https://digital.iservices.rte-france.com");
});
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<DayColorService, DayColorService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
