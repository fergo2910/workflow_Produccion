using Lbl_Cotizado_Autos_Web.Comunes;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using Lbl_Cotizado_Autos_Web.HogarSeguro;
using Lbl_Cotizador_Autos_Web.Acceso;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CotizadorWebAutos.HogarSeguro
{
    public partial class Cotizar_Agente : System.Web.UI.Page
    {
        
        int idUsuario = 0;
        int idPlan = 0;
        DataTable formasPagoPlan = new DataTable();
        DataTable informacionPlanSeleccionado = new DataTable();
        ConsultasBD clConsultasBD = new ConsultasBD();
        DataTable numeroPagosPlan = new DataTable();
        Varias clVarias = new Varias();
        string cCodprod = string.Empty;
        string cCodplan = string.Empty;
        string cRevplan = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            idUsuario = Convert.ToInt32(Request.QueryString["userId"]);
            idPlan = Convert.ToInt32(Request.QueryString["idplan"]);

            informacionPlanSeleccionado = clVarias.obtenerInformacionPlan(idPlan);

            cCodprod = informacionPlanSeleccionado.Rows[0]["codprod"].ToString();
            cCodplan = informacionPlanSeleccionado.Rows[0]["codplan"].ToString();
            cRevplan = informacionPlanSeleccionado.Rows[0]["revplan"].ToString();

            if (!IsPostBack)
            {
                idUsuario = Convert.ToInt32(Request.QueryString["userId"]);
                idPlan = Convert.ToInt32(Request.QueryString["idplan"]);               

                formasPagoPlan = clConsultasBD.ObtenerInformacionFormaPago(cCodprod, cCodplan, cRevplan);
                numeroPagosXPlan();
            }
        }
        protected void btnPersonalesSiguiente_Click(object sender, EventArgs e)
        {
            if (txtCorreoCliente.Text == string.Empty || txtNombreCliente.Text == string.Empty || txtTelefonoCliente.Text == string.Empty)
            {
                mostrarMensaje("Debe ingresar los datos personales para poder continuar.");
                return;
            }

            if (txtTelefonoCliente.Text.Length < 8)
            {
                mostrarMensaje("Debe ingresar un número teléfonico valido.");
                return;
            }

            if (rbtnTipoBien.SelectedValue == string.Empty)
            {
                mostrarMensaje("Seleccione si es inquilino o propietario.");
                return;
            }

            if (chkAgente.Checked && (txtCodigoAgente.Text == string.Empty))
            {
                mostrarMensaje("Debe ingresar un codigo de agente.");
                return;
            }            
        }
        protected void btnCotizarPropietarioUno_Click(object sender, EventArgs e)
        {
            guardarCotizacion("HOGARTOT-1", "COMBO01");
        }       
        private void guardarCotizacion(string id_plan_pol, string plan_pol)
        {
            Proceso_Cotizacion clCotizacion = new Proceso_Cotizacion();
            bool resultadoAgente = false;

            int planElegido = clCotizacion.obtenerIdPlanWeb(id_plan_pol, plan_pol);

            if (txtCodigoAgente.Text == string.Empty)
            {
                //int numeroCotizacion = Convert.ToInt32(clCotizacion.guardarCotizacion(txtNombreCliente.Text, txtCorreoCliente.Text, Convert.ToInt32(txtTelefonoCliente.Text), DateTime.Now, planElegido, id_plan_pol, plan_pol));


                switch (id_plan_pol + plan_pol)
                {
                    case "HOGARTOT-1COMBO01":
                        
                        break;
                    case "HOGARTOT-1COMBO02":
                       
                        break;
                    case "HOGARTOT-1COMBO03":
                      
                        break;
                }
            }
            else
            {
                resultadoAgente = clCotizacion.ObtenerAgente(txtCodigoAgente.Text);

                if (resultadoAgente == true)
                {
                    //int numeroCotizacion = Convert.ToInt32(clCotizacion.guardarCotizacion(txtNombreCliente.Text, txtCorreoCliente.Text, Convert.ToInt32(txtTelefonoCliente.Text), DateTime.Now, planElegido, id_plan_pol, plan_pol, Convert.ToInt32(txtCodigoAgente.Text)));

                    switch (id_plan_pol + plan_pol)
                    {
                        case "HOGARTOT-1COMBO01":
                           
                            break;
                        case "HOGARTOT-1COMBO02":
                            
                            break;
                        case "HOGARTOT-1COMBO03":
                           
                            break;
                    }
                }
                else
                {
                    mostrarMensaje("El codigo de agente no es valido.");

                }
            }
        }                      
        private void mostrarMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", "alert('" + mensaje + "');", true);
        }                
        protected void btnRegresarCotizacion_Click(object sender, EventArgs e)
        {
            //Response.Redirect("../Hogar_Seguro/Cotizaciones.aspx?codagent=" + codAgente);
        }
        protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            numeroPagosXPlan();
        }
        private void numeroPagosXPlan()
        {
            numeroPagosPlan = clConsultasBD.obtenerNumeroPagosXPlan(cCodprod, cCodplan, cRevplan, "" /*ddlFormaPago.SelectedValue*/);
            ddlNumeroPagos.DataSource = numeroPagosPlan;
            ddlNumeroPagos.DataValueField = "CODPLANFINAN";
            ddlNumeroPagos.DataTextField = "NOMPLAN";
            ddlNumeroPagos.DataBind();
        }
    }
}