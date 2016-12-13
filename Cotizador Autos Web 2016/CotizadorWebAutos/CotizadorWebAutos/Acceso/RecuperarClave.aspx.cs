using Lbl_Cotizado_Autos_Web.Acceso;
using Lbl_Cotizado_Autos_Web.Comunes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CotizadorWebAutos.Acceso
{
    public partial class RecuperarClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSolicitar_Click(object sender, EventArgs e)
        {
            AccesoUsuario clAcceso = new AccesoUsuario();
            DataTable datosUsuario = new DataTable();

            if (txtCorreoElectronico.Text.Contains("@"))
            {
                datosUsuario = clAcceso.buscarUsuarioXCorreo(txtCorreoElectronico.Text);
            }
            else
            {
                datosUsuario = clAcceso.buscarUsuarioXNombreUsuario(txtCorreoElectronico.Text);
            }            

            if (datosUsuario.Rows.Count == 0)
            {
                mostrarMensaje("No se encontró información para los datos ingresados.");
                return;
            }
            else if(datosUsuario.Rows.Count == 1)
            {
                Varias clVarias = new Varias();
                string nuevoPass = clVarias.crearPasswordNuevo(8);

                clAcceso.actualizarPasswordUsuario(nuevoPass, Convert.ToInt32(datosUsuario.Rows[0]["id_usuario"]));
                
                string urlRestablecerPass = System.Configuration.ConfigurationManager.AppSettings["LinkCreacionUsuario"];
                urlRestablecerPass += datosUsuario.Rows[0]["id_usuario"].ToString() + "&tokenId=" + nuevoPass;

                try
                {
                    enviarCorreoElectronico(txtCorreoElectronico.Text.Trim(), urlRestablecerPass);

                    mostrarMensaje("Revisa tu correo para restablecer la contraseña.", "../Acceso/Ingreso.aspx");
                }
                catch (Exception ex)
                {
                    mostrarMensaje("Ocurrio un error al enviar el correo. Se redireccionará a otra pagina para ingresar su nueva contraseña." + ex.Message,urlRestablecerPass);
                }  
            }            
        }
        private void enviarCorreoElectronico(string correoElectronico, string url)
        {
            string direccionSalida = ConfigurationSettings.AppSettings["direccionSalidaMensajes"];
            string passwordDireccionSalida = ConfigurationSettings.AppSettings["passwordDireccionSalidaMensajes"];
            string servidorCorreo = ConfigurationSettings.AppSettings["servidorCorreo"];

            MailMessage msg = new MailMessage();            
                msg.From = new MailAddress(correoElectronico);
                msg.To.Add(correoElectronico);
                msg.Body = "Estimado(a) Usuario," + Environment.NewLine + Environment.NewLine + Environment.NewLine
                + "Entra al siguiente link para configurar tu password" + Environment.NewLine + Environment.NewLine
                + url + Environment.NewLine + Environment.NewLine
                + "Atentamente," + Environment.NewLine + Environment.NewLine
                + "Aseguradora MAPFRE Guatemala";

                msg.IsBodyHtml = true;
                msg.Subject = "Reinicio De Contraseña";
                SmtpClient smt = new SmtpClient(servidorCorreo);
                smt.Port = 25;
                smt.Credentials = new NetworkCredential(direccionSalida, passwordDireccionSalida);
                
                smt.Send(msg); 
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