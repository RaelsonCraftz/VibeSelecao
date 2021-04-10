using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vibe.Domain.Model.Input
{
    public class AutenticacaoInput
    {
        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("senha")]
        public string Senha { get; set; }
    }
}
