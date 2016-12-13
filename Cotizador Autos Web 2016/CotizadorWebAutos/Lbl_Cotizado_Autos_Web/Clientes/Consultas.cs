using Lbl_Cotizador_Autos_Web.ConexionesBD;
using Oracle.DataAccess.Client;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.Clientes
{

    struct clsCli
       {
            public string CodCli;
            public DateTime FechaNac;
       }
   public class Consultas
    {
       public DataTable obtenerDatosClienteNitJuridico(int numID, string dvID)
       {
           DataTable datosCliente = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           cmd.Connection = conexionOracle;
           //cmd.CommandText = "PK_COTIZADOR_WEB.FN_OBTENER_DATOS_CLI";
           cmd.CommandText = "PK_COTIZADOR_WEB.FN_OBTENER_DATOS_JURIDICO";
           cmd.CommandType = CommandType.StoredProcedure;
           OracleParameter p1 = new OracleParameter("pNUMID", OracleDbType.Int32);
           OracleParameter p2 = new OracleParameter("pDVID", OracleDbType.Varchar2);
           OracleParameter p3 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

           p1.Value = numID;
           p2.Value = dvID;

           p3.Direction = ParameterDirection.ReturnValue;

           cmd.Parameters.Add(p3);
           cmd.Parameters.Add(p1);
           cmd.Parameters.Add(p2);

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
       public DataTable obtenerDatosClienteNit(int numID, string dvID)
       {
           DataTable datosCliente = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           cmd.Connection = conexionOracle;
           //cmd.CommandText = "PK_COTIZADOR_WEB.FN_OBTENER_DATOS_CLI";
           cmd.CommandText = "PK_COTIZADOR_WEB.FN_OBTENER_DATOS";
           cmd.CommandType = CommandType.StoredProcedure;
           OracleParameter p1 = new OracleParameter("pNUMID", OracleDbType.Int32);
           OracleParameter p2 = new OracleParameter("pDVID", OracleDbType.Varchar2);
           OracleParameter p3 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

           p1.Value = numID;
           p2.Value = dvID;

           p3.Direction = ParameterDirection.ReturnValue;

           cmd.Parameters.Add(p3);
           cmd.Parameters.Add(p1);
           cmd.Parameters.Add(p2);

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
       public bool confirmaNitCliente(int numID, string dvID)
       {
           bool respuesta = false;
           string resultado = string.Empty;

           OracleConnection conexion = new OracleConnection();
           Conexiones clConexion = new Conexiones();
           OracleCommand cmd = new OracleCommand();

           try
           {             
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
               cmd.Connection.Close();

               if (dvID.ToUpper() == resultado)
               {
                   respuesta = true;
               }
           }
           catch (OracleException ex)
           {
               cmd.Connection.Close();
               throw ex;
           }

           return respuesta;
       }
       public DataTable obtenerDireccionesCliente(string codCli)
       {
           DataTable datosCliente = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           cmd.Connection = conexionOracle;
           cmd.CommandText = "PK_COTIZADOR_WEB.FN_OBTENER_DIREC_CLI";
           cmd.CommandType = CommandType.StoredProcedure;
           OracleParameter p1 = new OracleParameter("pCodCLI", OracleDbType.Varchar2);
           OracleParameter p3 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

           p1.Value = codCli;

           p3.Direction = ParameterDirection.ReturnValue;

           cmd.Parameters.Add(p3);
           cmd.Parameters.Add(p1);


           datosCliente.Load(cmd.ExecuteReader());
           cmd.Connection.Close();

           return datosCliente;
       }
       public DataTable obtenerTelefonosCliente(string codCli)
       {
           DataTable datosCliente = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           cmd.Connection = conexionOracle;
           cmd.CommandText = "PK_COTIZADOR_WEB.FN_OBTENER_TELEF_CLI";
           cmd.CommandType = CommandType.StoredProcedure;
           OracleParameter p1 = new OracleParameter("pCodCLI", OracleDbType.Varchar2);
           OracleParameter p3 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

           p1.Value = codCli;

           p3.Direction = ParameterDirection.ReturnValue;

           cmd.Parameters.Add(p3);
           cmd.Parameters.Add(p1);


           datosCliente.Load(cmd.ExecuteReader());
           cmd.Connection.Close();

           return datosCliente;
       }

       public DateTime ObtenerBitacoraCliente(string pCodCli)
       {
           DateTime dFechaBitacora = DateTime.Now;

           dFechaBitacora = dFechaBitacora.AddDays(-365);
           
           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           OracleDataReader Reader;
           conexionOracle = clsConexion.abrirConexionOracleAcsel();


           try
           {
               cmd.Connection = conexionOracle;
               cmd.CommandText = "SELECT NVL(FECFINVIGENCIA,SYSDATE-365) " +
                                 "   FROM BITACORA_IVE_CLIENTE " +
                                 "   WHERE CODCLI   = '" + pCodCli + "'" +
                                 "     AND ORDEN    = (SELECT max(orden) " +
                                 "                       FROM bitacora_ive_cliente " +
                                 "                       WHERE codcli = '" + pCodCli + "')";

               Reader = cmd.ExecuteReader();
               if (Reader.Read())
                   dFechaBitacora = Reader.GetDateTime(0);
               Reader.Close();
               cmd.Connection.Close();
           }           
           catch (OracleException ex)
           {
               cmd.Connection.Close();
               throw ex;
           }

           return dFechaBitacora;
       }

       public Boolean ConsultaUsuarioInterno(string pIdUsuario)
       {
           Boolean UsuarioInterno = true;
           Conexiones clConexiones = new Conexiones();
           MySqlConnection conexion = new MySqlConnection();
           

           conexion = clConexiones.abrirConexionMysql();
           MySqlCommand cmd = new MySqlCommand("  SELECT usuario_interno from usuario where id_usuario = " + pIdUsuario, conexion);

           try
           {
               MySqlDataReader Reader;
               Reader = cmd.ExecuteReader();
               if (Reader.Read())               
                   UsuarioInterno = Reader.GetBoolean(0);
               Reader.Close();
               conexion.Close();
           }

           catch (MySqlException ex)
           {
               conexion.Close();
               throw ex;
           }

           return UsuarioInterno;
       }

       
       public void EnviaCorreoActualizacionDatos(string pCodCli, EstructuraClienteIndividual nuevoCliente, string Telefono, string pTipoCliente, EstructuraClienteJuridico nuevoJuridico)
       {
           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           cmd.Connection = conexionOracle;
           cmd.CommandText = "PK_COTIZADOR_WEB.EnviaCorreo_ActualizaCliente";
           cmd.CommandType = CommandType.StoredProcedure;           

           OracleParameter p1 = new OracleParameter("pCodCli", OracleDbType.Varchar2);
           OracleParameter p2 = new OracleParameter("pNombre", OracleDbType.Varchar2);
           OracleParameter p3 = new OracleParameter("pFechaNac", OracleDbType.Varchar2);
           OracleParameter p4 = new OracleParameter("pTipoId", OracleDbType.Varchar2);
           OracleParameter p5 = new OracleParameter("pIdentificacion", OracleDbType.Varchar2);
           OracleParameter p6 = new OracleParameter("pNacionalidad", OracleDbType.Varchar2);           
           OracleParameter p7 = new OracleParameter("pGenereo", OracleDbType.Varchar2);
           OracleParameter p8 = new OracleParameter("pNIT", OracleDbType.Varchar2);
           OracleParameter p9 = new OracleParameter("pEstadoCivil", OracleDbType.Varchar2);
           OracleParameter p10 = new OracleParameter("pTipoCliente", OracleDbType.Varchar2);
           OracleParameter p11 = new OracleParameter("pCorreoE", OracleDbType.Varchar2);
           OracleParameter p12 = new OracleParameter("pTelefono", OracleDbType.Varchar2);
           OracleParameter p13 = new OracleParameter("pDireccion", OracleDbType.Varchar2);
           OracleParameter p14 = new OracleParameter("pNombreRepresentate", OracleDbType.Varchar2);
           OracleParameter p15 = new OracleParameter("pActEco", OracleDbType.Varchar2);

           if (pTipoCliente == "P")
           {
               p1.Value = pCodCli;
               p2.Value = nuevoCliente.primer_nombre_individual + " " + nuevoCliente.segundo_nombre_individual + " " + nuevoCliente.primer_apellido_individual + " " + nuevoCliente.segundo_apellido_individual;
               p3.Value = nuevoCliente.fecha_nacimiento_individual;
               p4.Value = nuevoCliente.tipo_identificacion_individual;
               p5.Value = nuevoCliente.numero_identificacion_individual;
               p6.Value = nuevoCliente.nacionalidad_individual;
               p7.Value = nuevoCliente.genero_individual;
               p8.Value = nuevoCliente.nit_individual;
               p9.Value = nuevoCliente.estado_civil_individual;
               p10.Value = "P";
               p11.Value = nuevoCliente.correo_electronico_individual;
               p12.Value = Telefono;
               p13.Value = nuevoCliente.direccion_individual;
               p14.Value = "";
               p15.Value = "";
           }
           else 
           {
               p1.Value = pCodCli;
               p2.Value = nuevoJuridico.nombre_persona_juridica;
               p3.Value = nuevoJuridico.fecha_constitucion_empresa;
               p4.Value = nuevoJuridico.tipo_identificacion_representante;
               p5.Value = string.IsNullOrEmpty(nuevoJuridico.numero_identificacion_representante) ? "" : nuevoJuridico.numero_identificacion_representante;
               p6.Value = string.IsNullOrEmpty(nuevoJuridico.pais_origen_empresa) ? "" : nuevoJuridico.pais_origen_empresa;
               p7.Value = "N";
               p8.Value = nuevoJuridico.nit_persona_juridica;
               p9.Value = "N";
               p10.Value = "E";
               p11.Value = nuevoJuridico.correo_electronico_juridico;
               p12.Value = Telefono;
               p13.Value = nuevoJuridico.direccion_empresa;
               p14.Value = nuevoJuridico.primer_nombre_representante + " " + nuevoJuridico.segundo_nombre_representante + " " + nuevoJuridico.primer_apellido_representante + " " + nuevoJuridico.segundo_apellido_representante;
               p15.Value = nuevoJuridico.actividad_economica_empresa;
           }
           cmd.Parameters.Add(p1);
           cmd.Parameters.Add(p2);
           cmd.Parameters.Add(p3);
           cmd.Parameters.Add(p4);
           cmd.Parameters.Add(p5);
           cmd.Parameters.Add(p6);
           cmd.Parameters.Add(p7);
           cmd.Parameters.Add(p8);
           cmd.Parameters.Add(p9);
           cmd.Parameters.Add(p10);
           cmd.Parameters.Add(p11);
           cmd.Parameters.Add(p12);
           cmd.Parameters.Add(p13);
           cmd.Parameters.Add(p14);
           cmd.Parameters.Add(p15);

           cmd.ExecuteNonQuery();
           cmd.Connection.Close();

          
       }
        #region ConsultasVarias
       public DataTable obtenerPaises()
       {
           DataTable datosPais = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM PAIS", conexionOracle);

           try
           {
               adapter.Fill(datosPais);
               conexionOracle.Close();
           }
           catch (OracleException ex)
           {
               conexionOracle.Close();
               throw ex;
           }

           return datosPais;
       }
       public DataTable obtenerDepartamentos()
       {
           DataTable datosPais = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM ESTADO", conexionOracle);

           try
           {
               adapter.Fill(datosPais);
               conexionOracle.Close();
           }
           catch (OracleException ex)
           {
               conexionOracle.Close();
               throw ex;
           }

           return datosPais;
       }
       public DataTable obtenerMunicipios()
       {
           DataTable datosPais = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM CIUDAD", conexionOracle);

           try
           {
               adapter.Fill(datosPais);
               conexionOracle.Close();
           }
           catch (OracleException ex)
           {
               conexionOracle.Close();
               throw ex;
           }

           return datosPais;
       }
       //VICTORIA GUTIERREZ OBTENER ZONA
       public DataTable obtenerZonas()
       {
           DataTable datosPais = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM MUNICIPIO", conexionOracle);

           try
           {
               adapter.Fill(datosPais);
               conexionOracle.Close();
           }
           catch (OracleException ex)
           {
               conexionOracle.Close();
               throw ex;
           }

           return datosPais;
       }
       public DataTable obtenerNacionalidades()
       {
           DataTable datosCondiciones = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           OracleDataAdapter adapter = new OracleDataAdapter("SELECT CODLVAL, DESCRIP FROM LVAL WHERE TIPOLVAL = 'NACIONAL'", conexionOracle);

           try
           {
               adapter.Fill(datosCondiciones);
               conexionOracle.Close();
           }
           catch (OracleException ex)
           {
               conexionOracle.Close();
               throw ex;
           }

           return datosCondiciones;
       }
       public DataTable obtenerProfesiones()
       {
           DataTable datosCondiciones = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           OracleDataAdapter adapter = new OracleDataAdapter("SELECT CODLVAL, DESCRIP FROM LVAL WHERE TIPOLVAL = 'PROFES'", conexionOracle);

           try
           {
               adapter.Fill(datosCondiciones);
               conexionOracle.Close();
           }
           catch (OracleException ex)
           {
               conexionOracle.Close();
               throw ex;
           }

           return datosCondiciones;
       }
       public DataTable obtenerGeneros()
       {
           DataTable datosCondiciones = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           OracleDataAdapter adapter = new OracleDataAdapter("SELECT CODLVAL, DESCRIP FROM LVAL WHERE TIPOLVAL = 'SEXO'", conexionOracle);

           try
           {
               adapter.Fill(datosCondiciones);
               conexionOracle.Close();
           }
           catch (OracleException ex)
           {
               conexionOracle.Close();
               throw ex;
           }

           return datosCondiciones;
       }
       public DataTable obtenerEstadosCiviles()
       {
           DataTable datosCondiciones = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           OracleDataAdapter adapter = new OracleDataAdapter("SELECT CODLVAL, DESCRIP FROM LVAL WHERE TIPOLVAL = 'EDOCIVIL'", conexionOracle);

           try
           {
               adapter.Fill(datosCondiciones);
               conexionOracle.Close();
           }
           catch (OracleException ex)
           {
               conexionOracle.Close();
               throw ex;
           }

           return datosCondiciones;
       }
       public DataTable obtenerTiposDireccion()
       {
           DataTable datosCondiciones = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           OracleDataAdapter adapter = new OracleDataAdapter("SELECT CODLVAL, DESCRIP FROM LVAL WHERE TIPOLVAL = 'TIPODIR'", conexionOracle);

           try
           {
               adapter.Fill(datosCondiciones);
               conexionOracle.Close();
           }
           catch (OracleException ex)
           {
               conexionOracle.Close();
               throw ex;
           }

           return datosCondiciones;
       }
       public DataTable obtenerTiposTelefono()
       {
           DataTable datosCondiciones = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           OracleDataAdapter adapter = new OracleDataAdapter("SELECT CODLVAL, DESCRIP FROM LVAL WHERE TIPOLVAL = 'TIPOTEL'", conexionOracle);

           try
           {
               adapter.Fill(datosCondiciones);
               conexionOracle.Close();
           }
           catch (OracleException ex)
           {
               conexionOracle.Close();
               throw ex;
           }

           return datosCondiciones;       
       }
        #endregion

       public string DatosCorreos(int pIdProceso)
       {
           string Correos = "";
           Conexiones clConexiones = new Conexiones();
           MySqlConnection conexion = new MySqlConnection();

           conexion = clConexiones.abrirConexionMysql();
           MySqlCommand cmd = new MySqlCommand("  SELECT correos from correos_x_proceso where id_proceso = " + pIdProceso.ToString(), conexion);

           try
           {
               MySqlDataReader Reader;
               Reader = cmd.ExecuteReader();
               if (Reader.Read())
                   Correos = Reader.GetString(0);
               Reader.Close();
               conexion.Close();
           }

           catch (MySqlException ex)
           {
               conexion.Close();
               throw ex;
           }

           return Correos;
       }
    }
}
