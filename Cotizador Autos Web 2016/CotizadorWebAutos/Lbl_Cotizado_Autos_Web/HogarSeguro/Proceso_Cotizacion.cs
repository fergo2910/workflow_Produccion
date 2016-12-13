using Lbl_Cotizado_Autos_Web.Comunes;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using Lbl_Cotizado_Autos_Web.Estructuras;
using Lbl_Cotizador_Autos_Web.ConexionesBD;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.HogarSeguro
{
    public class Proceso_Cotizacion
    {
        #region PROCEDIMIENTOS_MYSQL

        /// <summary>
        /// Funcion que sirve para guardar la cotizacion para los planes de hogar seguro
        /// Desarrollado por: Victoria Gutierrez
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="Correo"></param>
        /// <param name="Telefono"></param>
        /// <param name="Fecha"></param>
        /// <param name="PlanSeleccionado"></param>
        /// <param name="CodAgente"></param>
        /// <returns></returns>
        public int guardarCotizacion(string Nombre, string Correo, int Telefono, DateTime Fecha, int PlanSeleccionado, string idePlanPol, string planPol, int CodAgente = 123, string MontoaAsegurar = "500000", float MontoaCancelar = 5000)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            int idCotizacion = 0;

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();
                command.CommandText = "Insert into cotizacion (nombre_apellido_cliente, correo_electronico_cliente, telefono_cliente, fecha_cotizacion, plan_cotizado, ideplanpol, planpol, codigo_agente_cotizando, monto_a_asegurar, monto_cancelar_poliza)" +
                                        " VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10) ";

                command.Parameters.AddWithValue("@1", Nombre);
                command.Parameters.AddWithValue("@2", Correo);
                command.Parameters.AddWithValue("@3", Telefono);
                command.Parameters.AddWithValue("@4", Fecha);
                command.Parameters.AddWithValue("@5", PlanSeleccionado);
                command.Parameters.AddWithValue("@6", idePlanPol);
                command.Parameters.AddWithValue("@7", planPol);
                command.Parameters.AddWithValue("@8", CodAgente);
                command.Parameters.AddWithValue("@9", MontoaAsegurar);
                command.Parameters.AddWithValue("@10", MontoaCancelar);

                command.ExecuteNonQuery();
                idCotizacion = Convert.ToInt32(command.LastInsertedId);

                cambiarEstadoCotizacion(idCotizacion);

                conexion.Close();

            }
            catch (MySqlException ex)
            {
                throw ex;
            }

            return idCotizacion;
        }
        public DataTable obtenerCotizacionesHogarSeguroXUsuario(int idUsuario)
        {
            DataTable datos = new DataTable();
            Varias clVarias = new Varias();
            int idEstadoCotizacion = 0;

            idEstadoCotizacion = clVarias.obtenerIdEstadoCotizacion("ELI");
           
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            string query = "SELECT  *, pl.nombre nombre_plan_web, es.nombre nombre_estado_cotizacion"
            + " FROM planes_web pl, cotizacion cot, estado_cotizacion es"
            + " where pl.id_plan_web = cot.id_plan_cotizado"
            + " and cot.estado_cotizacion = es.id_estado_cotizacion"
            + " and pl.codprod in ('MRIN')"
            + " and cot.id_usuario_cotizo = " + idUsuario
            + " and cot.estado_cotizacion != " + idEstadoCotizacion
            + " order by cot.id_cotizacion desc";

            try
            {
                adapter = new MySqlDataAdapter(query, conexion);
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                throw ex;
            }

            return datos;
        }
        public DataTable obtenerTodasCotizacionesHogarSeguro()
        {
            DataTable datos = new DataTable();
            Varias clVarias = new Varias();
            int idEstadoCotizacion = 0;

            idEstadoCotizacion = clVarias.obtenerIdEstadoCotizacion("ELI");

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            string query = "SELECT  *, pl.nombre nombre_plan_web, es.nombre nombre_estado_cotizacion"
            + " FROM planes_web pl, cotizacion cot, estado_cotizacion es"
            + " where pl.id_plan_web = cot.id_plan_cotizado"
            + " and cot.estado_cotizacion = es.id_estado_cotizacion"
            + " and pl.codprod in ('MRIN')"           
            + " and cot.estado_cotizacion != " + idEstadoCotizacion
            + " order by cot.id_cotizacion desc";

            try
            {
                adapter = new MySqlDataAdapter(query, conexion);
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                throw ex;
            }

            return datos;
        }        
        public DataTable ObtenerRolUsuario(string pCodigoUsuarioRemoto)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select rol_usuario from usuario" +
                "  where codigo_usuario_remoto  = " + pCodigoUsuarioRemoto, conexion);

            try
            {
                adapter.Fill(datos);
            }

            catch (MySqlException ex)
            {
                throw ex;
            }

            return datos;
        }
        public int obtenerIdPlanWeb(string ide_plan_pol, string plan_pol)
        {
            int resultado = 0;
            DataTable datos = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            try
            {
                adapter = new MySqlDataAdapter("SELECT id_plan_web FROM planes_web where ide_plan_pol = '" + ide_plan_pol + "' and plan_pol = '" + plan_pol + "'", conexion);

                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    resultado = int.Parse(datos.Rows[0]["id_plan_web"].ToString());
                }
            }

            catch (MySqlException ex)
            {
                throw ex;
            }

            return resultado;
        }
        private void cambiarEstadoCotizacion(int idCotizacion)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            DataTable resultadoIdepol = new DataTable();

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();
                command.CommandText = " update cotizacion set estado_cotizacion = 'HGVAL' where id_cotizacion = '" + idCotizacion + "'";

                command.ExecuteNonQuery();

                conexion.Close();


            }
            catch (MySqlException ex)
            {

                throw ex;
            }
        }
        public int obtenerIdPlan(string nombrePlan)
        {
            int idPlan = 0;
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter("select id_plan_web where nombre = '" + nombrePlan, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    idPlan = int.Parse(datos.Rows[0]["id_plan_web"].ToString());
                }
            }

            catch (MySqlException ex)
            {
                throw ex;
            }

            return idPlan;
        }

        public int guardarCotizacionAutos(string pNombreCompletoCliente, string pCorreoCliente, string pTelefonoCliente, double pMontoAsegurar,
                                          string pTipoVeh, string pDescTipoVeh, string pCodMarcaVeh, string pDescMarcaVeh, string pCodModelo,
                                          string pDescCodModelo, string pCodVersionVeh, string pDescVersionVeh, string pAnioVeh,
                                          int pNumeroAsientos, int pEstadoCotizacion, string pFechaCotizacion, string pFechaVigenciaInicio,
                                          string pFechaVigenciaFin, string pFechaInicioCobro, string pIdUsuarioCotizo, string pIdPlan,
                                          string pIDEPROCESO, string pIDEPOL, int pCantidadPagos, double pMontoTotalCancelar,
                                          double pMontoPagoFracDos, double pMontoPagoFracCuatro, double pMontoPagoFracSeis,
                                          double pMontoPagoFracOcho, double pMontoPagoFracDiez, double pMontoPagoFracDoce,
                                          double pMontoPagoTresCuotas, double pMontoPagoSeisCuotas, double pMontoPagoDiezCuotas,
                                          double pMontoPagoDoceCuotas, double pValorDeducible, double pSumaAseguradaCobrada, double pMontoPasajeros,
                                          double pValorGarantia, double pValorRC, double pLesionesOcupantes, double pValorCristales,
                                          double pMuerteAccidental, double pEquipoEspecial, string pCodigoAlarma, string pDescripcionAlarma, string pCODPLNAFRACC, string pMODPLANFRACC, double pPagoContado)
        {

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            Varias clVarias = new Varias();           
            DataTable resultadoPrimerPago = new DataTable();
            DataTable resultadoInfoCotizacionOracle = new DataTable();
            ConsultasBD objetoConsultas = new ConsultasBD();
            int idCotizacion = 0;
            int idEstadoCotizacion = 0;            

            try
            {
                idEstadoCotizacion = clVarias.obtenerIdEstadoCotizacion("VAL");

                conexion = clConexiones.abrirConexionMysql();
                MySqlCommand command = conexion.CreateCommand();
                
                command.CommandText = " INSERT INTO cotizacion (nombre_solicitante, correo_electronico_solicitante, telefono_solicitante, " +
                                        " monto_asegurado, tipoveh_vehiculo, desc_tipo_vehiculo, codmarca_vehiculo, " +
                                        " desc_marca_vehiculo, codmodelo_vehiculo, desc_modelo_vehiculo, " +
                                        " codversion_vehiculo, desc_version_vehiculo, anio_vehiculo, " +
                                        " num_asientos_vehiculo, estado_cotizacion, fecha_cotizacion, " +
                                        " fecha_vigencia_inicio, fecha_vigencia_fin, fecha_inicio_cobro, " +
                                        " id_usuario_cotizo, id_plan_cotizado, ideproceso_cot, idepol_cot, " +
                                        " numero_pagos_cot, monto_total_cancelar_poliza, frac_dos_pagos, " +
                                        " frac_cuatro_pagos, frac_seis_pagos, frac_ocho_pagos, frac_diez_pagos, " +
                                        " frac_doce_pagos, cuotas_tres_pagos, cuotas_seis_pagos, cuotas_diez_pagos, " +
                                        " cuotas_doce_pagos, valor_deducible, suma_asegurada_cobrada, monto_pasajero, " +
                                        " valor_garantia, valor_resp_civil, valor_lesion_a_ocupante_vehiculo, " +
                                        " valor_maximo_cristales_vehiculo, valor_maximo_muerte_accidental_vehiculo, valor_equipo_especial_vehiculo, " +
                                        " codigo_alarma, descripcion_alarma,codplanfracc,modplanfracc,pago_contado) " +
                                        " VALUES ('" + pNombreCompletoCliente + "', '" + pCorreoCliente + "', '" + pTelefonoCliente + "', " +
                                        " '" + pMontoAsegurar + "', '" + pTipoVeh + "','" + pDescTipoVeh + "', '" + pCodMarcaVeh + "', " +
                                        " '" + pDescMarcaVeh + "','" + pCodModelo + "', '" + pDescCodModelo + "', " +
                                        " '" + pCodVersionVeh + "','" + pDescVersionVeh + "', '" + pAnioVeh + "', " +
                                        " '" + pNumeroAsientos + "', '" + idEstadoCotizacion + "', NOW(), " +
                                        " '" + pFechaVigenciaInicio + "', '" + pFechaVigenciaFin + "', '" + pFechaInicioCobro + "', " +
                                        " '" + pIdUsuarioCotizo + "', '" + pIdPlan + "', '" + pIDEPROCESO + "', '" + pIDEPOL + "', " +
                                        " '" + pCantidadPagos + "', '" + pMontoTotalCancelar + "' ,'" + pMontoPagoFracDos + "' , " +
                                        " '" + pMontoPagoFracCuatro + "' ,'" + pMontoPagoFracSeis + "' ,'" + pMontoPagoFracOcho + "' ,'" + pMontoPagoFracDiez + "' , " +
                                        " '" + pMontoPagoFracDoce + "' ,'" + pMontoPagoTresCuotas + "' ,'" + pMontoPagoSeisCuotas + "' ,'" + pMontoPagoDiezCuotas + "', " +
                                        " '" + pMontoPagoDoceCuotas + "', '" + pValorDeducible + "' , '" + pSumaAseguradaCobrada + "', '" + pMontoPasajeros + "', " +
                                        " '" + pValorGarantia + "', '" + pValorRC + "', '" + pLesionesOcupantes + "', " +
                                        " '" + pValorCristales + "', '" + pMuerteAccidental + "', '" + pEquipoEspecial + "', " +
                                        " '" + pCodigoAlarma + "', '" + pDescripcionAlarma + "', '" + pCODPLNAFRACC + "', '" + pMODPLANFRACC + "', '" + pPagoContado + "') ";                    

                command.ExecuteNonQuery();

                idCotizacion = Convert.ToInt32(command.LastInsertedId);
                conexion.Close();

                conexion = clConexiones.abrirConexionMysql();
                command = conexion.CreateCommand();

                resultadoInfoCotizacionOracle = objetoConsultas.obtenerInformacionDatosCotizacionOracle(Convert.ToInt32(pIDEPROCESO));                

                conexion = clConexiones.abrirConexionMysql();
                command = conexion.CreateCommand();

                for (int i = 0; i < resultadoInfoCotizacionOracle.Rows.Count; i++)
                {
                    command.CommandText = " INSERT INTO reporte_cotizacion_autos (id_cotizacion, seccion_cobertura, " +
                                          " descripcion_seccion_cobertura, deducible_minimo, deducible_maximo, base, " +
                                          " suma_asegurada, cod_ramo_cert, cod_cobert, cub_ex, codigo_moneda) " +
                                          " VALUES ('" + idCotizacion + "', '" + resultadoInfoCotizacionOracle.Rows[i]["SECCION"].ToString() + "', " +
                                          " '" + resultadoInfoCotizacionOracle.Rows[i]["DESCRIPCION"].ToString() + "', " +
                                          " '" + resultadoInfoCotizacionOracle.Rows[i]["DEDMINIMO"].ToString() + "', " +
                                          " '" + resultadoInfoCotizacionOracle.Rows[i]["DEDUCIBLE"].ToString() + "', " +
                                          " '" + resultadoInfoCotizacionOracle.Rows[i]["BASE"].ToString() + "', " +
                                          " '" + resultadoInfoCotizacionOracle.Rows[i]["SUMAASEGURADA"].ToString() + "', " +
                                          " '" + resultadoInfoCotizacionOracle.Rows[i]["CODRAMOCERT"].ToString() + "', " +
                                          " '" + resultadoInfoCotizacionOracle.Rows[i]["CODCOBERT"].ToString() + "', " +
                                          " '" + resultadoInfoCotizacionOracle.Rows[i]["CUB_EX"].ToString() + "', " +
                                          " '" + resultadoInfoCotizacionOracle.Rows[i]["CODMONEDA"].ToString() + "') ";
                    command.ExecuteNonQuery();
                }

                conexion.Close();         
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return idCotizacion;
        }

        #endregion

        #region PROCEDIMIENTOS_ORACLE
        public bool ObtenerAgente(string pCodigoAgente)
        {
            DataTable datos = new DataTable();
            bool resultado = false;
            string contenidoConsulta = string.Empty;

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT CODUSUARIOREMO FROM USUARIO_REMOTO WHERE CODUSUARIOREMO = '" + pCodigoAgente + "'", conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    contenidoConsulta = datos.Rows[0]["CODUSUARIOREMO"].ToString();

                    if (contenidoConsulta == pCodigoAgente)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
            }

            return resultado;
        }

        #endregion
    }
}
