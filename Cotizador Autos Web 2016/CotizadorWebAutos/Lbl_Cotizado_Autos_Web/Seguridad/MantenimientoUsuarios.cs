using Lbl_Cotizador_Autos_Web.ConexionesBD;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.Seguridad
{
    public class MantenimientoUsuarios
    {
        public DataTable obtenerAccionesDetalle()
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select nombre_accion, detalle_accion from acciones_cotizador where estado = 1 and visible = 1";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerRolesAccesoXUsuario(int idUsuario)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from usuario_rol_acceso where estado = 1 and id_usuario = " + idUsuario;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }


            return datos;
        }
        public DataTable obtenerAccionesXRol(int idRol)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from acciones_rol where estado = 1 and id_rol = " + idRol;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }


            return datos;
        }
        public DataTable obtenerDetalleAccionesXRol(int idRol)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT ac.nombre_accion FROM acciones_rol ar, acciones_cotizador ac " +
                            " where ar.id_accion = ac.id_accion and ar.estado = true " +
                            " and ar.id_rol = "+ idRol;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }


            return datos;
        }
        public DataTable obtenerRolesDescuentoXUsuario(int idUsuario)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT usr.id_plan_web, pl.nombre nombre_plan, usr.id_usuario, usr.id_rol, rol.nombre nombre_rol,"
                             + " rol.porcentaje_descuento_maximo FROM usuario_rol_descuento usr, rol_descuento rol, planes_web pl"
                             + " where usr.id_rol = rol.id_rol and usr.id_plan_web = pl.id_plan_web"
                             + " and usr.estado = 1 and usr.id_usuario = " + idUsuario;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerPlanesRolesXUsuario(int idUsuario)
        {
            DataTable datos = new DataTable();
            return datos;
        }
        public DataTable obtenerRolesAccesoConRolesSuperUsuario(string rol)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            string query = "";
            if (rol.Equals("SUPER_USUARIO"))
            {
                query = "select * from rol_acceso where estado = 1";
            }
            else if (rol.Equals("ADMINISTRADOR_DESCUENTOS"))
            {
                query = "select * from rol_acceso where estado = 1 and nombre NOT IN ('SUPER_USUARIO')";
            }

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerNombreRol(string idRol)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = " SELECT nombre FROM rol_acceso where id_rol = " + idRol;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerDatosAccion(string idAccion)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = " SELECT nombre_accion,detalle_accion FROM acciones_cotizador where id_accion = " + idAccion;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public bool insertarRolAcceso(string NombreRol)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();
                MySqlCommand command = conexion.CreateCommand();
                string query = " INSERT INTO rol_acceso(nombre,fecha_creacion,estado) " +
                               " VALUES ('" + NombreRol + "',NOW(),TRUE)";
                command.CommandText = query;
                command.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
                throw ex;
            } 
        }
        public bool insertarAccion(string NombreAcción,string detalleAccion)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();
                MySqlCommand command = conexion.CreateCommand();
                string query = " INSERT INTO acciones_cotizador(nombre_accion,estado,detalle_accion,visible) " +
                               " VALUES ('" + NombreAcción + "',TRUE,'" + detalleAccion + "',TRUE)";
                command.CommandText = query;
                command.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
                throw ex;
            } 
        }
        public bool eliminarRolAcceso(string idRol)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();
                MySqlCommand command = conexion.CreateCommand();
                string query = " DELETE FROM rol_acceso WHERE id_rol=" + idRol;
                command.CommandText = query;
                command.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
                throw ex;
            }
        }
        public bool eliminarAccion(string idAccion)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();
                MySqlCommand command = conexion.CreateCommand();
                string query = " DELETE FROM acciones_cotizador WHERE id_accion=" + idAccion;
                command.CommandText = query;
                command.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
                throw ex;
            }
        }
        public bool modificarRolAcceso(string idRol, string NuevoNombre)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();
                MySqlCommand command = conexion.CreateCommand();
                string query = " UPDATE rol_acceso set nombre = '" + NuevoNombre + "' WHERE id_rol=" + idRol;
                command.CommandText = query;
                command.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
                throw ex;
            }
        }
        public bool modificarAccion(string idAccion, string NuevoNombre, string NuevoDetalle)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();
                MySqlCommand command = conexion.CreateCommand();
                string query = " UPDATE acciones_cotizador set nombre_accion = '" + NuevoNombre + "', detalle_accion = '" 
                    + NuevoDetalle + "'  WHERE id_accion=" + idAccion;
                command.CommandText = query;
                command.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                return false;
                throw ex;
            }
        }
        public DataTable obtenerRolesAccesoSuperUsuario()
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from rol_acceso where estado = 1";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerTodosRolesAccesoSuperUsuario()
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from rol_acceso";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerTodasAccionesSuperUsuario()
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from acciones_cotizador";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerRolesAcceso()
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from rol_acceso where estado = 1 and nombre NOT IN ('ADMINISTRADOR_DESCUENTOS', 'SUPER_USUARIO')";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerAccionesSistema()
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from acciones_cotizador where estado = 1;";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerRolesDescuento()
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT id_rol, concat(nombre, ' ', porcentaje_descuento_maximo, '%') nombre "
                    + "  FROM rol_descuento where estado = 1;";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }          

            return datos;
        }
        public DataTable obtenerPlanesOpcionales()
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from planes_web where estado = 1 and cod_intermediario = '000000'";                    

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerPermisosSistema()
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from permisos where estado = 1";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerPlanesOpcionalesXUsuario(int idUsuario)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from plan_usuario where estado = 1 and id_usuario = " + idUsuario;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerPermisosXUsuario(int idUsuario)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from permisos_usuario where estado = 1 and id_usuario = " + idUsuario;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerPlanesConDescuento()
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT * from planes_web where estado = 1 and tiene_descuentos = 1";                   

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerPlanesRoles(int idUsuario)
        {
            DataTable datos = new DataTable();
            return datos;
        }
        public void agregarRolAcceso(int idUsuario, List<string> seleccionados, List<string> noSeleccionados)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();

                string query = string.Empty;

                foreach (string sel in seleccionados)
                {
                    if (existeRolAccesoUsuario(idUsuario, int.Parse(sel)))
                    {
                        query = "update usuario_rol_acceso set estado = TRUE"
                            + " where id_usuario = " + idUsuario + " and id_rol = " + int.Parse(sel);

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        query = "insert into usuario_rol_acceso "
                            + " values (" + idUsuario + ", " + int.Parse(sel) + ", TRUE)";

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }                  
                }

                foreach (string noSel in noSeleccionados)
                {
                    if (existeRolAccesoUsuario(idUsuario, int.Parse(noSel)))
                    {
                        query = "update usuario_rol_acceso set estado = FALSE"
                           + " where id_usuario = " + idUsuario + " and id_rol = " + int.Parse(noSel);

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                }                

                conexion.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }            
        }
        public void agregarAccionRol(int idRol, List<string> seleccionados, List<string> noSeleccionados)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();

                string query = string.Empty;

                foreach (string sel in seleccionados)
                {
                    if (existeAccionRol(idRol, int.Parse(sel)))
                    {
                        query = "update acciones_rol set estado = TRUE"
                            + " where id_rol = " + idRol + " and id_accion = " + int.Parse(sel);

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        query = "insert into acciones_rol "
                            + " values (" + idRol + ", " + int.Parse(sel) + ", TRUE)";

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                }

                foreach (string noSel in noSeleccionados)
                {
                    if (existeAccionRol(idRol, int.Parse(noSel)))
                    {
                        query = "update acciones_rol set estado = FALSE"
                           + " where id_rol = " + idRol + " and id_accion = " + int.Parse(noSel);

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                }

                conexion.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }
        public void agregarPermisoUsuario(int idUsuario, List<string> seleccionados, List<string> noSeleccionados)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();

                string query = string.Empty;

                foreach (string sel in seleccionados)
                {
                    if (existePermisoUsuario(idUsuario, int.Parse(sel)))
                    {
                        query = "update permisos_usuario set estado = TRUE"
                            + " where id_usuario = " + idUsuario + " and id_permiso = " + int.Parse(sel);

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        query = "insert into permisos_usuario (id_usuario, id_permiso, estado)"
                            + " values (" + idUsuario + ", " + int.Parse(sel) + ", TRUE)";

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                }

                foreach (string noSel in noSeleccionados)
                {
                    if (existeRolAccesoUsuario(idUsuario, int.Parse(noSel)))
                    {
                        query = "update permisos_usuario set estado = FALSE"
                           + " where id_usuario = " + idUsuario + " and id_permiso = " + int.Parse(noSel);

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                }

                conexion.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }
        public void agregarPlanOpcional(int idUsuario, List<string> seleccionados, List<string> noSeleccionados)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();

                string query = string.Empty;

                foreach (string sel in seleccionados)
                {
                    if (existePlanUsuario(idUsuario, int.Parse(sel)))
                    {
                        query = "update plan_usuario set estado = TRUE"
                            + " where id_usuario = " + idUsuario + " and id_plan_web = " + int.Parse(sel);

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        query = "insert into plan_usuario "
                            + " values (" + int.Parse(sel) + ", " + idUsuario + ", TRUE)";

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                }

                foreach (string noSel in noSeleccionados)
                {
                    if (existePlanUsuario(idUsuario, int.Parse(noSel)))
                    {
                        query = "update plan_usuario set estado = FALSE"
                           + " where id_usuario = " + idUsuario + " and id_plan_web = " + int.Parse(noSel);

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                }

                conexion.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }
        public bool existeRolAccesoUsuario(int idUsuario, int idRol)
        {
            bool existe = false;

            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from usuario_rol_acceso where id_usuario = " 
                + idUsuario + " and id_rol = " + idRol;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    existe = true;   
                }

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }


            return existe;
        }
        public bool existeAccionRol(int idRol, int idAccion)
        {
            bool existe = false;

            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from acciones_rol where id_rol = "
                + idRol + " and id_accion = " + idAccion;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    existe = true;
                }

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }


            return existe;
        }
        public bool existePermisoUsuario(int idUsuario, int idPermiso)
        {
            bool existe = false;

            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from permisos_usuario where id_usuario = "
                + idUsuario + " and id_permiso = " + idPermiso;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    existe = true;
                }

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }


            return existe;
        }
        public bool existePlanUsuario(int idUsuario, int idPlan)
        {
            bool existe = false;

            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from plan_usuario where id_usuario = "
                + idUsuario + " and id_plan_web = " + idPlan;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    existe = true;
                }

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }


            return existe;
        }
        public void agregarRolDescuento(int idUsuario, int idRolDescuento, int idPlan, string operacion)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();

                string query = "" + idUsuario;

                if (existeRolDescuentoUsuario(idUsuario, idRolDescuento, idPlan))
                {
                    if (operacion == "Agregar")
                    {
                        query = "update usuario_rol_descuento set estado = TRUE"
                           + " where id_usuario = " + idUsuario + " and id_rol = " + idRolDescuento + " and id_plan_web = " + idPlan;

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        query = "update usuario_rol_descuento set estado = FALSE"
                            + " where id_usuario = " + idUsuario + " and id_rol = " + idRolDescuento + " and id_plan_web = " + idPlan;

                        command.CommandText = query;
                        command.ExecuteNonQuery();

                    }
                }
                else
                {
                    query = "insert into usuario_rol_descuento values (" + idPlan + ", " + idUsuario + ", " + idRolDescuento + " ,TRUE)";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }                

                conexion.Close();
            }
            catch (MySqlException ex)
            {

                throw ex;
            }
        }
        public bool existeRolDescuentoUsuario(int idUsuario, int idRol, int idPlan)
        {
            bool existe = false;

            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from usuario_rol_descuento where id_usuario = "
                + idUsuario + " and id_rol = " + idRol + "  and id_plan_web = " + idPlan;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    existe = true;
                }

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }


            return existe;
        }
        public void quitarRolAcceso(int idUsuario)
        {
            
        }
        public void quitarRolDescuento(int idUsuario)
        {
            
        }
        public void actualizarDatosGenerales(int idUsuario)
        {
            
        }
        public DataTable obtenerUsuariosSistema()
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT * FROM usuario ORDER BY nombre_unico_usuario";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public void borrarUsuario(int idUsuario)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();

                string query = "update usuario set estado = FALSE where id_usuario = " + idUsuario;

                command.CommandText = query;
                command.ExecuteNonQuery();               

                conexion.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }   
        }
        public void habilitarUsuario(int idUsuario)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();

                string query = "update usuario set estado = TRUE where id_usuario = " + idUsuario;

                command.CommandText = query;
                command.ExecuteNonQuery();

                conexion.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }   
        }
        public DataTable ListaCodigoIntermediario()
        {
            DataTable datos = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter("select CODINTER||'-'||NOMCOMERCIAL AS CODINTER from Intermediario ORDER BY CODINTER", conexionOracle);

            adapter.Fill(datos);
            conexionOracle.Close();
            return datos;
        }
        public DataTable obtenerUsuariosSistemanPorNombre(string Nombre = null)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT * FROM usuario WHERE nombre_unico_usuario LIKE '%" + Nombre + "%' ORDER BY nombre_unico_usuario";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerUsuariosSistemanPoriNTERMEDIARIO(string intermediario = null)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT * FROM usuario WHERE codigo_intermediario = '" + intermediario + "' ORDER BY nombre_unico_usuario";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
                throw ex;
            }

            return datos;
        }
        public DataTable ObtenerIntermediarios(string pIdUsuario)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT codinter codigo_intermediario, nomcomercial nombre " +
                            " FROM intermediarios " + 
                            " WHERE codinter not in (select codinter from intermediario_x_usuario where id_usuario = " + pIdUsuario + " and estado = TRUE) " +
                            "   AND estado = TRUE" +
                            " ORDER BY nomcomercial";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
                throw ex;
            }

            return datos;
        }
        public void MantenimientoIntermediario(string pIdUsuario, string pCodInter, string pAccion)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();

                string query = string.Empty;
                if (pAccion == "ELIMINAR")
                    query = "update intermediario_x_usuario set estado = FALSE"
                            + " where id_usuario = " + pIdUsuario + " and CodInter = '" + pCodInter + "'";
                else
                {
                    query = "Select 1 from intermediario_x_usuario where id_usuario = " + pIdUsuario +
                                " and codinter = '" + pCodInter + "'";
                    command.CommandText = query;
                    MySqlDataReader Reader;
                    Reader = command.ExecuteReader();
                    if (Reader.Read())                    
                        query = "update intermediario_x_usuario set estado = TRUE"
                            + " where id_usuario = " + pIdUsuario + " and CodInter = '" + pCodInter + "'";                                          
                    else
                        query = "insert into intermediario_x_usuario (id_usuario, codinter, estado) "
                            + " values( " + pIdUsuario + " ,'" + pCodInter + "', TRUE)";
                    Reader.Close();
                }
                command.CommandText = query;
                command.ExecuteNonQuery();
                conexion.Close();
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
        }
        public DataTable ObtenerIntermediariosAsignados(string pIdUsuario)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT i.codinter codigo_intermediario, i.nomcomercial nombre " +
                            " FROM intermediarios i, intermediario_x_usuario ixu " +
                            " WHERE i.codinter = ixu.codinter" +
                            "   AND i.estado = TRUE" +
                            "   AND ixu.id_usuario = " + pIdUsuario +
                            "   AND ixu.estado = TRUE" +
                            " ORDER BY i.codinter";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                datos = null;
                throw ex;
            }

            return datos;
        }
    }
}
