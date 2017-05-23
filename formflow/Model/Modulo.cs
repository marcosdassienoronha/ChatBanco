using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace formflow.Model
{
     public class  Pessoa
    {
        public static int contador = 0;
        public static bool aceite;
        public Pessoa()
        {

        }

        public String Nome { get; set; }
        public String CPF { get; set; }
        public String Conta{get; set;}
        

       


    }
}