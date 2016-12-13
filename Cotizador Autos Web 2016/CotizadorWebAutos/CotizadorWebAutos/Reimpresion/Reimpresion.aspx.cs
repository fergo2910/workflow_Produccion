using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using Lbl_Cotizado_Autos_Web.CotizacionesMysql;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Updates;
using Lbl_Cotizado_Autos_Web.Acceso;
using System.IO;
using System.Net;
using Lbl_Cotizador_Autos_Web.Acceso;

namespace CotizadorWebAutos.Reimpresion
{
    public partial class Reimpresion : System.Web.UI.Page
    {
        IngresoSistema.informacionUsuario informacionUsuario;
        public int idUsuario = 0;
        public DataTable rolesUsuario = new DataTable();
        ConsultasBD objectoConsultas = new ConsultasBD();
        public DataTable informacionPolizaEmitida = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();

                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];

                idUsuario = informacionUsuario.idUsuario;
                AccesoUsuario clAccesoUsuario = new AccesoUsuario();
                try
                {
                    rolesUsuario = clAccesoUsuario.obtenerRolesAccesoUsuario(idUsuario);

                    if (!IsPostBack)
                    {
                        rolesUsuario = clAccesoUsuario.obtenerRolesAccesoUsuario(idUsuario);
                    }
                    lblErrores.Visible = false;
                }
                catch (Exception ex)
                {
                    lblErrores.Visible = true;
                    lblErrores.Text = "Lo sentimos, la informacion de su poliza no se encuentra disponible en este momento " + ex.ToString();
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Principal/Menu_Principal.aspx");
        }
        protected void imprimirdocumentos(Object sender, CommandEventArgs e)
        {
            string nombreArchivo = e.CommandArgument.ToString();
            if(DownloadFile(nombreArchivo))
            {
                lblErrores.Visible = false;
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                string IpReimpresionPoliza = System.Configuration.ConfigurationManager.AppSettings["IpReimpresionPoliza"];
                string ruta = IpReimpresionPoliza + nombreArchivo;
                response.ClearContent();
                response.Clear();
                response.ContentType = "text/plain";
                response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ";");
                response.TransmitFile(ruta);
                response.Flush();
                response.End();
            }
            else
                lblErrores.Visible = true;
        }
        /// <summary>
        /// Método para validar si el archivo está disponible.
        /// Si está disponible retorna True
        /// Si no está disponible retorna false y un mensaje de error
        /// </summary>
        /// <param name="nombreArchivo">Nombre del archivo buscado</param>
        /// <returns>True/False</returns>
        public bool DownloadFile(string nombreArchivo)
        {
            string IpReimpresionPoliza = System.Configuration.ConfigurationManager.AppSettings["IpReimpresionPoliza"];
            string descFilePathAndName = IpReimpresionPoliza + nombreArchivo;
            try{WebRequest myre = WebRequest.Create(descFilePathAndName);}
            catch{return false;}
            try
            {
                byte[] fileData;
                using (WebClient client = new WebClient()){fileData = client.DownloadData(descFilePathAndName);}
                return true;
            }
            catch (Exception ex)
            {
                lblErrores.Text = "Lo sentimos, la informacion de su poliza no se encuentra disponible en este momento Error al descargar: " + ex.Message;
                return false;
            }
        }

        protected void detallePoliza(Object sender, CommandEventArgs e)
        {
            string idepol = e.CommandArgument.ToString();
            DataTable obtenerDocumentosPoliza = new DataTable();
            try
            {
                obtenerDocumentosPoliza = objectoConsultas.obtenerDocumentosPoliza(idepol);
                if (obtenerDocumentosPoliza.Rows.Count <= 0)
                {
                    lblErrores.Visible = true;
                    lblErrores.Text = "Lo sentimos, la informacion de su poliza no se encuentra disponible en este momento";
                }
                else
                {
                    gvDetallePoliza.Visible = true;
                    gvDetallePoliza.DataSource = obtenerDocumentosPoliza;
                    gvDetallePoliza.DataBind();
                    lblErrores.Visible = false;
                }
                
            }
            catch(Exception ex)
            {
                lblErrores.Visible = true;
                lblErrores.Text = "Lo sentimos, la informacion de su poliza no se encuentra disponible en este momento, " + ex.ToString();
            }
        }

        protected void btnBuscarPolizaEmitida_Click(object sender, EventArgs e)
        {
            gvInformacionPoliza.Visible = false;
            gvInformacionPoliza.DataSource = null;
            gvInformacionPoliza.DataBind();
            gvDetallePoliza.Visible = false;
            gvDetallePoliza.DataSource = null;
            gvDetallePoliza.DataBind();
            DataTable obtenerIntermediario = new DataTable();
            DataTable obtenerTipoUsuario = new DataTable();
            try
            {
                obtenerIntermediario = objectoConsultas.ObtenerCodigoIntermediario(idUsuario.ToString());
                informacionPolizaEmitida = objectoConsultas.ConsultarPolizasEmitidas(txtCodPol.Text.ToUpper(), txtNumpol.Text, obtenerIntermediario.Rows[0]["codigo_intermediario"].ToString(), obtenerIntermediario.Rows[0]["usuario_interno"].ToString());
                if (informacionPolizaEmitida.Rows.Count <= 0)
                {
                    lblErrores.Visible = true;
                    lblErrores.Text = "Lo sentimos, la poliza que busca no existe.";
                }
                else
                {
                    lblErrores.Visible = false;
                    gvInformacionPoliza.Visible = true;
                    gvInformacionPoliza.DataSource = informacionPolizaEmitida;
                    gvInformacionPoliza.DataBind();
                }
            }
            catch(Exception ex)
            {
                lblErrores.Visible = true;
                lblErrores.Text = "Lo sentimos, la informacion de su poliza no se encuentra disponible en este momento " + ex.ToString();
            }
        }

        protected void gvInformacionPoliza_RowDataBound(object sender, GridViewRowEventArgs e)
        {
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[7].Visible = false;
        }

        protected void gvDetallePoliza_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
        }
    }
}