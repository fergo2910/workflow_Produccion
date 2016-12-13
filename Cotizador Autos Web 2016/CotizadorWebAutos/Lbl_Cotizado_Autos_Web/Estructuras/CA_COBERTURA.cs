using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.Estructuras
{
    public class CA_COBERTURA
    {
        public List<string> COBERTURAS { get; set; }
        public string RAMO { get; set; }
        public string CODIGO { get; set; }
        public string SUMAASEGURADA { get; set; }
        public string PRIMA { get; set; }
        public string TASA { get; set; }
        public string DESCRIPCION_COBERTURA { get; set; }
        public string IDEPROCESO { get; set; }
        public string LLAVE { get; set; }
        public string LLAVE_ALTERNA { get; set; }
        public string LINEA { get; set; }
    }
}
