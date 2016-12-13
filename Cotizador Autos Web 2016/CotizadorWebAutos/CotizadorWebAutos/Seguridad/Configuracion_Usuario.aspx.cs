using Lbl_Cotizado_Autos_Web.Acceso;
using Lbl_Cotizado_Autos_Web.Comunes;
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
    public partial class Configuracion_Usuario : System.Web.UI.Page
    {
        IngresoSistema.informacionUsuario informacionUsuario;

        public int idUsuarioModificar = 0;
        DataTable rolesAccesoDisponibles;
        DataTable permisosDisponibles;
        DataTable rolesDescuentoDisponibles;
        DataTable planesConDescuentoDisponibles;
        DataTable planesOpcionalesDisponibles;
        DataTable dtIntermediario;
        DataTable rolesAccesoUsuario;
        MantenimientoUsuarios clMantUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();
                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];
                clMantUsuario = new MantenimientoUsuarios();

                idUsuarioModificar = Convert.ToInt32(Request.QueryString["editUser"]);
                rolesAccesoUsuario = clMantUsuario.obtenerRolesAccesoXUsuario(idUsuarioModificar);

                if (!IsPostBack)
                {
                    idUsuarioModificar = Convert.ToInt32(Request.QueryString["editUser"]);

                    rolesAccesoUsuario = new DataTable();
                    DataTable rolesDescuentoUsuario = new DataTable();
                    DataTable planesOpcionalesUsuario = new DataTable();
                    DataTable permisosUsuario = new DataTable();

                    rolesAccesoDisponibles = new DataTable();
                    rolesDescuentoDisponibles = new DataTable();

                    clMantUsuario = new MantenimientoUsuarios();

                    rolesAccesoUsuario = clMantUsuario.obtenerRolesAccesoXUsuario(idUsuarioModificar);
                    if (informacionUsuario.rolesAsignados.Contains("ADMINISTRADOR_DESCUENTOS") || informacionUsuario.rolesAsignados.Contains("SUPER_USUARIO"))
                    {
                        //Si es super usuario o administrador de descuentos puede ver el panel de descuentos
                        //y también puede ver su rol para poder asignar a otros administradores_descuentos/super_usuarios
                        pnlDescuento.Visible = true;
                        if (informacionUsuario.rolesAsignados.Contains("SUPER_USUARIO"))
                        {
                            rolesAccesoDisponibles = clMantUsuario.obtenerRolesAccesoConRolesSuperUsuario("SUPER_USUARIO");
                        }
                        else if (informacionUsuario.rolesAsignados.Contains("ADMINISTRADOR_DESCUENTOS"))
                        {
                            rolesAccesoDisponibles = clMantUsuario.obtenerRolesAccesoConRolesSuperUsuario("ADMINISTRADOR_DESCUENTOS");
                        }
                    }
                    else
                    {
                        rolesAccesoDisponibles = clMantUsuario.obtenerRolesAcceso();
                    }

                    rolesDescuentoUsuario = clMantUsuario.obtenerRolesDescuentoXUsuario(idUsuarioModificar);
                    rolesDescuentoDisponibles = clMantUsuario.obtenerRolesDescuento();

                    planesOpcionalesDisponibles = clMantUsuario.obtenerPlanesOpcionales();
                    planesOpcionalesUsuario = clMantUsuario.obtenerPlanesOpcionalesXUsuario(idUsuarioModificar);

                    permisosDisponibles = clMantUsuario.obtenerPermisosSistema();
                    permisosUsuario = clMantUsuario.obtenerPermisosXUsuario(idUsuarioModificar);

                    planesConDescuentoDisponibles = clMantUsuario.obtenerPlanesConDescuento();

                    grvRolesAsignados.DataSource = rolesAccesoDisponibles;
                    grvRolesAsignados.DataBind();

                    chkPermisosUsuario.DataSource = permisosDisponibles;
                    chkPermisosUsuario.DataTextField = "nombre";
                    chkPermisosUsuario.DataValueField = "id_permiso";
                    chkPermisosUsuario.DataBind();

                    cmbRolesDescuento.DataSource = rolesDescuentoDisponibles;
                    cmbRolesDescuento.DataTextField = "nombre";
                    cmbRolesDescuento.DataValueField = "id_rol";
                    cmbRolesDescuento.DataBind();

                    cmbPlanesDescuento.DataSource = planesConDescuentoDisponibles;
                    cmbPlanesDescuento.DataTextField = "nombre";
                    cmbPlanesDescuento.DataValueField = "id_plan_web";
                    cmbPlanesDescuento.DataBind();

                    LLenarComboIntermediarios();

                    LlenarGridIntermediarios(idUsuarioModificar.ToString());

                    for (int i = 0; i < chkPermisosUsuario.Items.Count; i++)
                    {
                        for (int j = 0; j < permisosUsuario.Rows.Count; j++)
                        {
                            if (chkPermisosUsuario.Items[i].Value.ToString() == permisosUsuario.Rows[j]["id_permiso"].ToString())
                            {
                                chkPermisosUsuario.Items[i].Selected = true;
                            }
                        }
                    }

                    grvRolesDescuento.DataSource = rolesDescuentoUsuario;
                    grvRolesDescuento.DataBind();

                    llenarDatosUsuario();
                }
            }
        }
        private void LLenarComboIntermediarios()
        {
            MantenimientoUsuarios clMantUsuario = new MantenimientoUsuarios();
            ddlIntermediario.DataSource = null;
            dtIntermediario = clMantUsuario.ObtenerIntermediarios(idUsuarioModificar.ToString());
            ddlIntermediario.DataSource = dtIntermediario;
            ddlIntermediario.DataTextField = "nombre";
            ddlIntermediario.DataValueField = "codigo_intermediario";
            ddlIntermediario.DataBind();
        }
        protected void btnGuardarRoles_Click(object sender, EventArgs e)
        {
            MantenimientoUsuarios clMantenimiento = new MantenimientoUsuarios();

            if (grvRolesDescuento.Rows.Count == 1)
            {
                mostrarMensaje("No puede agregar mas de un Rol de Descuento por usuario.");
            }
            else
            {
                clMantenimiento.agregarRolDescuento(idUsuarioModificar, int.Parse(cmbRolesDescuento.SelectedValue), int.Parse(cmbPlanesDescuento.SelectedValue), "Agregar");

                DataTable rolesDescuentoUsuario = new DataTable();

                rolesDescuentoUsuario = clMantenimiento.obtenerRolesDescuentoXUsuario(idUsuarioModificar);
                grvRolesDescuento.DataSource = rolesDescuentoUsuario;

                grvRolesDescuento.DataBind();
            }
        }
        protected void chkListRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<String> seleccionados = new List<string>();
            List<String> noseleccionados = new List<string>();
            MantenimientoUsuarios clMantenimiento = new MantenimientoUsuarios();

            //foreach (ListItem item in chkListRoles.Items)
            //{
            //    if (item.Selected)
            //    {
            //        seleccionados.Add(item.Value);
            //    }
            //    else
            //    {
            //        noseleccionados.Add(item.Value);
            //    }
            //}

            clMantenimiento.agregarRolAcceso(idUsuarioModificar, seleccionados, noseleccionados);

            seleccionados.Clear();
            noseleccionados.Clear();
        }
        protected void grvRolesDescuento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView listadoRoles = (GridView)sender;
            MantenimientoUsuarios clMantenimiento = new MantenimientoUsuarios();

            if (e.CommandName == "eliminar")
            {
                int idPlan = int.Parse(listadoRoles.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text);
                int IdRol = int.Parse(listadoRoles.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);

                clMantenimiento.agregarRolDescuento(idUsuarioModificar, IdRol, idPlan, e.CommandName);
            }

            DataTable rolesDescuentoUsuario = new DataTable();

            rolesDescuentoUsuario = clMantenimiento.obtenerRolesDescuentoXUsuario(idUsuarioModificar);
            grvRolesDescuento.DataSource = rolesDescuentoUsuario;

            grvRolesDescuento.DataBind();
        }
        private void mostrarMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", "alert('" + mensaje + "');", true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        private void mostrarMensaje(string mensaje, string url)
        {
            string script = "window.onload = function(){ alert('";
            script += mensaje;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }
        protected void chkPlanesOpcionales_SelectedIndexChanged(object sender, EventArgs e)
        {            
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Seguridad/Mantenimiento_Usuarios.aspx");                    
        }
        protected void gvIntermediarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            MantenimientoUsuarios clMantenimiento = new MantenimientoUsuarios();
            GridView Intermediarios = (GridView)sender;

            if (e.CommandName == "EliminaInter")
            {
                string sCodInter = Intermediarios.Rows[Convert.ToInt32(e.CommandArgument.ToString())].Cells[1].Text;

                clMantenimiento.MantenimientoIntermediario(idUsuarioModificar.ToString(), sCodInter, "ELIMINAR");
            }

            LlenarGridIntermediarios(idUsuarioModificar.ToString());
            LLenarComboIntermediarios();
        }
        private void LlenarGridIntermediarios(string pIdUsuario)
        {
            MantenimientoUsuarios clMantenimiento = new MantenimientoUsuarios();
            DataTable dtIntermediario = new DataTable();
            dtIntermediario = clMantenimiento.ObtenerIntermediariosAsignados(pIdUsuario);
            gvIntermediarios.DataSource = dtIntermediario;
            gvIntermediarios.DataBind();
            //UpdatePanel1.Update();
        }
        protected void btNuevoIntermediario_Click(object sender, EventArgs e)
        {
            MantenimientoUsuarios clMantenimiento = new MantenimientoUsuarios();
            clMantenimiento.MantenimientoIntermediario(idUsuarioModificar.ToString(),ddlIntermediario.SelectedValue,"NUEVO");
                
            // Se llena de nuevo el data grid de intermediarios asignados
            LlenarGridIntermediarios(idUsuarioModificar.ToString());

            // Se llena de nuevo el combo de intermediarios
            LLenarComboIntermediarios();
        }
        private void llenarDatosUsuario()
        {
            Varias clVarias = new Varias();
            DataTable datosUsuario = new DataTable();

            datosUsuario = clVarias.obtenerInformacionUsuarioLogueado(idUsuarioModificar);

            if (datosUsuario.Rows.Count == 1)
            {
                txtNombres.Text = datosUsuario.Rows[0]["nombres"].ToString();
                txtApellidos.Text = datosUsuario.Rows[0]["apellidos"].ToString();

                DateTime fechaCreacion = Convert.ToDateTime(datosUsuario.Rows[0]["fecha_creacion_usuario"].ToString());

                txtFechaCreacion.Text = fechaCreacion.ToString("dd-MM-yyyy");                

                if (datosUsuario.Rows[0]["usuario_interno"].ToString() == "1")
                {
                    chkUsuarioInterno.Checked = true;
                }
                else
                {
                    chkUsuarioInterno.Checked = false;
                }

                txtTelefono.Text = datosUsuario.Rows[0]["telefono"].ToString();
                txtCorreoElectronico.Text = datosUsuario.Rows[0]["correo_electronico"].ToString();

                ViewState.Add("Correo_Usuario", txtCorreoElectronico.Text);
            }
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            Varias clVarias = new Varias();
            AccesoUsuario clAccesoUsuario = new AccesoUsuario();
            DataTable datosUsuario = new DataTable();

            datosUsuario = clVarias.obtenerInformacionUsuarioLogueado(idUsuarioModificar);

            if (txtCorreoElectronico.Text == string.Empty || txtTelefono.Text == string.Empty)
            {
                mostrarMensaje("Debe ingresar los datos antes de actualizar el usuario.");
            }
            else
            {
                if (txtTelefono.Text.Length != 8)
                {
                    mostrarMensaje("Número de teléfono invalido.");
                }
                else
                {
                    if (txtCorreoElectronico.Text != ViewState["Correo_Usuario"].ToString())
                    {
                        if (!clAccesoUsuario.existeUsuarioXCorreo(txtCorreoElectronico.Text))
                        {
                            clAccesoUsuario.actualizarDatosUsuario(txtTelefono.Text, txtCorreoElectronico.Text, idUsuarioModificar);
                        }
                        else
                        {
                            mostrarMensaje("El correo electrónico ingresado ya esta siendo utilizado.");
                        }
                    }
                    else
                    {
                        clAccesoUsuario.actualizarDatosUsuario(txtTelefono.Text, txtCorreoElectronico.Text, idUsuarioModificar);
                    }
                }                
            }            
        }
        protected void chkPermisosUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<String> seleccionados = new List<string>();
            List<String> noseleccionados = new List<string>();
            MantenimientoUsuarios clMantenimiento = new MantenimientoUsuarios();

            foreach (ListItem item in chkPermisosUsuario.Items)
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

            clMantenimiento.agregarPermisoUsuario(idUsuarioModificar, seleccionados, noseleccionados);

            seleccionados.Clear();
            noseleccionados.Clear();
        }
        protected void btTodosIntermediarios_Click(object sender, EventArgs e)
        {
            DataTable DatosIntermediarios = new DataTable();
            MantenimientoUsuarios clMantenimiento = new MantenimientoUsuarios();
            DatosIntermediarios = clMantenimiento.ObtenerIntermediarios(idUsuarioModificar.ToString());
            for (int i=0; i < DatosIntermediarios.Rows.Count; i++)
            {
                clMantenimiento.MantenimientoIntermediario(idUsuarioModificar.ToString(), DatosIntermediarios.Rows[i].ItemArray[0].ToString(), "NUEVO");
            }

            // Se llena de nuevo el data grid de intermediarios asignados           
            LlenarGridIntermediarios(idUsuarioModificar.ToString());

            // Se llena de nuevo el combo de intermediarios
            LLenarComboIntermediarios();           
        }
        protected void gvIntermediarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LlenarGridIntermediarios(idUsuarioModificar.ToString());

            gvIntermediarios.PageIndex = e.NewPageIndex;
            gvIntermediarios.DataBind();
        }
        protected void grvRolesDescuento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[4].Visible = false;
        }
        protected void chkRol_CheckedChanged(object sender, EventArgs e)
        {
            List<String> seleccionados = new List<string>();
            List<String> noseleccionados = new List<string>();
            MantenimientoUsuarios clMantenimiento = new MantenimientoUsuarios();

            for (int i = 0; i < grvRolesAsignados.Rows.Count; i++)
			{
                CheckBox chk = (CheckBox)grvRolesAsignados.Rows[i].FindControl("chkRol");

                if (chk.Checked)
                {
                    seleccionados.Add(grvRolesAsignados.Rows[i].Cells[1].Text);

                }
                else
                {
                    noseleccionados.Add(grvRolesAsignados.Rows[i].Cells[1].Text);
                }
			}            

            clMantenimiento.agregarRolAcceso(idUsuarioModificar, seleccionados, noseleccionados);

            seleccionados.Clear();
            noseleccionados.Clear();
        }
        protected void grvRolesAsignados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkReq = (CheckBox)e.Row.FindControl("chkRol");

                DataRow[] idRol = rolesAccesoUsuario.Select("id_rol = " + int.Parse(e.Row.Cells[1].Text));

                if (idRol.Count() == 1)
                {
                    chkReq.Checked = true;
                }
            }

            e.Row.Cells[1].Visible = false;
        }
        protected void verDetalleAccion(Object sender, CommandEventArgs e)
        {            
            MantenimientoUsuarios clMantenimientoUsuario = new MantenimientoUsuarios();
            int id_rol = int.Parse(e.CommandArgument.ToString());

            lstDetalleAcciones.DataSource = clMantenimientoUsuario.obtenerDetalleAccionesXRol(id_rol);
            lstDetalleAcciones.DataValueField = "nombre_accion";
            lstDetalleAcciones.DataTextField = "nombre_accion";
            lstDetalleAcciones.DataBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { detalleAccion(); });", true);
        }

        protected void btnVerAcciones_Click(object sender, EventArgs e)
        {
            DataTable acciones_detalle = clMantUsuario.obtenerAccionesDetalle();
            if(acciones_detalle!=null)
            {
                grvAccesoDetalle.DataSource = acciones_detalle;
                grvAccesoDetalle.DataBind();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { MostrarAccesos(); });", true);
        }
    }
}