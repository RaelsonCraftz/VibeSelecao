using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Craftz.Core;

namespace Xamarin.Craftz.Services
{
    public interface IHttpService
    {
        HttpClient Client { get; }

        void SetBearer(string accessToken);

        void ClearBearer();
    }

    public class HttpService : IHttpService
    {
        public HttpClient Client { get; private set; }

        public HttpService()
        {
            Client = new HttpClient();
        }

        public void SetBearer(string accessToken)
        {
            Client.DefaultRequestHeaders.Add(DefaultHeaders.Authorization, $"Bearer {accessToken}");
        }

        public void ClearBearer()
        {
            Client.DefaultRequestHeaders.Clear();
        }
    }
}
