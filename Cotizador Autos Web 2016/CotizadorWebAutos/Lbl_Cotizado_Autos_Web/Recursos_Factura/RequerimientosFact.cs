
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Lbl_Cotizador_Autos_Web.ConexionesBD;
using Lbl_Cotizador_Autos_Web;
using MySql.Data.MySqlClient;
using Lbl_Cotizado_Autos_Web.Seguridad;
using Lbl_Cotizado_Autos_Web.Comunes;

namespace Lbl_Cotizador_Autos_Web.RecursosFactura
{
    public class RequerimientosFact

   {
        /// <summary>
        /// Función que busca polizas en paquete con el codigo de intermediario diferente de null
        /// Todos los paremetros pueden ser null excepto intermediario.
        /// </summary>
        /// <param name="cCODPOL">Codpol</param>
        /// <param name="nNUMPOL">Numpol</param>
        /// <param name="cNOMTER">Nombre con simbolos de %</param>
        /// <param name="cAPETER">Apellido con simbolos de %</param>
        /// <param name="nNUMID">Numid</param>
        /// <param name="cDVID">Dvid</param>
        /// <param name="cCodInter">Codigo de intermediario conectado</param>
        /// <param name="cPeriodo">Año seleccionado en combobox</param>
        /// <returns>tabla con valores de polizas encontradas</returns>
        public DataTable buscarPolizaPorIntermediario(string cCODPOL = null, int nNUMPOL = 0, string cNOMTER = null, string cAPETER = null, string nNUMID = null, string cDVID = null, string cCodInter = null, string cPeriodo = null)
        {
            DataTable datosCliente = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            cmd.Connection = conexionOracle;

            cmd.CommandText = "PK_COTIZADOR_WEB.FN_BUSCA_POLIZA";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("cCODPOL", OracleDbType.Varchar2);
            OracleParameter p2 = new OracleParameter("nNUMPOL", OracleDbType.Int32);
            OracleParameter p3 = new OracleParameter("cNOMTER", OracleDbType.Varchar2);
            OracleParameter p4 = new OracleParameter("cAPETER", OracleDbType.Varchar2);
            OracleParameter p5 = new OracleParameter("nNUMID", OracleDbType.Varchar2);
            OracleParameter p6 = new OracleParameter("cDVID", OracleDbType.Varchar2);
            OracleParameter p7 = new OracleParameter("cCodInter", OracleDbType.Varchar2);
            OracleParameter p8 = new OracleParameter("cPeriodo", OracleDbType.Varchar2);
            OracleParameter p9 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = cCODPOL;
            p2.Value = nNUMPOL;
            p3.Value = cNOMTER;
            p4.Value = cAPETER;
            p5.Value = nNUMID;
            p6.Value = cDVID;
            p7.Value = cCodInter;
            p8.Value = cPeriodo;

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
        /// <summary>
        /// Funcion que busca polizas en paquete con el codigo de intermediario null
        /// </summary>
        /// <param name="cCODPOL">Codpol</param>
        /// <param name="nNUMPOL">Numpol</param>
        /// <param name="cNOMTER">Nombre con simbolos de %</param>
        /// <param name="cAPETER">Apellido con simbolos de %</param>
        /// <param name="nNUMID">Numid</param>
        /// <param name="cDVID">Dvid</param>
        /// <param name="cPeriodo">Año seleccionado en combobox</param>
        /// <returns>tabla con valores de polizas encontradas</returns>
        public DataTable buscarPoliza(string cCODPOL = null, int nNUMPOL = 0, string cNOMTER = null, string cAPETER = null, string nNUMID = null, string cDVID = null, string cPeriodo = null)
        {
            DataTable datosCliente = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            cmd.Connection = conexionOracle;

            cmd.CommandText = "PK_COTIZADOR_WEB.FN_BUSCA_POLIZA";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("cCODPOL", OracleDbType.Varchar2);
            OracleParameter p2 = new OracleParameter("nNUMPOL", OracleDbType.Int32);
            OracleParameter p3 = new OracleParameter("cNOMTER", OracleDbType.Varchar2);
            OracleParameter p4 = new OracleParameter("cAPETER", OracleDbType.Varchar2);
            OracleParameter p5 = new OracleParameter("nNUMID", OracleDbType.Varchar2);
            OracleParameter p6 = new OracleParameter("cDVID", OracleDbType.Varchar2);
            OracleParameter p7 = new OracleParameter("cCodInter", OracleDbType.Varchar2);
            OracleParameter p8 = new OracleParameter("cPeriodo", OracleDbType.Varchar2);
            OracleParameter p9 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = cCODPOL;
            p2.Value = nNUMPOL;
            p3.Value = cNOMTER;
            p4.Value = cAPETER;
            p5.Value = nNUMID;
            p6.Value = cDVID;
            p7.Value = null;
            p8.Value = cPeriodo;

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
        /// <summary>
        /// Funcion que retorna las polizas buscadas por un usuario externo.
        /// Solo puede buscar sus propias polizas
        /// </summary>
        /// <param name="NUMID">Numid</param>
        /// <param name="DVID">Dvid</param>
        /// <param name="ANIO">anio seleccionado en combobox</param>
        /// <param name="CODPOL">codpol seleccionado en combobox</param>
        /// <param name="NUMPOL">numpol</param>
        /// <returns>Polizas de usuario</returns>
        public DataTable buscarPolizaRolConsultaFactura(string NUMID, string DVID,string ANIO = null, string CODPOL = null, string NUMPOL = null)
        {
            DataTable producto = new DataTable();
            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            string query = " SELECT DISTINCT F.IDEPOL, " +
                            "   F.CODPOL, " +
                            "   F.NUMPOL, " +
                            "   F.STSFACT, " +
                            "   P.STSPOL, " +
                            "   P.FECINIVIG, " +
                            "   P.FECFINVIG, " +
                            "   PR_MANTENIMIENTO.TERCERO_NOM_O_DIREC(F.TIPOID,F.NUMID,F.DVID,'N') NOMBRE " +
                            " FROM FACTURA F, " +
                            "   CORRELATIVO_FACTURA CF, " +
                            "   CLIENTE CL, " +
                            "   REL_ING RI, " +
                            "   POLIZA P " +
                            " WHERE F.CODFACT     = CF.CODFACT " +
                            " AND F.NUMRELING     = RI.NUMRELING " +
                            " AND TO_CHAR(RI.FECSTSRELING,'YYYY') = NVL ( '" + ANIO + "', TO_CHAR(RI.FECSTSRELING,'YYYY')) " +
                            " AND P.IDEPOL        = F.IDEPOL " +
                            " AND CL.TIPOID       = F.TIPOID " +
                            " AND CL.NUMID        = F.NUMID " +
                            " AND CL.DVID         = F.DVID " +
                            " AND CF.FACE        IS NOT NULL " +
                            " AND F.STSFACT       = 'COB' " +
                            " AND CL.STSCLI       = 'ACT' " +
                            " AND F.TIPOID        = 'NIT' " +
                            " AND F.NUMID         = " + NUMID +
                            " AND F.DVID          = '" + DVID + "' " +
                            " AND F.CODPOL        = NVL('" + CODPOL + "',F.CODPOL) " +
                            " AND F.NUMPOL        = NVL('" + NUMPOL + "',F.NUMPOL)";
            try
            { 
                conexion = conexionOracle.abrirConexionOracleAcsel();
                OracleDataAdapter Pdt = new OracleDataAdapter(query, conexion);
                Pdt.Fill(producto);
                conexion.Close();
            }
            catch(Exception ex)
            {
                conexion.Close();
                throw ex;
            }
            return producto;
        }
        /// <summary>
        /// Funcion que retorna el detalle de alguna factura. Busca todos los requerimientos
        /// </summary>
        /// <param name="idepol">Idepol </param>
        /// <param name="NUMID">Numid si tiene, puede ser null</param>
        /// <param name="DVID">Dvid si tiene, puede ser null</param>
        /// <returns>retorna lista de detalles de factura</returns>
        public DataTable buscarRequerimientosPoliza(int idepol, string NUMID, string DVID)
        {
            //OBTENIENDO DATOS 
            DataTable datos = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            string query = "SELECT DISTINCT (F.CODPOL || '-' || F.NUMPOL) POLIZA, " +
                            "   NVL(F.CODFACT, '**') SERIE, " +
                            "   F.IDEFACT, " +
                            "   F.NUMFACT FACTURA, " +
                            "   PR_MANTENIMIENTO.TERCERO_NOM_O_DIREC(F.TIPOID,F.NUMID,F.DVID,'N') NOMBRE, " +
                            "   TRUNC(RI.FECSTSRELING) FECHA_COBRO, " +
                            "   F.CODMONEDA, " +
                            "   F.MTOFACTMONEDA TOTALPRIMA, " +
                            "   F.IDEPOL " +
                            " FROM FACTURA F, " +
                            "   CORRELATIVO_FACTURA CF, " +
                            "   REL_ING RI, " +
                            "   CLIENTE CL, " +
                            "   POLIZA P " +
                            " WHERE F.CODFACT     = CF.CODFACT " +
                            " AND F.NUMRELING     = RI.NUMRELING " +
                            " AND P.IDEPOL        = F.IDEPOL " +
                            " AND CL.TIPOID       = F.TIPOID " +
                            " AND CL.NUMID        = F.NUMID " +
                            " AND CL.DVID         = F.DVID " +
                            " AND CF.FACE        IS NOT NULL " +
                            " AND F.STSFACT       = 'COB' " +
                            " AND CL.STSCLI       = 'ACT' " +
                            " AND F.TIPOID        = 'NIT' " +
                            " AND F.NUMID         = NVL('"+ NUMID +"',F.NUMID) " +
                            " AND F.DVID          = NVL('" + DVID + "',F.DVID) " +
                            " AND F.IDEPOL        = " + idepol;
            try{
                OracleDataAdapter adapter = new OracleDataAdapter(query, conexionOracle);
                adapter.Fill(datos);
                conexionOracle.Close();
            }
            catch (OracleException ex)
            {
                throw ex;
            }
            return datos;
        }

        //DESARROLLO CONSULTA DE FACTURAS - VICTORIA GUTIERREZ - VGUTIERREZ@MAPFRE.COM.GT - 15/07/2016
        //ORACLE
        //PARA BUSCAR DATOS DE FACTURAS, ESTE DEVUELVE LOS DATOS DEL CLIENTE BUSCANDOLO POR NIT, NOMBRE O CODIGO DE POLIZA
        public DataTable buscarDatoFactura(string cCODPOL = null, string nNUMPOL = null, string cNOMTER = null, string nNUMID = null, string cDVID = null, string cAnos = null, string fecha1 = null, string fecha2 = null, string intermediario = null)
            {
                DataTable datos = new DataTable();
                Conexiones clsConexion = new Conexiones();
                OracleConnection conexionOracle = new OracleConnection();
                conexionOracle = clsConexion.abrirConexionOracleAcsel();
                OracleDataAdapter adapter = new OracleDataAdapter("SELECT DISTINCT T.NIT, PR_MANTENIMIENTO.TERCERO_NOM_O_DIREC (t.tipoid, t.numid, t.dvid,'N') nombre,"
                         + " p.codpol, p.numpol FROM factura f, tercero t, poliza p, cliente c  WHERE f.tipoid  = t.tipoid AND f.numid   = t.numid"
                         + " AND f.dvid = t.dvid AND t.nit     = NVL ('" + nNUMID + cDVID +"', t.nit) AND f.stsfact = 'COB' AND f.idepol  = p.idepol AND t.tipoid  = c.tipoid "
                         + " AND t.numid   = c.numid AND t.dvid    = c.dvid"
                         + " AND pr.nombre_tercero (t.tipoid, t.numid, t.dvid) LIKE NVL('%" + cNOMTER + "%',pr.nombre_tercero (t.tipoid, t.numid, t.dvid)) AND P.CODPOL=NVL('" + cCODPOL + "',P.CODPOL)"
                         + " AND TO_CHAR(f.FECVENCFACT,'YYYY') = NVL ( '" + cAnos + "', TO_CHAR(f.FECVENCFACT,'YYYY')) "
                         + " AND f.FECVENCFACT BETWEEN NVL (TO_DATE ('" + fecha1 + "', 'YYYY/MM/DD'),f.FECVENCFACT) AND  NVL (TO_DATE ('" + fecha2 + "', 'YYYY/MM/DD'),f.FECVENCFACT)"
                         + " AND P.NUMPOL=NVL('" + nNUMPOL + "',P.NUMPOL) AND F.CODINTER=NVL('" + intermediario + "',F.CODINTER) ORDER BY nombre, p.codpol, p.numpol", conexionOracle);
                
                adapter.Fill(datos);
                conexionOracle.Close();
                return datos;
             }
        public DataTable buscarDatoFacturaConNit(string cCODPOL = null, string nNUMPOL = null, string cNOMTER = null, string nNUMID = null, string cDVID = null, string cAnos = null, string fecha1 = null, string fecha2 = null, string intermediario = null)
        {
            DataTable datos = new DataTable();
            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT DISTINCT pr.nombre_tercero (t.tipoid, t.numid, t.dvid) nombre,"
                    +" p.codpol, p.numpol, nit FROM factura f, tercero t, poliza p, cliente c WHERE f.tipoid  = t.tipoid AND f.numid = t.numid"
                    +" AND f.dvid = t.dvid AND t.nit = NVL ('" + nNUMID + cDVID + "', t.nit) AND f.stsfact = 'COB' AND f.idepol  = p.idepol AND t.tipoid  = c.tipoid"
                    +" AND t.numid = c.numid AND t.dvid = c.dvid AND TO_CHAR (f.fecvencfact, 'YYYY') = NVL('" + cAnos + "',TO_CHAR (f.fecvencfact, 'YYYY'))"
                    +" AND pr.nombre_tercero (t.tipoid, t.numid, t.dvid) LIKE '%" + cNOMTER + "%' AND P.CODPOL=NVL('" + cCODPOL + "',P.CODPOL) AND P.NUMPOL=NVL('" + nNUMPOL + "',P.NUMPOL)"
                     + " AND f.fecvencfact BETWEEN NVL (TO_DATE ('" + fecha1 + "', 'YYYY/MM/DD'),f.fecvencfact) AND  NVL (TO_DATE ('" + fecha2 + "', 'YYYY/MM/DD'),f.fecvencfact)"
                    +" AND F.CODINTER=NVL('" + intermediario + "',F.CODINTER)" , conexionOracle);

            adapter.Fill(datos);
            conexionOracle.Close();
            return datos;
        }
        /// <summary>
        /// Funcion que retorna la informacion de los requerimientos ingresados en textbox segun su permiso.
        /// Si tiene permiso colectivo, envia intermediario, sino, intermediario llega como null
        /// </summary>
        /// <param name="requerimientos">ids de requerimientos separados por coma</param>
        /// <param name="cAnos">anio seleccionado en combobox</param>
        /// <param name="intermediario">codigo de intermediario SI TIENE permiso FACTURA_COLETIVA</param>
        /// <returns></returns>
        public DataTable buscarPorReq(string requerimientos = null, string cAnos = null, string intermediario = null)
        {

            DataTable datosReq = new DataTable();
            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            string query = " SELECT (F.CODPOL || '-' || F.NUMPOL) POLIZA, " +
                            "   NVL(F.CODFACT, '**') SERIE, " +
                            "   F.IDEFACT, " +
                            "   F.NUMFACT FACTURA, " +
                            "   PR_MANTENIMIENTO.TERCERO_NOM_O_DIREC(F.TIPOID,F.NUMID,F.DVID,'N') NOMBRE, " +
                            "   TRUNC(RI.FECSTSRELING) FECHA_COBRO, " +
                            "   F.CODMONEDA, " +
                            "   F.MTOFACTMONEDA TOTALPRIMA, " +
                            "   F.IDEPOL " +
                            " FROM FACTURA F, " +
                            "   REL_ING RI " +
                            " WHERE F.STSFACT                 = 'COB' " +
                            " AND F.NUMRELING                 = RI.NUMRELING " +
                            " AND TO_CHAR(F.FECVENCFACT,'YYYY') = NVL ( '" + cAnos + "', TO_CHAR(F.FECVENCFACT,'YYYY')) " +
                            " AND F.CODINTER                  = NVL('" + intermediario + "', F.CODINTER) " +
                            " AND F.IDEFACT                  IN (" + requerimientos + ") " +
                            " ORDER BY RI.FECSTSRELING";
            try
            { 
                OracleDataAdapter factReq = new OracleDataAdapter(query, conexion);
                factReq.Fill(datosReq);
                conexion.Close();
            }
            catch(Exception ex)
            {
                conexion.Close();
                throw ex;
            }
            return datosReq;        
        }
        public DataTable buscarfacturacliente(string cCODPOL = null, string nNUMPOL = null, string CodCliNIT = null, string cAnos = null, string fecha1 = null, string fecha2 = null)
            {
                DataTable datosfac = new DataTable();
                Conexiones conexionOracle = new Conexiones();
                OracleConnection conexion = new OracleConnection();
                conexion = conexionOracle.abrirConexionOracleAcsel();
                OracleDataAdapter fact = new OracleDataAdapter(" SELECT POLIZA, serie ,factura, NOMBRE,  fecha_cobro, CODMONEDA, totalprima, IDEFACT"
                                         +" FROM (SELECT pl.codpol|| '-'|| to_char(pl.numpol) poliza,NUMFACT FACTURA,NVL(ft.codfact, '**') serie,pr_mantenimiento.TERCERO_NOM_O_DIREC(FT.TIPOID,FT.NUMID,FT.DVID,'N') NOMBRE,"
                                         +" (select fecstsreling from rel_ing where numreling=ft.numreling) fecha_cobro, ft.codmoneda, TO_CHAR(FACTURA_WEB(ft.idefact,'TOTALPRIMA'),'999999990.00') totalprima, ft.idefact"
                                         +" FROM poliza pl, factura ft, cliente cl"
                                         +" WHERE pl.idepol=ft.idepol AND ft.stsfact = 'COB' AND ft.tipoid = cl.tipoid AND ft.numid = cl.numid AND ft.dvid = cl.dvid"
                                         +" AND cl.codcli IN ( SELECT NVL (cli.codcliunico, cli.codcli) FROM tercero ter, cliente cli WHERE ter.nit = '" + CodCliNIT + "' "
                                         +" AND ter.tipoid = cli.tipoid AND ter.numid = cli.numid AND ter.dvid = cli.dvid )) WHERE poliza = '" + cCODPOL + "-" + nNUMPOL + "'"
                                         + " AND fecha_cobro BETWEEN NVL (TO_DATE ('" + fecha1 + "', 'YYYY/MM/DD'), fecha_cobro) AND  NVL (TO_DATE ('" + fecha2 + "', 'YYYY/MM/DD'),fecha_cobro)"
                                         +" AND TO_CHAR(FECHA_COBRO, 'YYYY') = NVL ('" + cAnos + "', TO_CHAR(FECHA_COBRO, 'YYYY'))"
                                         +" ORDER BY   TO_CHAR (fecha_cobro, 'YY') ASC, TO_CHAR (fecha_cobro, 'MM') ASC, TO_CHAR (fecha_cobro, 'DD') ASC" , conexion);
                    fact.Fill(datosfac);
                    conexion.Close();
                    return datosfac;
                 }
        public DataTable ImprimirFacturas(List<string> listadoFacturas) 
        {
            DataTable datosfac2 = new DataTable();
            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            string facturas = generarListadoFacturas(listadoFacturas);
            string query = "SELECT F.IDEFACT IDEFACT, " +
                            "   (F.CODPOL || '-' || F.NUMPOL) POLIZA, " +
                            "   TRUNC(RI.FECSTSRELING) FECHA_COBRO, " +
                            "   NVL(F.CODFACT, '**') SERIE, " +
                            "   F.NUMFACT FACTURA, " +
                            "   F.MTOFACTLOCAL TOTAL_DOLAR, " +
                            "   F.MTOFACTMONEDA PRIMATOTAL, " +
                            "   PR_FACTURA.DETALLE_FACTURA(F.IDEFACT,'F') CONCEPTO, " +
                            "   PR_MANTENIMIENTO.NOMBRE_AGENTE(F.CODINTER) NOMBRE_AGENTE, " +
                            "   F.CAE CAE, " +
                            "   F.CODMONEDA COD_MONEDA, " +
                            "   NVL(CL.CODCLIUNICO,CL.CODCLI) CODIGO_CLIENTE, " +
                            "   F.CODFACT CODFACT, " +
                            "   F.NUMFACT NUMFACT, " +
                            "   CF.NUMFACTFIN NUMFACTFIN, " +
                            "   CF.NORESOLUCION NORESOLUCION, " +
                            "   CF.FECRESOLUCION FECRESOLUCION, " +
                            "   CF.IMPRENTA IMPRENTA, " +
                            "   CF.NITIMPRENTA NITIMPRENTA, " +
                            "   CF.FACE FACE, " +
                            "   CF.DISPOSITIVO DISPOSITIVO, " +
                            "   PR_MANTENIMIENTO.TERCERO_NOM_O_DIREC(F.TIPOID,F.NUMID,F.DVID,'D') DIRECCIONCLIENTE, " +
                            "   PR_MANTENIMIENTO.RETORNA_NIT(F.TIPOID,F.NUMID,F.DVID) NIT, " +
                            "   PR_MANTENIMIENTO.TERCERO_NOM_O_DIREC(F.TIPOID,F.NUMID,F.DVID,'N') NOMBRE, " +
                            "   CF.PERIODO PERIODO, " +
                            "   UPPER(PR_MONTO_ESCRITO.MTO_ESCRITO_MONEDA(ROUND(F.MTOFACTLOCAL,2),F.CODMONEDA)) TOTAL_LETRAS, " +
                            "   TO_CHAR(RI.FECSTSRELING,'MM') " +
                            " FROM FACTURA F, " +
                            "   CORRELATIVO_FACTURA CF, " +
                            "   REL_ING RI, " +
                            "   CLIENTE CL " +
                            " WHERE F.CODFACT     = CF.CODFACT " +
                            " AND F.NUMRELING     = RI.NUMRELING " +
                            " AND CL.TIPOID       = F.TIPOID " +
                            " AND CL.NUMID        = F.NUMID " +
                            " AND CL.DVID         = F.DVID " +
                            " AND CL.STSCLI       = 'ACT' " +
                            " AND CF.FACE        IS NOT NULL " +
                            " AND F.IDEFACT       in (" + facturas + ")";
            try {
                OracleDataAdapter fact2 = new OracleDataAdapter(query, conexion);
                fact2.Fill(datosfac2);
                conexion.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return datosfac2;
        }
        public DataTable ImprimirFacturaIndividual(string IDEFACT = null)
        {
            DataTable datosfac2 = new DataTable();
            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            string query = "SELECT F.IDEFACT IDEFACT, " +
                            "   (F.CODPOL || '-' || F.NUMPOL) POLIZA, " +
                            "   TRUNC(RI.FECSTSRELING) FECHA_COBRO, " +
                            "   NVL(F.CODFACT, '**') SERIE, " +
                            "   F.NUMFACT FACTURA, " +
                            "   F.MTOFACTLOCAL TOTAL_DOLAR, " +
                            "   F.MTOFACTMONEDA PRIMATOTAL, " +
                            "   PR_FACTURA.DETALLE_FACTURA(F.IDEFACT,'F') CONCEPTO, " +
                            "   PR_MANTENIMIENTO.NOMBRE_AGENTE(F.CODINTER) NOMBRE_AGENTE, " +
                            "   F.CAE CAE, " +
                            "   F.CODMONEDA COD_MONEDA, " +
                            "   NVL(CL.CODCLIUNICO,CL.CODCLI) CODIGO_CLIENTE, " +
                            "   F.CODFACT CODFACT, " +
                            "   F.NUMFACT NUMFACT, " +
                            "   CF.NUMFACTFIN NUMFACTFIN, " +
                            "   CF.NORESOLUCION NORESOLUCION, " +
                            "   CF.FECRESOLUCION FECRESOLUCION, " +
                            "   CF.IMPRENTA IMPRENTA, " +
                            "   CF.NITIMPRENTA NITIMPRENTA, " +
                            "   CF.FACE FACE, " +
                            "   CF.DISPOSITIVO DISPOSITIVO, " +
                            "   PR_MANTENIMIENTO.TERCERO_NOM_O_DIREC(F.TIPOID,F.NUMID,F.DVID,'D') DIRECCIONCLIENTE, " +
                            "   PR_MANTENIMIENTO.RETORNA_NIT(F.TIPOID,F.NUMID,F.DVID) NIT, " +
                            "   PR_MANTENIMIENTO.TERCERO_NOM_O_DIREC(F.TIPOID,F.NUMID,F.DVID,'N') NOMBRE, " +
                            "   CF.PERIODO PERIODO, " +
                            "   UPPER(PR_MONTO_ESCRITO.MTO_ESCRITO_MONEDA(ROUND(F.MTOFACTLOCAL,2),F.CODMONEDA)) TOTAL_LETRAS, " +
                            "   TO_CHAR(RI.FECSTSRELING,'MM') " +
                            " FROM FACTURA F, " +
                            "   CORRELATIVO_FACTURA CF, " +
                            "   REL_ING RI, " +
                            "   CLIENTE CL " +
                            " WHERE F.CODFACT     = CF.CODFACT " +
                            " AND F.NUMRELING     = RI.NUMRELING " +
                            " AND CL.TIPOID       = F.TIPOID " +
                            " AND CL.NUMID        = F.NUMID " +
                            " AND CL.DVID         = F.DVID " +
                            " AND CL.STSCLI       = 'ACT' " +
                            " AND CF.FACE        IS NOT NULL " +
                            " AND F.IDEFACT       in (" + IDEFACT + ")";

            OracleDataAdapter fact2 = new OracleDataAdapter(query, conexion);
            
            fact2.Fill(datosfac2);
            conexion.Close();
            return datosfac2;
        }
        private string generarListadoFacturas(List<string> facturas)
        {
            string resultado = string.Empty;
            for (int i=0 ; i < facturas.Count(); i++)
            {
                if (i == facturas.Count() - 1)
                {
                    resultado += facturas[i];
                }
                else{
                    resultado += facturas[i] + ",";
                }
            }
            return resultado;
        }
        public DataTable Productos() {
                DataTable producto = new DataTable();

                Conexiones conexionOracle = new Conexiones();
                OracleConnection conexion = new OracleConnection();
                conexion = conexionOracle.abrirConexionOracleAcsel();


                OracleDataAdapter Pdt = new OracleDataAdapter("SELECT DISTINCT CODPOL FROM POLIZA WHERE STSPOL = 'ACT' ORDER BY CODPOL", conexion);
                
                Pdt.Fill(producto);
                
                conexion.Close();

                return producto;
            
            }
        //Este es para buscar las polizas colectivas asociados a un cliente (ejemplo: SEGUROS COLUMNA)
        public DataTable BuscandopolizaSeguroColectivo(string numid = null, string dvid = null)
        {
            DataTable producto = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();


            OracleDataAdapter Pdt = new OracleDataAdapter("SELECT DISTINCT CODPOL, NUMPOL FROM POLIZA WHERE CODCLI IN (SELECT NVL (cli.codcliunico, cli.codcli) FROM tercero ter,"
                + " cliente cli WHERE ter.NUMID = '" + numid + "' AND ter.DVID='" + dvid + "' AND ter.tipoid = cli.tipoid AND ter.numid = cli.numid AND ter.dvid = cli.dvid )", conexion);

            Pdt.Fill(producto);

            conexion.Close();

            return producto;

        }

        //MYSQL
        public DataTable sacandoUsuarioNit(string IDUSUARIO = null)
            {
                DataTable resultadoURemo = new DataTable();
                

                Conexiones clConexiones = new Conexiones();
                MySqlConnection conexion = new MySqlConnection();

                conexion = clConexiones.abrirConexionMysql();
                MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT numid, dvid FROM usuario where id_usuario = '" + IDUSUARIO + "'", conexion);
                
                    adapter.Fill(resultadoURemo);

                    conexion.Close();

                    return resultadoURemo;
            
            }
        public DataTable SancandoUsuarioFactura(string numidfact = null, string dvidfact = null)
        {
            DataTable resultadoURemo = new DataTable();
            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();



                OracleDataAdapter fact = new OracleDataAdapter("select * from USUARIO_REMOTO where numid = '" + numidfact + "' and dvid = '" + dvidfact + "'", conexion);
                fact.Fill(resultadoURemo);
                conexion.Close();
                return resultadoURemo;
        }
        public DataTable comboañosCONSULTA_FACTURA(string NIT = null)
        {
            DataTable datosNit2 = new DataTable();


            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            OracleDataAdapter adapter3 = new OracleDataAdapter("SELECT DISTINCT TO_CHAR (fecvencfact, 'YYYY') FECHA from factura"
                               +" WHERE NUMID || DVID = '" + NIT + "'"
                               +" AND STSFACT = 'COB' ORDER BY FECHA DESC ", conexionOracle);

            adapter3.Fill(datosNit2);


            conexionOracle.Close();

            return datosNit2;



        }
        /// HASTA AQUI CONSULTA DE FACTURAS--------------------------------------------------------------------------------------------
        //DESARROLLO PARA CREACIÓN DE USUARIO SOLO PARA CONSULTA DE FACTURAS - VICTORIA GUTIERREZ
        public DataTable ValidarDatoNU(string cCODPOL = null, string nNUMPOL = null, string nNUMID = null, string cDVID = null, string Correo = null, string NOMTER = null, string APTER = null)
        {
            DataTable DATOSBF = new DataTable();

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT DISTINCT T.NUMID, T.DVID, CODINTER, TELEX, DIR_COBRO, t.NOMTER, t.APETER, t.TELEF1, p.CODPOL, P.NUMPOL"
                                      +"  FROM factura f, tercero t, poliza p, cliente c WHERE f.tipoid  = t.tipoid AND f.numid   = t.numid "
                                      +" AND f.dvid = t.dvid AND T.NUMID = NVL('" + nNUMID + "', T.NUMID ) AND T.DVID = NVL('" + cDVID + "', t.DVID)"
                                      +" AND P.CODPOL= NVL('" + cCODPOL + "', P.CODPOL) AND P.NUMPOL= NVL('" + nNUMPOL + "', P.NUMPOL) "
                                      +" AND t.TELEX = NVL('" + Correo + "', T.TELEX) AND t.NOMTER LIKE NVL('" + NOMTER + "%',  t.NOMTER) AND t.APETER LIKE NVL('" + APTER + "%',  t.APETER)"
                                      +" AND f.idepol  = p.idepol AND t.tipoid  = c.tipoid AND t.numid   = c.numid AND t.dvid = c.dvid", conexionOracle);
           
            adapter.Fill(DATOSBF);


            conexionOracle.Close();

            return DATOSBF;
        }
        public DataTable PolizaDatoNU(string cCODPOL = null, string nNUMPOL = null, string nNUMID = null, string cDVID = null, string Correo = null, string NOMTER = null, string APTER = null)
        {
            DataTable DATOSBF = new DataTable();

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT DISTINCT p.CODPOL || P.NUMPOL AS POLIZA"
                                      + "  FROM factura f, tercero t, poliza p, cliente c WHERE f.tipoid  = t.tipoid AND f.numid   = t.numid "
                                      + " AND f.dvid = t.dvid AND T.NUMID = NVL('" + nNUMID + "', T.NUMID ) AND T.DVID = NVL('" + cDVID + "', t.DVID)"
                                      + " AND P.CODPOL= NVL('" + cCODPOL + "', P.CODPOL) AND P.NUMPOL= NVL('" + nNUMPOL + "', P.NUMPOL) "
                                      + " AND t.TELEX = NVL('" + Correo + "', T.TELEX) AND t.NOMTER LIKE NVL('" + NOMTER + "%',  t.NOMTER) AND t.APETER LIKE NVL('" + APTER + "%',  t.APETER)"
                                      + " AND f.idepol  = p.idepol AND t.tipoid  = c.tipoid AND t.numid   = c.numid AND t.dvid = c.dvid", conexionOracle);

            adapter.Fill(DATOSBF);


            conexionOracle.Close();

            return DATOSBF;
        }
        public int agregarNuevoUsuario2(NuevoUsuario Usuario)
        {
            int idUsuarioCreado = 0;
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
          

            


            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();

                string query = "INSERT INTO USUARIO (nombres, apellidos, telefono, correo_electronico, nombre_unico_usuario, clave_unica_usuario, codigo_intermediario, codigo_usuario_remoto, "
                + " usuario_interno, id_tipo_usuario, poliza_matriz_facturas, fecha_creacion_usuario, estado, numid, dvid)"
                + " VALUES('" + Usuario.NOMBRES + "', '" + Usuario.APELLIDOS + "', '" + Usuario.TELEFONO + "', '" + Usuario.CORREO_ELECTRONICO + "', '" + Usuario.NOMBRE_UNICO_USUARIO + "', " + "sha1('" + Usuario.PASSWORD + "'), " + Usuario.CODIGO_INTERMEDIARIO + ", " + Usuario.CODIGO_USUARIO_REMOTO + ", " + Usuario.USUARIO_INTERNO + ",1, NULL, NOW(), TRUE,"+Usuario.NUMID+","+Usuario.DVID+")";


                command.CommandText = query;

                command.ExecuteNonQuery();

                idUsuarioCreado = int.Parse(command.LastInsertedId.ToString());

                conexion.Close();
            }
            catch (MySqlException ex)
            {

                throw ex;
            }
            return idUsuarioCreado;
        }
        public void agregarRolAccesoFACTURA(int idUsuario)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            Varias clVarias = new Varias();
            int IDPLANFAC = clVarias.obtenerIdRolAcceso("DESCARGA_FACTURA");
            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();

                string query = "insert into usuario_rol_acceso "
                       + " values (" + idUsuario + ", " + IDPLANFAC + ", TRUE)";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

                command.CommandText = query;

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
