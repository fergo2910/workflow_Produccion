using Lbl_Cotizador_Autos_Web.ConexionesBD;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using System.Net.Mail;
using System.Configuration;
using System.Net;

namespace Lbl_Cotizado_Autos_Web.Clientes
{
    public class Operaciones
    {
        public long guardarDatosClienteIndividual(int numeroCotizacion, string rolCliente, string tipoCliente, EstructuraClienteIndividual clienteIndividual, DataTable telefonos)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();
                command.CommandText = " INSERT INTO cliente (numero_cotizacion, rol_cliente, tipo_cliente, primer_nombre_individual, segundo_nombre_individual, "
                + " primer_apellido_individual, segundo_apellido_individual, fecha_nacimiento_individual, genero_individual, estado_civil_individual, "
                + " profesion_individual, nacionalidad_individual, tipo_identificacion_individual, numero_identificacion_individual, "
                + " pais_emision_individual, depto_emision_individual, muni_emision_individual, nombre_negocio_individual, nit_individual, "
                + " actua_nombre_propio_individual, responsable_pago_individual, direccion_individual, pais_direccion_individual, "
                + " depto_direccion_individual, muni_direccion_individual, esPep_individual, relacionPep_individual, asociadoPep_individual, "
                + " correo_electronico_individual, zona_direccion_individual, calle_direccion_individual, avenida_direccion_individual, numero_casa_direccion_individual, "
                + " colonia_direccion_individual, edificio_direccion_individual, lote_direccion_individual, sector_direccion_individual, manzana_direccion_individual) "
                + " VALUES (@1,  @2,  @3,  @4,  @5,  @6,  @7,  @8,  @9,  @10,  @11,  @12,  @13,  @14,  @15, @16,  @17,  @18,  @19,  @20,  @21,  @22,  @23,  @24,  @25,  "
                + " @26,  @27,  @28,  @29, @30, @31, @32, @33, @34, @35, @36, @37, @38) ";

                command.Parameters.AddWithValue("@1", numeroCotizacion);
                command.Parameters.AddWithValue("@2", rolCliente);
                command.Parameters.AddWithValue("@3", tipoCliente);
                command.Parameters.AddWithValue("@4", clienteIndividual.primer_nombre_individual);
                command.Parameters.AddWithValue("@5", clienteIndividual.segundo_nombre_individual);
                command.Parameters.AddWithValue("@6", clienteIndividual.primer_apellido_individual);
                command.Parameters.AddWithValue("@7", clienteIndividual.segundo_apellido_individual);
                command.Parameters.AddWithValue("@8", clienteIndividual.fecha_nacimiento_individual);
                command.Parameters.AddWithValue("@9", clienteIndividual.genero_individual);
                command.Parameters.AddWithValue("@10", clienteIndividual.estado_civil_individual);
                command.Parameters.AddWithValue("@11", clienteIndividual.profesion_individual);
                command.Parameters.AddWithValue("@12", clienteIndividual.nacionalidad_individual);
                command.Parameters.AddWithValue("@13", clienteIndividual.tipo_identificacion_individual);
                command.Parameters.AddWithValue("@14", clienteIndividual.numero_identificacion_individual);
                command.Parameters.AddWithValue("@15", clienteIndividual.pais_emision_individual);
                command.Parameters.AddWithValue("@16", clienteIndividual.depto_emision_individual);
                command.Parameters.AddWithValue("@17", clienteIndividual.muni_emision_individual);
                command.Parameters.AddWithValue("@18", clienteIndividual.nombre_negocio_individual);
                command.Parameters.AddWithValue("@19", clienteIndividual.nit_individual);
                command.Parameters.AddWithValue("@20", clienteIndividual.actua_nombre_propio_individual);
                command.Parameters.AddWithValue("@21", clienteIndividual.responsable_pago_individual);
                command.Parameters.AddWithValue("@22", clienteIndividual.direccion_individual);
                command.Parameters.AddWithValue("@23", clienteIndividual.pais_direccion_individual);
                command.Parameters.AddWithValue("@24", clienteIndividual.depto_direccion_individual);
                command.Parameters.AddWithValue("@25", clienteIndividual.muni_direccion_individual);
                command.Parameters.AddWithValue("@26", clienteIndividual.esPep_individual);
                command.Parameters.AddWithValue("@27", clienteIndividual.relacionPep_individual);
                command.Parameters.AddWithValue("@28", clienteIndividual.asociadoPep_individual);
                command.Parameters.AddWithValue("@29", clienteIndividual.correo_electronico_individual);
                command.Parameters.AddWithValue("@30", clienteIndividual.zona_direccion_individual);
                command.Parameters.AddWithValue("@31", clienteIndividual.calle_direccion_individual);
                command.Parameters.AddWithValue("@32", clienteIndividual.avenida_direccion_individual);
                command.Parameters.AddWithValue("@33", clienteIndividual.numero_casa_direccion_individual);
                command.Parameters.AddWithValue("@34", clienteIndividual.colonia_direccion_individual);
                command.Parameters.AddWithValue("@35", clienteIndividual.edificio_direccion_individual);
                command.Parameters.AddWithValue("@36", clienteIndividual.lote_direccion_individual);
                command.Parameters.AddWithValue("@37", clienteIndividual.sector_direccion_individual);
                command.Parameters.AddWithValue("@38", clienteIndividual.manzana_direccion_individual);

