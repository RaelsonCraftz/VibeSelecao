using Acr.UserDialogs;
using Craftz.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vibe.Domain.Model;
using Vibe.Domain.Services;
using Vibe.Mobile.Cache.Model;
using Vibe.Mobile.Core;
using Vibe.Mobile.Services.Shared;
using Vibe.Mobile.ViewModels.Elements;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Vibe.Mobile.ViewModels
{
    public class ClientesViewModel : BaseViewModel
    {
        private readonly ICacheService cacheService;
        private readonly IClienteService clienteService;

        public ClientesViewModel()
        {
            cacheService = DependencyService.Get<ICacheService>();
            clienteService = DependencyService.Get<IClienteService>();
        }

        #region Bindable Properties

        public bool IsOffline
        {
            get => _isOffline;
            set { _isOffline = value; OnPropertyChanged(); VerCliente.ChangeCanExecute(); }
        }
        private bool _isOffline;

        public ObservableRangeCollection<ClienteElement> Clientes { get; } = new ObservableRangeCollection<ClienteElement>();

        #endregion

        #region Commands

        public Command Reconectar
        {
            get { if (_reconectar == null) _reconectar = new Command(ReconectarExecute); return _reconectar; }
        }
        private Command _reconectar;
        private void ReconectarExecute()
        {
            _ = SetBusyAsync(async () =>
            {
                await CarregarClientes();
            });
        }

        public Command<Cliente> VerCliente
        {
            get { if (_verCliente == null) _verCliente = new Command<Cliente>(VerClienteExecute, c => !IsOffline); return _verCliente; }
        }
        private Command<Cliente> _verCliente;
        private void VerClienteExecute(Cliente cliente)
        {
            logService.LogActionAsync(async () =>
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    UserDialogs.Instance.Toast("Você está em modo offline");

                    IsOffline = true;

                    return;
                }

                var json = JsonConvert.SerializeObject(cliente);
                await Shell.Current.GoToAsync($"{Rotas.ClienteDetail}?model={json}");
            },
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Toast(log);

                return Task.CompletedTask;
            });
        }

        #endregion

        #region Helpers

        private async Task CarregarClientes()
        {
            // Caso haja acesso a internet e a API está no ar
            if (Connectivity.NetworkAccess == NetworkAccess.Internet && 
                await IsServiceOnline())
            {
                await CarregarClientesOnline();
                return;
            }

            // Caso o acesso a internet esteja restrito
            CarregarClientesOffline();
        }

        private async Task CarregarClientesOnline()
        {
            await logService.LogRequestAsync(async () =>
            {
                var cpf = Preferences.Get(PreferenceKeys.UsuarioCpf, null);

                // Requisição de clientes na API
                var clientes = await clienteService.Cliente();

                Clientes.ReplaceRange(clientes.Select(l => new ClienteElement(l)));

                // Inserindo a nova lista de clientes no cache
                var cacheClientes = clientes.Select(l => new CacheCliente
                {
                    Id = l.Id,
                    IdUsuario = cpf,
                    Nome = l.Nome,
                    Cpf = l.Cpf,
                    Especial = l.Especial,
                }).ToList();
                cacheClientes.ForEach(l => cacheService.ClienteRepository.AddOrReplace(l));

                IsOffline = false;
            });
        }

        private void CarregarClientesOffline()
        {
            logService.LogAction(() =>
            {
                var cpf = Preferences.Get(PreferenceKeys.UsuarioCpf, null);

                // Busca os clientes que estão no cache
                var cacheClientes = cacheService.ClienteRepository.Find(l => l.IdUsuario == cpf);

                // Convertendo a lista do cache para um tipo legível para o ClienteElement
                var clientes = cacheClientes.Select(l => new Cliente
                {
                    Id = l.Id,
                    Cpf = l.Cpf,
                    Nome = l.Nome,
                    Especial = l.Especial,
                });

                Clientes.ReplaceRange(clientes.Select(l => new ClienteElement(l)));

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
            // Inicializa os serviços do BaseViewModel
            base.Initialize();

            _ = SetBusyAsync(async () =>
            {
                await CarregarClientes();
            });
        }

        #endregion
    }
}
