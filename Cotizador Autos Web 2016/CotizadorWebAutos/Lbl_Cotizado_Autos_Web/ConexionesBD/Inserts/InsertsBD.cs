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
using Lbl_Cotizado_Autos_Web.ConexionesBD.Updates;

namespace Lbl_Cotizado_Autos_Web.ConexionesBD.Inserts
{
    public class InsertsBD
    {
        #region INSERTS_MYSQL
        public int guardarCotizacionAutos(string pNombreCompletoCliente, string pCorreoCliente, string pTelefonoCliente,double pMontoAsegurar,
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
                                          double pMuerteAccidental, List<CA_COBERTURA> pCoberturasAdicionales, List<CA_REGARGO> pRecargos, 
                                          double pEquipoEspecial, string pCodigoAlarma, string pDescripcionAlarma, string pCODPLNAFRACC, 
                                          string pMODPLANFRACC, double pPagoContado, string pSeccionIII, string pCodInter, string pFORMAPAGOCOTIZADO, string pNUMEROPAGOSCOTIZADO)
        {

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            Consultas.ConsultasBD objetoConsultas = new Consultas.ConsultasBD();
            Operaciones objetoOperaciones = new Operaciones();

            DataTable resultadoIdepol = new DataTable();
            DataTable resultadoPrimerPago = new DataTable();
            DataTable resultadoInfoCotizacionOracle = new DataTable();
            int idCotizacion = 0;
            int idEstadoCotizacion = 0;
            int contadorInformacionReporte = 0;

            try
            {
                resultadoIdepol = objetoConsultas.ObtenerIdepol(pIDEPROCESO);
                idEstadoCotizacion = objetoOperaciones.obtenerIdEstadoCotizacion("VAL");

                conexion = clConexiones.abrirConexionMysql();
                MySqlCommand command = conexion.CreateCommand();
                if (resultadoIdepol.Rows[0]["IDEPOL"].ToString() != string.Empty)
                {
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
                                          " codigo_alarma, descripcion_alarma,codplanfracc,modplanfracc,pago_contado,seccion_iii, codigo_intermediario, " +
                                          " desc_forma_pago_cotizado, desc_numero_pagos_cotizado) " +
                                          " VALUES ('"+pNombreCompletoCliente+"', '"+pCorreoCliente+"', '"+pTelefonoCliente+"', " +
                                          " '"+pMontoAsegurar+"', '"+pTipoVeh+"','"+pDescTipoVeh+"', '"+pCodMarcaVeh+"', " +
                                          " '"+pDescMarcaVeh+"','"+pCodModelo+"', '"+pDescCodModelo+"', " + 
                                          " '"+pCodVersionVeh+"','"+pDescVersionVeh+"', '"+pAnioVeh+"', " +
                                          " '" + pNumeroAsientos + "', '" + idEstadoCotizacion + "', NOW(), " + 
                                          " '"+pFechaVigenciaInicio+"', '"+pFechaVigenciaFin+"', '"+pFechaInicioCobro+"', " + 
                                          " '"+pIdUsuarioCotizo+"', '"+pIdPlan+"', '"+pIDEPROCESO+"', '"+pIDEPOL+"', " +
                                          " '"+pCantidadPagos+"', '"+pMontoTotalCancelar+"' ,'"+pMontoPagoFracDos+"' , " + 
                                          " '"+pMontoPagoFracCuatro+"' ,'"+pMontoPagoFracSeis+"' ,'"+pMontoPagoFracOcho+"' ,'"+pMontoPagoFracDiez+"' , " + 
                                          " '"+pMontoPagoFracDoce+"' ,'"+pMontoPagoTresCuotas+"' ,'"+pMontoPagoSeisCuotas+"' ,'"+pMontoPagoDiezCuotas+"', " +
                                          " '"+pMontoPagoDoceCuotas+"', '"+pValorDeducible+"' , '"+pSumaAseguradaCobrada+"', '"+pMontoPasajeros+"', " + 
                                          " '"+pValorGarantia+"', '"+pValorRC+"', '"+pLesionesOcupantes+"', " +
                                          " '" + pValorCristales + "', '" + pMuerteAccidental + "', '" + pEquipoEspecial + "', " +
                                          " '" + pCodigoAlarma + "', '" + pDescripcionAlarma + "', '" + pCODPLNAFRACC + "', '" +
                                          pMODPLANFRACC + "', '" + pPagoContado + "', '" + pSeccionIII + "','" + pCodInter + "','" + pFORMAPAGOCOTIZADO + 
                                          "','" + pNUMEROPAGOSCOTIZADO + "') ";

                    #region CODIGO_COMENTADO_INSERT_POR_PARAMETROS
                    /* MySqlCommand command = conexion.CreateCommand();
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
                                          " valor_maximo_cristales_vehiculo, valor_maximo_muerte_accidental_vehiculo, " +
                                          " VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, " +
                                          " @13, @14, @15, @16, @17, @18, @19, @20, @21, @22, @23, " +
                                          " @24, @25 ,@26 ,@27 ,@28 ,@29 ,@30 ,@31 ,@32 ,@33 ,@34, " +
                                          " @35, @36, @37, @38, @39, @40, @41, @42, @43, @44) ";

                    command.Parameters.AddWithValue("@1", pNombreCompletoCliente);
                    command.Parameters.AddWithValue("@2", pCorreoCliente);
                    command.Parameters.AddWithValue("@3", pTelefonoCliente);

                    command.Parameters.AddWithValue("@4", pMontoAsegurar);
                    command.Parameters.AddWithValue("@5", pTipoVeh);
                    command.Parameters.AddWithValue("@6", pDescTipoVeh);
                    command.Parameters.AddWithValue("@7", pCodMarcaVeh);

                    command.Parameters.AddWithValue("@8", pDescMarcaVeh);
                    command.Parameters.AddWithValue("@9", pCodModelo);
                    command.Parameters.AddWithValue("@10", pDescCodModelo);

                    command.Parameters.AddWithValue("@11", pCodVersionVeh);
                    command.Parameters.AddWithValue("@12", pDescVersionVeh);
                    command.Parameters.AddWithValue("@13", pAnioVeh);

                    command.Parameters.AddWithValue("@14", pNumeroAsientos);
                    command.Parameters.AddWithValue("@15", pEstadoCotizacion);
                    command.Parameters.AddWithValue("@16", pFechaCotizacion);

                    command.Parameters.AddWithValue("@17", pFechaVigenciaInicio);
                    command.Parameters.AddWithValue("@18", pFechaVigenciaFin);
                    command.Parameters.AddWithValue("@19", pFechaInicioCobro);

                    command.Parameters.AddWithValue("@20", pIdUsuarioCotizo);
                    command.Parameters.AddWithValue("@21", pIdPlan);
                    command.Parameters.AddWithValue("@22", pIDEPROCESO);
                    command.Parameters.AddWithValue("@23", pIDEPOL);

                    command.Parameters.AddWithValue("@24", pCantidadPagos);
                    command.Parameters.AddWithValue("@25", pMontoTotalCancelar);
                    command.Parameters.AddWithValue("@26", pMontoPagoFracDos);

                    command.Parameters.AddWithValue("@27", pMontoPagoFracCuatro);
                    command.Parameters.AddWithValue("@28", pMontoPagoFracSeis);
                    command.Parameters.AddWithValue("@29", pMontoPagoFracOcho);
                    command.Parameters.AddWithValue("@30", pMontoPagoFracDiez);

                    command.Parameters.AddWithValue("@31", pMontoPagoFracDoce);
                    command.Parameters.AddWithValue("@32", pMontoPagoTresCuotas);
                    command.Parameters.AddWithValue("@33", pMontoPagoSeisCuotas);
                    command.Parameters.AddWithValue("@34", pMontoPagoDiezCuotas);

                    command.Parameters.AddWithValue("@35", pMontoPagoDoceCuotas);
                    command.Parameters.AddWithValue("@36", pValorDeducible);
                    command.Parameters.AddWithValue("@37", pSumaAseguradaCobrada);
                    command.Parameters.AddWithValue("@38", pMontoPasajeros);
               
                    command.Parameters.AddWithValue("@39", pValorGarantia);
                    command.Parameters.AddWithValue("@40", pValorRC);
                    command.Parameters.AddWithValue("@41", pLesionesOcupantes);

                    command.Parameters.AddWithValue("@42", pValorCristales);
                    command.Parameters.AddWithValue("@43", pMuerteAccidental);*/
                    /*conexion = clConexiones.abrirConexionMysql();

                    MySqlCommand command = conexion.CreateCommand();
                    command.CommandText = " INSERT INTO cotizacion (nombre_apellido_cliente, cotizacion_emitida, " +
                                          " fecha_cotizacion, plan_cotizado, monto_a_asegurar, " +
                                          " codigo_agente_cotizando, estado, ideproceso, ideplanpol, planpol, " +
                                          " vigenciainicio, vigenciafinal, fechainiciocobro, tipoveh, codmarca, " +
                                          " codmodelo, codversion, anioveh, numpuesto,idepol,correo_electronico_cliente,telefono_cliente, telefono_cliente_otro,numero_pagos, estado_cotizacion,monto_cancelar_poliza,monto_cuatro_pagos,monto_seis_pagos,monto_doce_pagos,descripcion_tipo_vehiculo,descripcion_marca_vehiculo,descripcion_linea_vehiculo,valor_deducible,suma_aseg_cob,monto_pasajero) " +
                                          " VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17, @18, @19, @20, @21, @22, @23, @24,'VAL',@25,@26,@27,@28,@29,@30,@31,@32,@33,@34) ";

                    command.Parameters.AddWithValue("@1", pNombreCliente);
                    command.Parameters.AddWithValue("@2", pCotizacionEmitida);
                    command.Parameters.AddWithValue("@3", pFechaCotizacion);
                    command.Parameters.AddWithValue("@4", pPlanCotizado);
                    command.Parameters.AddWithValue("@5", pMontoAsegurar);
                    command.Parameters.AddWithValue("@6", pCodigoAgenteCotizando);
                    command.Parameters.AddWithValue("@7", pEstado);
                    command.Parameters.AddWithValue("@8", pIdeProceso);
                    command.Parameters.AddWithValue("@9", pIdeplan);
                    command.Parameters.AddWithValue("@10", pPlanPol);
                    command.Parameters.AddWithValue("@11", pVigenciaInicio);
                    command.Parameters.AddWithValue("@12", pVigenciaFinal);
                    command.Parameters.AddWithValue("@13", pFechaInicioCobro);
                    command.Parameters.AddWithValue("@14", pTipoVehiculo);
                    command.Parameters.AddWithValue("@15", pCodigoMarca);
                    command.Parameters.AddWithValue("@16", pCodigoModelo);
                    command.Parameters.AddWithValue("@17", pCodigoVersion);
                    command.Parameters.AddWithValue("@18", pAnioVehiculo);
                    command.Parameters.AddWithValue("@19", pNumeroAsientos);
                    command.Parameters.AddWithValue("@20", resultadoIdepol.Rows[0]["IDEPOL"].ToString());
                    command.Parameters.AddWithValue("@21", pCorreo);
                    command.Parameters.AddWithValue("@22", pTelefonoPrincipal);
                    command.Parameters.AddWithValue("@23", pTelefonoSecundario);
                    command.Parameters.AddWithValue("@24", pNumeroPagos);
                    command.Parameters.AddWithValue("@25", float.Parse(pMontoCancelar));
                    command.Parameters.AddWithValue("@26", float.Parse(pMontoCuatroPagos));
                    command.Parameters.AddWithValue("@27", float.Parse(pMontoSeisPagos));
                    command.Parameters.AddWithValue("@28", float.Parse(pMontoDocePagos));
                    command.Parameters.AddWithValue("@29", pTipoVehiculoDescripcion);
                    command.Parameters.AddWithValue("@30", pMarcaVehiculoDescripcion);
                    command.Parameters.AddWithValue("@31", pLineaVehiculoDescripcion);
                    command.Parameters.AddWithValue("@32", pMontoDeducible);

                    if (pPlanCotizado == "2")
                    {
                        montoPasajero = 2000;
                        sumaAsegurada = montoPasajero * Convert.ToInt32(pNumeroAsientos);

                    }
                    else
                    {
                        montoPasajero = 2000;
                        sumaAsegurada = montoPasajero * Convert.ToInt32(pNumeroAsientos);
                    }

                    //CAMPOS EN LOS CUALES VAN EL CALCULO POR PASAJEROS
                    //--------------------------------------------------------
                    command.Parameters.AddWithValue("@33", sumaAsegurada);
                    command.Parameters.AddWithValue("@34", montoPasajero);
                    //--------------------------------------------------------*/
#endregion

                    command.ExecuteNonQuery();
                    idCotizacion = Convert.ToInt32(command.LastInsertedId);
                    conexion.Close();

                    conexion = clConexiones.abrirConexionMysql();
                    command = conexion.CreateCommand();

                    for (int i = 0; i < pCoberturasAdicionales.Count; i++)
                    {
                        command.CommandText = " INSERT INTO coberturas_adicionales (id_cotizacion, codigo_cobertura, suma_asegurada, " +
                                              " prima_cobertura, tasa_cobertura, descripcion_cobertura, ramo_cobertura)  " +
                                              " VALUES ('" + idCotizacion + "', '" + pCoberturasAdicionales[i].CODIGO + "', '" + Convert.ToDouble(pCoberturasAdicionales[i].SUMAASEGURADA.ToString()) + "'," +
                                              " '" + pCoberturasAdicionales[i].PRIMA + "', '" + pCoberturasAdicionales[i].TASA + "', '" + pCoberturasAdicionales[i].DESCRIPCION_COBERTURA + "', '" + pCoberturasAdicionales[i].RAMO + "') ";
                        command.ExecuteNonQuery();
                    }                  
                    conexion.Close();

                    conexion = clConexiones.abrirConexionMysql();
                    command = conexion.CreateCommand();
                    string descrpRecargo = string.Empty;
                    string monto = string.Empty;
                    string porcentaje = string.Empty;
                    for (int i = 0; i < pRecargos.Count; i++)
                    {
                        if (pRecargos[i].DESCRIPCION_RECARGO == null)
                        {
                            descrpRecargo = "DESCUENTO";
                            monto = "0";
                            if (pRecargos[i].PORCENTAJE == string.Empty)
                            {
                                porcentaje = "0";
                            }
                            else
                            {
                                porcentaje = pRecargos[i].PORCENTAJE;
                            }
                            
                        }
                        else
                        {
                            descrpRecargo = pRecargos[i].DESCRIPCION_RECARGO;
                            monto = pRecargos[i].MONTO;
                            porcentaje = pRecargos[i].PORCENTAJE;
                        }
                        command.CommandText = " INSERT INTO recargos (id_cotizacion, codigo_recargo, tipo_recargo, monto_recargo, " +
                                              " descripcion_recargo,porcentaje_recargo)  " +
                                              " VALUES ('" + idCotizacion + "', '" + pRecargos[i].CODIGO + "', '" + pRecargos[i].TIPO + "'," +
                                              " '" + monto + "', '" + descrpRecargo + "' , '" + porcentaje + "') ";
                        command.ExecuteNonQuery();
                    }
                    conexion.Close();
                }

                resultadoInfoCotizacionOracle = objetoConsultas.obtenerInformacionDatosCotizacionOracle(Convert.ToInt32(pIDEPROCESO));
                contadorInformacionReporte = resultadoInfoCotizacionOracle.Rows.Count;

                conexion = clConexiones.abrirConexionMysql();
                command = conexion.CreateCommand();
                for (int i = 0; i < contadorInformacionReporte; i++)
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
        public bool InsertarVehiculoEInspeccion(string pIdCotiacion, string pTipoVehiculo, string pMarcaVehiculo, string pLineaVehiculo,
                                                int pAnioVehiculo, string pTarjetaCirculacion, string pNumeroChasis, string pTipoPlaca,
                                                string pNumeroPlaca, string pCorrelativoPlaca, string pCilindraje, int pTonelaje,
                                                string pColor, string pNumeroMotor, string pNumeroInspeccion, string pComentarios)
        {
            bool resultado = false;
            int procesoFinalizado = 0;
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            DataTable resultadoIdepol = new DataTable();
            Operaciones objetoOperaciones = new Operaciones();
            UpdatesBD objetoUpdates = new UpdatesBD();
            string placaUnificada = pNumeroPlaca + pCorrelativoPlaca;

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();
                command.CommandText = " INSERT INTO inspeccion_vehiculo  " +
                                      " (id_cotizacion, tipo_vehiculo, marca_vehiculo, " +
                                      " linea_vehiculo, anio_vehiculo, tarjeta_circulacion, " +
                                      " numero_chasis, tipo_placa, numero_placa, " +
                                      " correlativo_placa, numero_placa_vehiculo_unificada, " +
                                      " cilindraje, tonelaje, color, numero_motor, " +
                                      " numero_inspeccion, comentarios_inspeccion,fecha_inspeccion) " +
                                      " VALUES (@1, @2, @3, @4, @5, @6, @7, @8, @9, @10, @11, @12, @13, @14, @15, @16, @17,NOW()) ";

                command.Parameters.AddWithValue("@1", pIdCotiacion);
                command.Parameters.AddWithValue("@2", pTipoVehiculo);
                command.Parameters.AddWithValue("@3", pMarcaVehiculo);
                command.Parameters.AddWithValue("@4", pLineaVehiculo);
                command.Parameters.AddWithValue("@5", pAnioVehiculo);
                command.Parameters.AddWithValue("@6", pTarjetaCirculacion);
                command.Parameters.AddWithValue("@7", pNumeroChasis);
                command.Parameters.AddWithValue("@8", pTipoPlaca);
                command.Parameters.AddWithValue("@9", pNumeroPlaca);
                command.Parameters.AddWithValue("@10", pCorrelativoPlaca);
                command.Parameters.AddWithValue("@11", placaUnificada);
                command.Parameters.AddWithValue("@12", pCilindraje);
                command.Parameters.AddWithValue("@13", pTonelaje);
                command.Parameters.AddWithValue("@14", pColor);
                command.Parameters.AddWithValue("@15", pNumeroMotor);
                command.Parameters.AddWithValue("@16", pNumeroInspeccion);
                command.Parameters.AddWithValue("@17", pComentarios);

                command.ExecuteNonQuery();

                conexion.Close();

                resultado = true;

                if (resultado)
                {
                    procesoFinalizado = objetoOperaciones.obtenerIdEstadoCotizacion("INS");
                    resultado = objetoUpdates.UpdateEstadoCotizacion(pIdCotiacion, procesoFinalizado);   
                }
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        #endregion
    }
}
