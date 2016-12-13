using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Inserts;
using Lbl_Cotizado_Autos_Web.Estructuras;
using Lbl_Cotizado_Autos_Web.Comunes;
using Lbl_Cotizador_Autos_Web.Acceso;

namespace CotizadorWebAutos.Productos_Web
{
    public partial class Cotizar_Auto : System.Web.UI.Page
    {
        IngresoSistema.informacionUsuario informacionUsuario;
        DataTable btNombrePlan = new DataTable();
        DataTable dtNumeroPagos = new DataTable();
        DataTable dtTipoVehiculos = new DataTable();
        DataTable dtMarcasVehiculos = new DataTable();
        DataTable dtLineasVehiculos = new DataTable();
        DataTable dtLIntermediarioXUsuario = new DataTable();
        DataTable dtRC = new DataTable();
        DataTable dtMenores = new DataTable();
        DataTable dtCristales = new DataTable();
        DataTable dtMuerteAc = new DataTable();
        DataTable dtDanosOcupantes = new DataTable();
        ConsultasBD objetoConsultas = new ConsultasBD();
        InsertsBD objetoInserts = new InsertsBD();
        DataTable informacionPlan = new DataTable();
        DataTable coberturasAdicionales = new DataTable();
        DataTable RecargosAdicionales = new DataTable();
        DataTable informacionRCPorRow = new DataTable();
        DataTable informacionOcupanteAUSUNO = new DataTable();
        DataTable informacionOcupanteAUSDOS = new DataTable();
        DataTable pagos = new DataTable();
        DataTable montosVisaCuotas = new DataTable();
        DataTable montosFraccionados = new DataTable();
        DataTable resultadoPrimaDesglozada = new DataTable();
        CA_COBERTURA Cobertura = new CA_COBERTURA();
        CA_REGARGO Recargo = new CA_REGARGO();
        DataTable descuentos = new DataTable();
        DataTable formaPago = new DataTable();
        DataTable modplan = new DataTable();
        DataTable descripcionNombresPlanes = new DataTable();
        List<CA_COBERTURA> listaCoberturas = new List<CA_COBERTURA>();
        List<CA_REGARGO> listaRecargos = new List<CA_REGARGO>();       
        string nombrePlan = string.Empty;
        string idPlan = string.Empty;
        string idUsuario = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();
                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];

                idPlan = Session["idPlan"].ToString();
                idUsuario = informacionUsuario.idUsuario.ToString();

                descripcionNombresPlanes = objetoConsultas.ObtenerNombrePlan(idPlan);
                nombrePlan = descripcionNombresPlanes.Rows[0]["nombre"].ToString();
                // MMora 18082016:  Esto lo coloco en lugar del switch de nombre plan para colocar moneda e imagin
                imagenProducto.ImageUrl = descripcionNombresPlanes.Rows[0]["url_imagen"].ToString();
                lblValorVehiculoMoneda.Text = descripcionNombresPlanes.Rows[0]["moneda"].ToString();
                lblEquipoEspecialMoneda.Text = descripcionNombresPlanes.Rows[0]["moneda"].ToString();

                if (nombrePlan == "SEGURO COMPLETO")
                {
                    txtDescuento.Visible = true;
                    lblDescuentos.Visible = true;
                }
                else
                {
                    txtDescuento.Visible = false;
                    lblDescuentos.Visible = false;
                }

                if (nombrePlan == "R.C. 99")
                {
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                }

                if (nombrePlan == "SEGURO RC DE AUTOMOVIL")
                {
                    Panel1.Visible = false;
                    Panel2.Visible = false;
                    Panel3.Visible = false;
                }

