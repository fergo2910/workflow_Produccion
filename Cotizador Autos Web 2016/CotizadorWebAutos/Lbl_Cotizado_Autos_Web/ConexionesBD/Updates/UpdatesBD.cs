using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Lbl_Cotizador_Autos_Web.ConexionesBD;
using Lbl_Cotizado_Autos_Web.Estructuras;
using System.Xml.Linq;
using Lbl_Cotizado_Autos_Web.Clientes;

namespace Lbl_Cotizado_Autos_Web.ConexionesBD.Updates
{
    public class UpdatesBD
    {
        public bool UpdateEstadoCotizacion(string pIdCotizacion, int pEstadoCotizacion)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            DataTable resultadoIdepol = new DataTable();
            bool resultado = false;

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();
                command.CommandText = " update cotizacion set estado_cotizacion = '" + pEstadoCotizacion + "' where id_cotizacion = '" + pIdCotizacion + "'";

                command.ExecuteNonQuery();
                resultado = true;
                conexion.Close();

            }
            catch (MySqlException ex)
            {
                conexion.Close();
                resultado = false;
                throw ex;
            }
            return resultado;
        }
        public bool ActualizarAutorizacionCotizacion(string pIdCotizacion, string pTipoAutorizacion, string pComentarioAutorizacion)
        {
            bool resultado = false;
            int idEstadoCotizacion = 0;
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            Operaciones objetoOperaciones = new Operaciones();

            try
            {
                conexion = clConexiones.abrirConexionMysql();
                MySqlCommand command = conexion.CreateCommand();

                if (pTipoAutorizacion == "AUT")
                {
                    idEstadoCotizacion = objetoOperaciones.obtenerIdEstadoCotizacion(pTipoAutorizacion);
                    UpdateEstadoCotizacion(pIdCotizacion,idEstadoCotizacion);
                    command.CommandText = " update cotizacion set estatus_autorizacion_inspeccion = 'AUTORIZADO',comentario_autorizacion = '" + pComentarioAutorizacion + "' where id_cotizacion = '" + pIdCotizacion + "'";
                }
                if (pTipoAutorizacion == "DEN")
                {
                    idEstadoCotizacion = objetoOperaciones.obtenerIdEstadoCotizacion(pTipoAutorizacion);
                    UpdateEstadoCotizacion(pIdCotizacion, idEstadoCotizacion);
                    command.CommandText = " update cotizacion set estatus_autorizacion_inspeccion = 'DENEGADO',comentario_autorizacion = '" + pComentarioAutorizacion + "' where id_cotizacion = '" + pIdCotizacion + "'";
                }

                command.ExecuteNonQuery();
                conexion.Close();
                resultado = true;
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                resultado = false;
                throw ex;
            }

            return resultado;
        }
        public bool DeleteInformacionCotizacionOracle(string pIdCotizacion)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            DataTable resultadoIdepol = new DataTable();
            Operaciones objetoOperaciones = new Operaciones();
            bool resultado = false;
            int idEstadoCotizacion = 0;

            try
            {

                idEstadoCotizacion = objetoOperaciones.obtenerIdEstadoCotizacion("ELI");
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();
                command.CommandText = " update cotizacion set estado_cotizacion = '" + idEstadoCotizacion + "' where id_cotizacion = '" + pIdCotizacion + "'";

                command.ExecuteNonQuery();
                conexion.Close();

                conexion = clConexiones.abrirConexionMysql();

                command = conexion.CreateCommand();
                command.CommandText = " DELETE FROM reporte_cotizacion_autos WHERE id_cotizacion = '" + pIdCotizacion + "'";

                command.ExecuteNonQuery();
                conexion.Close();

                resultado = true;
                conexion.Close();

            }
            catch (MySqlException ex)
            {
                conexion.Close();
                resultado = false;
                throw ex;
            }
            return resultado;
        }
    }
}
