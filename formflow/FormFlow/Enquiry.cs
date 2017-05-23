using formflow.Model;
using Microsoft.Bot.Builder.FormFlow;
using System;




namespace formflow.FormFlow
{
     
    public enum YesOrNoOptionsConta { Sim = 1, Nao };
    public enum YesOrNoOptionsNegociacao { Sim = 1, Nao };
    

    [Serializable]
    public class Enquiry
    {
        

            [Prompt("Olá, sou um representante do Banco 1500, notamos que há algumas contas suas pendentes e gostariamos de negocia-las. A conta número 23323-90 é " +
            "pertencente a você? {||}")]
            public  YesOrNoOptionsConta Conta { get; set; }
            [Prompt("Como você se chama? ")]
            public string Nome { get; set; }
            [Prompt("Certo. {Nome} ,por favor, forneça seu Cpf? ")]
            public string Cpf { get; set; }
            [Prompt("Voce está de acordo com o combinado na negociação? {||}")]
            public YesOrNoOptionsNegociacao AcordoNegociacao { get; set; }
            


        public static IForm<Enquiry> BuildForm()
        {
            
            var builder = new FormBuilder<Enquiry>();
            builder.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Auto;
            builder.Configuration.Yes = new string[] { "sim" };
            builder.Configuration.No = new string[] { "nao" };
            // Builds an IForm<T> based on BasicForm

            return builder
                .Field("Conta", state => !Pessoa.aceite)
                .Message("Obrigado, desconsidere o contato.", state => Pessoa.aceite)
                .Field("Nome", state => !Pessoa.aceite)
                .Field("Cpf", state => !Pessoa.aceite)
                .Field("AcordoNegociacao", state => !Pessoa.aceite)
                .AddRemainingFields()
                .Confirm("Resumo da negociação: Nome: {Nome} \n CPF: {Cpf} \n Aceite da Negociação: {AcordoNegociacao}", state => !Pessoa.aceite)
                .Build();

        }

        public static IFormDialog<Enquiry> BuildFormDialog(FormOptions options = FormOptions.PromptInStart)
        {
            // Generated a new FormDialog<T> based on IForm<BasicForm>
            return FormDialog.FromForm(BuildForm, options);
        }


    }
}