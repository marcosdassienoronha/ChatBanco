using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace formflow.Model
{
    public class Conexao
    {
        //objeto cliente que será retornado pelo método 
        private Cliente cliente;
        private Oferta oferta;
        SqlDataReader leitor;
        SqlCommand cmd;

        //instância da conexão 
        SqlConnection conn;
        //public string Nome { get; set; }
        public Conexao() {
            cliente = new Cliente();
            oferta = new Oferta();
            conn = new SqlConnection(@"Data Source=server1500fhcurso.database.windows.net;Initial Catalog=db1500fh;User ID=user1500fh;Password=15@@fh123;");
            //abro conexão 
            conn.Open();
        }

        //método que faz a consulta no bd e obtém o cliente 
        //cujo o nome é informado pelo parâmetro 
        public Cliente ObterClientePorNome(string nome)
        {
            
            //string com o comando a ser executado 
            string sql = "SELECT * from bot.cliente WHERE nome_cliente=@Nome";

            //instância do comando recebendo como parâmetro 
            cmd = new SqlCommand(sql, conn);

            //informo o parâmetro do comando 
            cmd.Parameters.AddWithValue("@Nome", nome);

            
            //instância do leitor 
            leitor = cmd.ExecuteReader();
            formatCliente(leitor);
            return cliente;
        }
        public Cliente ObterClientePorStatus(string _status)
        {
            string sql = "SELECT * from bot.cliente WHERE status=@Status";
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Status", _status);
            leitor = cmd.ExecuteReader();
            formatCliente(leitor); 
            return cliente;
        }
        public Oferta ObterOferta(int _cliente)
        {
            SqlDataReader leitor;
            SqlCommand cmd; 
            conn.Open();
            string sql = "SELECT * from bot.ofertas WHERE id_cliente=@Cliente";
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Cliente", _cliente);
            leitor = cmd.ExecuteReader();
            formatOferta(leitor);
            return oferta;
        }


        private Oferta formatOferta(SqlDataReader _leitor)
        {
            while (_leitor.Read())
            {
                oferta.IdOferta = Convert.ToInt32(_leitor["id_oferta"].ToString());
                oferta.ValorDivida = float.Parse(_leitor["valor_divida"].ToString());
                oferta.ValorOferta = float.Parse(_leitor["valor_oferta"].ToString());
                oferta.NumParcelas = int.Parse(_leitor["num_parcelas"].ToString());
            }

            conn.Close();
            return oferta;
        }
        private Cliente formatCliente(SqlDataReader _leitor) {
            while (_leitor.Read())
            {
                //passo os valores para o objeto cliente 
                //que será retornado 
                cliente.IdCliente = Convert.ToInt32(_leitor["id_cliente"].ToString());
                cliente.Nome = _leitor["nome_cliente"].ToString();
                cliente.Conta = _leitor["conta"].ToString();
                cliente.Agencia = _leitor["agencia"].ToString();
                cliente.CodigoPostal = _leitor["cod_postal"].ToString();
                cliente.CodigoEstado = _leitor["cod_estado"].ToString();
                cliente.Endereco = _leitor["endereco"].ToString();
                cliente.DDDTelefone = _leitor["ddd_telefone"].ToString();
                cliente.Telefone = _leitor["telefone"].ToString();
                cliente.DDDFax = _leitor["ddd_fax"].ToString();
                cliente.Fax = _leitor["fax"].ToString();
                cliente.CPF = _leitor["cpf"].ToString();
                cliente.Status = _leitor["status"].ToString();
            }
            conn.Close();
            return cliente;
        }
    }
}