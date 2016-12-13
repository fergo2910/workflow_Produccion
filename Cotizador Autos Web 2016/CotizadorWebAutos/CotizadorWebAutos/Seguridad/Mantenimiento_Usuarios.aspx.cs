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
    public partial class Mantenimiento_Usuarios : System.Web.UI.Page
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
                    DataTable datosUsuarios = new DataTable();
                    DataTable codigosintermediarios = new DataTable();
                    MantenimientoUsuarios clUsuarios = new MantenimientoUsuarios();

                    datosUsuarios = clUsuarios.obtenerUsuariosSistema();

                    grvUsuariosSistema.DataSource = datosUsuarios;
                    grvUsuariosSistema.DataBind();

                    codigosintermediarios = clUsuarios.ListaCodigoIntermediario();
                    ddlcodinter.DataSource = codigosintermediarios;
                    ddlcodinter.DataTextField = "CODINTER";
                    ddlcodinter.DataValueField = "CODINTER";
                    ddlcodinter.DataBind();
                    ddlcodinter.Items.Add("Todos");
                }
            }
        }
        protected void grvUsuariosSistema_RowCommand(object sender, GridViewCommandEventArgs e)
        { 
            GridView listadoUsuarios = (GridView)sender;
            int idUsuarioSeleccionado = 0;

            switch (e.CommandName)
            {
                case "Configurar":
                    idUsuarioSeleccionado = int.Parse(listadoUsuarios.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);
                    Response.Redirect("../Seguridad/Configuracion_Usuario.aspx?editUser=" + idUsuarioSeleccionado);                    
                    break; 
                default:
                    break;
            }
        }
        protected void borrarUsuario(Object sender, CommandEventArgs e)
        {
            int idUsuarioSeleccionado = int.Parse(e.CommandArgument.ToString());

            MantenimientoUsuarios clMantenimiento = new MantenimientoUsuarios();
            clMantenimiento.borrarUsuario(idUsuarioSeleccionado);

            grvUsuariosSistema.DataSource = clMantenimiento.obtenerUsuariosSistema();
            grvUsuariosSistema.DataBind();
        }
        protected void habilitarUsuario(Object sender, CommandEventArgs e)
        {
            int idUsuarioSeleccionado = int.Parse(e.CommandArgument.ToString());

            MantenimientoUsuarios clMantenimiento = new MantenimientoUsuarios();
            clMantenimiento.habilitarUsuario(idUsuarioSeleccionado);

            grvUsuariosSistema.DataSource = clMantenimiento.obtenerUsuariosSistema();
            grvUsuariosSistema.DataBind();
        }       
        protected void grvUsuariosSistema_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnBorrar = (LinkButton)e.Row.FindControl("btnBorrar");
                LinkButton btnHabilitar = (LinkButton)e.Row.FindControl("btnHabilitar");

                if (e.Row.Cells[6].Text == "0")
                {
                    btnBorrar.Visible = false;
                }
                else
                {
                    btnHabilitar.Visible = false;
                }                
            }
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[6].Visible = false;
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Principal/Menu_Principal.aspx");  
        }
        protected void txtbusquedaNombre_TextChanged(object sender, EventArgs e)
        {
            DataTable datosUsuarios = new DataTable();
            MantenimientoUsuarios clUsuarios = new MantenimientoUsuarios();
            if (!string.IsNullOrEmpty(txtbusquedaNombre.Text))
            {
                datosUsuarios = clUsuarios.obtenerUsuariosSistemanPorNombre(txtbusquedaNombre.Text);
                grvUsuariosSistema.DataSource = datosUsuarios;
                grvUsuariosSistema.DataBind();
            }
            else if (string.IsNullOrEmpty(txtbusquedaNombre.Text))
            {

                datosUsuarios = clUsuarios.obtenerUsuariosSistema();

                grvUsuariosSistema.DataSource = datosUsuarios;
                grvUsuariosSistema.DataBind();
            }
        }
        protected void ddlcodinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable datosUsuarios = new DataTable();
            MantenimientoUsuarios clUsuarios = new MantenimientoUsuarios();

            if (ddlcodinter.SelectedValue.Equals("Todos"))
            {
                datosUsuarios = clUsuarios.obtenerUsuariosSistema();

                grvUsuariosSistema.DataSource = datosUsuarios;
                grvUsuariosSistema.DataBind();                
            }
            else {

                string intermediario = ddlcodinter.SelectedValue;

                string[] words = intermediario.Split('-');

                string Codinter = words[0];
                string Nombre = words[1];

                datosUsuarios = clUsuarios.obtenerUsuariosSistemanPoriNTERMEDIARIO(Codinter);

                grvUsuariosSistema.DataSource = datosUsuarios;
                grvUsuariosSistema.DataBind();            
            }
        }
    }
}