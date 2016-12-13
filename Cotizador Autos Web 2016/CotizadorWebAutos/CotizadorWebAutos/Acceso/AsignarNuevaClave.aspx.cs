using Lbl_Cotizado_Autos_Web.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CotizadorWebAutos.Acceso
{
    public partial class AsignarNuevaClave : System.Web.UI.Page
    {
        int idUsuario = 0;
        string tokenId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            idUsuario = Convert.ToInt32(Request.QueryString["userId"]);
            tokenId = Request.QueryString["tokenId"];
            if (!IsPostBack)
            {
                idUsuario = Convert.ToInt32(Request.QueryString["userId"]);
                tokenId = Request.QueryString["tokenId"];

                verificarUsuario(idUsuario, tokenId);
            }
           
        }
        private void verificarUsuario(int idUsuario, string token)
        {
            AccesoUsuario clAcceso = new AccesoUsuario();
            
            if (clAcceso.usuarioCorrecto(idUsuario, token))
            {
                txtClave.Enabled = true;
                txtRepetirClave.Enabled = true;
            }
            else
            {
                txtClave.Enabled = false;
                txtRepetirClave.Enabled = false;
            }             
        }

        protected void btnGuardarContraseña_Click(object sender, EventArgs e)
        {
            AccesoUsuario clAcceso = new AccesoUsuario();

            if (txtClave.Text == txtRepetirClave.Text)
            {
                clAcceso.actualizarPasswordUsuario(txtClave.Text, idUsuario);
                mostrarMensaje
                    ("La contaseña se cambio exitosamente. Ahora puedes ingresar al sistema.", "../Acceso/Ingreso.aspx");
            }
            else
            {
                mostrarMensaje("Las contraseñas no coinciden. Vuelva a intentar.");
            }
        }

        private void mostrarMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", "alert('" + mensaje + "');", true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        private void mostrarMensaje(string mensaje, string url)
        {
            string script = "window.onload = function(){ alert('";
            script += mensaje;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }
    }
}