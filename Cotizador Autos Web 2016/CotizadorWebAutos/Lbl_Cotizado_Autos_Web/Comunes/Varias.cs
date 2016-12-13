using Lbl_Cotizador_Autos_Web.ConexionesBD;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lbl_Cotizador_Autos_Web.RecursosFactura;

namespace Lbl_Cotizado_Autos_Web.Comunes
{
    public class Varias
    {
        public int obtenerIdEstadoCotizacion(string nombreEstado)
        {
            int id = 0;

            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from estado_cotizacion where nombre = '" + nombreEstado + "'";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    id = int.Parse(datos.Rows[0]["id_estado_cotizacion"].ToString());
                }

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
            }

            return id;
        }
        public void actualizarEstadoCotizacion(int idCotizacion, int idEstado)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();      

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();
                command.CommandText = " update cotizacion set estado_cotizacion = " + idEstado
                    + " where id_cotizacion = " + idCotizacion;                    

                command.ExecuteNonQuery();

                conexion.Close();

            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
        }
        public int obtenerIdRolAcceso(string nombreRolAcceso)
        {
            int id = 0;

            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select * from rol_acceso where nombre = '" + nombreRolAcceso + "'";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    id = int.Parse(datos.Rows[0]["id_rol"].ToString());
                }

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
            }

            return id;
        }
        public string crearPasswordNuevo(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public string obtenerNombrePlan(int idPlan)
        {
            string nombrePlan = string.Empty;

            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select nombre from planes_web where id_plan_web = " + idPlan;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    nombrePlan = datos.Rows[0]["nombre"].ToString();
                }

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
            }

            return nombrePlan;
        }
        public DataTable obtenerInformacionPlan(int idPlan)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT * FROM planes_web where id_plan_web = " + idPlan, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable obtenerInformacionPlan(string idPlanPol, string planPol)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT * FROM planes_web where ide_plan_pol = '" + idPlanPol + "' and plan_pol = '" + planPol+"'", conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable obtenerInformacionCotizacion(int idCotizacion)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT * FROM cotizacion where id_cotizacion = " + idCotizacion, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable obtenerInformacionUsuarioLogueado(int idUsuario)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT * FROM usuario where id_usuario = " + idUsuario, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public string CodificarA64(string ptoEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.UTF8.GetBytes(ptoEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
        public string DecodificarDesde64(string pencodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(pencodedData);
            string returnValue = System.Text.ASCIIEncoding.UTF8.GetString(encodedDataAsBytes);
            return returnValue;
        }
    }
}
