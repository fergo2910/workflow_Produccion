using Lbl_Cotizador_Autos_Web.ConexionesBD;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.Seguridad
{
    public class ConfiguracionPlanIntermediario
    {
        public DataTable obtenerIntermediariosActivos()
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select codinter, concat(codinter, ' - ', nomcomercial) nombre from intermediarios where estado = 1 order by codinter";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                datos = null;
            }

            return datos;
        }
        public DataTable obtenerCatalogoPlanes()
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from planes_web where estado = 1";

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
        public DataTable obtenerPlanesDeIntermediario(string codIntermediario)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from plan_intermediario where estado = 1 and codinter = '" + codIntermediario + "'";

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
        public void agregarPlanIntermediario(string codIntermediario, List<string> seleccionados, List<string> noSeleccionados)
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
                    if (existePlanIntermediario(codIntermediario, int.Parse(sel)))
                    {
                        query = "update plan_intermediario set estado = TRUE"
                            + " where codinter = '" + codIntermediario + "' and id_plan_web = " + int.Parse(sel);

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        query = "insert into plan_intermediario "
                            + " values ('" + codIntermediario + "', " + int.Parse(sel) + ", TRUE)";

                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }
                }

                foreach (string noSel in noSeleccionados)
                {
                    if (existePlanIntermediario(codIntermediario, int.Parse(noSel)))
                    {
                        query = "update plan_intermediario set estado = FALSE"
                           + " where codinter = '" + codIntermediario + "' and id_plan_web = " + int.Parse(noSel);

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
        public bool existePlanIntermediario(string codIntermediario, int idPlanWeb)
        {
            bool existe = false;

            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from plan_intermediario where codinter = '"
                + codIntermediario + "' and id_plan_web = " + idPlanWeb;

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
    }
}
