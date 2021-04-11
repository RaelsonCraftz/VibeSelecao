using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vibe.Domain.Model
{
    public class Cliente
    {
        public Cliente() { }

        public Cliente(Cliente model)
        {
            Id = model.Id;
            Cpf = model.Cpf;
            Nome = model.Nome;
            Especial = model.Especial;
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("especial")]
        public bool Especial { get; set; }
    }
}
