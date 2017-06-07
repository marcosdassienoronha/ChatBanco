using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace formflow.Model
{
    public class Conexao
    {
        //objeto cliente que será retornado pelo método 
        private Cliente cliente;
        private Oferta oferta;
        private Negociacao negociacao;
        private Contato contato;
        SqlDataReader leitor;
        SqlCommand cmd;

       

        //instância da conexão 
        SqlConnection conn;
        //public string Nome { get; set; }
        public Conexao() {
            cliente = new Cliente();
            oferta = new Oferta();
            negociacao = new Negociacao();
            contato = new Contato();
            conn = new SqlConnection(@"Data Source=server1500fhcurso.database.windows.net;Initial Catalog=db1500fh;User ID=user1500fh;Password=15@@fh123;");
            //abro conexão 
           //conn.Open();
        }
        public void abrirConexao() {
            conn.Open();
        }
        public void fecharConexao()
        {
            conn.Close();
        }

        //método que faz a consulta no bd e obtém o cliente 
        //cujo o nome é informado pelo parâmetro 
        public Cliente ObterClientePorNome(string nome)
        {
            conn.Open();
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
        //public Cliente ObterStatusParcelaPorNome(string nome)
        //{

        //    //string com o comando a ser executado 
        //    string sql = "SELECT * from bot.cliente WHERE nome_cliente=@Nome";

        //    //instância do comando recebendo como parâmetro 
        //    cmd = new SqlCommand(sql, conn);

        //    //informo o parâmetro do comando 
        //    cmd.Parameters.AddWithValue("@Nome", nome);


        //    //instância do leitor 
        //    leitor = cmd.ExecuteReader();
        //    formatCliente(leitor);
        //    return cliente;
        //}
        public Cliente ObterClientePorStatus(string _status)
        {
            conn.Open();
            string sql = "SELECT * from bot.cliente WHERE status=@Status";
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Status", _status);
            leitor = cmd.ExecuteReader();
            formatCliente(leitor); 
            return cliente;
        }
        public ArrayList ObterOfertas(int _cliente)
        {
            SqlDataReader leitor;
            SqlCommand cmd; 
            conn.Open();
            string sql = "SELECT * from [bot].[ofertas] INNER JOIN [bot].[clienteoferta] ON [bot].[ofertas].[id_oferta] = [bot].[clienteoferta].[id_oferta]" +
                "INNER JOIN [bot].[cliente] ON [bot].[cliente].[id_cliente]=@Cliente";
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Cliente", _cliente);
            leitor = cmd.ExecuteReader();
            return formatOfertas(leitor);
        }
        public Negociacao ObterNegociacao(int _cliente)
        {
            
            SqlDataReader leitor;
            SqlCommand cmd;
            conn.Open();
            string sql = "SELECT * from [bot].[negociacao] INNER JOIN [bot].[contato] ON [bot].[negociacao].[id_contato] = [bot].[contato].[id_contato]" +
                "INNER JOIN [bot].[cliente] ON [bot].[cliente].[id_cliente]=@Cliente";
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Cliente", _cliente);
            leitor = cmd.ExecuteReader();
            formatNegociacao(leitor);
            return negociacao;
        }

        private ArrayList formatOfertas(SqlDataReader _leitor)
        {
            ArrayList ofertas = new ArrayList();
            while (_leitor.Read())
            {
                oferta = new Oferta();
                oferta.IdOferta = Convert.ToInt32(_leitor["id_oferta"].ToString());
                oferta.entrada = float.Parse(_leitor["entrada"].ToString());
                oferta.desconto = float.Parse(_leitor["desconto"].ToString());
                oferta.numParcelas = int.Parse(_leitor["num_parcelas"].ToString());
                oferta.status = _leitor["status"].ToString();
                ofertas.Add(oferta);
            }

            conn.Close();
            return ofertas;
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
                cliente.Divida = float.Parse(_leitor["divida"].ToString());

            }
            conn.Close();
            return cliente;
        }

        private Negociacao formatNegociacao(SqlDataReader _leitor)
        {
            while (_leitor.Read())
            {
                negociacao.idNegociacao = int.Parse(_leitor["id_negociacao"].ToString());
                negociacao.idContato = Convert.ToInt32(_leitor["id_contato"].ToString());
                negociacao.valorNegociacao = float.Parse(_leitor["valor_negociacao"].ToString());
                negociacao.inicioPagamento = Convert.ToDateTime(_leitor["inicio_pagamento"].ToString());
                negociacao.qtdParcelasNegociacao = int.Parse(_leitor["qtd_parcelas_negociacao"].ToString());
                negociacao.parcelasPagasNegociacao = int.Parse(_leitor["parcelas_pagas_negociacao"].ToString());
                negociacao.diaPagamento = Convert.ToInt32(_leitor["dia_pagamento"].ToString());

            }

            conn.Close();
            return negociacao;
        }
        public void updateOferta(int _id, String _status) {
            SqlCommand cmd;
            //conn.Open();
            string sql = "UPDATE [bot].[ofertas] SET status = @Status WHERE id_oferta = @Id";
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Status", _status);
            cmd.Parameters.AddWithValue("@Id", _id);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (SqlException)
            {
            }
            //conn.Close();
        }
        public String SalvarContatoRealizado(int _cliente)
        {
            DateTime today = DateTime.Now;


                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"INSERT INTO [bot].[contato](id_cliente, data_contato, canal, tentativa_contato) 
                            VALUES(@param2,@param3,@param4,@param5)";

                    
                    cmd.Parameters.AddWithValue("@param2", _cliente );
                    cmd.Parameters.AddWithValue("@param3", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    cmd.Parameters.AddWithValue("@param4", "Facebook");
                    cmd.Parameters.AddWithValue("@param5", "OK"); //ATUALIZAR ESTA VALIDACAO DE ACORDO COM O RETORNO DA TENTATIVA DE CONTATO
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (SqlException e)
                    {
                return "error" + e;
                    }

            Modulo.idContato = ObterIdContato(_cliente);


            return null; 

        }
        public int ObterIdContato(int _cliente)
        {
            conn.Open();
            string sql = "SELECT * from bot.contato WHERE id_cliente=@Cliente ";
            cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Cliente", _cliente);
            leitor = cmd.ExecuteReader();
            formatContato(leitor);
            return contato.id_contato;
        }
        public String SalvarNegociacao(int _cliente, int numeroOferta)
        {
            DateTime today = DateTime.Now;


            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"INSERT INTO [bot].[negociacao](id_contato, valor_negociacao, inicio_pagamento, qtd_parcelas_negociacao
                                        ,parcelas_pagas_negociacao, dia_pagamento) 
                            VALUES(@param2,@param3,@param4,@param5,@param6,@param7)";


            cmd.Parameters.AddWithValue("@param2", Modulo.idContato);
            if (numeroOferta == 1){
                cmd.Parameters.AddWithValue("@param3", Modulo.cliente.Divida - ((Modulo.oferta1.desconto / 100) * Modulo.cliente.Divida)); //cade o valor negociacao?
                cmd.Parameters.AddWithValue("@param4", "");
                cmd.Parameters.AddWithValue("@param5", Modulo.oferta1.numParcelas);
                cmd.Parameters.AddWithValue("@param6", "");
                cmd.Parameters.AddWithValue("@param7", Modulo.diaPagamento);
            }

            if (numeroOferta == 2) {
                cmd.Parameters.AddWithValue("@param3", Modulo.cliente.Divida - ((Modulo.oferta2.desconto / 100) * Modulo.cliente.Divida)); //cade o valor negociacao?
                cmd.Parameters.AddWithValue("@param4", "");
                cmd.Parameters.AddWithValue("@param5", Modulo.oferta2.numParcelas);
                cmd.Parameters.AddWithValue("@param6", "");
                cmd.Parameters.AddWithValue("@param7", Modulo.diaPagamento);
            }
            if (numeroOferta == 3){
                cmd.Parameters.AddWithValue("@param3", Modulo.cliente.Divida - ((Modulo.oferta3.desconto / 100) * Modulo.cliente.Divida)); //cade o valor negociacao?
                cmd.Parameters.AddWithValue("@param4", "");
                cmd.Parameters.AddWithValue("@param5", Modulo.oferta3.numParcelas);
                cmd.Parameters.AddWithValue("@param6", "");
                cmd.Parameters.AddWithValue("@param7", Modulo.diaPagamento);
            }

            
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException e)
            {
                return "error" + e;
            }

            return null;

        }
        public List<Oferta> ObterOferta(int _id)
        {
            
            conn.Open();
            //string com o comando a ser executado 
            string sql = "SELECT * from bot.ofertas WHERE id_oferta=@Id";

            //instância do comando recebendo como parâmetro 
            cmd = new SqlCommand(sql, conn);

            //informo o parâmetro do comando 
            cmd.Parameters.AddWithValue("@Id", _id);


            //instância do leitor 
            leitor = cmd.ExecuteReader();
            List<Oferta> ofertas = new List<Oferta>();
            while (leitor.Read())
            {
                oferta = formatOferta(leitor);
                ofertas.Add(oferta);
            }
            return ofertas;
        }
        private Oferta formatOferta(SqlDataReader _leitor)
        {
            
            while (_leitor.Read())
            {
                oferta.IdOferta = Convert.ToInt32(_leitor["id_oferta"].ToString());
                oferta.entrada = float.Parse(_leitor["entrada"].ToString());
                oferta.desconto = float.Parse(_leitor["desconto"].ToString());
                oferta.numParcelas = int.Parse(_leitor["num_parcelas"].ToString());
                oferta.status = _leitor["status"].ToString();
            }

            conn.Close();
            return oferta;
        }

        private Contato formatContato(SqlDataReader _leitor)
        {

            while (_leitor.Read())
            {
                contato.id_contato = Convert.ToInt32(_leitor["id_contato"].ToString());
                contato.id_cliente = Convert.ToInt32(_leitor["id_cliente"].ToString());
                contato.data_contato = DateTime.Parse(_leitor["data_contato"].ToString());
                contato.canal = _leitor["canal"].ToString();
                contato.tentativa_contato = _leitor["tentativa_contato"].ToString();
            }

            conn.Close();
            return contato;
        }
    }

     
}