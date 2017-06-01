﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace formflow.Model
{
    public class Negociacao
    {

        public int idNegociacao { get; set; }
        public int idContato { get; set; }
        public float valorNegociacao { get; set; } 
        public DateTime inicioPagamento { get; set; }
        public int qtdParcelasNegociacao { get; set; }
        public int parcelasPagasNegociacao { get; set; }
        public DateTime diaPagamento { get; set; }


        
    }
}