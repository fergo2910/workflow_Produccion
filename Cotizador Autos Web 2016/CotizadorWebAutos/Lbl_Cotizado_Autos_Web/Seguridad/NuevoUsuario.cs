using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.Seguridad
{
    public class NuevoUsuario
    {
        public string NOMBRES { get; set; }
        public string APELLIDOS { get; set; }
        public string TELEFONO { get; set; }
        public string CORREO_ELECTRONICO { get; set; }
        public string NOMBRE_UNICO_USUARIO { get; set; }
        public string CLAVE_UNICA_USUARIO { get; set; }
        public string CODIGO_INTERMEDIARIO { get; set; }
        public string IDE_PROVEEDOR { get; set; }
        public string CODIGO_USUARIO_REMOTO { get; set; }      
        public string USUARIO_INTERNO { get; set; }
        public string DIRECCION { get; set; }
        public string NUMID { get; set; }
        public string DVID { get; set; }
        public string PASSWORD { get; set; }
        public string PASSWORD_CONFIRMADO { get; set; }
        public string ID_USUARIO_CREADOR { get; set; }
    }
}
