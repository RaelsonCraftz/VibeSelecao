using Craftz.ViewModel;
using Vibe.Domain.Model;
using Vibe.Mobile.Assets.UI;

namespace Vibe.Mobile.ViewModels.Elements
{
    public class ClienteElement : BaseElement<Cliente>
    {
        public ClienteElement() : base() { }

        public ClienteElement(Cliente model) : base(model) { }

        #region Bindable Properties

        public string Estrela => 
            Model.Especial 
            ? MaterialIcon.Star 
            : MaterialIcon.StarBorder;

        public string Cpf
        {
            get => Model.Cpf;
            set { Model.Cpf = value; OnPropertyChanged(); }
        }

        public string Nome
        {
            get => Model.Nome;
            set { Model.Nome = value; OnPropertyChanged(); }
        }

        #endregion
    }
}
