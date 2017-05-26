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

        //instância da conexão 
        private SqlConnection conn;
        //public string Nome { get; set; }
        public Conexao() {
            cliente = new Cliente();
            oferta = new Oferta();
            conn = new SqlConnection(@"Data Source=server1500fhcurso.database.windows.net;Initial Catalog=db1500fh;User ID=user1500fh;Password=15@@fh123;");
        }
        //public string conecta(){
        //        string strcon = @"Data Source=server1500fhcurso.database.windows.net;Initial Catalog=db1500fh;User ID=user1500fh;Password=15@@fh123;";
        //        SqlConnection conexao = new SqlConnection(strcon);
        //        SqlCommand cmd = new SqlCommand("SELECT nome_cliente FROM bot.Cliente", conexao);
        //      c     conexao.Open(); // abre a conexão com o banco   
        //        SqlDataReader dr = cmd.ExecuteReader(); // executa cmd
        //        dr.Read();


        //    } 


        //método que faz a consulta no bd e obtém o cliente 
        //cujo o nome é informado pelo parâmetro 
        public Cliente ObterClientePorNome(string nome)
        {
            
            //string com o comando a ser executado 
            string sql = "SELECT * from bot.cliente WHERE nome_cliente=@Nome";

            //instância do comando recebendo como parâmetro 
            //a string com o comando e a conexão 
            SqlCommand cmd = new SqlCommand(sql, conn);

            //informo o parâmetro do comando 
            cmd.Parameters.AddWithValue("@Nome", nome);

            //abro conexão 
            conn.Open();

            //instância do leitor 
            SqlDataReader leitor = cmd.ExecuteReader();

            //enquanto leitor lê 
            while (leitor.Read())
            {
                //passo os valores para o objeto cliente 
                //que será retornado 
                cliente.IdCliente = Convert.ToInt32(leitor["id_cliente"].ToString());
                cliente.Nome = leitor["nome_cliente"].ToString();
                cliente.Conta = leitor["conta"].ToString();
                cliente.Agencia = leitor["agencia"].ToString();
                cliente.CodigoPostal = leitor["cod_postal"].ToString();
                cliente.CodigoEstado = leitor["cod_estado"].ToString();
                cliente.Endereco = leitor["endereco"].ToString();
                cliente.DDDTelefone = leitor["ddd_telefone"].ToString();
                cliente.Telefone = leitor["telefone"].ToString();
                cliente.DDDFax = leitor["ddd_fax"].ToString();
                cliente.Fax = leitor["fax"].ToString();
                cliente.CPF = leitor["cpf"].ToString();
                cliente.Status = leitor["status"].ToString();
            }

            //fecha conexão
            conn.Close();

            //Retorno o objeto cliente cujo o  
            //nome é igual ao informado no parâmetro 
            return cliente;
        }
        public Cliente ObterClientePorStatus(string _status)
        {

            //string com o comando a ser executado 
            string sql = "SELECT * from bot.cliente WHERE status=@Status";

            //instância do comando recebendo como parâmetro 
            //a string com o comando e a conexão 
            SqlCommand cmd = new SqlCommand(sql, conn);

            //informo o parâmetro do comando 
            cmd.Parameters.AddWithValue("@Status", _status);

            //abro conexão 
            conn.Open();

            //instância do leitor 
            SqlDataReader leitor = cmd.ExecuteReader();

            //enquanto leitor lê 
            while (leitor.Read())
            {
                //passo os valores para o objeto cliente 
                //que será retornado 
                cliente.IdCliente = Convert.ToInt32(leitor["id_cliente"].ToString());
                cliente.Nome = leitor["nome_cliente"].ToString();
                cliente.Conta = leitor["conta"].ToString();
                cliente.Agencia = leitor["agencia"].ToString();
                cliente.CodigoPostal = leitor["cod_postal"].ToString();
                cliente.CodigoEstado = leitor["cod_estado"].ToString();
                cliente.Endereco = leitor["endereco"].ToString();
                cliente.DDDTelefone = leitor["ddd_telefone"].ToString();
                cliente.Telefone = leitor["telefone"].ToString();
                cliente.DDDFax = leitor["ddd_fax"].ToString();
                cliente.Fax = leitor["fax"].ToString();
                cliente.CPF = leitor["cpf"].ToString();
                cliente.Status = leitor["status"].ToString();
            }

            //fecha conexão
            conn.Close();

            //Retorno o objeto cliente cujo o  
            //nome é igual ao informado no parâmetro 
            return cliente;
        }
        public Oferta ObterOferta(int _cliente)
        {

            string sql = "SELECT * from bot.ofertas WHERE id_cliente=@Cliente";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@Cliente", _cliente);

            conn.Open();

            SqlDataReader leitor = cmd.ExecuteReader();
            while (leitor.Read())
            {
                oferta.IdOferta = Convert.ToInt32(leitor["id_oferta"].ToString());
                oferta.ValorDivida = float.Parse(leitor["valor_divida"].ToString());
                oferta.ValorOferta =float.Parse(leitor["valor_oferta"].ToString());
                oferta.NumParcelas = int.Parse(leitor["num_parcelas"].ToString());
            }

            conn.Close(); 
            return oferta;
        }
    }
}