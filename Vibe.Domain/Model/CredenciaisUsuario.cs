using Newtonsoft.Json;

namespace Vibe.Domain.Model
{
    public class CredenciaisUsuario
    {
        public string Cpf { get; set; }

        public string SenhaHash { get; set; }
    }
}
