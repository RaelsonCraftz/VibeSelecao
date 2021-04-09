using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Xamarin.Craftz.Services
{
    public interface IHttpService
    {
        HttpClient Client { get; }
    }

    public class HttpService : IHttpService
    {
        public HttpClient Client { get; private set; }

        public HttpService()
        {
            Client = new HttpClient();
        }
    }
}
