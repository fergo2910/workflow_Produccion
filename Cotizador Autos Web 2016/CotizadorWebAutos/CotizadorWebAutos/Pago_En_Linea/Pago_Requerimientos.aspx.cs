using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lbl_Cotizado_Autos_Web.PagoEnLinea;
using System.Data;
using System.Xml;
using System.Drawing;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using Lbl_Cotizado_Autos_Web.Comunes;
using System.Xml.Linq;

namespace CotizadorWebAutos.Pago_En_Linea
{
    public partial class Pago_Requerimientos : System.Web.UI.Page
    {
        int idUsuario = 2;
        string codigoIntermediario = string.Empty;
        public List<int> conteoPositivo = new List<int>();
        public List<int> conteoNegativo = new List<int>();
        Varias clVarias = new Varias();
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable informacionTipoCuenta = new DataTable();
            DataTable informacionEntidadFinanciera = new DataTable();
            DataTable informacionTipoTarjeta = new DataTable();
            Requerimientos clRequerimientos = new Requerimientos();

            informacionTipoCuenta = clRequerimientos.ObtenerTipoCuenta();
            informacionEntidadFinanciera = clRequerimientos.ObtenerBancoCuenta();
            informacionTipoTarjeta = clRequerimientos.ObtenerTipoTarjeta();
            //idUsuario = Convert.ToInt32(Request.QueryString["userId"]);
            idUsuario = 2; 
            codigoIntermediario = clVarias.obtenerInformacionUsuarioLogueado(idUsuario).Rows[0]["codigo_intermediario"].ToString();

            if (!IsPostBack)
            {
                //idUsuario = Convert.ToInt32(Request.QueryString["userId"]);
                idUsuario = 2; 
                codigoIntermediario = clVarias.obtenerInformacionUsuarioLogueado(idUsuario).Rows[0]["codigo_intermediario"].ToString();
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
            
            datos = clRequerimientos.obtenerOperacionesPoliza(numPol, txtCodPol.Text.Trim());

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
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int idepolSeleccionado = int.Parse(arg[0].ToString());
            int numOperSeleccionado = int.Parse(arg[1].ToString());

            //ViewState.Add("idepolSeleccionado", idepolSeleccionado);

            obtenerRequerimientos(idepolSeleccionado, numOperSeleccionado);

            lblTotal.Focus();
        }
        private DataTable obtenerRequerimientos(int idPpol, int numOper)
        {
            Requerimientos clRequerimientos = new Requerimientos();
            DataTable requerimientos = new DataTable();

            requerimientos = clRequerimientos.buscarRequerimientos(idPpol, numOper);

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
                CheckBox chkReq = (CheckBox)e.Row.FindControl("chkRequerimiento");

                if (e.Row.Cells[6].Text == "COB")
                {
                    chkReq.BackColor = Color.Red;
                    //chkReq.Checked = true;
                    chkReq.Enabled = false;
                    e.Row.Cells[6].Text = "Cobrado";
                }
                else
                {
                    chkReq.BackColor = Color.Green;
                    e.Row.Cells[6].Text = "Pendiente";
                }
            }
        }       
        protected void chkRequerimiento_CheckedChanged(object sender, EventArgs e)
        {
            List<int> seleccionados = new List<int>();
            List<int> noseleccionados = new List<int>();
            CheckBox chkSeleccionado = new CheckBox();

            foreach (GridViewRow item in grvRequerimientos.Rows)
            {
                chkSeleccionado = item.Cells[0].Controls[1] as CheckBox;

                if (item.Cells[6].Text == "Cobrado")
                {
                    seleccionados.Add(item.RowIndex);
                }
                else
                {
                    if (chkSeleccionado.Checked)
                    {
                        seleccionados.Add(item.RowIndex);
                    }
                    else
                    {
                        noseleccionados.Add(item.RowIndex);
                    }    
                }        
            }

            for (int i = 0; i < seleccionados.Count; i++)
            {
                if (int.Parse(seleccionados[i].ToString()) != i)
                {
                    for (int j= i; j < grvRequerimientos.Rows.Count; j++)
                    {
                        chkSeleccionado = (CheckBox)grvRequerimientos.Rows[j].FindControl("chkRequerimiento");

                        chkSeleccionado.Checked = false;
                    }

                    calcularTotalPago();
                    break;
                }
                else
                {
                    calcularTotalPago();
                }                
            }

            seleccionados.Clear();
            noseleccionados.Clear();
        }
        private void calcularTotalPago()
        {
            CheckBox chkSeleccionado = new CheckBox();

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
            WSCobroEnLineaMAPFRE.COBRO_TC_POSClient servicioCobro = new WSCobroEnLineaMAPFRE.COBRO_TC_POSClient();
            Varias clVarias = new Varias();
            DataTable datosTerminalCobro = new DataTable();
            string respuestaCobro = string.Empty;

            datosTerminalCobro = clRequerimientos.ObtenerInformacionTerminalCobro("COBWEB");

            var xmlfromLINQ = new XElement("COBRO",
                new XElement("DATOS_COBRO",
                    new XElement("NUMERO_TARJETA", txtNumeroTarjeta.Text.Trim()),
                    new XElement("FEC_VENCIMIENTO", txtFechaVencimientoTarjeta.Text.Trim()),
                    new XElement("MONTO", txtMontoCancelar.Text.Trim()),
                    new XElement("PROVEEDOR", "900000")
                    ));

            //Se codifica el xml para envio de informacion a traves del servicio web de cobro
            string xmlEnvioCodificado = clVarias.CodificarA64(xmlfromLINQ.ToString());

            //Se realiza la llamada al servicio web de cobro.
            respuestaCobro = servicioCobro.COBRAR_TC(xmlEnvioCodificado);

            string strInfoDecodificada = string.Empty;
            strInfoDecodificada = clVarias.DecodificarDesde64(respuestaCobro);            

            XmlDocument xml = new XmlDocument();
            DataSet datosCobro = new DataSet();
            xml.LoadXml(strInfoDecodificada);

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
                    documentoIngreso.CODENTFINAN = ddlBancoTarjeta.SelectedValue;
                    documentoIngreso.CODMONEDA = codMoneda;
                    documentoIngreso.MONTO = txtMontoCancelar.Text;
                    documentoIngreso.NUMREFDOC = txtNumeroTarjeta.Text;
                    documentoIngreso.TIPODOCING = ddlTipoCuenta.SelectedValue;
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
                    btnCobrar.Focus();
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
            txtCodPol.Text = string.Empty;
            txtNumPol.Text = string.Empty;

            //obtenerRequerimientos(int.Parse(ViewState["idepolSeleccionado"].ToString()));
        }
        private void mostrarMensajeJavaScript(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        protected void btnRegresarFooter_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Principal/Menu_Principal.aspx?userId=" + idUsuario);
        }
        protected void btnRegresarHead_Click(object sender, EventArgs e)
        {            
            Response.Redirect("../Principal/Menu_Principal.aspx?userId=" + idUsuario);
        }
    }
}