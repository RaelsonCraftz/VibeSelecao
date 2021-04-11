using SQLite;
using System;
using System.IO;
using Vibe.Mobile.Cache.Model;
using Vibe.Mobile.Cache.Repository;
using Xamarin.Forms;

namespace Vibe.Mobile.Services.Shared
{
    public interface ICacheService
    {
        IClienteRepository ClienteRepository { get; set; }
        IUsuarioRepository UsuarioRepository { get; set; }
    }

    public class CacheService : ICacheService
    {
        private readonly SQLiteConnection Database;
        private string databaseName = "VibeSelecao.db3";
        private string databasePath;

        public CacheService()
        {
            SetDbPath();

            Database = new SQLiteConnection(databasePath);
            Database.CreateTable<CacheCliente>();
            Database.CreateTable<CacheUsuario>();

            ClienteRepository = new ClienteRepository(Database);
            UsuarioRepository = new UsuarioRepository(Database);
        }

        public void SetDbPath()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
                    break;
                case Device.iOS:
                    SQLitePCL.Batteries_V2.Init();
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName);
                    break;
                default:
                    break;
            }
        }

        public IClienteRepository ClienteRepository { get; set; }
        public IUsuarioRepository UsuarioRepository { get; set; }
    }
}
