using Vibe.Mobile.Services.Shared;
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
            DependencyService.Register<LogService>();
            DependencyService.Register<HttpService>();
            DependencyService.Register<ApplicationService>();

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
