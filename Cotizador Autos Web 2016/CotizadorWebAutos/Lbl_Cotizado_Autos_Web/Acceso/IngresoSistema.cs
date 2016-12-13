using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Data;
using Lbl_Cotizador_Autos_Web.ConexionesBD;

namespace Lbl_Cotizador_Autos_Web.Acceso
{
    public class IngresoSistema
    {
        public DataTable datosAccesoUsuario(string usuario, string password)
        {           
            DataTable datos = new DataTable("Usuario");

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();           

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from usuario" +
                " where nombre_unico_usuario = '" + usuario.ToUpper() + "' and clave_unica_usuario = sha1('" + password + "')", conexion);

           try
            { 
                adapter.Fill(datos);                
            }

            catch (MySqlException ex)
            {
                throw ex;
            }

           return datos;
        }
        /// <summary>
        /// Funcion que obtiene lista de intermediarios para mostrar en registro de usuarios
        /// </summary>
        /// <returns>DataTable con codinter y cod_nombre</returns>
        public DataTable ObtenerIntermediarios()
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = " SELECT '000000' as codinter, 'INGRESAR INTERMEDIARIO' as cod_nom " +
                           " UNION " +
                           " SELECT i.codinter, CONCAT(i.codinter,' - ',i.nomcomercial) as cod_nom  " +
                           " FROM intermediarios i  " +
                           " WHERE estado = TRUE  " +
                           " ORDER BY codinter;";

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
        public class informacionUsuario
        {
            public int idUsuario { get; set; }
            public string nombreUsuario { get; set; }
            public string apellidoUsuario { get; set; }
            public string codIntermediario { get; set; }
            public string ideProveedor { get; set; }
            public bool usuarioInterno { get; set; }
            public string codigoUsuarioRemoto { get; set; }
            public string correoElectronico { get; set; }
            public string nombreUsuarioUnico { get; set; }
            public int numId { get; set; }
            public string dvID { get; set; }
            public List<string> rolesAsignados { get; set; }
            public List<string> accionesPermitidas { get; set; }

            public informacionUsuario()
            {
                this.rolesAsignados = new List<string>();
                this.accionesPermitidas = new List<string>();
            }
        }
    }
}
