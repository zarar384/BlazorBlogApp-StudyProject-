using Data.Models;
using Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorWebApp.Test
{
    internal class BlogApiMock : IBlogApi
    {
        public Task DeleteBlogPostAsync(string id)
        {
            return Task.CompletedTask;
        }

        public Task DeleteCategoryAsync(string id)
        {
            return Task.CompletedTask;
        }

        public Task DeleteCommentAsync(string id)
        {
            return Task.CompletedTask;
        }

        public Task DeleteTagAsync(string id)
        {
            return Task.CompletedTask;
        }

        public async Task<BlogPost?> GetBlogPostAsync(string id)
        {
            BlogPost post = new()
            {
                Id = id,
                Text = $"One Piece is real! Id: {id}",
                Title = $"Can someone send me a dollar?",
                PublishDate = DateTime.Now,
                Category = await GetCategoryAsync("Movie"),
            };

            post.Tags.Add(await GetTagAsync("Movie"));
            post.Tags.Add(await GetTagAsync("I hate my life, please help me"));
            return post;
        }

        public Task<int> GetBlogPostCountAsync()
        {
            return Task.FromResult(10);
        }

        public async Task<List<BlogPost>> GetBlogPostsAsync(int numberOfPosts, int startIndex)
        {
            List<BlogPost> list = new();
            for (int a = 0; a < numberOfPosts; a++)
            {
                list.Add(await GetBlogPostAsync($"{startIndex + a}"));
            }
            return list;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            List<Category> list = new();
            for (int a = 0; a < 10; a++)
            {
                list.Add(await GetCategoryAsync($"{a}"));
            }
            return list;
        }

        public Task<Category?> GetCategoryAsync(string id)
        {
            return Task.FromResult(new Category() { Id = id, Name = $"Category {id}" });
        }

        public Task<List<Comment>> GetCommentsAsync(string blogPostId)
        {
            var comments = new List<Comment>
            {
                 new Comment
                 {
                    BlogPostId = blogPostId,
                    Date = DateTime.Now,
                    Id = $"Comment1",
                    Name = "Monkey D Luffy",
                    Text = "I'm gonna be king of the pirates!"
                 },
            };

            return Task.FromResult(comments);
        }

        public Task<Tag?> GetTagAsync(string id)
        {
            return Task.FromResult(new Tag() { Id = id, Name = $"Tag {id}" });
        }

        public async Task<List<Tag>> GetTagsAsync()
        {
            List<Tag> list = new();
            for (int a = 0; a < 10; a++)
            {
                list.Add(await GetTagAsync($"{a}"));
            }
            return list;
        }

        public Task<BlogPost?> SaveBlogPostAsync(BlogPost item)
        {
            return Task.FromResult(item);
        }

        public Task<Category?> SaveCategoryAsync(Category item)
        {
            return Task.FromResult(item);
        }

        public Task<Comment?> SaveCommentAsync(Comment item)
        {
            return Task.FromResult(item);
        }

        public Task<Tag?> SaveTagAsync(Tag item)
        {
            return Task.FromResult(item);
        }
    }
}
