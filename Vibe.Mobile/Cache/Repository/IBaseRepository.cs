using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vibe.Mobile.Cache.Model;

namespace Vibe.Mobile.Cache.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : CacheEntity
    {
        TEntity Get(int id);
        List<TEntity> GetAll();
        List<TEntity> Find(Func<TEntity, bool> predicate);
        int Add(TEntity entity);
        int AddRange(List<TEntity> entities);
        int AddOrReplace(TEntity entity);
        int Update(TEntity entity);
        int UpdateRange(List<TEntity> entities);
        int Remove(TEntity entity);
    }

    public interface IBaseRepository<TEntity, TKey> where TEntity : CacheEntity<TKey>
    {
        TEntity Get(TKey id);
        List<TEntity> GetAll();
        List<TEntity> Find(Func<TEntity, bool> predicate);
        int Add(TEntity entity);
        int AddRange(List<TEntity> entities);
        int AddOrReplace(TEntity entity);
        int Update(TEntity entity);
        int UpdateRange(List<TEntity> entities);
        int Remove(TEntity entity);
    }

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : CacheEntity, new()
    {
        protected readonly SQLiteConnection database;

        public BaseRepository(SQLiteConnection context)
        {
            database = context;
        }

        public TEntity Get(int id)
        {
            return this.database.Find<TEntity>(id);
        }

        public List<TEntity> GetAll()
        {
            var entities = this.database.Table<TEntity>().ToList();

            return entities;
        }

        public List<TEntity> Find(Func<TEntity, bool> predicate)
        {
            var entities = this.database.Table<TEntity>().Where(predicate).ToList();

            return entities;
        }

        public int Add(TEntity entity)
        {
            return this.database.Insert(entity);
        }

        public int AddRange(List<TEntity> entities)
        {
            return this.database.InsertAll(entities);
        }

        public int AddOrReplace(TEntity entity)
        {
            return this.database.InsertOrReplace(entity);
        }

        public int Update(TEntity entity)
        {
            return this.database.Update(entity);
        }

        public int UpdateRange(List<TEntity> entities)
        {
            return this.database.UpdateAll(entities);
        }

        public int Remove(TEntity entity)
        {
            return this.database.Delete(entity);
        }
    }

    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : CacheEntity<TKey>, new()
    {
        protected readonly SQLiteConnection database;

        public BaseRepository(SQLiteConnection context)
        {
            database = context;
        }

        public TEntity Get(TKey id)
        {
            return this.database.Find<TEntity>(id);
        }

        public List<TEntity> GetAll()
        {
            var entities = this.database.Table<TEntity>().ToList();

            return entities;
        }

        public List<TEntity> Find(Func<TEntity, bool> predicate)
        {
            var entities = this.database.Table<TEntity>().Where(predicate).ToList();

            return entities;
        }

        public int Add(TEntity entity)
        {
            return this.database.Insert(entity);
        }

        public int AddRange(List<TEntity> entities)
        {
            return this.database.InsertAll(entities);
        }

        public int AddOrReplace(TEntity entity)
        {
            return this.database.InsertOrReplace(entity);
        }

        public int Update(TEntity entity)
        {
            return this.database.Update(entity);
        }

        public int UpdateRange(List<TEntity> entities)
        {
            return this.database.UpdateAll(entities);
        }

        public int Remove(TEntity entity)
        {
            return this.database.Delete(entity);
        }
    }
}
