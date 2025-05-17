using Blazored.SessionStorage;
using BlazorWebApp.Client;
using BlazorWebApp.Client.Services;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SharedComponents.Interfaces;
using WebCustomElements.Components;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.RegisterCustomElement<Counter>("my-blazor-counter");

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddHttpClient("Api", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddTransient<IBlogApi, BlogApiWebClient>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddScoped<IBrowserStorage, BlogBrowserStorage>();

builder.Services.AddSingleton<IBlogNotificationService, BlazorWebAssemblyBlogNotificationService>();

await builder.Build().RunAsync();
