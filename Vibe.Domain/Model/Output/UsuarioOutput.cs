using Newtonsoft.Json;

namespace Vibe.Domain.Model.Output
{
    public class UsuarioOutput : BaseOutput
    {
        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("nascimento")]
        public string Nascimento { get; set; }
    }
}
