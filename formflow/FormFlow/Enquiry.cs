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

        //----------------//----------------//
        //Variaveis da negociacao
        public int idNegociacao = Modulo.negociacao.idNegociacao;
        public int idContato = Modulo.negociacao.idContato;
        public float valorNegociacao = Modulo.negociacao.valorNegociacao;
        public int qtdParcelasNegociacao = Modulo.negociacao.qtdParcelasNegociacao;
        public int parcelasPagasNegociacao = Modulo.negociacao.parcelasPagasNegociacao;
        
        //----------------//----------------//


        //Variaveis da Conta
        public string ContaCliente = Modulo.cliente.Conta;
        public string AgenciaCliente = Modulo.cliente.Agencia;

        //----------------//----------------//
        //Variaveis de oferta
        public float ValorDivida = Modulo.cliente.Divida;
        

        public float ValorNovo1 = Modulo.cliente.Divida - ((Modulo.oferta1.desconto / 100) * Modulo.cliente.Divida);
        public float ValorDesconto1 = Modulo.oferta1.desconto;
        public int NumParcelas1 = Modulo.oferta1.numParcelas;
        public float Entrada1 = Modulo.oferta1.entrada;

        public float ValorNovo2 = Modulo.cliente.Divida - ((Modulo.oferta2.desconto / 100) * Modulo.cliente.Divida);
        public float ValorDesconto2 = Modulo.oferta2.desconto;
        public int NumParcelas2 = Modulo.oferta2.numParcelas;
        public float Entrada2 = Modulo.oferta2.entrada;

        public float ValorNovo3 = Modulo.cliente.Divida - ((Modulo.oferta3.desconto / 100) * Modulo.cliente.Divida);
        public float ValorDesconto3 = Modulo.oferta3.desconto;
        public int NumParcelas3 = Modulo.oferta3.numParcelas;
        public float Entrada3 = Modulo.oferta3.entrada;
        //----------------//----------------//



        [Prompt("Notas que você tem uma parcela prestes a vencer. Aproveite para pagar e ganhar até x% de desconto! ")]
        public YesOrNoOptionsConta ParcelaVencendo { get; set; }


        [Prompt("Olá, sou um representante do Banco 1500, notamos que há algumas contas suas pendentes e gostariamos de negocia-las. A conta número {ContaCliente} da agência {AgenciaCliente} é " +
            "pertencente a você? {||}")]
        public YesOrNoOptionsConta Conta { get; set; }


        [Prompt("Como você se chama? ")]
        public string Nome { get; set; }


        [Prompt("Certo. {Nome}, por favor, forneça seu Cpf? ")]
        public string Cpf { get; set; }


        //----------------//----------------//
        //Confirmar se aceita a negociação final
        //
        [Prompt("Como havia dito, constatamos no sistema uma divida no valor de R${ValorDivida} a qual podemos dar um desconto de {ValorDesconto1}%, com isso o valor cai para R${ValorNovo1}. Tudo isso podemos dividir em {NumParcelas1} vezes. Voce está de acordo com o combinado na negociação? {||}")]
        public YesOrNoOptionsNegociacao AcordoNegociacao { get; set; }
        //----------------------------------//

        //----------------//----------------//
        //Confirmar se aceita a negociação final
        //
        [Prompt("Tudo bem {Nome}, estamos aqui para te ajudar a renegociar sua dívida, e se dermos um desconto de {ValorDesconto2}%, deixando o valor da dívida em R${ValorNovo2}. Dividindo em {NumParcelas2} vezes, ficaria melhor pra você? {||}")]
        public YesOrNoOptionsNegociacao AcordoNegociacao2 { get; set; }
        //----------------------------------//

        //----------------//----------------//
        //Confirmar se aceita a negociação final
        //
        [Prompt("Como eu disse, nosso desejo é te ajudar, com isso estamos te dando um desconto no valor da dívida em {ValorDesconto3}%, passando agora para R${ValorNovo3} em {NumParcelas3} vezes. Aproveite este desconto, é o máximo que posso fazer por você. Você concorda? {||}")]
        public YesOrNoOptionsNegociacao AcordoNegociacao3 { get; set; }
        //----------------------------------//

        //----------------//----------------//
        //Dia para pagamento
        //
        [Prompt("Qual  é o melhor dia, a contar a partir deste mes, para realizar os pagamentos?")]
        public String diaPagamento { get; set; }
        //----------------------------------//

        //----------------//----------------//
        //Mostrar resumo final da negociação
        [Prompt("Resumo Final da Negociação: \r\rValor Divida: {ValorDivida}  \n\n\n\rValor Acordado/Negociado: {valorNegociacao}  " +
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
                .Field("AcordoNegociacao", state => !Modulo.aceite1)
                .Field("AcordoNegociacao2", state => Modulo.aceite2)
                .Field("AcordoNegociacao3", state => Modulo.aceite3)
                .Field("diaPagamento", state => Modulo.diaPagamentoAceite)
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