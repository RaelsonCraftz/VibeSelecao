using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vibe.Domain.Model.Output
{
    public class BaseOutput
    {
        [JsonProperty("mensagem")]
        public string Mensagem { get; set; }
    }
}
