using Lbl_Cotizado_Autos_Web.Clientes;
using Lbl_Cotizado_Autos_Web.Comunes;
using Lbl_Cotizador_Autos_Web.Acceso;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CotizadorWebAutos.Cliente
{
    public partial class ResponsablePago : System.Web.UI.Page
    {
        #region VARIABLES GLOBALES        
        int idCotizacion = 0;
        int idPlanSeleccionado = 0;

        DataTable datosPais;
        DataTable datosDepartamentos;
        DataTable datosMunicipios;
        DataTable datosZonas;
        DataTable datosNacionalidades;
        DataTable datosProfesiones;
        DataTable datosEstadoCivil;
        DataTable datosGeneros;
        DataTable datosTelefonos;
        IngresoSistema.informacionUsuario informacionUsuario;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();               

                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];
                idCotizacion = (int)Session["cotId"];
                idPlanSeleccionado = (int)Session["idPlan"];

                if (!IsPostBack)
                {
                    llenarCombos();
                }
            } 
        }
        protected void btnBuscarNitCliente_Click(object sender, EventArgs e)
        {
            if (txtDvIdCliente.Text == string.Empty || txtNumIDCliente.Text == string.Empty)
            {
                mostrarMensaje("Debe ingresar los campos del NIT.");

                return;
            }

            DataTable datosCliente = new DataTable();
            DataTable direccionesCliente = new DataTable();
            DataTable telefonosCliente = new DataTable();
            Consultas clConsultas = new Consultas();
            bool nitValido = false;

            nitValido = clConsultas.confirmaNitCliente(Convert.ToInt32(txtNumIDCliente.Text), txtDvIdCliente.Text);

            if (nitValido)
            {
                //CLIENTE INDIVIDUAL
                if (cmbTipoCliente.SelectedValue == "P")
                {
                    datosCliente = clConsultas.obtenerDatosClienteNit(Convert.ToInt32(txtNumIDCliente.Text), txtDvIdCliente.Text.Trim());

                    if (datosCliente.Rows.Count == 1)
                    {
                        string codCli = datosCliente.Rows[0]["CODCLI"].ToString();
                        direccionesCliente = clConsultas.obtenerDireccionesCliente(codCli);
                        telefonosCliente = clConsultas.obtenerTelefonosCliente(codCli);

                        if (datosCliente.Rows[0]["TIPOCLI"].ToString() == "P")
                        {
                            pnlDatosClienteExistente.Visible = true;
                            pnlClienteIndividualExistente.Visible = true;

                            pnlDatosClienteNuevo.Visible = false;
                            pnlClienteJuridicoExistente.Visible = false;

                            llenarControlesAsegTitularIndividual(datosCliente, telefonosCliente, direccionesCliente);
                        }
                    }
                    else
                    {
                        //limpiarControles();
                        mostrarMensaje("No se encontro información del cliente. Se habilitarán los campos para ingresar un nuevo cliente Individual.");

                        txtNumIdNuevoResPagoIndividual.Text = txtNumIDCliente.Text;
                        txtDvIdNuevoResPagoIndividual.Text = txtDvIdCliente.Text.Trim();

                        pnlDatosClienteExistente.Visible = false;
                        pnlNuevoRespPagoJuridico.Visible = false;
                        pnlDatosClienteNuevo.Visible = true;
                        pnlNuevoRespPagoIndividual.Visible = true;
                    }
                }
                else
                {
                    //CLIENTE JURIDICO
                    datosCliente = clConsultas.obtenerDatosClienteNitJuridico(Convert.ToInt32(txtNumIDCliente.Text), txtDvIdCliente.Text.Trim());

                    if (datosCliente.Rows.Count == 1)
                    {
                        string codCli = datosCliente.Rows[0]["CODCLI"].ToString();
                        direccionesCliente = clConsultas.obtenerDireccionesCliente(codCli);
                        telefonosCliente = clConsultas.obtenerTelefonosCliente(codCli);

                        if (datosCliente.Rows[0]["TIPOCLI"].ToString() == "E")
                        {
                            pnlDatosClienteExistente.Visible = true;
                            pnlClienteIndividualExistente.Visible = false;

                            pnlDatosClienteNuevo.Visible = false;
                            pnlClienteJuridicoExistente.Visible = true;

                            llenarConrolesAsegTitularJuridico(datosCliente, telefonosCliente, direccionesCliente);
                        }
                    }
                    else
                    {
                        //limpiarControles();
                        mostrarMensaje("No se encontro información del cliente. Se habilitarán los campos para ingresar un nuevo cliente Jurídico.");
                        pnlDatosClienteExistente.Visible = false;
                        pnlDatosClienteNuevo.Visible = true;
                        pnlNuevoRespPagoJuridico.Visible = true;
                        pnlNuevoRespPagoIndividual.Visible = false;
                    }
                }
            }
            else
            {
                mostrarMensaje("El NIT ingresado es invalido.");
                pnlDatosClienteExistente.Visible = false;
                pnlDatosClienteNuevo.Visible = false;
            }
        }
        private void llenarControlesAsegTitularIndividual(DataTable datosCliente, DataTable telefonosCliente, DataTable direccionesCliente)
        {
            txtNombreIndividualExistente.Text = datosCliente.Rows[0]["NOMBRE"].ToString();

            if (datosCliente.Rows[0]["FECNAC"].ToString() == string.Empty)
            {
                txtFechaNacimientoIndividualExistente.Text = string.Empty;
            }
            else
            {
                DateTime fechaNacimiento = Convert.ToDateTime(datosCliente.Rows[0]["FECNAC"].ToString());
                txtFechaNacimientoIndividualExistente.Text = fechaNacimiento.ToString("dd-MM-yyyy");
            }
            
            cmbGeneroIndividualExistente.SelectedValue = datosCliente.Rows[0]["CODGENERO"].ToString();
            cmbEstadoCivilIndividualExistente.SelectedValue = datosCliente.Rows[0]["CODESTADOCIVIL"].ToString();
            cmbProfesionIndividualExistente.SelectedValue = datosCliente.Rows[0]["CODPROFESION"].ToString();
            cmbNacionalidadIndividualExistente.SelectedValue = datosCliente.Rows[0]["CODNACIONALIDAD"].ToString();
            cmbTipoIdentificacionIndividualExistente.SelectedValue = datosCliente.Rows[0]["CODTIPOIDENT"].ToString();
            txtNumeroIdentificacionIndividualExistente.Text = datosCliente.Rows[0]["NUMIDENT"].ToString();
            cmbPaisEmisionIndividualExistente.SelectedValue = datosCliente.Rows[0]["CODPAISEMISION"].ToString();
            cmbDeptoEmisionIndividualExistente.SelectedValue = datosCliente.Rows[0]["CODESTADOEMISION"].ToString();
            cmbMuniEmisionIndividualExistente.SelectedValue = datosCliente.Rows[0]["CODCIUDADEMISION"].ToString();
            txtNumIdIndividualExistente.Text = txtNumIDCliente.Text;
            txtDvIdIndividualExistente.Text = txtDvIdCliente.Text;
            txtCorreoIndividualExistente.Text = datosCliente.Rows[0]["CORREO"].ToString();
            if (txtCorreoIndividualExistente.Text == string.Empty)
                txtCorreoIndividualExistente.Enabled = true;

            if (datosCliente.Rows[0]["ESPEP"].ToString() == "S")
                chkEsPepIndividualExistente.Checked = true;
            else
                chkEsPepIndividualExistente.Checked = false;

            if (datosCliente.Rows[0]["RELACIONPEP"].ToString() == "S")
                chkRelPepIndividualExistente.Checked = true;
            else
                chkRelPepIndividualExistente.Checked = false;

            if (datosCliente.Rows[0]["ASOCIADOPEP"].ToString() == "S")
                chkAsociadoPepIndividualExistente.Checked = true;
            else
                chkAsociadoPepIndividualExistente.Checked = false;

            grvTelefonosIndividualExistente.DataSource = telefonosCliente;
            grvTelefonosIndividualExistente.DataBind();

            grvDireccionesIndividualExistente.DataSource = direccionesCliente;
            grvDireccionesIndividualExistente.DataBind();
        }
        private void llenarConrolesAsegTitularJuridico(DataTable datosCliente, DataTable telefonosCliente, DataTable direccionesCliente)
        {
            txtNombrePersonaJuridicaJuridicoExistente.Text = datosCliente.Rows[0]["NOMBRE_PERSONA_JURIDICA"].ToString();
            txtNumIdPersonaJuridicaJuridicoExistente.Text = txtNumIDCliente.Text;
            txtDvIdPersonaJuridicaJuridicoExistente.Text = txtDvIdCliente.Text;
            //txtNombreRepLegalJuridicoExistente.Text = datosCliente.Rows[0]["NOMBRE_REP_LEGAL"].ToString();
            //txtNumIdRepLegalJuridicoExistente.Text = datosCliente.Rows[0]["NIT_REP_LEGAL"].ToString().Split('-')[0];
            //txtDvIdRepLegalJuridicoExistente.Text = datosCliente.Rows[0]["NIT_REP_LEGAL"].ToString().Split('-')[1];
            //txtDvIdRepLegalJuridicoExistente.Text = datosCliente.Rows[0]["NOMBRE_REP_LEGAL"].ToString();
            if(datosCliente.Rows[0]["FECHA_CONSTITUCION"].ToString()==string.Empty)
            {
                txtFechaConstitucionJuridicoExistente.Text = "";
            }
            else
            {
                DateTime fechaConstitucion = Convert.ToDateTime(datosCliente.Rows[0]["FECHA_CONSTITUCION"].ToString());
                txtFechaConstitucionJuridicoExistente.Text = fechaConstitucion.ToString("dd-MM-yyyy");
            }

            if (datosCliente.Rows[0]["PAIS_EMPRESA"].ToString() == "NO EXISTENTE")
            {
                cmbOrigenEmpresaJuridicoExistente.DataSource = null;
                cmbOrigenEmpresaJuridicoExistente.DataBind();
            }
            else
            {
                cmbOrigenEmpresaJuridicoExistente.SelectedValue = datosCliente.Rows[0]["PAIS_EMPRESA"].ToString();
            }

            txtActividadEconomicaJuridicoExistente.Text = datosCliente.Rows[0]["ACTIVIDAD_EMPRESA"].ToString();

            if (datosCliente.Rows[0]["ESPEP"].ToString() == "S")
            {
                chkEsPepJuridicoExistente.Checked = true;
            }
            else
            {
                chkEsPepJuridicoExistente.Checked = false;
            }

            if (datosCliente.Rows[0]["RELACIONPEP"].ToString() == "S")
            {
                chkRelPepJuridicoExistente.Checked = true;
            }
            else
            {
                chkRelPepJuridicoExistente.Checked = false;
            }

            if (datosCliente.Rows[0]["ASOCIADOPEP"].ToString() == "S")
            {
                chkAsociadoPepJuridicoExistente.Checked = true;
            }
            else
            {
                chkAsociadoPepJuridicoExistente.Checked = false;
            }

            txtCorreoJuridicoExistente.Text = datosCliente.Rows[0]["TELEX"].ToString();
            if (txtCorreoJuridicoExistente.Text == string.Empty)
                txtCorreoJuridicoExistente.Enabled = true;
            grvTelefonosJuridicoExistente.DataSource = telefonosCliente;
            grvTelefonosJuridicoExistente.DataBind();
            grvDireccionesJuridicoExistente.DataSource = direccionesCliente;
            grvDireccionesJuridicoExistente.DataBind();

        }
        private void llenarCombos()
        {
            datosPais = new DataTable();
            datosDepartamentos = new DataTable();
            datosMunicipios = new DataTable();
            datosZonas = new DataTable();
            datosNacionalidades = new DataTable();
            datosProfesiones = new DataTable();
            datosEstadoCivil = new DataTable();
            datosGeneros = new DataTable();

            datosTelefonos = new DataTable();
            datosTelefonos.Columns.Add("Número", typeof(string));

            Consultas clVarios = new Consultas();

            datosPais = clVarios.obtenerPaises();
            datosDepartamentos = clVarios.obtenerDepartamentos();
            datosMunicipios = clVarios.obtenerMunicipios();
            datosZonas = clVarios.obtenerZonas();
            datosNacionalidades = clVarios.obtenerNacionalidades();
            datosProfesiones = clVarios.obtenerProfesiones();
            datosEstadoCivil = clVarios.obtenerEstadosCiviles();
            datosGeneros = clVarios.obtenerGeneros();

            ViewState.Add("datosPais", datosPais);
            ViewState.Add("datosDepartamentos", datosDepartamentos);
            ViewState.Add("datosMunicipios", datosMunicipios);
            ViewState.Add("datosZonas", datosZonas);
            ViewState.Add("datosTelefonos", datosTelefonos);

            //--------------------------------------------PAISES-----------------------------------------------------------------------

            #region PAISES
            //Cliente Individual Existente
            cmbPaisEmisionIndividualExistente.DataSource = datosPais;
            cmbPaisEmisionIndividualExistente.DataTextField = "DESCPAIS";
            cmbPaisEmisionIndividualExistente.DataValueField = "CODPAIS";
            cmbPaisEmisionIndividualExistente.DataBind();
            cmbPaisEmisionIndividualExistente.SelectedValue = "001";

            //Cliente Individual Nuevo Responsable Pago
            cmbPaisEmisionNuevoResPagoIndividual.DataSource = datosPais;
            cmbPaisEmisionNuevoResPagoIndividual.DataTextField = "DESCPAIS";
            cmbPaisEmisionNuevoResPagoIndividual.DataValueField = "CODPAIS";
            cmbPaisEmisionNuevoResPagoIndividual.DataBind();
            cmbPaisEmisionNuevoResPagoIndividual.SelectedValue = "001";

            cmbPaisEmisionNuevoResPagoIndividual.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbPaisEmisionNuevoResPagoIndividual.SelectedIndex = 0;

            cmbPaisDireccionNuevoResPagoIndividual.DataSource = datosPais;
            cmbPaisDireccionNuevoResPagoIndividual.DataTextField = "DESCPAIS";
            cmbPaisDireccionNuevoResPagoIndividual.DataValueField = "CODPAIS";
            cmbPaisDireccionNuevoResPagoIndividual.DataBind();
            cmbPaisDireccionNuevoResPagoIndividual.SelectedValue = "001";

            cmbPaisDireccionNuevoResPagoIndividual.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbPaisDireccionNuevoResPagoIndividual.SelectedIndex = 0;

            //Cliente Juridico Nuevo Responsable Pago
            cmbPaisEmisionNuevoRespPagoJuridico.DataSource = datosPais;
            cmbPaisEmisionNuevoRespPagoJuridico.DataTextField = "DESCPAIS";
            cmbPaisEmisionNuevoRespPagoJuridico.DataValueField = "CODPAIS";
            cmbPaisEmisionNuevoRespPagoJuridico.DataBind();
            cmbPaisEmisionNuevoRespPagoJuridico.SelectedValue = "001";

            cmbPaisEmisionNuevoRespPagoJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbPaisEmisionNuevoRespPagoJuridico.SelectedIndex = 0;

            cmbPaisDireccionNuevoRespPagoJuridico.DataSource = datosPais;
            cmbPaisDireccionNuevoRespPagoJuridico.DataTextField = "DESCPAIS";
            cmbPaisDireccionNuevoRespPagoJuridico.DataValueField = "CODPAIS";
            cmbPaisDireccionNuevoRespPagoJuridico.DataBind();
            cmbPaisDireccionNuevoRespPagoJuridico.SelectedValue = "001";

            cmbPaisDireccionNuevoRespPagoJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbPaisDireccionNuevoRespPagoJuridico.SelectedIndex = 0;

            #endregion

            //--------------------------------------------DEPARTAMENTOS-----------------------------------------------------------------------

            #region DEPARTAMENTOS            

            cmbDeptoEmisionIndividualExistente.DataSource = datosDepartamentos;
            cmbDeptoEmisionIndividualExistente.DataTextField = "DESCESTADO";
            cmbDeptoEmisionIndividualExistente.DataValueField = "CODESTADO";
            cmbDeptoEmisionIndividualExistente.DataBind();
            cmbDeptoEmisionIndividualExistente.SelectedValue = "001";

            //Cliente Individual Nuevo Responsable Pago
            cmbDeptoEmisionNuevoResPagoIndividual.DataSource = datosDepartamentos;
            cmbDeptoEmisionNuevoResPagoIndividual.DataTextField = "DESCESTADO";
            cmbDeptoEmisionNuevoResPagoIndividual.DataValueField = "CODESTADO";
            cmbDeptoEmisionNuevoResPagoIndividual.DataBind();
            cmbDeptoEmisionNuevoResPagoIndividual.SelectedValue = "001";

            cmbDeptoEmisionNuevoResPagoIndividual.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbDeptoEmisionNuevoResPagoIndividual.SelectedIndex = 0;

            cmbDeptoDireccionNuevoResPagoIndividual.DataSource = datosDepartamentos;
            cmbDeptoDireccionNuevoResPagoIndividual.DataTextField = "DESCESTADO";
            cmbDeptoDireccionNuevoResPagoIndividual.DataValueField = "CODESTADO";
            cmbDeptoDireccionNuevoResPagoIndividual.DataBind();
            cmbDeptoDireccionNuevoResPagoIndividual.SelectedValue = "001";

            cmbDeptoDireccionNuevoResPagoIndividual.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbDeptoDireccionNuevoResPagoIndividual.SelectedIndex = 0;

            //Cliente Juridico Nuevo Responsable Pago
            cmbDeptoEmisionNuevoRespPagoJuridico.DataSource = datosDepartamentos;
            cmbDeptoEmisionNuevoRespPagoJuridico.DataTextField = "DESCESTADO";
            cmbDeptoEmisionNuevoRespPagoJuridico.DataValueField = "CODESTADO";
            cmbDeptoEmisionNuevoRespPagoJuridico.DataBind();
            cmbDeptoEmisionNuevoRespPagoJuridico.SelectedValue = "001";

            cmbDeptoEmisionNuevoRespPagoJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbDeptoEmisionNuevoRespPagoJuridico.SelectedIndex = 0;

            cmbDeptoDireccionNuevoRespPagoJuridico.DataSource = datosDepartamentos;
            cmbDeptoDireccionNuevoRespPagoJuridico.DataTextField = "DESCESTADO";
            cmbDeptoDireccionNuevoRespPagoJuridico.DataValueField = "CODESTADO";
            cmbDeptoDireccionNuevoRespPagoJuridico.DataBind();
            cmbDeptoDireccionNuevoRespPagoJuridico.SelectedValue = "001";

            cmbDeptoDireccionNuevoRespPagoJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbDeptoDireccionNuevoRespPagoJuridico.SelectedIndex = 0;

            #endregion

            //--------------------------------------------MUNICIPIOS-----------------------------------------------------------------------

            #region MUNICIPIOS

            //Cliente Individual Existente
            cmbMuniEmisionIndividualExistente.DataSource = datosMunicipios;
            cmbMuniEmisionIndividualExistente.DataTextField = "DESCCIUDAD";
            cmbMuniEmisionIndividualExistente.DataValueField = "CODCIUDAD";
            cmbMuniEmisionIndividualExistente.DataBind();
            cmbMuniEmisionIndividualExistente.SelectedValue = "001";

            //Cliente Individual Nuevo Responsable Pago
            cmbMuniEmisionNuevoResPagoIndividual.DataSource = datosMunicipios;
            cmbMuniEmisionNuevoResPagoIndividual.DataTextField = "DESCCIUDAD";
            cmbMuniEmisionNuevoResPagoIndividual.DataValueField = "CODCIUDAD";
            cmbMuniEmisionNuevoResPagoIndividual.DataBind();
            cmbMuniEmisionNuevoResPagoIndividual.SelectedValue = "001";

            cmbMuniEmisionNuevoResPagoIndividual.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbMuniEmisionNuevoResPagoIndividual.SelectedIndex = 0;

            cmbMuniDireccionNuevoResPagoIndividual.DataSource = datosMunicipios;
            cmbMuniDireccionNuevoResPagoIndividual.DataTextField = "DESCCIUDAD";
            cmbMuniDireccionNuevoResPagoIndividual.DataValueField = "CODCIUDAD";
            cmbMuniDireccionNuevoResPagoIndividual.DataBind();
            cmbMuniDireccionNuevoResPagoIndividual.SelectedValue = "001";

            cmbMuniDireccionNuevoResPagoIndividual.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbMuniDireccionNuevoResPagoIndividual.SelectedIndex = 0;

            //Cliente Juridico Nuevo Responsable Pago
            cmbMuniEmisionNuevoRespPagoJuridico.DataSource = datosMunicipios;
            cmbMuniEmisionNuevoRespPagoJuridico.DataTextField = "DESCCIUDAD";
            cmbMuniEmisionNuevoRespPagoJuridico.DataValueField = "CODCIUDAD";
            cmbMuniEmisionNuevoRespPagoJuridico.DataBind();
            cmbMuniEmisionNuevoRespPagoJuridico.SelectedValue = "001";

            cmbMuniEmisionNuevoRespPagoJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbMuniEmisionNuevoRespPagoJuridico.SelectedIndex = 0;

            cmbMuniDireccionNuevoRespPagoJuridico.DataSource = datosMunicipios;
            cmbMuniDireccionNuevoRespPagoJuridico.DataTextField = "DESCCIUDAD";
            cmbMuniDireccionNuevoRespPagoJuridico.DataValueField = "CODCIUDAD";
            cmbMuniDireccionNuevoRespPagoJuridico.DataBind();
            cmbMuniDireccionNuevoRespPagoJuridico.SelectedValue = "001";

            cmbMuniDireccionNuevoRespPagoJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbMuniDireccionNuevoRespPagoJuridico.SelectedIndex = 0;

            #endregion

            //--------------------------------------------ZONAS-----------------------------------------------------------------------
            #region ZONAS
            cmbZonaDireccionNuevoRespPagoIndividual.DataSource = datosZonas;
            cmbZonaDireccionNuevoRespPagoIndividual.DataTextField = "DESCMUNICIPIO";
            cmbZonaDireccionNuevoRespPagoIndividual.DataValueField = "CODMUNICIPIO";
            cmbZonaDireccionNuevoRespPagoIndividual.DataBind();

            cmbZonaDireccionNuevoRespPagoIndividual.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbZonaDireccionNuevoRespPagoIndividual.SelectedIndex = 0;

            cmbZonaDireccionNuevoRespPagoJuridico.DataSource = datosZonas;
            cmbZonaDireccionNuevoRespPagoJuridico.DataTextField = "DESCMUNICIPIO";
            cmbZonaDireccionNuevoRespPagoJuridico.DataValueField = "CODMUNICIPIO";
            cmbZonaDireccionNuevoRespPagoJuridico.DataBind();

            cmbZonaDireccionNuevoRespPagoJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbZonaDireccionNuevoRespPagoJuridico.SelectedIndex = 0;
            #endregion

            //--------------------------------------------NACIONALIDADES-----------------------------------------------------------------------

            #region NACIONALIDADES
            //Cliente Individual Existente
            cmbNacionalidadIndividualExistente.DataSource = datosNacionalidades;
            cmbNacionalidadIndividualExistente.DataTextField = "DESCRIP";
            cmbNacionalidadIndividualExistente.DataValueField = "CODLVAL";
            cmbNacionalidadIndividualExistente.DataBind();
            cmbNacionalidadIndividualExistente.SelectedValue = "000";

            //Cliente Juridico Existente
            cmbOrigenEmpresaJuridicoExistente.DataSource = datosPais;
            cmbOrigenEmpresaJuridicoExistente.DataTextField = "DESCPAIS";
            cmbOrigenEmpresaJuridicoExistente.DataValueField = "CODPAIS";
            cmbOrigenEmpresaJuridicoExistente.DataBind();
            cmbOrigenEmpresaJuridicoExistente.SelectedValue = "001";

            //Cliente Individual Nuevo Responsable Pago
            cmbNacionalidadNuevoResPagoIndividual.DataSource = datosNacionalidades;
            cmbNacionalidadNuevoResPagoIndividual.DataTextField = "DESCRIP";
            cmbNacionalidadNuevoResPagoIndividual.DataValueField = "CODLVAL";
            cmbNacionalidadNuevoResPagoIndividual.DataBind();
            cmbNacionalidadNuevoResPagoIndividual.SelectedValue = "000";

            //Cliente Juridico Nuevo Responsable Pago
            cmbOrigenEmpresaNuevoRespPagoJuridico.DataSource = datosPais;
            cmbOrigenEmpresaNuevoRespPagoJuridico.DataTextField = "DESCPAIS";
            cmbOrigenEmpresaNuevoRespPagoJuridico.DataValueField = "CODPAIS";
            cmbOrigenEmpresaNuevoRespPagoJuridico.DataBind();
            cmbOrigenEmpresaNuevoRespPagoJuridico.SelectedValue = "001";

            #endregion

            //--------------------------------------------PROFESIONES-----------------------------------------------------------------------

            #region PROFESIONES
            //Cliente Individual Existente
            cmbProfesionIndividualExistente.DataSource = datosProfesiones;
            cmbProfesionIndividualExistente.DataTextField = "DESCRIP";
            cmbProfesionIndividualExistente.DataValueField = "CODLVAL";
            cmbProfesionIndividualExistente.DataBind();

            //Cliente Individual Nuevo Responsable Pago
            cmbProfesionNuevoResPagoIndividual.DataSource = datosProfesiones;
            cmbProfesionNuevoResPagoIndividual.DataTextField = "DESCRIP";
            cmbProfesionNuevoResPagoIndividual.DataValueField = "CODLVAL";
            cmbProfesionNuevoResPagoIndividual.DataBind();

            #endregion

            //--------------------------------------------ESTADOS CIVILES-----------------------------------------------------------------------

            #region ESTADOS_CIVILES
            //Cliente Individual Existente   
            cmbEstadoCivilIndividualExistente.DataSource = datosEstadoCivil;
            cmbEstadoCivilIndividualExistente.DataTextField = "DESCRIP";
            cmbEstadoCivilIndividualExistente.DataValueField = "CODLVAL";
            cmbEstadoCivilIndividualExistente.DataBind();

            //Cliente Individual Nuevo Responsable Pago
            cmbEstadoCivilNuevoResPagoIndividual.DataSource = datosEstadoCivil;
            cmbEstadoCivilNuevoResPagoIndividual.DataTextField = "DESCRIP";
            cmbEstadoCivilNuevoResPagoIndividual.DataValueField = "CODLVAL";
            cmbEstadoCivilNuevoResPagoIndividual.DataBind();
            #endregion

            //--------------------------------------------GENEROS-----------------------------------------------------------------------

            #region GENEROS
            //Cliente Individual Existente            
            cmbGeneroIndividualExistente.DataSource = datosGeneros;
            cmbGeneroIndividualExistente.DataTextField = "DESCRIP";
            cmbGeneroIndividualExistente.DataValueField = "CODLVAL";
            cmbGeneroIndividualExistente.DataBind();

            //Cliente Individual Nuevo Responsable Pago
            cmbGeneroNuevoResPagoIndividual.DataSource = datosGeneros;
            cmbGeneroNuevoResPagoIndividual.DataTextField = "DESCRIP";
            cmbGeneroNuevoResPagoIndividual.DataValueField = "CODLVAL";
            cmbGeneroNuevoResPagoIndividual.DataBind();
            #endregion
        }
        protected void btnGuardarNuevoResPagoIndividual_Click(object sender, EventArgs e)
        {
            guardarAseguradoIndividual();
        }
        protected void btnGuardarNuevoRespPagoJuridico_Click(object sender, EventArgs e)
        {
            guardarAseguradoJuridico();
        }
        protected void btnGuardarClienteIndividualExistente_Click(object sender, EventArgs e)
        {
            if (txtCorreoIndividualExistente.Text == string.Empty)
            {
                mostrarMensaje("El correo es un campo obligatorio. Ingrese un correo válido.");
                txtCorreoIndividualExistente.Focus();
                return;
            }
            guardarAseguradoIndividualExistente();
        }
        protected void btnGuardarJuridicoExistente_Click(object sender, EventArgs e)
        {
            if (txtCorreoJuridicoExistente.Text == string.Empty)
            {
                mostrarMensaje("El correo es un campo obligatorio. Ingrese un correo válido.");
                txtCorreoJuridicoExistente.Focus();
                return;
            }
            guardarAseguradoJuridicoExistente();
        }
        private void guardarAseguradoIndividual()
        {
            Operaciones operacionesCliente = new Operaciones();
            EstructuraClienteIndividual nuevoCliente = new EstructuraClienteIndividual();
            DataTable telefonos = new DataTable();
            telefonos.Columns.Add("Telefono", typeof(string));

            //validacion de campos obligatorios

            if (txtPrimerNombreNuevoResPagoIndividual.Text == string.Empty)
            {
                mostrarMensaje("El campo Primer Nombre es obligatorio.");
                txtPrimerNombreNuevoResPagoIndividual.Focus();
                return;
            }

            if (txtPrimerApellidoNuevoResPagoIndividual.Text == string.Empty)
            {
                mostrarMensaje("El campo Primer Apellido es obligatorio.");
                txtPrimerApellidoNuevoResPagoIndividual.Focus();
                return;
            }           

            if (txtFechaNacimientoNuevoResPagoIndividual.Text == string.Empty)
            {
                mostrarMensaje("El campo Fecha Nacimiento es obligatorio.");
                txtFechaNacimientoNuevoResPagoIndividual.Focus();
                return;
            }

            if (txtNumeroIdentificacionNuevoResPagoIndividual.Text == string.Empty)
            {
                mostrarMensaje("El campo Número Identificación es obligatorio.");
                txtNumeroIdentificacionNuevoResPagoIndividual.Focus();
                return;
            }

            if (txtCorreoNuevoResPagoIndividual.Text == string.Empty)
            {
                mostrarMensaje("El campo Correo Electrónico es obligatorio.");
                txtCorreoNuevoResPagoIndividual.Focus();
                return;
            }

            if (grvTelefonosNuevoResPagoIndividual.Rows.Count == 0)
            {
                mostrarMensaje("Debe agregar al menos un número de teléfono.");
                txtTelefonoNuevoResPagoIndividual.Focus();
                return;
            }

            //if (!chkEsPepIndividualExistente.Checked && !chkRelPepIndividualExistente.Checked && !chkAsociadoPepIndividualExistente.Checked && !chkNoPEP.Checked)
            //{
            //    mostrarMensaje("Debe de seleccionar al menos una opcion para Persona Expuesta Políticamente");
            //    return;
            //}

            nuevoCliente.primer_nombre_individual = txtPrimerNombreNuevoResPagoIndividual.Text;
            nuevoCliente.segundo_nombre_individual = txtSegundoNombreNuevoResPagoIndividual.Text;
            nuevoCliente.primer_apellido_individual = txtPrimerApellidoNuevoResPagoIndividual.Text;
            nuevoCliente.segundo_apellido_individual = txtSegundoApellidoNuevoResPagoIndividual.Text;

            //formateo de fecha para insertar en mysql
            DateTime fechaNacimientoIndividual = Convert.ToDateTime(txtFechaNacimientoNuevoResPagoIndividual.Text);
            nuevoCliente.fecha_nacimiento_individual = fechaNacimientoIndividual.ToString("yyyy-MM-dd");

            nuevoCliente.genero_individual = cmbGeneroNuevoResPagoIndividual.SelectedValue;
            nuevoCliente.estado_civil_individual = cmbEstadoCivilNuevoResPagoIndividual.SelectedValue;
            nuevoCliente.profesion_individual = cmbProfesionNuevoResPagoIndividual.SelectedValue;
            nuevoCliente.nacionalidad_individual = cmbNacionalidadNuevoResPagoIndividual.SelectedValue;
            nuevoCliente.tipo_identificacion_individual = cmbTipoIdentificacionNuevoResPagoIndividual.SelectedValue;
            nuevoCliente.numero_identificacion_individual = txtNumeroIdentificacionNuevoResPagoIndividual.Text;
            nuevoCliente.pais_emision_individual = cmbPaisEmisionNuevoResPagoIndividual.SelectedValue;
            nuevoCliente.depto_emision_individual = cmbDeptoEmisionNuevoResPagoIndividual.SelectedValue;
            nuevoCliente.muni_emision_individual = cmbMuniEmisionNuevoResPagoIndividual.SelectedValue;
            nuevoCliente.nombre_negocio_individual = txtNegocioPropioNuevoResPagoIndividual.Text;
            nuevoCliente.nit_individual = txtNumIdNuevoResPagoIndividual.Text.Trim() + txtDvIdNuevoResPagoIndividual.Text.Trim();

            if (chkActuaNombrePropioNuevoResPagoIndividual.Checked)
                nuevoCliente.actua_nombre_propio_individual = "S";
            else
                nuevoCliente.actua_nombre_propio_individual = "N";

            nuevoCliente.direccion_individual = unificarDireccionIndividual();
            nuevoCliente.pais_direccion_individual = cmbPaisDireccionNuevoResPagoIndividual.SelectedValue;
            nuevoCliente.depto_direccion_individual = cmbDeptoDireccionNuevoResPagoIndividual.SelectedValue;
            nuevoCliente.muni_direccion_individual = cmbMuniDireccionNuevoResPagoIndividual.SelectedValue;
            nuevoCliente.zona_direccion_individual = cmbZonaDireccionNuevoRespPagoIndividual.SelectedValue;
            nuevoCliente.calle_direccion_individual = txtCalleDireccionNuevoRespPagoIndividual.Text;
            nuevoCliente.avenida_direccion_individual = txtAvenidaDireccionNuevoRespPagoIndividual.Text;
            nuevoCliente.numero_casa_direccion_individual = txtNumCasaDireccionNuevoRespPagoIndividual.Text;
            nuevoCliente.colonia_direccion_individual = txtColoniaDireccionNuevoRespPagoIndividual.Text;
            nuevoCliente.edificio_direccion_individual = txtEdificioDireccionNuevoRespPagoIndividual.Text;
            nuevoCliente.lote_direccion_individual = txtLoteDireccionNuevoRespPagoIndividual.Text;
            nuevoCliente.manzana_direccion_individual = txtManzanaDireccionNuevoRespPagoIndividual.Text;
            nuevoCliente.sector_direccion_individual = txtSectorDireccionNuevoRespPagoIndividual.Text;

            if (chkEsPepNuevoResPagoIndividual.Checked)
                nuevoCliente.esPep_individual = "S";
            else
                nuevoCliente.esPep_individual = "N";

            if (chkRelPepNuevoResPagoIndividual.Checked)
                nuevoCliente.relacionPep_individual = "S";
            else
                nuevoCliente.relacionPep_individual = "N";

            if (chkAsociadoPepNuevoResPagoIndividual.Checked)
                nuevoCliente.asociadoPep_individual = "S";
            else
                nuevoCliente.asociadoPep_individual = "N";

            nuevoCliente.correo_electronico_individual = txtCorreoNuevoResPagoIndividual.Text;

            for (int i = 0; i < grvTelefonosNuevoResPagoIndividual.Rows.Count; i++)
            {
                telefonos.Rows.Add(grvTelefonosNuevoResPagoIndividual.Rows[i].Cells[0].Text);
            }

            operacionesCliente.guardarDatosClienteIndividual(idCotizacion, "RESP", cmbTipoCliente.SelectedValue, nuevoCliente, telefonos);
            operacionesCliente.actualizarEstadoCotizacion(idCotizacion);
            redireccionarPaginaBien();
        }
        private void guardarAseguradoIndividualExistente()
        {
            Operaciones operacionesCliente = new Operaciones();
            EstructuraClienteIndividual nuevoCliente = new EstructuraClienteIndividual();
            DataTable telefonos = new DataTable();
            telefonos.Columns.Add("Telefono", typeof(string));


            string primerNombre = string.Empty;
            string segundoNombre = string.Empty;
            string primerApellido = string.Empty;
            string segundoApellido = string.Empty;

            String[] nombreCompleto = txtNombreIndividualExistente.Text.Split(' ');

            if (nombreCompleto.Length == 1)
            {
                primerNombre = nombreCompleto[0].ToString();
            }

            if (nombreCompleto.Length == 2)
            {
                primerNombre = nombreCompleto[0].ToString();
                segundoNombre = nombreCompleto[1].ToString();
            }

            if (nombreCompleto.Length == 3)
            {
                primerNombre = nombreCompleto[0].ToString();
                segundoNombre = nombreCompleto[1].ToString();
                primerApellido = nombreCompleto[2].ToString();
            }

            if (nombreCompleto.Length == 4)
            {
                primerNombre = nombreCompleto[0].ToString();
                segundoNombre = nombreCompleto[1].ToString();
                primerApellido = nombreCompleto[2].ToString();
                segundoApellido = nombreCompleto[3].ToString();
            }

            if (nombreCompleto.Length > 4)
            {
                primerNombre = nombreCompleto[0].ToString();
                segundoNombre = nombreCompleto[1].ToString();
                primerApellido = nombreCompleto[2].ToString();

                for (int i = 3; i < nombreCompleto.Length; i++)
                {
                    segundoApellido += nombreCompleto[i].ToString() + " ";
                }
            }

            //if (!chkEsPepIndividualExistente.Checked && !chkRelPepIndividualExistente.Checked && !chkAsociadoPepIndividualExistente.Checked && !chkNoPEP.Checked)
            //{
            //    mostrarMensaje("Debe de seleccionar al menos una opcion para Persona Expuesta Políticamente");
            //    return;
            //}

            nuevoCliente.primer_nombre_individual = primerNombre;
            nuevoCliente.segundo_nombre_individual = segundoNombre;
            nuevoCliente.primer_apellido_individual = primerApellido;
            nuevoCliente.segundo_apellido_individual = segundoApellido;

            //formateo de fecha para insertar en mysql
            DateTime fechaNacimientoIndividual = Convert.ToDateTime(txtFechaNacimientoIndividualExistente.Text);
            nuevoCliente.fecha_nacimiento_individual = fechaNacimientoIndividual.ToString("yyyy-MM-dd");
            nuevoCliente.genero_individual = cmbGeneroIndividualExistente.SelectedValue;
            nuevoCliente.estado_civil_individual = cmbEstadoCivilIndividualExistente.SelectedValue;
            nuevoCliente.profesion_individual = cmbProfesionIndividualExistente.SelectedValue;
            nuevoCliente.nacionalidad_individual = cmbNacionalidadIndividualExistente.SelectedValue;
            nuevoCliente.tipo_identificacion_individual = cmbTipoIdentificacionIndividualExistente.SelectedValue;
            nuevoCliente.numero_identificacion_individual = txtNumeroIdentificacionIndividualExistente.Text;
            nuevoCliente.pais_emision_individual = cmbPaisEmisionIndividualExistente.SelectedValue;
            nuevoCliente.depto_emision_individual = cmbDeptoEmisionIndividualExistente.SelectedValue;
            nuevoCliente.muni_emision_individual = cmbMuniEmisionIndividualExistente.SelectedValue;
            nuevoCliente.nombre_negocio_individual = txtNegocioPropioIndividualExistente.Text;
            nuevoCliente.nit_individual = txtNumIdIndividualExistente.Text.Trim()  + txtDvIdIndividualExistente.Text.Trim();

            if (chkActuaNombrePropioIndividualExistente.Checked)
                nuevoCliente.actua_nombre_propio_individual = "S";
            else
                nuevoCliente.actua_nombre_propio_individual = "N";

            if (chkEsPepIndividualExistente.Checked)
                nuevoCliente.esPep_individual = "S";
            else
                nuevoCliente.esPep_individual = "N";

            if (chkRelPepIndividualExistente.Checked)
                nuevoCliente.relacionPep_individual = "S";
            else
                nuevoCliente.relacionPep_individual = "N";

            if (chkAsociadoPepIndividualExistente.Checked)
                nuevoCliente.asociadoPep_individual = "S";
            else
                nuevoCliente.asociadoPep_individual = "N";

            nuevoCliente.correo_electronico_individual = txtCorreoIndividualExistente.Text;
            
            for (int i = 0; i < grvTelefonosIndividualExistente.Rows.Count; i++)
            {
                telefonos.Rows.Add(grvTelefonosIndividualExistente.Rows[i].Cells[0].Text);
            }

            if (grvDireccionesIndividualExistente.Rows.Count > 0)
            {
                nuevoCliente.direccion_individual = grvDireccionesIndividualExistente.Rows[0].Cells[0].Text;
            }
            else
            {
                nuevoCliente.direccion_individual = "N/A";
            }

            operacionesCliente.guardarDatosClienteIndividual(idCotizacion, "RESP", cmbTipoCliente.SelectedValue, nuevoCliente, telefonos);
            operacionesCliente.actualizarEstadoCotizacion(idCotizacion);
            redireccionarPaginaBien();            
        }
        private void guardarAseguradoJuridico()
        {
            Operaciones operacionesCliente = new Operaciones();
            EstructuraClienteJuridico nuevoCliente = new EstructuraClienteJuridico();
            DataTable telefonos = new DataTable();
            telefonos.Columns.Add("Telefono", typeof(string));

            //validacion de campos obligatorios

            if (txtNombreNuevoRespPagoJuridico.Text == string.Empty)
            {
                mostrarMensaje("El campo Nombre Jurídico es obligatorio.");
                txtNombreNuevoRespPagoJuridico.Focus();
                return;
            }

            if (txtNumIdNuevoRespPagoJuridico.Text == string.Empty || txtDvIdNuevoRespPagoJuridico.Text == string.Empty)
            {
                mostrarMensaje("Los campos del nit son obligatorios.");
                txtNumIdNuevoRespPagoJuridico.Focus();
                return;
            }

            //if (txtPrimerNombreNuevoRespPagoJuridico.Text == string.Empty)
            //{
            //    mostrarMensaje("El campo Primer Nombre es obligatorio.");
            //    txtPrimerNombreNuevoRespPagoJuridico.Focus();
            //    return;
            //}

            //if (txtPrimerApellidoNuevoRespPagoJuridico.Text == string.Empty)
            //{
            //    mostrarMensaje("El campo Primer Apellido es obligatorio.");
            //    txtPrimerApellidoNuevoRespPagoJuridico.Focus();
            //    return;
            //}            

            //if (txtNumeroIdentificacionNuevoRespPagoJuridico.Text == string.Empty)
            //{
            //    mostrarMensaje("El campo Numero Identificación es obligatorio.");
            //    txtNumeroIdentificacionNuevoRespPagoJuridico.Focus();
            //    return;
            //}

            if (txtFechaConstitucionNuevoRespPagoJuridico.Text == string.Empty)
            {
                mostrarMensaje("El campo Fecha Constitución es obligatorio.");
                txtFechaConstitucionNuevoRespPagoJuridico.Focus();
                return;
            }

            if (txtActividadEconomicaNuevoRespPagoJuridico.Text == string.Empty)
            {
                mostrarMensaje("El campo Actvidad Económica es obligatorio.");
                txtActividadEconomicaNuevoRespPagoJuridico.Focus();
                return;
            }

            if (txtCorreoNuevoRespPagoJuridico.Text == string.Empty)
            {
                mostrarMensaje("El campo Correo Electrónico es obligatorio.");
                txtCorreoNuevoRespPagoJuridico.Focus();
                return;
            }

            if (grvTelefonosNuevoResPagoJuridico.Rows.Count == 0)
            {
                mostrarMensaje("Debe agregar al menos un número de teléfono.");
                txtTelefonoNuevoRespPagoJuridico.Focus();
                return;
            }

            //if (!chkEsPepIndividualExistente.Checked && !chkRelPepIndividualExistente.Checked && !chkAsociadoPepIndividualExistente.Checked && !chkNoPEP.Checked)
            //{
            //    mostrarMensaje("Debe de seleccionar al menos una opcion para Persona Expuesta Políticamente");
            //    return;
            //}

            nuevoCliente.nombre_persona_juridica = txtNombreNuevoRespPagoJuridico.Text;
            nuevoCliente.nit_persona_juridica = txtNumIdNuevoRespPagoJuridico.Text + txtDvIdNuevoRespPagoJuridico.Text;
            nuevoCliente.primer_nombre_representante = "";// txtPrimerNombreNuevoRespPagoJuridico.Text;
            nuevoCliente.segundo_nombre_representante = "";// txtSegundoNombreNuevoRespPagoJuridico.Text;
            nuevoCliente.primer_apellido_representante = "";// txtPrimerApellidoNuevoRespPagoJuridico.Text;
            nuevoCliente.segundo_apellido_representante = "";// txtSegundoApellidoNuevoRespPagoJuridico.Text;
            nuevoCliente.tipo_identificacion_representante = cmbTipoIdentificacionNuevoRespPagoJuridico.SelectedValue;
            nuevoCliente.numero_identificacion_representante = "";// txtNumeroIdentificacionNuevoRespPagoJuridico.Text;
            //formateo de fecha para insertar en mysql
            DateTime fechaNacimientoIndividual = Convert.ToDateTime(txtFechaConstitucionNuevoRespPagoJuridico.Text);
            nuevoCliente.fecha_constitucion_empresa = fechaNacimientoIndividual.ToString("yyyy-MM-dd");
            nuevoCliente.pais_origen_empresa = cmbOrigenEmpresaNuevoRespPagoJuridico.SelectedValue;
            nuevoCliente.pais_emision_representante = cmbPaisEmisionNuevoRespPagoJuridico.SelectedValue;
            nuevoCliente.depto_emision_representante = cmbDeptoEmisionNuevoRespPagoJuridico.SelectedValue;
            nuevoCliente.muni_emision_representante = cmbMuniEmisionNuevoRespPagoJuridico.SelectedValue;
            nuevoCliente.actividad_economica_empresa = txtActividadEconomicaNuevoRespPagoJuridico.Text;
            nuevoCliente.direccion_empresa = unificarDireccionJuridico();
            nuevoCliente.pais_direccion_empresa = cmbPaisDireccionNuevoRespPagoJuridico.SelectedValue;
            nuevoCliente.depto_direccion_empresa = cmbDeptoDireccionNuevoRespPagoJuridico.SelectedValue;
            nuevoCliente.muni_direccion_empresa = cmbMuniDireccionNuevoRespPagoJuridico.SelectedValue;
            nuevoCliente.zona_direccion_empresa = cmbZonaDireccionNuevoRespPagoJuridico.SelectedValue;
            nuevoCliente.calle_direccion_empresa = txtCalleDireccionNuevoRespPagoJuridico.Text;
            nuevoCliente.avenida_direccion_empresa = txtAvenidaDireccionNuevoRespPagoJuridico.Text;
            nuevoCliente.numero_casa_direccion_empresa = txtNumCasaDireccionNuevoRespPagoJuridico.Text;
            nuevoCliente.colonia_direccion_empresa = txtColoniaDireccionNuevoRespPagoJuridico.Text;
            nuevoCliente.edificio_direccion_empresa = txtEdificioDireccionNuevoRespPagoJuridico.Text;
            nuevoCliente.lote_direccion_empresa = txtLoteDireccionNuevoRespPagoJuridico.Text;
            nuevoCliente.manzana_direccion_empresa = txtManzanaDireccionNuevoRespPagoJuridico.Text;
            nuevoCliente.sector_direccion_empresa = txtSectorDireccionNuevoRespPagoJuridico.Text;

            if (chkEsPepNuevoRespPagoJuridico.Checked)
                nuevoCliente.esPep_juridico = "S";
            else
                nuevoCliente.esPep_juridico = "N";

            if (chkRelPepNuevoRespPagoJuridico.Checked)
                nuevoCliente.relacionPep_juridico = "S";
            else
                nuevoCliente.relacionPep_juridico = "N";

            if (chkAsociadoPepNuevoRespPagoJuridico.Checked)
                nuevoCliente.asociadoPep_juridico = "S";
            else
                nuevoCliente.asociadoPep_juridico = "N";

            nuevoCliente.correo_electronico_juridico = txtCorreoNuevoRespPagoJuridico.Text;

            for (int i = 0; i < grvTelefonosNuevoResPagoJuridico.Rows.Count; i++)
            {
                telefonos.Rows.Add(grvDireccionesJuridicoExistente.Rows[i].Cells[0].Text);
            }
           
            operacionesCliente.guardarDatosClienteJuridico(idCotizacion, "RESP", cmbTipoCliente.SelectedValue, nuevoCliente, telefonos);
            operacionesCliente.actualizarEstadoCotizacion(idCotizacion);
            redireccionarPaginaBien();
        }
        private void guardarAseguradoJuridicoExistente()
        {
            Operaciones operacionesCliente = new Operaciones();
            EstructuraClienteJuridico nuevoCliente = new EstructuraClienteJuridico();
            DataTable telefonos = new DataTable();
            telefonos.Columns.Add("Telefono", typeof(string));

            //string primerNombre = string.Empty;
            //string segundoNombre = string.Empty;
            //string primerApellido = string.Empty;
            //string segundoApellido = string.Empty;

            //String[] nombreCompleto = txtNombreRepLegalJuridicoExistente.Text.Split(' ');

            //if (nombreCompleto.Length == 1)
            //{
            //    primerNombre = nombreCompleto[0].ToString();
            //}

            //if (nombreCompleto.Length == 2)
            //{
            //    primerNombre = nombreCompleto[0].ToString();
            //    segundoNombre = nombreCompleto[1].ToString();
            //}

            //if (nombreCompleto.Length == 3)
            //{
            //    primerNombre = nombreCompleto[0].ToString();
            //    segundoNombre = nombreCompleto[1].ToString();
            //    primerApellido = nombreCompleto[2].ToString();
            //}

            //if (nombreCompleto.Length == 4)
            //{
            //    primerNombre = nombreCompleto[0].ToString();
            //    segundoNombre = nombreCompleto[1].ToString();
            //    primerApellido = nombreCompleto[2].ToString();
            //    segundoApellido = nombreCompleto[3].ToString();
            //}

            //if (nombreCompleto.Length > 4)
            //{
            //    primerNombre = nombreCompleto[0].ToString();
            //    segundoNombre = nombreCompleto[1].ToString();
            //    primerApellido = nombreCompleto[2].ToString();

            //    for (int i = 3; i < nombreCompleto.Length; i++)
            //    {
            //        segundoApellido += nombreCompleto[i].ToString() + " ";
            //    }
            //}

            //if (!chkEsPepIndividualExistente.Checked && !chkRelPepIndividualExistente.Checked && !chkAsociadoPepIndividualExistente.Checked && !chkNoPEP.Checked)
            //{
            //    mostrarMensaje("Debe de seleccionar al menos una opcion para Persona Expuesta Políticamente");
            //    return;
            //}

            nuevoCliente.nombre_persona_juridica = txtNombrePersonaJuridicaJuridicoExistente.Text;
            nuevoCliente.nit_persona_juridica = txtNumIdPersonaJuridicaJuridicoExistente.Text.Trim()  + txtDvIdPersonaJuridicaJuridicoExistente.Text.Trim();
            nuevoCliente.primer_nombre_representante = "";// primerNombre;
            nuevoCliente.segundo_nombre_representante = "";// segundoNombre;
            nuevoCliente.primer_apellido_representante = "";// primerApellido;
            nuevoCliente.segundo_apellido_representante = "";// segundoApellido;

            //formateo de fecha para insertar en mysql
            DateTime fechaNacimientoIndividual;
            if(txtFechaConstitucionJuridicoExistente.Text.Equals(string.Empty))
            {
                fechaNacimientoIndividual = DateTime.Now;
            }
            else
            {
                fechaNacimientoIndividual = Convert.ToDateTime(txtFechaConstitucionJuridicoExistente.Text);
            }
            
            nuevoCliente.fecha_constitucion_empresa = fechaNacimientoIndividual.ToString("yyyy-MM-dd");
            nuevoCliente.pais_origen_empresa = cmbOrigenEmpresaJuridicoExistente.SelectedValue;
            nuevoCliente.actividad_economica_empresa = txtActividadEconomicaJuridicoExistente.Text;

            if (chkEsPepJuridicoExistente.Checked)
                nuevoCliente.esPep_juridico = "S";
            else
                nuevoCliente.esPep_juridico = "N";

            if (chkRelPepJuridicoExistente.Checked)
                nuevoCliente.relacionPep_juridico = "S";
            else
                nuevoCliente.relacionPep_juridico = "N";

            if (chkAsociadoPepJuridicoExistente.Checked)
                nuevoCliente.asociadoPep_juridico = "S";
            else
                nuevoCliente.asociadoPep_juridico = "N";

            nuevoCliente.correo_electronico_juridico = txtCorreoJuridicoExistente.Text;

            for (int i = 0; i < grvDireccionesJuridicoExistente.Rows.Count; i++)
            {
                telefonos.Rows.Add(grvDireccionesJuridicoExistente.Rows[i].Cells[0].Text);
            }

            operacionesCliente.guardarDatosClienteJuridico(idCotizacion, "RESP", cmbTipoCliente.SelectedValue, nuevoCliente, telefonos);
            operacionesCliente.actualizarEstadoCotizacion(idCotizacion);
            redireccionarPaginaBien();
        }

        #region DIRECCIONESCLIENTE
        #endregion

        #region TELEFONOSCLIENTE





        #endregion

        #region Funcionalidad_PAISES_DEPARTAMENTOS
        protected void cmbPaisEmisionIndividualExistente_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosDepartamentos = ViewState["datosDepartamentos"] as DataTable;
            datosPais = ViewState["datosPais"] as DataTable;

            DataRow[] departamentos = datosDepartamentos.Select("CODPAIS = " + cmbPaisEmisionIndividualExistente.SelectedValue);

            if (departamentos.Count() > 1)
            {
                datosDepartamentos = departamentos.CopyToDataTable();

                cmbDeptoEmisionIndividualExistente.Visible = true;
                cmbMuniEmisionIndividualExistente.Visible = true;
                cmbDeptoEmisionIndividualExistente.DataSource = datosDepartamentos;
                cmbDeptoEmisionIndividualExistente.DataTextField = "DESCESTADO";
                cmbDeptoEmisionIndividualExistente.DataValueField = "CODESTADO";
                cmbDeptoEmisionIndividualExistente.DataBind();

            }
            else
            {
                cmbDeptoEmisionIndividualExistente.Visible = false;
                cmbMuniEmisionIndividualExistente.Visible = false;
            }
        }
        protected void cmbDeptoEmisionIndividualExistente_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosMunicipios = ViewState["datosMunicipios"] as DataTable;

            DataRow[] municipios = datosMunicipios.Select("CODPAIS = " + cmbPaisEmisionIndividualExistente.SelectedValue + "AND CODESTADO = " + cmbDeptoEmisionIndividualExistente.SelectedValue);

            if (municipios.Count() > 1)
            {
                datosMunicipios = municipios.CopyToDataTable();

                cmbMuniEmisionIndividualExistente.Visible = true;
                cmbMuniEmisionIndividualExistente.DataSource = datosMunicipios;
                cmbMuniEmisionIndividualExistente.DataTextField = "DESCCIUDAD";
                cmbMuniEmisionIndividualExistente.DataValueField = "CODCIUDAD";
                cmbMuniEmisionIndividualExistente.DataBind();
            }
            else
            {
                cmbMuniEmisionIndividualExistente.Visible = false;
            }
        }
        protected void cmbPaisEmisionJuridicoExistente_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosDepartamentos = ViewState["datosDepartamentos"] as DataTable;
            datosPais = ViewState["datosPais"] as DataTable;

            DataRow[] departamentos = datosDepartamentos.Select("CODPAIS = " + cmbPaisEmisionIndividualExistente.SelectedValue);

            if (departamentos.Count() > 1)
            {
                datosDepartamentos = departamentos.CopyToDataTable();

                cmbDeptoEmisionIndividualExistente.Visible = true;
                cmbMuniEmisionIndividualExistente.Visible = true;
                cmbDeptoEmisionIndividualExistente.DataSource = datosDepartamentos;
                cmbDeptoEmisionIndividualExistente.DataTextField = "DESCESTADO";
                cmbDeptoEmisionIndividualExistente.DataValueField = "CODESTADO";
                cmbDeptoEmisionIndividualExistente.DataBind();

            }
            else
            {
                cmbDeptoEmisionIndividualExistente.Visible = false;
                cmbMuniEmisionIndividualExistente.Visible = false;
            }
        }
        protected void cmbPaisDireccionJuridicoExistente_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosDepartamentos = ViewState["datosDepartamentos"] as DataTable;
            datosPais = ViewState["datosPais"] as DataTable;

            DataRow[] departamentos = datosDepartamentos.Select("CODPAIS = " + cmbPaisEmisionIndividualExistente.SelectedValue);

            if (departamentos.Count() > 1)
            {
                datosDepartamentos = departamentos.CopyToDataTable();

                cmbDeptoEmisionIndividualExistente.Visible = true;
                cmbMuniEmisionIndividualExistente.Visible = true;
                cmbDeptoEmisionIndividualExistente.DataSource = datosDepartamentos;
                cmbDeptoEmisionIndividualExistente.DataTextField = "DESCESTADO";
                cmbDeptoEmisionIndividualExistente.DataValueField = "CODESTADO";
                cmbDeptoEmisionIndividualExistente.DataBind();

            }
            else
            {
                cmbDeptoEmisionIndividualExistente.Visible = false;
                cmbMuniEmisionIndividualExistente.Visible = false;
            }
        }
        protected void cmbPaisEmisionNuevoResPagoIndividual_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosDepartamentos = ViewState["datosDepartamentos"] as DataTable;
            datosPais = ViewState["datosPais"] as DataTable;

            DataRow[] departamentos = datosDepartamentos.Select("CODPAIS = " + cmbPaisEmisionNuevoResPagoIndividual.SelectedValue);

            if (departamentos.Count() > 1)
            {
                datosDepartamentos = departamentos.CopyToDataTable();

                cmbDeptoEmisionNuevoResPagoIndividual.Visible = true;
                cmbMuniEmisionNuevoResPagoIndividual.Visible = true;
                cmbDeptoEmisionNuevoResPagoIndividual.DataSource = datosDepartamentos;
                cmbDeptoEmisionNuevoResPagoIndividual.DataTextField = "DESCESTADO";
                cmbDeptoEmisionNuevoResPagoIndividual.DataValueField = "CODESTADO";
                cmbDeptoEmisionNuevoResPagoIndividual.DataBind();

                cmbDeptoEmisionNuevoResPagoIndividual_SelectedIndexChanged(cmbDeptoEmisionNuevoResPagoIndividual, new EventArgs());
            }
            else
            {
                cmbDeptoEmisionNuevoResPagoIndividual.Visible = false;
                cmbMuniEmisionNuevoResPagoIndividual.Visible = false;
            }
        }
        protected void cmbDeptoEmisionNuevoResPagoIndividual_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosMunicipios = ViewState["datosMunicipios"] as DataTable;

            DataRow[] municipios = datosMunicipios.Select("CODPAIS = " + cmbPaisEmisionNuevoResPagoIndividual.SelectedValue + "AND CODESTADO = " + cmbDeptoEmisionNuevoResPagoIndividual.SelectedValue);

            if (municipios.Count() > 1)
            {
                datosMunicipios = municipios.CopyToDataTable();

                cmbMuniEmisionNuevoResPagoIndividual.Visible = true;
                cmbMuniEmisionNuevoResPagoIndividual.DataSource = datosMunicipios;
                cmbMuniEmisionNuevoResPagoIndividual.DataTextField = "DESCCIUDAD";
                cmbMuniEmisionNuevoResPagoIndividual.DataValueField = "CODCIUDAD";
                cmbMuniEmisionNuevoResPagoIndividual.DataBind();
            }
            else
            {
                cmbMuniEmisionNuevoResPagoIndividual.Visible = false;
            }
        }
        protected void cmbPaisDireccionNuevoResPagoIndividual_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosDepartamentos = ViewState["datosDepartamentos"] as DataTable;
            datosPais = ViewState["datosPais"] as DataTable;

            DataRow[] departamentos = datosDepartamentos.Select("CODPAIS = " + cmbPaisDireccionNuevoResPagoIndividual.SelectedValue);

            if (departamentos.Count() > 1)
            {
                datosDepartamentos = departamentos.CopyToDataTable();

                cmbDeptoDireccionNuevoResPagoIndividual.Visible = true;
                cmbMuniDireccionNuevoResPagoIndividual.Visible = true;
                cmbDeptoDireccionNuevoResPagoIndividual.DataSource = datosDepartamentos;
                cmbDeptoDireccionNuevoResPagoIndividual.DataTextField = "DESCESTADO";
                cmbDeptoDireccionNuevoResPagoIndividual.DataValueField = "CODESTADO";
                cmbDeptoDireccionNuevoResPagoIndividual.DataBind();

                cmbDeptoDireccionNuevoResPagoIndividual_SelectedIndexChanged(cmbDeptoDireccionNuevoResPagoIndividual, new EventArgs());
            }
            else
            {
                cmbDeptoDireccionNuevoResPagoIndividual.Visible = false;
                cmbMuniDireccionNuevoResPagoIndividual.Visible = false;
            }
        }
        protected void cmbDeptoDireccionNuevoResPagoIndividual_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosMunicipios = ViewState["datosMunicipios"] as DataTable;

            DataRow[] municipios = datosMunicipios.Select("CODPAIS = " + cmbPaisDireccionNuevoResPagoIndividual.SelectedValue + "AND CODESTADO = " + cmbDeptoDireccionNuevoResPagoIndividual.SelectedValue);

            if (municipios.Count() > 1)
            {
                datosMunicipios = municipios.CopyToDataTable();

                cmbMuniDireccionNuevoResPagoIndividual.Visible = true;
                cmbMuniDireccionNuevoResPagoIndividual.DataSource = datosMunicipios;
                cmbMuniDireccionNuevoResPagoIndividual.DataTextField = "DESCCIUDAD";
                cmbMuniDireccionNuevoResPagoIndividual.DataValueField = "CODCIUDAD";
                cmbMuniDireccionNuevoResPagoIndividual.DataBind();

                cmbMuniDireccionNuevoResPagoIndividual_SelectedIndexChanged(cmbMuniDireccionNuevoResPagoIndividual, new EventArgs());
            }
            else
            {
                cmbMuniDireccionNuevoResPagoIndividual.Visible = false;
            }
        }
        protected void cmbPaisEmisionNuevoResPagoJuridico_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosDepartamentos = ViewState["datosDepartamentos"] as DataTable;
            datosPais = ViewState["datosPais"] as DataTable;

            DataRow[] departamentos = datosDepartamentos.Select("CODPAIS = " + cmbPaisEmisionNuevoRespPagoJuridico.SelectedValue);

            if (departamentos.Count() > 1)
            {
                datosDepartamentos = departamentos.CopyToDataTable();

                cmbDeptoEmisionNuevoRespPagoJuridico.Visible = true;
                cmbMuniEmisionIndividualExistente.Visible = true;
                cmbDeptoEmisionNuevoRespPagoJuridico.DataSource = datosDepartamentos;
                cmbDeptoEmisionNuevoRespPagoJuridico.DataTextField = "DESCESTADO";
                cmbDeptoEmisionNuevoRespPagoJuridico.DataValueField = "CODESTADO";
                cmbDeptoEmisionNuevoRespPagoJuridico.DataBind();

                cmbDeptoEmisionNuevoResPagoJuridico_SelectedIndexChanged(cmbDeptoEmisionNuevoRespPagoJuridico, new EventArgs());
            }
            else
            {
                cmbDeptoEmisionIndividualExistente.Visible = false;
                cmbMuniEmisionIndividualExistente.Visible = false;
            }
        }
        protected void cmbDeptoEmisionNuevoResPagoJuridico_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosMunicipios = ViewState["datosMunicipios"] as DataTable;

            DataRow[] municipios = datosMunicipios.Select("CODPAIS = " + cmbPaisEmisionNuevoRespPagoJuridico.SelectedValue + "AND CODESTADO = " + cmbDeptoEmisionNuevoRespPagoJuridico.SelectedValue);

            if (municipios.Count() > 1)
            {
                datosMunicipios = municipios.CopyToDataTable();

                cmbMuniEmisionNuevoRespPagoJuridico.Visible = true;
                cmbMuniEmisionNuevoRespPagoJuridico.DataSource = datosMunicipios;
                cmbMuniEmisionNuevoRespPagoJuridico.DataTextField = "DESCCIUDAD";
                cmbMuniEmisionNuevoRespPagoJuridico.DataValueField = "CODCIUDAD";
                cmbMuniEmisionNuevoRespPagoJuridico.DataBind();
            }
            else
            {
                cmbMuniEmisionNuevoRespPagoJuridico.Visible = false;
            }
        }
        protected void cmbPaisDireccionNuevoResPagoJuridico_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosDepartamentos = ViewState["datosDepartamentos"] as DataTable;
            datosPais = ViewState["datosPais"] as DataTable;

            DataRow[] departamentos = datosDepartamentos.Select("CODPAIS = " + cmbPaisDireccionNuevoRespPagoJuridico.SelectedValue);

            if (departamentos.Count() > 1)
            {
                datosDepartamentos = departamentos.CopyToDataTable();

                cmbDeptoDireccionNuevoRespPagoJuridico.Visible = true;
                cmbMuniDireccionNuevoRespPagoJuridico.Visible = true;
                cmbDeptoDireccionNuevoRespPagoJuridico.DataSource = datosDepartamentos;
                cmbDeptoDireccionNuevoRespPagoJuridico.DataTextField = "DESCESTADO";
                cmbDeptoDireccionNuevoRespPagoJuridico.DataValueField = "CODESTADO";
                cmbDeptoDireccionNuevoRespPagoJuridico.DataBind();

                cmbDeptoDireccionNuevoResPagoJuridico_SelectedIndexChanged(cmbDeptoDireccionNuevoRespPagoJuridico, new EventArgs());
            }
            else
            {
                cmbDeptoDireccionNuevoRespPagoJuridico.Visible = false;
                cmbMuniDireccionNuevoRespPagoJuridico.Visible = false;
            }
        }
        protected void cmbDeptoDireccionNuevoResPagoJuridico_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosMunicipios = ViewState["datosMunicipios"] as DataTable;

            DataRow[] municipios = datosMunicipios.Select("CODPAIS = " + cmbPaisDireccionNuevoRespPagoJuridico.SelectedValue + "AND CODESTADO = " + cmbDeptoDireccionNuevoRespPagoJuridico.SelectedValue);

            if (municipios.Count() > 1)
            {
                datosMunicipios = municipios.CopyToDataTable();

                cmbMuniDireccionNuevoRespPagoJuridico.Visible = true;
                cmbMuniDireccionNuevoRespPagoJuridico.DataSource = datosMunicipios;
                cmbMuniDireccionNuevoRespPagoJuridico.DataTextField = "DESCCIUDAD";
                cmbMuniDireccionNuevoRespPagoJuridico.DataValueField = "CODCIUDAD";
                cmbMuniDireccionNuevoRespPagoJuridico.DataBind();

                cmbMuniDireccionNuevoRespPagoJuridico_SelectedIndexChanged(cmbMuniDireccionNuevoRespPagoJuridico, new EventArgs());
            }
            else
            {
                cmbMuniDireccionNuevoRespPagoJuridico.Visible = false;
            }
        }
        protected void cmbMuniDireccionNuevoResPagoIndividual_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosZonas = ViewState["datosZonas"] as DataTable;

            DataRow[] zonas = datosZonas.Select("CODPAIS = " + cmbPaisDireccionNuevoResPagoIndividual.SelectedValue + "AND CODESTADO = '" + cmbDeptoDireccionNuevoResPagoIndividual.SelectedValue + "'" + "AND CODCIUDAD = '" + cmbMuniDireccionNuevoResPagoIndividual.SelectedValue + "'");

            if (zonas.Count() > 1)
            {
                datosZonas = zonas.CopyToDataTable();

                cmbZonaDireccionNuevoRespPagoIndividual.DataSource = null;
                cmbZonaDireccionNuevoRespPagoIndividual.Visible = true;
                cmbZonaDireccionNuevoRespPagoIndividual.DataSource = datosZonas;
                cmbZonaDireccionNuevoRespPagoIndividual.DataTextField = "DESCMUNICIPIO";
                cmbZonaDireccionNuevoRespPagoIndividual.DataValueField = "CODMUNICIPIO";
                cmbZonaDireccionNuevoRespPagoIndividual.DataBind();
            }
            else
            {
                cmbZonaDireccionNuevoRespPagoIndividual.Visible = false;
            }
        }
        protected void cmbMuniDireccionNuevoRespPagoJuridico_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosZonas = ViewState["datosZonas"] as DataTable;

            DataRow[] zonas = datosZonas.Select("CODPAIS = " + cmbPaisDireccionNuevoRespPagoJuridico.SelectedValue + "AND CODESTADO = '" + cmbDeptoDireccionNuevoRespPagoJuridico.SelectedValue + "'" + "AND CODCIUDAD = '" + cmbMuniDireccionNuevoRespPagoJuridico.SelectedValue + "'");

            if (zonas.Count() > 1)
            {
                datosZonas = zonas.CopyToDataTable();

                cmbZonaDireccionNuevoRespPagoJuridico.DataSource = null;
                cmbZonaDireccionNuevoRespPagoJuridico.Visible = true;
                cmbZonaDireccionNuevoRespPagoJuridico.DataSource = datosZonas;
                cmbZonaDireccionNuevoRespPagoJuridico.DataTextField = "DESCMUNICIPIO";
                cmbZonaDireccionNuevoRespPagoJuridico.DataValueField = "CODMUNICIPIO";
                cmbZonaDireccionNuevoRespPagoJuridico.DataBind();
            }
            else
            {
                cmbZonaDireccionNuevoRespPagoJuridico.Visible = false;
            }
        }

        #endregion

        #region FUNCIONALIDADPEP
        protected void chkEsPepNuevoResPagoIndividual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEsPepNuevoResPagoIndividual.Checked)
            {
                pnlPepNuevoIndividual.Visible = true;
            }
            else
            {
                if (!chkRelPepNuevoResPagoIndividual.Checked && !chkAsociadoPepNuevoResPagoIndividual.Checked)
                {
                    pnlPepNuevoIndividual.Visible = false;
                }
            }
        }
        protected void chkRelPepNuevoResPagoIndividual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRelPepNuevoResPagoIndividual.Checked)
            {
                pnlPepNuevoIndividual.Visible = true;
            }
            else
            {
                if (!chkEsPepNuevoResPagoIndividual.Checked && !chkAsociadoPepNuevoResPagoIndividual.Checked)
                {
                    pnlPepNuevoIndividual.Visible = false;
                }
            }
        }
        protected void chkAsociadoPepNuevoResPagoIndividual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAsociadoPepNuevoResPagoIndividual.Checked)
            {
                pnlPepNuevoIndividual.Visible = true;
            }
            else
            {
                if (!chkEsPepNuevoResPagoIndividual.Checked && !chkRelPepNuevoResPagoIndividual.Checked)
                {
                    pnlPepNuevoIndividual.Visible = false;
                }
            }
        }
        protected void chkEsPepNuevoRespPagoJuridico_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEsPepNuevoRespPagoJuridico.Checked)
            {
                pnlPepNuevoIndividual.Visible = true;
            }
            else
            {
                if (!chkRelPepNuevoRespPagoJuridico.Checked && !chkAsociadoPepNuevoRespPagoJuridico.Checked)
                {
                    pnlPepNuevoIndividual.Visible = false;
                }
            }
        }
        protected void chkRelPepNuevoRespPagoJuridico_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRelPepNuevoRespPagoJuridico.Checked)
            {
                pnlPepNuevoIndividual.Visible = true;
            }
            else
            {
                if (!chkEsPepNuevoRespPagoJuridico.Checked && !chkAsociadoPepNuevoRespPagoJuridico.Checked)
                {
                    pnlPepNuevoIndividual.Visible = false;
                }
            }
        }
        protected void chkAsociadoPepNuevoRespPagoJuridico_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAsociadoPepNuevoRespPagoJuridico.Checked)
            {
                pnlPepNuevoIndividual.Visible = true;
            }
            else
            {
                if (!chkEsPepNuevoRespPagoJuridico.Checked && !chkRelPepNuevoRespPagoJuridico.Checked)
                {
                    pnlPepNuevoIndividual.Visible = false;
                }
            }
        }
        #endregion
        private void mostrarMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", "alert('" + mensaje + "');", true);
        }  
        private void redireccionarPaginaBien()
        {
            DataTable informacionPlanSeleccionado = new DataTable();
            Varias clVarias = new Varias();
            informacionPlanSeleccionado = clVarias.obtenerInformacionPlan(idPlanSeleccionado);

            if (informacionPlanSeleccionado.Rows[0]["codprod"].ToString() == "MRIN")
            {
                Response.Redirect("../HogarSeguro/Cotizaciones.aspx?plan=" + idPlanSeleccionado);
            }
            else
            {
                Response.Redirect("../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlanSeleccionado);
            }
        }
        protected void btnAgregarTelefonoNuevoResPagoIndividual_Click(object sender, EventArgs e)
        {
             datosTelefonos = ViewState["datosTelefonos"] as DataTable;


            if (datosTelefonos.Rows.Count < 4)
            {
                if (txtTelefonoNuevoResPagoIndividual.Text != string.Empty)
                {
                    if (txtTelefonoNuevoResPagoIndividual.Text.Length != 8)
                    {

                        mostrarMensajeJavaScript("Ingrese un número teléfonico valido.");
                        return;
                    }

                    datosTelefonos.Rows.Add(txtTelefonoNuevoResPagoIndividual.Text);
                    grvTelefonosNuevoResPagoIndividual.DataSource = datosTelefonos;
                    grvTelefonosNuevoResPagoIndividual.DataBind();
                }
                else
                {
                    mostrarMensajeJavaScript("El campo teléfono no puede estar vacío.");
                    return;
                }
            }
            else
            {
                mostrarMensajeJavaScript("Solo pueden agregar 4 números de teléfono.");
            }

        }
        private void mostrarMensajeJavaScript(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        protected void btnAgregarTelefonoNuevoRespPagoJuridico_Click(object sender, EventArgs e)
        {

            datosTelefonos = ViewState["datosTelefonos"] as DataTable;


            if (datosTelefonos.Rows.Count < 4)
            {
                if (txtTelefonoNuevoRespPagoJuridico.Text != string.Empty)
                {
                    if (txtTelefonoNuevoRespPagoJuridico.Text.Length != 8)
                    {

                        mostrarMensajeJavaScript("Ingrese un número teléfonico valido.");
                        return;
                    }

                    datosTelefonos.Rows.Add(txtTelefonoNuevoRespPagoJuridico.Text);
                    grvTelefonosNuevoResPagoJuridico.DataSource = datosTelefonos;
                    grvTelefonosNuevoResPagoJuridico.DataBind();
                }
                else
                {
                    mostrarMensajeJavaScript("El campo teléfono no puede estar vacío.");
                    return;
                }
            }
            else
            {
                mostrarMensajeJavaScript("Solo pueden agregar 4 números de teléfono.");
            }
        }
        private string unificarDireccionIndividual()
        {
            string resultado = string.Empty;

            resultado = txtCalleDireccionNuevoRespPagoIndividual.Text + " " + txtAvenidaDireccionNuevoRespPagoIndividual.Text
                + " " + txtNumCasaDireccionNuevoRespPagoIndividual.Text + " " + cmbZonaDireccionNuevoRespPagoIndividual.SelectedItem.Text + " " + txtColoniaDireccionNuevoRespPagoIndividual.Text + " " +
                txtEdificioDireccionNuevoRespPagoIndividual.Text;

            return resultado;
        }
        private string unificarDireccionJuridico()
        {
            string resultado = string.Empty;

            resultado = txtCalleDireccionNuevoRespPagoJuridico.Text + " " + txtAvenidaDireccionNuevoRespPagoJuridico.Text
               + " " + txtNumCasaDireccionNuevoRespPagoJuridico.Text + " " + cmbZonaDireccionNuevoRespPagoJuridico.SelectedItem.Text + " " + txtColoniaDireccionNuevoRespPagoJuridico.Text + " " +
               txtEdificioDireccionNuevoRespPagoJuridico.Text;

            return resultado;
        }
        /// <summary>
        /// Boton que elimina al cliente que se agrego como 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //eliminar cliente de cotizacion No. idCotizacion
            Operaciones OPremoverCliente = new Operaciones();
            bool resp = OPremoverCliente.removerClientesCotizacionCancelada(idCotizacion);
            if(resp)
            {
                //correcto y redirigir pagina
                redireccionarPaginaBien();
            }
            else
            {
                //error y mostrar en pagina
            }
        }
    }
}