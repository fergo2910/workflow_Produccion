using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Inserts;
using Lbl_Cotizado_Autos_Web.Estructuras;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using Lbl_Cotizador_Autos_Web.Acceso;

namespace CotizadorWebAutos.Productos_Web
{
    public partial class Inspeccion : System.Web.UI.Page
    {
        ConsultasBD objetoConsultas = new ConsultasBD();
        InsertsBD objetoInserts = new InsertsBD();
        DataTable informacionVehiculo = new DataTable();
        IngresoSistema.informacionUsuario informacionUsuario;
        int idCotizacion = 0;
        int idPlanSeleccionado = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();

                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];
                idCotizacion = (int)Session["cotId"];
                idPlanSeleccionado = (int)Session["idPlan"];

                if (!IsPostBack)
                {
                    informacionVehiculo = objetoConsultas.ObtenerDatosVehiculoCotizado(idCotizacion);

                    txtTipoVehiculo.Text = informacionVehiculo.Rows[0]["DESCRIP"].ToString();
                    txtMarcaVehiculo.Text = informacionVehiculo.Rows[0]["DESCMARCA"].ToString();
                    txtLineaVehiculo.Text = informacionVehiculo.Rows[0]["DESCMODELO"].ToString();
                    txtAnioVehiculo.Text = informacionVehiculo.Rows[0]["ANIO"].ToString();
                }
            }
            
        }
        protected void btnGuardarDatosVehiculo_Click1(object sender, EventArgs e)
        {
            if ((txtTipoVehiculo.Text != "") && (txtMarcaVehiculo.Text != "") && (txtLineaVehiculo.Text != "") && (txtAnioVehiculo.Text != "")  &&
                (txtNumeroChasis.Text != "") && (txtCilindraje.Text != "") && (txtColor.Text != "") &&
                (txtNumeroMotor.Text != "") && (txtNumeroInspeccion.Text != "") && (txtComentariosInspeccion.Text != ""))
            {
                bool resultadoInsertVehiculo = false;
                int valorTonelaje = 0;

                if (txtTonelaje.Text == "")
                {
                    valorTonelaje = 0;
                }

                if (txtTonelaje.Text == "")
                {
                    valorTonelaje = 0;
                }

                resultadoInsertVehiculo = objetoInserts.InsertarVehiculoEInspeccion(idCotizacion.ToString(), txtTipoVehiculo.Text,
                                          txtMarcaVehiculo.Text, txtLineaVehiculo.Text, Convert.ToInt32(txtAnioVehiculo.Text), txtNumeroTarjetaCirculacion.Text,
                                          txtNumeroChasis.Text.ToUpper(), ddlTipoPlaca.SelectedValue.ToString().ToUpper(), txtNumeroPlaca.Text.ToUpper(), txtCorrelativoPlaca.Text,
                                          txtCilindraje.Text, valorTonelaje, txtColor.Text, txtNumeroMotor.Text.ToUpper(),
                                          txtNumeroInspeccion.Text, txtComentariosInspeccion.Text.ToUpper());

                if (resultadoInsertVehiculo)
                {
                    //envioCorreoAMapfre(Convert.ToInt32(txtNumeroInspeccion.Text), Convert.ToInt32(cotId));
                    //ClientScript.RegisterStartupScript(this.GetType(), "mensaje", "alert('" + "DATOS GUARDADOS" + "');", true);
                    mensaje("DATOS GUARDADOS", "../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlanSeleccionado);
                }
                else
                {
                    MensajeError("OCURRIO UN ERROR");
                }
            }
            else
            {
                MensajeError("OCURRIO UN ERROR, CAMPOS VACIOS");
            }
        }
        private void envioCorreoAMapfre(int numeroInspeccion, int numeroCotizacion)
        {
            DataTable correos = new DataTable();
            DataTable correoUsuarioLogeado = new DataTable();
            ConsultasBD objetoConsultas = new ConsultasBD();

            correos = objetoConsultas.obtenerCorreosPorRol("AUTORIZADOR");
            correoUsuarioLogeado = objetoConsultas.obtenerCorreoUsuario(numeroCotizacion);

            if (correos.Rows.Count > 0)
            {
                for (int i = 0; i < correos.Rows.Count; i++)
                {
                    //SE COMENTO A SOLICITUD DE JUAN CARLOS ----- ENVIO DE MENSAJES A AUTORIZADORES
                    // VICTORIA GUTIERREZ 11/08/2016
                 /*   string correo = string.Empty;

                    correo = correos.Rows[i]["correo_electronico"].ToString();

                    MailMessage msg = new MailMessage();
                    try
                    {
                        msg.From = new MailAddress(correoUsuarioLogeado.Rows[0]["correo_electronico"].ToString());
                        msg.To.Add(correo);
                        msg.Body = "Estimado(a) Usuario, " + "\n" + "\n"
                        + "Se ha realizado la carga de una inspección en el sistema. " + "\n" + "\n"
                        + "Los datos son los siguientes: " + "\n"
                        + "Número de cotización: " + numeroCotizacion + "\n"
                        + "Número de inspección: " + numeroInspeccion;

                        msg.Subject = "Carga Inspección";
                        SmtpClient smt = new SmtpClient(ConfigurationManager.AppSettings["servidorCorreo"]);
                        smt.Port = 25;
                        smt.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["direccionSalidaMensajes"], ConfigurationManager.AppSettings["passwordDireccionSalidaMensajes"]);

                        smt.Send(msg);
                    }
                    catch (Exception ex)
                    {
                        string script = "<script>alert('" + ex.Message + "')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "Error al enviar correo de autorización de inspección.", script);
                    }*/
                }
            }

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlanSeleccionado);
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
    }
}