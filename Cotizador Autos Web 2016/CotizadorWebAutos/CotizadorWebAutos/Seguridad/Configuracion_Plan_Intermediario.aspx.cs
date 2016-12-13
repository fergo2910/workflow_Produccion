using Lbl_Cotizado_Autos_Web.Seguridad;
using Lbl_Cotizador_Autos_Web.Acceso;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CotizadorWebAutos.Seguridad
{
    public partial class Configuracion_Plan_Intermediario : System.Web.UI.Page
    {
        IngresoSistema.informacionUsuario informacionUsuario;     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();

                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];

                if (!IsPostBack)
                {
                    llenarIntermediariosActivos();
                }
            }
        }
        private void llenarIntermediariosActivos()
        {            
            ConfiguracionPlanIntermediario clConfiguracion = new ConfiguracionPlanIntermediario();

            cmbIntermediarios.DataSource = clConfiguracion.obtenerIntermediariosActivos();
            cmbIntermediarios.DataTextField = "nombre";
            cmbIntermediarios.DataValueField = "codinter";
            cmbIntermediarios.DataBind();                
        }
        protected void cmbIntermediarios_SelectedIndexChanged(object sender, EventArgs e)
        {            
                DataTable planesDisponibles = new DataTable();
                DataTable planesDeIntermediario = new DataTable();
                ConfiguracionPlanIntermediario clConfiguracion = new ConfiguracionPlanIntermediario();

                planesDisponibles = clConfiguracion.obtenerCatalogoPlanes();
                planesDeIntermediario = clConfiguracion.obtenerPlanesDeIntermediario(cmbIntermediarios.SelectedValue);

                chkPlanesDisponibles.Items.Clear();

                chkPlanesDisponibles.DataSource = planesDisponibles;
                chkPlanesDisponibles.DataTextField = "nombre";
                chkPlanesDisponibles.DataValueField = "id_plan_web";
                chkPlanesDisponibles.DataBind();

                for (int k = 0; k < chkPlanesDisponibles.Items.Count; k++)
                {
                    for (int m = 0; m < planesDeIntermediario.Rows.Count; m++)
                    {
                        if (chkPlanesDisponibles.Items[k].Value.ToString() == planesDeIntermediario.Rows[m]["id_plan_web"].ToString())
                        {
                            chkPlanesDisponibles.Items[k].Selected = true;
                        }
                    }
                }
        }
        protected void chkPlanesDisponibles_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<String> seleccionados = new List<string>();
            List<String> noseleccionados = new List<string>();
            ConfiguracionPlanIntermediario clConfiguracion = new ConfiguracionPlanIntermediario();

            foreach (ListItem item in chkPlanesDisponibles.Items)
            {
                if (item.Selected)
                {
                    seleccionados.Add(item.Value);
                }
                else
                {
                    noseleccionados.Add(item.Value);
                }
            }

            clConfiguracion.agregarPlanIntermediario(cmbIntermediarios.SelectedValue, seleccionados, noseleccionados);

            seleccionados.Clear();
            noseleccionados.Clear();
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Principal/Menu_Principal.aspx");            
        }
    }
}