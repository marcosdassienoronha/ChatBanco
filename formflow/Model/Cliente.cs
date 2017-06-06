

using System;
using System.Collections;


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
        public float Divida { get; set; }
        public Oferta oferta;
        public ArrayList ofertas ;
        Conexao conexao ;

        public Cliente(){
            oferta = new Oferta();
            ofertas = new ArrayList();
            //conexao = new Conexao();
        }
        public void validarOferta(String _status) { //altera status da oferta e já adiciona outra
            conexao = new Conexao();            
            conexao.updateOferta(this.oferta.IdOferta, _status);
            countOfertas();
        }
        // retorna a quantidade de ofertas disponivel para oferecer ao cliente
        // para retornar a quantidade de ofertas ainda disponivel basta usar [cliente].ofertas.Count;
        public int countOfertas() { 
            conexao = new Conexao();  
            ofertas = conexao.ObterOfertas(this.IdCliente);
            //foreach (Oferta _oferta in ofertas) {
            //    if (_oferta.status == "offered")
            //        ofertas.Remove(_oferta);
            //}
            //this.oferta = (Oferta)ofertas[0];
            return ofertas.Count;
        }
    }
}