using System;
using System.Collections.Generic;
using System.Text;

namespace Vibe.Mobile.Cache.Model
{
    public class CacheCliente : CacheEntity<string>
    {
        public string IdUsuario { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public bool Especial { get; set; }
    }
}
