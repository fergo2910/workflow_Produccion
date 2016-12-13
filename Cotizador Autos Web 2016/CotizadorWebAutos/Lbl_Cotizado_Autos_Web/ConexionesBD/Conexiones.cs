using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.DataAccess.Client;
using Lbl_Cotizado_Autos_Web.ConexionesBD;

namespace Lbl_Cotizador_Autos_Web.ConexionesBD
{
    public class Conexiones
    {
        /// <summary>
        /// Funcion que sirve para abrir la conexion con la base de datos de mysql.
        /// 29/01/2016
        /// Julio Luna
        /// </summary>
        /// <returns></returns>
        public MySqlConnection abrirConexionMysql()
        {            
            MySqlConnection mysqlConexion = new MySqlConnection();
            string strConexion = ConfigurationManager.ConnectionStrings["ConexionMysql"].ConnectionString;
            //string sUsuario = GlobalConection.Usuario;
            //string sClave = GlobalConection.Clave;
            //strConexion = strConexion.Substring(0, strConexion.IndexOf("User Id="));
            //strConexion = strConexion + "User Id=" + sUsuario + ";password=" + sClave;
            mysqlConexion.ConnectionString = strConexion;

            
            try
            {
                mysqlConexion.Open();
            }
            catch (MySqlException ex)
            {
                throw (ex);
            }

            return mysqlConexion;            
        }

        /// <summary>
        /// Funcion que sirve para abrir la conexion con la base de datos Oracle para el sistema Acsel
        /// 29/01/2016
        /// Julio Luna
        /// </summary>
        /// <returns></returns>

        public OracleConnection abrirConexionOracleAcsel()
        {    
            OracleConnection conexion = new OracleConnection();
            string strConexion = ConfigurationManager.ConnectionStrings["ConexionOracleAcsel"].ConnectionString;
            //strConexion.Substring(0, strConexion.IndexOf("User Id="));
            //string sUsuario = GlobalConection.Usuario;
            //string sClave = GlobalConection.Clave;
            //strConexion = strConexion.Substring(0, strConexion.IndexOf("User Id="));
            //strConexion = strConexion + "User Id=" + sUsuario + ";password=" + sClave + ";";
            conexion.ConnectionString = strConexion;
            try
            {
                conexion.Open();
            }
            catch (OracleException ex)
            {
                throw (ex);
            }            

            return conexion;
        }           
    }
}
