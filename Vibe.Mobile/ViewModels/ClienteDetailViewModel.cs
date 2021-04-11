using Acr.UserDialogs;
using Craftz.ViewModel;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Vibe.Domain.Model;
using Vibe.Domain.Services;
using Vibe.Mobile.ViewModels.Elements;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Vibe.Mobile.ViewModels
{
    public class ClienteDetailViewModel : BaseViewModel<Cliente>
    {
        private readonly IClienteService clienteService;

        public ClienteDetailViewModel()
        {
            clienteService = DependencyService.Get<IClienteService>();
        }

        #region Bindable Properties

        public ClienteElement Cliente
        {
            get => _cliente;
            set { _cliente = value; OnPropertyChanged(); }
        }
        private ClienteElement _cliente;

        public ClienteDetailElement Detalhe
        {
            get => _detalhe;
            set { _detalhe = value; OnPropertyChanged(); }
        }
        private ClienteDetailElement _detalhe;

        #endregion

        #region Commands

        private async Task CarregarDetalhes()
        {
            await logService.LogRequestAsync(async () =>
            {
                // Requisição de detalhes do cliente na API
                var detalhes = await clienteService.Cliente(Cliente.Model.Id);

                Detalhe = new ClienteDetailElement(new ClienteDetail
                {
                    UrlImagem = detalhes.UrlImagem,
                    Empresa = detalhes.Empresa,
                    Cidade = detalhes.Endereco.Cidade,
                    Complemento = detalhes.Endereco.Complemento,
                    Endereco = detalhes.Endereco.Endereco,
                    Numero = detalhes.Endereco.Numero,
                });

                var bytesImagem = await DownloadUrlImage(Detalhe.Model.UrlImagem);

                // Se não houver bytes, converter a imagem para a logo
                if (bytesImagem == null)
                {
                    Detalhe.Imagem = ImageSource.FromFile("icLogo");
                    return;
                }

                using (var ms = new MemoryStream(bytesImagem))
                {
                    Detalhe.Imagem = ImageSource.FromStream(() => ms);

                    // Buffer para evitar que o memorystream feche antes do Detalhe.Imagem processar a imagem
                    await Task.Delay(300);
                }
            },
            log =>
            {
                if (log != null)
                    UserDialogs.Instance.Toast(log);

                return Task.CompletedTask;
            });
        }

        private async Task<byte[]> DownloadUrlImage(string url)
        {
            using (var response = await http.Client.GetAsync(url))
            {
                if (response.StatusCode == HttpStatusCode.OK)
                    return await response.Content.ReadAsByteArrayAsync();

                return null;
            }
        }

        #endregion

        #region Helpers



        #endregion

        #region Initializers

        public override void Initialize(Cliente model)
        {
            base.Initialize(model);

            Cliente = new ClienteElement(model);

            _ = SetBusyAsync(async () =>
            {
                await CarregarDetalhes();
            });
        }

        #endregion
    }
}
