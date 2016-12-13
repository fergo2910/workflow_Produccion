using Lbl_Cotizador_Autos_Web.ConexionesBD;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.Oficina_Virtual
{
    public class Validacion_Primas
    {

        public DataTable obtenerPrimasPagadas(string codinter, DateTime fecini, DateTime fecfin)
        {
            DataTable polizaObtenida = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            cmd.Connection = conexionOracle;
            cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.PRIMAS_PAGADAS";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("pcodinter", OracleDbType.Varchar2);
            OracleParameter p2 = new OracleParameter("pfecini", OracleDbType.Date);
            OracleParameter p3 = new OracleParameter("pfecfin", OracleDbType.Date);
            OracleParameter retorno = new OracleParameter("Return_Value", OracleDbType.RefCursor);
            p1.Value = codinter;
            p2.Value = fecini;
            p3.Value = fecfin;
            retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            try
            {
                polizaObtenida.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return polizaObtenida;
        }

        public DataTable obtenerPrimasPendientes(string codinter, DateTime fecfin)
        {
            DataTable polizaObtenida = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            cmd.Connection = conexionOracle;
            cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.PRIMAS_PENDIENTES";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("pcodinter", OracleDbType.Varchar2);
            OracleParameter p3 = new OracleParameter("pfecfin", OracleDbType.Date);
            OracleParameter retorno = new OracleParameter("Return_Value", OracleDbType.RefCursor);
            p1.Value = codinter;
            p3.Value = fecfin;
            retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p3);
            try
            {
                polizaObtenida.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return polizaObtenida;
        }
    
        public string obtenerNombreIntermediario(string codinter)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            conexion = clConexiones.abrirConexionMysql();
            string query = "SELECT nomcomercial FROM intermediarios where codinter = " + codinter;
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);
            try
            {
                adapter.Fill(datos);
                if (datos.Rows.Count > 0 && datos.Rows[0]["nomcomercial"] != null)
                    return datos.Rows[0]["nomcomercial"].ToString();
                else
                    return "";
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                return "";
            }
        }

        public bool crearArchivoExcel(string codinter, DateTime fecini, DateTime fecfin, string bandera)
        {
            DataTable resultado = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            cmd.Connection = conexionOracle;
            cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.DATOS_COBRO_AGENTE";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("pcodinter", OracleDbType.Varchar2);
            OracleParameter p2 = new OracleParameter("pfecini", OracleDbType.Date);
            OracleParameter p3 = new OracleParameter("pfecfin", OracleDbType.Date);
            OracleParameter p4 = new OracleParameter("ppagpen", OracleDbType.Varchar2);
            OracleParameter retorno = new OracleParameter("Return_Value", OracleDbType.Int32);
            p1.Value = codinter;
            p2.Value = fecini;
            p3.Value = fecfin;
            p4.Value = bandera;
            retorno.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            try
            {
                resultado.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
                return true;
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                return false;
            }
        }
    }
}
