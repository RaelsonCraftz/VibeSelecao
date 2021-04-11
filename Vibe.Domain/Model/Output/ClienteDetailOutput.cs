using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vibe.Domain.Model.Output
{
    public class ClienteDetailOutput
    {
        [JsonProperty("urlImagem")]
        public string UrlImagem { get; set; }

        [JsonProperty("empresa")]
        public string Empresa { get; set; }

        [JsonProperty("endereco")]
        public ClienteEndereco Endereco { get; set; }
    }
}
