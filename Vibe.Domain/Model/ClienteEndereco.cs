using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vibe.Domain.Model
{
    public class ClienteEndereco
    {
        [JsonProperty("endereco")]
        public string Endereco { get; set; }

        [JsonProperty("numero")]
        public string Numero { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("cidade")]
        public string Cidade { get; set; }
    }
}
