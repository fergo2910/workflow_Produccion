using Lbl_Cotizado_Autos_Web.Clientes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CotizadorWebAutos.Personas.GastosMedicosIndividual
{
    public partial class cotizar : System.Web.UI.Page
    {
        DataTable datosHijos;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calendarFecNacTitular.StartDate = DateTime.Now.AddYears(-65);
                llenarCombos();
            }
            
        }
        private void llenarCombos()
        {
            Consultas clVarios = new Consultas();
            DataTable datosGeneros = new DataTable();
            DataTable datosEstadoCivil = new DataTable();

            datosGeneros = clVarios.obtenerGeneros();
            datosEstadoCivil = clVarios.obtenerEstadosCiviles();

            cmbGeneroTitular.DataSource = datosGeneros;
            cmbGeneroTitular.DataTextField = "DESCRIP";
            cmbGeneroTitular.DataValueField = "CODLVAL";
            cmbGeneroTitular.DataBind();

            cmbGeneroTitular.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbGeneroTitular.SelectedIndex = 0;

            cmbGeneroConyuge.DataSource = datosGeneros;
            cmbGeneroConyuge.DataTextField = "DESCRIP";
            cmbGeneroConyuge.DataValueField = "CODLVAL";
            cmbGeneroConyuge.DataBind();

            cmbGeneroConyuge.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbGeneroConyuge.SelectedIndex = 0;

            cmbEstadoCivilTitular.DataSource = datosEstadoCivil;
            cmbEstadoCivilTitular.DataTextField = "DESCRIP";
            cmbEstadoCivilTitular.DataValueField = "CODLVAL";
            cmbEstadoCivilTitular.DataBind();

            cmbEstadoCivilTitular.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbEstadoCivilTitular.SelectedIndex = 0;

            datosHijos = new DataTable();
            datosHijos.Columns.Add("id_hijo", typeof(string));

            ViewState.Add("datosHijos", datosHijos);
        }
        protected void chkConyuge_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConyuge.Checked)
            {
                pnlDatosConyuge.Visible = true;                
            }
            else
            {
                pnlDatosConyuge.Visible = false;                
            }
        }
        protected void btnAgregarHijos_Click(object sender, EventArgs e)
        {
            pnlHIjos.Visible = true;
            datosHijos = ViewState["datosHijos"] as DataTable;
            
            datosHijos.Rows.Add(datosHijos.Rows.Count +1);

            dtlHijos.DataSource = datosHijos;
            dtlHijos.DataBind();        
        }
        protected void dtlHijos_DeleteCommand(object source, DataListCommandEventArgs e)
        {
            datosHijos = ViewState["datosHijos"] as DataTable;

            for (int i = 0; i < datosHijos.Rows.Count; i++)
            {
                int a = (int.Parse(datosHijos.Rows[i][0].ToString()));
                int b = int.Parse(dtlHijos.DataKeys[e.Item.ItemIndex].ToString());

                if (a == b)
                {
                    datosHijos.Rows.RemoveAt(i);
                }
            }

            dtlHijos.DataSource = datosHijos;
            dtlHijos.DataBind();

            if (datosHijos.Rows.Count == 0)
            {
                pnlHIjos.Visible = false;
            }
        }

        protected void btnCalcularPlanes_Click(object sender, EventArgs e)
        {
            if (!pnlPlanes.Visible)
            {
                pnlPlanes.Visible = true;
                btnSeleccionarDiamante.Focus();
            }
        }         
    }
}