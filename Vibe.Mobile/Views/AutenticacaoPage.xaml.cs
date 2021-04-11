using Craftz.Views;
using Vibe.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace Vibe.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AutenticacaoPage : BasePage<AutenticacaoViewModel>
    {
        public AutenticacaoPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            senhaEntry.Text = string.Empty;
        }
    }
}