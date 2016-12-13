using Lbl_Cotizador_Autos_Web.Acceso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lbl_Cotizado_Autos_Web.Oficina_Virtual;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System.Net;
using System.IO;

namespace CotizadorWebAutos.Oficina_Virtual
{
    public partial class Consulta_Primas_Pagadas : System.Web.UI.Page
    {
        IngresoSistema.informacionUsuario informacionUsuario;
        Validacion_Primas objetoPrimasPagadas = new Validacion_Primas();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();
                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];
                if (informacionUsuario.accionesPermitidas.Contains("Descargar_Primas_Pagadas"))
                {
                    btnBuscar.Enabled = true;
                    if (!IsPostBack)
                    {
                        this.fecFin.SelectedDate = this.fecFin.VisibleDate = DateTime.Today;
                        this.fecIni.SelectedDate = this.fecIni.VisibleDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
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
            if(fecIni.SelectedDate != null && fecFin!=null)
            {
                DateTime fini = fecIni.SelectedDate, ffin = fecFin.SelectedDate;
                if (!(fecIni.SelectedDate.AddMonths(2) >= fecFin.SelectedDate))
                {
                    lblErrorGeneral.Text = "El rango de tiempo seleccionado supera los dos meses. <br />" +
                        "Seleccione dos fechas que tengan como máximo 2 meses de diferencia.";
                    lblErrorGeneral.Visible = true;
                }
                else if (fecIni.SelectedDate > fecFin.SelectedDate)
                {
                    lblErrorGeneral.Text = "La fecha de inicio es mayor que la de fin. <br /> " +
                        "Favor seleccionar fechas que tengan como máximo 2 meses de diferencia y coherencia en las fechas.";
                    lblErrorGeneral.Visible = true;
                }
                else
                {
                    lblErrorGeneral.Visible = false;
                    DataTable primasObtenidas = objetoPrimasPagadas.obtenerPrimasPagadas(informacionUsuario.codIntermediario.ToString(), fini, ffin);
                    ViewState["primasObtenidas"] = primasObtenidas;
                    if (primasObtenidas.Rows.Count > 0)
                    {
                        pnlResultadoPrimas.Visible = true;
                        grvResultadoPrimas.DataSource = ViewState["primasObtenidas"];
                        grvResultadoPrimas.DataBind();
                        btnExportarPrimasPagadasExcel.Focus();
                    }
                    else
                    {
                        lblErrorGeneral.Text = "No se encontraron datos con los parametros ingresados";
                        lblErrorGeneral.Visible = true;
                    }
                }
            }
        }

        protected void btnExportarPrimasPagadas_Click(object sender, EventArgs e)
        {
            if (fecIni.SelectedDate != null && fecFin != null)
            {
                DateTime fini = fecIni.SelectedDate, ffin = fecFin.SelectedDate;
                ReportDocument objetoReporteI = new ReportDocument();
                string nombrePDF = "Primas_Pagadas_" + DateTime.Now.ToString();
                objetoReporteI.Load(Server.MapPath("../Reportes/RPT/Primas_PP/Primas_Pagadas_RPT.rpt"));
                objetoReporteI.Database.Tables[0].SetDataSource(ViewState["primasObtenidas"]);
                objetoReporteI.SetParameterValue("FechaInicio", fini);
                objetoReporteI.SetParameterValue("FechaFin", ffin);
                objetoReporteI.SetParameterValue("codagente", informacionUsuario.codIntermediario);
                objetoReporteI.SetParameterValue("nombreAgente", objetoPrimasPagadas.obtenerNombreIntermediario(informacionUsuario.codIntermediario));
                objetoReporteI.ExportToHttpResponse
                      (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, nombrePDF);
            }
        }

        protected void btnExportarPrimasPagadasExcel_Click(object sender, EventArgs e)
        {
            btnExportarPrimasPagadas.Enabled = false;
            btnExportarPrimasPagadasExcel.Enabled = false;
            if (fecIni.SelectedDate != null && fecFin != null)
            {
                DateTime fini = fecIni.SelectedDate, ffin = fecFin.SelectedDate;
                if (objetoPrimasPagadas.crearArchivoExcel(informacionUsuario.codIntermediario, fini, ffin, "C"))
                {
                    string nombreArchivo = "Cobros.xls"; //nombre definido en package***
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
            btnExportarPrimasPagadas.Enabled = true;
            btnExportarPrimasPagadasExcel.Enabled = true;
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

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Principal/Menu_Principal.aspx");
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Oficina_Virtual/Consulta_Primas_Pagadas.aspx");
        }
    }
}