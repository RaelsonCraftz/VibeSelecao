using System.Threading.Tasks;
using Vibe.Domain.Model.Input;
using Vibe.Domain.Model.Output;

namespace Vibe.Domain.Services
{
    public interface IUsuarioService
    {
        Task<BaseOutput> Usuario(CriarUsuarioInput input);
    }
}
