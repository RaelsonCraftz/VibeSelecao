using Craftz.ViewModel;
using System.Security.Cryptography;
using System.Text;
using Vibe.Domain.Model.Input;
using Vibe.Domain.Services;
using Vibe.Mobile.Services.Shared;
using Xamarin.Forms;

namespace Vibe.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginService loginService;
        private readonly IApplicationService applicationService;

        public LoginViewModel()
        {
            loginService = DependencyService.Get<ILoginService>();
            applicationService = DependencyService.Get<IApplicationService>();
        }

        #region Bindable Properties

        public string CPF
        {
            get => _cpf;
            set { _cpf = value; OnPropertyChanged(); }
        }
        private string _cpf;

        // A senha é armazenada na memória já criptografada
        public string PasswordHash
        {
            get => _passwordHash;
            set { _passwordHash = CalcularMD5Hash(value); OnPropertyChanged(); }
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
            logService.LogRequestAsync(async () =>
            {
                var token = await loginService.Autenticacao(new LoginInput
                { 
                    cpf = CPF,
                    senha = PasswordHash,
                });

                applicationService.SetToken(token);

                //TODO: continuar com a tela de login
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



        #endregion
    }
}
