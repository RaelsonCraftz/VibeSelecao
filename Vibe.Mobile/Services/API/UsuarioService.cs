using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vibe.Domain.Model.Input;
using Vibe.Domain.Model.Output;
using Vibe.Domain.Services;

namespace Vibe.Mobile.Services.API
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        //TODO: nome não está intuitivo. Avaliar com a equipe do backend convenção mais intuitiva para o nome das rotas
        // Exemplo: api/{serviço}/{metodo} -> api/Usuario/Criar
        public async Task<BaseOutput> Usuario(CriarUsuarioInput input)
        {
            return await Read<BaseOutput>(await http.Client.PostAsync(GetEndpoint(), GetJsonContent(input)));
        }

        public async Task<UsuarioOutput> Usuario(string cpf)
        {
            return await Read<UsuarioOutput>(await http.Client.GetAsync($"{GetEndpoint()}/{cpf}"));
        }
    }
}
