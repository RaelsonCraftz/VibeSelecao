using Acr.UserDialogs;
using Craftz.ViewModel;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Vibe.Domain.Model.Input;
using Vibe.Domain.Services;
using Vibe.Mobile.Services.Shared;
using Xamarin.Forms;

namespace Vibe.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAutenticacaoService autenticacaoService;
        private readonly IApplicationService applicationService;

        public LoginViewModel()
        {
            autenticacaoService = DependencyService.Get<IAutenticacaoService>();
            applicationService = DependencyService.Get<IApplicationService>();
        }

        #region Bindable Properties

        public string CPFSemMascara => CPF
            .Replace("-", "")
            .Replace(".", "");

        public string CPF
        {
            get => _cpf;
            set { _cpf = value; OnPropertyChanged(); }
        }
        private string _cpf;

        // A senha é armazenada na memória da aplicação já criptografada
        public string PasswordHash
        {
            get => _passwordHash;
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _passwordHash = string.Empty;
                    return;
                }

                _passwordHash = CalcularMD5Hash(value); 
                OnPropertyChanged(); 
            }
        }
        private string _passwordHash;

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
                        Cpf = CPF,
                        Senha = PasswordHash,
                    });

                    // Caso não haja chave, exibir a mensagem enviada pela API
                    if (string.IsNullOrWhiteSpace(autenticacao.Chave))
                    {
                        UserDialogs.Instance.Toast(autenticacao.Mensagem);
                        return;
                    }

                    applicationService.SetToken(autenticacao.Chave);

                    //TODO: ir para a próxima tela
                },
                log =>
                {
                    if (!string.IsNullOrWhiteSpace(log))
                        UserDialogs.Instance.Toast(log);

                    return Task.CompletedTask;
                });
            });
        }

        #endregion

        #region Helpers

        private string CalcularMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        #endregion

        #region Initializers

        public override void Initialize()
        {
            // Inicialize os serviços no BaseViewModel
            base.Initialize();
        }

        #endregion
    }
}
