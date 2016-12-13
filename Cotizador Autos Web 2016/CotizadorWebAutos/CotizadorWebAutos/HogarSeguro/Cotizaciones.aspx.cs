using Lbl_Cotizado_Autos_Web.Acceso;
using Lbl_Cotizado_Autos_Web.Comunes;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using Lbl_Cotizado_Autos_Web.Estructuras;
using Lbl_Cotizado_Autos_Web.HogarSeguro;
using Lbl_Cotizado_Autos_Web.ProcesoEmision;
using Lbl_Cotizador_Autos_Web.Acceso;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using Lbl_Cotizado_Autos_Web.CotizacionesMysql;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Updates;
//using Lbl_Cotizado_Autos_Web.Acceso;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace CotizadorWebAutos.HogarSeguro
{
    public partial class Cotizaciones : System.Web.UI.Page
    {
        public DataTable rolesUsuario = new DataTable();

        IngresoSistema.informacionUsuario informacionUsuario;

        UpdatesBD objetoUpdate = new UpdatesBD();
        ConsultasBD objectoConsultas = new ConsultasBD();
        ReportDocument objetoReporte = new ReportDocument();
        DataTable InformacionCotizacionMYSQL = new DataTable("DT_Prime_Dolares_MYSQL");
        DataTable InformacionCotizacionORACLE = new DataTable("DT_Prime_Dolares_ORACLE");
        DataTable InformacionBeneficios = new DataTable("DT_Beneficios");
        DataTable InformacionPlanesWeb = new DataTable("DT_Planes_Web");
        DataSet ds = new DataSet("DS_Prime_Dolares");
        DataTable descripcionNombresPlanes = new DataTable();
        string nombrePlan = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();
                AccesoUsuario clAccesoUsuario = new AccesoUsuario();

                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];

                if (!IsPostBack)
                {
                    mostrarCotizaciones();
                }
            }            
        }
        private void mostrarCotizaciones()
        {
            Proceso_Cotizacion clProcesoCotizacion = new Proceso_Cotizacion();

            if (informacionUsuario.accionesPermitidas.Contains("Ver_Todas_Cotizaciones"))
            {
                grvCotizacionesIntermediario.DataSource = clProcesoCotizacion.obtenerTodasCotizacionesHogarSeguro();
                grvCotizacionesIntermediario.DataBind();
                return;
            }
            else if (informacionUsuario.accionesPermitidas.Contains("Ver_Cotizaciones_Usuario"))
            {
                grvCotizacionesIntermediario.DataSource = clProcesoCotizacion.obtenerCotizacionesHogarSeguroXUsuario(informacionUsuario.idUsuario);
                grvCotizacionesIntermediario.DataBind();
                return;
            }            
        }
        protected void btnCotizacionNueva_Click(object sender, EventArgs e)
        {
            Response.Redirect("../HogarSeguro/Cotizar.aspx");
        }
        protected void ingresarCliente(Object sender, CommandEventArgs e)
        {            
            string idPlan = string.Empty;
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int cotSeleccionada = int.Parse(arg[0]);
            int idPlanSeleccionado = int.Parse(arg[1]);

            crearVariablesSession(cotSeleccionada, idPlanSeleccionado);
            Response.Redirect("../Cliente/AseguradoTitular.aspx");
        }        
        protected void ingresarEmision(Object sender, CommandEventArgs e)
        {
            string idPlan = string.Empty;
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int cotSeleccionada = int.Parse(arg[0]);
            int idPlanSeleccionado = int.Parse(arg[1]);

            string respuesta = string.Empty;

            respuesta = EmitirPolizaHogarSeguro(cotSeleccionada, idPlanSeleccionado);

            if (!respuesta.Contains("ORA"))
            {
                mensajeUrl(respuesta, "../HogarSeguro/Cotizaciones.aspx");
                lblError.Visible = false;
            }
            else
            {
                lblError.Text = respuesta;
                lblError.Visible = true;
            }           
        }        
        protected void eliminarCotizacion(Object sender, CommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');
            int cotSeleccionada = int.Parse(arg[0]);
           
            bool resultadoEliminar = false;
            Proceso_Cotizacion clProcesoCotizacion = new Proceso_Cotizacion();

            resultadoEliminar = objetoUpdate.DeleteInformacionCotizacionOracle(cotSeleccionada.ToString());

            mostrarCotizaciones();                 
        }
        protected void imprimirCotizacion(Object sender, CommandEventArgs e)
        {
            //int idCotizacionSeleccionada = int.Parse(e.CommandArgument.ToString());
            string ubicacionPDF = Server.MapPath(("~/Reportes/Generados/"));
            string correoUsuario = string.Empty;
            
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int cotSeleccionada = int.Parse(arg[0]);
            int idPlanSeleccionado = int.Parse(arg[1]);

            string idPlanString = idPlanSeleccionado.ToString();

            descripcionNombresPlanes = objectoConsultas.ObtenerNombrePlan(idPlanString);
            nombrePlan = descripcionNombresPlanes.Rows[0]["nombre"].ToString();
            InformacionCotizacionMYSQL = objectoConsultas.ObtenerInformacionCotizacionMYSQLHOGAR(cotSeleccionada.ToString());
            InformacionCotizacionORACLE = objectoConsultas.ObtenerInformacionCotizacionORACLE(cotSeleccionada.ToString());
            InformacionPlanesWeb = objectoConsultas.ObtenerNombrePlan(idPlanString);
            ds.Tables.Add(InformacionCotizacionMYSQL);
            string nombrePDF = informacionUsuario.idUsuario + "_" + DateTime.Now.Date.ToString("ddMMyyyy") + cotSeleccionada + nombrePlan;
            string archivoPDF = ubicacionPDF + nombrePDF;

            switch (nombrePlan)
            {
                case "HOGAR SEGURO DIAMANTE/PROPIETARIO":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/HogarSeguro.rpt"));
                    break;
                case "HOGAR SEGURO RUBI/PROPIETARIO":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/HogarSeguro.rpt"));
                    break;
                case "HOGAR SEGURO ESMERALDA/PROPIETARIO":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/HogarSeguro.rpt"));
                    break;
                case "HOGAR SEGURO DIAMANTE/INQUILINO":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/HogarSeguroInqui.rpt"));
                    break;
                case "HOGAR SEGURO RUBI/INQUILINO":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/HogarSeguroInqui.rpt"));
                    break;
                case "HOGAR SEGURO ESMERALDA/INQUILINO":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/HogarSeguroInqui.rpt"));
                    break;
            }

            objetoReporte.Database.Tables[0].SetDataSource(InformacionCotizacionMYSQL);
            objetoReporte.Database.Tables[1].SetDataSource(InformacionCotizacionORACLE);

            objetoReporte.ExportToHttpResponse
                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, nombrePDF); 
        }
        protected void grvCotizacionesIntermediario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnINC = (LinkButton)e.Row.FindControl("btnINC");
                LinkButton btnEMI = (LinkButton)e.Row.FindControl("btnEMI"); 

                if (informacionUsuario.accionesPermitidas.Contains("Ingresar_Cliente") || informacionUsuario.accionesPermitidas.Contains("Emitir"))
                {
                    if (e.Row.Cells[4].Text == "VAL")
                    {
                        btnINC.Visible = true;
                        btnEMI.Visible = false;                            
                    }

                    if (e.Row.Cells[4].Text == "INC")
                    {
                        btnINC.Visible = false;                           
                        btnEMI.Visible = true;
                    }
                }            
            }

            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;              
        }
        private void mostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        private void mensajeUrl(string mensaje, string url)
        {
            string script = "window.onload = function(){ alert('";
            script += mensaje;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "Redirect", script, true);
        }
        private string EmitirPolizaHogarSeguro(int idCotizacion, int idPlanSeleccionado)
        {
            lblError.Visible = false;
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
            DataTable datosBien = new DataTable();

            ConsultasBD clConsultasBD = new ConsultasBD();
            EmitirPoliza clEmitirPoliza = new EmitirPoliza();
            Proceso_Emision_Hogar_Seguro clEmisionHogar = new Proceso_Emision_Hogar_Seguro();

            planSeleccionado = clVarias.obtenerInformacionPlan(idPlanSeleccionado);
            datosDelCliente = clEmitirPoliza.ObtenerInformacionCliente(idCotizacion.ToString());
            telefonosCliente = clEmitirPoliza.ObtenerInformacionTelefonos(idCotizacion.ToString());
            cotizacion = clVarias.obtenerInformacionCotizacion(idCotizacion);                     
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
                    Asegurado.ALDEA_LOCALIDAD_ZONA = cliente["zona_direccion_individual"].ToString();
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
                    Asegurado.ALDEA_LOCALIDAD_ZONA = cliente["zona_direccion_empresa"].ToString();
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
            //Certificado.NOMBRE_EJECUTIVO = usuarioLogueado.Rows[0]["correo_electronico"].ToString();
            Certificado.NOMBRE_EJECUTIVO = informacionUsuario.correoElectronico;
            Certificado.DIRECC = datosBien.Rows[0]["direccion_bien"].ToString();
            Certificado.PAIS_RIESGO = datosBien.Rows[0]["pais_direccion"].ToString();
            Certificado.DEPARTAMENTO_RIESGO = datosBien.Rows[0]["depto_direccion"].ToString();
            Certificado.MUNICIPIO_RIESGO = datosBien.Rows[0]["muni_direccion"].ToString();
            Certificado.ALDEA_LOCALIDAD_ZONA_RIESGO = "000";
            Certificado.CODINTER = informacionUsuario.codIntermediario;
            //Certificado.CODINTER = usuarioLogueado.Rows[0]["codigo_intermediario"].ToString();

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
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Principal/Menu_Principal.aspx");
        }
        private void crearVariablesSession(int idCotizacion, int idPlan)
        {
            Session["cotId"] = idCotizacion;
            Session["idPlan"] = idPlan;
        }
    }
}