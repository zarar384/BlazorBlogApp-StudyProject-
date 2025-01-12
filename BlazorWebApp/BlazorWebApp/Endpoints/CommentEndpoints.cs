using Data.Models;
using Data.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebApp.Endpoints
{
    public static class CommentEndpoints
    {
        public static void MapCommentApi(this WebApplication app)
        {
            var commentsGroup = app.MapGroup("/api/Comments");
            commentsGroup.MapGet("/{*blogPostid}", async (IBlogApi api, string blogPostid) =>
            {
                return Results.Ok(await api.GetCommentsAsync(blogPostid));
            }).RequireAuthorization();

            commentsGroup.MapPut("", async (IBlogApi api, [FromBody] Comment item) =>
            {
                return Results.Ok(await api.SaveCommentAsync(item));
            }).RequireAuthorization();

            commentsGroup.MapDelete("/{*id}", async (IBlogApi api, string id) =>
            {
                await api.DeleteCommentAsync(id);
                return Results.Ok();
            });
        }
    }
}
