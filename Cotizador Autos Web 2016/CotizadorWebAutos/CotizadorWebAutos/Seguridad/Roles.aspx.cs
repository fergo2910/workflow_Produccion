using Lbl_Cotizado_Autos_Web.Seguridad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CotizadorWebAutos.Seguridad
{
    public partial class Roles : System.Web.UI.Page
    {
        public MantenimientoUsuarios clMantenimientoUsuarios = new MantenimientoUsuarios();
        public DataTable rolesSistema;
        public DataTable accionesSistema;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                llenarRolesSistema();
                llenarAccionesSistema();
                llenarRolesMantenimiento();
                llenarAccionesMantenimiento();
            }
        }
        private void llenarRolesSistema()
        {
            if (lstRolesSistema.SelectedIndex == -1)
            {
                clMantenimientoUsuarios = new MantenimientoUsuarios();
                rolesSistema = clMantenimientoUsuarios.obtenerRolesAccesoSuperUsuario();

                //lstRolesSistema.Items.Clear();

                lstRolesSistema.DataSource = rolesSistema;
                lstRolesSistema.DataTextField = "nombre";
                lstRolesSistema.DataValueField = "id_rol";
                lstRolesSistema.DataBind();    
            }
                        
        }
        private void llenarAccionesSistema()
        {
            clMantenimientoUsuarios = new MantenimientoUsuarios();
            accionesSistema = clMantenimientoUsuarios.obtenerAccionesSistema();

            //chkAccionesRol.Items.Clear();

            chkAccionesRol.DataSource = accionesSistema;
            chkAccionesRol.DataTextField = "nombre_accion";
            chkAccionesRol.DataValueField = "id_accion";
            chkAccionesRol.DataBind();                       
        }
        private void llenarRolesMantenimiento()
        {
            clMantenimientoUsuarios = new MantenimientoUsuarios();
            rolesSistema = clMantenimientoUsuarios.obtenerTodosRolesAccesoSuperUsuario();
            grvListaRoles.DataSource = rolesSistema;
            grvListaRoles.DataBind();
        }
        private void llenarAccionesMantenimiento()
        {
            clMantenimientoUsuarios = new MantenimientoUsuarios();
            DataTable accionesSuperUsuario = clMantenimientoUsuarios.obtenerTodasAccionesSuperUsuario();
            grvListaAccions.DataSource = accionesSuperUsuario;
            grvListaAccions.DataBind();
        }
        protected void lstRolesSistema_SelectedIndexChanged(object sender, EventArgs e)
        {
            clMantenimientoUsuarios = new MantenimientoUsuarios();
            DataTable accionesXRol = new DataTable();
            chkAccionesRol.Items.Clear();

            if (lstRolesSistema.SelectedIndex != -1 )
            {
                accionesXRol = clMantenimientoUsuarios.obtenerAccionesXRol(int.Parse(lstRolesSistema.SelectedValue));
                llenarAccionesSistema();
            }

            

            for (int i = 0; i < chkAccionesRol.Items.Count; i++)
            {
                for (int j = 0; j < accionesXRol.Rows.Count; j++)
                {
                    if (chkAccionesRol.Items[i].Value.ToString() == accionesXRol.Rows[j]["id_accion"].ToString())
                    {
                        chkAccionesRol.Items[i].Selected = true;
                    }
                }
            }
        }       
        protected void chkAccionesRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<String> seleccionados = new List<string>();
            List<String> noseleccionados = new List<string>();
            MantenimientoUsuarios clMantenimiento = new MantenimientoUsuarios();

            foreach (ListItem item in chkAccionesRol.Items)
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

            clMantenimiento.agregarAccionRol(int.Parse(lstRolesSistema.SelectedValue), seleccionados, noseleccionados);

            seleccionados.Clear();
            noseleccionados.Clear();
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Principal/Menu_Principal.aspx");  
        }
        protected void btnAgregarRol_Click(object sender, EventArgs e)
        {
            if (!txtNombreRol.Text.Equals(string.Empty) && !txtNombreRol.Text.Contains(' '))
            {
                if (clMantenimientoUsuarios.insertarRolAcceso(txtNombreRol.Text))
                {
                    llenarRolesSistema();
                    llenarRolesMantenimiento();
                    txtNombreRol.Text = "";
                }
                else
                {
                    lblMensaje.Text = "Error al insertar";
                }
            }
            else
            {
                lblMensaje.Text = "El nombre del nuevo rol tiene espacios vacios o está vacio";
            }
        }
        protected void btnEliminar_Command(object sender, CommandEventArgs e)
        {
            if (clMantenimientoUsuarios.eliminarRolAcceso(e.CommandArgument.ToString()))
            {
                llenarRolesSistema();
                llenarRolesMantenimiento();
            }
        }
        protected void btnModificar_Command(object sender, CommandEventArgs e)
        {
            DataTable rolSeleccionado = clMantenimientoUsuarios.obtenerNombreRol(e.CommandArgument.ToString());
            lblHiddenIdRol.Text = e.CommandArgument.ToString();
            txtNuevoNombreRol.Text = rolSeleccionado.Rows[0]["nombre"].ToString();
            pnlModificar.Visible = true;
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (clMantenimientoUsuarios.modificarRolAcceso(lblHiddenIdRol.Text,txtNuevoNombreRol.Text))
            {
                pnlModificar.Visible = false;
                txtNuevoNombreRol.Text = "";
                llenarRolesSistema();
                llenarRolesMantenimiento();
            }
        }

        protected void btnAgregarAccion_Click(object sender, EventArgs e)
        {
            if(!txtNombreAccion.Text.Equals(string.Empty) && !txtNombreAccion.Text.Contains(' '))
            {
                if (clMantenimientoUsuarios.insertarAccion(txtNombreAccion.Text,txtDeetalleAccion.Text))
                {
                    llenarAccionesSistema();
                    llenarAccionesMantenimiento();
                    txtNombreAccion.Text = "";
                    txtDeetalleAccion.Text = "";
                }
                else
                {
                    lblMensajeAccion.Text = "Error al insertar";
                }
            }
            else
            {
                lblMensajeAccion.Text = "El nombre de la nueva accion tiene espacios vacios o está vacio";
            }
        }
        protected void btnEliminarAccion_Command(object sender, CommandEventArgs e)
        {
            if (clMantenimientoUsuarios.eliminarAccion(e.CommandArgument.ToString()))
            {
                llenarRolesSistema();
                llenarRolesMantenimiento();
            }
        }
        protected void btnModificarAccion_Command(object sender, CommandEventArgs e)
        {
            DataTable rolSeleccionado = clMantenimientoUsuarios.obtenerDatosAccion(e.CommandArgument.ToString());
            lblHiddenIdAccion.Text = e.CommandArgument.ToString();
            txtNuevoNombreAccion.Text = rolSeleccionado.Rows[0]["nombre_accion"].ToString();
            txtNuevoDetalleAccion.Text = rolSeleccionado.Rows[0]["detalle_accion"].ToString();
            pnlModificarAccion.Visible = true;
        }
        protected void btnModificarAccion_Click(object sender, EventArgs e)
        {
            if (clMantenimientoUsuarios.modificarAccion(lblHiddenIdAccion.Text, txtNuevoNombreAccion.Text,txtNuevoDetalleAccion.Text))
            {
                pnlModificarAccion.Visible = false;
                txtNuevoNombreAccion.Text = "";
                txtNuevoDetalleAccion.Text = "";
                llenarAccionesSistema();
                llenarAccionesMantenimiento();
            }
        }
    }
}