                if (!IsPostBack)
                {

                    btNombrePlan = objetoConsultas.ObtenerNombrePlan(idPlan);
                    nombrePlan = btNombrePlan.Rows[0]["nombre"].ToString();

                    string[] monto = btNombrePlan.Rows[0]["maximo_asegurar_casco"].ToString().Split('.');

                    txtValorVehiculo.Text = monto[0].ToString();



                    formaPago = objetoConsultas.ObtenerInformacionFormaPago(btNombrePlan.Rows[0]["codprod"].ToString(),
                                                                           btNombrePlan.Rows[0]["codplan"].ToString(),
                                                                           btNombrePlan.Rows[0]["revplan"].ToString());

                    ddlFormaPago.DataSource = formaPago;
                    ddlFormaPago.DataValueField = "CODIGO";
                    ddlFormaPago.DataTextField = "DESCRIP";
                    ddlFormaPago.DataBind();

                    lblNombreProducto.Text = nombrePlan;
                    dtNumeroPagos = objetoConsultas.obtenerNumeroPagosXPlan(btNombrePlan.Rows[0]["codprod"].ToString(),
                                                                                    btNombrePlan.Rows[0]["codplan"].ToString(),
                                                                                    btNombrePlan.Rows[0]["revplan"].ToString(),
                                                                                    ddlFormaPago.SelectedValue.Substring(0, 1));
                    ddlNumeroPagos.DataSource = dtNumeroPagos;
                    ddlNumeroPagos.DataValueField = "CODPLANFINAN";
                    ddlNumeroPagos.DataTextField = "NOMPLAN";
                    ddlNumeroPagos.DataBind();

                    dtTipoVehiculos = objetoConsultas.ObtenerTipoVehiculo(btNombrePlan.Rows[0]["codprod"].ToString(),
                                                                           btNombrePlan.Rows[0]["codplan"].ToString(),
                                                                           btNombrePlan.Rows[0]["revplan"].ToString());

                    ddlTipoVehiculo.DataSource = dtTipoVehiculos;
                    ddlTipoVehiculo.DataValueField = "TIPOVEH";
                    ddlTipoVehiculo.DataTextField = "DESCRIP";
                    ddlTipoVehiculo.DataBind();

                    dtMarcasVehiculos = objetoConsultas.ObtenerListadoMarcasVehiculo();

                    ddlMarcaVehiculo.DataSource = dtMarcasVehiculos;
                    ddlMarcaVehiculo.DataValueField = "CODMARCA";
                    ddlMarcaVehiculo.DataTextField = "DESCMARCA";
                    ddlMarcaVehiculo.DataBind();

                    dtLineasVehiculos = objetoConsultas.ObtenerListadoLineasVehiculo(ddlMarcaVehiculo.SelectedValue);

                    ddlLineaVehiculo.DataSource = dtLineasVehiculos;
                    ddlLineaVehiculo.DataValueField = "CODMODELO";
                    ddlLineaVehiculo.DataTextField = "DESCMODELO";
                    ddlLineaVehiculo.DataBind();

                    // MMora 11082016: Se toman los datos de intermediarios asignados a cada usuario             

                    dtLIntermediarioXUsuario = objetoConsultas.ObtenerIntermediariosXUsuario(idUsuario);
                    ddlIntermediario.DataSource = dtLIntermediarioXUsuario;
                    ddlIntermediario.DataValueField = "CodInter";
                    ddlIntermediario.DataTextField = "NomComercial";
                    ddlIntermediario.DataBind();


                    if (txtValorVehiculo.Text == string.Empty)
                    {
                        txtValorVehiculo.Text = "100000";
                    }

                    obtenerRCVehiculo();

                    coberturasAdicionales = objetoConsultas.ObtenerInformacionCoberturasAdicionales(btNombrePlan.Rows[0]["codprod"].ToString(),
                                                                                    btNombrePlan.Rows[0]["codplan"].ToString(),
                                                                                    btNombrePlan.Rows[0]["revplan"].ToString(),
                                                                                    ddlTipoVehiculo.SelectedValue, txtValorVehiculo.Text);

                    gvCoberturasAdicionales.DataSource = coberturasAdicionales;
                    gvCoberturasAdicionales.DataBind();

                    if (coberturasAdicionales.Rows.Count == 0)
                    {
                        lblCoberturasAdicionales.Visible = false;
                    }
                    else
                    {
                        lblCoberturasAdicionales.Visible = true;
                    }


                    RecargosAdicionales = objetoConsultas.ObtenerInformacionRecargosAdicionales(btNombrePlan.Rows[0]["codprod"].ToString(),
                                                                                    btNombrePlan.Rows[0]["codplan"].ToString(),
                                                                                    btNombrePlan.Rows[0]["revplan"].ToString());

                    gvRecargosAdicionales.DataSource = RecargosAdicionales;
                    gvRecargosAdicionales.DataBind();

                    if (RecargosAdicionales.Rows.Count == 0)
                    {
                        lblRecargos.Visible = false;
                    }
                    else
                    {
                        lblRecargos.Visible = true;
                    }

                    dtDanosOcupantes = objetoConsultas.ObtenerInforomacionCoberturaOcupantes(btNombrePlan.Rows[0]["codprod"].ToString(),
                                                                                     btNombrePlan.Rows[0]["codplan"].ToString(),
                                                                                     btNombrePlan.Rows[0]["revplan"].ToString(),
                                                                                     ddlTipoVehiculo.SelectedValue, ddlLineaVehiculo.SelectedValue,
                                                                                     ddlMarcaVehiculo.SelectedValue, txtValorVehiculo.Text);

                    ddlDanosOcupantes.DataSource = dtDanosOcupantes;
                    ddlDanosOcupantes.DataValueField = "CODSUBPLAN";
                    ddlDanosOcupantes.DataTextField = "SUMAPORASEG";
                    ddlDanosOcupantes.DataBind();

                    ddlAnioVehiculo.Items.Clear();

                    int contador = 0;
                    if ((nombrePlan == "AUTO PRIME $.") || (nombrePlan == "AUTO PRIME Q."))
                    {
                        for (int i = (DateTime.Now.Year - 5); i <= DateTime.Now.Year + 1; i++)
                        {
                            ListItem item = new ListItem(((DateTime.Now.Year + 1) - contador).ToString(), ((DateTime.Now.Year + 1) - contador).ToString());
                            ddlAnioVehiculo.Items.Add(item);
                            contador = contador + 1;
                        }
                    }

                    contador = 0;
                    if ((nombrePlan == "R.C. 99") || (nombrePlan == "SEGURO RC DE AUTOMOVIL"))
                    {
                        for (int i = (DateTime.Now.Year - 20); i <= DateTime.Now.Year + 1; i++)
                        {
                            ListItem item = new ListItem(((DateTime.Now.Year + 1) - contador).ToString(), ((DateTime.Now.Year + 1) - contador).ToString());
                            ddlAnioVehiculo.Items.Add(item);
                            contador = contador + 1;
                        }
                    }

                    contador = 0;
                    if ((nombrePlan == "PROMOCION DIEZ (Q)") || (nombrePlan == "PROMOCION DIEZ ($)") || (nombrePlan == "AUTO DIEZ (Q)") || (nombrePlan == "AUTO DIEZ ($)"))
                    {
                        for (int i = (DateTime.Now.Year - 10); i <= DateTime.Now.Year + 1; i++)
                        {
                            ListItem item = new ListItem(((DateTime.Now.Year + 1) - contador).ToString(), ((DateTime.Now.Year + 1) - contador).ToString());
                            ddlAnioVehiculo.Items.Add(item);
                            contador = contador + 1;
                        }
                    }

                    contador = 0;
                    if ((nombrePlan != "AUTO PRIME $.") && (nombrePlan != "AUTO PRIME Q.") && (nombrePlan != "R.C. 99") &&
                        (nombrePlan != "PROMOCION DIEZ (Q)") && (nombrePlan != "PROMOCION DIEZ ($)") && (nombrePlan != "SEGURO RC DE AUTOMOVIL") &&
                        (nombrePlan != "AUTO DIEZ (Q)") && (nombrePlan != "AUTO DIEZ ($)"))
                    {
                        for (int i = (DateTime.Now.Year - 15); i <= DateTime.Now.Year + 1; i++)
                        {
                            ListItem item = new ListItem(((DateTime.Now.Year + 1) - contador).ToString(), ((DateTime.Now.Year + 1) - contador).ToString());
                            ddlAnioVehiculo.Items.Add(item);
                            contador = contador + 1;
                        }
                    }
                }
            }
        }
        protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            btNombrePlan = objetoConsultas.ObtenerNombrePlan(idPlan);
            dtNumeroPagos = objetoConsultas.obtenerNumeroPagosXPlan(btNombrePlan.Rows[0]["codprod"].ToString(),
                                                                                btNombrePlan.Rows[0]["codplan"].ToString(),
                                                                                btNombrePlan.Rows[0]["revplan"].ToString(),
                                                                                ddlFormaPago.SelectedValue.Substring(0,1));
            ddlNumeroPagos.DataSource = dtNumeroPagos;
            ddlNumeroPagos.DataValueField = "CODPLANFINAN";
            ddlNumeroPagos.DataTextField = "NOMPLAN";
            ddlNumeroPagos.DataBind();
        }
        protected void ddlMarcaVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btNombrePlan = objetoConsultas.ObtenerNombrePlan(idPlan);
            nombrePlan = btNombrePlan.Rows[0]["nombre"].ToString();

            obtenerRCVehiculo();
            
            dtLineasVehiculos = objetoConsultas.ObtenerListadoLineasVehiculo(ddlMarcaVehiculo.SelectedValue);

            ddlLineaVehiculo.DataSource = dtLineasVehiculos;
            ddlLineaVehiculo.DataValueField = "CODMODELO";
            ddlLineaVehiculo.DataTextField = "DESCMODELO";
            ddlLineaVehiculo.DataBind();

            dtDanosOcupantes = objetoConsultas.ObtenerInforomacionCoberturaOcupantes(btNombrePlan.Rows[0]["codprod"].ToString(),
                                                                                 btNombrePlan.Rows[0]["codplan"].ToString(),
                                                                                 btNombrePlan.Rows[0]["revplan"].ToString(),
                                                                                 ddlTipoVehiculo.SelectedValue, ddlLineaVehiculo.SelectedValue,
                                                                                 ddlMarcaVehiculo.SelectedValue, txtValorVehiculo.Text);

            ddlDanosOcupantes.DataSource = dtDanosOcupantes;
            ddlDanosOcupantes.DataValueField = "CODSUBPLAN";
            ddlDanosOcupantes.DataTextField = "SUMAPORASEG";
            ddlDanosOcupantes.DataBind();
        }
        private void obtenerRCVehiculo()
        {
            btNombrePlan = objetoConsultas.ObtenerNombrePlan(idPlan);

            //Se dejo quemado el codigo del ramo AUR1 28/06/2016 JULIO LUNA
            dtRC = objetoConsultas.ObtenerInformacionRC(btNombrePlan.Rows[0]["codprod"].ToString(),
                                                            btNombrePlan.Rows[0]["codplan"].ToString(),
                                                            btNombrePlan.Rows[0]["revplan"].ToString(), "AUR1",
                                                            ddlTipoVehiculo.SelectedValue,
                                                            ddlLineaVehiculo.SelectedValue,
                                                            ddlMarcaVehiculo.SelectedValue);

            ddlRC.DataSource = dtRC;
            ddlRC.DataValueField = "SUMAASEG";
            ddlRC.DataTextField = "SUMAASEG";
            ddlRC.DataBind();
        }
        protected void ddlTipoVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btNombrePlan = objetoConsultas.ObtenerNombrePlan(idPlan);
            if (txtValorVehiculo.Text == string.Empty)
            {
                txtValorVehiculo.Text = "100000";
            }

            obtenerRCVehiculo();

            dtDanosOcupantes = objetoConsultas.ObtenerInforomacionCoberturaOcupantes(btNombrePlan.Rows[0]["codprod"].ToString(),
                                                                                 btNombrePlan.Rows[0]["codplan"].ToString(),
                                                                                 btNombrePlan.Rows[0]["revplan"].ToString(),
                                                                                 ddlTipoVehiculo.SelectedValue, ddlLineaVehiculo.SelectedValue,
                                                                                 ddlMarcaVehiculo.SelectedValue, txtValorVehiculo.Text);

            ddlDanosOcupantes.DataSource = dtDanosOcupantes;
            ddlDanosOcupantes.DataValueField = "CODSUBPLAN";
            ddlDanosOcupantes.DataTextField = "SUMAPORASEG";
            ddlDanosOcupantes.DataBind();

            coberturasAdicionales = objetoConsultas.ObtenerInformacionCoberturasAdicionales(btNombrePlan.Rows[0]["codprod"].ToString(),
                                                                                btNombrePlan.Rows[0]["codplan"].ToString(),
                                                                                btNombrePlan.Rows[0]["revplan"].ToString(),
                                                                                ddlTipoVehiculo.SelectedValue, txtValorVehiculo.Text);

            gvCoberturasAdicionales.DataSource = coberturasAdicionales;
            gvCoberturasAdicionales.DataBind();

            RecargosAdicionales = objetoConsultas.ObtenerInformacionRecargosAdicionales(btNombrePlan.Rows[0]["codprod"].ToString(),
                                                                                btNombrePlan.Rows[0]["codplan"].ToString(),
                                                                                btNombrePlan.Rows[0]["revplan"].ToString());

            gvRecargosAdicionales.DataSource = RecargosAdicionales;
            gvRecargosAdicionales.DataBind();
        }
        protected void btnCotizar_Click(object sender, EventArgs e)
        {
            if ((txtNombresCliente.Text != string.Empty) && (txtApellidosCliente.Text != string.Empty) && (txtValorVehiculo.Text != string.Empty) &&
                (txtTelefonoPrincipal.Text != string.Empty) && (txtCorreoElectronico.Text != string.Empty))
            {
                if (txtAsientosVehiculo.Text == string.Empty)
                {
                    txtAsientosVehiculo.Text = "5";
                }

                btNombrePlan = objetoConsultas.ObtenerNombrePlan(idPlan);

                string[] monto = btNombrePlan.Rows[0]["maximo_asegurar_casco"].ToString().Split('.');

                //txtValorVehiculo.Text = monto[0].ToString();

                if (Convert.ToInt32(monto[0].ToString()) >= float.Parse(txtValorVehiculo.Text))
                {
                    //proggressIcon.Visible = true;
                    DataTable descuentos = new DataTable();
                    descuentos = objetoConsultas.ObtenerDescuentos(idUsuario, idPlan);
                    if (txtDescuento.Text == string.Empty)
                    {
                        txtDescuento.Text = "0";
                    }
                    if ((descuentos.Rows.Count > 0))
                    {
                        if ((Convert.ToInt32(txtDescuento.Text) <= Convert.ToInt32(descuentos.Rows[0]["porcentaje_descuento_maximo"].ToString())) && (Convert.ToInt32(txtDescuento.Text) >= 0))
                        {
                            Cotizar();
                        }
                        else
                        {
                            MensajeError("EL DESCENTO NO PUEDE SER MAYOR A " + descuentos.Rows[0]["porcentaje_descuento_maximo"].ToString() + "%");
                        }
                    }
                    else
                    {
                        txtDescuento.Text = "0";
                        Cotizar();
                    }

                    //if ((nombrePlan != "SEGURO COMPLETO"))
                    //{
                    //    Cotizar();
                    //}

                    //proggressIcon.Visible = false;
                }
                else
                {
                    MensajeError("LO SENTIMOS NO PUEDE COTIZAR MONTOS MAYORES A " + btNombrePlan.Rows[0]["maximo_asegurar_casco"].ToString());
                }
            }
            else
            {
                MensajeError("ERROR, CAMPOS OBLIGARIOS VACIOS");
            }
        }
        public void Cotizar()
        {
            //Primero se debe obtener el IDEPROCESO DEL PAQUETE 
            string IDEPROCESO = objetoConsultas.GET_IDPROCESO("ACSEL_WEB").ToString();
            //Se crea la estructura del asegurado para generar el XML
            List<CA_ASEGURADO> listadoAsegurados = new List<CA_ASEGURADO>();
            CA_ASEGURADO Asegurado = new CA_ASEGURADO();

            DataTable UsuarioLogueado = new DataTable();
            Varias clVarias = new Varias();

            UsuarioLogueado = clVarias.obtenerInformacionUsuarioLogueado(int.Parse(idUsuario));

            Asegurado.USUARIO = informacionUsuario.codigoUsuarioRemoto;

            Asegurado.IDEPROCESO = IDEPROCESO;
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
            Asegurado.NIT = "846611-4"; //NIT MAPFRE
            Asegurado.TIPO_CLIENTE = "E";
            Asegurado.ESTADO_CIVIL = "S";
            Asegurado.ACTIVIDAD_ECONOMICA = "879";
            Asegurado.PAIS_DIRECCION = "001";
            Asegurado.DEPARTAMENTO_DIRECCION = "001";
            Asegurado.MUNICIPIO_DIRECCION = "001";
            Asegurado.ALDEA_LOCALIDAD_ZONA = "000";
            Asegurado.DIRECCION = "GUATEMALA";
            Asegurado.CORREO_ELECTRONICO = txtCorreoElectronico.Text;
            Asegurado.LINEA = "1";
            Asegurado.LAYOUT = "0";
            Asegurado.STSCA = "INC";
            Asegurado.DPI = "";
            Asegurado.PORCENTAJE = "100";
            Asegurado.TELEFONO1 = txtTelefonoPrincipal.Text;
           
            if (txtTelefonoSecundario.Text.Length > 0)
            {
                Asegurado.TELEFONO2 = txtTelefonoSecundario.Text;
            }

            listadoAsegurados.Add(Asegurado);

            informacionPlan = objetoConsultas.ObtenerNombrePlan(idPlan);

            //Se crea la estructura del certificado para generar el XML
            List<CA_CERTIFICADO> listadoCertificados = new List<CA_CERTIFICADO>();            

            CA_CERTIFICADO Certificado = new CA_CERTIFICADO();          

            Certificado.IDEPROCESO = IDEPROCESO;
            Certificado.LLAVE = "1";
            Certificado.LLAVE_ALTERNA = "1";
            Certificado.IDEPLANPOL = informacionPlan.Rows[0]["ide_plan_pol"].ToString();
            Certificado.PLANPOL = informacionPlan.Rows[0]["plan_pol"].ToString();
            Certificado.VIGENCIA_INICIAL = DateTime.Today.ToString("dd/MM/yyyy");
            Certificado.VIGENCIA_FINAL = DateTime.Today.AddDays(365).ToString("dd/MM/yyyy");
            Certificado.FECHA_INICIO_COBRO = DateTime.Today.ToString("dd/MM/yyyy");
            Certificado.PRIMER_PAGO_REALIZADO = "N";
            Certificado.FORMA_PAGO = "A";
            Certificado.PAGOS = "12";
            Certificado.SUMAASEGURADA = "";
            Certificado.LINEA = "1";
            Certificado.LAYOUT = "0";
            Certificado.STSCA = "INC";
            Certificado.MONTO_ASEGURADO = txtValorVehiculo.Text;
            Certificado.TIPOVEH = ddlTipoVehiculo.SelectedValue;
            Certificado.CODMARCA = ddlMarcaVehiculo.SelectedValue;
            Certificado.CODMODELO = ddlLineaVehiculo.SelectedValue;
            Certificado.CODVERSION = "02";
            Certificado.ANOVEH = ddlAnioVehiculo.SelectedValue;
            Certificado.NUMPUESTOS = txtAsientosVehiculo.Text;
            Certificado.SECCION_III = ddlDanosOcupantes.SelectedValue;
            // MMora 11082016: Cambio asignacion de codigo de intermediario al que tenga el combo
            //Certificado.CODINTER = UsuarioLogueado.Rows[0]["codigo_intermediario"].ToString();
            Certificado.CODINTER = ddlIntermediario.SelectedValue.Trim();

            if (rbAlarma.SelectedValue != "0")
            {
                Certificado.ALARMA = rbAlarma.SelectedValue;
            }
            
            if(txtEquipoEspecial.Text != string.Empty)
            {
                double primaEquipoEspecial = 0;
                double montoMaximoEspecial = 0;
                double sumaAsegurada = 0;
                DataTable tasaEquipoEspecial = new DataTable();
                Cobertura = new CA_COBERTURA();

                montoMaximoEspecial = Convert.ToDouble(txtValorVehiculo.Text) * 0.1; // TRAER A TABLA TARIFA_OTROS_TIPO_VEH
                tasaEquipoEspecial = objetoConsultas.ObtenerTasaEquipoEspecial(informacionPlan.Rows[0]["codprod"].ToString(),
                                                                                 informacionPlan.Rows[0]["codplan"].ToString(),
                                                                                 informacionPlan.Rows[0]["revplan"].ToString(),
                                                                                 ddlTipoVehiculo.SelectedValue, txtValorVehiculo.Text);

                if (Convert.ToDouble(txtEquipoEspecial.Text) > (montoMaximoEspecial))
                {
                    sumaAsegurada = montoMaximoEspecial;
                }
                else
                {
                    sumaAsegurada = Convert.ToDouble(txtEquipoEspecial.Text);
                }

                primaEquipoEspecial = (sumaAsegurada * Convert.ToDouble(tasaEquipoEspecial.Rows[0]["TASA"].ToString())) / 100;

                Cobertura.RAMO = tasaEquipoEspecial.Rows[0]["CODRAMO"].ToString();
                Cobertura.CODIGO = tasaEquipoEspecial.Rows[0]["CODCOBERT"].ToString();
                Cobertura.SUMAASEGURADA = sumaAsegurada.ToString();
                Cobertura.PRIMA = primaEquipoEspecial.ToString();
                Cobertura.TASA = tasaEquipoEspecial.Rows[0]["TASA"].ToString();
                Cobertura.IDEPROCESO = IDEPROCESO.ToString();
                Cobertura.LLAVE = "1";
                Cobertura.LLAVE_ALTERNA = "1";
                Cobertura.LINEA = "1";

                listaCoberturas.Add(Cobertura);
            }

            int i = 0;
            foreach (GridViewRow item in gvCoberturasAdicionales.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("cbSelect");
                Cobertura = new CA_COBERTURA();
                if (chk != null && chk.Checked)
                {
                    DropDownList ddlMontos = (item.FindControl("ddlMontos") as DropDownList);
                    string[] valorComboSeleccionado = ddlMontos.SelectedValue.ToString().Split(';');
                    Cobertura.RAMO = valorComboSeleccionado[0];//gvCoberturasAdicionales.Rows[i].Cells[1].Text;
                    Cobertura.CODIGO = valorComboSeleccionado[1];//gvCoberturasAdicionales.Rows[i].Cells[2].Text;
                    Cobertura.SUMAASEGURADA = valorComboSeleccionado[2];//gvCoberturasAdicionales.Rows[i].Cells[3].Text;
                    Cobertura.PRIMA = valorComboSeleccionado[3];//gvCoberturasAdicionales.Rows[i].Cells[5].Text; 
                    Cobertura.TASA = valorComboSeleccionado[4];//gvCoberturasAdicionales.Rows[i].Cells[4].Text; 
                    Cobertura.IDEPROCESO = IDEPROCESO.ToString();
                    Cobertura.LLAVE = "1";
                    Cobertura.LLAVE_ALTERNA = "1";
                    Cobertura.LINEA = "1";
                    listaCoberturas.Add(Cobertura);
                }

                i++;
            }

            informacionRCPorRow = objetoConsultas.ObtenerInforomacionRCPorROW(informacionPlan.Rows[0]["codprod"].ToString(),
                                                                                 informacionPlan.Rows[0]["codplan"].ToString(),
                                                                                 informacionPlan.Rows[0]["revplan"].ToString(),
                                                                                 ddlTipoVehiculo.SelectedValue, ddlLineaVehiculo.SelectedValue,
                                                                                 ddlMarcaVehiculo.SelectedValue,
                                                                                 txtValorVehiculo.Text,
                                                                                 ddlRC.SelectedValue);

            informacionOcupanteAUSUNO = objetoConsultas.ObtenerInformacionOcupanteAUSUNO(informacionPlan.Rows[0]["codprod"].ToString(),
                                                                                                 informacionPlan.Rows[0]["codplan"].ToString(),
                                                                                                 informacionPlan.Rows[0]["revplan"].ToString(),
                                                                                                 ddlTipoVehiculo.SelectedValue,txtValorVehiculo.Text,
                                                                                                 ddlDanosOcupantes.SelectedValue,
                                                                                                 ddlLineaVehiculo.SelectedValue,
                                                                                                 ddlMarcaVehiculo.SelectedValue);

            informacionOcupanteAUSDOS = objetoConsultas.ObtenerInformacionOcupanteAUSDOS(informacionPlan.Rows[0]["codprod"].ToString(),
                                                                                                 informacionPlan.Rows[0]["codplan"].ToString(),
                                                                                                 informacionPlan.Rows[0]["revplan"].ToString(),
                                                                                                 ddlTipoVehiculo.SelectedValue, txtValorVehiculo.Text,
                                                                                                 ddlDanosOcupantes.SelectedValue,
                                                                                                 ddlLineaVehiculo.SelectedValue,
                                                                                                 ddlMarcaVehiculo.SelectedValue);
            
            Cobertura = new CA_COBERTURA();
            //INGRESO DE R.C. EN EL CERTIFICADO
            //--------------------------------------------------------------------------------   
            Cobertura.RAMO = informacionRCPorRow.Rows[0]["CODRAMO"].ToString();
            Cobertura.CODIGO = informacionRCPorRow.Rows[0]["CODCOBERT"].ToString();
            Cobertura.SUMAASEGURADA = informacionRCPorRow.Rows[0]["SUMAASEG"].ToString();
            Cobertura.PRIMA = informacionRCPorRow.Rows[0]["PRIMA"].ToString();
            Cobertura.TASA = informacionRCPorRow.Rows[0]["TASA"].ToString();
            Cobertura.IDEPROCESO = IDEPROCESO.ToString();
            Cobertura.LLAVE = "1";
            Cobertura.LLAVE_ALTERNA = "1";
            Cobertura.LINEA = "1";

            listaCoberturas.Add(Cobertura);
            //--------------------------------------------------------------------------------
            Cobertura = new CA_COBERTURA();
            //INGRESO DE LESIONES POR OCUPANTES EN EL CERTIFICADO AUS1
            //--------------------------------------------------------------------------------
            Cobertura.RAMO = informacionOcupanteAUSUNO.Rows[0]["CODRAMO"].ToString();
            Cobertura.CODIGO = informacionOcupanteAUSUNO.Rows[0]["CODCOBERT"].ToString();
            Cobertura.SUMAASEGURADA = informacionOcupanteAUSUNO.Rows[0]["SUMAPORASEG"].ToString();
            Cobertura.PRIMA = informacionOcupanteAUSUNO.Rows[0]["PRIMA"].ToString();
            Cobertura.TASA = informacionOcupanteAUSUNO.Rows[0]["TASA"].ToString();
            Cobertura.IDEPROCESO = IDEPROCESO.ToString();
            Cobertura.LLAVE = "1";
            Cobertura.LLAVE_ALTERNA = "1";
            Cobertura.LINEA = "1";

            listaCoberturas.Add(Cobertura);
            //--------------------------------------------------------------------------------
            Cobertura = new CA_COBERTURA();
            //INGRESO DE LESIONES POR OCUPANTES EN EL CERTIFICADO AUS2
            //--------------------------------------------------------------------------------
            Cobertura.RAMO = informacionOcupanteAUSDOS.Rows[0]["CODRAMO"].ToString();
            Cobertura.CODIGO = informacionOcupanteAUSDOS.Rows[0]["CODCOBERT"].ToString();
            Cobertura.SUMAASEGURADA = informacionOcupanteAUSDOS.Rows[0]["SUMAPORASEG"].ToString();
            Cobertura.PRIMA = informacionOcupanteAUSDOS.Rows[0]["PRIMA"].ToString();
            Cobertura.TASA = informacionOcupanteAUSDOS.Rows[0]["TASA"].ToString();
            Cobertura.IDEPROCESO = IDEPROCESO.ToString();
            Cobertura.LLAVE = "1";
            Cobertura.LLAVE_ALTERNA = "1";
            Cobertura.LINEA = "1";

            listaCoberturas.Add(Cobertura);
            //--------------------------------------------------------------------------------
            i = 0;

            foreach (GridViewRow item in gvRecargosAdicionales.Rows)
            {
                CheckBox chk = (CheckBox)item.FindControl("cbSelect");
                Recargo = new CA_REGARGO();

                if (chk != null && chk.Checked)
                {
                    Recargo.CODIGO = gvRecargosAdicionales.Rows[i].Cells[1].Text;
                    Recargo.TIPO = "R";
                    Recargo.PORCENTAJE = gvRecargosAdicionales.Rows[i].Cells[4].Text;
                    Recargo.MONTO = gvRecargosAdicionales.Rows[i].Cells[3].Text;
                    Recargo.IDEPROCESO = IDEPROCESO.ToString();
                    Recargo.LLAVE = "1";
                    Recargo.LLAVE_ALTERNA = "1";
                    Recargo.LINEA = "1";

                    listaRecargos.Add(Recargo);
                }

                i++;
            }           

            //INGRESO DE MENORES EN EL CERTIFICADO
            //--------------------------------------------------------------------------------
            /*Cobertura.CODIGO = "LESI";
            Cobertura.SUMAASEGURADA = ddlMenores.SelectedItem.Text;
            Cobertura.PRIMA = "LESI";
            Cobertura.TASA = "LESI";

            listaCoberturas.Add(Cobertura);*/
            //--------------------------------------------------------------------------------

            //INGRESO DE CRISTALES EN EL CERTIFICADO
            //--------------------------------------------------------------------------------
            /*Cobertura.CODIGO = "CRIS";
            //Cobertura.SUMAASEGURADA = ddlCristales.SelectedItem.Text;
            Cobertura.PRIMA = "CRIS";
            Cobertura.TASA = "CRIS";

            listaCoberturas.Add(Cobertura);*/
            //--------------------------------------------------------------------------------

            //INGRESO DE MUERTE ACCIDENTAL EN EL CERTIFICADO
            //--------------------------------------------------------------------------------
            /*Cobertura.CODIGO = "MUER";
            //Cobertura.SUMAASEGURADA = ddlMuerte.SelectedItem.Text;
            Cobertura.PRIMA = "MUER";
            Cobertura.TASA = "MUER";

            listaCoberturas.Add(Cobertura);*/
            //--------------------------------------------------------------------------------

            listadoCertificados.Add(Certificado);

            if (nombrePlan == "SEGURO COMPLETO")
            {
                descuentos = objetoConsultas.ObtenerDescuentos(informacionPlan.Rows[0]["codprod"].ToString(),
                                                                informacionPlan.Rows[0]["codplan"].ToString(),
                                                                informacionPlan.Rows[0]["revplan"].ToString());
                Recargo = new CA_REGARGO();

                Recargo.CODIGO = descuentos.Rows[0]["CODRECADCTO"].ToString();
                Recargo.TIPO = "D";
                Recargo.PORCENTAJE = txtDescuento.Text;//"0";
                Recargo.MONTO = "";//txtDescuento.Text;//descuentos.Rows[0]["CODRECADCTO"].ToString();
                Recargo.IDEPROCESO = IDEPROCESO.ToString();
                Recargo.LLAVE = "1";
                Recargo.LLAVE_ALTERNA = "1";
                Recargo.LINEA = "1";

                listaRecargos.Add(Recargo);
            }

            //Luego se generan los xml con los datos de los layouts
            string xmlAsegurado = objetoConsultas.generarXML_CA_ASEGURADO(listadoAsegurados);
            string xmlCertificado = objetoConsultas.generarXML_CA_CERTIFICADO(listadoCertificados, listaCoberturas,listaRecargos);

            //Se ejecuta el proceso de carga de lote que lleva como parametros los xml generados anteriormente

            string respuesta = objetoConsultas.CARGA_LOTE(Convert.ToInt32(IDEPROCESO), "", xmlCertificado, xmlAsegurado, string.Empty);
            
            if (respuesta.Contains("satisfactoriamente"))
            {
                int idepol;

                objetoConsultas.CARGA_ARCHXML(Convert.ToInt32(IDEPROCESO));
                objetoConsultas.CREAR(Convert.ToInt32(IDEPROCESO));

                //se obtiene el idepol 
                idepol = objetoConsultas.ObtenerIdepol(int.Parse(IDEPROCESO));

                ViewState.Add("IDEPROCESO", IDEPROCESO);
                ViewState.Add("IDEPOL", idepol);
                ViewState.Add("IDEPLAN", informacionPlan.Rows[0]["ide_plan_pol"].ToString());
                ViewState.Add("PLANPOL", informacionPlan.Rows[0]["plan_pol"].ToString());
                btnGuardarCotizacionFinal.Visible = true;                

                //Se activa la poliza
                if (objetoConsultas.ActivarPolizaCotizacion(idepol))
                {
                    //Se genera cond financiamiento para la cotizacion
                    objetoConsultas.GenerarCondFinanciamientoTCotizacion(idepol);

                    montosVisaCuotas = objetoConsultas.obtenerMontoPrimaTotalPorTipoPago(informacionPlan.Rows[0]["codprod"].ToString(),
                                                                                 informacionPlan.Rows[0]["codplan"].ToString(),
                                                                                 informacionPlan.Rows[0]["revplan"].ToString(),
                                                                                 "S", ddlNumeroPagos.SelectedValue, lblValorVehiculoMoneda.Text,
                                                                                 idepol.ToString());


                    montosFraccionados = objetoConsultas.obtenerMontoPrimaTotalPorTipoPago(informacionPlan.Rows[0]["codprod"].ToString(),
                                                                                     informacionPlan.Rows[0]["codplan"].ToString(),
                                                                                     informacionPlan.Rows[0]["revplan"].ToString(),
                                                                                     "N", ddlNumeroPagos.SelectedValue, lblValorVehiculoMoneda.Text,
                                                                                     idepol.ToString());

                    gvInformacionPlanesCuotas.DataSource = montosVisaCuotas;
                    gvInformacionPlanesCuotas.DataBind();

                    gvInformacionPlanesFraccionado.DataSource = montosFraccionados;
                    gvInformacionPlanesFraccionado.DataBind();

                    lblResumenPagosFraccionados.Visible = true;
                    lblResumenPagosVisaCuotas.Visible = true;

                    btnRegresar.Focus();

                    ViewState.Add("montosVisaCuotas", montosVisaCuotas);
                    ViewState.Add("montosFraccionados", montosFraccionados);
                }
                else
                {
                    MensajeError("ERROR AL ACTIVAR LA POLIZA, PROCESO NUMERO: " + IDEPROCESO);
                }
            }
            else
            {
                MensajeError("ERROR, PROCESO NUMERO: " + IDEPROCESO);
            }            
        }
        private void mensaje(string mensaje, string url)
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
        protected void gvRecargosAdicionales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
        }
        protected void gvCoberturasAdicionales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            btNombrePlan = objetoConsultas.ObtenerNombrePlan(idPlan);
            coberturasAdicionales = objetoConsultas.ObtenerCoberturasAdicionalesPorCombo(btNombrePlan.Rows[0]["codprod"].ToString(),
                                                                                btNombrePlan.Rows[0]["codplan"].ToString(),
                                                                                btNombrePlan.Rows[0]["revplan"].ToString(),
                                                                                ddlTipoVehiculo.SelectedValue, txtValorVehiculo.Text);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow[] codCobert = coberturasAdicionales.Select("CODCOBERT = '"+ e.Row.Cells[1].Text +"'");
                DataTable newTable = new DataTable();
               

                if (codCobert.Count() > 0)
                {
                    newTable = coberturasAdicionales.Clone();

                    foreach (DataRow item in codCobert)
                    {
                        newTable.ImportRow(item);
                    }

                    DropDownList ddlMontos = (e.Row.FindControl("ddlMontos") as DropDownList);
                    ddlMontos.DataSource = newTable;
                    ddlMontos.DataTextField = "SUMAASEG";
                    ddlMontos.DataValueField = "VALORCOMBO";
                    ddlMontos.DataBind();
                    //string sa = ddlMontos.SelectedValue;
                    //Add Default Item in the DropDownList
                    //ddlMontos.Items.Insert(0, new ListItem("Please select"));
                }
                //Find the DropDownList in the Row
               

                // Select the Country of Customer in DropDownList
                //string country = (e.Row.FindControl("lblValor") as Label).Text;
                //ddlMontos.Items.FindByValue(country).Selected = true;
            }

            e.Row.Cells[1].Visible = false;
           // e.Row.Cells[2].Visible = false;
           // e.Row.Cells[4].Visible = false;
           // e.Row.Cells[6].Visible = false;
        }
        protected void btnGuardarCotizacionFinal_Click(object sender, EventArgs e)
        {
            int resultadoInsertCotizacion = 0;
            
            if((txtNombresCliente.Text != string.Empty) && (txtApellidosCliente.Text != string.Empty) && (txtValorVehiculo.Text !=string.Empty) &&
                (txtTelefonoPrincipal.Text != string.Empty) && (txtCorreoElectronico.Text != string.Empty))
            {
                double montoTotalCancelar = 0;

                informacionPlan = objetoConsultas.ObtenerNombrePlan(idPlan);

                montosVisaCuotas = ViewState["montosVisaCuotas"] as DataTable;
                montosFraccionados = ViewState["montosFraccionados"] as DataTable;


                //Se guarda el monto total a cancelar en base a la seleccion de la forma y numero de pagos elegida por el usuario
                if (ddlFormaPago.SelectedItem.Text.Contains("CUOTA"))
                {
                    DataRow[] montoTotal = montosVisaCuotas.Select("CODPLANFINAN = '" + ddlNumeroPagos.SelectedValue + "'");

                    if (montoTotal.Count() == 1)
                    {
                        montoTotalCancelar = double.Parse(montoTotal[0]["MONTO"].ToString());
                    }
                }
                else
                {
                    DataRow[] montoTotal = montosFraccionados.Select("CODPLANFINAN = '" + ddlNumeroPagos.SelectedValue + "'");

                    if (montoTotal.Count() == 1)
                    {
                        montoTotalCancelar = double.Parse(montoTotal[0]["MONTO"].ToString());
                    }
                }      
                
                double valorDeducible = 0.00;

                if (Convert.ToDouble(txtValorVehiculo.Text) > 50000.00)
                {
                    valorDeducible = Convert.ToDouble(txtValorVehiculo.Text) * 0.03;

                    if (valorDeducible < 1800)
                    {
                        valorDeducible = 1800.00;
                    }
                }
                else
                {
                    valorDeducible = 1800.00;
                }

                int i = 0;
                foreach (GridViewRow item in gvCoberturasAdicionales.Rows)
                {
                    CheckBox chk = (CheckBox)item.FindControl("cbSelect");
                    Cobertura = new CA_COBERTURA();
                    if (chk != null && chk.Checked)
                    {
                        DropDownList ddlMontos = (item.FindControl("ddlMontos") as DropDownList);
                        string[] valorComboSeleccionado = ddlMontos.SelectedValue.ToString().Split(';');
                        Cobertura.RAMO = valorComboSeleccionado[0];//gvCoberturasAdicionales.Rows[i].Cells[1].Text;
                        Cobertura.CODIGO = valorComboSeleccionado[1];//gvCoberturasAdicionales.Rows[i].Cells[2].Text;
                        Cobertura.SUMAASEGURADA = valorComboSeleccionado[2];//gvCoberturasAdicionales.Rows[i].Cells[3].Text;
                        Cobertura.PRIMA = valorComboSeleccionado[3];//gvCoberturasAdicionales.Rows[i].Cells[5].Text; 
                        Cobertura.TASA = valorComboSeleccionado[4];//gvCoberturasAdicionales.Rows[i].Cells[4].Text; 

                        listaCoberturas.Add(Cobertura);
                        //Cobertura.RAMO = gvCoberturasAdicionales.Rows[i].Cells[1].Text;
                        //Cobertura.CODIGO = gvCoberturasAdicionales.Rows[i].Cells[2].Text;
                        //Cobertura.SUMAASEGURADA = gvCoberturasAdicionales.Rows[i].Cells[3].Text;
                        //Cobertura.PRIMA = gvCoberturasAdicionales.Rows[i].Cells[5].Text;
                        //Cobertura.TASA = gvCoberturasAdicionales.Rows[i].Cells[4].Text;
                        //Cobertura.DESCRIPCION_COBERTURA = gvCoberturasAdicionales.Rows[i].Cells[7].Text;
                        //listaCoberturas.Add(Cobertura);
                    }
                    i++;
                }
                i = 0;
                foreach (GridViewRow item in gvRecargosAdicionales.Rows)
                {
                    CheckBox chk = (CheckBox)item.FindControl("cbSelect");
                    Recargo = new CA_REGARGO();

                    if (chk != null && chk.Checked)
                    {
                        Recargo.CODIGO = gvRecargosAdicionales.Rows[i].Cells[1].Text;
                        Recargo.TIPO = "R";
                        Recargo.PORCENTAJE = gvRecargosAdicionales.Rows[i].Cells[4].Text;
                        Recargo.MONTO = gvRecargosAdicionales.Rows[i].Cells[3].Text;
                        Recargo.DESCRIPCION_RECARGO = gvRecargosAdicionales.Rows[i].Cells[2].Text;
                        listaRecargos.Add(Recargo);
                    }
                    i++;
                }

                if (nombrePlan == "SEGURO COMPLETO")
                {
                    descuentos = objetoConsultas.ObtenerDescuentos(informacionPlan.Rows[0]["codprod"].ToString(),
                                                                informacionPlan.Rows[0]["codplan"].ToString(),
                                                                informacionPlan.Rows[0]["revplan"].ToString()); 
                    Recargo = new CA_REGARGO();

                    Recargo.CODIGO = descuentos.Rows[0]["CODRECADCTO"].ToString();
                    Recargo.TIPO = "D";
                    Recargo.PORCENTAJE = txtDescuento.Text;//"0";
                    Recargo.MONTO = "";//descuentos.Rows[0]["CODRECADCTO"].ToString();

                    listaRecargos.Add(Recargo);

                }

                if (txtAsientosVehiculo.Text == string.Empty)
                {
                    txtAsientosVehiculo.Text = "5";
                }

                double valorFinalEquipoEspecial = 0;
                informacionPlan = objetoConsultas.ObtenerNombrePlan(idPlan);

                if (txtEquipoEspecial.Text != string.Empty)
                {
                    double primaEquipoEspecial = 0;
                    double montoMaximoEspecial = 0;
                    double sumaAsegurada = 0;
                    DataTable tasaEquipoEspecial = new DataTable();
                    Cobertura = new CA_COBERTURA();

                    montoMaximoEspecial = Convert.ToDouble(txtValorVehiculo.Text) * 0.1; // TRAER A TABLA TARIFA_OTROS_TIPO_VEH
                    tasaEquipoEspecial = objetoConsultas.ObtenerTasaEquipoEspecial(informacionPlan.Rows[0]["codprod"].ToString(),
                                                                                     informacionPlan.Rows[0]["codplan"].ToString(),
                                                                                     informacionPlan.Rows[0]["revplan"].ToString(),
                                                                                     ddlTipoVehiculo.SelectedValue, txtValorVehiculo.Text);

                    if (Convert.ToDouble(txtEquipoEspecial.Text) > (montoMaximoEspecial))
                    {
                        sumaAsegurada = montoMaximoEspecial;
                    }
                    else
                    {
                        sumaAsegurada = Convert.ToDouble(txtEquipoEspecial.Text);
                    }

                    primaEquipoEspecial = (sumaAsegurada * Convert.ToDouble(tasaEquipoEspecial.Rows[0]["TASA"].ToString())) / 100;
                    valorFinalEquipoEspecial = sumaAsegurada;
                    Cobertura.RAMO = tasaEquipoEspecial.Rows[0]["CODRAMO"].ToString();
                    Cobertura.CODIGO = tasaEquipoEspecial.Rows[0]["CODCOBERT"].ToString();
                    Cobertura.SUMAASEGURADA = sumaAsegurada.ToString();
                    Cobertura.PRIMA = primaEquipoEspecial.ToString();
                    Cobertura.TASA = tasaEquipoEspecial.Rows[0]["TASA"].ToString();

                    listaCoberturas.Add(Cobertura);
                }

                string codigoAlarma = string.Empty;
                string descripAlarma = string.Empty;

                if (rbAlarma.SelectedValue != "0")
                {
                    codigoAlarma = rbAlarma.SelectedValue;
                    descripAlarma = rbAlarma.SelectedItem.Text;
                }

                //informacionPlan = objectoConsultas.ObtenerNombrePlan(idPlan);
                informacionRCPorRow = objetoConsultas.ObtenerInforomacionRCPorROW(informacionPlan.Rows[0]["codprod"].ToString(),
                                                                                 informacionPlan.Rows[0]["codplan"].ToString(),
                                                                                 informacionPlan.Rows[0]["revplan"].ToString(),
                                                                                 ddlTipoVehiculo.SelectedValue, ddlLineaVehiculo.SelectedValue,
                                                                                 ddlLineaVehiculo.SelectedValue,
                                                                                 txtValorVehiculo.Text,
                                                                                 ddlRC.SelectedValue);

                informacionOcupanteAUSUNO = objetoConsultas.ObtenerInformacionOcupanteAUSUNO(informacionPlan.Rows[0]["codprod"].ToString(),
                                                                                                 informacionPlan.Rows[0]["codplan"].ToString(),
                                                                                                 informacionPlan.Rows[0]["revplan"].ToString(),
                                                                                                 ddlTipoVehiculo.SelectedValue, txtValorVehiculo.Text,
                                                                                                 ddlDanosOcupantes.SelectedValue,
                                                                                                 ddlLineaVehiculo.SelectedValue,
                                                                                                 ddlMarcaVehiculo.SelectedValue);

                informacionOcupanteAUSDOS = objetoConsultas.ObtenerInformacionOcupanteAUSDOS(informacionPlan.Rows[0]["codprod"].ToString(),
                                                                                                     informacionPlan.Rows[0]["codplan"].ToString(),
                                                                                                     informacionPlan.Rows[0]["revplan"].ToString(),
                                                                                                     ddlTipoVehiculo.SelectedValue, txtValorVehiculo.Text,
                                                                                                     ddlDanosOcupantes.SelectedValue,
                                                                                                     ddlLineaVehiculo.SelectedValue,
                                                                                                     ddlMarcaVehiculo.SelectedValue);

                Cobertura = new CA_COBERTURA();
                //INGRESO DE R.C. EN EL CERTIFICADO
                //--------------------------------------------------------------------------------   
                Cobertura.RAMO = informacionRCPorRow.Rows[0]["CODRAMO"].ToString();
                Cobertura.CODIGO = informacionRCPorRow.Rows[0]["CODCOBERT"].ToString();
                Cobertura.SUMAASEGURADA = informacionRCPorRow.Rows[0]["SUMAASEG"].ToString();
                Cobertura.PRIMA = informacionRCPorRow.Rows[0]["PRIMA"].ToString();
                string tasa = string.Empty;

                if (informacionRCPorRow.Rows[0]["TASA"].ToString().Length == 0)
                {
                     tasa = "0";
                }
                else
                {
                    tasa = informacionRCPorRow.Rows[0]["TASA"].ToString();
                }

                Cobertura.TASA = tasa;

                listaCoberturas.Add(Cobertura);
                tasa = string.Empty;
                Cobertura = new CA_COBERTURA();
                //INGRESO DE LESIONES POR OCUPANTES EN EL CERTIFICADO AUS1
                //--------------------------------------------------------------------------------
                Cobertura.RAMO = informacionOcupanteAUSUNO.Rows[0]["CODRAMO"].ToString();
                Cobertura.CODIGO = informacionOcupanteAUSUNO.Rows[0]["CODCOBERT"].ToString();
                Cobertura.SUMAASEGURADA = informacionOcupanteAUSUNO.Rows[0]["SUMAPORASEG"].ToString();
                Cobertura.PRIMA = informacionOcupanteAUSUNO.Rows[0]["PRIMA"].ToString();
                Cobertura.TASA = informacionOcupanteAUSUNO.Rows[0]["TASA"].ToString();

                //--------------------------------------------------------------------------------
                if (informacionRCPorRow.Rows[0]["TASA"].ToString().Length == 0)
                {
                    tasa = "0";
                }
                else
                {
                    tasa = informacionRCPorRow.Rows[0]["TASA"].ToString();
                }
                Cobertura.TASA = tasa;

                listaCoberturas.Add(Cobertura);
                //--------------------------------------------------------------------------------
                //--------------------------------------------------------------------------------
                Cobertura = new CA_COBERTURA();
                //INGRESO DE LESIONES POR OCUPANTES EN EL CERTIFICADO AUS1
                //--------------------------------------------------------------------------------
                Cobertura.RAMO = informacionOcupanteAUSDOS.Rows[0]["CODRAMO"].ToString();
                Cobertura.CODIGO = informacionOcupanteAUSDOS.Rows[0]["CODCOBERT"].ToString();
                Cobertura.SUMAASEGURADA = informacionOcupanteAUSDOS.Rows[0]["SUMAPORASEG"].ToString();
                Cobertura.PRIMA = informacionOcupanteAUSDOS.Rows[0]["PRIMA"].ToString();
                Cobertura.TASA = informacionOcupanteAUSDOS.Rows[0]["TASA"].ToString();

                //--------------------------------------------------------------------------------
                if (informacionRCPorRow.Rows[0]["TASA"].ToString().Length == 0)
                {
                     tasa = "0";
                }
                else
                {
                    tasa = informacionRCPorRow.Rows[0]["TASA"].ToString();
                }
                Cobertura.TASA = tasa;

                listaCoberturas.Add(Cobertura);
                //--------------------------------------------------------------------------------
                modplan = objetoConsultas.ObtenerMODPLAN(informacionPlan.Rows[0]["codprod"].ToString(), informacionPlan.Rows[0]["codplan"].ToString(), informacionPlan.Rows[0]["revplan"].ToString(), ddlFormaPago.SelectedValue.Substring(0,1), ddlNumeroPagos.SelectedValue);

                double PAGOCONTADOFRAC = 0;
                double PAGODOSFRAC = 0;
                double PAGOCUATROFRAC = 0;
                double PAGOSEISFRAC = 0;
                double PAGOOCHOFRAC = 0;
                double PAGODIEZFRAC = 0;
                double PAGODOCEFRAC = 0;

                for (int j = 0; j < montosFraccionados.Rows.Count; j++)
                {
                    switch (montosFraccionados.Rows[j]["CUOTAS"].ToString())
                    {
                        case "1":
                            PAGOCONTADOFRAC = double.Parse(montosFraccionados.Rows[j]["MONTO"].ToString());
                            break;
                        case "2":
                            PAGODOSFRAC = double.Parse(montosFraccionados.Rows[j]["MONTO"].ToString());
                            break;
                        case "4":
                            PAGOCUATROFRAC = double.Parse(montosFraccionados.Rows[j]["MONTO"].ToString());
                            break;
                        case "6":
                            PAGOSEISFRAC = double.Parse(montosFraccionados.Rows[j]["MONTO"].ToString());
                            break;
                        case "8":
                            PAGOOCHOFRAC = double.Parse(montosFraccionados.Rows[j]["MONTO"].ToString());
                            break;
                        case "10":
                            PAGODIEZFRAC = double.Parse(montosFraccionados.Rows[j]["MONTO"].ToString());
                            break;
                        case "12":
                            PAGODOCEFRAC = double.Parse(montosFraccionados.Rows[j]["MONTO"].ToString());
                            break;
                        default:
                            break;
                    }                    
                }

                double PAGOTRESVISC = 0;
                double PAGOSEIVISC = 0;
                double PAGODIEZVISC = 0;
                double PAGODOCEVISC = 0;

                for (int K = 0; K < montosVisaCuotas.Rows.Count; K++)
                {
                    switch (montosVisaCuotas.Rows[K]["CUOTAS"].ToString())
                    {

                        case "3":
                            PAGOTRESVISC = double.Parse(montosVisaCuotas.Rows[K]["MONTO"].ToString());
                            break;
                        case "6":
                            PAGOSEIVISC = double.Parse(montosVisaCuotas.Rows[K]["MONTO"].ToString());
                            break;
                        case "10":
                            PAGODIEZVISC = double.Parse(montosVisaCuotas.Rows[K]["MONTO"].ToString());
                            break;
                        case "12":
                            PAGODOCEVISC = double.Parse(montosVisaCuotas.Rows[K]["MONTO"].ToString());
                            break;                        
                        default:
                            break;
                    }      
                }
               
                //MUERTE ACCIDENTAL Y CRISTALES SE LES COLOCO DE VALOR 0.00 PORQUE SE AGREGARON A LA TABLA DE COBERTURAS_ADICIONALES
                resultadoInsertCotizacion = objetoInserts.guardarCotizacionAutos((txtNombresCliente.Text + " " +txtApellidosCliente.Text),
                    txtCorreoElectronico.Text, txtTelefonoPrincipal.Text, Convert.ToDouble(txtValorVehiculo.Text), ddlTipoVehiculo.SelectedValue,
                    ddlTipoVehiculo.SelectedItem.Text, ddlMarcaVehiculo.SelectedValue, ddlMarcaVehiculo.SelectedItem.Text, ddlLineaVehiculo.SelectedValue,
                    ddlLineaVehiculo.SelectedItem.Text, "02", "QUEMADO", ddlAnioVehiculo.SelectedItem.Text, Convert.ToInt32(txtAsientosVehiculo.Text), 1,
                    DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Today.ToString("yyyy-MM-dd"), DateTime.Today.AddDays(365).ToString("yyyy-MM-dd"), DateTime.Today.ToString("yyyy-MM-dd"), idUsuario, idPlan, ViewState["IDEPROCESO"].ToString(),
                    ViewState["IDEPOL"].ToString(), Convert.ToInt32(ddlNumeroPagos.SelectedValue.Substring(4, 2)), 
                    montoTotalCancelar,

                    PAGODOSFRAC, PAGOCUATROFRAC,
                    PAGOSEISFRAC, PAGOOCHOFRAC,
                    PAGODIEZFRAC, PAGODOCEFRAC,
                    PAGOTRESVISC, PAGOSEIVISC,
                    PAGODIEZVISC, PAGODOCEVISC,
                    
                    valorDeducible, Convert.ToDouble(txtValorVehiculo.Text),
                    Convert.ToDouble(ddlDanosOcupantes.SelectedItem.Text), 0.00, Convert.ToDouble(ddlRC.SelectedItem.Text), Convert.ToDouble(ddlDanosOcupantes.SelectedItem.Text), // EL VALOR 0.00 ES DE GARANTIA
                    0.00, 0.00, listaCoberturas, listaRecargos, valorFinalEquipoEspecial, codigoAlarma, descripAlarma, ddlNumeroPagos.SelectedValue, modplan.Rows[0]["MODPLAN"].ToString(),
                    PAGOCONTADOFRAC, ddlDanosOcupantes.SelectedValue, ddlIntermediario.SelectedValue.Trim(), ddlFormaPago.SelectedItem.Text, ddlNumeroPagos.SelectedItem.Text); // EL PRIMER VALOR 0.00 ES CRISTALES Y EL SEGUNDO ES MUERTE ACCIDENTAL

                if(resultadoInsertCotizacion != 0)
                {
                    //mensaje("La cotización ha sido guardada.", "../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan");
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('LA COTIZACION HA SIDO GUARDADA');", true);
                    Response.Redirect("../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan);
                }
                else
                {
                    MensajeError("OCURRIO UN ERROR, AL MOMENTO DE GUARDAR LA COTIZACION");
                }
            }                                                                                                                 
        }
        protected void btnCancelarCotizacion_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan);
        }
        public void MensajeError(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('"+mensaje+"');", true); 
        }
        protected void ddlLineaVehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerRCVehiculo();

            btNombrePlan = objetoConsultas.ObtenerNombrePlan(idPlan);
            nombrePlan = btNombrePlan.Rows[0]["nombre"].ToString();

            dtDanosOcupantes = objetoConsultas.ObtenerInforomacionCoberturaOcupantes(btNombrePlan.Rows[0]["codprod"].ToString(),
                                                                                 btNombrePlan.Rows[0]["codplan"].ToString(),
                                                                                 btNombrePlan.Rows[0]["revplan"].ToString(),
                                                                                 ddlTipoVehiculo.SelectedValue, ddlLineaVehiculo.SelectedValue,
                                                                                 ddlMarcaVehiculo.SelectedValue, txtValorVehiculo.Text);

            ddlDanosOcupantes.DataSource = dtDanosOcupantes;
            ddlDanosOcupantes.DataValueField = "CODSUBPLAN";
            ddlDanosOcupantes.DataTextField = "SUMAPORASEG";
            ddlDanosOcupantes.DataBind();
        }
    }
}