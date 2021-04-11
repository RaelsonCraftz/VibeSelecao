using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Vibe.Mobile.Cache.Model;

namespace Vibe.Mobile.Cache.Repository
{
    public interface IClienteRepository : IBaseRepository<CacheCliente, string>
    {

    }

    public class ClienteRepository : BaseRepository<CacheCliente, string>, IClienteRepository
    {
        public ClienteRepository(SQLiteConnection context) : base(context) { }

        #region Implementation



        #endregion
    }
}
