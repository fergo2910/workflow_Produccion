using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.Estructuras
{
    public class CA_REGARGO
    {
        public List<string> RECARGOS { get; set; }
        public string CODIGO { get; set; }
        public string TIPO { get; set; }
        public string PORCENTAJE { get; set; }
        public string MONTO { get; set; }
        public string DESCRIPCION_RECARGO { get; set; }
        public string IDEPROCESO { get; set; }
        public string LLAVE { get; set; }
        public string LLAVE_ALTERNA { get; set; }
        public string LINEA { get; set; }
    }
}
