using Craftz.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vibe.Mobile.Core;
using Vibe.Mobile.Services.Shared;
using Xamarin.Craftz.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vibe.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : BasePage
    {
        public LoadingPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Thread de background
            Task.Run(async () =>
            {
                // Buffer para a inicialização
                await Task.Delay(500);

                // Animação inicial
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    stack.FadeTo(1, 1000, Easing.CubicOut);
                    stack.TranslateTo(0, 48, 1000, Easing.CubicOut);
                    image.TranslateTo(0, -48, 1000, Easing.CubicOut);
                });

                // Aguarda as animações
                await Task.Delay(2000);

                // Inicializando os serviços necessários
                var applicationService = DependencyService.Get<IApplicationService>();
                var httpService = DependencyService.Get<IHttpService>();

                // Checa as informações de usuário no cache do dispositivo
                var accessToken = Preferences.Get(PreferenceKeys.AccessToken, null);
                var cpf = Preferences.Get(PreferenceKeys.UsuarioCpf, null);
                var senhaHash = Preferences.Get(PreferenceKeys.UsuarioSenhaHash, null);

                // Caso não exista nada, ir para autenticacao
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    if (string.IsNullOrWhiteSpace(cpf) || string.IsNullOrWhiteSpace(senhaHash) || string.IsNullOrWhiteSpace(accessToken))
                    {
                        await Shell.Current.GoToAsync($"//{Rotas.Autenticacao}");
                        return;
                    }

                    // Caso exista algo, ir para tela inicial após definir o access token no header padrão do HttpClient
                    httpService.SetBearer(accessToken);
                    await Shell.Current.GoToAsync($"//{Rotas.MeuPerfil}");
                });
            });
        }
    }
}