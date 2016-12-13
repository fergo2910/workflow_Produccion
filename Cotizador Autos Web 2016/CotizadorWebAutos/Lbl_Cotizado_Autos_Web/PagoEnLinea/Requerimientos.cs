using Lbl_Cotizador_Autos_Web.ConexionesBD;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lbl_Cotizado_Autos_Web.PagoEnLinea
{
    public class Requerimientos
    {
        public DataTable buscarPolizaCliente(string cCODPOL = null, int nNUMPOL = 0, string cNOMTER = null, string cAPETER = null, string nNUMID = null, string cDVID = null, string cCodInter = null)
        {
            DataTable datosCliente = new DataTable();

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();

            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;

            cmd.CommandText = "PK_COTIZADOR_WEB.FN_REGRESA_POLIZA";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("cCODPOL", OracleDbType.Varchar2);
            OracleParameter p2 = new OracleParameter("nNUMPOL", OracleDbType.Int32);
            OracleParameter p3 = new OracleParameter("cNOMTER", OracleDbType.Varchar2);
            OracleParameter p4 = new OracleParameter("cAPETER", OracleDbType.Varchar2);
            OracleParameter p5 = new OracleParameter("nNUMID", OracleDbType.Varchar2);
            OracleParameter p6 = new OracleParameter("cDVID", OracleDbType.Varchar2);
            OracleParameter p7 = new OracleParameter("cCodInter", OracleDbType.Varchar2);
            OracleParameter p8 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = cCODPOL;
            p2.Value = nNUMPOL;
            p3.Value = cNOMTER;
            p4.Value = cAPETER;
            p5.Value = nNUMID;
            p6.Value = cDVID;
            p7.Value = cCodInter;

            p8.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p8);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);

            try
            {
                datosCliente.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return datosCliente;
        }
        public DataTable buscarRequerimientos(int idePol, int numOper)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();

            string query = "SELECT f.idefact, f.codfact, f.numfact, f.codmoneda moneda, "
                + " f.mtofactlocal Monto, f.stsfact Estado, r.FECSTSRELING Fecha_Cobro, "                
                + " pr.nombre_tercero(f.tipoid, f.numid, f.dvid) RespPago"
                + " FROM factura f, rel_ing r"
                + " WHERE f.IDEPOL = " + idePol
                + " and f.numoper = " + numOper
                + " and r.numreling (+) = f.numreling";

            OracleDataAdapter adapter = new OracleDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {

            }

            return datos;
        }
        public DataTable buscarRequerimientosPoliza(int idepol)
        {
            DataTable datosCliente = new DataTable();

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();

            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;

            cmd.CommandText = "PK_COTIZADOR_WEB.FN_REGRESA_REQS";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("nNUMPOL", OracleDbType.Int32);           
            OracleParameter p2 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = idepol;
           
            p2.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p1);           

            try
            {
                datosCliente.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return datosCliente;
        }
        public string generarXMLCobro(List<REQUERIMIENTO> listadoRequerimientos, List<DOCING> listaDocing)
        {
            var xmlfromLINQ = new XElement("DATOS",
                                
                       from c in listadoRequerimientos
                       select new XElement("REQUERIMIENTO",
                        new XElement("IDEFACT", c.IDEFACT)),
                        from d in listaDocing
                            select new XElement("DOCING", 
                                new XElement("TIPODOCING", d.TIPODOCING), 
                                new XElement("CODMONEDA", d.CODMONEDA),
                                new XElement("MONTO", d.MONTO),
                                new XElement("CODENTFINAN", d.CODENTFINAN),
                                new XElement("NUMREFDOC", d.NUMREFDOC),
                                new XElement("CLAVEAUTDOCING", d.CLAVEAUTDOCING)));

            return xmlfromLINQ.ToString();
        }
        public string CobrarTC(string pCodProd, string pNumPol, string pNumCert,
                                string pCodEntFinan, string pCardNumber, string pMonto, 
                                string pFecVencTDC, string pCodUsr)
        {
           
            string resultado = string.Empty;

            OracleConnection conexion = new OracleConnection();
            Conexiones clConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "PR_TDC_SAS.DEBITO_TDC";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("pCodProd", OracleDbType.Varchar2, 32767);
                OracleParameter p2 = new OracleParameter("pNumPol", OracleDbType.Int32);
                OracleParameter p3 = new OracleParameter("pNumCert", OracleDbType.Int32);
                OracleParameter p4 = new OracleParameter("pCodEntFinan", OracleDbType.Varchar2, 32767);
                OracleParameter p5 = new OracleParameter("pCardNumber", OracleDbType.Varchar2, 32767);
                OracleParameter p6 = new OracleParameter("pMonto", OracleDbType.Double);
                OracleParameter p7 = new OracleParameter("pFecVencTDC", OracleDbType.Varchar2, 32767);
                OracleParameter p8 = new OracleParameter("pCodUsr", OracleDbType.Varchar2, 32767);
                OracleParameter p9 = new OracleParameter("Return_Value", OracleDbType.XmlType, 32767);

                p1.Value = pCodProd;
                p2.Value = Convert.ToInt32(pNumPol);
                p3.Value = Convert.ToInt32(pNumCert);
                p4.Value = pCodEntFinan;
                p5.Value = pCardNumber;
                p6.Value = Convert.ToDouble(pMonto);
                p7.Value = "01/" + pFecVencTDC;
                p8.Value = pCodUsr;
                p9.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(p9);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                cmd.Parameters.Add(p6);
                cmd.Parameters.Add(p7);
                cmd.Parameters.Add(p8);

                cmd.ExecuteNonQuery();

                resultado = ((OracleXmlType)cmd.Parameters["Return_Value"].Value).Value;

                conexion.Close();

            }
            catch (OracleException ex)
            {
                throw ex;                
            }

            return resultado;
        }
        public string CobrarAcsel(string xmlfactura)
        {
            string resultado = string.Empty;

            OracleConnection conexion = new OracleConnection();
            Conexiones clConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "pr_cobro_web.cobrar";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("xmlfactura", OracleDbType.Clob);
                OracleParameter p2 = new OracleParameter("Return_Value", OracleDbType.Clob);

                p1.Value = xmlfactura;
                p2.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p1);

                cmd.ExecuteNonQuery();

                resultado = (((OracleClob)cmd.Parameters["Return_Value"].Value)).Value;
            }
            catch (OracleException ex)
            {
                throw ex;
            }

            return resultado;
        }
        public DataTable ObtenerInformacionTerminalCobro(string pCodProdPoliza)
        {
            DataTable resultado = new DataTable();
            DataTable datosVehiculo = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT * FROM configuracion_terminales_cobro where codigo_producto_poliza = '" + pCodProdPoliza + "'", conexion);
            try
            {
                adapter.Fill(resultado);
            }
            catch (MySqlException ex)
            {
                throw ex;
            }

            return resultado;
        }
        public DataTable obtenerOperacionesPoliza(int numPol, string codPol)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();

            string query = "select p.idepol,  "
                            + "pr.nombre_cliente(P.codcli) nombre, "
                            + " op.tipoop, "
                            + " op.numcert, "
                            + " op.numoper, "
                            + " op.fecmov, "
                            + " op.mtooper, "
                            + " Op.Mtooperanu "
                            + " from poliza P, oper_pol op "
                            + " where P.codpol  = '" + codPol + "' "
                            + " AND P.NUMPOL = " + numPol
                            + " and p.idepol = op.idepol "
                            + "AND EXISTS (SELECT DISTINCT 1 FROM factura f WHERE f.NumOper = op.NUmOPer) ";

            OracleDataAdapter adapter = new OracleDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {

            }

            return datos;
        }
        public DataTable ObtenerTipoCuenta()
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" SELECT descrip descripcion,codlval codigo FROM LVAL " +
                                                              " WHERE TIPOLVAL = 'DOCPCLI' " +
                                                              " AND codlval != 'CTT' " +
                                                              " AND codlval != 'CTA' " +
                                                              " AND codlval != 'TCC' " +
                                                              " ORDER BY 1 ", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {

            }

            return datos;
        }
        public DataTable ObtenerBancoCuenta()
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" SELECT descrip descripcion,codlval codigo FROM LVAL " +
                                                              " WHERE TIPOLVAL = 'ENTFIN' " +
                                                              " ORDER BY 1 ", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {

            }

            return datos;
        }
        public DataTable ObtenerTipoTarjeta()
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" SELECT descrip descripcion,codlval codigo FROM LVAL " +
                                                              " WHERE TIPOLVAL = 'TIPTARCR' " +
                                                              " ORDER BY 1 ", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {

            }

            return datos;
        }
    }    
}
