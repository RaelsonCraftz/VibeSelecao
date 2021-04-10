using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vibe.Domain.Model.Input;

namespace Vibe.Domain.Services
{
    public interface IUsuarioService
    {
        Task<string> Usuario(CriarUsuarioInput input);
    }
}
