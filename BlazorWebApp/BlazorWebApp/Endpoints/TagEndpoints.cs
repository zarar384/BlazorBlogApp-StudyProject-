using Data.Models;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebApp.Endpoints
{
    public static class TagEndpoints
    {
        public static void MapTagApi(this WebApplication app)
        {
            var tagsGroup = app.MapGroup("/api/tags");
            tagsGroup.MapGet("", async (IBlogApi api) =>
            {
                return await api.GetTagsAsync();
            });

            tagsGroup.MapGet("/{*id}", async (IBlogApi api, string id) =>
            {
                return await api.GetTagAsync(id);
            });

            tagsGroup.MapPut("", async (IBlogApi api, [FromBody] Tag item) =>
            {
                return Results.Ok(await api.SaveTagAsync(item));
            }).RequireAuthorization();

            tagsGroup.MapDelete("/{*id}", async (IBlogApi api, string id) =>
            {
                await api.DeleteTagAsync(id);
                return Results.Ok();
            }).RequireAuthorization();
        }
    }
}
