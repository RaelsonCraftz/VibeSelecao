using Craftz.Views;
using Vibe.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace Vibe.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeuPerfilPage : BasePage<MeuPerfilViewModel>
    {
        public MeuPerfilPage()
        {
            InitializeComponent();
        }
    }
}