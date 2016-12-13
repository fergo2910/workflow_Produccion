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
using Lbl_Cotizado_Autos_Web.ProcesoEmision;
using Lbl_Cotizado_Autos_Web.Comunes;
using System.Threading;
using Lbl_Cotizador_Autos_Web.Acceso;

namespace CotizadorWebAutos.Productos_Web
{
    public partial class Autorizacion : System.Web.UI.Page
    {        
        int idPlan = 0;
        int cotId = 0;
        ConsultasBD objetoConsultas = new ConsultasBD();
        InsertsBD objetoInserts = new InsertsBD();
        UpdatesBD objetoUpdates = new UpdatesBD();
        IngresoSistema.informacionUsuario informacionUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();
                DataTable comentarioInspeccion = new DataTable();

                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];
                cotId = (int)Session["cotId"];
                idPlan = (int)Session["idPlan"];

                comentarioInspeccion = objetoConsultas.ObtenerComentarioInspeccion(cotId.ToString());
                txtComentariosInspeccion.Text = comentarioInspeccion.Rows[0]["comentarios_inspeccion"].ToString();
                txtCotizacion.Text = cotId.ToString();
                txtNumeroInspeccion.Text = comentarioInspeccion.Rows[0]["numero_inspeccion"].ToString();
                Regresar.Attributes.Add("onclick", "history.back(); return false;");  
            }            
        }
        protected void btnGuardarAutorizacion_Click(object sender, EventArgs e)
        {
            bool resultadoUpdate = false;
            if ((txtComentarioAutorizacion.Text != "") && (ddlAutorizacionMAPFRE.SelectedValue != "0"))
            {
                resultadoUpdate = objetoUpdates.ActualizarAutorizacionCotizacion(cotId.ToString(), ddlAutorizacionMAPFRE.SelectedValue, txtComentarioAutorizacion.Text.ToUpper());
                if (resultadoUpdate)
                {
                    if (ddlAutorizacionMAPFRE.SelectedValue == "AUT")
                    {
                        btnEmision.Visible = false;
                        MensajeError("PROCEDA A EMITIR LA POLIZA");

                        Response.Redirect("../Principal/MisCotizaciones.aspx?");
                    }

                    if (ddlAutorizacionMAPFRE.SelectedValue == "DEN")
                    {
                        Response.Redirect("../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan);                        
                    }
                }
                else
                {
                    MensajeError("OCURRIO UN ERROR AL CAMBIAR EL ESTADO DE LA COTIZACION");
                }
            }
            else
            {
                MensajeError("OCURRIO UN ERROR, CAMPOS VACIOS");
            }
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
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Varias clVarias = new Varias();
            //int idEstadoCotizacion = clVarias.obtenerIdEstadoCotizacion("PRE-EMI");

            //clVarias.actualizarEstadoCotizacion(cotId, idEstadoCotizacion);

            Response.Redirect("../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan);
        }
        protected void btnEmision_Click(object sender, EventArgs e)
        {
            EmitirPoliza clEmision = new EmitirPoliza();
            Varias clVarias = new Varias();
            DataTable datosUsuarioLogueado = new DataTable();
            datosUsuarioLogueado = clVarias.obtenerInformacionUsuarioLogueado(informacionUsuario.idUsuario);
            
            string respuesta = string.Empty;

            //respuesta = clEmision.Emitir(cotId.ToString(), datosUsuarioLogueado, int.Parse(idPlan), int.Parse(idUsuario));

            if (!respuesta.Contains("ORA"))
            {
                mensajeUrl(respuesta, "../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan);
            }
            else {
                lblRespuesta.Text = respuesta;
                lblRespuesta.Visible = true;
            }
        }
        private void mensajeUrl(string mensaje, string url)
        {
            string script = "window.onload = function(){ alert('";
            script += mensaje;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Redirect", script, true);
        }
    }
}