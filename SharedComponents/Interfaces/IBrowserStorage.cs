namespace SharedComponents.Interfaces
{
    public interface IBrowserStorage
    {
        Task<T?> GetAsnyc<T>(string key);
        Task SetAsync(string key, object value);
        Task DeleteAsync(string key);
    }
}
