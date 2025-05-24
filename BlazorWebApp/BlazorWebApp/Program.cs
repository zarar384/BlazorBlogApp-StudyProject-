using Data;
using BlazorWebApp;
using BlazorWebApp.Services;
using Data.Models.Interfaces;
using BlazorWebApp.Endpoints;
using BlazorWebApp.Components;
using SharedComponents.Interfaces;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorWebApp.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options =>
    {
        if (builder.Environment.IsDevelopment() || builder.Environment.EnvironmentName == "Internal")
        {
            options.DetailedErrors = true;
        }
    })
    .AddInteractiveWebAssemblyComponents();


builder.Services.AddScoped<IBrowserStorage, BlogProtectedBrowserStorage>();
builder.Services.AddSignalR();

builder.Services.AddOptions<BlogApiJsonDirectAccessSetting>()
    .Configure(options =>
    {
        options.DataPath = @"..\..\..\Data\";
        options.BlogPostsFolder = "Blogposts";
        options.TagsFolder = "Tags";
        options.CategoriesFolder = "Categories";
        options.CommentsFolder = "Comments";
    });
builder.Services.AddScoped<IBlogApi, BlogApiJsonDirectAccess>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<IBlogNotificationService, BlazorServerBlogNotificationService>();
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Authority"] ?? ""; ;
    options.ClientId = builder.Configuration["Auth0:ClientId"] ?? ""; ;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

//SignalR
app.MapHub<BlogNotificationHub>("/BlogNotificationHub");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorWebApp.Client._Imports).Assembly)
    .AddAdditionalAssemblies(typeof(SharedComponents.Pages.Home).Assembly);

app.MapBlogPostApi();
app.MapCategoryApi();
app.MapCommentApi();
app.MapTagApi();
app.MapAuth0Api();

app.Run();
