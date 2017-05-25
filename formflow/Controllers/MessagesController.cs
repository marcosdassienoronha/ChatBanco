



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



            // if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            // {
            Modulo.contador += 1; 
                if (Modulo.contador == 2 && activity.Text.Equals("2"))
                {
                    Modulo.aceite = true;
                    Modulo.contador = 1; 
                }

                else if (Modulo.contador == 5 && activity.Text.Equals("1"))
                {
                    Modulo.aceite = false;
                }
                else if (Modulo.contador == 5 && activity.Text.Equals("2"))
                {
                    Modulo.aceite = true;
                }
                await Conversation.SendAsync(activity, () => {
                    return Chain.From(() => FormDialog.FromForm(Enquiry.BuildForm));
                });
           // }
            var responseHttp = Request.CreateResponse(HttpStatusCode.OK);
            return responseHttp;
        }
    }
}



