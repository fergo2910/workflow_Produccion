using Lbl_Cotizador_Autos_Web.ConexionesBD;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.GM_Control_Cupones
{
    public class ValidacionPoliza
    {
        /// <summary>
        /// Obtiene la lista de asegurados según póliza ingresada
        /// </summary>
        /// <param name="codpol">codigo de poliza, solo letras</param>
        /// <param name="numpol">numero de poliza, solo números</param>
        /// <param name="certificado">numero de certifiacdo, solo números</param>
        /// <param name="fecha">fecha, ingresada por el sistema</param>
        /// <returns>Record de todos los pacientes asociados a la póliza</returns>
        public DataTable polizaActiva(string codpol, int numpol, int certificado, DateTime fecha)
        {
            DataTable datosAsegurado = new DataTable();

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();

            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;
            cmd.CommandText = "PR_FUNC_SALUD.GET_BENEF_CERTIF";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("pCodProd", OracleDbType.Varchar2);
            OracleParameter p2 = new OracleParameter("pNumPol", OracleDbType.Int32);
            OracleParameter p3 = new OracleParameter("pNumCert", OracleDbType.Int32);
            OracleParameter p4 = new OracleParameter("pFecOcurr", OracleDbType.Date);
            OracleParameter p5 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = codpol;
            p2.Value = numpol;
            p3.Value = certificado;
            p4.Value = fecha;

            p5.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);

            try
            {
                datosAsegurado.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return datosAsegurado;

        }
        /// <summary>
        /// Obtener las observaciones de el paciente solicitado
        /// </summary>
        /// <param name="ideaseg">id de asegurado</param>
        /// <param name="fecha">fecha de busqueda</param>
        /// <returns>El record de observaciones que la poliza tiene</returns>
        public DataTable observacionesPoliza(string ideaseg, DateTime fecha)
        {
            DataTable obsPoliza = new DataTable();

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();

            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;
            cmd.CommandText = "PR_FUNC_SALUD.GET_ASEG_OBSERV";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("pIdeAseg", OracleDbType.Int32);
            OracleParameter p2 = new OracleParameter("pFecOcurr", OracleDbType.Date);
            OracleParameter p3 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = ideaseg;
            p2.Value = fecha;

            p3.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

            try
            {
                obsPoliza.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return obsPoliza;
        }
        /// <summary>
        /// Obtiene todos los cupones que el usuario ha utilizado
        /// </summary>
        /// <param name="ideaseg">Id de cualquier paciente</param>
        /// <returns>Tabla de cupones utilizados</returns>
        public DataTable obtenerHistorialAsegurados(int ideaseg)
        {
            DataTable datosHistorial = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;
            cmd.CommandText = "PR_FUNC_SALUD.HIST_CUPONES";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("pIdeAseg", OracleDbType.Int32);
            OracleParameter p2 = new OracleParameter("pIdeProveedor", OracleDbType.Int32);
            OracleParameter p3 = new OracleParameter("pFecOcurr", OracleDbType.Date);
            OracleParameter p4 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = ideaseg;
            p2.Value = DBNull.Value;
            p3.Value = DBNull.Value;

            p4.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);

            try
            {
                datosHistorial.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return datosHistorial;

        }
        /// <summary>
        /// Metodo que genera un cupón para el proveedor
        /// </summary>
        /// <param name="ideaseg">id asegurado </param>
        /// <param name="ideproveedor">id proveedor</param>
        /// <param name="fecha">fecha actual</param>
        /// <returns></returns>
        public string retornaCupones(int ideaseg, int ideproveedor, DateTime fecha)
        {
            string mensajeRetorno = string.Empty;
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;
            cmd.CommandText = "PR_FUNC_SALUD.GRABAR_CUPON";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("pIdeAseg", OracleDbType.Int32);
            OracleParameter p2 = new OracleParameter("pFecOcurr", OracleDbType.Date);
            OracleParameter p3 = new OracleParameter("pIdeProveedor", OracleDbType.Int32);
            OracleParameter p4 = new OracleParameter("Return_Value", OracleDbType.Varchar2,32767);

            p1.Value = ideaseg;
            p2.Value = fecha;
            p3.Value = ideproveedor;

            p4.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);

            try
            {
                cmd.ExecuteReader();
                mensajeRetorno = cmd.Parameters["Return_Value"].Value.ToString();
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return mensajeRetorno;

        }
        /// <summary>
        /// Obtiene todos los cupones que el médico ha utilizado en todos los asegurados
        /// </summary>
        /// <param name="ideproveedor">Id de médico conectado</param>
        /// <returns>Tabla de cupones utilizados por medico</returns>
        public DataTable obtenerHistorialMedico(int ideproveedor)
        {
            DataTable datosHistorial = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;
            cmd.CommandText = "PR_FUNC_SALUD.HIST_CUPONES";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("pIdeAseg", OracleDbType.Int32);
            OracleParameter p2 = new OracleParameter("pIdeProveedor", OracleDbType.Int32);
            OracleParameter p3 = new OracleParameter("pFecOcurr", OracleDbType.Date);
            OracleParameter p4 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = DBNull.Value;
            p2.Value = ideproveedor;
            p3.Value = DBNull.Value;

            p4.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);

            try
            {
                datosHistorial.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return datosHistorial;

        }
        public DataTable obtenerDatosMedico(string numid, string dvid)
        {
            return null;
        }
        
    }
}
