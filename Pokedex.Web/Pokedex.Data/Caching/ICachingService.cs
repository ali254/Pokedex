namespace Pokedex.Data.Caching
{
    public interface ICachingService
    {
        public bool ContainsKey(string key);

        public T Get<T>(string key);

        public void Set<T>(string key, T obj, int seconds);

        public void Remove(string key);
    }
}
