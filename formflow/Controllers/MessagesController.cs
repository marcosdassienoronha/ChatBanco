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
using Microsoft.Bot.Connector;
using System.Linq;
using System.Threading;

namespace formflow
{
    [BotAuthentication]
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
            Modulo.cliente = cliente;
            Modulo.oferta1 = (Oferta)cliente.ofertas[0];
            Modulo.oferta2 = (Oferta)cliente.ofertas[1];
            Modulo.oferta3 = (Oferta)cliente.ofertas[2];
            Modulo.negociacao = conexao.ObterNegociacao(cliente.IdCliente);
            //----------------//----------------//

          
               
                 
                   

                    
               
                
            

            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            {
                string msg = activity.Text.ToLower().Trim();
                if (msg == "start over" || msg == "exit" || msg.Equals("quit") || msg == "done" || msg == "start again" || msg == "restart" || msg == "leave" || msg == "reset")
                {
                    //This is where the conversation gets reset!
                    activity.GetStateClient().BotState.DeleteStateForUser(activity.ChannelId, activity.From.Id);
                }
                //----------------//----------------//
                //CUIDADO COM O DEBUG POIS PODE ALTERAR O RESULTADOS DAS VALIDAÇÕES, JÁ QUE ELE ALTERAR A ORDEM DAS PERGUNTAS
                //var reply = activity.CreateReply($""+cliente.Nome+cliente.IdCliente+" - "+cliente.oferta.IdOferta+" "+ cliente.oferta.ValorDivida);
                //await connector.Conversations.ReplyToActivityAsync(reply);
                //----------------//----------------//


                //----------------//----------------//
                //Armazena as informações desta tentativa/efetivacao de contato com o cliente
                //if (Modulo.contador == 0) 
                //{

                // String pagamento = Schedular.VerificarVencimentoParcela(cliente.IdCliente);

                // var reply = activity.CreateReply($"" + pagamento);
                // await connector.Conversations.ReplyToActivityAsync(reply);
                //}
                //----------------//----------------//
                //Valida interesse na negociação------------------------------------//
                if (Modulo.contador == 1 && activity.Text.Equals("2") || Modulo.contador == 1 && activity.Text.Equals("Nao"))
            {
                Modulo.aceite = true;

            }


            //Valida o Nome------------------------------------//
            if (Modulo.contador == 2)
            {
                if (!activity.Text.Equals(cliente.Nome.Trim()))
                {
                    Modulo.aceite = true;
                    Modulo.aceite1 = true;
                    Modulo.aceite2 = false;
                    Modulo.aceite3 = false;

                }
            }

            //Valida o CPF------------------------------------//
            if (Modulo.contador == 3 && (activity.Text.Trim() != cliente.CPF.Trim()))
            {
                Modulo.aceite = true;
                Modulo.aceite = true;
                Modulo.aceite1 = true;
                Modulo.aceite2 = false;
                Modulo.aceite3 = false;
            }

            //Primeira proposta------------------------------------//

            if (Modulo.contador == 4 && activity.Text.Equals("2") || Modulo.contador == 4 && activity.Text.Equals("Nao"))
            {
                Modulo.aceite1 = false;
                Modulo.aceite2 = true;
                Modulo.aceite3 = false;


            }


            if (Modulo.contador == 4 && activity.Text.Equals("1") || Modulo.contador == 4 && activity.Text.Equals("Sim"))
            {
                Modulo.numeroNegociacao = 1;
                Modulo.aceite = false;
                Modulo.diaPagamentoAceite = true;
            }
            //-----------------------------------------------------//


            //Segunda proposta------------------------------------//
            if (Modulo.contador == 5 && activity.Text.Equals("2") || Modulo.contador == 5 && activity.Text.Equals("Nao"))
            {

                Modulo.aceite1 = false;
                Modulo.aceite2 = false;
                Modulo.aceite3 = true;
            }

            if (Modulo.contador == 5 && activity.Text.Equals("1") || Modulo.contador == 5 && activity.Text.Equals("Sim"))
            {
                Modulo.numeroNegociacao = 2;
                Modulo.aceite = false;
                Modulo.diaPagamentoAceite = true;
            }
            //-----------------------------------------------------//


            //Terceira proposta------------------------------------//
            if (Modulo.contador == 6 && activity.Text.Equals("2") || Modulo.contador == 6 && activity.Text.Equals("Nao"))
            {
                Modulo.aceite3 = true;
            }

            if (Modulo.contador == 6 && activity.Text.Equals("1") || Modulo.contador == 6 && activity.Text.Equals("Sim"))
            {
                Modulo.numeroNegociacao = 3;
                Modulo.aceite = false;
                Modulo.diaPagamentoAceite = true;
            }
            //-----------------------------------------------------//


            //Dia Para Pagamento------------------------------------//
            if (Modulo.contador == 5 || Modulo.contador == 6 && !activity.Text.Equals("1") || Modulo.contador == 7 && !activity.Text.Equals("1") || Modulo.contador == 6 && !activity.Text.Equals("Nao") || Modulo.contador == 7 && !activity.Text.Equals("Nao"))
            {
                Modulo.diaPagamento = int.Parse(response.entities[0].entity.ToString());
                //Modulo.negociacao = conexao.ObterNegociacao(cliente.IdCliente);
                conexao.SalvarContatoRealizado(cliente.IdCliente);
                conexao.SalvarNegociacao(cliente.IdCliente, Modulo.numeroNegociacao);
                    

                }


            //-----------------------------------------------------//

            Modulo.contador += 1;

            await Conversation.SendAsync(activity, () =>
            {
                return Chain.From(() => FormDialog.FromForm(Enquiry.BuildForm));
            });
                 
        }
                var responseHttp = Request.CreateResponse(HttpStatusCode.OK);
         
                    return responseHttp;

        }
    }
}



