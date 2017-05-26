using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace formflow.Model
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Conta { get; set; }
        public string Agencia { get; set; }
        public string CodigoPostal { get; set; }
        public string CodigoEstado { get; set; }
        public string Endereco { get; set; }
        public string DDDTelefone { get; set; }
        public string Telefone { get; set; }
        public string DDDFax { get; set; }
        public string Fax { get; set; }
        public string CPF { get; set; }
        public string Status { get; set; }
    }
}