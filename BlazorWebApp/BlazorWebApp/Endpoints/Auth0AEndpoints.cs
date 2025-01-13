using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace BlazorWebApp.Endpoints
{
    public static class Auth0AEndpoints
    {
        public static void MapAuth0Api(this WebApplication app)
        {
            app.MapGet("account/login", async (string redirectUri, HttpContext context) =>
            {
                var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(redirectUri)
                .Build();

                await context.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            });

            app.MapGet("authentication/logout", async (HttpContext context) =>
            {
                var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri("/")
                .Build();

                await context.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            });
        }
    }
}
