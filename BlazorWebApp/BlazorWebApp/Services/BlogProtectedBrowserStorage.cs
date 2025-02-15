using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SharedComponents.Interfaces;

namespace BlazorWebApp.Services
{
    public class BlogProtectedBrowserStorage : IBrowserStorage
    {
        private readonly ProtectedSessionStorage _storage;

        public BlogProtectedBrowserStorage(ProtectedSessionStorage storage)
        {
            _storage = storage;
        }

        public async Task DeleteAsync(string key)
        {
            await _storage.DeleteAsync(key);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _storage.GetAsync<T>(key);
            return value.Success ? value.Value : default;
        }

        public async Task SetAsync(string key, object value)
        {
            await _storage.SetAsync(key, value);
        }
    }
}
