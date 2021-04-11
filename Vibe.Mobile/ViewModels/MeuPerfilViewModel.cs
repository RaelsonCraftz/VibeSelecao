using Acr.UserDialogs;
using Craftz.ViewModel;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Vibe.Domain.Model;
using Vibe.Domain.Model.Input;
using Vibe.Domain.Services;
using Vibe.Mobile.Cache.Model;
using Vibe.Mobile.Core;
using Vibe.Mobile.Extensions;
using Vibe.Mobile.Services.Shared;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Vibe.Mobile.ViewModels
{
    public class MeuPerfilViewModel : BaseViewModel
    {
        private readonly IApplicationService applicationService;
        private readonly IAutenticacaoService autenticacaoService;
        private readonly ICacheService cacheService;
        private readonly IUsuarioService usuarioService;

        public MeuPerfilViewModel()
        {
            applicationService = DependencyService.Get<IApplicationService>();
            autenticacaoService = DependencyService.Get<IAutenticacaoService>();
            cacheService = DependencyService.Get<ICacheService>();
            usuarioService = DependencyService.Get<IUsuarioService>();
        }

        #region Bindable Properties

        public bool IsOffline
        {
            get => _isOffline;
            set { _isOffline = value; OnPropertyChanged(); Reconectar?.ChangeCanExecute(); }
        }
        private bool _isOffline;

        public string CpfComMascara => Cpf?.ToCpfFormat() ?? string.Empty;

        public string Cpf
        {
            get => _cpf;
            set { _cpf = value; OnPropertyChanged(); OnPropertyChanged(nameof(CpfComMascara)); }
        }
        private string _cpf;

        public string Nome
        {
            get => _nome;
            set { _nome = value; OnPropertyChanged(); }
        }
        private string _nome;

        public DateTime Nascimento
        {
            get => _nascimento;
            set { _nascimento = value; OnPropertyChanged(); }
        }
        private DateTime _nascimento;

        #endregion

        #region Commands

        public Command Reconectar
        {
            get { if (_reconectar == null) _reconectar = new Command(ReconectarExecute, () => IsOffline); return _reconectar; }
        }
        private Command _reconectar;
        private void ReconectarExecute()
        {
            _ = SetBusyAsync(async () =>
            {
                await CarregarUsuario();
            });
        }

        #endregion

        #region Helpers

        private async Task CarregarUsuario()
        {
            // Caso haja acesso a internet
            if (Connectivity.NetworkAccess == NetworkAccess.Internet &&
                await IsServiceOnline())
            {
                await CarregarPerfilOnline();
                return;
            }

            // Caso o acesso a internet esteja restrito
            CarregarPerfilOffline();
        }

        private async Task CarregarPerfilOnline()
        {
            await logService.LogRequestAsync(async () =>
            {
                var cpf = Preferences.Get(PreferenceKeys.UsuarioCpf, null);

                // Requisição com informações do usuário na API
                var usuario = await usuarioService.Usuario(cpf);
                Cpf = usuario.Cpf;
                Nome = usuario.Nome;
                Nascimento = DateTime.ParseExact(usuario.Nascimento, Formats.DateTimeFormat, CultureInfo.InvariantCulture);

                cacheService.UsuarioRepository.AddOrReplace(new CacheUsuario
                {
                    Id = usuario.Cpf,
                    Nome = usuario.Nome,
                    Nascimento = usuario.Nascimento,
                });

                IsOffline = false;
            });
        }

        private void CarregarPerfilOffline()
        {
            logService.LogAction(() =>
            {
                var cpf = Preferences.Get(PreferenceKeys.UsuarioCpf, null);

                // Busca informações do usuário que estão no cache
                var usuario = cacheService.UsuarioRepository.Get(cpf);
                Cpf = usuario.Id;
                Nome = usuario.Nome;
                Nascimento = DateTime.ParseExact(usuario.Nascimento, Formats.DateTimeFormat, CultureInfo.InvariantCulture);

                UserDialogs.Instance.Toast("Você está em modo offline");

                IsOffline = true;
            });
        }

        private async Task<bool> IsServiceOnline()
        {
            //TODO: checar com o time to backend se isso é suficiente
            var result = await http.Client.GetAsync(AppConsts.RemoteApiUrl.Replace("/api", string.Empty));
            return !string.IsNullOrEmpty(await result.Content?.ReadAsStringAsync());
        }

        #endregion

        #region Initializers

        public override void Initialize()
        {
            base.Initialize();

            _ = SetBusyAsync(async () =>
            {
                await CarregarUsuario();
            });
        }

        #endregion
    }
}
