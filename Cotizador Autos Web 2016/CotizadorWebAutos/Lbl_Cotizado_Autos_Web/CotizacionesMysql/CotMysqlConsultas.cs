using Lbl_Cotizador_Autos_Web.ConexionesBD;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.CotizacionesMysql
{
    public class CotMysqlConsultas
    {
        public string obtenerEstadoCotizacion(int idCotizacion)
        {
            DataTable datos = new DataTable();
            string estadoCotizacion = string.Empty;

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "select es.nombre from cotizacion cot, estado_cotizacion es"
            + " where cot.estado_cotizacion =  es.id_estado_cotizacion and cot.id_cotizacion = " + idCotizacion;

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    estadoCotizacion = datos.Rows[0]["nombre"].ToString();
                }

                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
            }

            return estadoCotizacion;
        }
    }
}
