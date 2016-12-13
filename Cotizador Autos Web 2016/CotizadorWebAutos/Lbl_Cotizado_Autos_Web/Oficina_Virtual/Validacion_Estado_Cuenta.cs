using Lbl_Cotizador_Autos_Web.ConexionesBD;
using Oracle.DataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.Oficina_Virtual
{
    public class Validacion_Estado_Cuenta
    {
        public DataTable obtenerProductos()
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            string query = " SELECT UNIQUE DESCRIP FROM LVAL WHERE TIPOLVAL = 'AREASEG' ";
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
        /// <summary>
        /// funcion secuencial de búsqueda de pólizas.
        /// Verifica: ingresó numid junto con dvid y realiza la condicion
        /// sino: realiza condicion por separadado si es que ingresolo solo 1
        /// Verifica: ingreso ramo diferente de todos con numpol y realiza condicion
        /// sino: realiza condicion solo por numpol
        /// Verifica: ingreso de nombre y apellido y realiza la condicion
        /// sino: realiza condicion con nombre o apellido
        /// </summary>
        /// <param name="numid"></param>
        /// <param name="dvid"></param>
        /// <param name="ramo"></param>
        /// <param name="numpol"></param>
        /// <param name="nombres"></param>
        /// <param name="apellidos"></param>
        /// <returns></returns>
        public DataTable busquedaPoliza(string numid = null, string dvid = null, string ramo = null, string numpol = null,
            string nombres = null, string apellidos = null, string codIntermediario = null, bool esUsuarioCobros = false)
        {
            string consulta = " SELECT DISTINCT CODPOL, NUMPOL, PR_MANTENIMIENTO.NOMBRE_CLIENTE(CODCLI) NOMBRE FROM POLIZA PO WHERE ";
            ArrayList condiciones = new ArrayList();

            if (!numid.Equals(string.Empty) && !dvid.Equals(string.Empty))
            {
                condiciones.Add(" CODCLI IN ( SELECT CODCLI FROM CLIENTE CI, TERCERO TE WHERE CI.TIPOID = TE.TIPOID " +
                                " AND CI.DVID = TE.DVID AND CI.NUMID = TE.NUMID AND LTRIM(RTRIM(REPLACE(TE.NIT,'-',''))) =  " + numid + "||" + dvid + ") ");
                condiciones.Add(" IDEPOL IN ( SELECT DISTINCT IDEPOL FROM RESP_PAGO RE, CLIENTE CI, TERCERO TE " +
                                " WHERE RE.CODCLI = CI.CODCLI AND CI.DVID = TE.DVID AND CI.NUMID = TE.NUMID " +
                                " AND CI.TIPOID = TE.TIPOID AND LTRIM(RTRIM(REPLACE(TE.NIT,'-',''))) =  " + numid + "||" + dvid + " ) ");
            }
            else if (!numid.Equals(string.Empty))
            {
                condiciones.Add(" CODCLI IN ( SELECT CODCLI FROM CLIENTE CI, TERCERO TE WHERE CI.TIPOID = TE.TIPOID " +
                                " AND CI.DVID = TE.DVID AND CI.NUMID = TE.NUMID AND TE.NUMID = " + numid + " ) ");
                condiciones.Add(" IDEPOL IN ( SELECT DISTINCT IDEPOL FROM RESP_PAGO RE, CLIENTE CI, TERCERO TE " +
                                " WHERE RE.CODCLI = CI.CODCLI AND CI.DVID = TE.DVID AND CI.NUMID = TE.NUMID " +
                                " AND CI.TIPOID = TE.TIPOID AND TE.NUMID = " + numid + " ) ");
            }
            else if (!dvid.Equals(string.Empty))
            {
                condiciones.Add(" CODCLI IN ( SELECT CODCLI FROM CLIENTE CI, TERCERO TE WHERE CI.TIPOID = TE.TIPOID " +
                                " AND CI.DVID = TE.DVID AND CI.NUMID = TE.NUMID AND TE.DVID = '" + dvid + "' ) ");
                condiciones.Add(" IDEPOL IN ( SELECT DISTINCT IDEPOL FROM RESP_PAGO RE, CLIENTE CI, TERCERO TE " +
                                " WHERE RE.CODCLI = CI.CODCLI AND CI.DVID = TE.DVID AND CI.NUMID = TE.NUMID " +
                                " AND CI.TIPOID = TE.TIPOID AND TE.DVID = '" + dvid + "' ) ");
            }

            if (!ramo.Equals("TODOS") && !numpol.Equals(string.Empty))
                condiciones.Add(" CODPOL IN ( SELECT CODLVAL FROM LVAL WHERE TIPOLVAL = 'AREASEG' AND DESCRIP = '" + ramo + "' ) ");
            else if ((!numid.Equals(string.Empty) || !dvid.Equals(string.Empty) || 
                !nombres.Equals(string.Empty) || !apellidos.Equals(string.Empty)) && !ramo.Equals("TODOS"))
            {
                condiciones.Add(" CODPOL IN ( SELECT CODLVAL FROM LVAL WHERE TIPOLVAL = 'AREASEG' AND DESCRIP = '" + ramo + "' ) ");
            }
            if (!numpol.Equals(string.Empty))
                condiciones.Add(" NUMPOL = " + numpol);

            if (!nombres.Equals(string.Empty) && !apellidos.Equals(string.Empty))
            {
                condiciones.Add(" IDEPOL IN ( SELECT DISTINCT IDEPOL FROM RESP_PAGO R, CLIENTE_TERCERO C WHERE R.CODCLI = C.CODCLI AND NOMTER LIKE '%" + nombres.ToUpper() + "%' AND APETER LIKE '%" + apellidos.ToUpper() + "%' ) ");
                condiciones.Add(" CODCLI IN ( SELECT CODCLI FROM CLIENTE_TERCERO WHERE NOMTER LIKE '%" + nombres.ToUpper() + "%' AND APETER LIKE '%" + apellidos.ToUpper() + "%' ) ");
            }
            else if (!nombres.Equals(string.Empty))
            {
                condiciones.Add(" IDEPOL IN ( SELECT DISTINCT IDEPOL FROM RESP_PAGO R, CLIENTE_TERCERO C WHERE R.CODCLI = C.CODCLI AND NOMTER LIKE '%" + nombres.ToUpper() + "%' ) ");
                condiciones.Add(" CODCLI IN ( SELECT CODCLI FROM CLIENTE_TERCERO WHERE NOMTER LIKE '%" + nombres.ToUpper() + "%' ) ");
            }
            else if (!apellidos.Equals(string.Empty))
            {
                condiciones.Add(" IDEPOL IN ( SELECT DISTINCT IDEPOL FROM RESP_PAGO R, CLIENTE_TERCERO C WHERE R.CODCLI = C.CODCLI AND APETER LIKE '%" + apellidos.ToUpper() + "%' ) ");
                condiciones.Add(" CODCLI IN ( SELECT CODCLI FROM CLIENTE_TERCERO WHERE APETER LIKE '%" + apellidos.ToUpper() + "%' ) ");
            }
            if (!esUsuarioCobros)
            {
                condiciones.Add(" ( SELECT DISTINCT CODINTER FROM FACTURA FA WHERE FA.IDEPOL = PO.IDEPOL) = " + codIntermediario);
            }
            foreach(string c in condiciones)
            {
                if (c.Equals(condiciones[0].ToString()))
                    consulta += c;
                else
                    consulta += " AND " + c;
            }
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = clConexiones.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(consulta, conexion);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codpol"></param>
        /// <param name="numpol"></param>
        /// <returns></returns>
        public DataTable busquedaDetallePoliza(string codpol, string numpol)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            string query = " SELECT CODPOL,NUMPOL,' del ' || to_char(FECINIVIG,'dd/mm/yyyy')|| ' al ' ||to_char(FECFINVIG,'dd/mm/yyyy') FECVIGENCIA,STSPOL,FECANUL,IDEPOL " +
                           " FROM POLIZA WHERE CODPOL = '" + codpol + "' AND NUMPOL = " + numpol +
                           " ORDER BY FECINIVIG DESC";
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
        public string descripEstadoPol(string tipolval)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            string query = " SELECT DESCRIP FROM LVAL WHERE TIPOLVAL = 'STSPOL' AND CODLVAL = '" + tipolval + "'";
            conexion = clConexiones.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(query, conexion);
            try
            {
                adapter.Fill(datos);
                conexion.Close();
                if (datos.Rows.Count > 0 && datos.Rows[0]["DESCRIP"] != null)
                    return datos.Rows[0]["DESCRIP"].ToString();
                else
                    return "";
            }
            catch (OracleException ex)
            {
                conexion.Close();
                return "";
            }
        }
        public DataTable busquedaDetalleVigencia(string idepol)
        {
            string tipoPoliza = obtenerTipoPoliza(idepol);
            return impresionQueryFactura(idepol, tipoPoliza);
        }
        public DataTable busquedaSaldosFavor(string idepol)
        {
            string tipoPoliza = obtenerTipoPoliza(idepol);
            return impresionQueryDescuentos(idepol, tipoPoliza);
        }

        #region creacion reporte
        /// <summary>
        /// FUNCIONES QUE RETORNAN DATASET PARA REPORTE DE ESTADO DE CUENTA
        /// Se envía si es de autos - daños o vida - gastos medicos
        /// Se elige el paquete y se realiza la consulta.
        /// </summary>
        /// <param name="idepol"></param>
        /// <param name="tipoPoliza"></param>
        /// <returns></returns>
        public DataTable impresionQueryPoliza(string idepol, string tipoPoliza)
        {
            DataTable polizaObtenida = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            cmd.Connection = conexionOracle;
            if(tipoPoliza.Equals("AUTODAÑO"))
            {
                cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.EC_AUTOS_DAÑOS_POLIZA";
            }
            else
            {
                cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.EC_VIDA_GM_POLIZA";
            }
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1_a_d = new OracleParameter("pIDEPOL", OracleDbType.Int32);
            OracleParameter retorno_a_d = new OracleParameter("Return_Value", OracleDbType.RefCursor);
            p1_a_d.Value = idepol;
            retorno_a_d.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno_a_d);
            cmd.Parameters.Add(p1_a_d);
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
        public DataTable impresionQueryFactura(string idepol, string tipoPoliza)
        {
            DataTable facturasObtenidas = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            cmd.Connection = conexionOracle;
            if (tipoPoliza.Equals("AUTODAÑO"))
            {
                cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.EC_AUTOS_DAÑOS_FACTURA";
            }
            else
            {
                cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.EC_VIDA_GM_FACTURA";
            }
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1_a_d = new OracleParameter("pIDEPOL", OracleDbType.Int32);
            OracleParameter retorno_a_d = new OracleParameter("Return_Value", OracleDbType.RefCursor);
            p1_a_d.Value = idepol;
            retorno_a_d.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno_a_d);
            cmd.Parameters.Add(p1_a_d);
            try
            {
                facturasObtenidas.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return facturasObtenidas;
        }
        public DataTable impresionQueryDescuentos(string idepol, string tipoPoliza)
        {
            DataTable descuentosObtenidos = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            cmd.Connection = conexionOracle;
            if (tipoPoliza.Equals("AUTODAÑO"))
            {
                cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.EC_AUTOS_DAÑOS_DESCUENTOS";
            }
            else
            {
                cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.EC_VIDA_GM_DESCUENTOS";
            }
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1_a_d = new OracleParameter("pIDEPOL", OracleDbType.Int32);
            OracleParameter retorno_a_d = new OracleParameter("Return_Value", OracleDbType.RefCursor);
            p1_a_d.Value = idepol;
            retorno_a_d.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno_a_d);
            cmd.Parameters.Add(p1_a_d);
            try
            {
                descuentosObtenidos.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return descuentosObtenidos;
        }
        public DataTable impresionQueryDescuentos2(string idepol, string tipoPoliza)
        {
            DataTable descuentosObtenidos = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            cmd.Connection = conexionOracle;
            if (tipoPoliza.Equals("AUTODAÑO"))
            {
                cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.EC_AUTOS_DAÑOS_DESCUENTOS2";
            }
            else
            {
                cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.EC_VIDA_GM_DESCUENTOS2";
            }
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1_a_d = new OracleParameter("pIDEPOL", OracleDbType.Int32);
            OracleParameter retorno_a_d = new OracleParameter("Return_Value", OracleDbType.RefCursor);
            p1_a_d.Value = idepol;
            retorno_a_d.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno_a_d);
            cmd.Parameters.Add(p1_a_d);
            try
            {
                descuentosObtenidos.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return descuentosObtenidos;
        }
        public DataTable impresionQueryIntermediario(string idepol, string tipoPoliza)
        {
            DataTable intermediarioObtenido = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            cmd.Connection = conexionOracle;
            if (tipoPoliza.Equals("AUTODAÑO"))
            {
                cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.EC_AUTOS_DAÑOS_INTERMEDIARIO";
            }
            else
            {
                cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.EC_VIDA_GM_INTERMEDIARIO";
            }
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1_a_d = new OracleParameter("pIDEPOL", OracleDbType.Int32);
            OracleParameter retorno_a_d = new OracleParameter("Return_Value", OracleDbType.RefCursor);
            p1_a_d.Value = idepol;
            retorno_a_d.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno_a_d);
            cmd.Parameters.Add(p1_a_d);
            try
            {
                intermediarioObtenido.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return intermediarioObtenido;
        }
        public DataTable impresionQueryTotales(string idepol, string tipoPoliza)
        {
            DataTable totalesObtenidos = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            cmd.Connection = conexionOracle;
            if (tipoPoliza.Equals("AUTODAÑO"))
            {
                cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.EC_AUTOS_DAÑOS_TOTALES";
            }
            else
            {
                cmd.CommandText = "PKG_COTIZADOR_WEB_REPORTES.EC_VIDA_GM_TOTALES";
            }
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1_a_d = new OracleParameter("pIDEPOL", OracleDbType.Int32);
            OracleParameter retorno_a_d = new OracleParameter("Return_Value", OracleDbType.RefCursor);
            p1_a_d.Value = idepol;
            retorno_a_d.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(retorno_a_d);
            cmd.Parameters.Add(p1_a_d);
            try
            {
                totalesObtenidos.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return totalesObtenidos;
        }
        public string obtenerCodProd(string idepol)
        {
            string query = "SELECT CODPROD FROM POLIZA WHERE IDEPOL = " + idepol;
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            OracleDataAdapter adapter;
            try
            {
                conexion = clConexiones.abrirConexionOracleAcsel();
                adapter = new OracleDataAdapter(query, conexion);
                adapter.Fill(datos);
                conexion.Close();
                if (datos.Rows.Count > 0 && datos.Rows[0]["CODPROD"] != null)
                    return datos.Rows[0]["CODPROD"].ToString();
                else
                    return "";
            }
            catch (OracleException ex)
            {
                conexion.Close();
                return "";
            }
        }
        public string obtenerIndpneteo(string idepol)
        {
            string query = "SELECT INDPNETEO FROM POLIZA WHERE IDEPOL = " + idepol;
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            OracleDataAdapter adapter;
            try
            {
                conexion = clConexiones.abrirConexionOracleAcsel();
                adapter = new OracleDataAdapter(query, conexion);
                adapter.Fill(datos);
                conexion.Close();
                if (datos.Rows.Count > 0 && datos.Rows[0]["INDPNETEO"] != null)
                    return datos.Rows[0]["INDPNETEO"].ToString();
                else
                    return "";
            }
            catch (OracleException ex)
            {
                conexion.Close();
                return "";
            }
        }
        public string obtenerTipoPoliza(string idepol)
        {
            string CODPROD = obtenerCodProd(idepol);
            string INDP = obtenerIndpneteo(idepol);
            string query = "SELECT " + 
                               "    CASE WHEN " + 
                               "         ( " +
                               "             pr_gastos_medicos.salud('" + CODPROD + "') = 'N' " + 
                               "         ) OR ( " + 
                               "             ( " +
                               "                 pr_gastos_medicos.salud('" + CODPROD + "') = 'S' " + 
                               "             ) AND ( " +
                               "                 nvl('" + INDP + "','N') = 'N' " + 
                               "             ) " + 
                               "         ) " + 
                               "     THEN " + 
                               "         1 " + 
                               "     ELSE " + 
                               "         0 " + 
                               "     END " + 
                           "    tipopoliza " + 
                           " FROM " + 
                           "    dual";
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            OracleDataAdapter adapter;
            try
            {
                conexion = clConexiones.abrirConexionOracleAcsel();
                adapter = new OracleDataAdapter(query, conexion);
                adapter.Fill(datos);
                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }
            if (datos.Rows.Count > 0)
            {
                string tipo = datos.Rows[0]["tipopoliza"].ToString();
                switch (tipo)
                {
                    case "1":
                        return "AUTODAÑO";
                    case "0":
                        return "VIDAMEDICO";
                    default:
                        return "";
                }
            }
            else
                return "";
        }
        #endregion
    }
}
