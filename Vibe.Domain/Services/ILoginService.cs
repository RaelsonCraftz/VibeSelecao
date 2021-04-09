using System.Threading.Tasks;
using Vibe.Domain.Model.Input;

namespace Vibe.Domain.Services
{
    public interface ILoginService
    {
        Task<string> Autenticacao(LoginInput login);
    }
}
