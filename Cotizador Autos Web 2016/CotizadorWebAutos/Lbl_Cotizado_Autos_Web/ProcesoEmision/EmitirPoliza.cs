using Lbl_Cotizado_Autos_Web.Comunes;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using Lbl_Cotizado_Autos_Web.Estructuras;
using Lbl_Cotizado_Autos_Web.HogarSeguro;
using Lbl_Cotizador_Autos_Web.Acceso;
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

namespace Lbl_Cotizado_Autos_Web.ProcesoEmision
{
    public class EmitirPoliza
    {
        public string Emitir(string pIdCotizacion, DataTable usuarioLogueado, int pIdPlan, IngresoSistema.informacionUsuario informacionUsuario, string fechaInicioVigenciaPoliza = "")
        {
            string resp = string.Empty;
            string polizaActiva = string.Empty;
            string IDPLANPOL = string.Empty;
            string PLANPOL = string.Empty;

            DataTable informacionCotizacion = new DataTable();
            DataTable datosDelCliente = new DataTable();
            DataTable informacionVehiculo = new DataTable();
            DataTable telefonosCliente = new DataTable();                       
            DataTable coberturasPoliza = new DataTable();
            DataTable recargosPoliza = new DataTable();
            DataTable informacionPlanSeleccionado = new DataTable();

            //Primero se debe obtener el IDEPROCESO DEL PAQUETE 
            string IDEPROCESO = GET_IDPROCESO("ACSEL_WEB").ToString();

            //Se crea la estructura del asegurado para generar el XML
            List<CA_ASEGURADO> listadoAsegurados = new List<CA_ASEGURADO>();
            CA_ASEGURADO Asegurado = new CA_ASEGURADO();

            //Se crea la estructura del certificado para generar el XML
            List<CA_CERTIFICADO> listadoCertificados = new List<CA_CERTIFICADO>();                      
            CA_CERTIFICADO Certificado = new CA_CERTIFICADO();

            //Se crea la estructura de coberturas 
            List<CA_COBERTURA> listaCoberturas = new List<CA_COBERTURA>();
            CA_COBERTURA Cobertura = new CA_COBERTURA();

            //Se crea la estructura de  descuentos
            List<CA_REGARGO> listaRecargos = new List<CA_REGARGO>();           
            CA_REGARGO Recargo = new CA_REGARGO();
            
            informacionCotizacion = ObtenerInformacionCotizacion(pIdCotizacion);
            datosDelCliente = ObtenerInformacionCliente(pIdCotizacion);
            informacionVehiculo = ObtenerInformacionVehiculo(pIdCotizacion);
            telefonosCliente = ObtenerInformacionTelefonos(pIdCotizacion);
            coberturasPoliza = obtenerCoberturasPoliza(int.Parse(pIdCotizacion));
            recargosPoliza = obtenerRecargosPoliza(int.Parse(pIdCotizacion));
            informacionPlanSeleccionado = obtenerInformacionPlan(pIdPlan);

            IDPLANPOL = informacionPlanSeleccionado.Rows[0]["ide_plan_pol"].ToString();
            PLANPOL = informacionPlanSeleccionado.Rows[0]["plan_pol"].ToString();
            int contadorClientes = 1;

            foreach (DataRow cliente in datosDelCliente.Rows)
            {
                Asegurado = new CA_ASEGURADO();                
                string rolCliente = cliente["rol_cliente"].ToString();
                string tipoCliente = cliente["tipo_cliente"].ToString();

                Asegurado.USUARIO = informacionUsuario.codigoUsuarioRemoto;

                //Cliente Individual
                if (tipoCliente == "P")
                {
                    Asegurado.IDEPROCESO = IDEPROCESO;
                    Asegurado.LLAVE = "1";
                    Asegurado.LLAVE_ALTERNA = "1";
                    Asegurado.LINEA = contadorClientes.ToString();
                    Asegurado.LAYOUT = "0";
                    Asegurado.STSCA = "INC"; 

                    if (datosDelCliente.Rows.Count == 1)
                    {
                        Asegurado.CONTRATANTE = "S";
                        Asegurado.ASEGURADO = "S";
                        Asegurado.RESP_PAGO = "S";
                        Asegurado.BENEFICIARIO = "N";
                        Asegurado.DEPENDIENTE = "N";
                        Asegurado.PORCENTAJE = "100";
                    }   

                    if (datosDelCliente.Rows.Count == 2)
                    {
                        switch (rolCliente)
                        {
                            case "ASEG":
                                Asegurado.CONTRATANTE = "S";
                                Asegurado.ASEGURADO = "S";
                                Asegurado.RESP_PAGO = "N";
                                Asegurado.BENEFICIARIO = "N";
                                Asegurado.DEPENDIENTE = "N";
                                Asegurado.PORCENTAJE = "0";
                                break;
                            case "RESP":
                                Asegurado.CONTRATANTE = "N";
                                Asegurado.ASEGURADO = "N";
                                Asegurado.RESP_PAGO = "S";
                                Asegurado.BENEFICIARIO = "N";
                                Asegurado.DEPENDIENTE = "N";
                                Asegurado.PORCENTAJE = "100";
                                break;
                            default:
                                break;
                        } 
                    }

                    switch (telefonosCliente.Rows.Count)
                    {
                        case 1:
                            Asegurado.TELEFONO1 = telefonosCliente.Rows[0]["numero_telefono"].ToString();
                            break;
                        case 2:
                            for (int j = 0; j < telefonosCliente.Rows.Count - 1; j++)
                            {
                                Asegurado.TELEFONO1 = telefonosCliente.Rows[j]["numero_telefono"].ToString();
                                Asegurado.TELEFONO2 = telefonosCliente.Rows[j]["numero_telefono"].ToString();
                            }
                            break;
                        case 3:
                            for (int k = 0; k < telefonosCliente.Rows.Count - 1; k++)
                            {
                                Asegurado.TELEFONO1 = telefonosCliente.Rows[k]["numero_telefono"].ToString();
                                Asegurado.TELEFONO2 = telefonosCliente.Rows[k]["numero_telefono"].ToString();
                                Asegurado.TELEFONO3 = telefonosCliente.Rows[k]["numero_telefono"].ToString();
                            }
                            break;
                        case 4:
                            for (int l = 0; l < telefonosCliente.Rows.Count - 1; l++)
                            {
                                Asegurado.TELEFONO1 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                                Asegurado.TELEFONO2 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                                Asegurado.TELEFONO3 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                                Asegurado.TELEFONO4 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                            }
                            break;

                        default:
                            break;
                    }

                    Asegurado.NOMBRE = cliente["primer_nombre_individual"].ToString();
                    Asegurado.SEGUNDO_NOMBRE = cliente["segundo_nombre_individual"].ToString();
                    Asegurado.PRIMER_APELLIDO = cliente["primer_apellido_individual"].ToString();
                    Asegurado.SEGUNDO_APELLIDO = cliente["segundo_apellido_individual"].ToString();
                    Asegurado.GENERO = cliente["genero_individual"].ToString(); //OBLIGATORIO
                    Asegurado.FECHANAC = Convert.ToDateTime(cliente["fecha_nacimiento_individual"].ToString()).ToString("dd/MM/yyyy"); //OBLIGATORIO
                    Asegurado.NACIONAL_EXTRANJERO = "N"; //OBLIGATORIO VERIFICAR DESPUES
                    Asegurado.PAIS_NACIMIENTO = cliente["pais_emision_individual"].ToString(); //OBLIGATORIO
                    Asegurado.DEPARTAMENTO_NACIMIENTO = cliente["depto_emision_individual"].ToString(); //OBLIGATORIO
                    Asegurado.MUNICIPIO_NACIMIENTO = cliente["muni_emision_individual"].ToString(); //OBLIGATORIO
                    Asegurado.PAIS_CEDULA = cliente["pais_emision_individual"].ToString(); //OBLIGATORIO
                    Asegurado.DEPARTAMENTO_CEDULA = cliente["depto_emision_individual"].ToString(); //OBLIGATORIO
                    Asegurado.MUNICIPIO_CEDULA = cliente["muni_emision_individual"].ToString(); //OBLIGATORIO
                    Asegurado.NIT = cliente["nit_individual"].ToString(); //OBLIGATORIO CON GUION
                    Asegurado.TIPO_CLIENTE = tipoCliente;
                    Asegurado.ESTADO_CIVIL = cliente["estado_civil_individual"].ToString(); //OBLIGATORIO
                    Asegurado.ACTIVIDAD_ECONOMICA = cliente["profesion_individual"].ToString(); //OBLIGATORIO


                    //------------------------------DIRECCION CLIENTE

                    Asegurado.PAIS_DIRECCION = cliente["pais_emision_individual"].ToString(); 
                    Asegurado.DEPARTAMENTO_DIRECCION = cliente["depto_emision_individual"].ToString(); 
                    Asegurado.MUNICIPIO_DIRECCION = cliente["muni_emision_individual"].ToString();                     
                    Asegurado.DIRECCION = cliente["direccion_individual"].ToString();
                    Asegurado.ALDEA_LOCALIDAD_ZONA = cliente["zona_direccion_individual"].ToString();
                    Asegurado.CALLECLASIF = cliente["calle_direccion_individual"].ToString();
                    Asegurado.AVENIDACLASIF = cliente["avenida_direccion_individual"].ToString();
                    Asegurado.CASA_NUMERO = cliente["numero_casa_direccion_individual"].ToString();
                    Asegurado.COLONIA = cliente["colonia_direccion_individual"].ToString();
                    Asegurado.LOTE = cliente["lote_direccion_individual"].ToString();
                    Asegurado.SECTOR = cliente["sector_direccion_individual"].ToString();
                    Asegurado.MANZANA = cliente["manzana_direccion_individual"].ToString();

                    //-----------------------------------------------
                    
                    Asegurado.CORREO_ELECTRONICO = cliente["correo_electronico_individual"].ToString(); //OBLIGATORIO                
                    Asegurado.DPI = cliente["numero_identificacion_individual"].ToString(); //OBLIGATORIO
                    //18/05/2016
                    //Se agrego el id de la cotizacion para efectos de monitoreo.
                    Asegurado.IDBASE = pIdCotizacion;
                    Asegurado.CODPLANFRAC = informacionCotizacion.Rows[0]["codplanfracc"].ToString();
                    Asegurado.MODPLANFRAC = informacionCotizacion.Rows[0]["modplanfracc"].ToString();

                    listadoAsegurados.Add(Asegurado);
                }

                //Cliente Juridico
                if (tipoCliente == "E")
                {
                    Asegurado.IDEPROCESO = IDEPROCESO;
                    Asegurado.LLAVE = "1";
                    Asegurado.LLAVE_ALTERNA = "1";
                    Asegurado.LINEA = contadorClientes.ToString();
                    Asegurado.LAYOUT = "0";
                    Asegurado.STSCA = "INC";                   

                    if (datosDelCliente.Rows.Count == 1)
                    {
                        Asegurado.CONTRATANTE = "S";
                        Asegurado.ASEGURADO = "S";
                        Asegurado.RESP_PAGO = "S";
                        Asegurado.BENEFICIARIO = "N";
                        Asegurado.DEPENDIENTE = "N";
                        Asegurado.PORCENTAJE = "100";
                    }

                    if (datosDelCliente.Rows.Count == 2)
                    {
                        switch (rolCliente)
                        {
                            case "ASEG":
                                Asegurado.CONTRATANTE = "S";
                                Asegurado.ASEGURADO = "S";
                                Asegurado.RESP_PAGO = "S";
                                Asegurado.BENEFICIARIO = "N";
                                Asegurado.DEPENDIENTE = "N";
                                Asegurado.PORCENTAJE = "0";
                                break;
                            case "RESP":
                                Asegurado.CONTRATANTE = "N";
                                Asegurado.ASEGURADO = "N";
                                Asegurado.RESP_PAGO = "S";
                                Asegurado.BENEFICIARIO = "N";
                                Asegurado.DEPENDIENTE = "N";
                                Asegurado.PORCENTAJE = "100";
                                break;
                            default:
                                break;
                        }
                    }

                    switch (telefonosCliente.Rows.Count)
                    {
                        case 1:
                            Asegurado.TELEFONO1 = telefonosCliente.Rows[0]["numero_telefono"].ToString();
                            break;
                        case 2:
                            for (int j = 0; j < telefonosCliente.Rows.Count - 1; j++)
                            {
                                Asegurado.TELEFONO1 = telefonosCliente.Rows[j]["numero_telefono"].ToString();
                                Asegurado.TELEFONO2 = telefonosCliente.Rows[j]["numero_telefono"].ToString();
                            }
                            break;
                        case 3:
                            for (int k = 0; k < telefonosCliente.Rows.Count - 1; k++)
                            {
                                Asegurado.TELEFONO1 = telefonosCliente.Rows[k]["numero_telefono"].ToString();
                                Asegurado.TELEFONO2 = telefonosCliente.Rows[k]["numero_telefono"].ToString();
                                Asegurado.TELEFONO3 = telefonosCliente.Rows[k]["numero_telefono"].ToString();
                            }
                            break;
                        case 4:
                            for (int l = 0; l < telefonosCliente.Rows.Count - 1; l++)
                            {
                                Asegurado.TELEFONO1 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                                Asegurado.TELEFONO2 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                                Asegurado.TELEFONO3 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                                Asegurado.TELEFONO4 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                            }
                            break;

                        default:
                            break;
                    }
                    
                    Asegurado.NOMBRE = cliente["nombre_persona_juridica"].ToString();//nombreCliente; //OBLIGATORIO
                    Asegurado.GENERO = "N"; //OBLIGATORIO
                    Asegurado.FECHANAC = ""; //OBLIGATORIO
                    Asegurado.NACIONAL_EXTRANJERO = "N"; //OBLIGATORIO VERIFICAR DESPUES
                    Asegurado.PAIS_NACIMIENTO = ""; //OBLIGATORIO
                    Asegurado.DEPARTAMENTO_NACIMIENTO = ""; //OBLIGATORIO
                    Asegurado.MUNICIPIO_NACIMIENTO = ""; //OBLIGATORIO
                    Asegurado.PAIS_CEDULA = ""; //OBLIGATORIO
                    Asegurado.DEPARTAMENTO_CEDULA = ""; //OBLIGATORIO
                    Asegurado.MUNICIPIO_CEDULA = ""; //OBLIGATORIO
                    Asegurado.NIT = cliente["nit_persona_juridica"].ToString(); //OBLIGATORIO CON GUION
                    Asegurado.TIPO_CLIENTE = cliente["tipo_cliente"].ToString(); //OBLIGATORIO
                    Asegurado.ESTADO_CIVIL = "N"; //OBLIGATORIO
                    Asegurado.ACTIVIDAD_ECONOMICA = "985"; //OBLIGATORIO

                    //------------------------------DIRECCION CLIENTE
                    Asegurado.PAIS_DIRECCION = cliente["pais_direccion_empresa"].ToString(); //OBLIGATORIO
                    Asegurado.DEPARTAMENTO_DIRECCION = cliente["depto_direccion_empresa"].ToString(); //OBLIGATORIO
                    Asegurado.MUNICIPIO_DIRECCION = cliente["muni_direccion_empresa"].ToString(); //OBLIGATORIO                    
                    Asegurado.DIRECCION = cliente["direccion_empresa"].ToString(); //OBLIGATORIO
                    Asegurado.ALDEA_LOCALIDAD_ZONA = cliente["zona_direccion_empresa"].ToString();
                    Asegurado.CALLECLASIF = cliente["calle_direccion_empresa"].ToString();
                    Asegurado.AVENIDACLASIF = cliente["avenida_direccion_empresa"].ToString();
                    Asegurado.CASA_NUMERO = cliente["numero_casa_direccion_empresa"].ToString();
                    Asegurado.COLONIA = cliente["colonia_direccion_empresa"].ToString();
                    Asegurado.LOTE = cliente["lote_direccion_empresa"].ToString();
                    Asegurado.SECTOR = cliente["sector_direccion_empresa"].ToString();
                    Asegurado.MANZANA = cliente["manzana_direccion_empresa"].ToString();

                    //-----------------------------------------------

                    Asegurado.CORREO_ELECTRONICO = cliente["correo_electronico_juridico"].ToString(); //OBLIGATORIO
                    Asegurado.LINEA = contadorClientes.ToString(); //OBLIGATORIO AQUI SE ASIGNA CUANTAS PERSONAS ESTAN INVOLUCRADAS (RESP PAGO, CONTRATANTE ETC)
                    Asegurado.LAYOUT = "0"; //QUEMADO 0
                    Asegurado.STSCA = "INC"; //QUEMADO
                    Asegurado.DPI = "";
                    //18/05/2016
                    //Se agrego el id de la cotizacion para efectos de monitoreo.
                    Asegurado.IDBASE = pIdCotizacion;
                    Asegurado.CODPLANFRAC = informacionCotizacion.Rows[0]["codplanfracc"].ToString();
                    Asegurado.MODPLANFRAC = informacionCotizacion.Rows[0]["modplanfracc"].ToString();

                    listadoAsegurados.Add(Asegurado);
                }

                contadorClientes++;
            }

            Certificado.IDEPROCESO = IDEPROCESO;
            Certificado.LLAVE = "1";
            Certificado.LLAVE_ALTERNA = "1";
            Certificado.IDEPLANPOL = IDPLANPOL;
            Certificado.PLANPOL = PLANPOL;

            if (fechaInicioVigenciaPoliza == string.Empty)
            {
                Certificado.VIGENCIA_INICIAL = DateTime.Today.ToString("dd/MM/yyyy");
                Certificado.VIGENCIA_FINAL = DateTime.Today.AddDays(365).ToString("dd/MM/yyyy");
            }
            else
            {
                Certificado.VIGENCIA_INICIAL = fechaInicioVigenciaPoliza;
                Certificado.VIGENCIA_FINAL = DateTime.Parse(fechaInicioVigenciaPoliza).AddDays(365).ToString("dd/MM/yyyy");
            }
            
            Certificado.FECHA_INICIO_COBRO = DateTime.Today.ToString("dd/MM/yyyy");
            Certificado.PRIMER_PAGO_REALIZADO = "N"; //OBLIGATORIO QUEMADO
            Certificado.FORMA_PAGO = "M"; //VERIFICAR ESTO POR EL MOMENTO ESTA QUEMADO
            Certificado.PAGOS = informacionCotizacion.Rows[0]["numero_pagos_cot"].ToString();  //OBLIGATORIO
            Certificado.SUMAASEGURADA = informacionCotizacion.Rows[0]["monto_asegurado"].ToString();  //OBLIGATORIO
            Certificado.LINEA = "1";
            Certificado.LAYOUT = "0";
            Certificado.STSCA = "INC";
            Certificado.MONTO_ASEGURADO = informacionCotizacion.Rows[0]["monto_asegurado"].ToString();
            Certificado.TIPOVEH = informacionCotizacion.Rows[0]["tipoveh_vehiculo"].ToString();
            Certificado.CODMARCA = informacionCotizacion.Rows[0]["codmarca_vehiculo"].ToString();
            Certificado.CODMODELO = informacionCotizacion.Rows[0]["codmodelo_vehiculo"].ToString();
            Certificado.CODVERSION = informacionCotizacion.Rows[0]["codversion_vehiculo"].ToString(); //VERIFICAR ESTO POR EL MOMENTO ESTA QUEMADO
            Certificado.ANOVEH = informacionCotizacion.Rows[0]["anio_vehiculo"].ToString();
            Certificado.NUMPUESTOS = informacionCotizacion.Rows[0]["num_asientos_vehiculo"].ToString();
            Certificado.SECCION_III = informacionCotizacion.Rows[0]["seccion_iii"].ToString(); //VERIFICAR ESTO POR EL MOMENTO ESTA QUEMADO 
            Certificado.TIPO_OPERACION = "EMI"; // QUEMADO
            Certificado.SERIALMOTOR = informacionVehiculo.Rows[0]["numero_motor"].ToString();
            Certificado.SERIALCARROCERIA = informacionVehiculo.Rows[0]["numero_chasis"].ToString();
            Certificado.COLOR = informacionVehiculo.Rows[0]["color"].ToString();
            Certificado.NUMPLACA = informacionVehiculo.Rows[0]["numero_placa"].ToString() + informacionVehiculo.Rows[0]["correlativo_placa"].ToString();
            Certificado.TIPO_PLACA = informacionVehiculo.Rows[0]["tipo_placa"].ToString();
            Certificado.NOMBRE_EJECUTIVO = usuarioLogueado.Rows[0]["correo_electronico"].ToString(); 
            Certificado.CODINTER = informacionCotizacion.Rows[0]["codigo_intermediario"].ToString();           

            listadoCertificados.Add(Certificado);

            foreach (DataRow cob in coberturasPoliza.Rows)
            {
                Cobertura = new CA_COBERTURA();
                Cobertura.CODIGO = cob["codigo_cobertura"].ToString();
                Cobertura.SUMAASEGURADA = cob["suma_asegurada"].ToString();
                Cobertura.PRIMA = cob["prima_cobertura"].ToString();
                Cobertura.TASA = cob["tasa_cobertura"].ToString();
                Cobertura.RAMO = cob["ramo_cobertura"].ToString();
                Cobertura.IDEPROCESO = IDEPROCESO.ToString();
                Cobertura.LLAVE = "1";
                Cobertura.LLAVE_ALTERNA = "1";
                Cobertura.LINEA = "1";

                listaCoberturas.Add(Cobertura);
            }

            foreach (DataRow rec in recargosPoliza.Rows)
            {
                Recargo = new CA_REGARGO();
                Recargo.CODIGO = rec["codigo_recargo"].ToString();
                Recargo.TIPO = rec["tipo_recargo"].ToString();
                Recargo.PORCENTAJE = rec["porcentaje_recargo"].ToString();
                Recargo.MONTO = rec["monto_recargo"].ToString();
                Recargo.IDEPROCESO = IDEPROCESO.ToString();
                Recargo.LLAVE = "1";
                Recargo.LLAVE_ALTERNA = "1";
                Recargo.LINEA = "1";

                listaRecargos.Add(Recargo);
            }
            
            //prueba
            //Luego se generan los xml con los datos de los layouts
            string xmlAsegurado = generarXML_CA_ASEGURADO(listadoAsegurados);
            string xmlCertificado = generarXML_CA_CERTIFICADO(listadoCertificados, listaCoberturas, listaRecargos);            

            //Se ejecuta el proceso de carga de lote que lleva como parametros los xml generados anteriormente
            string respuesta = CARGA_LOTE(Convert.ToInt32(IDEPROCESO), "", xmlCertificado, xmlAsegurado, string.Empty);

            if (respuesta.Contains("satisfactoriamente"))
            {
                CARGA_ARCHXML(Convert.ToInt32(IDEPROCESO));

                resp =  CREAR(Convert.ToInt32(IDEPROCESO));

                if (resp == string.Empty)
                {
                     polizaActiva = PolizaActivarEmitida(IDEPROCESO);

                    if (!polizaActiva.Contains("ORA"))
                    {
                        DataTable informacionPoliza = new DataTable();
                        informacionPoliza = ObtenerInformacionPolizaEmitida(IDEPROCESO);

                        string mNumpol = string.Empty;
                        string mCodPol = string.Empty;

                        mNumpol = informacionPoliza.Rows[0]["NUMPOL"].ToString();
                        mCodPol = informacionPoliza.Rows[0]["CODPOL"].ToString();

                        InsertarInformacionPolizaEmitidia(int.Parse(pIdCotizacion), IDEPROCESO,
                            informacionUsuario.idUsuario, informacionPoliza.Rows[0]["IDEPOL"].ToString(), 
                            mCodPol, mNumpol);

                        resp = "La poliza se emitio correctamente. " + mCodPol + "-" + mNumpol;
                    }
                    else
                    {
                        resp = "No se activo la poliza. IDEPROCESO = " + IDEPROCESO+" "+polizaActiva;
                    }
                }
                else
                {
                    string err = "No se creo la poliza. \n " + resp + " " +  IDEPROCESO;

                    resp = err;
                }
            }

            return resp;
        }
        public string EmitirPolizaHogarSeguro(int idCotizacion, int idPlanSeleccionado, IngresoSistema.informacionUsuario informacionUsuario)
        {            
            string resultadoPoliza = string.Empty;
            string NumOperPolizaEmitida = string.Empty;
            string idePlanPol = string.Empty;
            string planPol = string.Empty;
            string cCodprod = string.Empty;
            string cCodplan = string.Empty;
            string cRevplan = string.Empty;
            int contadorClientes = 1;
            Varias clVarias = new Varias();

            DataTable cotizacion = new DataTable();
            DataTable datosDelCliente = new DataTable();
            DataTable telefonosCliente = new DataTable();
            DataTable planSeleccionado = new DataTable();
            DataTable usuarioLogueado = new DataTable();
            DataTable datosBien = new DataTable();

            ConsultasBD clConsultasBD = new ConsultasBD();
            EmitirPoliza clEmitirPoliza = new EmitirPoliza();
            Proceso_Emision_Hogar_Seguro clEmisionHogar = new Proceso_Emision_Hogar_Seguro();

            planSeleccionado = clVarias.obtenerInformacionPlan(idPlanSeleccionado);
            datosDelCliente = clEmitirPoliza.ObtenerInformacionCliente(idCotizacion.ToString());
            telefonosCliente = clEmitirPoliza.ObtenerInformacionTelefonos(idCotizacion.ToString());
            cotizacion = clVarias.obtenerInformacionCotizacion(idCotizacion);
            usuarioLogueado = clVarias.obtenerInformacionUsuarioLogueado(informacionUsuario.idUsuario);
            datosBien = clEmisionHogar.obtenerDireccionBien(idCotizacion);

            cCodprod = planSeleccionado.Rows[0]["codprod"].ToString();
            cCodplan = planSeleccionado.Rows[0]["codplan"].ToString();
            cRevplan = planSeleccionado.Rows[0]["revplan"].ToString();
            idePlanPol = planSeleccionado.Rows[0]["ide_plan_pol"].ToString();
            planPol = planSeleccionado.Rows[0]["plan_pol"].ToString();

            //Primero se debe obtener el IDEPROCESO DEL PAQUETE 
            int IDEPROCESO = clConsultasBD.GET_IDPROCESO("ACSEL_WEB");

            //Se crea la estructura del asegurado para generar el XML
            List<CA_ASEGURADO> listadoAsegurados = new List<CA_ASEGURADO>();
            CA_ASEGURADO Asegurado = new CA_ASEGURADO();

            //Se crea la estructura del certificado para generar el XML
            List<CA_CERTIFICADO> listadoCertificados = new List<CA_CERTIFICADO>();
            CA_CERTIFICADO Certificado = new CA_CERTIFICADO();

            //Se crea la estructura de coberturas 
            List<CA_COBERTURA> listaCoberturas = new List<CA_COBERTURA>();

            //Se crea la estructura de  descuentos
            List<CA_REGARGO> listaRecargos = new List<CA_REGARGO>();


            foreach (DataRow cliente in datosDelCliente.Rows)
            {
                Asegurado = new CA_ASEGURADO();
                string rolCliente = cliente["rol_cliente"].ToString();
                string tipoCliente = cliente["tipo_cliente"].ToString();

                Asegurado.USUARIO = informacionUsuario.codigoUsuarioRemoto;

                //Cliente Individual
                if (tipoCliente == "P")
                {
                    Asegurado.IDEPROCESO = IDEPROCESO.ToString();
                    Asegurado.LLAVE = "1";
                    Asegurado.LLAVE_ALTERNA = "1";
                    Asegurado.LINEA = contadorClientes.ToString();
                    Asegurado.LAYOUT = "0";
                    Asegurado.STSCA = "INC";

                    if (datosDelCliente.Rows.Count == 1)
                    {
                        Asegurado.CONTRATANTE = "S";
                        Asegurado.ASEGURADO = "S";
                        Asegurado.RESP_PAGO = "S";
                        Asegurado.BENEFICIARIO = "N";
                        Asegurado.DEPENDIENTE = "N";
                        Asegurado.PORCENTAJE = "100";
                    }

                    if (datosDelCliente.Rows.Count == 2)
                    {
                        switch (rolCliente)
                        {
                            case "ASEG":
                                Asegurado.CONTRATANTE = "S";
                                Asegurado.ASEGURADO = "S";
                                Asegurado.RESP_PAGO = "N";
                                Asegurado.BENEFICIARIO = "N";
                                Asegurado.DEPENDIENTE = "N";
                                Asegurado.PORCENTAJE = "0";
                                break;
                            case "RESP":
                                Asegurado.CONTRATANTE = "N";
                                Asegurado.ASEGURADO = "N";
                                Asegurado.RESP_PAGO = "S";
                                Asegurado.BENEFICIARIO = "N";
                                Asegurado.DEPENDIENTE = "N";
                                Asegurado.PORCENTAJE = "100";
                                break;
                            default:
                                break;
                        }
                    }

                    switch (telefonosCliente.Rows.Count)
                    {
                        case 1:
                            Asegurado.TELEFONO1 = telefonosCliente.Rows[0]["numero_telefono"].ToString();
                            break;
                        case 2:
                            for (int j = 0; j < telefonosCliente.Rows.Count - 1; j++)
                            {
                                Asegurado.TELEFONO1 = telefonosCliente.Rows[j]["numero_telefono"].ToString();
                                Asegurado.TELEFONO2 = telefonosCliente.Rows[j]["numero_telefono"].ToString();
                            }
                            break;
                        case 3:
                            for (int k = 0; k < telefonosCliente.Rows.Count - 1; k++)
                            {
                                Asegurado.TELEFONO1 = telefonosCliente.Rows[k]["numero_telefono"].ToString();
                                Asegurado.TELEFONO2 = telefonosCliente.Rows[k]["numero_telefono"].ToString();
                                Asegurado.TELEFONO3 = telefonosCliente.Rows[k]["numero_telefono"].ToString();
                            }
                            break;
                        case 4:
                            for (int l = 0; l < telefonosCliente.Rows.Count - 1; l++)
                            {
                                Asegurado.TELEFONO1 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                                Asegurado.TELEFONO2 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                                Asegurado.TELEFONO3 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                                Asegurado.TELEFONO4 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                            }
                            break;

                        default:
                            break;
                    }

                    Asegurado.NOMBRE = cliente["primer_nombre_individual"].ToString();
                    Asegurado.SEGUNDO_NOMBRE = cliente["segundo_nombre_individual"].ToString();
                    Asegurado.PRIMER_APELLIDO = cliente["primer_apellido_individual"].ToString();
                    Asegurado.SEGUNDO_APELLIDO = cliente["segundo_apellido_individual"].ToString();
                    Asegurado.GENERO = cliente["genero_individual"].ToString();
                    Asegurado.FECHANAC = Convert.ToDateTime(cliente["fecha_nacimiento_individual"].ToString()).ToString("dd/MM/yyyy");
                    Asegurado.NACIONAL_EXTRANJERO = "N";
                    Asegurado.PAIS_NACIMIENTO = cliente["pais_emision_individual"].ToString();
                    Asegurado.DEPARTAMENTO_NACIMIENTO = cliente["depto_emision_individual"].ToString();
                    Asegurado.MUNICIPIO_NACIMIENTO = cliente["muni_emision_individual"].ToString();
                    Asegurado.PAIS_CEDULA = cliente["pais_emision_individual"].ToString();
                    Asegurado.DEPARTAMENTO_CEDULA = cliente["depto_emision_individual"].ToString();
                    Asegurado.MUNICIPIO_CEDULA = cliente["muni_emision_individual"].ToString();
                    Asegurado.NIT = cliente["nit_individual"].ToString();
                    Asegurado.TIPO_CLIENTE = tipoCliente;
                    Asegurado.ESTADO_CIVIL = cliente["estado_civil_individual"].ToString();
                    Asegurado.ACTIVIDAD_ECONOMICA = cliente["profesion_individual"].ToString();
                    Asegurado.PAIS_DIRECCION = cliente["pais_emision_individual"].ToString();
                    Asegurado.DEPARTAMENTO_DIRECCION = cliente["depto_emision_individual"].ToString();
                    Asegurado.MUNICIPIO_DIRECCION = cliente["muni_emision_individual"].ToString();
                    Asegurado.ALDEA_LOCALIDAD_ZONA = "000";
                    Asegurado.DIRECCION = cliente["direccion_individual"].ToString();
                    Asegurado.CORREO_ELECTRONICO = cliente["correo_electronico_individual"].ToString();
                    Asegurado.DPI = cliente["numero_identificacion_individual"].ToString();
                    Asegurado.IDBASE = idCotizacion.ToString();
                    Asegurado.CODPLANFRAC = cotizacion.Rows[0]["codplanfracc"].ToString();
                    Asegurado.MODPLANFRAC = cotizacion.Rows[0]["modplanfracc"].ToString();

                    listadoAsegurados.Add(Asegurado);
                }

                //Cliente Juridico
                if (tipoCliente == "E")
                {
                    Asegurado.IDEPROCESO = IDEPROCESO.ToString();
                    Asegurado.LLAVE = "1";
                    Asegurado.LLAVE_ALTERNA = "1";
                    Asegurado.LINEA = contadorClientes.ToString();
                    Asegurado.LAYOUT = "0";
                    Asegurado.STSCA = "INC";

                    if (datosDelCliente.Rows.Count == 1)
                    {
                        Asegurado.CONTRATANTE = "S";
                        Asegurado.ASEGURADO = "S";
                        Asegurado.RESP_PAGO = "S";
                        Asegurado.BENEFICIARIO = "N";
                        Asegurado.DEPENDIENTE = "N";
                        Asegurado.PORCENTAJE = "100";
                    }

                    if (datosDelCliente.Rows.Count == 2)
                    {
                        switch (rolCliente)
                        {
                            case "ASEG":
                                Asegurado.CONTRATANTE = "S";
                                Asegurado.ASEGURADO = "S";
                                Asegurado.RESP_PAGO = "S";
                                Asegurado.BENEFICIARIO = "N";
                                Asegurado.DEPENDIENTE = "N";
                                Asegurado.PORCENTAJE = "0";
                                break;
                            case "RESP":
                                Asegurado.CONTRATANTE = "N";
                                Asegurado.ASEGURADO = "N";
                                Asegurado.RESP_PAGO = "S";
                                Asegurado.BENEFICIARIO = "N";
                                Asegurado.DEPENDIENTE = "N";
                                Asegurado.PORCENTAJE = "100";
                                break;
                            default:
                                break;
                        }
                    }

                    switch (telefonosCliente.Rows.Count)
                    {
                        case 1:
                            Asegurado.TELEFONO1 = telefonosCliente.Rows[0]["numero_telefono"].ToString();
                            break;
                        case 2:
                            for (int j = 0; j < telefonosCliente.Rows.Count - 1; j++)
                            {
                                Asegurado.TELEFONO1 = telefonosCliente.Rows[j]["numero_telefono"].ToString();
                                Asegurado.TELEFONO2 = telefonosCliente.Rows[j]["numero_telefono"].ToString();
                            }
                            break;
                        case 3:
                            for (int k = 0; k < telefonosCliente.Rows.Count - 1; k++)
                            {
                                Asegurado.TELEFONO1 = telefonosCliente.Rows[k]["numero_telefono"].ToString();
                                Asegurado.TELEFONO2 = telefonosCliente.Rows[k]["numero_telefono"].ToString();
                                Asegurado.TELEFONO3 = telefonosCliente.Rows[k]["numero_telefono"].ToString();
                            }
                            break;
                        case 4:
                            for (int l = 0; l < telefonosCliente.Rows.Count - 1; l++)
                            {
                                Asegurado.TELEFONO1 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                                Asegurado.TELEFONO2 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                                Asegurado.TELEFONO3 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                                Asegurado.TELEFONO4 = telefonosCliente.Rows[l]["numero_telefono"].ToString();
                            }
                            break;

                        default:
                            break;
                    }

                    Asegurado.NOMBRE = cliente["nombre_persona_juridica"].ToString();
                    Asegurado.GENERO = "N";
                    Asegurado.FECHANAC = "";
                    Asegurado.NACIONAL_EXTRANJERO = "N";
                    Asegurado.PAIS_NACIMIENTO = "";
                    Asegurado.DEPARTAMENTO_NACIMIENTO = "";
                    Asegurado.MUNICIPIO_NACIMIENTO = "";
                    Asegurado.PAIS_CEDULA = "";
                    Asegurado.DEPARTAMENTO_CEDULA = "";
                    Asegurado.MUNICIPIO_CEDULA = "";
                    Asegurado.NIT = cliente["nit_persona_juridica"].ToString();
                    Asegurado.TIPO_CLIENTE = cliente["tipo_cliente"].ToString();
                    Asegurado.ESTADO_CIVIL = "N";
                    Asegurado.ACTIVIDAD_ECONOMICA = "985";
                    Asegurado.PAIS_DIRECCION = cliente["pais_direccion_empresa"].ToString();
                    Asegurado.DEPARTAMENTO_DIRECCION = cliente["depto_direccion_empresa"].ToString();
                    Asegurado.MUNICIPIO_DIRECCION = cliente["muni_direccion_empresa"].ToString();
                    Asegurado.ALDEA_LOCALIDAD_ZONA = "000";
                    Asegurado.DIRECCION = cliente["direccion_empresa"].ToString();
                    Asegurado.CORREO_ELECTRONICO = cliente["correo_electronico_juridico"].ToString();
                    Asegurado.LINEA = "1";
                    Asegurado.LAYOUT = "0";
                    Asegurado.STSCA = "INC";
                    Asegurado.DPI = "";

                    Asegurado.IDBASE = idCotizacion.ToString();
                    Asegurado.CODPLANFRAC = cotizacion.Rows[0]["codplanfracc"].ToString();
                    Asegurado.MODPLANFRAC = cotizacion.Rows[0]["modplanfracc"].ToString();

                    listadoAsegurados.Add(Asegurado);
                }

                contadorClientes++;
            }

            Certificado.IDEPROCESO = IDEPROCESO.ToString();
            Certificado.LLAVE = "1";
            Certificado.LLAVE_ALTERNA = "1";
            Certificado.IDEPLANPOL = idePlanPol;
            Certificado.PLANPOL = planPol;
            Certificado.VIGENCIA_INICIAL = DateTime.Today.ToString("dd/MM/yyyy");
            Certificado.VIGENCIA_FINAL = DateTime.Today.AddDays(365).ToString("dd/MM/yyyy");
            Certificado.FECHA_INICIO_COBRO = DateTime.Today.ToString("dd/MM/yyyy");
            Certificado.PRIMER_PAGO_REALIZADO = "N";
            Certificado.FORMA_PAGO = "A";
            Certificado.PAGOS = cotizacion.Rows[0]["numero_pagos_cot"].ToString();
            Certificado.SUMAASEGURADA = cotizacion.Rows[0]["monto_asegurado"].ToString();
            Certificado.MONTO_ASEGURADO = cotizacion.Rows[0]["monto_asegurado"].ToString();
            Certificado.LINEA = "1";
            Certificado.LAYOUT = "0";
            Certificado.STSCA = "INC";
            Certificado.TIPO_OPERACION = "EMI";
            Certificado.NOMBRE_EJECUTIVO = usuarioLogueado.Rows[0]["correo_electronico"].ToString();
            Certificado.DIRECC = datosBien.Rows[0]["direccion_bien"].ToString();
            Certificado.PAIS_RIESGO = datosBien.Rows[0]["pais_direccion"].ToString();
            Certificado.DEPARTAMENTO_RIESGO = datosBien.Rows[0]["depto_direccion"].ToString();
            Certificado.MUNICIPIO_RIESGO = datosBien.Rows[0]["muni_direccion"].ToString();
            Certificado.ALDEA_LOCALIDAD_ZONA_RIESGO = "000";
            Certificado.CODINTER = usuarioLogueado.Rows[0]["codigo_intermediario"].ToString();

            listadoCertificados.Add(Certificado);

            //Luego se generan los xml con los datos de los layouts
            string xmlAsegurado = clEmitirPoliza.generarXML_CA_ASEGURADO(listadoAsegurados);
            string xmlCertificado = clEmitirPoliza.generarXML_CA_CERTIFICADO(listadoCertificados, listaCoberturas, listaRecargos);

            //Se ejecuta el proceso de carga de lote que lleva como parametros los xml generados anteriormente
            string respuesta = clEmitirPoliza.CARGA_LOTE(Convert.ToInt32(IDEPROCESO), "", xmlCertificado, xmlAsegurado, string.Empty);

            if (respuesta.Contains("satisfactoriamente"))
            {
                clEmitirPoliza.CARGA_ARCHXML(Convert.ToInt32(IDEPROCESO));

                resultadoPoliza = clEmitirPoliza.CREAR(Convert.ToInt32(IDEPROCESO));

                if (resultadoPoliza == string.Empty)
                {
                    DataTable datosPolizaEmitida = new DataTable();
                    int idePolEmitido = 0;

                    datosPolizaEmitida = clEmitirPoliza.ObtenerIdepol(IDEPROCESO.ToString());

                    if (datosPolizaEmitida.Rows.Count == 0)
                    {
                        resultadoPoliza = "ORA:2016 No se generó ningún IDEPOL. IDEPROCESO: " + IDEPROCESO;
                    }
                    else
                    {
                        idePolEmitido = int.Parse(datosPolizaEmitida.Rows[0]["IDEPOL"].ToString());

                        NumOperPolizaEmitida = clEmitirPoliza.ActivarPolizaEmision(idePolEmitido.ToString());

                        if (!NumOperPolizaEmitida.Contains("ORA"))
                        {
                            clEmitirPoliza.GenerarAcreencia(Convert.ToInt32(NumOperPolizaEmitida));
                            clEmitirPoliza.GenerarFactura(Convert.ToInt32(NumOperPolizaEmitida));
                            clEmitirPoliza.CargaAutomaticaDistribuyeDocs(IDEPROCESO.ToString());
                            clEmitirPoliza.CobrarLoteFacturaEmisionCargaMasiva(IDEPROCESO.ToString());

                            DataTable polizaEmitida = new DataTable();
                            polizaEmitida = clEmitirPoliza.ObtenerInformacionPolizaEmitida(int.Parse(datosPolizaEmitida.Rows[0]["IDEPOL"].ToString()));

                            string numpolEmitido = string.Empty;
                            string codpolEmitido = string.Empty;

                            numpolEmitido = polizaEmitida.Rows[0]["NUMPOL"].ToString();
                            codpolEmitido = polizaEmitida.Rows[0]["CODPOL"].ToString();

                            clEmitirPoliza.InsertarInformacionPolizaEmitidia(idCotizacion, IDEPROCESO.ToString(),
                                informacionUsuario.idUsuario, idePolEmitido.ToString(),
                                codpolEmitido, numpolEmitido);

                            resultadoPoliza = "La poliza se emitio correctamente. " + codpolEmitido + "-" + numpolEmitido;
                        }
                        else
                        {
                            resultadoPoliza = "No se activó la poliza. IDEPROCESO = " + IDEPROCESO + ". ERROR: " + NumOperPolizaEmitida;
                        }
                    }
                }
                else
                {
                    string err = "No se creo la poliza. \n " + resultadoPoliza;

                    resultadoPoliza = err;
                }
            }

            return resultadoPoliza;
        }

