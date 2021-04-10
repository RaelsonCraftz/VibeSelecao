using System.Threading.Tasks;
using Vibe.Domain.Model.Input;
using Vibe.Domain.Model.Output;

namespace Vibe.Domain.Services
{
    public interface IAutenticacaoService
    {
        Task<AutenticacaoOutput> Autenticacao(AutenticacaoInput login);
    }
}
