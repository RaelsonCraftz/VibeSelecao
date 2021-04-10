using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vibe.Domain.Model.Input
{
    public class CriarUsuarioInput
    {
        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("nascimento")]
        public string Nascimento { get; set; }

        [JsonProperty("senha")]
        public string Senha { get; set; }
    }
}
