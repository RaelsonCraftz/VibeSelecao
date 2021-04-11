using Craftz.Views;
using Vibe.Domain.Model;
using Vibe.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace Vibe.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClienteDetailPage : BasePage<ClienteDetailViewModel, Cliente>
    {
        public ClienteDetailPage()
        {
            InitializeComponent();
        }
    }
}