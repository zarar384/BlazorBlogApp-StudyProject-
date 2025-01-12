using Data.Models;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebApp.Endpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryApi(this WebApplication app)
        {
            var categoriesGroup = app.MapGroup("/api/Categories");
            categoriesGroup.MapGet("", async(IBlogApi api) =>
            {
                return Results.Ok(await api.GetCategoriesAsync());
            });

            categoriesGroup.MapGet("/{*id}", async (IBlogApi api, string id) =>
            {
                return Results.Ok(await api.GetCategoryAsync(id));
            });

            categoriesGroup.MapPost("", async (IBlogApi api, [FromBody] Category item) =>
            {
                return Results.Ok(await api.SaveCategoryAsync(item));
            }).RequireAuthorization();

            categoriesGroup.MapDelete("/{*id}", async (IBlogApi api, string id) =>
            {
                await api.DeleteCategoryAsync(id);
                return Results.Ok();
            }).RequireAuthorization();
        }
    }
}
