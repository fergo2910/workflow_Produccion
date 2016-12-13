using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Lbl_Cotizado_Autos_Web.Oficina_Virtual;
using Lbl_Cotizador_Autos_Web.Acceso;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CotizadorWebAutos.Oficina_Virtual
{
    public partial class Consulta_Primas_Pendientes : System.Web.UI.Page
    {

        IngresoSistema.informacionUsuario informacionUsuario;
        Validacion_Primas objetoPrimasPendientes = new Validacion_Primas();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();
                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];
                if (informacionUsuario.accionesPermitidas.Contains("Descargar_Primas_Pendientes"))
                {
                    btnBuscar.Enabled = true;
                    if (!IsPostBack)
                    {
                        this.fecFin.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1); //this.fecFin.VisibleDate = DateTime.Today;
                    }
                }
                else
                {
                    btnBuscar.Enabled = false;
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (fecFin != null)
            {
                DateTime ffin = fecFin.SelectedDate, fmax = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1);
                if (!(ffin <= fmax))
                {
                    lblErrorGeneral.Text = "El día seleccionado es mayor al mes actual. <br />" +
                        "Seleccione una fecha menor al mes actual.";
                    lblErrorGeneral.Visible = true;
                }
                else
                {
                    lblErrorGeneral.Visible = false;
                    DataTable primasObtenidas = objetoPrimasPendientes.obtenerPrimasPendientes(informacionUsuario.codIntermediario.ToString(), ffin);
                    ViewState["primasObtenidas"] = primasObtenidas;
                    if (primasObtenidas.Rows.Count > 0)
                    {
                        pnlResultadoPrimas.Visible = true;
                        grvResultadoPrimas.DataSource = primasObtenidas;
                        grvResultadoPrimas.DataBind();
                        btnExportarPrimasPendientesExcel.Focus();
                    }
                    else
                    {
                        lblErrorGeneral.Text = "No se encontraron datos con los parametros ingresados";
                        lblErrorGeneral.Visible = true;
                    }
                }
            }
        }

        protected void btnExportarPrimasPendientes_Click(object sender, EventArgs e)
        {
            if (fecFin != null)
            {
                DateTime ffin = fecFin.SelectedDate;
                ReportDocument objetoReporteI = new ReportDocument();
                string nombrePDF = "Primas_Pendientes_" + DateTime.Now.ToString();
                objetoReporteI.Load(Server.MapPath("../Reportes/RPT/Primas_PP/Primas_Pendientes_RPT.rpt"));
                objetoReporteI.Database.Tables[0].SetDataSource(ViewState["primasObtenidas"]);
                objetoReporteI.SetParameterValue("FechaFin", ffin);
                objetoReporteI.SetParameterValue("codagente", informacionUsuario.codIntermediario);
                objetoReporteI.SetParameterValue("nombreAgente", objetoPrimasPendientes.obtenerNombreIntermediario(informacionUsuario.codIntermediario));
                objetoReporteI.ExportToHttpResponse
                      (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, nombrePDF);
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Principal/Menu_Principal.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Oficina_Virtual/Consulta_Primas_Pendientes.aspx");
        }

        protected void btnExportarPrimasPendientesExcel_Click(object sender, EventArgs e)
        {
            btnExportarPrimasPendientes.Enabled = false;
            btnExportarPrimasPendientesExcel.Enabled = false;
            if (fecFin != null)
            {
                DateTime ffin = fecFin.SelectedDate;
                if (objetoPrimasPendientes.crearArchivoExcel(informacionUsuario.codIntermediario, ffin, ffin, "P"))
                {
                    string nombreArchivo = "Proyectado.xls"; //nombre definido en package***
                    if (DownloadFile(nombreArchivo))
                    {
                        lblError.Visible = false;
                        string IpReimpresionPoliza = System.Configuration.ConfigurationManager.AppSettings["IpReimpresionPoliza"];
                        string ruta = IpReimpresionPoliza + nombreArchivo;
                        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                        response.ClearContent();
                        response.Clear();
                        response.ContentType = "text/plain";
                        response.AddHeader("Content-Disposition", "attachment; filename=" + nombreArchivo + ";");
                        response.TransmitFile(ruta);
                        response.Flush();
                        response.End();
                    }
                    else
                        lblError.Visible = true;
                }
            }
            btnExportarPrimasPendientes.Enabled = true;
            btnExportarPrimasPendientesExcel.Enabled = true;
        }
        public bool DownloadFile(string nombreArchivo)
        {
            string IpReimpresionPoliza = System.Configuration.ConfigurationManager.AppSettings["IpReimpresionPoliza"];
            string descFilePathAndName = IpReimpresionPoliza + nombreArchivo;
            try { WebRequest myre = WebRequest.Create(descFilePathAndName); }
            catch { return false; }
            try
            {
                byte[] fileData;
                using (WebClient client = new WebClient()) { fileData = client.DownloadData(descFilePathAndName); }
                return true;
            }
            catch (Exception ex)
            {
                lblError.Text = "Error en la descarga: " + ex.Message;
                return false;
            }
        }
    }
}