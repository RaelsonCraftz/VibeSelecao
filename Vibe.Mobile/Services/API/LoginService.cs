using System.Threading.Tasks;
using Vibe.Domain.Model.Input;
using Vibe.Domain.Services;

namespace Vibe.Mobile.Services.API
{
    public class LoginService : BaseService, ILoginService
    {
        public async Task<string> Autenticacao(LoginInput input)
        {
            return await Read(await http.Client.PostAsync(GetEndpoint(), GetJsonContent(input)));
        }
    }
}