        #region PROCESOS_ORACLE
        public int GET_IDPROCESO(string pCodUsr)
        {
            int resultado = 0;

            OracleConnection conexion = new OracleConnection();
            Conexiones clsConexion = new Conexiones();
            OracleCommand cmd = new OracleCommand();

            try
            {
                conexion = clsConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "pr_oper_sas.GET_IDPROCESO";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("pCodUsr", OracleDbType.Varchar2, 100);
                OracleParameter p2 = new OracleParameter("Return_Value", OracleDbType.Int32);

                p1.Value = pCodUsr;
                p2.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p1);

                cmd.ExecuteNonQuery();

                resultado = int.Parse(cmd.Parameters["Return_Value"].Value.ToString());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return resultado;
        }
        public string CARGA_LOTE(int pIdProceso, string pCodUsr, string pXML_Cert, string pXML_Aseg, string pXML_Bien, string pWebLogic = "S")
        {
            string resultado = string.Empty;

            OracleConnection conexion = new OracleConnection();
            Conexiones clsConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clsConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;


                if (pXML_Bien != string.Empty)
                {
                    cmd.CommandText = "pr_oper_sas.CARGA_LOTE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    OracleParameter p1 = new OracleParameter("pIdProceso", OracleDbType.Int32, 100);
                    OracleParameter p2 = new OracleParameter("pCodUsr", OracleDbType.Varchar2, 100);
                    OracleParameter p3 = new OracleParameter("pXML_Cert", OracleDbType.Clob);
                    OracleParameter p4 = new OracleParameter("pXML_Aseg", OracleDbType.Clob);
                    OracleParameter p5 = new OracleParameter("pXML_Bien", OracleDbType.Clob);
                    OracleParameter p6 = new OracleParameter("pWebLogic", OracleDbType.Varchar2, 100);
                    OracleParameter p7 = new OracleParameter("Return_Value", OracleDbType.Varchar2, 32767);

                    p1.Value = pIdProceso;
                    p2.Value = pCodUsr;
                    p3.Value = pXML_Cert;
                    p4.Value = pXML_Aseg;
                    p5.Value = pXML_Bien;
                    p6.Value = pWebLogic;
                    p7.Direction = ParameterDirection.ReturnValue;

                    cmd.Parameters.Add(p7);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    cmd.Parameters.Add(p5);
                    cmd.Parameters.Add(p6);
                }
                else
                {
                    cmd.CommandText = "pr_oper_sas.CARGA_LOTE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    OracleParameter p1 = new OracleParameter("pIdProceso", OracleDbType.Int32, 100);
                    OracleParameter p2 = new OracleParameter("pCodUsr", OracleDbType.Varchar2, 100);
                    OracleParameter p3 = new OracleParameter("pXML_Cert", OracleDbType.Clob);
                    OracleParameter p4 = new OracleParameter("pXML_Aseg", OracleDbType.Clob);
                    OracleParameter p5 = new OracleParameter("Return_Value", OracleDbType.Varchar2, 32767);

                    p1.Value = pIdProceso;
                    p2.Value = pCodUsr;
                    p3.Value = pXML_Cert;
                    p4.Value = pXML_Aseg;
                    p5.Direction = ParameterDirection.ReturnValue;

                    cmd.Parameters.Add(p5);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                }


                cmd.ExecuteNonQuery();

                resultado = cmd.Parameters["Return_Value"].Value.ToString();

                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        public void CARGA_ARCHXML(int IDEPROCESO)
        {
            //int resultado = 0;

            OracleConnection conexion = new OracleConnection();
            Conexiones clsConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clsConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "PR_CARGA_AUTOMATICA.CARGA_ARCHXML";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nIdProceso", OracleDbType.Int32);
                //OracleParameter p2 = new OracleParameter("Return_Value", OracleDbType.Int32);

                p1.Value = IDEPROCESO;
                //p2.Direction = ParameterDirection.ReturnValue;

                // cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p1);

                cmd.ExecuteNonQuery();

                conexion.Close();

                //resultado = int.Parse(cmd.Parameters["Return_Value"].Value.ToString());
            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }

            //return resultado;
        }
        public string CREAR(int IDEPROCESO)
        {
            string error = string.Empty;
            OracleConnection conexion = new OracleConnection();
            Conexiones clsConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clsConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "PR_CARGA_AUTOMATICA.CREAR";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nProceso", OracleDbType.Int32);

                p1.Value = IDEPROCESO;

                cmd.Parameters.Add(p1);

                cmd.ExecuteNonQuery();

                conexion.Close();

                error = verificarErrorCARGA(IDEPROCESO);

            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }
            return error;
        }

