using formflow.Model;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.FormFlow.Advanced;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using static formflow.Model.Conexao;

namespace formflow.FormFlow
{
     
    public enum YesOrNoOptionsConta { Sim = 1, Nao };
    public enum YesOrNoOptionsNegociacao { Sim = 1, Nao };
    




    [Serializable]
    public class Enquiry
    {

        //----------------//----------------//
        //Variaveis da negociacao
        public int idNegociacao = Modulo.negociacao.idNegociacao;
        public int idContato = Modulo.negociacao.idContato;
        public float valorNegociacao = Modulo.negociacao.valorNegociacao;
        public int qtdParcelasNegociacao = Modulo.negociacao.qtdParcelasNegociacao;
        public int parcelasPagasNegociacao = Modulo.negociacao.parcelasPagasNegociacao;
        public DateTime diaPagamento = Modulo.negociacao.diaPagamento;

        //----------------//----------------//


        //----------------//----------------//
        //Variaveis de oferta
        public float ValorDivida = Modulo.oferta.ValorDivida;
        public float ValorOferta = Modulo.oferta.ValorOferta;
        public int NumParcelas = Modulo.oferta.NumParcelas;
        //----------------//----------------//



        [Prompt("Notas que você tem uma parcela prestes a vencer. Aproveite para pagar e ganhar até x% de desconto! ")]
        public YesOrNoOptionsConta ParcelaVencendo { get; set; }


        [Prompt("Olá, sou um representante do Banco 1500, notamos que há algumas contas suas pendentes e gostariamos de negocia-las. A conta número 23323-90 é " +
            "pertencente a você? {||}")]
            public  YesOrNoOptionsConta Conta { get; set; }


            [Prompt("Como você se chama? ")]
            public string Nome { get; set; }


            [Prompt("Certo. {Nome}, por favor, forneça seu Cpf? ")]
            public string Cpf { get; set; }


        //----------------//----------------//
        //Confirmar se aceita a negociação final
        //
        [Prompt("Como havia dito, constatamos no sistema uma divida no valor de {ValorDivida} a qual podemos fazer por {ValorOferta} em {NumParcelas} vezes. Voce está de acordo com o combinado na negociação? {||}")]
            public YesOrNoOptionsNegociacao AcordoNegociacao { get; set; }
        //----------------------------------//


        //----------------//----------------//
        //Mostrar resumo final da negociação
        [Prompt("Resumo Final da Negociação: \r\rValor Divida: {ValorDivida}  \n\n\n\rValor Acordado/Negociado: {valorNegociacao}  \n\n\n\rQuantidade de Parcelas Ofertadas: {NumParcelas}" +
            "\n\n\n\rQuantidade de Parcela(s) Acordada(s)/Negociada(s): {qtdParcelasNegociacao}  \n\n\n\rDia para Pagamento(s): {diaPagamento}")]
        public YesOrNoOptionsNegociacao ResumoFinalNegociacao { get; set; }
        //----------------------------------//

 
 
       

        public static IForm<Enquiry> BuildForm()
        {
           

            var builder = new FormBuilder<Enquiry>();
            builder.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Auto;
            builder.Configuration.Yes = new string[] { "sim" };
            builder.Configuration.No = new string[] { "nao" };
            // Builds an IForm<T> based on BasicForm

            return builder
                //.Field("ParcelaVencendo", state => !Modulo.aceite)
                .Field("Conta", state => !Modulo.aceite)
                .Field("Nome", state => !Modulo.aceite)
                .Field("Cpf", state => !Modulo.aceite)
                .Field("AcordoNegociacao", state => !Modulo.aceite)
                .Message("Obrigado, desconsidere o contato.", state => Modulo.aceite)
                .Message("Obrigado, negociação concluida com sucesso!", state => !Modulo.aceite)
                .Field("ResumoFinalNegociacao", state => !Modulo.aceite)
                .AddRemainingFields(null)
                .Build();

             
        }

        public static IFormDialog<Enquiry> BuildFormDialog(FormOptions options = FormOptions.PromptInStart)
        {
            // Generated a new FormDialog<T> based on IForm<BasicForm>
            return FormDialog.FromForm(BuildForm, options);
        }


    }
}