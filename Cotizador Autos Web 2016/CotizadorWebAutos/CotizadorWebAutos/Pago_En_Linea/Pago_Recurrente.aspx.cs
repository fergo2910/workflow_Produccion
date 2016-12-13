using Lbl_Cotizado_Autos_Web.PagoEnLinea;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace CotizadorWebAutos.Pago_En_Linea
{
    public partial class Pago_Recurrente : System.Web.UI.Page
    {
        public List<int> conteoPositivo = new List<int>();
        public List<int> conteoNegativo = new List<int>();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable informacionTipoCuenta = new DataTable();
            DataTable informacionEntidadFinanciera = new DataTable();
            DataTable informacionTipoTarjeta = new DataTable();
            Requerimientos clRequerimientos = new Requerimientos();

            informacionTipoCuenta = clRequerimientos.ObtenerTipoCuenta();
            informacionEntidadFinanciera = clRequerimientos.ObtenerBancoCuenta();
            informacionTipoTarjeta = clRequerimientos.ObtenerTipoTarjeta();

            if (!IsPostBack)
            {
                ddlTipoCuenta.DataSource = informacionTipoCuenta;
                ddlTipoCuenta.DataValueField = "codigo";
                ddlTipoCuenta.DataTextField = "descripcion";
                ddlTipoCuenta.DataBind();

                ddlBancoTarjeta.DataSource = informacionEntidadFinanciera;
                ddlBancoTarjeta.DataValueField = "codigo";
                ddlBancoTarjeta.DataTextField = "descripcion";
                ddlBancoTarjeta.DataBind();

                ddlTipoTarjeta.DataSource = informacionTipoTarjeta;
                ddlTipoTarjeta.DataValueField = "codigo";
                ddlTipoTarjeta.DataTextField = "descripcion";
                ddlTipoTarjeta.DataBind();
            }
        }
        protected void btnBuscarPolizas_Click(object sender, EventArgs e)
        {
            Requerimientos clRequerimientos = new Requerimientos();
            DataTable datos = new DataTable();

            int numPol = 0;

            if (txtNumPol.Text != string.Empty)
            {
                numPol = int.Parse(txtNumPol.Text);
            }

            datos = clRequerimientos.buscarPolizaCliente(txtCodPol.Text.Trim(), numPol, txtNombres.Text, txtApellidos.Text, txtNumIDCliente.Text, txtDvIdCliente.Text, "900000");

            if (datos.Rows.Count > 0)
            {
                grvPolizas.DataSource = datos;
                grvPolizas.DataBind();

                pnlResultadoBusqueda.Visible = true;
                pnlRequerimientos.Visible = false;
                lblTotal.Visible = false;
            }
            else
            {
                pnlResultadoBusqueda.Visible = false;
                pnlRequerimientos.Visible = false;
            }
        }
        protected void grvPolizas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
        }
        protected void detallePoliza(Object sender, CommandEventArgs e)
        {
            int idepolSeleccionado = int.Parse(e.CommandArgument.ToString());

            ViewState.Add("idepolSeleccionado", idepolSeleccionado);

            obtenerReqsPoliza(idepolSeleccionado);

            lblTotal.Focus();
        }
        private DataTable obtenerReqsPoliza(int idepol)
        {
            Requerimientos clRequerimientos = new Requerimientos();
            DataTable requerimientos = new DataTable();

            requerimientos = clRequerimientos.buscarRequerimientosPoliza(idepol);

            if (requerimientos.Rows.Count > 0)
            {
                grvRequerimientos.DataSource = requerimientos;
                grvRequerimientos.DataBind();
                pnlRequerimientos.Visible = true;
                lblTotal.Visible = true;
                lblTotalRequerimientos.Focus();
            }
            else
            {
                pnlRequerimientos.Visible = true;
                lblTotal.Visible = false;
            }

            return requerimientos;
        }
        protected void grvRequerimientos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[6].Text == "COB")
                {                    
                    e.Row.Cells[6].Text = "Cobrado";
                }
                else
                {
                    e.Row.Cells[6].Text = "Pendiente";
                }
            }
        }
        protected void grvRequerimientos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void chkRequerimiento_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSeleccionado = new CheckBox();

            for (int i = 0; i < grvRequerimientos.Rows.Count; i++)
            {
                chkSeleccionado = grvRequerimientos.Rows[i].Cells[0].Controls[1] as CheckBox;

                if (chkSeleccionado.Checked)
                {
                    conteoPositivo.Add(i);
                }
                if ((!chkSeleccionado.Checked) && (chkSeleccionado.Enabled == true))
                {
                    conteoNegativo.Add(i);
                }
            }

            for (int i = 0; i < conteoPositivo.Count; i++)
            {
                if (conteoPositivo[i] < conteoNegativo[i])
                {
                    chkSeleccionado = grvRequerimientos.Rows[conteoPositivo[i]].Cells[0].Controls[1] as CheckBox;
                    chkSeleccionado.Checked = false;
                    mostrarMensajeJavaScript("No se pueden realizar pagos salteados.");
                }
            }

            //foreach (GridViewRow reqs in grvRequerimientos.Rows)
            //{
            //    chkSeleccionado = reqs.Cells[0].Controls[1] as CheckBox;
            //}
            //if()



            /*ConsultasBD clConsultas = new ConsultasBD();
            bool pagoSalteado = false;
            CheckBox chkSeleccionado = new CheckBox();

            foreach (GridViewRow reqs in grvRequerimientos.Rows)
            {
                chkSeleccionado = reqs.Cells[0].Controls[1] as CheckBox;

                if (chkSeleccionado.Checked)
                {
                    pagoSalteado = clConsultas.esPagoSalteado(int.Parse(reqs.Cells[1].Text));
                }

                if (pagoSalteado)
                {
                    chkSeleccionado.Checked = false;
                    mostrarMensajeJavaScript("No se pueden realizar pagos salteados.");
                    return;
                }
                else
                {
                    double sumaRequerimientos = 0;
                    double restaRequerimientos = 0;
                    string moneda = string.Empty;
                    string totRequerimientos = string.Empty;

                    foreach (GridViewRow item in grvRequerimientos.Rows)
                    {
                        sumaRequerimientos += double.Parse(item.Cells[5].Text);
                        moneda = item.Cells[4].Text;
                    }

                    foreach (GridViewRow row in grvRequerimientos.Rows)
                    {
                        chkSeleccionado = row.Cells[0].Controls[1] as CheckBox;

                        if (!chkSeleccionado.Checked)
                        {
                            restaRequerimientos += double.Parse(row.Cells[5].Text);
                        }
                    }

                    totRequerimientos = moneda + " " + (sumaRequerimientos - restaRequerimientos).ToString();
                    lblTotalRequerimientos.Text = totRequerimientos;

                    txtMontoCancelar.Text = (sumaRequerimientos - restaRequerimientos).ToString();
                    upnlFooter.Update();
                }
            } */
        }
        protected void chkMostrarPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarPass.Checked)
            {
                txtNumeroTarjeta.TextMode = TextBoxMode.Number;
            }
            else
            {
                txtNumeroTarjeta.TextMode = TextBoxMode.Password;
            }
        }
        protected void btnCobrar_Click(object sender, EventArgs e)
        {
            Requerimientos clRequerimientos = new Requerimientos();
            DataTable datosTerminalCobro = new DataTable();
            string respuestaCobro = string.Empty;

            datosTerminalCobro = clRequerimientos.ObtenerInformacionTerminalCobro("COBWEB");

            //Se debe realizar una funcion para validar que los requerimientos no esten salteados antes de efectuar el cobro.

            respuestaCobro = clRequerimientos.CobrarTC(datosTerminalCobro.Rows[0]["codigo_producto_poliza"].ToString(),
                                                        datosTerminalCobro.Rows[0]["numero_poliza"].ToString(),
                                                        datosTerminalCobro.Rows[0]["numero_certificado"].ToString(),
                                                        datosTerminalCobro.Rows[0]["codigo_entidad_financiera"].ToString(),
                                                        txtNumeroTarjeta.Text, txtMontoCancelar.Text,
                                                        txtFechaVencimientoTarjeta.Text, "ACSEL_WEB");

            XmlDocument xml = new XmlDocument();
            DataSet datosCobro = new DataSet();
            xml.LoadXml(respuestaCobro);

            datosCobro.ReadXml(new XmlNodeReader(xml));
            string respuesta = string.Empty;
            string codigoRespuesta = string.Empty;

            if (datosCobro.Tables[0].Rows.Count == 1)
            {
                respuesta = datosCobro.Tables[0].Rows[0]["DescRespuesta"].ToString();
                codigoRespuesta = datosCobro.Tables[0].Rows[0]["CodRespuesta"].ToString();

                if (codigoRespuesta == "00")
                {
                    //string numeroAutorizacion = datosCobro.Tables[0].Rows[0]["NumAutorizacion"].ToString();
                    CheckBox chkSeleccionado = new CheckBox();
                    Cobro_TC CobroTC = new Cobro_TC();
                    DOCING documentoIngreso = new DOCING();
                    REQUERIMIENTO req = new REQUERIMIENTO();
                    List<REQUERIMIENTO> listadoRequerimientos = new List<REQUERIMIENTO>();
                    List<DOCING> listadoDocIng = new List<DOCING>();
                    string codMoneda = string.Empty;
                    string resultadoXMLCobro = string.Empty;

                    foreach (GridViewRow item in grvRequerimientos.Rows)
                    {
                        req = new REQUERIMIENTO();
                        chkSeleccionado = item.Cells[0].Controls[1] as CheckBox;

                        if (chkSeleccionado.Checked)
                        {
                            req.IDEFACT = item.Cells[1].Text;
                            codMoneda = item.Cells[4].Text;
                            listadoRequerimientos.Add(req);
                        }
                    }

                    documentoIngreso.CLAVEAUTDOCING = "12354";
                    documentoIngreso.CODENTFINAN = "000208";
                    documentoIngreso.CODMONEDA = codMoneda;
                    documentoIngreso.MONTO = txtMontoCancelar.Text;
                    documentoIngreso.NUMREFDOC = txtNumeroTarjeta.Text;
                    documentoIngreso.TIPODOCING = "TAR";
                    listadoDocIng.Add(documentoIngreso);

                    resultadoXMLCobro = clRequerimientos.generarXMLCobro(listadoRequerimientos, listadoDocIng);

                    clRequerimientos.CobrarAcsel(resultadoXMLCobro);
                }
                else
                {
                    lblErrorCobro.Text = respuesta;
                    lblErrorCobro.Visible = true;
                    limpiarControlesCobrar();
                    upnlFooter.Update();
                }
            }
        }
        private void limpiarControlesCobrar()
        {
            txtNumeroTarjeta.Text = string.Empty;
            txtFechaVencimientoTarjeta.Text = string.Empty;
            txtDvIdCliente.Text = string.Empty;
            txtNumIDCliente.Text = string.Empty;
            txtNombres.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtCodPol.Text = string.Empty;
            txtNumPol.Text = string.Empty;

            obtenerReqsPoliza(int.Parse(ViewState["idepolSeleccionado"].ToString()));
        }
        private void mostrarMensajeJavaScript(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        protected void grvOperaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grvOperaciones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}