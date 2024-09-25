
namespace RRBank.Application.Services.Caching
{
    public interface ICachingService
    {
        Task SetAsync<T>(string key, T value);
        Task<T> GetAsync<T>(string key);
        Task SetListAsync<T>(string key, List<T> list);
        Task<List<T>> GetListAsync<T>(string key);
    }
}
