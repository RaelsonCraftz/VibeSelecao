using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Vibe.Mobile.Cache.Model;

namespace Vibe.Mobile.Cache.Repository
{
    public interface IUsuarioRepository : IBaseRepository<CacheUsuario, string>
    {

    }

    public class UsuarioRepository : BaseRepository<CacheUsuario, string>, IUsuarioRepository
    {
        public UsuarioRepository(SQLiteConnection context) : base(context) { }

        #region Implementation



        #endregion
    }
}
