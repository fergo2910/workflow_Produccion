using Lbl_Cotizador_Autos_Web;
using Lbl_Cotizador_Autos_Web.Acceso;
using CotizadorWebAutos.FacturaElectronica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lbl_Cotizado_Autos_Web.ConexionesBD;
using Lbl_Cotizado_Autos_Web.Acceso;

namespace CotizadorWebAutos.Acceso
{
    public partial class Ingreso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Cuando se realizan pruebas, ejecuto esto para que no me tire throw ex por oracle.
            //Lbl_Cotizador_Autos_Web.ConexionesBD.Conexiones a = new Lbl_Cotizador_Autos_Web.ConexionesBD.Conexiones();
            //Oracle.DataAccess.Client.OracleConnection con = a.abrirConexionOracleAcsel();
            //con.Close();
        }
        protected void btnLoginIngresar_Click(object sender, EventArgs e)
        {
            if (txtPasswordUsuario.Text == string.Empty && txtNombreUsuario.Text == string.Empty)
            {
                mostrarNuevoMensaje("Debe ingresar los datos del usuario.");
                return;
            }
            else
            {
                if (txtNombreUsuario.Text == string.Empty)
                {
                    mostrarNuevoMensaje("Debe ingresar el usuario.");
                    return;

                }
                else if (txtPasswordUsuario.Text == string.Empty)
                {
                    mostrarNuevoMensaje("Debe ingresar la contraseña.");
                    return;
                }
            }

            GlobalConection.SetDatosConexion(txtNombreUsuario.Text, txtPasswordUsuario.Text);

            IngresoSistema clIngreso = new IngresoSistema();
            IngresoSistema.informacionUsuario informacionUsuario = new IngresoSistema.informacionUsuario();

            DataTable datosUsuarioLogin = clIngreso.datosAccesoUsuario(txtNombreUsuario.Text, txtPasswordUsuario.Text);
            

            if (datosUsuarioLogin.Rows.Count == 1)
            {
                DataTable rolesUsuario = new DataTable();
                AccesoUsuario clAccesoUsuario = new AccesoUsuario();
                DataTable accionesUsuario = new DataTable();
                //Se llena la clase con los datos basicos del usuario que esta ingresando al sistema
                informacionUsuario.idUsuario = int.Parse(datosUsuarioLogin.Rows[0]["id_usuario"].ToString());
                informacionUsuario.codIntermediario = datosUsuarioLogin.Rows[0]["codigo_intermediario"].ToString();
                informacionUsuario.nombreUsuario = datosUsuarioLogin.Rows[0]["nombres"].ToString();
                informacionUsuario.apellidoUsuario = datosUsuarioLogin.Rows[0]["apellidos"].ToString();
                informacionUsuario.ideProveedor = datosUsuarioLogin.Rows[0]["ideproveedor"].ToString();

                if (datosUsuarioLogin.Rows[0]["usuario_interno"].ToString() == "1")
                {
                    informacionUsuario.usuarioInterno = true;
                }
                else
                {
                    informacionUsuario.usuarioInterno = false;
                }
                
                informacionUsuario.codigoUsuarioRemoto = datosUsuarioLogin.Rows[0]["codigo_usuario_remoto"].ToString();
                informacionUsuario.correoElectronico = datosUsuarioLogin.Rows[0]["correo_electronico"].ToString();
                informacionUsuario.nombreUsuarioUnico = datosUsuarioLogin.Rows[0]["nombre_unico_usuario"].ToString();

                if (datosUsuarioLogin.Rows[0]["numid"].ToString() == string.Empty)
                {
                    informacionUsuario.numId = 0;
                }
                else
                {
                    informacionUsuario.numId = int.Parse(datosUsuarioLogin.Rows[0]["numid"].ToString());
                }
                
                informacionUsuario.dvID = datosUsuarioLogin.Rows[0]["dvid"].ToString();   
            
                //se buscan los roles que tiene asignado el usuario
                rolesUsuario = clAccesoUsuario.obtenerRolesAccesoUsuario(informacionUsuario.idUsuario); 

                Dictionary<int, string> rolesAsignados = new Dictionary<int,string>();

                //Se recorren los roles asignados al usuario 
                for (int i = 0; i < rolesUsuario.Rows.Count; i++)
			    {
			        rolesAsignados.Add(int.Parse(rolesUsuario.Rows[i]["id_rol"].ToString()), rolesUsuario.Rows[i]["nombre"].ToString());
                    informacionUsuario.rolesAsignados.Add(rolesUsuario.Rows[i]["nombre"].ToString());
			    }

                //Se buscan las acciones permitidas que tiene el usuario basado en los roles que tiene asignados.
                accionesUsuario = clAccesoUsuario.obtenerAccionesUsuario(rolesAsignados);

                for (int i = 0; i < accionesUsuario.Rows.Count; i++)
                {
                    informacionUsuario.accionesPermitidas.Add(accionesUsuario.Rows[i]["nombre_accion"].ToString());
                }
                
                Session["informacionUsuario"] = informacionUsuario;

                if (informacionUsuario.accionesPermitidas.Count == 1)
                {
                    if (informacionUsuario.accionesPermitidas.Contains("Descargar_Factura_Individual") ||
                        informacionUsuario.accionesPermitidas.Contains("Descargar_Facturas") ||
                        informacionUsuario.accionesPermitidas.Contains("Descargar_Facturas_X_Intermediario"))
                    {
                        Response.Redirect("../FacturaElectronica/FacturaElectronica.aspx");
                        return;
                    }
                    if (informacionUsuario.accionesPermitidas.Contains("Consultar_Cupones"))
                    {
                        Response.Redirect("../GM_Control_Cupones/Control_Cupones.aspx");
                        return;
                    }

                    Response.Redirect("../Principal/Menu_Principal.aspx");
                }
                else
                {
                     Response.Redirect("../Principal/Menu_Principal.aspx");
                }
                   
            }
            else
            {
                mostrarNuevoMensaje("Usuario o contraseña incorrecto.");
            }
        }

        private void mostrarMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", "alert('" + mensaje + "');", true);
        }

        private void mostrarNuevoMensaje(string mensaje2) {

            MostrarMensaje.Text = mensaje2;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { MostrandoMensajeaUsuario(); });", true);

        }

        
    }
}