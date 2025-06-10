namespace Meteorites.Business.Services
{
    public interface ICacheService
    {
        void Set<T>(string key, T value, TimeSpan? expirationTime);

        T? Get<T>(string key);

        void Delete(string key);
    }
}
