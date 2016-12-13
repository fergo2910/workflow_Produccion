using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Inserts;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Updates;
using Lbl_Cotizado_Autos_Web.Estructuras;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace CotizadorWebAutos.Pagos
{
    public partial class Pagos : System.Web.UI.Page
    {
        public string idUsuario = string.Empty;
        public string idPlan = string.Empty;
        public string cotId = string.Empty;
        ConsultasBD objetoConsultas = new ConsultasBD();
        InsertsBD objetoInserts = new InsertsBD();
        UpdatesBD objetoUpdates = new UpdatesBD();
        protected void Page_Load(object sender, EventArgs e)
        {
            idUsuario = Request.QueryString["userId"];
            cotId = Request.QueryString["cotId"];
            DataTable comentarioInspeccion = new DataTable();

            if (!IsPostBack)
            {
                idUsuario = Request.QueryString["userId"];
                cotId = Request.QueryString["cotId"];
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Principal/Menu_Principal.aspx?userId=" + idUsuario);
        }
        public void MensajeError(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        private void mensaje(string mensaje, string url)
        {
            string script = "window.onload = function(){ alert('";
            script += mensaje;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }

        protected void btnBuscarPoliza_Click(object sender, EventArgs e)
        {

        }
    }
}