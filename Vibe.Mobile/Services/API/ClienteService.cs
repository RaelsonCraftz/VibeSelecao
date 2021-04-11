using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vibe.Domain.Model;
using Vibe.Domain.Model.Output;
using Vibe.Domain.Services;

namespace Vibe.Mobile.Services.API
{
    public class ClienteService : BaseService, IClienteService
    {
        public async Task<List<Cliente>> Cliente()
        {
            return await Read<List<Cliente>>(await http.Client.GetAsync(GetEndpoint()));
        }

        public async Task<ClienteDetailOutput> Cliente(string id)
        {
            return await Read<ClienteDetailOutput>(await http.Client.GetAsync($"{GetEndpoint()}/{id}"));
        }
    }
}
