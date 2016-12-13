using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Lbl_Cotizador_Autos_Web;
using Lbl_Cotizado_Autos_Web.GM_Control_Cupones;
using System.Data;
using Lbl_Cotizador_Autos_Web.Acceso;

namespace CotizadorWebAutos.GM_Control_Cupones
{
    public partial class Control_Cupones : System.Web.UI.Page
    {

        string Mensaje = string.Empty;
        ValidacionPoliza vp = new ValidacionPoliza();
        IngresoSistema.informacionUsuario informacionUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();
                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];
                
                quitarAlertaObs();
                quitarAlertaBusqueda();
                txtfecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
                btnCobrarCupon.Visible = true; //se quita boton al mostrar altertaObs()
                btnCobrarCupon.Enabled = true;
                txtcodpol.Focus();
            }
        }
        public void alertaBusqueda(string Mensaje)
        {
            //<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            AlertaMsg.InnerText = Mensaje;
            AlertaMsg.Attributes.Add("class", "alert alert-danger text-center btn-sm btn-block");
            HtmlGenericControl accion = new HtmlGenericControl("a");
            AlertaMsg.Controls.Add(accion); // tabs is id of ul tag which is runat=server
            accion.Attributes.Add("href", "#");
            accion.Attributes.Add("data-dismiss", "alert");
            accion.Attributes.Add("aria-label", "close");
            accion.Attributes.Add("class", "close");
            accion.InnerHtml = "&times;";

            TablaAsociados.DataSource = null;
            TablaAsociados.DataBind();
        }
        public void quitarAlertaBusqueda()
        {
            //<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            AlertaMsg.InnerText = Mensaje;
            AlertaMsg.Attributes.Add("class", "");
            HtmlGenericControl accion = new HtmlGenericControl();
            AlertaMsg.Controls.Add(accion); // tabs is id of ul tag which is runat=server
            accion.Attributes.Add("href", "");
            accion.Attributes.Add("data-dismiss", "");
            accion.Attributes.Add("aria-label", "");
            accion.Attributes.Add("class", "");
            accion.InnerHtml = "";
        }
        public void alertaObs(string Mensaje)
        {
            //<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            alertaObservaciones.InnerText = Mensaje;
            alertaObservaciones.Attributes.Add("class", "alert alert-danger text-center btn-sm btn-block col-lg-6");
            HtmlGenericControl accion = new HtmlGenericControl("a");
            alertaObservaciones.Controls.Add(accion); // tabs is id of ul tag which is runat=server
            accion.Attributes.Add("href", "#");
            accion.Attributes.Add("data-dismiss", "alert");
            accion.Attributes.Add("aria-label", "close");
            accion.Attributes.Add("class", "close");
            accion.InnerHtml = "&times;";

            //btnCobrarCupon.Visible = false;
            //TablaAsociados.DataSource = null;
            //TablaAsociados.DataBind();
            //observaciones.DataSource = null;
            //observaciones.DataBind();
        }
        public void quitarAlertaObs()
        {
            //<a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
            alertaObservaciones.InnerText = Mensaje;
            alertaObservaciones.Attributes.Add("class", "");
            HtmlGenericControl accion = new HtmlGenericControl();
            alertaObservaciones.Controls.Add(accion); // tabs is id of ul tag which is runat=server
            accion.Attributes.Add("href", "");
            accion.Attributes.Add("data-dismiss", "");
            accion.Attributes.Add("aria-label", "");
            accion.Attributes.Add("class", "");
            accion.InnerHtml = "";
        }
        protected void btnBusqueda_Click(object sender, EventArgs e)
        {
            //limpiando tablas para nueva busqueda
            pnlBeneficiarios.Visible = false;
            pnlObservaciones.Visible = false;
            pnlHistorial.Visible = false;
            TablaAsociados.DataSource = null;
            TablaAsociados.DataBind();
            observaciones.DataSource = null;
            observaciones.DataBind();
            historialCupones.DataSource = null;
            historialCupones.DataBind();

            int numpol, certificado;
            DataTable retorno;

            if (txtcodpol.Text == string.Empty || txtnumpol.Text == string.Empty ||
                txtcertificado.Text == string.Empty)
            {
                alertaBusqueda("Ingresar todos los campos");
                return;
            }
            
            if(!Int32.TryParse(txtnumpol.Text, out numpol))
            {
                alertaBusqueda("Numero de poliza solo acepta numeros");
                return;
            }
            
            if (!Int32.TryParse(txtcertificado.Text, out certificado))
            {
                alertaBusqueda("El certificado solo acepta numeros");
                return;
            }

            try 
            {
                ViewState["dataRetorno"] = vp.polizaActiva(txtcodpol.Text.ToUpper(), numpol, certificado, DateTime.Now);
                retorno = (DataTable)ViewState["dataRetorno"];
            }
            catch (Exception ex)
            {
                alertaBusqueda(ex.Message);
                retorno = null;
                return;
            }

            if(retorno == null || retorno.Rows.Count == 0)
            {
                alertaBusqueda("No hay datos disponibles");
                return;
            }
            else
            {
                ViewState["ideaseg"] = retorno.Rows[0]["IDEASEG"].ToString();
                actualizarHistorial();
                TablaAsociados.DataSource = retorno;
                TablaAsociados.DataBind();
                quitarAlertaBusqueda();
                pnlBeneficiarios.Visible = true;
            }
        }
        protected void seleccion_Click(object sender, ImageClickEventArgs e)
        {
            //limpiando tabla para nuevo asegurado
            observaciones.DataSource = null;
            observaciones.DataBind();

            int filaSeleccionada = 0;
            DataTable observaciones_asegurado;
            for (int i = 0; i < TablaAsociados.Rows.Count; i++)
            {
                ImageButton btn = (ImageButton)TablaAsociados.Rows[i].Cells[0].FindControl("btnSeleccion");

                if (btn != null)
                {
                    if (btn.Equals((ImageButton)sender))
                    {
                        HiddenField hf = (HiddenField)TablaAsociados.Rows[i].Cells[0].FindControl("valorIDEASEG");
                        if (hf != null)
                        {
                            //Label1.Text = hf.Value;
                            ViewState["ideaseg"] = hf.Value;
                            filaSeleccionada = i;
                            try
                            {
                                observaciones_asegurado = vp.observacionesPoliza((string)ViewState["ideaseg"], DateTime.Now);
                                if(observaciones_asegurado.Rows.Count>0)
                                {
                                    actualizarHistorial();
                                    observaciones.DataSource = observaciones_asegurado;
                                    observaciones.DataBind();
                                    pnlObservaciones.Visible = true;
                                }
                                else
                                {
                                    alertaObs("No se encontró ningún dato");
                                    pnlObservaciones.Visible = true;
                                }
                            }catch(Exception ex)
                            {
                                alertaObs(ex.Message);
                            }
                            break;
                        }
                    }
                }
            }
            TablaAsociados.DataSource = (DataTable)ViewState["dataRetorno"];
            TablaAsociados.DataBind();
            TablaAsociados.Rows[filaSeleccionada].BackColor = System.Drawing.Color.Gray;
            pnlObservaciones.Focus();
        }
        protected void btnCobrarCupon_Click(object sender, EventArgs e)
        {
            int codigoProveedor = int.Parse(informacionUsuario.ideProveedor);
            string data = vp.retornaCupones(int.Parse((string)ViewState["ideaseg"]), codigoProveedor, DateTime.Parse(txtfecha.Text));
            if (data != string.Empty)
            {
                lblinformacionCupon.Text = data;
            }
            else
                lblinformacionCupon.Text = "No existen datos";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { informacionCupon(); });", true);
            btnCobrarCupon.Enabled = false;
            actualizarHistorial();
        }
        public void actualizarHistorial()
        {
            DataTable historial_asegurados = vp.obtenerHistorialAsegurados(int.Parse((string)ViewState["ideaseg"]));
            historialCupones.DataSource = historial_asegurados;
            historialCupones.DataBind();
            pnlHistorial.Visible = true;
            lblTitulo.Text = "Historial de cupones. Total utilizados: " + historial_asegurados.Rows.Count;
        }
        protected void historialMedico_Click(object sender, EventArgs e)
        {
            Response.Redirect("Historial_Cupones.aspx");
        }

    }
}