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
    public partial class WebForm1 : System.Web.UI.Page
    {
        int idUsuario = 0;
        int idPlan = 0;
        int idCotizacion = 0;
        DataTable datosPais;
        DataTable datosDepartamentos;
        DataTable datosMunicipios;
        DataTable formasPagoPlan = new DataTable();
        DataTable informacionPlanSeleccionado = new DataTable();
        ConsultasBD clConsultasBD = new ConsultasBD();
        DataTable numeroPagosPlan = new DataTable();
        Varias clVarias = new Varias();
        string cCodprod = string.Empty;
        string cCodplan = string.Empty;
        string cRevplan = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            idUsuario = Convert.ToInt32(Request.QueryString["userId"]);
            idPlan = Convert.ToInt32(Request.QueryString["idplan"]);

            if (!IsPostBack)
            {
                idUsuario = Convert.ToInt32(Request.QueryString["userId"]);
                idPlan = Convert.ToInt32(Request.QueryString["idplan"]);
                llenarCombosGeograficos();
            }
        }
        private void llenarCombosGeograficos()
        {
            datosPais = new DataTable();
            datosDepartamentos = new DataTable();
            datosMunicipios = new DataTable();
            Consultas clVarios = new Consultas();

            datosPais = clVarios.obtenerPaises();
            datosDepartamentos = clVarios.obtenerDepartamentos();
            datosMunicipios = clVarios.obtenerMunicipios();

            ViewState.Add("datosPais", datosPais);
            ViewState.Add("datosDepartamentos", datosDepartamentos);
            ViewState.Add("datosMunicipios", datosMunicipios);

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
        private string completarCodigoAgente(int codAgenteRemoto)
        {
            string[] CodigoAgenteMYSQL = new string[2];
            string cantidaddeceros = "";

            CodigoAgenteMYSQL[0] = cantidaddeceros;
            CodigoAgenteMYSQL[1] = Convert.ToString(codAgenteRemoto);

            if (CodigoAgenteMYSQL[1].Length < 6)
            {
                switch (CodigoAgenteMYSQL[1].Length)
                {
                    case 5:
                        CodigoAgenteMYSQL[0] = "0";
                        break;
                    case 4:
                        CodigoAgenteMYSQL[0] = "00";
                        break;
                    case 3:
                        CodigoAgenteMYSQL[0] = "000";
                        break;
                    case 2:
                        CodigoAgenteMYSQL[0] = "0000";
                        break;
                    case 1:
                        CodigoAgenteMYSQL[0] = "00000";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                CodigoAgenteMYSQL[0] = "";
            }

            return CodigoAgenteMYSQL[0] + CodigoAgenteMYSQL[1];
        }
        protected void btnRegresarCotizacion_Click(object sender, EventArgs e)
        {
            //Response.Redirect("../Hogar_Seguro/Cotizaciones.aspx?codagent=" + codAgente);
        }
        protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
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

            cCodprod = informacionPlanSeleccionado.Rows[0]["codprod"].ToString();
            cCodplan = informacionPlanSeleccionado.Rows[0]["codplan"].ToString();
            cRevplan = informacionPlanSeleccionado.Rows[0]["revplan"].ToString();

            numeroPagosPlan = clConsultasBD.obtenerNumeroPagosXPlan(cCodprod, cCodplan, cRevplan, ddlFormaPago.SelectedValue);
            ddlNumeroPagos.DataSource = numeroPagosPlan;
            ddlNumeroPagos.DataValueField = "CODPLANFINAN";
            ddlNumeroPagos.DataTextField = "NOMPLAN";
            ddlNumeroPagos.DataBind();
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
                pnlPagos.Visible = true;
                upnlPagos.Update();
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
                pnlPagos.Visible = true;
                upnlPagos.Update();
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
                pnlPagos.Visible = true;
                upnlPagos.Update();
                btnCotizar.Focus();

                //llenarCombos("HOGARTOT-1", "COMBO03");
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
                pnlPagos.Visible = true;
                upnlPagos.Update();
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
                pnlPagos.Visible = true;
                upnlPagos.Update();
                btnCotizar.Focus();

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
                pnlPagos.Visible = true;
                upnlPagos.Update();
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
            
            ddlFormaPago.DataSource = formasPagoPlan;
            ddlFormaPago.DataValueField = "CODIGO";
            ddlFormaPago.DataTextField = "DESCRIP";
            ddlFormaPago.DataBind();
            ddlFormaPago_SelectedIndexChanged(ddlFormaPago, new EventArgs());
            //numeroPagosXPlan();

            
        }
        private void Cotizar(string id_plan_pol, string plan_pol, IngresoSistema.informacionUsuario informacionUsuario)
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

            //Se crea la estructura del bien para generar el XML
            List<CA_BIENES_CERTIFICADO> listadoBienes = new List<CA_BIENES_CERTIFICADO>();
            CA_BIENES_CERTIFICADO Bien = new CA_BIENES_CERTIFICADO();

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
            Certificado.TIPO_OPERACION = "EMI"; // QUEMADO    

            //Se agrega el certificado al listado de certificados
            listadoCertificados.Add(Certificado);

            Proceso_Emision_Hogar_Seguro objetoEmisionHogarSeguro = new Proceso_Emision_Hogar_Seguro();
            DataTable informacionCodClaseBien = new DataTable();
            
            informacionCodClaseBien = objetoEmisionHogarSeguro.obtenerClaseCodBien();

            for (int i = 0; i < informacionCodClaseBien.Rows.Count; i++)
            {
                Bien = new CA_BIENES_CERTIFICADO();
                Bien.IDEPROCESO = IDEPROCESO.ToString();
                Bien.LLAVE = "1";
                Bien.LLAVE_ALTERNA = "1";
                Bien.LINEA = (1 + i).ToString();
                Bien.CLASEBIEN = informacionCodClaseBien.Rows[i]["CLASEBIEN"].ToString();
                Bien.CODBIEN = informacionCodClaseBien.Rows[i]["CODBIEN"].ToString();
                Bien.DIRECC = txtDireccionVivienda.Text;
                Bien.PAIS_RIESGO = cmbPais.SelectedValue;
                Bien.DEPARTAMENTO_RIESGO = cmbDepartamento.SelectedValue;
                Bien.MUNICIPIO_RIESGO = cmbMunicipio.SelectedValue;
                Bien.ALDEA_LOCALIDAD_ZONA_RIESGO = "000";
                Bien.MONTO_ASEGURADO = "0";
                Bien.LAYOUT = "0";
                Bien.STSCA = "INC";

                listadoBienes.Add(Bien);
            }

            //Estas listas se agregan vacias porque el plan no tiene recargos ni coberturas adicionales
            List<CA_COBERTURA> listaCoberturas = new List<CA_COBERTURA>();
            List<CA_REGARGO> listaRecargos = new List<CA_REGARGO>();

            //Luego se generan los xml con los datos de los layouts
            string xmlAsegurado = clEmitirPoliza.generarXML_CA_ASEGURADO(listadoAsegurados);
            string xmlCertificado = clEmitirPoliza.generarXML_CA_CERTIFICADO(listadoCertificados, listaCoberturas, listaRecargos);
            string xmlBien = clEmitirPoliza.generarXML_CA_BIEN(listadoBienes);

            //Se ejecuta el proceso de carga de lote que lleva como parametros los xml generados anteriormente
            string respuesta = clEmitirPoliza.CARGA_LOTE(Convert.ToInt32(IDEPROCESO), "", xmlCertificado, xmlAsegurado, xmlBien);           

            if (respuesta.Contains("satisfactoriamente"))
            {
               clEmitirPoliza.CARGA_ARCHXML(Convert.ToInt32(IDEPROCESO));
               clEmitirPoliza.CREAR(Convert.ToInt32(IDEPROCESO));

               DataTable idepol = new DataTable();
               idepol = clEmitirPoliza.ObtenerIdepol(IDEPROCESO.ToString());

               ViewState.Add("IDEPROCESO", IDEPROCESO);
               ViewState.Add("IDEPOL", idepol.Rows[0]["IDEPOL"].ToString());

               DataTable montosVisaCuotas = new DataTable();
               DataTable montosFraccionados = new DataTable();

               informacionPlanSeleccionado = clVarias.obtenerInformacionPlan(id_plan_pol, plan_pol);

               cCodprod = informacionPlanSeleccionado.Rows[0]["codprod"].ToString();
               cCodplan = informacionPlanSeleccionado.Rows[0]["codplan"].ToString();
               cRevplan = informacionPlanSeleccionado.Rows[0]["revplan"].ToString();

               //montosVisaCuotas = clEmitirPoliza.obtenerMontoPrimaTotalPorTipoPago(cCodprod, cCodplan, cRevplan, "S", 
               //    IDEPROCESO.ToString(), ddlNumeroPagos.SelectedValue);

               //montosFraccionados = clEmitirPoliza.obtenerMontoPrimaTotalPorTipoPago(cCodprod, cCodplan, cRevplan, "N", 
               //    IDEPROCESO.ToString(), ddlNumeroPagos.SelectedValue);

               if (montosFraccionados.Rows.Count > 0)
               {
                   ViewState.Add("PAGOCONTADO", montosFraccionados.Rows[0]["Monto"].ToString());

                   ViewState.Add("PAGODOSFRAC", montosFraccionados.Rows[1]["Monto"].ToString());

                   ViewState.Add("PAGOCUATROFRAC", montosFraccionados.Rows[2]["Monto"].ToString());

                   ViewState.Add("PAGOSEIFRACS", montosFraccionados.Rows[3]["Monto"].ToString());

                   ViewState.Add("PAGOOCHOFRAC", montosFraccionados.Rows[4]["Monto"].ToString());

                   ViewState.Add("PAGODIEZFRAC", montosFraccionados.Rows[5]["Monto"].ToString());

                   ViewState.Add("PAGODOCEFRAC", montosFraccionados.Rows[6]["Monto"].ToString());
               }
               else
               {
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
                   ViewState.Add("PAGODOSVISC", "0");

                   ViewState.Add("PAGOTRESVISC", montosVisaCuotas.Rows[0]["Monto"].ToString());

                   ViewState.Add("PAGOSEIVISC", montosVisaCuotas.Rows[1]["Monto"].ToString());

                   ViewState.Add("PAGODIEZVISC", montosVisaCuotas.Rows[2]["Monto"].ToString());

                   ViewState.Add("PAGODOCEVISC", montosVisaCuotas.Rows[3]["Monto"].ToString());
               }
               else
               {
                   ViewState.Add("PAGODOSVISC", "0");

                   ViewState.Add("PAGOTRESVISC", "0");

                   ViewState.Add("PAGOSEIVISC", "0");

                   ViewState.Add("PAGODIEZVISC", "0");

                   ViewState.Add("PAGODOCEVISC", "0");
               }

               gvInformacionPlanesCuotas.DataSource = montosVisaCuotas;
               gvInformacionPlanesCuotas.DataBind();

               gvInformacionPlanesFraccionado.DataSource = montosFraccionados;
               gvInformacionPlanesFraccionado.DataBind();

               lblResumenPagosFraccionados.Visible = true;
               lblResumenPagosVisaCuotas.Visible = true;
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
            }
            else
            {
                cmbMunicipio.Visible = false;
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
        protected void btnRegresar_Click(object sender, EventArgs e)
        {

        }
        protected void btnCotizar_Click(object sender, EventArgs e)
        {
            if (rdPropietarioBronce.Checked)
            {
                //Cotizar("HOGARTOT-1", "COMBO01");
            }

            if (rdPropietarioPlata.Checked)
            {
                //Cotizar("HOGARTOT-1", "COMBO02");
            }

            if (rdPropietarioOro.Checked)
            {
                //Cotizar("HOGARTOT-1", "COMBO03");
            }

            //btnGuardarCotizacion.Visible = true;
        }
        protected void btnGuardarCotizacion_Click(object sender, EventArgs e)
        {
            InsertsBD objetoInserts = new InsertsBD();
            Proceso_Cotizacion clProcesoCotizacion = new Proceso_Cotizacion();
            double montoTotalCancelar = 0;
            double valorDeducible = 0.00;
            double valorFinalEquipoEspecial = 0;
            string codigoAlarma = string.Empty;
            string descripAlarma = string.Empty;          

            clProcesoCotizacion.guardarCotizacionAutos(txtNombreCliente.Text,
                    txtCorreoCliente.Text, txtTelefonoCliente.Text, Convert.ToDouble(0), "001",
                    "001", "001", "001", "001",
                    "001", "02", "QUEMADO", "0000", Convert.ToInt32(0), 1,
                    DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Today.ToString("yyyy-MM-dd"), 
                    DateTime.Today.AddDays(365).ToString("yyyy-MM-dd"), 
                    DateTime.Today.ToString("yyyy-MM-dd"), 
                    idUsuario.ToString(), idPlan.ToString(), 
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
                    "0", 
                    Convert.ToDouble(ViewState["PAGOCONTADO"].ToString())); 

            btnGuardarCotizacion.Enabled = false;
        }            
    }
}