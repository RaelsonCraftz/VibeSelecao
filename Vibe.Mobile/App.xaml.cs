using Vibe.Mobile.Services.API;
using Vibe.Mobile.Services.Shared;
using Vibe.Mobile.Views;
using Xamarin.Craftz.Services;
using Xamarin.Forms;

namespace Vibe.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            // Registrando serviços que não são de plataforma
            DependencyService.Register<ApplicationService>();
            DependencyService.Register<HttpService>();
            DependencyService.Register<LogService>();

            // Serviços de API
            DependencyService.Register<AutenticacaoService>();
            DependencyService.Register<UsuarioService>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
