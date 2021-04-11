using SQLite;

namespace Vibe.Mobile.Cache.Model
{
    public class CacheEntity
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
    }

    public class CacheEntity<T>
    {
        [PrimaryKey]
        public T Id { get; set; }
    }
}
