using Craftz.Views;
using Vibe.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace Vibe.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CadastroPage : BasePage<CadastroViewModel>
    {
        public CadastroPage()
        {
            InitializeComponent();
        }
    }
}