using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace formflow.Model
{
    public class Contato
    {
        public int id_contato { get; set; }
        public float id_cliente { get; set; }
        public DateTime data_contato { get; set; }
        public String canal { get; set; }
        public String tentativa_contato { get; set; }
    }
}