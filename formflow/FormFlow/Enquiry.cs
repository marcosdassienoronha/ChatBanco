using Microsoft.Bot.Builder.FormFlow;
using System;




namespace formflow.FormFlow
{
    public enum YesOrNoOptions { Sim = 1, Nao };

    [Serializable]
    public class Enquiry
    {


            //[Prompt("Qual é o seu nome?")]
            // public string Name { get; set; }
            [Prompt("Olá, sou um representante do Banco 1500, notamos que há algumas contas suas pendentes e gostariamos de negocia-las. A conta número 23323-90 é " +
            "pertencente a você? {||}")]
            public YesOrNoOptions Conta { get; set; }
            [Prompt("Como você se chama? {||}")]
            public bool Nome { get; set; }
        [Prompt("Voce está de acordo com o combinado na negociação? {||}")]
            public bool AcordoNegociacao { get; set; }
            



        public Service ServiceRequired { get; set; }

        public enum Service
        {
            Consultancy, Support, ProjectDelivery, Unknown
        }
        public static IForm<Enquiry> BuildForm()
        {
            var builder = new FormBuilder<Enquiry>();
            builder.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Auto;
            builder.Configuration.Yes = new string[] { "sim" };
            builder.Configuration.No = new string[] { "nao" };
            // Builds an IForm<T> based on BasicForm
            return builder.Build();
        }

        public static IFormDialog<Enquiry> BuildFormDialog(FormOptions options = FormOptions.PromptInStart)
        {
            // Generated a new FormDialog<T> based on IForm<BasicForm>
            return FormDialog.FromForm(BuildForm, options);
        }


        //  public static IForm<Enquiry> BuildEnquiryForm()                                 //Cria um formulario adicionando alguns campos, retorna um IForm
        //  {
        //      return new FormBuilder<Enquiry>()                                           //aqui se define a ordem da apresentação das mensagens (Bot-to-User)
        //          .Field("Fala1")
        //          .Field("Name")
        //          .Build();
        //  }


    }
}