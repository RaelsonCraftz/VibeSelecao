using System;
using System.Collections.Generic;
using System.Text;

namespace Vibe.Domain.Model
{
    public class ClienteDetail
    {
        public ClienteDetail() { }

        public ClienteDetail(ClienteDetail model) 
        {
            UrlImagem = this.UrlImagem;
            Empresa = this.Empresa;
            Endereco = this.Endereco;
            Numero = this.Numero;
            Complemento = this.Complemento;
            Cidade = this.Cidade;
        }

        public string UrlImagem { get; set; }
        public string Empresa { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
    }
}
