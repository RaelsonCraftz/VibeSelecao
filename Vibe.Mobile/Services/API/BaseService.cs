using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Vibe.Mobile.Core;
using Xamarin.Craftz.Services;
using Xamarin.Forms;

namespace Vibe.Mobile.Services.API
{
    public class BaseService
    {
        protected readonly IHttpService http;

        public BaseService()
        {
            http = DependencyService.Get<IHttpService>();
        }

        public string GetEndpoint(object param = null, [CallerMemberName] string method = "")
        {
            var serviceName = this.GetType().Name.Replace("Service", string.Empty);

            var queryString = string.Empty;
            if (param != null)
            {
                var queryParameters = from p in param.GetType().GetProperties()
                                      where p.GetValue(param, null) != null
                                      select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(param, null).ToString());

                queryString = $"?{string.Join("&", queryParameters.ToArray())}";
            }

            return $"{AppConsts.RemoteApiUrl}/{serviceName}/{method}{queryString}";
        }

        public HttpContent GetJsonContent(object input)
        {
            if (input == null)
                return null;

            return new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");
        }

        public async Task<T> Read<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<string> Read(HttpResponseMessage response)
        {
            return await response.Content.ReadAsStringAsync();
        }
    }
}
