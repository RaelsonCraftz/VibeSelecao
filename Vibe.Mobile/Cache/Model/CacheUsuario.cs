using System;
using System.Collections.Generic;
using System.Text;

namespace Vibe.Mobile.Cache.Model
{
    // O id do usuário é uma string (CPF)
    public class CacheUsuario : CacheEntity<string>
    {
        public string Nome { get; set; }
        public string Nascimento { get; set; }
    }
}
