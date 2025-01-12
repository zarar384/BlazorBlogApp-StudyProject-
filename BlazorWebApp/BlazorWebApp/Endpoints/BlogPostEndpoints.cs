using Data.Models;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebApp.Endpoints
{
    public static class BlogPostEndpoints
    {
        public static void MapBlogPostApi(this WebApplication app)
        {
            var blogPostsGroup = app.MapGroup("/api/BlogPosts");
            blogPostsGroup.MapGet("", async (IBlogApi api, [FromQuery] int numberofposts, [FromQuery] int startindex) =>
            {
                if (numberofposts <= 0 || startindex < 0)
                {
                    return Results.BadRequest("Invalid parameters. 'numberofposts' must be greater than 0 and 'startindex' must be non-negative.");
                }

                var blogPosts = await api.GetBlogPostsAsync(numberofposts, startindex);

                return Results.Ok(blogPosts);
            });

            blogPostsGroup.MapGet("/{*id}",
                async (IBlogApi api, string id) =>
                {
                    return Results.Ok(await api.GetBlogPostAsync(id));
                });

            blogPostsGroup.MapPut("", async (IBlogApi api, [FromBody] BlogPost item) =>
            {
                return Results.Ok(await api.SaveBlogPostAsync(item));
            }).RequireAuthorization();

            blogPostsGroup.MapDelete("/{*id}",
                async (IBlogApi api, string id) =>
                {
                    await api.DeleteBlogPostAsync(id);
                    return Results.Ok();
                }).RequireAuthorization();

            app.MapGet("/api/BlogPostCount",
                async (IBlogApi api) =>
                {
                    return Results.Ok(await api.GetBlogPostCountAsync());
                });
        }
    }
}