                command.ExecuteNonQuery();
                conexion.Close();

                guardarTelefonosCliente(telefonos, clienteIndividual.nit_individual, numeroCotizacion);
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;

            }

            return numeroCotizacion;
        }
        public long guardarDatosClienteJuridico(int numeroCotizacion, string rolCliente, string tipoCliente, EstructuraClienteJuridico clienteJuridico, DataTable telefonos)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();
                command.CommandText = "INSERT INTO cliente (numero_cotizacion, rol_cliente, tipo_cliente, nombre_persona_juridica, nit_persona_juridica, primer_nombre_representante, segundo_nombre_representante,"
                + " primer_apellido_representante, segundo_apellido_representante, tipo_identificacion_representante,"
                + " numero_identificacion_representante, pais_emision_representante, depto_emision_representante,"
                + " muni_emision_representante, fecha_constitucion_empresa, pais_origen_empresa, actividad_economica_empresa,"
                + " direccion_empresa, pais_direccion_empresa, depto_direccion_empresa, muni_direccion_empresa, esPep_juridico,"
                + " relacionPep_juridico, asociadoPep_juridico, correo_electronico_juridico, zona_direccion_empresa, calle_direccion_empresa, avenida_direccion_empresa, "
                + " numero_casa_direccion_empresa, colonia_direccion_empresa, edificio_direccion_empresa, lote_direccion_empresa, sector_direccion_empresa, manzana_direccion_empresa) "
                + " VALUES (@1,  @2,  @3,  @4,  @5,  @6,  @7,  @8,  @9,  @10,  @11,  @12,  @13,  @14,  @15, @16,  @17,  @18,  @19,  @20,  @21,  @22,  @23,  @24,  @25, @26, @27, @28, @29, @30, @31, @32, @33, @34)";

                command.Parameters.AddWithValue("@1", numeroCotizacion);
                command.Parameters.AddWithValue("@2", rolCliente);
                command.Parameters.AddWithValue("@3", tipoCliente);
                command.Parameters.AddWithValue("@4", clienteJuridico.nombre_persona_juridica);
                command.Parameters.AddWithValue("@5", clienteJuridico.nit_persona_juridica);
                command.Parameters.AddWithValue("@6", clienteJuridico.primer_nombre_representante);
                command.Parameters.AddWithValue("@7", clienteJuridico.segundo_nombre_representante);
                command.Parameters.AddWithValue("@8", clienteJuridico.primer_apellido_representante);
                command.Parameters.AddWithValue("@9", clienteJuridico.segundo_apellido_representante);
                command.Parameters.AddWithValue("@10", clienteJuridico.tipo_identificacion_representante);
                command.Parameters.AddWithValue("@11", clienteJuridico.numero_identificacion_representante);
                command.Parameters.AddWithValue("@12", clienteJuridico.pais_emision_representante);
                command.Parameters.AddWithValue("@13", clienteJuridico.depto_emision_representante);
                command.Parameters.AddWithValue("@14", clienteJuridico.muni_emision_representante);
                command.Parameters.AddWithValue("@15", clienteJuridico.fecha_constitucion_empresa);
                command.Parameters.AddWithValue("@16", clienteJuridico.pais_origen_empresa);
                command.Parameters.AddWithValue("@17", clienteJuridico.actividad_economica_empresa);
                command.Parameters.AddWithValue("@18", clienteJuridico.direccion_empresa);
                command.Parameters.AddWithValue("@19", clienteJuridico.pais_direccion_empresa);
                command.Parameters.AddWithValue("@20", clienteJuridico.depto_direccion_empresa);
                command.Parameters.AddWithValue("@21", clienteJuridico.muni_direccion_empresa);
                command.Parameters.AddWithValue("@22", clienteJuridico.esPep_juridico);
                command.Parameters.AddWithValue("@23", clienteJuridico.relacionPep_juridico);
                command.Parameters.AddWithValue("@24", clienteJuridico.asociadoPep_juridico);
                command.Parameters.AddWithValue("@25", clienteJuridico.correo_electronico_juridico);
                command.Parameters.AddWithValue("@26", clienteJuridico.zona_direccion_empresa);
                command.Parameters.AddWithValue("@27", clienteJuridico.calle_direccion_empresa);
                command.Parameters.AddWithValue("@28", clienteJuridico.avenida_direccion_empresa);
                command.Parameters.AddWithValue("@29", clienteJuridico.numero_casa_direccion_empresa);
                command.Parameters.AddWithValue("@30", clienteJuridico.colonia_direccion_empresa);
                command.Parameters.AddWithValue("@31", clienteJuridico.edificio_direccion_empresa);
                command.Parameters.AddWithValue("@32", clienteJuridico.lote_direccion_empresa);
                command.Parameters.AddWithValue("@33", clienteJuridico.sector_direccion_empresa);
                command.Parameters.AddWithValue("@34", clienteJuridico.manzana_direccion_empresa);

                command.ExecuteNonQuery();

                conexion.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;

            }

            return numeroCotizacion;
        }
        public void actualizarEstadoCotizacion(int idCotizacion)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            DataTable resultadoIdepol = new DataTable();
            int idEstadoCotizacion = 0;

            idEstadoCotizacion = obtenerIdEstadoCotizacion("INC");

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();
                string query = string.Empty;

                query = "update cotizacion set estado_cotizacion = " + idEstadoCotizacion + " where id_cotizacion = " + idCotizacion;
                command.CommandText = query;
                
                command.ExecuteNonQuery();
                conexion.Close();
                //Se quito el envio de correo ya que no es necesario enviarlo cada vez que se guarda el cliente.
                //envioCorreoAMapfre(idCotizacion);
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
        }
        private void envioCorreoAMapfre(int numeroCotizacion)
        {
            DataTable correos = new DataTable();
            DataTable correoUsuarioLogeado = new DataTable();
            ConsultasBD objetoConsultas = new ConsultasBD();

            correos = objetoConsultas.obtenerCorreosPorRol("INFORMACION_VEHICULO");
            correoUsuarioLogeado = objetoConsultas.obtenerCorreoUsuario(numeroCotizacion);

            if (correos.Rows.Count > 0)
            {
                for (int i = 0; i < correos.Rows.Count; i++)
                {
                    string correo = string.Empty;

                    correo = correos.Rows[i]["correo_electronico"].ToString();

                    MailMessage msg = new MailMessage();
                    try
                    {
                        msg.From = new MailAddress(correoUsuarioLogeado.Rows[0]["correo_electronico"].ToString());
                        msg.To.Add(correo);
                        msg.Body = "Estimado(a) Usuario, " + "\n" + "\n"
                        + "Se ha creado una cotización en el sistema, proceda a ingresar la información vehiculo. " + "\n" + "\n"
                        + "Los datos son los siguientes: " + "\n"
                        + "Número de cotización: " + numeroCotizacion + "\n"
                        + "Nombre Cliente: " + correoUsuarioLogeado.Rows[0]["nombre_cliente"].ToString();

                        msg.Subject = "Ingreso información de vehículo";
                        SmtpClient smt = new SmtpClient(ConfigurationManager.AppSettings["servidorCorreo"]);
                        smt.Port = 25;
                        smt.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["direccionSalidaMensajes"], ConfigurationManager.AppSettings["passwordDireccionSalidaMensajes"]);

                        smt.Send(msg);
                    }
                    catch (Exception ex)
                    {
                        string script = "<script>alert('" + ex.Message + "')</script>";
                        //ClientScript.RegisterStartupScript(this.GetType(), "Error al enviar correo de autorización de inspección.", script);
                    }
                }
            }

        }
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
        private void guardarTelefonosCliente(DataTable datosTelefono, string nitCliente, int idCotizacion)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            try
            {
                conexion = clConexiones.abrirConexionMysql();
                MySqlCommand command = conexion.CreateCommand();

                for (int i = 0; i < datosTelefono.Rows.Count; i++)
                {
                    command.CommandText = " INSERT INTO telefonos_cliente (nit_cliente, id_cotizacion, numero_telefono) "
                    + " VALUES ('" + nitCliente + "' ," + idCotizacion + " ," + datosTelefono.Rows[i][0] + ")";

                    command.ExecuteNonQuery();
                }
                conexion.Close();
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
        }

        public bool removerClientesCotizacionCancelada(int idCotizacion)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            try
            {
                conexion = clConexiones.abrirConexionMysql();
                MySqlCommand command = conexion.CreateCommand();
                string query = "DELETE FROM cliente where numero_cotizacion = " + idCotizacion;
                command.CommandText = query;
                command.ExecuteNonQuery();
                conexion.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                return false;
                throw ex;
            }
        }
    }
}
