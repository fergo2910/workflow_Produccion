using Lbl_Cotizador_Autos_Web.ConexionesBD;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.HogarSeguro
{
    public class Proceso_Emision_Hogar_Seguro
    {
        public DataTable obtenerCotizacionesAgente(int pCodUsuarioRemoto, string pRol)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            string query = string.Empty;

            switch (pRol)
            {
                case "AGENTE":
                    query = "select * from cotizacion cot, planes_web pl where"
                    + " cot.plan_cotizado = pl.id_plan_web and cot.estado_cotizacion not in ('EMI', 'ANU')"
                    + " and codigo_agente_cotizando = " + pCodUsuarioRemoto
                    + " order by cot.id_cotizacion desc";
                    break;
                default:
                    break;
            }

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

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
        public DataTable obtenerPlanFinanciamiento(string idePlanPol, string planPol)
        {
            DataTable datosPais = new DataTable();

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();

            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            string query = "SELECT * FROM PLAN_FINANCIAMIENTO_PLAN_PROD p, ca_planes c"
                            + " where c.ideplanpol = '" + idePlanPol + "'"
                            + " and c.planpol = '" + planPol + "'"
                            + " and p.codprod = c.codprod and p.codplan = c.codigo_plan and p.REVPLAN = c.CODIGO_REV";

            OracleDataAdapter adapter = new OracleDataAdapter(query, conexionOracle);

            try
            {
                adapter.Fill(datosPais);
                conexionOracle.Close();
            }
            catch (OracleException ex)
            {
                throw ex;
            }

            return datosPais;
        }
        public DataTable obtenerClaseCodBien()
        {
            DataTable datosBien = new DataTable();

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();

            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            string query = "select clasebien, codbien, descbien from bien"
            + " where codbien = '001'";

            OracleDataAdapter adapter = new OracleDataAdapter(query, conexionOracle);

            try
            {
                adapter.Fill(datosBien);
                conexionOracle.Close();
            }
            catch (OracleException ex)
            {
                throw ex;
            }

            return datosBien;
        }
        public DataTable obtenerDireccionBien(int idCotizacion)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from datos_bien"
           + " where id_cotizacion = " + idCotizacion;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

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
        public void guardarDatosDelBien(int idCotizacion, string direccion, string codPais, string descPais, string codDepto, string descDepto, string codMuni, string descMuni)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();
                command.CommandText = " INSERT INTO datos_bien (id_cotizacion, direccion_bien, pais_direccion, descripcion_pais, depto_direccion, descripcion_depto, muni_direccion, descripcion_muni) " +
                                      " VALUES (@1, @2, @3, @4, @5, @6, @7, @8) ";

                command.Parameters.AddWithValue("@1", idCotizacion);
                command.Parameters.AddWithValue("@2", direccion);
                command.Parameters.AddWithValue("@3", codPais);
                command.Parameters.AddWithValue("@4", descPais);
                command.Parameters.AddWithValue("@5", codDepto);
                command.Parameters.AddWithValue("@6", descDepto);
                command.Parameters.AddWithValue("@7", codMuni);
                command.Parameters.AddWithValue("@8", descMuni);

                command.ExecuteNonQuery();                

                conexion.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
        }
        private void actualizarEstadoCotizacion(int idCotizacion)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();
                command.CommandText = " update cotizacion set estado_cotizacion = 'HGBIEN' where id_cotizacion = " + idCotizacion;

                command.ExecuteNonQuery();

                conexion.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }

        }
    }
}
