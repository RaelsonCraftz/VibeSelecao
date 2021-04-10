using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vibe.Domain.Model.Output
{
    public class AutenticacaoOutput : BaseOutput
    {
        [JsonProperty("chave")]
        public string Chave { get; set; }
    }
}
