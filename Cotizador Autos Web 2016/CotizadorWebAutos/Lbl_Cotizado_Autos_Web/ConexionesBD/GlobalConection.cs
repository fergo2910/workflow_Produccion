using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.ConexionesBD
{
    public static class GlobalConection
    {
        static GlobalConection()
        {
            Usuario = string.Empty;
            Clave = string.Empty;
        }

        public static string Usuario { get; private set; }
        public static string Clave { get; private set; }

        public static void SetDatosConexion(string pUsuario, string pClave)
        {
            Usuario = pUsuario;
            Clave = pClave;
        }
    }
}
