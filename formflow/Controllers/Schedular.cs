using formflow.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace formflow.Controllers
{
    public class Schedular
    {
        //PRECISA CORRECAO DA NEGOCIACAO PARA UTILIZAR ESTE METODO
        public static String VerificarVencimentoParcela(int _cliente)
        {
            DateTime today = DateTime.Today;

             //VERIFICA VALIDADE DA PARCELA E JA OFERECE BONIFICACAO CASO PAGUE EM DIA
                if (Modulo.negociacao.diaPagamento.AddDays(7).Day == today.Day)
                 return "Falta 1 Semana para o vencimento da parcela deste mes. Aproveite para pagar e ganhar até x% de desconto!";
            //Modulo.aceite = true/false



            return "Debug: Falta 1 Semana para o vencimento da parcela deste mes.";


        }

    }
}