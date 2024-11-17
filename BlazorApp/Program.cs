using BlazorApp;
using Blazored.LocalStorage;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Primitives;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredModal();
builder.Services.AddLocalization();
builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddOidcAuthentication(c =>
{
    builder.Configuration.Bind("oidc", c.ProviderOptions);
    c.UserOptions.RoleClaim = "role";
});
var app= builder.Build();

var localStorageService = app.Services.GetRequiredService<ILocalStorageService>();

var culture = await localStorageService.GetItemAsStringAsync("BlazorCulture");
if(culture==null)
{
    culture = "en-US";
    await localStorageService.SetItemAsStringAsync("BlazorCulture", culture);
}
var cultureInfo = new CultureInfo(culture);
CultureInfo.DefaultThreadCurrentCulture= cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture= cultureInfo;



await app.RunAsync();
