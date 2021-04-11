using Craftz.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using Vibe.Domain.Model;
using Xamarin.Forms;

namespace Vibe.Mobile.ViewModels.Elements
{
    public class ClienteDetailElement : BaseElement<ClienteDetail>
    {
        public ClienteDetailElement() : base() { }

        public ClienteDetailElement(ClienteDetail model) : base(model) { }

        #region Bindable Properties

        public ImageSource Imagem
        {
            get => _imagem;
            set { _imagem = value; OnPropertyChanged(); }
        }
        private ImageSource _imagem;

        public string Empresa
        {
            get => Model.Empresa;
            set { Model.Empresa = value; OnPropertyChanged(); }
        }

        public string Endereco
        {
            get => Model.Endereco;
            set { Model.Endereco = value; OnPropertyChanged(); }
        }

        public string Numero
        {
            get => Model.Numero;
            set { Model.Numero = value; OnPropertyChanged(); }
        }

        public string Complemento
        {
            get => Model.Complemento;
            set { Model.Complemento = value; OnPropertyChanged(); }
        }

        public string Cidade
        {
            get => Model.Cidade;
            set { Model.Cidade = value; OnPropertyChanged(); }
        }

        #endregion
    }
}
