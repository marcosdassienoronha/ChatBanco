using ChatBot.Serialization;
using ChatBot.Services;
using formflow.FormFlow;
using formflow.Model;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
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
            Oferta oferta = new Oferta();


            //---------Este cliente encontra-se com todos seus dados [inclusive a oferta - cliente.oferta]
            cliente = conexao.ObterClientePorNome("Renato");
            //cliente = conexao.ObterClientePorStatus("Inadimplente");
            cliente.oferta = conexao.ObterOferta(cliente.IdCliente);
            Modulo.oferta = cliente.oferta;

            ////CUIDADO COM O DEBUG POIS PODE ALTERAR O RESULTADOS DAS VALIDAÇÕES, JÁ QUE ELE ALTERAR A ORDEM DAS PERGUNTAS
            //var reply = activity.CreateReply($""+cliente.Nome+cliente.IdCliente+" - "+cliente.oferta.IdOferta+" "+ cliente.oferta.ValorDivida);
            //await connector.Conversations.ReplyToActivityAsync(reply);
            //-----------------------------------------------------------------------------------------------------------
            //var reply = activity.CreateReply($"Contador: " + Modulo.contador);
            //await connector.Conversations.ReplyToActivityAsync(reply);

            //var reply5 = activity.CreateReply($"Activity Text: " + activity.Text);
            //await connector.Conversations.ReplyToActivityAsync(reply5);

            //var reply6 = activity.CreateReply($"CPF: " + cliente.CPF);
            //await connector.Conversations.ReplyToActivityAsync(reply6);
            
                if (Modulo.contador == 1 && activity.Text.Equals("2"))
                {
                    //var reply2 = activity.CreateReply($"NAO E A CONTA DA PESSOA | contador = " + Modulo.contador);
                    //await connector.Conversations.ReplyToActivityAsync(reply2);
                    Modulo.aceite = true;
                 
                    

                }

                if (Modulo.contador == 2)
                {

                //var reply3 = activity.CreateReply($"Contador: " + Modulo.contador + cliente.Nome+activity.Text);
                //await connector.Conversations.ReplyToActivityAsync(reply3);

                if (!activity.Text.Equals(cliente.Nome.Trim()))
                        Modulo.aceite = true;

            }
                if (Modulo.contador == 4 && activity.Text.Equals("2"))
                {
                    //var reply4 = activity.CreateReply($"Contador: " + Modulo.contador);
                    //await connector.Conversations.ReplyToActivityAsync(reply4);
                    Modulo.aceite = true;
               

                }
            if (Modulo.contador == 4 && activity.Text.Equals("1"))
            {
                //var reply23 = activity.CreateReply($"activity text = " + activity.Text);
                //await connector.Conversations.ReplyToActivityAsync(reply23);
                //var reply2 = activity.CreateReply($"cliente.cpf  = " + cliente.CPF);
                //await connector.Conversations.ReplyToActivityAsync(reply2);
                Modulo.aceite = false;
                

            }

            else if (Modulo.contador == 3 && (activity.Text.Trim() != cliente.CPF.Trim()))
                {
                    //var reply23 = activity.CreateReply($"activity text = " + activity.Text);
                    //await connector.Conversations.ReplyToActivityAsync(reply23);
                    //var reply2 = activity.CreateReply($"cliente.cpf  = " + cliente.CPF);
                    //await connector.Conversations.ReplyToActivityAsync(reply2);
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



