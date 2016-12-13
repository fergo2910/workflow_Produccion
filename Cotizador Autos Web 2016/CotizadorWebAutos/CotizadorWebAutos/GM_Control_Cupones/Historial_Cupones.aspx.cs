using Lbl_Cotizado_Autos_Web.GM_Control_Cupones;
using Lbl_Cotizador_Autos_Web.Acceso;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CotizadorWebAutos.GM_Control_Cupones
{
    public partial class Historial_Cupones : System.Web.UI.Page
    {
        IngresoSistema.informacionUsuario informacionUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();
                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];
                ValidacionPoliza vp = new ValidacionPoliza();
                DataTable historial_asegurados = vp.obtenerHistorialMedico(int.Parse(informacionUsuario.ideProveedor));
                grvHistorialCupones.DataSource = historial_asegurados;
                grvHistorialCupones.DataBind();
            }
        }

        protected void regresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Control_Cupones.aspx");
        }
    }
}