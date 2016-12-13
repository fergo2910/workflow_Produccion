using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using Lbl_Cotizador_Autos_Web.Acceso;

namespace CotizadorWebAutos.Master
{
    public partial class Master_Cotizador : System.Web.UI.MasterPage
    {        
        ConsultasBD objetoConsultas = new ConsultasBD();
        public string idUsuario = string.Empty;
        IngresoSistema.informacionUsuario informacionUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();

                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];

                lblNombreUsuario.Text = informacionUsuario.codIntermediario +
                    " - " + informacionUsuario.nombreUsuario.ToString() +
                    " " + informacionUsuario.apellidoUsuario.ToString();                             
            }
            else if (Request.QueryString["userId"] == null && Request.QueryString["tokenId"] == null)
            {
                Response.Redirect("/Acceso/Ingreso.aspx");
            }            
        }
    }
}