        private string verificarErrorCARGA(int IDPROCESO)
        {
            string descripcionERROR = string.Empty;

            string query = "select * from ca_bitacora where ideproceso = " + IDPROCESO;

            DataTable datos = new DataTable();                     

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    descripcionERROR = datos.Rows[0]["ERROR"].ToString();                    
                }

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
                descripcionERROR = ex.Message;
            }

            return descripcionERROR;
        }
        public DataTable ObtenerInformacionPolizaEmitida(string pIdeProceso)
        {
            DataTable datos = new DataTable();
            DataTable idepol = new DataTable();

            idepol = ObtenerIdepol(pIdeProceso);

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM POLIZA WHERE IDEPOL ='" + idepol.Rows[0]["IDEPOL"].ToString() + "'", conexion);

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
        public DataTable ObtenerInformacionPolizaEmitida(int idePol)
        {
            DataTable datos = new DataTable();
            DataTable idepol = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT * FROM POLIZA WHERE IDEPOL = " + idePol, conexion);

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
        public DataTable ObtenerIdepol(string pIdeProceso)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT IDEPOL FROM CA_CERTIFICADO WHERE IDEPROCESO ='" + pIdeProceso + "'", conexion);

            adapter.Fill(datos);

            conexion.Close();

            return datos;
        }        
        public void CargaAutomaticaDistribuyeDocs(string pIdeProceso)
        {
            string resultado = string.Empty;

            OracleConnection conexion = new OracleConnection();
            Conexiones clConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "pr_carga_automatica.distribuye_docs";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nIdeProceso", OracleDbType.Int32);

                cmd.Parameters.Add(p1);

                p1.Value = pIdeProceso;

                cmd.ExecuteNonQuery();

                conexion.Close();

            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }
        }
        public bool CobrarLoteFacturaEmisionCargaMasiva(string pIdeProceso)
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
                cmd.CommandText = "pr_cobro_lote_factura.emision_carga_masiva";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nIdeProceso", OracleDbType.Int32);

                cmd.Parameters.Add(p1);

                p1.Value = pIdeProceso;

                cmd.ExecuteNonQuery();

                respuesta = true;

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
                respuesta = false;
                throw ex;
            }

            return respuesta;
        }
        public void AplicaCobroAcsel(string pIdeProceso)
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
                cmd.CommandText = "pr_wsprocel.aplica_cobro";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nIdeProceso", OracleDbType.Int32);

                cmd.Parameters.Add(p1);

                p1.Value = pIdeProceso;

                cmd.ExecuteNonQuery();

                respuesta = true;

                conexion.Close();

            }
            catch (OracleException ex)
            {
                conexion.Close();
                //throw ex;
            }
        }
        public string ActivarPolizaEmision(string pIdepol)
        {
            string respuesta = string.Empty;
            string resultado = string.Empty;

            OracleConnection conexion = new OracleConnection();
            Conexiones clConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "PR_POLIZA.ACTIVAR";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nidepol", OracleDbType.Int32);
                OracleParameter p2 = new OracleParameter("ctipomov", OracleDbType.Varchar2, 32767);
                OracleParameter p3 = new OracleParameter("ctipoemi", OracleDbType.Varchar2, 32767);
                OracleParameter p4 = new OracleParameter("Return_Value", OracleDbType.Varchar2, 32767);

                p1.Value = pIdepol;
                p2.Value = "D";
                p3.Value = "E";
                p4.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);


                cmd.ExecuteNonQuery();

                resultado = cmd.Parameters["Return_Value"].Value.ToString();


                respuesta = resultado;

                conexion.Close();

            }
            catch (OracleException ex)
            {
                conexion.Close();
                respuesta = ex.Message;
            }

            return respuesta;
        }
        public void GenerarFactura(int pNumerOper)
        {
            string resultado = string.Empty;

            OracleConnection conexion = new OracleConnection();
            Conexiones clConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "pr_cond_financiamiento.generar_factura";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nNumOper", OracleDbType.Int32);

                p1.Value = pNumerOper;

                cmd.Parameters.Add(p1);

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }
        }
        public void GenerarAcreencia(int pNumerOper)
        {            
            string resultado = string.Empty;

            OracleConnection conexion = new OracleConnection();
            Conexiones clConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "pr_cond_financiamiento.generar_acreencia";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nNumOper", OracleDbType.Int32);

                p1.Value = pNumerOper;

                cmd.Parameters.Add(p1);

                cmd.ExecuteNonQuery();                

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }
        }
        public void AsignaDoctoPago(string nIdeproceso, string nNumOper, string nIdePol)
        {
            OracleConnection conexion = new OracleConnection();
            Conexiones clConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "pr_cond_financiamiento.generar_acreencia";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nIdeProceso", OracleDbType.Int32);
                OracleParameter p2 = new OracleParameter("nNumOper", OracleDbType.Int32);
                OracleParameter p3 = new OracleParameter("nIdePol ", OracleDbType.Int32);

                p1.Value = nIdeproceso;
                p2.Value = nNumOper;
                p3.Value = nIdePol;

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
            catch (OracleException ex)
            {
                //throw ex;
            }
        }        
        public DataTable ObtenerDescripcionVehiculos(string pTipoVehiculo, string pCodigoMarca, string pCodigoModelo, string pAnio)
        {
            DataTable resultado = new DataTable();
            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();

            try
            {
                conexion = conexionOracle.abrirConexionOracleAcsel();
                OracleDataAdapter adapter = new OracleDataAdapter(" SELECT LV.DESCRIP,MAR.DESCMARCA,M.DESCMODELO, " + pAnio + " ANIO" +
                                                                  " FROM MODELO_VEH M, MARCA_VEH MAR, LVAL LV " +
                                                                  " WHERE MAR.CODMARCA = '" + pCodigoMarca + "' " +
                                                                  " AND M.CODMODELO = '" + pCodigoModelo + "' " +
                                                                  " AND M.CODMARCA = MAR.CODMARCA " +
                                                                  " AND LV.TIPOLVAL = 'TIPOVEH' " +
                                                                  " AND LV.CODLVAL = '" + pTipoVehiculo + "'", conexion);
                adapter.Fill(resultado);
                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        #endregion

        #region PROCESO_MYSQL
        
        public void InsertarInformacionPolizaEmitidia(int pIdCotizacion, string pIDEPROCESO, int pIdeUsuario, string pIdePol, string pCodPol, string pNumPol)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            DataTable resultadoIdepol = new DataTable();

            int idEstadoCotizacion = obtenerIdEstadoCotizacion("EMI");

            try
            {
                conexion = clConexiones.abrirConexionMysql();

                MySqlCommand command = conexion.CreateCommand();
                command.CommandText = " update cotizacion set fecha_emision = now()"
                    + ", id_usuario_emitio = " + pIdeUsuario
                    + ", ideproceso_emi = '" + pIDEPROCESO + "'" 
                    + ", idepol_emi = '" + pIdePol + "' , codpol_emi = '" + pCodPol + "'" 
                    + ", numpol_emi = '" + pNumPol + "'"
                    + ", estado_cotizacion = " + idEstadoCotizacion
                    + "  where id_cotizacion = '" + pIdCotizacion + "'";

                command.ExecuteNonQuery();

                conexion.Close();

            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
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
        public DataTable ObtenerInformacionCotizacion(string pIdCotizacion)
        {
            DataTable resultado = new DataTable();
            DataTable datosVehiculo = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT * FROM cotizacion where id_cotizacion = '" + pIdCotizacion + "'", conexion);
            try
            {
                adapter.Fill(resultado);
                conexion.Close();
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        public DataTable ObtenerInformacionVehiculo(string pIdCotizacion)
        {
            DataTable resultado = new DataTable();
            DataTable datosVehiculo = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT * FROM inspeccion_vehiculo where id_cotizacion = '" + pIdCotizacion + "'", conexion);
            try
            {
                adapter.Fill(resultado);
                conexion.Close();
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        public DataTable ObtenerInformacionTelefonos(string pIdCotizacion)
        {
            DataTable resultado = new DataTable();
            DataTable datosVehiculo = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT numero_telefono FROM telefonos_cliente where id_cotizacion = '" + pIdCotizacion + "'", conexion);
            try
            {
                adapter.Fill(resultado);
                conexion.Close();
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        public DataTable ObtenerInformacionCliente(string pIdCotizacion)
        {
            DataTable resultado = new DataTable();
            DataTable datosVehiculo = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT * FROM cliente where numero_cotizacion = '" + pIdCotizacion + "'", conexion);
            try
            {
                adapter.Fill(resultado);
                conexion.Close();
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        public string PolizaActivarEmitida(string pIdeProceso)
        {
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            string resultado = string.Empty;
            bool resultadoCobroLote = false;
            DataTable resultadoIdepol = new DataTable();

            try
            {
                resultadoIdepol = ObtenerIdepol(pIdeProceso);

                if (resultadoIdepol.Rows[0]["IDEPOL"].ToString() != string.Empty)
                {
                    resultado = ActivarPolizaEmision(resultadoIdepol.Rows[0]["IDEPOL"].ToString());

                    if (!resultado.Contains("ORA"))
                    {
                        GenerarAcreencia(Convert.ToInt32(resultado));
                        GenerarFactura(Convert.ToInt32(resultado));
                        resultadoCobroLote = CobrarLoteFacturaEmisionCargaMasiva(pIdeProceso);
                        CargaAutomaticaDistribuyeDocs(pIdeProceso);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }

            return resultado;
        }
        public DataTable obtenerCoberturasPoliza(int idCotizacion)
        {
            DataTable resultado = new DataTable();
            DataTable datosVehiculo = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT * FROM coberturas_adicionales where id_cotizacion = " + idCotizacion, conexion);
            try
            {
                adapter.Fill(resultado);
                conexion.Close();
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        public DataTable obtenerRecargosPoliza(int idCotizacion)
        {
            DataTable resultado = new DataTable();
            DataTable datosVehiculo = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT * FROM recargos where id_cotizacion = " + idCotizacion, conexion);
            try
            {
                adapter.Fill(resultado);
                conexion.Close();
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        public DataTable obtenerInformacionPlan(int idPlan)
        {
            DataTable resultado = new DataTable();
            DataTable datosVehiculo = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT * FROM planes_web where estado = 1 and id_plan_web = " + idPlan, conexion);
            try
            {
                adapter.Fill(resultado);
                conexion.Close();
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }

        #endregion

        #region GeneracionXML
        public string generarXML_CA_ASEGURADO(List<CA_ASEGURADO> datosAsegurado)
        {
            var xmlfromLINQ = new XElement("PROCESO",
                       from c in datosAsegurado
                       select new XElement("ASEGURADO",
                           new XElement("IDEPROCESO", c.IDEPROCESO),
                             new XElement("LLAVE", c.LLAVE),
                             new XElement("LLAVE_ALTERNA", c.LLAVE_ALTERNA),
                             new XElement("CONTRATANTE", c.CONTRATANTE),
                             new XElement("ASEGURADO", c.ASEGURADO),
                             new XElement("RESP_PAGO", c.RESP_PAGO),
                             new XElement("BENEFICIARIO", c.BENEFICIARIO),
                             new XElement("DEPENDIENTE", c.DEPENDIENTE),
                             new XElement("CODIGO_CLIENTE", c.CODIGO_CLIENTE),
                             new XElement("NOMBRE", c.NOMBRE),
                             new XElement("PRIMER_APELLIDO", c.PRIMER_APELLIDO),
                             new XElement("SEGUNDO_APELLIDO", c.SEGUNDO_APELLIDO),
                             new XElement("APELLIDO_CASADA", c.APELLIDO_CASADA),
                             new XElement("GENERO", c.GENERO),
                             new XElement("FECHANAC", c.FECHANAC),
                             new XElement("NACIONAL_EXTRANJERO", c.NACIONAL_EXTRANJERO),
                             new XElement("PAIS_NACIMIENTO", c.PAIS_NACIMIENTO),
                             new XElement("DEPARTAMENTO_NACIMIENTO", c.DEPARTAMENTO_NACIMIENTO),
                             new XElement("MUNICIPIO_NACIMIENTO", c.MUNICIPIO_NACIMIENTO),
                             new XElement("CEDULA", c.CEDULA),
                             new XElement("PAIS_CEDULA", c.PAIS_CEDULA),
                             new XElement("DEPARTAMENTO_CEDULA", c.DEPARTAMENTO_CEDULA),
                             new XElement("MUNICIPIO_CEDULA", c.MUNICIPIO_CEDULA),
                             new XElement("PASAPORTE", c.PASAPORTE),
                             new XElement("NIT", c.NIT),
                             new XElement("TIPO_CLIENTE", c.TIPO_CLIENTE),
                             new XElement("ESTADO_CIVIL", c.ESTADO_CIVIL),
                             new XElement("ACTIVIDAD_ECONOMICA", c.ACTIVIDAD_ECONOMICA),
                             new XElement("OCUPACION", c.OCUPACION),
                             new XElement("PROFESION", c.PROFESION),
                             new XElement("DEPORTE", c.DEPORTE),
                             new XElement("PAIS_DIRECCION", c.PAIS_DIRECCION),
                             new XElement("DEPARTAMENTO_DIRECCION", c.DEPARTAMENTO_DIRECCION),
                             new XElement("MUNICIPIO_DIRECCION", c.MUNICIPIO_DIRECCION),
                             new XElement("ALDEA_LOCALIDAD_ZONA", c.ALDEA_LOCALIDAD_ZONA),
                             new XElement("DIRECCION", c.DIRECCION),
                             new XElement("COLONIA", c.COLONIA),
                             new XElement("ZONA_DIR", c.ZONA_DIR),
                             new XElement("TELEFONO1", c.TELEFONO1),
                             new XElement("TELEFONO2", c.TELEFONO2),
                             new XElement("TELEFONO3", c.TELEFONO3),
                             new XElement("TELEFONO4", c.TELEFONO4),
                             new XElement("CORREO_ELECTRONICO", c.CORREO_ELECTRONICO),
                             new XElement("PORCENTAJE", c.PORCENTAJE),
                             new XElement("PARENTESCO", c.PARENTESCO),
                             new XElement("TIPOID", c.TIPOID),
                             new XElement("NUMID", c.NUMID),
                             new XElement("DVID", c.DVID),
                             new XElement("CODCLI", c.CODCLI),
                             new XElement("NUEVACEDULA", c.NUEVACEDULA),
                             new XElement("RAMO", c.RAMO),
                             new XElement("USUARIO", c.USUARIO),
                             new XElement("FECHA", c.FECHA),
                             new XElement("REFERENCIA", c.REFERENCIA),
                             new XElement("LINEA", c.LINEA),
                             new XElement("LAYOUT", c.LAYOUT),
                             new XElement("STSCA", c.STSCA),
                             new XElement("DPI", c.DPI),
                             new XElement("TIPO_CTA", c.TIPO_CTA),
                             new XElement("IDBASE", c.IDBASE),
                             new XElement("SEGUNDO_NOMBRE", c.SEGUNDO_NOMBRE),
                             new XElement("CASA_NUMERO", c.CASA_NUMERO),
                             new XElement("APTO_SIMILAR", c.APTO_SIMILAR),
                             new XElement("AVENIDA", c.AVENIDA),
                             new XElement("CALLE", c.CALLE),
                             new XElement("LOTE", c.LOTE),
                             new XElement("MANZANA", c.MANZANA),
                             new XElement("AVENIDACLASIF", c.AVENIDACLASIF),
                             new XElement("CALLECLASIF", c.CALLECLASIF),
                             new XElement("SECTOR", c.SECTOR),
                             new XElement("ZONA", c.ZONA),
                             new XElement("RELACION_DEPENDENCIA", c.RELACION_DEPENDENCIA),
                             new XElement("NOMBRE_PATRONO", c.NOMBRE_PATRONO),
                             new XElement("FECHA_INGRESO", c.FECHA_INGRESO),
                             new XElement("TIPO_CUENTA", c.TIPO_CUENTA),
                             new XElement("NUMERO_CUENTA", c.NUMERO_CUENTA),
                             new XElement("ENTIDAD_CUENTA", c.ENTIDAD_CUENTA),
                             new XElement("CODIGO_ENTIDAD_TARJETA", c.CODIGO_ENTIDAD_TARJETA),
                             new XElement("AUTORIZACION", c.AUTORIZACION),
                             new XElement("USUARIO_AUTORIZACION", c.USUARIO_AUTORIZACION),
                             new XElement("NUMERO_LOTE", c.NUMERO_LOTE),
                             new XElement("MONTO_COBRADO", c.MONTO_COBRADO),
                             new XElement("NUMCHQ", c.NUMCHQ),
                             new XElement("FECSTS", c.FECSTS),
                             new XElement("MTOCHQ", c.MTOCHQ),
                             new XElement("DEDUCIBLE", c.DEDUCIBLE),
                             new XElement("TIMBRES", c.TIMBRES),
                             new XElement("IVA", c.IVA),
                             new XElement("ISR", c.ISR),
                             new XElement("CODCOBERT", c.CODCOBERT),
                             new XElement("MTOTOTRES", c.MTOTOTRES),
                             new XElement("CODPLANFRAC", c.CODPLANFRAC),
                             new XElement("MODPLANFRAC", c.MODPLANFRAC),
                             new XElement("FECVENC_CUENTA", c.FECVENC_CUENTA),
                             new XElement("INDPEP", c.INDPEP),
                             new XElement("ACTUA_NOMBRE_PROPIO", c.ACTUA_NOMBRE_PROPIO)
                           ));

            return xmlfromLINQ.ToString();
        }
        public string generarXML_CA_CERTIFICADO(List<CA_CERTIFICADO> datosCertificado, List<CA_COBERTURA> pCoberturas, List<CA_REGARGO> pRecargos)
        {
            var xmlfromLINQ = new XElement("PROCESO",
                        from c in datosCertificado
                        select new XElement("CERTIFICADO",
                                 new XElement("IDEPROCESO", c.IDEPROCESO),
                                 new XElement("LLAVE", c.LLAVE),
                                 new XElement("LLAVE_ALTERNA", c.LLAVE_ALTERNA),
                                 new XElement("IDEPLANPOL", c.IDEPLANPOL),
                                 new XElement("PLANPOL", c.PLANPOL),
                                 new XElement("CERTIFICADO", c.CERTIFICADO),
                                 new XElement("CERTIFICADO_REF", c.CERTIFICADO_REF),
                                 new XElement("FECHA_INICIAL_CUENTA_CREDITO", c.FECHA_INICIAL_CUENTA_CREDITO),
                                 new XElement("FECHA_FINAL_CUENTA_CREDITO", c.FECHA_FINAL_CUENTA_CREDITO),
                                 new XElement("VIGENCIA_INICIAL", c.VIGENCIA_INICIAL),
                                 new XElement("VIGENCIA_FINAL", c.VIGENCIA_FINAL),
                                 new XElement("FECHA_INICIO_COBRO", c.FECHA_INICIO_COBRO),
                                 new XElement("PRIMER_PAGO_REALIZADO", c.PRIMER_PAGO_REALIZADO),
                                 new XElement("FORMA_PAGO", c.FORMA_PAGO),
                                 new XElement("PAGOS", c.PAGOS),
                                 new XElement("MONTO_ASEGURADO", c.MONTO_ASEGURADO),
                                 new XElement("PRIMA_COBRAR", c.PRIMA_COBRAR),
                                 new XElement("TIPO_CUENTA", c.TIPO_CUENTA),
                                 new XElement("NUMERO_CUENTA", c.NUMERO_CUENTA),
                                 new XElement("ENTIDAD_CUENTA", c.ENTIDAD_CUENTA),
                                 new XElement("CODIGO_ENTIDAD_TARJETA", c.CODIGO_ENTIDAD_TARJETA),
                                 new XElement("IDEPOL", c.IDEPOL),
                                 new XElement("NUMCERT", c.NUMCERT),
                                 new XElement("USUARIO", c.USUARIO),
                                 new XElement("FECHA", c.FECHA),
                                 new XElement("LINEA", c.LINEA),
                                 new XElement("LAYOUT", c.LAYOUT),
                                 new XElement("STSCA", c.STSCA),
                                 new XElement("SUMA_VALIDA", c.SUMA_VALIDA),
                                 new XElement("TIPOVEH", c.TIPOVEH),
                                 new XElement("CODMARCA", c.CODMARCA),
                                 new XElement("CODMODELO", c.CODMODELO),
                                 new XElement("CODVERSION", c.CODVERSION),
                                 new XElement("TIPO_PLACA", c.TIPO_PLACA),
                                 new XElement("NUMPLACA", c.NUMPLACA),
                                 new XElement("ANOVEH", c.ANOVEH),
                                 new XElement("COLOR", c.COLOR),
                                 new XElement("SERIALCARROCERIA", c.SERIALCARROCERIA),
                                 new XElement("SERIALMOTOR", c.SERIALMOTOR),
                                 new XElement("USO", c.USO),
                                 new XElement("TITULO", c.TITULO),
                                 new XElement("EXCESO_RC", c.EXCESO_RC),
                                 new XElement("SECCION_III", c.SECCION_III),
                                 new XElement("NUMPUESTOS", c.NUMPUESTOS),
                                 new XElement("MOD_PAGOS", c.MOD_PAGOS),
                                 new XElement("SUMAASEGURADA", c.SUMAASEGURADA),
                                 new XElement("VIGENCIA_FINAL_EMITIDA", c.VIGENCIA_FINAL_EMITIDA),
                                 new XElement("NUMERO_CUENTA_VENCE", c.NUMERO_CUENTA_VENCE),
                                 new XElement("TIPO_CTA_PAGO", c.TIPO_CTA_PAGO),
                                 new XElement("NUMERO_CTA_PAGO", c.NUMERO_CTA_PAGO),
                                 new XElement("ENTIDAD_CTA_PAGO", c.ENTIDAD_CTA_PAGO),
                                 new XElement("CODIGO_ENTIDAD_TARJETA_PAGO", c.CODIGO_ENTIDAD_TARJETA_PAGO),
                                 new XElement("NUMPOL", c.NUMPOL),
                                 new XElement("ALARMA", c.ALARMA),
                                 new XElement("COD_AGENCIA", c.COD_AGENCIA),
                                 new XElement("COD_EJECUTIVO", c.COD_EJECUTIVO),
                                 new XElement("CODAGENCIADISTRIBUIDOR", c.CODAGENCIADISTRIBUIDOR),
                                 new XElement("MONTO_CUOTA", c.MONTO_CUOTA),
                                 new XElement("CORPORATIVO", c.CORPORATIVO),
                                 new XElement("NOMBRE_EJECUTIVO", c.NOMBRE_EJECUTIVO),
                                 new XElement("AFILIADA", c.AFILIADA),
                                 new XElement("CATEGORIA", c.CATEGORIA),
                                 new XElement("CLASEBIEN", c.CLASEBIEN),
                                 new XElement("CODBIEN", c.CODBIEN),
                                 new XElement("MARCA", c.MARCA),
                                 new XElement("MODELO", c.MODELO),
                                 new XElement("SERIE", c.SERIE),
                                 new XElement("DESCRIP", c.DESCRIP),
                                 new XElement("DIRECC", c.DIRECC),
                                 new XElement("PAIS_RIESGO", c.PAIS_RIESGO),
                                 new XElement("DEPARTAMENTO_RIESGO", c.DEPARTAMENTO_RIESGO),
                                 new XElement("MUNICIPIO_RIESGO", c.MUNICIPIO_RIESGO),
                                 new XElement("ALDEA_LOCALIDAD_ZONA_RIESGO", c.ALDEA_LOCALIDAD_ZONA_RIESGO),
                                 new XElement("CODMOTVEXC", c.CODMOTVEXC),
                                 new XElement("TIPO_OPERACION", c.TIPO_OPERACION),
                                 new XElement("CUOTAS_COBRADAS", c.CUOTAS_COBRADAS),
                                 new XElement("MONTO_COBRADO", c.MONTO_COBRADO),
                                 new XElement("CODINTER", c.CODINTER),
                                 new XElement("COBERTURAS",
                                                            from x in pCoberturas
                                                            select new XElement("COBERTURA",
                                                            new XElement("RAMO", x.RAMO),
                                                            new XElement("CODIGO", x.CODIGO),
                                                            new XElement("SUMAASEGURADA", x.SUMAASEGURADA),
                                                            new XElement("PRIMA", x.PRIMA),
                                                            new XElement("IDEPROCESO", x.IDEPROCESO),
                                                            new XElement("LLAVE", x.LLAVE),
                                                            new XElement("LLAVE_ALTERNA", x.LLAVE_ALTERNA),
                                                            new XElement("LINEA", x.LINEA),
                                                            new XElement("TASA", x.TASA))
                                                            ),
                                new XElement("RECARGOS",
                                                            from y in pRecargos
                                                            select new XElement("RECARGO",
                                                            new XElement("CODIGO", y.CODIGO),
                                                            new XElement("TIPO", y.TIPO),
                                                            new XElement("PORCENTAJE", y.PORCENTAJE),
                                                            new XElement("IDEPROCESO", y.IDEPROCESO),
                                                            new XElement("LLAVE", y.LLAVE),
                                                            new XElement("LLAVE_ALTERNA", y.LLAVE_ALTERNA),
                                                            new XElement("LINEA", y.LINEA),
                                                            new XElement("MONTO", y.MONTO))
                                                            )
                            ));

            return xmlfromLINQ.ToString();
        }
        #endregion       
    }
}
