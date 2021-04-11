using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vibe.Domain.Model;
using Vibe.Domain.Model.Output;

namespace Vibe.Domain.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> Cliente();
        Task<ClienteDetailOutput> Cliente(string id);
    }
}
