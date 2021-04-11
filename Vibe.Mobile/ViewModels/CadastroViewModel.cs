using Acr.UserDialogs;
using Craftz.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Vibe.Domain.Model.Input;
using Vibe.Domain.Services;
using Vibe.Mobile.Core;
using Xamarin.Forms;

namespace Vibe.Mobile.ViewModels
{
    public class CadastroViewModel : BaseViewModel
    {
        private readonly IUsuarioService usuarioService;

        public CadastroViewModel()
        {
            usuarioService = DependencyService.Get<IUsuarioService>();
        }

        private string CpfSemMascara => Cpf
            .Replace("-", string.Empty)
            .Replace(".", string.Empty);

        #region Bindable Properties

        public string Cpf
        {
            get => _cpf;
            set { _cpf = value; OnPropertyChanged(); }
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
        private DateTime _nascimento = DateTime.Today;

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

        public Command Cadastrar
        {
            get { if (_cadastrar == null) _cadastrar = new Command(CadastrarExecute); return _cadastrar; }
        }
        private Command _cadastrar;
        private void CadastrarExecute()
        {
            _ = SetBusyAsync(async () =>
            {
                await logService.LogRequestAsync(async () =>
                {
                    var cadastro = await usuarioService.Usuario(new CriarUsuarioInput
                    {
                        Cpf = CpfSemMascara,
                        Nome = this.Nome,
                        Nascimento = DateTime.SpecifyKind(this.Nascimento, DateTimeKind.Utc).ToString("o"),
                        Senha = SenhaHash,
                    });

                    // Caso haja alguma mensagem de erro enviada pelo sistema
                    if (cadastro != null &&
                        !string.IsNullOrWhiteSpace(cadastro.Mensagem))
                    {
                        UserDialogs.Instance.Toast(cadastro.Mensagem);
                        return;
                    }

                    await Shell.Current.GoToAsync($"//{Rotas.Autenticacao}");

                    UserDialogs.Instance.Toast("Cadastro feito com sucesso");
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
