using CrystalDecisions.CrystalReports.Engine;
using Lbl_Cotizado_Autos_Web.Acceso;
using Lbl_Cotizado_Autos_Web.Clientes;
using Lbl_Cotizado_Autos_Web.Comunes;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Inserts;
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

namespace CotizadorWebAutos.HogarSeguro
{
    public partial class Cotizar1 : System.Web.UI.Page
    {        
        int idCotizacion = 0;
        int idPlan = 0;
        DataTable datosPais;
        DataTable datosDepartamentos;
        DataTable datosMunicipios;
        DataTable datosZonas;
        DataTable formasPagoPlan = new DataTable();
        DataTable informacionPlanSeleccionado = new DataTable();
        ConsultasBD clConsultasBD = new ConsultasBD();
        DataTable numeroPagosPlan = new DataTable();
        Varias clVarias = new Varias();
        string cCodprod = string.Empty;
        string cCodplan = string.Empty;
        string cRevplan = string.Empty;
        IngresoSistema.informacionUsuario informacionUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();
                AccesoUsuario clAccesoUsuario = new AccesoUsuario();

                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];
            }

            if (informacionUsuario.idUsuario == 0)
            {
                btnGuardarCotizacion.Visible = false;
                btnRegresar.Visible = false;                
            }

            if (!IsPostBack)
            {
                llenarCombosGeograficos();
            }
        }
        private void llenarCombosGeograficos()
        {
            datosPais = new DataTable();
            datosDepartamentos = new DataTable();
            datosMunicipios = new DataTable();
            datosZonas = new DataTable();
            Consultas clVarios = new Consultas();

            datosPais = clVarios.obtenerPaises();
            datosDepartamentos = clVarios.obtenerDepartamentos();
            datosMunicipios = clVarios.obtenerMunicipios();
            datosZonas = clVarios.obtenerZonas();

            ViewState.Add("datosPais", datosPais);
            ViewState.Add("datosDepartamentos", datosDepartamentos);
            ViewState.Add("datosMunicipios", datosMunicipios);
            ViewState.Add("datosZonas", datosZonas);

            //PAISES
            cmbPais.DataSource = datosPais;
            cmbPais.DataTextField = "DESCPAIS";
            cmbPais.DataValueField = "CODPAIS";
            cmbPais.DataBind();

            cmbPais.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbPais.SelectedValue = "001";

            cmbPais_SelectedIndexChanged(cmbPais, new EventArgs());

            //DEPARTAMENTOS
            cmbDepartamento.DataSource = datosDepartamentos;
            cmbDepartamento.DataTextField = "DESCESTADO";
            cmbDepartamento.DataValueField = "CODESTADO";
            cmbDepartamento.DataBind();

            cmbDepartamento_SelectedIndexChanged(cmbDepartamento, new EventArgs());

            cmbDepartamento.Items.Insert(0, new ListItem(String.Empty, String.Empty));

            //MUNICIPIOS
            cmbMunicipio.DataSource = datosMunicipios;
            cmbMunicipio.DataTextField = "DESCCIUDAD";
            cmbMunicipio.DataValueField = "CODCIUDAD";
            cmbMunicipio.DataBind();


            cmbMunicipio_SelectedIndexChanged(cmbMunicipio, new EventArgs());

            cmbZona.Items.Insert(0, new ListItem(String.Empty, String.Empty));

            //ZONA
            cmbZona.DataSource = datosZonas;
            cmbZona.DataTextField = "DESCMUNICIPIO";
            cmbZona.DataValueField = "CODMUNICIPIO";
            cmbZona.DataBind();

        }
        protected void btnMostrarPlanes_Click(object sender, EventArgs e)
        {
            if (txtCorreoCliente.Text == string.Empty || txtNombreCliente.Text == string.Empty || txtTelefonoCliente.Text == string.Empty)
            {
                mostrarMensaje("Debe ingresar los datos personales para poder continuar.");
                return;
            }

            if (txtTelefonoCliente.Text.Length < 8)
            {
                mostrarMensaje("Debe ingresar un número teléfonico valido.");
                return;
            }

            if (rbtnTipoBien.SelectedValue == string.Empty)
            {
                mostrarMensaje("Seleccione si es inquilino o propietario.");
                return;
            }

            if (txtDireccionVivienda.Text == string.Empty)
            {
                mostrarMensaje("Ingrese la dirección del bien.");
                return;
            }

            if (rbtnTipoBien.SelectedValue == "Inquilino")
            {
                pnlPlanInquilino.Visible = true;
                pnlPlanPropietario.Visible = false;
            }
            else
            {
                pnlPlanPropietario.Visible = true;
                pnlPlanInquilino.Visible = false;
            }
        }
        private void mostrarMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", "alert('" + mensaje + "');", true);
        }        
        protected void btnRegresarCotizacion_Click(object sender, EventArgs e)
        {
            //Response.Redirect("../Hogar_Seguro/Cotizaciones.aspx?codagent=" + codAgente);
        }
        protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            numeroPagosXPlan();
        }
        protected void ddlFormaPagoExterno_SelectedIndexChanged(object sender, EventArgs e)
        {
            numeroPagosXPlan();
        }
        private void numeroPagosXPlan()
        {
            if (rdPropietarioBronce.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO01");
            }

            if (rdPropietarioPlata.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO02");
            }

            if (rdPropietarioOro.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO03");
            }

            if (rdInquilinoBronce.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO04");
            }

            if (rdInquilinoPlata.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO05");
            }

            if (rdInquilinoOro.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO06");
            }

            cCodprod = informacionPlanSeleccionado.Rows[0]["codprod"].ToString();
            cCodplan = informacionPlanSeleccionado.Rows[0]["codplan"].ToString();
            cRevplan = informacionPlanSeleccionado.Rows[0]["revplan"].ToString();

            

            if (informacionUsuario.idUsuario == 0)
            {
                numeroPagosPlan = clConsultasBD.obtenerNumeroPagosXPlan(cCodprod, cCodplan, cRevplan, ddlFormaPagoExterno.SelectedValue.Substring(0,1));
                ddlNumeroPagosExterno.DataSource = numeroPagosPlan;
                ddlNumeroPagosExterno.DataValueField = "CODPLANFINAN";
                ddlNumeroPagosExterno.DataTextField = "NOMPLAN";
                ddlNumeroPagosExterno.DataBind();
            }
            else
            {
                numeroPagosPlan = clConsultasBD.obtenerNumeroPagosXPlan(cCodprod, cCodplan, cRevplan, ddlFormaPago.SelectedValue.Substring(0,1));
                ddlNumeroPagos.DataSource = numeroPagosPlan;
                ddlNumeroPagos.DataValueField = "CODPLANFINAN";
                ddlNumeroPagos.DataTextField = "NOMPLAN";
                ddlNumeroPagos.DataBind();
            }
            
        }

        #region SeleccionPlanes
        protected void rdInquilinoBronce_CheckedChanged(object sender, EventArgs e)
        {
            if (rdInquilinoBronce.Checked)
            {
                rdPropietarioPlata.Checked = false;
                rdPropietarioOro.Checked = false;
                rdInquilinoPlata.Checked = false;
                rdInquilinoOro.Checked = false;

                if (informacionUsuario.idUsuario == 0)
                {
                    pnlDetalleCotExterna.Visible = true;
                    upnlDetalleCotExterna.Update();
                }
                else
                {
                    pnlPagos.Visible = true;
                    upnlPagos.Update();
                }
                
                btnCotizar.Focus();

                llenarCombos("HOGARTOT-1", "COMBO04");
            }
        }
        protected void rdInquilinoPlata_CheckedChanged(object sender, EventArgs e)
        {
            if (rdInquilinoPlata.Checked)
            {
                rdPropietarioBronce.Checked = false;
                rdPropietarioOro.Checked = false;
                rdInquilinoBronce.Checked = false;
                rdInquilinoOro.Checked = false;

                if (informacionUsuario.idUsuario == 0)
                {
                    pnlDetalleCotExterna.Visible = true;
                    upnlDetalleCotExterna.Update();
                }
                else
                {
                    pnlPagos.Visible = true;
                    upnlPagos.Update();
                }

                btnCotizar.Focus();

                llenarCombos("HOGARTOT-1", "COMBO05");
            }
        }
        protected void rdInquilinoOro_CheckedChanged(object sender, EventArgs e)
        {
            if (rdInquilinoOro.Checked)
            {
                rdPropietarioBronce.Checked = false;
                rdPropietarioPlata.Checked = false;
                rdInquilinoBronce.Checked = false;
                rdInquilinoPlata.Checked = false;

                if (informacionUsuario.idUsuario == 0)
                {
                    pnlDetalleCotExterna.Visible = true;
                    upnlDetalleCotExterna.Update();
                }
                else
                {
                    pnlPagos.Visible = true;
                    upnlPagos.Update();
                }

                btnCotizar.Focus();

                llenarCombos("HOGARTOT-1", "COMBO06");
            }
        }
        protected void rdPropietarioBronce_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPropietarioBronce.Checked)
            {
                rdPropietarioPlata.Checked = false;
                rdPropietarioOro.Checked = false;
                rdInquilinoPlata.Checked = false;
                rdInquilinoOro.Checked = false;

                if (informacionUsuario.idUsuario == 0)
                {
                    pnlDetalleCotExterna.Visible = true;
                    upnlDetalleCotExterna.Update();
                }
                else
                {
                    pnlPagos.Visible = true;
                    upnlPagos.Update();
                }

                btnCotizar.Focus();

                llenarCombos("HOGARTOT-1", "COMBO01");
            }
        }
        protected void rdPropietarioPlata_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPropietarioPlata.Checked)
            {
                rdPropietarioBronce.Checked = false;
                rdPropietarioOro.Checked = false;
                rdInquilinoBronce.Checked = false;
                rdInquilinoOro.Checked = false;

                if (informacionUsuario.idUsuario == 0)
                {
                    pnlDetalleCotExterna.Visible = true;
                    upnlDetalleCotExterna.Update();
                    
                }
                else
                {
                    pnlPagos.Visible = true;
                    upnlPagos.Update();
                    btnCotizar.Focus();
                }

               

                llenarCombos("HOGARTOT-1", "COMBO02");
            }
        }
        protected void rdPropietarioOro_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPropietarioOro.Checked)
            {

                rdPropietarioBronce.Checked = false;
                rdPropietarioPlata.Checked = false;
                rdInquilinoBronce.Checked = false;
                rdInquilinoPlata.Checked = false;

                if (informacionUsuario.idUsuario == 0)
                {
                    pnlDetalleCotExterna.Visible = true;
                    upnlDetalleCotExterna.Update();
                }
                else
                {
                    pnlPagos.Visible = true;
                    upnlPagos.Update();
                }

                btnCotizar.Focus();

                llenarCombos("HOGARTOT-1", "COMBO03");
            }
        }

        #endregion

        private void llenarCombos(string id_plan_pol, string plan_pol)
        {
            informacionPlanSeleccionado = clVarias.obtenerInformacionPlan(id_plan_pol, plan_pol);

            cCodprod = informacionPlanSeleccionado.Rows[0]["codprod"].ToString();
            cCodplan = informacionPlanSeleccionado.Rows[0]["codplan"].ToString();
            cRevplan = informacionPlanSeleccionado.Rows[0]["revplan"].ToString();

            formasPagoPlan = clConsultasBD.ObtenerInformacionFormaPago(cCodprod, cCodplan, cRevplan);

            if (informacionUsuario.idUsuario == 0)
            {
                ddlFormaPagoExterno.DataSource = formasPagoPlan;
                ddlFormaPagoExterno.DataValueField = "CODIGO";
                ddlFormaPagoExterno.DataTextField = "DESCRIP";
                ddlFormaPagoExterno.DataBind();
                ddlFormaPagoExterno_SelectedIndexChanged(ddlFormaPagoExterno, new EventArgs());
            }
            else
            {
                ddlFormaPago.DataSource = formasPagoPlan;
                ddlFormaPago.DataValueField = "CODIGO";
                ddlFormaPago.DataTextField = "DESCRIP";
                ddlFormaPago.DataBind();
                ddlFormaPago_SelectedIndexChanged(ddlFormaPago, new EventArgs());
            }
            
            //numeroPagosXPlan();
        }
        private void Cotizar(string id_plan_pol, string plan_pol)
        {
            ConsultasBD clEmitirPoliza = new ConsultasBD();           

            //Primero se debe obtener el IDEPROCESO DEL PAQUETE 
            int IDEPROCESO = clEmitirPoliza.GET_IDPROCESO("ACSEL_WEB");

            //Se crea la estructura del asegurado para generar el XML
            List<CA_ASEGURADO> listadoAsegurados = new List<CA_ASEGURADO>();
            CA_ASEGURADO Asegurado = new CA_ASEGURADO();

            //Se crea la estructura del certificado para generar el XML
            List<CA_CERTIFICADO> listadoCertificados = new List<CA_CERTIFICADO>();
            CA_CERTIFICADO Certificado = new CA_CERTIFICADO();

            Asegurado.USUARIO = informacionUsuario.codigoUsuarioRemoto;

            Asegurado.IDEPROCESO = IDEPROCESO.ToString();
            Asegurado.LLAVE = "1";
            Asegurado.LLAVE_ALTERNA = "1";
            Asegurado.CONTRATANTE = "S";
            Asegurado.ASEGURADO = "S";
            Asegurado.RESP_PAGO = "S";
            Asegurado.BENEFICIARIO = "N";
            Asegurado.DEPENDIENTE = "N";
            Asegurado.NOMBRE = "MAPFRE | SEGUROS GUATEMALA, S.A.";
            Asegurado.GENERO = "M";
            Asegurado.FECHANAC = "01/01/1980";
            Asegurado.NACIONAL_EXTRANJERO = "N";
            Asegurado.PAIS_NACIMIENTO = "001";
            Asegurado.DEPARTAMENTO_NACIMIENTO = "001";
            Asegurado.MUNICIPIO_NACIMIENTO = "001";
            Asegurado.PAIS_CEDULA = "001";
            Asegurado.DEPARTAMENTO_CEDULA = "001";
            Asegurado.MUNICIPIO_CEDULA = "001";
            Asegurado.NIT = "846611-4";
            Asegurado.TIPO_CLIENTE = "E";
            Asegurado.ESTADO_CIVIL = "S";
            Asegurado.ACTIVIDAD_ECONOMICA = "879";
            Asegurado.PAIS_DIRECCION = "001";
            Asegurado.DEPARTAMENTO_DIRECCION = "001";
            Asegurado.MUNICIPIO_DIRECCION = "001";
            Asegurado.ALDEA_LOCALIDAD_ZONA = "000";
            Asegurado.DIRECCION = "GUATEMALA";
            Asegurado.CORREO_ELECTRONICO = txtCorreoCliente.Text;
            Asegurado.LINEA = "1";
            Asegurado.LAYOUT = "0";
            Asegurado.STSCA = "INC";
            Asegurado.DPI = "";
            Asegurado.PORCENTAJE = "100";
            Asegurado.TELEFONO1 = txtTelefonoCliente.Text;
            Asegurado.IDBASE = idCotizacion.ToString();

            //Se agrega el asegurado a la lista de asegurados
            listadoAsegurados.Add(Asegurado);

            Certificado.IDEPROCESO = IDEPROCESO.ToString();
            Certificado.LLAVE = "1";
            Certificado.LLAVE_ALTERNA = "1";
            Certificado.IDEPLANPOL = id_plan_pol;
            Certificado.PLANPOL = plan_pol;
            Certificado.VIGENCIA_INICIAL = DateTime.Today.ToString("dd/MM/yyyy");
            Certificado.VIGENCIA_FINAL = DateTime.Today.AddDays(365).ToString("dd/MM/yyyy");
            Certificado.FECHA_INICIO_COBRO = DateTime.Today.ToString("dd/MM/yyyy");
            Certificado.PRIMER_PAGO_REALIZADO = "N";
            Certificado.FORMA_PAGO = "A";
            Certificado.PAGOS = "12";
            Certificado.SUMAASEGURADA = "";
            Certificado.MONTO_ASEGURADO = "0";
            Certificado.LINEA = "1";
            Certificado.LAYOUT = "0";
            Certificado.STSCA = "INC";
            Certificado.TIPO_OPERACION = "EMI";
            Certificado.DIRECC = txtDireccionVivienda.Text;
            Certificado.PAIS_RIESGO = cmbPais.SelectedValue;
            Certificado.DEPARTAMENTO_RIESGO = cmbDepartamento.SelectedValue;
            Certificado.MUNICIPIO_RIESGO = cmbMunicipio.SelectedValue;
            Certificado.ALDEA_LOCALIDAD_ZONA_RIESGO = cmbZona.SelectedValue;
            Certificado.CODINTER = informacionUsuario.codIntermediario;
                
            //Se agrega el certificado al listado de certificados
            listadoCertificados.Add(Certificado);

            //Estas listas se agregan vacias porque el plan no tiene recargos ni coberturas adicionales
            List<CA_COBERTURA> listaCoberturas = new List<CA_COBERTURA>();
            List<CA_REGARGO> listaRecargos = new List<CA_REGARGO>();

            //Luego se generan los xml con los datos de los layouts
            string xmlAsegurado = clEmitirPoliza.generarXML_CA_ASEGURADO(listadoAsegurados);
            string xmlCertificado = clEmitirPoliza.generarXML_CA_CERTIFICADO(listadoCertificados, listaCoberturas, listaRecargos);
            
            string xmlBien = string.Empty;
            //Se ejecuta el proceso de carga de lote que lleva como parametros los xml generados anteriormente
            string respuesta = clEmitirPoliza.CARGA_LOTE(Convert.ToInt32(IDEPROCESO), "", xmlCertificado, xmlAsegurado, xmlBien);

            string resultadoPoliza = string.Empty;

            if (respuesta.Contains("satisfactoriamente"))
            {
                bool polizaActiva = false;

                //Se ejecuta el proceso PR_CARGA_AUTOMATICA.CARGA_ARCHXML 
                clEmitirPoliza.CARGA_ARCHXML(Convert.ToInt32(IDEPROCESO));

                //Se ejecuta el proceso PR_CARGA_AUTOMATICA.CREAR y se valida que no exista error en la tabla CA_Bitacora
                resultadoPoliza = clEmitirPoliza.CREAR(Convert.ToInt32(IDEPROCESO));

                if (resultadoPoliza == string.Empty)
                {
                    DataTable idepol = new DataTable();

                    //Se obtiene el IDEPOL de la tabla CA_CERTIFICADO
                    idepol = clEmitirPoliza.ObtenerIdepol(IDEPROCESO.ToString());

                    if (idepol.Rows.Count == 1)
                    {
                        //Se ejecuta el proceso PR_POLIZA.ACTIVAR para activar la poliza de cotización
                        polizaActiva = clConsultasBD.ActivarPolizaCotizacion(int.Parse(idepol.Rows[0]["IDEPOL"].ToString()));

                        if (polizaActiva)
                        {
                            //Se genera el plan de financiamiento con el proceso PR_COND_FINANCIAMIENTO_T.GENERAR
                            if (clConsultasBD.GenerarCondFinanciamientoTCotizacion(int.Parse(idepol.Rows[0]["IDEPOL"].ToString())))
                            {
                                ViewState.Add("IDEPROCESO", IDEPROCESO);
                                ViewState.Add("IDEPOL", idepol.Rows[0]["IDEPOL"].ToString());

                                DataTable montosVisaCuotas = new DataTable();
                                DataTable montosFraccionados = new DataTable();

                                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan(id_plan_pol, plan_pol);

                                cCodprod = informacionPlanSeleccionado.Rows[0]["codprod"].ToString();
                                cCodplan = informacionPlanSeleccionado.Rows[0]["codplan"].ToString();
                                cRevplan = informacionPlanSeleccionado.Rows[0]["revplan"].ToString();

                                //Se obtiene el plan de pagos para visacuotas del plan seleccionado
                                montosVisaCuotas = clEmitirPoliza.obtenerMontoPrimaGenerales(cCodprod, cCodplan, cRevplan,
                                                                                            "S", int.Parse(idepol.Rows[0]["IDEPOL"].ToString()),
                                                                                            ddlNumeroPagos.SelectedValue, "Q");

                                //Se obtiene el plan de pagos fraccionado del plan seleccionado
                                montosFraccionados = clEmitirPoliza.obtenerMontoPrimaGenerales(cCodprod, cCodplan, cRevplan,
                                                                                            "N", int.Parse(idepol.Rows[0]["IDEPOL"].ToString()),
                                                                                            ddlNumeroPagos.SelectedValue, "Q");

                                if (montosFraccionados.Rows.Count > 0)
                                {
                                    lblResumenPagosFraccionados.Visible = true;

                                    foreach (DataRow row in montosFraccionados.Rows)
                                    {
                                        switch (row["CUOTAS"].ToString())
                                        {
                                            case "1":
                                                ViewState.Add("PAGOCONTADO", row["Monto"].ToString());
                                                break;
                                            case "2":
                                                ViewState.Add("PAGODOSFRAC", row["Monto"].ToString());
                                                break;
                                            case "4":
                                                ViewState.Add("PAGOCUATROFRAC", row["Monto"].ToString());
                                                break;
                                            case "6":
                                                ViewState.Add("PAGOSEIFRACS", row["Monto"].ToString());
                                                break;
                                            case "8":
                                                ViewState.Add("PAGOOCHOFRAC", row["Monto"].ToString());
                                                break;
                                            case "10":
                                                ViewState.Add("PAGODIEZFRAC", row["Monto"].ToString());
                                                break;
                                            case "12":
                                                ViewState.Add("PAGODOCEFRAC", row["Monto"].ToString());
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    lblResumenPagosFraccionados.Visible = false;

                                    ViewState.Add("PAGOCONTADO", "0");

                                    ViewState.Add("PAGODOSFRAC", "0");

                                    ViewState.Add("PAGOCUATROFRAC", "0");

                                    ViewState.Add("PAGOSEIFRACS", "0");

                                    ViewState.Add("PAGOOCHOFRAC", "0");

                                    ViewState.Add("PAGODIEZFRAC", "0");

                                    ViewState.Add("PAGODOCEFRAC", "0");
                                }

                                if (montosVisaCuotas.Rows.Count > 0)
                                {
                                    lblResumenPagosVisaCuotas.Visible = true;

                                    ViewState.Add("PAGODOSVISC", "0");

                                    ViewState.Add("PAGOTRESVISC", montosVisaCuotas.Rows[0]["Monto"].ToString());

                                    ViewState.Add("PAGOSEIVISC", montosVisaCuotas.Rows[1]["Monto"].ToString());

                                    ViewState.Add("PAGODIEZVISC", montosVisaCuotas.Rows[2]["Monto"].ToString());

                                    ViewState.Add("PAGODOCEVISC", montosVisaCuotas.Rows[3]["Monto"].ToString());
                                }
                                else
                                {
                                    lblResumenPagosVisaCuotas.Visible = false;

                                    ViewState.Add("PAGODOSVISC", "0");

                                    ViewState.Add("PAGOTRESVISC", "0");

                                    ViewState.Add("PAGOSEIVISC", "0");

                                    ViewState.Add("PAGODIEZVISC", "0");

                                    ViewState.Add("PAGODOCEVISC", "0");
                                }

                                if (informacionUsuario.idUsuario == 0)
                                {
                                    gvInformacionPlanesCuotasExterno.DataSource = montosVisaCuotas;
                                    gvInformacionPlanesCuotasExterno.DataBind();

                                    gvInformacionPlanesFraccionadoExterno.DataSource = montosFraccionados;
                                    gvInformacionPlanesFraccionadoExterno.DataBind();

                                    guardarCotizacionExterna();
                                }
                                else
                                {
                                    gvInformacionPlanesCuotas.DataSource = montosVisaCuotas;
                                    gvInformacionPlanesCuotas.DataBind();

                                    gvInformacionPlanesFraccionado.DataSource = montosFraccionados;
                                    gvInformacionPlanesFraccionado.DataBind();

                                    btnGuardarCotizacion.Visible = true;
                                    btnGuardarCotizacion.Focus();
                                }
                            }
                            else
                            {
                                resultadoPoliza = "Error al generar financiamiento. IDEPROCESO: " + IDEPROCESO;
                            }
                        }
                        else
                        {
                            resultadoPoliza = "Error al activar la Póliza. IDEPROCESO: " + IDEPROCESO;
                        }
                    }
                    else
                    {
                        resultadoPoliza = "Error al consultar Póliza. IDEPROCESO: " + IDEPROCESO;
                    }
                }
                else
                {
                    resultadoPoliza = "Error al cotizar la Póliza. IDEPROCESO: " + IDEPROCESO + " " + resultadoPoliza;

                }
            }
            else
            {
                resultadoPoliza = "Error al cargar certificado y asegurado. IDEPROCESO: " + IDEPROCESO;
            }

            if (resultadoPoliza != string.Empty)
            {
                lblError.Text = resultadoPoliza;
                lblError.Visible = true;
                lblError.Focus();
            }
        }        
        protected void cmbPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosDepartamentos = ViewState["datosDepartamentos"] as DataTable;
            datosPais = ViewState["datosPais"] as DataTable;

            DataRow[] departamentos = datosDepartamentos.Select("CODPAIS = " + cmbPais.SelectedValue);

            if (departamentos.Count() > 1)
            {
                datosDepartamentos = departamentos.CopyToDataTable();

                cmbDepartamento.Visible = true;
                cmbMunicipio.Visible = true;
                cmbDepartamento.DataSource = datosDepartamentos;
                cmbDepartamento.DataTextField = "DESCESTADO";
                cmbDepartamento.DataValueField = "CODESTADO";
                cmbDepartamento.DataBind();

                cmbDepartamento_SelectedIndexChanged(cmbDepartamento, new EventArgs());
            }
            else
            {
                cmbDepartamento.Visible = false;
                cmbMunicipio.Visible = false;
            }
        }
        protected void cmbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosMunicipios = ViewState["datosMunicipios"] as DataTable;

            DataRow[] municipios = datosMunicipios.Select("CODPAIS = " + cmbPais.SelectedValue + "AND CODESTADO = '" + cmbDepartamento.SelectedValue + "'");

            if (municipios.Count() > 1)
            {
                datosMunicipios = municipios.CopyToDataTable();

                cmbMunicipio.Visible = true;
                cmbMunicipio.DataSource = datosMunicipios;
                cmbMunicipio.DataTextField = "DESCCIUDAD";
                cmbMunicipio.DataValueField = "CODCIUDAD";
                cmbMunicipio.DataBind();

                cmbMunicipio_SelectedIndexChanged(cmbMunicipio, new EventArgs());
            }
            else
            {
                cmbMunicipio.Visible = false;
            }
        }
        protected void cmbMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosZonas = ViewState["datosZonas"] as DataTable;

            DataRow[] zonas = datosZonas.Select("CODPAIS = " + cmbPais.SelectedValue + "AND CODESTADO = '" + cmbDepartamento.SelectedValue + "'" + "AND CODCIUDAD = '" + cmbMunicipio.SelectedValue + "'");

            if (zonas.Count() > 1)
            {
                datosZonas = zonas.CopyToDataTable();

                cmbZona.DataSource = null;
                cmbZona.Visible = true;
                cmbZona.DataSource = datosZonas;
                cmbZona.DataTextField = "DESCMUNICIPIO";
                cmbZona.DataValueField = "CODMUNICIPIO";
                cmbZona.DataBind();
            }
            else
            {
                cmbZona.Visible = false;
            }
        }      
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            DataTable PlanHogarSeguro = new DataTable();
            PlanHogarSeguro = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO00");
            Response.Redirect("../HogarSeguro/Cotizaciones.aspx?userId=" + informacionUsuario.idUsuario + "&plan=" + PlanHogarSeguro.Rows[0]["id_plan_web"]);
        }
        protected void btnCotizar_Click(object sender, EventArgs e)
        {
            if (rdPropietarioBronce.Checked)
            {
                Cotizar("HOGARTOT-1", "COMBO01");
            }

            if (rdPropietarioPlata.Checked)
            {
                Cotizar("HOGARTOT-1", "COMBO02");
            }

            if (rdPropietarioOro.Checked)
            {
                Cotizar("HOGARTOT-1", "COMBO03");
            }

            if (rdInquilinoBronce.Checked)
            {
                Cotizar("HOGARTOT-1", "COMBO04");
            }

            if (rdInquilinoPlata.Checked)
            {
                Cotizar("HOGARTOT-1", "COMBO05");
            }

            if (rdInquilinoOro.Checked)
            {
                Cotizar("HOGARTOT-1", "COMBO06");
            }

            //btnGuardarCotizacion.Visible = true;
        }
        protected void btnGuardarCotizacion_Click(object sender, EventArgs e)
        {
            guardarCotizacionInterna();
        }
        private void guardarCotizacionInterna()
        {
            InsertsBD objetoInserts = new InsertsBD();
            Proceso_Cotizacion clProcesoCotizacion = new Proceso_Cotizacion();
            double montoTotalCancelar = 0;
            double valorDeducible = 0.00;
            double valorFinalEquipoEspecial = 0;
            string codigoAlarma = string.Empty;
            string descripAlarma = string.Empty;

            if (ViewState["PAGODOSFRAC"] == null)
            {
                ViewState.Add("PAGODOSFRAC", "0");
            }

            if (ViewState["PAGOCUATROFRAC"] == null)
            {
                ViewState.Add("PAGOCUATROFRAC", "0");
            }

            if (ViewState["PAGOSEIFRACS"] == null)
            {
                ViewState.Add("PAGOSEIFRACS", "0");
            }

            if (ViewState["PAGOOCHOFRAC"] == null)
            {
                ViewState.Add("PAGOOCHOFRAC", "0");
            }

            if (ViewState["PAGODIEZFRAC"] == null)
            {
                ViewState.Add("PAGODIEZFRAC", "0");
            }

            if (ViewState["PAGODOCEFRAC"] == null)
            {
                ViewState.Add("PAGODOCEFRAC", "0");
            }

            if (ddlFormaPago.SelectedItem.Text == "CREDI-CUOTAS" || ddlFormaPago.SelectedItem.Text == "VISA-CUOTAS")
            {
                if (ddlNumeroPagos.SelectedItem.Text.Contains("2"))
                {
                    montoTotalCancelar = Convert.ToDouble(ViewState["PAGODOSVISC"].ToString());
                }

                if (ddlNumeroPagos.SelectedItem.Text.Contains("3"))
                {
                    montoTotalCancelar = Convert.ToDouble(ViewState["PAGOTRESVISC"].ToString());
                }

                if (ddlNumeroPagos.SelectedItem.Text.Contains("6"))
                {
                    montoTotalCancelar = Convert.ToDouble(ViewState["PAGOSEIVISC"].ToString());
                }

                if (ddlNumeroPagos.SelectedItem.Text.Contains("10"))
                {
                    montoTotalCancelar = Convert.ToDouble(ViewState["PAGODIEZVISC"].ToString());
                }

                if (ddlNumeroPagos.SelectedItem.Text.Contains("12"))
                {
                    montoTotalCancelar = Convert.ToDouble(ViewState["PAGODOCEVISC"].ToString());
                }
            }

            if (ddlFormaPago.SelectedItem.Text == "FRACCIONADO")
            {
                switch (ddlNumeroPagos.SelectedItem.Text)
                {
                    case "CONTADO":
                        montoTotalCancelar = Convert.ToDouble(ViewState["PAGOCONTADO"].ToString());
                        break;
                    case "2 pagos":
                        montoTotalCancelar = Convert.ToDouble(ViewState["PAGODOSFRAC"].ToString());
                        break;
                    case "4 pagos":
                        montoTotalCancelar = Convert.ToDouble(ViewState["PAGOCUATROFRAC"].ToString());
                        break;
                    case "6 pagos":
                        montoTotalCancelar = Convert.ToDouble(ViewState["PAGOSEIFRACS"].ToString());
                        break;
                    case "8 pagos":
                        montoTotalCancelar = Convert.ToDouble(ViewState["PAGOOCHOFRAC"].ToString());
                        break;
                    case "10 pagos":
                        montoTotalCancelar = Convert.ToDouble(ViewState["PAGODIEZFRAC"].ToString());
                        break;
                    case "12 pagos":
                        montoTotalCancelar = Convert.ToDouble(ViewState["PAGODOCEFRAC"].ToString());
                        break;
                    default:
                        break;
                }
            }

            string modPlan = obtenerModPlan();

            idCotizacion = clProcesoCotizacion.guardarCotizacionAutos(txtNombreCliente.Text,
                    txtCorreoCliente.Text, txtTelefonoCliente.Text, Convert.ToDouble(0), "001",
                    "001", "001", "001", "001",
                    "001", "02", "QUEMADO", "0000", Convert.ToInt32(0), 1,
                    DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Today.ToString("yyyy-MM-dd"),
                    DateTime.Today.AddDays(365).ToString("yyyy-MM-dd"),
                    DateTime.Today.ToString("yyyy-MM-dd"),
                    informacionUsuario.idUsuario.ToString(), idPlan.ToString(),
                    ViewState["IDEPROCESO"].ToString(),
                    ViewState["IDEPOL"].ToString(),
                    Convert.ToInt32(ddlNumeroPagos.SelectedValue.Substring(4, 2)),
                    montoTotalCancelar,
                    Convert.ToDouble(ViewState["PAGODOSFRAC"].ToString()),
                    Convert.ToDouble(ViewState["PAGOCUATROFRAC"].ToString()),
                    Convert.ToDouble(ViewState["PAGOSEIFRACS"].ToString()),
                    Convert.ToDouble(ViewState["PAGOOCHOFRAC"].ToString()),
                    Convert.ToDouble(ViewState["PAGODIEZFRAC"].ToString()),
                    Convert.ToDouble(ViewState["PAGODOCEFRAC"].ToString()),
                    Convert.ToDouble(ViewState["PAGOTRESVISC"].ToString()),
                    Convert.ToDouble(ViewState["PAGOSEIVISC"].ToString()),
                    Convert.ToDouble(ViewState["PAGODIEZVISC"].ToString()),
                    Convert.ToDouble(ViewState["PAGODOCEVISC"].ToString()),
                    valorDeducible,
                    Convert.ToDouble(0),
                    Convert.ToDouble(0),
                    0.00,
                    Convert.ToDouble(0),
                    Convert.ToDouble(0),
                    0.00,
                    0.00,
                    valorFinalEquipoEspecial,
                    codigoAlarma,
                    descripAlarma,
                    ddlNumeroPagos.SelectedValue,
                    modPlan,
                    Convert.ToDouble(ViewState["PAGOCONTADO"].ToString()));

            Proceso_Emision_Hogar_Seguro clEmision = new Proceso_Emision_Hogar_Seguro();

            clEmision.guardarDatosDelBien(idCotizacion, txtDireccionVivienda.Text, cmbPais.SelectedValue, cmbPais.SelectedItem.Text, cmbDepartamento.SelectedValue, cmbDepartamento.SelectedItem.Text, cmbMunicipio.SelectedValue, cmbMunicipio.SelectedItem.Text);
            
            mostrarMensajeRedirect("La cotización ha sido guardada.");
            btnGuardarCotizacion.Enabled = false;                      
        }
        private void guardarCotizacionExterna()
        {
            InsertsBD objetoInserts = new InsertsBD();
            Proceso_Cotizacion clProcesoCotizacion = new Proceso_Cotizacion();
            double montoTotalCancelar = 0;
            double valorDeducible = 0.00;
            double valorFinalEquipoEspecial = 0;
            string codigoAlarma = string.Empty;
            string descripAlarma = string.Empty;

            if (ViewState["PAGODOSFRAC"] == null)
            {
                ViewState.Add("PAGODOSFRAC", "0");
            }

            if (ViewState["PAGOCUATROFRAC"] == null)
            {
                ViewState.Add("PAGOCUATROFRAC", "0");
            }

            if (ViewState["PAGOSEIFRACS"] == null)
            {
                ViewState.Add("PAGOSEIFRACS", "0");
            }

            if (ViewState["PAGOOCHOFRAC"] == null)
            {
                ViewState.Add("PAGOOCHOFRAC", "0");
            }

            if (ViewState["PAGODIEZFRAC"] == null)
            {
                ViewState.Add("PAGODIEZFRAC", "0");
            }

            if (ViewState["PAGODOCEFRAC"] == null)
            {
                ViewState.Add("PAGODOCEFRAC", "0");
            }

            if (ddlFormaPagoExterno.SelectedItem.Text == "CREDI-CUOTAS" || ddlFormaPagoExterno.SelectedItem.Text == "VISA-CUOTAS")
            {
                if (ddlNumeroPagosExterno.SelectedItem.Text.Contains("2"))
                {
                    montoTotalCancelar = Convert.ToDouble(ViewState["PAGODOSVISC"].ToString());
                }

                if (ddlNumeroPagosExterno.SelectedItem.Text.Contains("3"))
                {
                    montoTotalCancelar = Convert.ToDouble(ViewState["PAGOTRESVISC"].ToString());
                }

                if (ddlNumeroPagosExterno.SelectedItem.Text.Contains("6"))
                {
                    montoTotalCancelar = Convert.ToDouble(ViewState["PAGOSEIVISC"].ToString());
                }

                if (ddlNumeroPagosExterno.SelectedItem.Text.Contains("10"))
                {
                    montoTotalCancelar = Convert.ToDouble(ViewState["PAGODIEZVISC"].ToString());
                }

                if (ddlNumeroPagosExterno.SelectedItem.Text.Contains("12"))
                {
                    montoTotalCancelar = Convert.ToDouble(ViewState["PAGODOCEVISC"].ToString());
                }
            }

            if (ddlFormaPagoExterno.SelectedItem.Text == "FRACCIONADO")
            {
                switch (ddlNumeroPagosExterno.SelectedItem.Text)
                {
                    case "CONTADO":
                        montoTotalCancelar = Convert.ToDouble(ViewState["PAGOCONTADO"].ToString());
                        break;
                    case "2 pagos":
                        montoTotalCancelar = Convert.ToDouble(ViewState["PAGODOSFRAC"].ToString());
                        break;
                    case "4 pagos":
                        montoTotalCancelar = Convert.ToDouble(ViewState["PAGOCUATROFRAC"].ToString());
                        break;
                    case "6 pagos":
                        montoTotalCancelar = Convert.ToDouble(ViewState["PAGOSEIFRACS"].ToString());
                        break;
                    case "8 pagos":
                        montoTotalCancelar = Convert.ToDouble(ViewState["PAGOOCHOFRAC"].ToString());
                        break;
                    case "10 pagos":
                        montoTotalCancelar = Convert.ToDouble(ViewState["PAGODIEZFRAC"].ToString());
                        break;
                    case "12 pagos":
                        montoTotalCancelar = Convert.ToDouble(ViewState["PAGODOCEFRAC"].ToString());
                        break;
                    default:
                        break;
                }
            }

            string modPlan = obtenerModPlan();

            idCotizacion = clProcesoCotizacion.guardarCotizacionAutos(txtNombreCliente.Text,
                    txtCorreoCliente.Text, txtTelefonoCliente.Text, Convert.ToDouble(0), "001",
                    "001", "001", "001", "001",
                    "001", "02", "QUEMADO", "0000", Convert.ToInt32(0), 1,
                    DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Today.ToString("yyyy-MM-dd"),
                    DateTime.Today.AddDays(365).ToString("yyyy-MM-dd"),
                    DateTime.Today.ToString("yyyy-MM-dd"),
                    "1", idPlan.ToString(),
                    ViewState["IDEPROCESO"].ToString(),
                    ViewState["IDEPOL"].ToString(),
                    Convert.ToInt32(ddlNumeroPagosExterno.SelectedValue.Substring(4, 2)),
                    montoTotalCancelar,
                    Convert.ToDouble(ViewState["PAGODOSFRAC"].ToString()),
                    Convert.ToDouble(ViewState["PAGOCUATROFRAC"].ToString()),
                    Convert.ToDouble(ViewState["PAGOSEIFRACS"].ToString()),
                    Convert.ToDouble(ViewState["PAGOOCHOFRAC"].ToString()),
                    Convert.ToDouble(ViewState["PAGODIEZFRAC"].ToString()),
                    Convert.ToDouble(ViewState["PAGODOCEFRAC"].ToString()),
                    Convert.ToDouble(ViewState["PAGOTRESVISC"].ToString()),
                    Convert.ToDouble(ViewState["PAGOSEIVISC"].ToString()),
                    Convert.ToDouble(ViewState["PAGODIEZVISC"].ToString()),
                    Convert.ToDouble(ViewState["PAGODOCEVISC"].ToString()),
                    valorDeducible,
                    Convert.ToDouble(0),
                    Convert.ToDouble(0),
                    0.00,
                    Convert.ToDouble(0),
                    Convert.ToDouble(0),
                    0.00,
                    0.00,
                    valorFinalEquipoEspecial,
                    codigoAlarma,
                    descripAlarma,
                    ddlNumeroPagosExterno.SelectedValue,
                    modPlan,
                    Convert.ToDouble(ViewState["PAGOCONTADO"].ToString()));

            Proceso_Emision_Hogar_Seguro clEmision = new Proceso_Emision_Hogar_Seguro();

            clEmision.guardarDatosDelBien(idCotizacion, txtDireccionVivienda.Text, cmbPais.SelectedValue, cmbPais.SelectedItem.Text, cmbDepartamento.SelectedValue, cmbDepartamento.SelectedItem.Text, cmbMunicipio.SelectedValue, cmbMunicipio.SelectedItem.Text);

            lblNumeroCotizacion.Text = idCotizacion.ToString();
            lblNumeroCotizacion.Visible = false;
            btnImprimirCotizacion.Visible = true;
        }
        private void mostrarMensajeRedirect(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + mensaje + "');", true);
            Response.Redirect("../HogarSeguro/Cotizaciones.aspx?userId=" + informacionUsuario.idUsuario + "&plan=" + idPlan);
        }
        private string obtenerModPlan()
        {
            string modPlan = string.Empty;
            DataTable resultado = new DataTable();

            if (rdPropietarioBronce.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO01");
            }

            if (rdPropietarioPlata.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO02");
            }

            if (rdPropietarioOro.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO03");
            }

            if (rdInquilinoBronce.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO04");
            }

            if (rdInquilinoPlata.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO05");
            }

            if (rdInquilinoOro.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO06");
            }            

            cCodprod = informacionPlanSeleccionado.Rows[0]["codprod"].ToString();
            cCodplan = informacionPlanSeleccionado.Rows[0]["codplan"].ToString();
            cRevplan = informacionPlanSeleccionado.Rows[0]["revplan"].ToString();
            idPlan = int.Parse(informacionPlanSeleccionado.Rows[0]["id_plan_web"].ToString());

            if (informacionUsuario.idUsuario == 0)
            {
                resultado = clConsultasBD.ObtenerMODPLAN(cCodprod, cCodplan, cRevplan, ddlFormaPagoExterno.SelectedValue.Substring(0, 1), ddlNumeroPagosExterno.SelectedValue);
            }
            else
            {
                resultado = clConsultasBD.ObtenerMODPLAN(cCodprod, cCodplan, cRevplan, ddlFormaPago.SelectedValue.Substring(0, 1), ddlNumeroPagos.SelectedValue);
                
            }            

            if (resultado.Rows.Count == 1)
            {
                modPlan = resultado.Rows[0]["MODPLAN"].ToString();
            }

            return modPlan;
        }
        protected void btnImprimirCotizacion_Click(object sender, EventArgs e)
        {  
            string correoUsuario = string.Empty;
            string idUsuarioString = informacionUsuario.idUsuario.ToString();

            if (rdPropietarioBronce.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO01");
            }

            if (rdPropietarioPlata.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO02");
            }

            if (rdPropietarioOro.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO03");
            }

            if (rdInquilinoBronce.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO04");
            }

            if (rdInquilinoPlata.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO05");
            }

            if (rdInquilinoOro.Checked)
            {
                informacionPlanSeleccionado = clVarias.obtenerInformacionPlan("HOGARTOT-1", "COMBO06");
            }
            
            ReportDocument objetoReporte = new ReportDocument();
            ConsultasBD objectoConsultas = new ConsultasBD();            
            DataTable InformacionCotizacionMYSQL = new DataTable("DT_Prime_Dolares_MYSQL");
            DataTable InformacionCotizacionORACLE = new DataTable("DT_Prime_Dolares_ORACLE");
            DataTable InformacionBeneficios = new DataTable("DT_Beneficios");
            DataTable InformacionPlanesWeb = new DataTable("DT_Planes_Web");            
            string nombrePlan = string.Empty;
            
            nombrePlan = informacionPlanSeleccionado.Rows[0]["nombre"].ToString();
            InformacionCotizacionMYSQL = objectoConsultas.ObtenerInformacionCotizacionMYSQLHOGAR(lblNumeroCotizacion.Text);
            InformacionCotizacionORACLE = objectoConsultas.ObtenerInformacionCotizacionORACLE(lblNumeroCotizacion.Text);            
            
            string nombrePDF = informacionUsuario.idUsuario + "_" + DateTime.Now.Date.ToString("ddMMyyyy") + lblNumeroCotizacion.Text + nombrePlan;            

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
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/HogarSeguroDiamanteInqui.rpt"));
                    break;
                case "HOGAR SEGURO RUBI/INQUILINO":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/HogarSeguroRubiInqui.rpt"));
                    break;
                case "HOGAR SEGURO ESMERALDA/INQUILINO":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/HogarSeguroEsmeralInqui.rpt"));
                    break;
                //default:
                //  objetoReporte.Load(Server.MapPath("../Reportes/RPT/HogarSeguro.rpt"));
                //break;
            }

            objetoReporte.Database.Tables[0].SetDataSource(InformacionCotizacionMYSQL);
            objetoReporte.Database.Tables[1].SetDataSource(InformacionCotizacionORACLE);

            upnlDetalleCotExterna.Update();
            objetoReporte.ExportToHttpResponse
                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, nombrePDF);

            upnlDetalleCotExterna.Update();
        }
    }  
}