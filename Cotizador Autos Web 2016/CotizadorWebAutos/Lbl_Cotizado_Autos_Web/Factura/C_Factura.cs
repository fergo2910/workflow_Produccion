using Lbl_Cotizador_Autos_Web.ConexionesBD;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.Factura
{
    class C_Factura
    {

        #region Oracle

        public string ConsultandoFactura()
        {
            string NitInicio = string.Empty;
            string NitFinal = string.Empty;
            string IdePol = string.Empty;
            string desde = string.Empty;
            string hasta = string.Empty;
            string CodProd = string.Empty;
            string CodPol = string.Empty;
            string NumPol = string.Empty;


            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();

            conexion = clConexiones.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" SELECT LPAD(MAX(codusuarioremo)+1,6,'0') CODUSUARIOREMOTO FROM USUARIO_REMOTO;", conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    codigoUsuarioRemoto = datos.Rows[0]["CODUSUARIOREMOTO"].ToString();
                }

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return codigoUsuarioRemoto;
        }
        public void agregarUsuarioRemotoOracle(NuevoUsuario Usuario)
        {
            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();

            try
            {
                conexion = clConexiones.abrirConexionOracleAcsel();

                OracleCommand command = conexion.CreateCommand();

                string nombres = Usuario.NOMBRES + " " + Usuario.APELLIDOS;

                command.CommandText = " INSERT INTO usuario_remoto(TIPOID,NUMID,DVID,NOMBRE,DIRECCION,IDUSUARIO,PASSWORD,"
                                        + "EMAIL,CODUSUARIOREMO,CODINTER,INDTIPOENT,INDTIPOUSUARIO)"
                                        + " VALUES ('NIT', '" + Usuario.NUMID + "', '" + Usuario.DVID + "', '" + nombres + "', '" + Usuario.DIRECCION + "', '" + Usuario.NOMBRE_UNICO_USUARIO + "', '" + Usuario.PASSWORD + "', '" + Usuario.CORREO_ELECTRONICO + "', '" + Usuario.CODIGO_USUARIO_REMOTO + "', '" + Usuario.CODIGO_INTERMEDIARIO + "', 'E', '" + Usuario.USUARIO_INTERNO + "')";

                //command.Parameters.Add("@1", "'NIT'");
                //command.Parameters.Add("@2", Usuario.NUMID);
                //command.Parameters.Add("@3", Usuario.DVID);
                //command.Parameters.Add("@4", Usuario.NOMBRES + " " + Usuario.APELLIDOS);
                //command.Parameters.Add("@5", Usuario.DIRECCION);
                //command.Parameters.Add("@6", Usuario.NOMBRE_UNICO_USUARIO);
                //command.Parameters.Add("@7", Usuario.PASSWORD);
                //command.Parameters.Add("@8", Usuario.CORREO_ELECTRONICO);
                //command.Parameters.Add("@9", Usuario.CODIGO_USUARIO_REMOTO);
                //command.Parameters.Add("@10", Usuario.CODIGO_INTERMEDIARIO);
                //command.Parameters.Add("@11", "E");

                //if (Usuario.USUARIO_INTERNO == "1")
                //{
                //    command.Parameters.Add("@12", "1");
                //}
                //else
                //{
                //    command.Parameters.Add("@12", "2");
                //}                              

                command.ExecuteNonQuery();

                conexion.Close();
            }
            catch (MySqlException ex)
            {

                throw ex;
            }
        }
        public bool nitValidoCliente(int numID, string dvID)
        {
            bool respuesta = false;
            string resultado = string.Empty;

            OracleConnection conexion = new OracleConnection();
            Conexiones clConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "PR_MANTENIMIENTO.DIGITO_VERIFICADOR";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nnumnit", OracleDbType.Int32);
                OracleParameter p2 = new OracleParameter("Return_Value", OracleDbType.Varchar2, 32767);

                p1.Value = numID;

                p2.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p1);


                cmd.ExecuteNonQuery();

                resultado = cmd.Parameters["Return_Value"].Value.ToString();

                if (dvID.ToUpper() == resultado)
                {
                    respuesta = true;
                }
            }
            catch (OracleException ex)
            {
                throw ex;
            }

            return respuesta;
        }
        public DataTable obtenerIntermediario(string codigoIntermediario)
        {
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            OracleConnection conexion = new OracleConnection();

            conexion = clConexiones.abrirConexionOracleAcsel();

            string query = "select * from intermediario where codinter = '" + codigoIntermediario + "'";
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

        #endregion
    }
}
