using Lbl_Cotizado_Autos_Web.Comunes;
using Lbl_Cotizado_Autos_Web.Seguridad;
using Lbl_Cotizador_Autos_Web.ConexionesBD;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.Acceso
{
    public class AccesoUsuario
    {
        #region MySQl
        public DataTable buscarUsuarioXCorreo(string correoElectronico)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from usuario where correo_electronico = '" + correoElectronico + "'" ;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);            

            try
            {
                adapter.Fill(datos);
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public DataTable buscarUsuarioXNombreUsuario(string nombreUsuario)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from usuario where nombre_unico_usuario = '" + nombreUsuario.Trim() + "'";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
            }

            catch (MySqlException ex)
            {
                datos = null;
            }

            return datos;
        }
        public void actualizarPasswordUsuario(string nuevoPass, int idUsuario)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();            

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();

                string query = " update usuario set clave_unica_usuario = sha1('" + nuevoPass + "') " 
                    + " where id_usuario = " + idUsuario;

                command.CommandText = query;

                command.ExecuteNonQuery();

                conexion.Close();
            }
            catch (MySqlException ex)
            {

                throw ex;
            }
 
        }
        public void actualizarDatosUsuario(string telefono, string correo, int idUsuario)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();

                string query = "update usuario set telefono = '" + telefono + "', correo_electronico = '" + correo + "' "
                    + " where id_usuario = " + idUsuario;

                command.CommandText = query;

                command.ExecuteNonQuery();

                conexion.Close();
            }
            catch (MySqlException ex)
            {

                throw ex;
            }

        }
        public bool usuarioCorrecto(int idUsuario, string tokenId)
        {
            bool respuesta = false;

            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from usuario where id_usuario = " + idUsuario + " and clave_unica_usuario = sha1('" + tokenId + "')";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    respuesta = true;
                }
            }

            catch (MySqlException ex)
            {
                
            }

            return respuesta;
        }
        public bool existeUsuarioXNombreUsuario(string nombreUsuario)
        {
            bool respuesta = false;

            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from usuario where nombre_unico_usuario = '" + nombreUsuario + "'";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    respuesta = true;
                }
            }

            catch (MySqlException ex)
            {

            }

            return respuesta;
        }
        public bool existeUsuarioXNit(int numid, string dvid)
        {
            bool respuesta = false;
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            conexion = clConexiones.abrirConexionMysql();
            string query = "select * from usuario where numid = " + numid + " and dvid = '" + dvid + "'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);
            try
            {
                adapter.Fill(datos);
                if (datos.Rows.Count == 1)
                    respuesta = true;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            return respuesta;
        }
        public bool existeUsuarioXCorreo(string correoElectronico)
        {
            bool respuesta = false;

            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from usuario where correo_electronico = '" + correoElectronico + "'";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    respuesta = true;
                }
            }

            catch (MySqlException ex)
            {

            }

            return respuesta;
        }
        /// <summary>
        /// Este metodo se actualizó para ingresar diferentes tipos de usuarios.
        /// El parametro banderaTipoUsuario es web o medico (por el momento)
        /// </summary>
        /// <param name="Usuario">Clase usuario</param>
        /// <param name="banderaTipoUsuario">web o medico</param>
        /// <returns>codigo de usuario</returns>
        public int agregarNuevoUsuario(NuevoUsuario Usuario, string banderaTipoUsuario)
        {
            int idUsuarioCreado = 0;
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();
                int tipoUsuario;
                switch (banderaTipoUsuario)
                {
                    case "web":
                        tipoUsuario = obtenerTipoUsuario("ADMINISTRADOR");
                        break;
                    case "medico":
                        tipoUsuario = obtenerTipoUsuario("MEDICO");
                        break;
                    default:
                        tipoUsuario = obtenerTipoUsuario("ADMINISTRADOR");
                        break;
                }
                string query = "INSERT INTO USUARIO (numid, dvid, nombres, apellidos, telefono, correo_electronico, nombre_unico_usuario, clave_unica_usuario, codigo_intermediario, codigo_usuario_remoto, "
                + " usuario_interno, id_tipo_usuario, poliza_matriz_facturas, fecha_creacion_usuario, estado, ideproveedor,id_usuario_creador)"
                + " VALUES('" + Usuario.NUMID + "','" + Usuario.DVID + "','" + Usuario.NOMBRES + "', '" + Usuario.APELLIDOS + "', '" + Usuario.TELEFONO + "', '" + Usuario.CORREO_ELECTRONICO + "', '"
                + Usuario.NOMBRE_UNICO_USUARIO + "', " + "sha1('" + Usuario.PASSWORD + "'), '" + Usuario.CODIGO_INTERMEDIARIO + "', '" + Usuario.CODIGO_USUARIO_REMOTO + "', " + Usuario.USUARIO_INTERNO + ", "
                + tipoUsuario + ", NULL, NOW(), TRUE, '" + Usuario.IDE_PROVEEDOR + "'," + Usuario.ID_USUARIO_CREADOR + ")";
                                                              

                command.CommandText = query;

                command.ExecuteNonQuery();

                idUsuarioCreado = int.Parse(command.LastInsertedId.ToString());

                conexion.Close();
            }
            catch (MySqlException ex)
            {

                throw ex;
            }
            return idUsuarioCreado;
        }
        public int obtenerTipoUsuario(string nombre)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            conexion = clConexiones.abrirConexionMysql();
            int tipoUsuario = 0;
            string query = string.Empty;

            query = "SELECT id_tipo_usuario FROM tipo_usuario where nombre = '" + nombre + "' ";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    tipoUsuario = int.Parse(datos.Rows[0]["id_tipo_usuario"].ToString());
                }

                conexion.Close();
            }
            catch (MySqlException ex)
            {
                conexion.Close();
            }

            return tipoUsuario;
        }
        public string obtenerIdeProveedor(string numid, string dvid)
        {
            string ideproveedor = string.Empty;
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();

            string query = " SELECT IDEPROVEEDOR FROM SCP_PROVEEDOR "
                           + " WHERE TIPOPROV = 'MED' "
                           + " AND STSPROV = 'ACT' "
                           + " AND INDAFILIADO = 'S' "
                           + " AND NUMID = '" + numid + "' "
                           + " AND DVID = '" + dvid + "' ";

            conexion = clConexiones.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(query, conexion);
            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count > 0)
                {
                    ideproveedor = datos.Rows[0]["IDEPROVEEDOR"].ToString();
                }

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return ideproveedor;
        }
        public void agregarRolNuevoUsuario(int idUsuario, string tipoUsuario)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            Varias clVarias = new Varias();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();

                string query = string.Empty;

                switch (tipoUsuario)
                {
                    case "interno":
                        int idRolEmisorInterno = clVarias.obtenerIdRolAcceso("EMISOR_INTERNO");
                        query = "insert into usuario_rol_acceso (id_usuario, id_rol, estado) values (" + idUsuario + "," + idRolEmisorInterno + ",TRUE)";
                        break;
                    case "externo":
                        int idRolEmisorExterno = clVarias.obtenerIdRolAcceso("EMISOR_EXTERNO");
                        query = "insert into usuario_rol_acceso (id_usuario, id_rol, estado) values (" + idUsuario + "," + idRolEmisorExterno + ",TRUE)";
                        break;
                    case "medico":
                        int idRolMedico = clVarias.obtenerIdRolAcceso("CONSULTOR_MEDICO");
                        query = "insert into usuario_rol_acceso (id_usuario, id_rol, estado) values (" + idUsuario + "," + idRolMedico + ",TRUE)";
                        break;
                    default:
                        break;
                }

                command.CommandText = query;

                command.ExecuteNonQuery();
                conexion.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }        
        }
        public DataTable obtenerRolesAccesoUsuario(int idUsuario)
        {
            DataTable datos = new DataTable("Usuario");

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select rol.nombre, rol.id_rol from usuario_rol_acceso us, rol_acceso rol"
                            + " where us.id_rol = rol.id_rol"
                            + " and us.estado = 1"
                            + " and us.id_usuario = " + idUsuario;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                throw ex;
            }

            return datos;
        }

        public DataTable obtenerAccionesUsuario(Dictionary<int, string> rolesAsignados)
        {
            DataTable datos = new DataTable("Usuario");

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = string.Empty;
            string roles = generarListadoRoles(rolesAsignados);

            query = "SELECT distinct ar.id_accion, a.nombre_accion FROM acciones_rol ar, acciones_cotizador a "
                    + " where  ar.id_accion = a.id_accion"
                    + " and ar.id_rol IN (" + roles + ")"
                    + "and ar.estado = true";             

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                throw ex;
            }

            return datos;
        }
        private string generarListadoRoles(Dictionary<int, string> roles)
        {
            string resultado = string.Empty;
            int contador = 0;

            foreach (KeyValuePair<int, string> rol in roles)
            {
                if (contador == roles.Count -1)
                {
                    resultado += rol.Key;
                }
                else
	            {
                    resultado += rol.Key + ",";
	            }

                contador++;
            }
           
            return resultado;
        }
        public DataTable obtenerPermisosUsuario(int idUsuario)
        {
            DataTable datos = new DataTable("Usuario");

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select p.nombre from permisos_usuario pu, permisos p"
                            + " where p.id_permiso = pu.id_permiso"
                            + " and pu.estado = 1"
                            + " and pu.id_usuario = " + idUsuario;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                throw ex;
            }

            return datos;
        }
        public void ingresarIntermediarioWeb(DataTable intermediario)
        {
            string codIntermediario = intermediario.Rows[0]["codinter"].ToString();

            if (!existeIntermediario(codIntermediario))
            {
                Conexiones clConexiones = new Conexiones();
                MySqlConnection conexion = new MySqlConnection();

                try
                {
                    conexion = clConexiones.abrirConexionMysql();

                    MySqlCommand command = conexion.CreateCommand();

                    string query = "INSERT INTO intermediarios (codinter, nomcomercial, fecha_creacion, estado)  "
                    + " VALUES('" + codIntermediario + "', '" + intermediario.Rows[0]["nomcomercial"].ToString() + "', now(), TRUE)";


                    command.CommandText = query;

                    command.ExecuteNonQuery();

                    conexion.Close();
                }
                catch (MySqlException ex)
                {

                    throw ex;
                }
            }
        }
        public bool existeIntermediario(string codigoIntermediario)
        {
            bool existe = false;

            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from intermediarios where codinter = '" + codigoIntermediario + "'";

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
                conexion.Close();
            }

            return existe;
        }
        public DataTable obtenerCorreosEncargadosWeb()
        {
            DataTable datos = new DataTable("Usuario");
            Varias clVarias = new Varias();

            int idRolAccesoUsuario = clVarias.obtenerIdRolAcceso("ENCARGADO_WEB");

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT distinct us.correo_electronico FROM usuario_rol_acceso rol, usuario us "
                            + " where rol.id_usuario = us.id_usuario and rol.id_rol = " + idRolAccesoUsuario;                            

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                throw ex;
            }

            return datos;
        }

        #endregion

        #region Oracle
        public DataTable existeUsuarioRemotoOracle(string nombreUsuario, string numid, string dvid)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();

            string query = " select * from usuario_remoto " +
                           " where idusuario = '" + nombreUsuario + "' " +
                           " UNION " +
                           " select * from usuario_remoto " +
                           " where numid = " + numid + " " +
                           " and dvid = '" + dvid + "' ";

            conexion = clConexiones.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public DataTable obtenerUsuarioRemotoOracle(string nombreUsuario)
        {   
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();

            string query = "select * from usuario_remoto " +
                           " where (idusuario = '" + nombreUsuario + "') ";                          

            conexion = clConexiones.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);       
                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public string generarCodigoUsuarioRemoto()
        {
            string codigoUsuarioRemoto = string.Empty;

            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();

            conexion = clConexiones.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" SELECT LPAD(MAX(codusuarioremo)+1,6,'0') CODUSUARIOREMOTO FROM USUARIO_REMOTO;", conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    codigoUsuarioRemoto = datos.Rows[0]["CODUSUARIOREMOTO"].ToString();
                }

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return codigoUsuarioRemoto;
        }
        public void agregarUsuarioRemotoOracle(NuevoUsuario Usuario, string banderaTipoUsuario)
        {
            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();

            try
            {
                conexion = clConexiones.abrirConexionOracleAcsel();

                OracleCommand command = conexion.CreateCommand();

                string nombres = Usuario.NOMBRES + " " + Usuario.APELLIDOS;
           
                switch(banderaTipoUsuario)
                {
                    case "medico":
                        {
                            command.CommandText = " INSERT INTO usuario_remoto(TIPOID,NUMID,DVID,NOMBRE,DIRECCION,IDUSUARIO,PASSWORD,"
                               + "EMAIL,CODUSUARIOREMO,CODINTER,INDTIPOENT,INDINTERNO)"
                               + " VALUES ('NIT', '" + Usuario.NUMID + "', '" + Usuario.DVID + "', '" + nombres + "', '" + Usuario.DIRECCION + "', '"
                               + Usuario.NOMBRE_UNICO_USUARIO + "', '" + Usuario.PASSWORD + "', '" + Usuario.CORREO_ELECTRONICO + "', '" + Usuario.CODIGO_USUARIO_REMOTO
                               + "', '" + Usuario.IDE_PROVEEDOR + "', 'E', '" + Usuario.USUARIO_INTERNO + "')";
                            break;
                        }
                    case "web":
                        {
                            command.CommandText = " INSERT INTO usuario_remoto(TIPOID,NUMID,DVID,NOMBRE,DIRECCION,IDUSUARIO,PASSWORD,"
                                + "EMAIL,CODUSUARIOREMO,CODINTER,INDTIPOENT,INDINTERNO)"
                                + " VALUES ('NIT', '" + Usuario.NUMID + "', '" + Usuario.DVID + "', '" + nombres + "', '" + Usuario.DIRECCION + "', '"
                                + Usuario.NOMBRE_UNICO_USUARIO + "', '" + Usuario.PASSWORD + "', '" + Usuario.CORREO_ELECTRONICO + "', '" + Usuario.CODIGO_USUARIO_REMOTO
                                + "', '" + Usuario.CODIGO_INTERMEDIARIO + "', 'E', '" + Usuario.USUARIO_INTERNO + "')";
                            break;
                        }
                    default:
                        {
                            command.CommandText = " INSERT INTO usuario_remoto(TIPOID,NUMID,DVID,NOMBRE,DIRECCION,IDUSUARIO,PASSWORD,"
                                + "EMAIL,CODUSUARIOREMO,CODINTER,INDTIPOENT,INDINTERNO)"
                                + " VALUES ('NIT', '" + Usuario.NUMID + "', '" + Usuario.DVID + "', '" + nombres + "', '" + Usuario.DIRECCION + "', '" 
                                + Usuario.NOMBRE_UNICO_USUARIO + "', '" + Usuario.PASSWORD + "', '" + Usuario.CORREO_ELECTRONICO + "', '" + Usuario.CODIGO_USUARIO_REMOTO 
                                + "', '" + Usuario.CODIGO_INTERMEDIARIO + "', 'E', '" + Usuario.USUARIO_INTERNO + "')";
                            break;
                        }
                }
                

                //command.Parameters.Add("@1", "'NIT'");
                //command.Parameters.Add("@2", Usuario.NUMID);
                //command.Parameters.Add("@3", Usuario.DVID);
                //command.Parameters.Add("@4", Usuario.NOMBRES + " " + Usuario.APELLIDOS);
                //command.Parameters.Add("@5", Usuario.DIRECCION);
                //command.Parameters.Add("@6", Usuario.NOMBRE_UNICO_USUARIO);
                //command.Parameters.Add("@7", Usuario.PASSWORD);
                //command.Parameters.Add("@8", Usuario.CORREO_ELECTRONICO);
                //command.Parameters.Add("@9", Usuario.CODIGO_USUARIO_REMOTO);
                //command.Parameters.Add("@10", Usuario.CODIGO_INTERMEDIARIO);
                //command.Parameters.Add("@11", "E");

                //if (Usuario.USUARIO_INTERNO == "1")
                //{
                //    command.Parameters.Add("@12", "1");
                //}
                //else
                //{
                //    command.Parameters.Add("@12", "2");
                //}                              

                command.ExecuteNonQuery();

                conexion.Close();
            }
            catch (MySqlException ex)
            {

                throw ex;
            } 
        }
        public bool nitValidoCliente(int numID, string dvID)
        {
            bool respuesta = false;
            string resultado = string.Empty;

            OracleConnection conexion = new OracleConnection();
            Conexiones clConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "PR_MANTENIMIENTO.DIGITO_VERIFICADOR";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nnumnit", OracleDbType.Int32);
                OracleParameter p2 = new OracleParameter("Return_Value", OracleDbType.Varchar2, 32767);

                p1.Value = numID;

                p2.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p1);


                cmd.ExecuteNonQuery();

                resultado = cmd.Parameters["Return_Value"].Value.ToString();

                if (dvID.ToUpper() == resultado)
                {
                    respuesta = true;
                }
            }
            catch (OracleException ex)
            {
                throw ex;
            }

            return respuesta;
        }
        public DataTable obtenerIntermediario(string codigoIntermediario)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();

            conexion = clConexiones.abrirConexionOracleAcsel();

            string query = "select codinter, pr_mantenimiento.nombre_agente(codinter) nomcomercial from intermediario where codinter = '" + codigoIntermediario + "'";
            OracleDataAdapter adapter = new OracleDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }

        #endregion
    }
}
