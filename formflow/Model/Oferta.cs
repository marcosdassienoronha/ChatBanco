﻿using System;

namespace formflow.Model
{
    public class Oferta
    {
        public int IdOferta { get; set; }
        public float entrada { get; set; }
        public float desconto { get; set; }
        public int numParcelas { get; set; }
        public String status { get; set; }

    }
}