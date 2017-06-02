using ChatBot.Serialization;
using ChatBot.Services;
using formflow.Controllers;
using formflow.FormFlow;
using formflow.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;



namespace formflow
{
    //[BotAuthentication]
    public class MessagesController : ApiController
    {

        [ResponseType(typeof(void))]

        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            string sim = string.Empty;
            string nao = string.Empty;
            var intent = new Intent();
            var entity = new ChatBot.Serialization.Entity();
            var response = await Luis.GetResponse(activity.Text);

            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            Cliente cliente = new Cliente();
            Conexao conexao = new Conexao();
            Negociacao negociacao = new Negociacao();
            Oferta oferta = new Oferta();
            ArrayList ofertas = new ArrayList();

            //----------------//----------------//
            //Este cliente encontra-se com todos seus dados [inclusive a oferta - cliente.oferta]
            cliente = conexao.ObterClientePorNome("Renato"); // busca um cliente
            cliente.countOfertas(); // busca e retorna as ofertas a ofertar
           // cliente.validarOferta("offered"); // atribui o novo status da oferta e atualiza a proxima oferta
            
            cliente.validarOferta("offer");//validar oferta no banco, offered = ofertado **** ofeerm = a ofertar  
            //----------------//----------------//


            //----------------//----------------//
            //Atribuindo o objeto atual a estes objeto da classe Modulo,
            //para poder utiliza-los na classe "Enquiry", mais especificamente, nos "Propts".
            Modulo.oferta = cliente.oferta;
            Modulo.negociacao = conexao.ObterNegociacao(cliente.IdCliente);
            //----------------//----------------//


            //----------------//----------------//
            //CUIDADO COM O DEBUG POIS PODE ALTERAR O RESULTADOS DAS VALIDAÇÕES, JÁ QUE ELE ALTERAR A ORDEM DAS PERGUNTAS
            //var reply = activity.CreateReply($""+cliente.Nome+cliente.IdCliente+" - "+cliente.oferta.IdOferta+" "+ cliente.oferta.ValorDivida);
            //await connector.Conversations.ReplyToActivityAsync(reply);
            //----------------//----------------//


                //----------------//----------------//
                //Armazena as informações desta tentativa/efetivacao de contato com o cliente
                if (Modulo.contador == 0) 
                {
                             String retorno = conexao.SalvarContatoRealizado(cliente.IdCliente);
                            String pagamento = Schedular.VerificarVencimentoParcela(cliente.IdCliente);

               // var reply = activity.CreateReply($"" + pagamento);
               // await connector.Conversations.ReplyToActivityAsync(reply);
            }
            //----------------//----------------//
            if (Modulo.contador == 1 && activity.Text.Equals("2"))
                {    
                        Modulo.aceite = true;
                        
                }

                if (Modulo.contador == 2)
                {
                        if (!activity.Text.Equals(cliente.Nome.Trim()))
                             Modulo.aceite = true;
                }
                if (Modulo.contador == 4 && activity.Text.Equals("2"))
                {
                        Modulo.aceite = true;
                }
                if (Modulo.contador == 4 && activity.Text.Equals("1"))
                {
                        Modulo.aceite = false;
                }
                if (Modulo.contador == 3 && (activity.Text.Trim() != cliente.CPF.Trim()))
                {
                        Modulo.aceite = true;
                }

                   Modulo.contador += 1;
            
            await Conversation.SendAsync(activity, () =>
            {
                return Chain.From(() => FormDialog.FromForm(Enquiry.BuildForm));
            });

                var responseHttp = Request.CreateResponse(HttpStatusCode.OK);
         
                    return responseHttp;

        }
    }
}



