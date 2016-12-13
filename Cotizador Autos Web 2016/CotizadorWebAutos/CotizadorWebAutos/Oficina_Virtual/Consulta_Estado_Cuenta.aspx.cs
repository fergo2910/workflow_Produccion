using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lbl_Cotizado_Autos_Web.Oficina_Virtual;
using Lbl_Cotizador_Autos_Web.Acceso;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

namespace CotizadorWebAutos.Oficina_Virtual
{
    public partial class Consulta_Estado_Cuenta : System.Web.UI.Page
    {

        IngresoSistema.informacionUsuario informacionUsuario;
        Validacion_Estado_Cuenta objetoEstadoCuenta = new Validacion_Estado_Cuenta();
        bool esUsuarioCobros = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();
                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];

                comboRamoPolizas.DataTextField = "DESCRIP";
                comboRamoPolizas.DataValueField = "DESCRIP";
                comboRamoPolizas.Items.Insert(0, "TODOS");
                comboRamoPolizas.DataSource = objetoEstadoCuenta.obtenerProductos();
                comboRamoPolizas.DataBind();
                if (informacionUsuario.accionesPermitidas.Contains("Descargar_Estado_Cuenta_Intermediario") || informacionUsuario.accionesPermitidas.Contains("Descargar_Estado_Cuenta"))
                {//SI TIENE UNO DE LOS DOS PERMISOS NECESARIOS, PUEDE BUSCAR ESTADOS DE CUENTA
                    btnBuscar.Enabled = true;
                    if (informacionUsuario.accionesPermitidas.Contains("Descargar_Estado_Cuenta"))
                        esUsuarioCobros = true;//TIENE TODOS LOS PERMISOS PARA VER TODAS LOS ESTADOS DE CUENTA
                    else//ESTADOS DE CUENTA POR INTERMEDIARIO
                        esUsuarioCobros = false;
                }
                else //SINO TIENE NINGUN PERMISO, NO PUEDE BUSCAR ESTADOS DE CUENTA
                    btnBuscar.Enabled = false;
            }
        }
        /// <summary>
        /// Metodo de busqueda según parametros, una búsqueda secuencial según los parametros obtenidos.
        /// Paso 1
        /// </summary>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            pnlResultadoVigencias.Visible = false;//QUITAR PANEL DE VIGENCIAS
            pnlResultadoEstadoCuenta.Visible = false;//QUITAR PANEL DE ESTADO DE CUENTA

            if (txtNumid.Text.Equals(string.Empty) && txtDvid.Text.Equals(string.Empty) &&
                (comboRamoPolizas.SelectedValue.Equals("Todos") && txtNumPol.Text.Equals(string.Empty)) &&
                txtNombres.Text.Equals(string.Empty) && txtApellidos.Text.Equals(string.Empty))
            {
                lblErrorGeneral.Text = "No se puede realizar una búsqueda sin parámetros";
                lblErrorGeneral.Visible = true;
                return;
            }
            else
                lblErrorGeneral.Visible = false;

            string numid = txtNumid.Text;
            string dvid = txtDvid.Text;
            string ramo = comboRamoPolizas.SelectedValue;
            string numpol = txtNumPol.Text;
            string nombres = txtNombres.Text;
            string apellidos = txtApellidos.Text;

            DataTable polizasObtenidas = objetoEstadoCuenta.busquedaPoliza(numid, dvid, ramo, numpol, nombres, apellidos, informacionUsuario.codIntermediario, esUsuarioCobros);
            if (polizasObtenidas.Rows.Count > 0)
            {
                grvPolizas.DataSource = polizasObtenidas;
                grvPolizas.DataBind();
                pnlResultadoPolizas.Visible = true;
                pnlResultadoPolizas.Focus();
            }
            else
            {
                lblErrorGeneral.Text = "No existen pólizas con los parámetros ingresados";
                lblErrorGeneral.Visible = true;
                pnlResultadoPolizas.Visible = false;
            }
        }
        /// <summary>
        /// Metodo de muestra de detalles según poliza seleccionada
        /// </summary>
        protected void btnDetallePolizas_Command(object sender, CommandEventArgs e)
        {
            pnlResultadoEstadoCuenta.Visible = false;//QUITAR PANEL DE ESTADO DE CUENTA

            string parametro = e.CommandArgument.ToString();
            string[] listaparametros = parametro .Split(';');
            string codpol = listaparametros[0];
            string numpol = listaparametros[1];
            DataTable polizasObtenidas = objetoEstadoCuenta.busquedaDetallePoliza(codpol,numpol);
            if(polizasObtenidas.Rows.Count>0)
            {
                foreach(DataRow dr in polizasObtenidas.Rows)
                {
                    dr["STSPOL"] = objetoEstadoCuenta.descripEstadoPol(dr["STSPOL"].ToString());
                }
                grvVigencias.DataSource = polizasObtenidas;
                grvVigencias.DataBind();
                pnlResultadoVigencias.Visible = true;
                pnlResultadoVigencias.Focus();
            }
            else
            {
                pnlResultadoVigencias.Visible = false;
                lblInfoPoliza.Text = "No existe información para esta póliza";
            }
        }
        /// <summary>
        /// metodo que muestra el estado de cuenta que se va a imprimir.
        /// </summary>
        protected void btnDetalleVigencias_Command(object sender, CommandEventArgs e)
        {
            string idepol = e.CommandArgument.ToString();
            lblidepolseleccionado.Text = idepol;
            DataTable vigenciasObtenidas = objetoEstadoCuenta.busquedaDetalleVigencia(idepol);
            DataTable saldosObtenidos = objetoEstadoCuenta.busquedaSaldosFavor(idepol);
            if (vigenciasObtenidas.Rows.Count > 0)
            {
                grvEstadoCuenta.DataSource = vigenciasObtenidas;
                grvEstadoCuenta.DataBind();
                grvSaldosFavor.DataSource = saldosObtenidos;
                grvSaldosFavor.DataBind();
                pnlResultadoEstadoCuenta.Visible = true;
                pnlEstadoCuenta.Focus();
            }
            else
            {
                pnlResultadoEstadoCuenta.Visible = false;
                lblInfoVigencia.Text = "No existe información para esta vigencia";
            }
        }
        /// <summary>
        /// metodo que llena el data set para realizar el reporte.
        /// </summary>
        protected void btnImprimirEstadoCuenta_Click(object sender, EventArgs e)
        {
            if (!lblidepolseleccionado.Text.Equals(string.Empty))
            {
                string tipoPoliza = objetoEstadoCuenta.obtenerTipoPoliza(lblidepolseleccionado.Text);
                DataTable tablaPoliza = new DataTable();
                DataTable tablaFactura = new DataTable();
                DataTable tablaDescuentos = new DataTable();
                DataTable tablaDescuentos2 = new DataTable();
                DataTable tablaIntermediario = new DataTable();
                DataTable tablaTotales = new DataTable();
                ReportDocument objetoReporteI = new ReportDocument();
                string nombrePDF = comboRamoPolizas.SelectedValue + "_" + lblidepolseleccionado.Text + "_" + DateTime.Now.ToString();
                objetoReporteI.Load(Server.MapPath("../Reportes/RPT/Estado_Cuenta/Estado_Cuenta.rpt"));
                objetoReporteI.Database.Tables[0].SetDataSource(
                    tablaDescuentos = objetoEstadoCuenta.impresionQueryDescuentos(lblidepolseleccionado.Text, tipoPoliza)
                    );
                objetoReporteI.Database.Tables[1].SetDataSource(
                    tablaDescuentos2 = objetoEstadoCuenta.impresionQueryDescuentos2(lblidepolseleccionado.Text, tipoPoliza)
                    );
                objetoReporteI.Database.Tables[2].SetDataSource(
                    tablaFactura = objetoEstadoCuenta.impresionQueryFactura(lblidepolseleccionado.Text, tipoPoliza)
                    );
                objetoReporteI.Database.Tables[3].SetDataSource(
                    tablaIntermediario = objetoEstadoCuenta.impresionQueryIntermediario(lblidepolseleccionado.Text, tipoPoliza)
                    );
                objetoReporteI.Database.Tables[4].SetDataSource(
                    tablaPoliza = objetoEstadoCuenta.impresionQueryPoliza(lblidepolseleccionado.Text, tipoPoliza)
                    );
                objetoReporteI.Database.Tables[5].SetDataSource(
                    tablaTotales = objetoEstadoCuenta.impresionQueryTotales(lblidepolseleccionado.Text, tipoPoliza)
                    );

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
            Response.Redirect("../Oficina_Virtual/Consulta_Estado_Cuenta.aspx");
        }

    }
}