using Acr.UserDialogs;
using Craftz.ViewModel;
using System.Threading.Tasks;
using Vibe.Domain.Model;
using Vibe.Domain.Model.Input;
using Vibe.Domain.Services;
using Vibe.Mobile.Core;
using Vibe.Mobile.Services.Shared;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Vibe.Mobile.ViewModels
{
    public class AutenticacaoViewModel : BaseViewModel
    {
        private readonly IApplicationService applicationService;
        private readonly IAutenticacaoService autenticacaoService;
        private readonly ICacheService cacheService;

        public AutenticacaoViewModel()
        {
            applicationService = DependencyService.Get<IApplicationService>();
            autenticacaoService = DependencyService.Get<IAutenticacaoService>();
            cacheService = DependencyService.Get<ICacheService>();
        }
        
        private string Cpf => CpfComMascara
            .Replace("-", string.Empty)
            .Replace(".", string.Empty);

        #region Bindable Properties

        public string CpfComMascara
        {
            get => _cpfComMascara;
            set { _cpfComMascara = value; OnPropertyChanged(); }
        }
        private string _cpfComMascara;

        // A senha é armazenada na memória da aplicação já criptografada
        public string SenhaHash
        {
            get => _senhaHash;
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _senhaHash = string.Empty;
                    return;
                }

                _senhaHash = Crypto.CalcularMD5Hash(value); 
                OnPropertyChanged(); 
            }
        }
        private string _senhaHash;

        #endregion

        #region Commands

        public Command Autenticar
        {
            get { if (_autenticar == null) _autenticar = new Command(AutenticarExecute); return _autenticar; }
        }
        private Command _autenticar;
        private void AutenticarExecute()
        {
            _ = SetBusyAsync(async () =>
            {
                await logService.LogRequestAsync(async () =>
                {
                    var autenticacao = await autenticacaoService.Autenticacao(new AutenticacaoInput
                    {
                        Cpf = this.Cpf,
                        Senha = SenhaHash,
                    });

                    // Caso não haja chave, exibir a mensagem enviada pela API
                    if (string.IsNullOrWhiteSpace(autenticacao.Chave))
                    {
                        UserDialogs.Instance.Toast(autenticacao.Mensagem);
                        return;
                    }

                    applicationService.SetCacheUsuario(autenticacao.Chave, new CredenciaisUsuario
                    {
                        Cpf = this.Cpf,
                        SenhaHash = this.SenhaHash,
                    });

                    await Shell.Current.GoToAsync($"//{Rotas.MeuPerfil}");
                },
                log =>
                {
                    if (!string.IsNullOrWhiteSpace(log))
                        UserDialogs.Instance.Toast(log);

                    return Task.CompletedTask;
                });
            });
        }

        public Command AbrirCadastro
        {
            get { if (_abrirCadastro == null) _abrirCadastro = new Command(AbrirCadastroExecute); return _abrirCadastro; }
        }
        private Command _abrirCadastro;
        private void AbrirCadastroExecute()
        {
            logService.LogActionAsync(async () =>
            {
                await Shell.Current.GoToAsync(Rotas.Cadastro);
            });
        }

        #endregion

        #region Helpers



        #endregion

        #region Initializers

        public override void Initialize()
        {
            // Inicialize os serviços do BaseViewModel
            base.Initialize();
        }

        #endregion
    }
}
