using Lbl_Cotizado_Autos_Web.Clientes;
using Lbl_Cotizado_Autos_Web.Acceso;
using Lbl_Cotizado_Autos_Web.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using Lbl_Cotizador_Autos_Web.Acceso;

namespace CotizadorWebAutos.Cliente
{
    public partial class AseguradoTitular : System.Web.UI.Page
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
        DataTable datosDirecciones;
        DataTable datosTipoDireccion;
        DataTable datosTipoTelefono;
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
                        string sCodCliOracle = datosCliente.Rows[0]["CODCLI"].ToString();
                        ViewState.Add("CodCliOracle", sCodCliOracle);
                        direccionesCliente = clConsultas.obtenerDireccionesCliente(sCodCliOracle);
                        telefonosCliente = clConsultas.obtenerTelefonosCliente(sCodCliOracle);

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
                        txtNumIdNuevoAsegIndividual.Text = txtNumIDCliente.Text;
                        txtDvIdNuevoAsegIndividual.Text = txtDvIdCliente.Text.Trim();

                        Boolean bActualizacion = false; // MMora esto es para utilizar el panel de nuevo asegurado para ingreso de actualizacion de datos
                        ViewState.Add("ActualizacionDatosCliente", bActualizacion);
                        btnNoEnviarActualizacion.Visible = false;
                        pnlDatosClienteExistente.Visible = false;
                        pnlNuevoAseguradoJuridico.Visible = false;
                        pnlDatosClienteNuevo.Visible = true;
                        pnlNuevoAseguradoIndividual.Visible = true;
                        
                    }
                }
                else
                {
                    //CLIENTE JURIDICO
                    datosCliente = clConsultas.obtenerDatosClienteNitJuridico(Convert.ToInt32(txtNumIDCliente.Text), txtDvIdCliente.Text.Trim());

                    if (datosCliente.Rows.Count == 1)
                    {
                        string sCodCliOracle = datosCliente.Rows[0]["CODCLI"].ToString();
                        ViewState.Add("CodCliOracle", sCodCliOracle);
                        direccionesCliente = clConsultas.obtenerDireccionesCliente(sCodCliOracle);
                        telefonosCliente = clConsultas.obtenerTelefonosCliente(sCodCliOracle);

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
                        txtNumIdNuevoAsegIndividual.Text = txtNumIdNuevoAsegJuridico.Text;
                        txtDvIdNuevoAsegIndividual.Text = txtDvIdNuevoAsegJuridico.Text.Trim();

                        Boolean bActualizacion = false; // MMora esto es para utilizar el panel de nuevo asegurado para ingreso de actualizacion de datos
                        ViewState.Add("ActualizacionDatosCliente", bActualizacion);
                        btbNoEnviarActualizacionJuridico.Visible = false;
                        pnlDatosClienteExistente.Visible = false;
                        pnlDatosClienteNuevo.Visible = true;
                        pnlNuevoAseguradoJuridico.Visible = true;
                        pnlNuevoAseguradoIndividual.Visible = false;
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
            //Validacion de campos vacios
            string nombreCliente = string.Empty;
            string fecNacimiento = string.Empty;          

            nombreCliente = datosCliente.Rows[0]["NOMBRE"].ToString();
            fecNacimiento = datosCliente.Rows[0]["FECNAC"].ToString();

            txtNombreIndividualExistente.Text = nombreCliente;

            if (fecNacimiento == string.Empty)
            {
                txtFechaNacimientoIndividualExistente.Text = string.Empty;
            }
            else
            {
                DateTime fechaNacimiento = Convert.ToDateTime(fecNacimiento);
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

            cmbPaisEmisionIndividualExistente_SelectedIndexChanged(cmbPaisEmisionIndividualExistente, new EventArgs());

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
            txtNombreRepLegalJuridicoExistente.Text = datosCliente.Rows[0]["NOMBRE_REP_LEGAL"].ToString();
            if (datosCliente.Rows[0]["NIT_REP_LEGAL"].ToString().Contains("-"))
            {
                txtNumIdRepLegalJuridicoExistente.Text = datosCliente.Rows[0]["NIT_REP_LEGAL"].ToString().Split('-')[0];
                txtDvIdRepLegalJuridicoExistente.Text = datosCliente.Rows[0]["NIT_REP_LEGAL"].ToString().Split('-')[1];            
            }
            else
            {
                string nit = datosCliente.Rows[0]["NIT_REP_LEGAL"].ToString();
                txtNumIdRepLegalJuridicoExistente.Text = nit.Substring(nit.Length - 1);
                txtDvIdRepLegalJuridicoExistente.Text = Convert.ToString(nit[nit.Length - 1]);
            } 

            if (datosCliente.Rows[0]["FECHA_CONSTITUCION"].ToString() == string.Empty)
            {
                txtFechaConstitucionJuridicoExistente.Text = string.Empty;
            }
            else
            {
                DateTime fechaConstitucion = Convert.ToDateTime(datosCliente.Rows[0]["FECHA_CONSTITUCION"].ToString());

                txtFechaConstitucionJuridicoExistente.Text = fechaConstitucion.ToString("dd-MM-yyyy");
            }    

            if (datosCliente.Rows[0]["PAIS_EMPRESA"].ToString() == "NO EXISTENTE")
            {
                cmbOrigenEmpresaJuridicoExistente.SelectedIndex = 0;
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
            datosTipoDireccion = new DataTable();
            datosTipoTelefono = new DataTable();

            datosTelefonos = new DataTable();
            datosTelefonos.Columns.Add("Numero", typeof(string));
            //datosTelefonos.Columns.Add("Tipo", typeof(string));
            //datosTelefonos.Columns.Add("CodTipo", typeof(string));

            datosDirecciones = new DataTable();
            datosDirecciones.Columns.Add("Direccion", typeof(string));
            datosDirecciones.Columns.Add("Pais", typeof(string));
            datosDirecciones.Columns.Add("Departamento", typeof(string));
            datosDirecciones.Columns.Add("Municipio", typeof(string));
            datosDirecciones.Columns.Add("Tipo", typeof(string));
            datosDirecciones.Columns.Add("codPais", typeof(string));
            datosDirecciones.Columns.Add("codEstado", typeof(string));
            datosDirecciones.Columns.Add("codCiudad", typeof(string));
            datosDirecciones.Columns.Add("codLval", typeof(string));

            Consultas clVarios = new Consultas();

            datosPais = clVarios.obtenerPaises();
            datosDepartamentos = clVarios.obtenerDepartamentos();
            datosMunicipios = clVarios.obtenerMunicipios();
            datosZonas = clVarios.obtenerZonas();
            datosNacionalidades = clVarios.obtenerNacionalidades();
            datosProfesiones = clVarios.obtenerProfesiones();
            datosEstadoCivil = clVarios.obtenerEstadosCiviles();
            datosGeneros = clVarios.obtenerGeneros();
            datosTipoDireccion = clVarios.obtenerTiposDireccion();
            datosTipoTelefono = clVarios.obtenerTiposTelefono();

            ViewState.Add("datosPais", datosPais);
            ViewState.Add("datosDepartamentos", datosDepartamentos);
            ViewState.Add("datosMunicipios", datosMunicipios);
            ViewState.Add("datosZonas", datosZonas);
            ViewState.Add("datosTelefonos", datosTelefonos);
            ViewState.Add("datosDirecciones", datosDirecciones);

            //--------------------------------------------PAISES-----------------------------------------------------------------------

            #region PAISES
            //Cliente Individual Existente
            cmbPaisEmisionIndividualExistente.DataSource = datosPais;
            cmbPaisEmisionIndividualExistente.DataTextField = "DESCPAIS";
            cmbPaisEmisionIndividualExistente.DataValueField = "CODPAIS";
            cmbPaisEmisionIndividualExistente.DataBind();            

            //Cliente Individual Nuevo Asegurado            
            cmbPaisEmisionNuevoAsegIndividual.DataSource = datosPais;
            cmbPaisEmisionNuevoAsegIndividual.DataTextField = "DESCPAIS";
            cmbPaisEmisionNuevoAsegIndividual.DataValueField = "CODPAIS";
            cmbPaisEmisionNuevoAsegIndividual.DataBind();

            cmbPaisEmisionNuevoAsegIndividual.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbPaisEmisionNuevoAsegIndividual.SelectedIndex = 0;

            cmbPaisDireccionNuevoAsegIndividual.DataSource = datosPais;
            cmbPaisDireccionNuevoAsegIndividual.DataTextField = "DESCPAIS";
            cmbPaisDireccionNuevoAsegIndividual.DataValueField = "CODPAIS";
            cmbPaisDireccionNuevoAsegIndividual.DataBind();

            cmbPaisDireccionNuevoAsegIndividual.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbPaisDireccionNuevoAsegIndividual.SelectedIndex = 0;

            //Cliente Juridico Nuevo Asegurado
            cmbPaisEmisionNuevoAsegJuridico.DataSource = datosPais;
            cmbPaisEmisionNuevoAsegJuridico.DataTextField = "DESCPAIS";
            cmbPaisEmisionNuevoAsegJuridico.DataValueField = "CODPAIS";
            cmbPaisEmisionNuevoAsegJuridico.DataBind();

            cmbPaisEmisionNuevoAsegJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbPaisEmisionNuevoAsegJuridico.SelectedIndex = 0;

            cmbPaisDireccionNuevoAsegJuridico.DataSource = datosPais;
            cmbPaisDireccionNuevoAsegJuridico.DataTextField = "DESCPAIS";
            cmbPaisDireccionNuevoAsegJuridico.DataValueField = "CODPAIS";
            cmbPaisDireccionNuevoAsegJuridico.DataBind();

            cmbPaisDireccionNuevoAsegJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbPaisDireccionNuevoAsegJuridico.SelectedIndex = 0;

            #endregion

            //--------------------------------------------DEPARTAMENTOS-----------------------------------------------------------------------

            #region DEPARTAMENTOS
            //Cliente Individual Existente
            cmbDeptoEmisionIndividualExistente.DataSource = datosDepartamentos;
            cmbDeptoEmisionIndividualExistente.DataTextField = "DESCESTADO";
            cmbDeptoEmisionIndividualExistente.DataValueField = "CODESTADO";
            cmbDeptoEmisionIndividualExistente.DataBind();            

            //Cliente Individual Nuevo Asegurado
            cmbDeptoEmisionNuevoAsegIndividual.DataSource = datosDepartamentos;
            cmbDeptoEmisionNuevoAsegIndividual.DataTextField = "DESCESTADO";
            cmbDeptoEmisionNuevoAsegIndividual.DataValueField = "CODESTADO";
            cmbDeptoEmisionNuevoAsegIndividual.DataBind();

            cmbDeptoEmisionNuevoAsegIndividual.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbDeptoEmisionNuevoAsegIndividual.SelectedIndex = 0;

            cmbDeptoDireccionNuevoAsegIndividual.DataSource = datosDepartamentos;
            cmbDeptoDireccionNuevoAsegIndividual.DataTextField = "DESCESTADO";
            cmbDeptoDireccionNuevoAsegIndividual.DataValueField = "CODESTADO";
            cmbDeptoDireccionNuevoAsegIndividual.DataBind();

            cmbDeptoDireccionNuevoAsegIndividual.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbDeptoDireccionNuevoAsegIndividual.SelectedIndex = 0;

            //Cliente Juridico Nuevo Asegurado
            cmbDeptoDireccionNuevoAsegJuridico.DataSource = datosDepartamentos;
            cmbDeptoDireccionNuevoAsegJuridico.DataTextField = "DESCESTADO";
            cmbDeptoDireccionNuevoAsegJuridico.DataValueField = "CODESTADO";
            cmbDeptoDireccionNuevoAsegJuridico.DataBind();

            cmbDeptoDireccionNuevoAsegJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbDeptoDireccionNuevoAsegJuridico.SelectedIndex = 0;

            cmbDeptoEmisionNuevoAsegJuridico.DataSource = datosDepartamentos;
            cmbDeptoEmisionNuevoAsegJuridico.DataTextField = "DESCESTADO";
            cmbDeptoEmisionNuevoAsegJuridico.DataValueField = "CODESTADO";
            cmbDeptoEmisionNuevoAsegJuridico.DataBind();

            cmbDeptoEmisionNuevoAsegJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbDeptoEmisionNuevoAsegJuridico.SelectedIndex = 0;

            #endregion

            //--------------------------------------------MUNICIPIOS-----------------------------------------------------------------------

            #region MUNICIPIOS

            //Cliente Individual Existente
            cmbMuniEmisionIndividualExistente.DataSource = datosMunicipios;
            cmbMuniEmisionIndividualExistente.DataTextField = "DESCCIUDAD";
            cmbMuniEmisionIndividualExistente.DataValueField = "CODCIUDAD";
            cmbMuniEmisionIndividualExistente.DataBind();           

            //Cliente Individual Nuevo Asegurado
            cmbMuniEmisionNuevoAsegIndividual.DataSource = datosMunicipios;
            cmbMuniEmisionNuevoAsegIndividual.DataTextField = "DESCCIUDAD";
            cmbMuniEmisionNuevoAsegIndividual.DataValueField = "CODCIUDAD";
            cmbMuniEmisionNuevoAsegIndividual.DataBind();

            cmbMuniEmisionNuevoAsegIndividual.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbMuniEmisionNuevoAsegIndividual.SelectedIndex = 0;

            cmbMuniDireccionNuevoAsegIndividual.DataSource = datosMunicipios;
            cmbMuniDireccionNuevoAsegIndividual.DataTextField = "DESCCIUDAD";
            cmbMuniDireccionNuevoAsegIndividual.DataValueField = "CODCIUDAD";
            cmbMuniDireccionNuevoAsegIndividual.DataBind();

            cmbMuniDireccionNuevoAsegIndividual.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbMuniDireccionNuevoAsegIndividual.SelectedIndex = 0;

            //Cliente Juridico Nuevo Asegurado
            cmbMuniEmisionNuevoAsegJuridico.DataSource = datosMunicipios;
            cmbMuniEmisionNuevoAsegJuridico.DataTextField = "DESCCIUDAD";
            cmbMuniEmisionNuevoAsegJuridico.DataValueField = "CODCIUDAD";
            cmbMuniEmisionNuevoAsegJuridico.DataBind();

            cmbMuniEmisionNuevoAsegJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbMuniEmisionNuevoAsegJuridico.SelectedIndex = 0;

            cmbMuniDireccionNuevoAsegJuridico.DataSource = datosMunicipios;
            cmbMuniDireccionNuevoAsegJuridico.DataTextField = "DESCCIUDAD";
            cmbMuniDireccionNuevoAsegJuridico.DataValueField = "CODCIUDAD";
            cmbMuniDireccionNuevoAsegJuridico.DataBind();

            cmbMuniDireccionNuevoAsegJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbMuniDireccionNuevoAsegJuridico.SelectedIndex = 0;

            #endregion

            //--------------------------------------------ZONAS-----------------------------------------------------------------------
            #region ZONAS
            cmbZonaDireccionNuevoAsegIndividual.DataSource = datosZonas;
            cmbZonaDireccionNuevoAsegIndividual.DataTextField = "DESCMUNICIPIO";
            cmbZonaDireccionNuevoAsegIndividual.DataValueField = "CODMUNICIPIO";
            cmbZonaDireccionNuevoAsegIndividual.DataBind();

            cmbZonaDireccionNuevoAsegIndividual.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbZonaDireccionNuevoAsegIndividual.SelectedIndex = 0;

            cmbZonaDireccionNuevoAsegJuridico.DataSource = datosZonas;
            cmbZonaDireccionNuevoAsegJuridico.DataTextField = "DESCMUNICIPIO";
            cmbZonaDireccionNuevoAsegJuridico.DataValueField = "CODMUNICIPIO";
            cmbZonaDireccionNuevoAsegJuridico.DataBind();

            cmbZonaDireccionNuevoAsegJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbZonaDireccionNuevoAsegJuridico.SelectedIndex = 0;
            #endregion

            //--------------------------------------------NACIONALIDADES-----------------------------------------------------------------------

            #region NACIONALIDADES
            //Cliente Individual Existente
            cmbNacionalidadIndividualExistente.DataSource = datosNacionalidades;
            cmbNacionalidadIndividualExistente.DataTextField = "DESCRIP";
            cmbNacionalidadIndividualExistente.DataValueField = "CODLVAL";
            cmbNacionalidadIndividualExistente.DataBind();            

            //Cliente Juridico Existente

            cmbOrigenEmpresaJuridicoExistente.DataSource = datosPais;
            cmbOrigenEmpresaJuridicoExistente.DataTextField = "DESCPAIS";
            cmbOrigenEmpresaJuridicoExistente.DataValueField = "CODPAIS";
            cmbOrigenEmpresaJuridicoExistente.DataBind();

            cmbOrigenEmpresaJuridicoExistente.Items.Insert(0, new ListItem(String.Empty, String.Empty));


            //Cliente Individual Nuevo Asegurado

            cmbNacionalidadNuevoAsegIndividual.DataSource = datosNacionalidades;
            cmbNacionalidadNuevoAsegIndividual.DataTextField = "DESCRIP";
            cmbNacionalidadNuevoAsegIndividual.DataValueField = "CODLVAL";
            cmbNacionalidadNuevoAsegIndividual.DataBind();            

            //Cliente Juridico Nuevo Asegurado

            cmbOrigenEmpresaNuevoAsegJuridico.DataSource = datosPais;
            cmbOrigenEmpresaNuevoAsegJuridico.DataTextField = "DESCPAIS";
            cmbOrigenEmpresaNuevoAsegJuridico.DataValueField = "CODPAIS";
            cmbOrigenEmpresaNuevoAsegJuridico.DataBind();

            cmbOrigenEmpresaNuevoAsegJuridico.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            cmbOrigenEmpresaNuevoAsegJuridico.SelectedIndex = 0;

            #endregion

            //--------------------------------------------PROFESIONES-----------------------------------------------------------------------

            #region PROFESIONES
            //Cliente Individual Existente
            cmbProfesionIndividualExistente.DataSource = datosProfesiones;
            cmbProfesionIndividualExistente.DataTextField = "DESCRIP";
            cmbProfesionIndividualExistente.DataValueField = "CODLVAL";
            cmbProfesionIndividualExistente.DataBind();            

            //Cliente Individual Nuevo Asegurado
            cmbProfesionNuevoAsegIndividual.DataSource = datosProfesiones;
            cmbProfesionNuevoAsegIndividual.DataTextField = "DESCRIP";
            cmbProfesionNuevoAsegIndividual.DataValueField = "CODLVAL";
            cmbProfesionNuevoAsegIndividual.DataBind();
            
            #endregion

            //--------------------------------------------ESTADOS CIVILES-----------------------------------------------------------------------

            #region ESTADOS_CIVILES
            //Cliente Individual Existente   
            cmbEstadoCivilIndividualExistente.DataSource = datosEstadoCivil;
            cmbEstadoCivilIndividualExistente.DataTextField = "DESCRIP";
            cmbEstadoCivilIndividualExistente.DataValueField = "CODLVAL";
            cmbEstadoCivilIndividualExistente.DataBind();            

            //Cliente Individual Nuevo Asegurado    
            cmbEstadoCivilNuevoAsegIndividual.DataSource = datosEstadoCivil;
            cmbEstadoCivilNuevoAsegIndividual.DataTextField = "DESCRIP";
            cmbEstadoCivilNuevoAsegIndividual.DataValueField = "CODLVAL";
            cmbEstadoCivilNuevoAsegIndividual.DataBind();                     

            #endregion

            //--------------------------------------------GENEROS-----------------------------------------------------------------------

            #region GENEROS
            //Cliente Individual Existente            
            cmbGeneroIndividualExistente.DataSource = datosGeneros;
            cmbGeneroIndividualExistente.DataTextField = "DESCRIP";
            cmbGeneroIndividualExistente.DataValueField = "CODLVAL";
            cmbGeneroIndividualExistente.DataBind();            

            //Cliente Individual Nuevo Asegurado   
            cmbGeneroNuevoAsegIndividual.DataSource = datosGeneros;
            cmbGeneroNuevoAsegIndividual.DataTextField = "DESCRIP";
            cmbGeneroNuevoAsegIndividual.DataValueField = "CODLVAL";
            cmbGeneroNuevoAsegIndividual.DataBind();            

            //--------------------------------------------TIPOS DIRECCION-----------------------------------------------------------------------

            //cmbTipoDireccionNuevoAsegIndividual.DataSource = datosTipoDireccion;
            //cmbTipoDireccionNuevoAsegIndividual.DataTextField = "DESCRIP";
            //cmbTipoDireccionNuevoAsegIndividual.DataValueField = "CODLVAL";
            //cmbTipoDireccionNuevoAsegIndividual.DataBind();

            cmbTipoTelefonoNuevoAsegIndividual.DataSource = datosTipoTelefono;
            cmbTipoTelefonoNuevoAsegIndividual.DataTextField = "DESCRIP";
            cmbTipoTelefonoNuevoAsegIndividual.DataValueField = "CODLVAL";
            cmbTipoTelefonoNuevoAsegIndividual.DataBind();

            #endregion
        }
        protected void btnGuardarNuevoAsegIndividual_Click(object sender, EventArgs e)
        {
            if (ViewState["ActualizacionDatosCliente"].ToString() == "False")
                guardarAseguradoIndividual();
            else
            {
                enviarCorreoElectronico("P");
                if (!chkRespPagoIndividualExistente.Checked)
                {
                    mostrarMensaje("Los datos del asegurado se enviaron para actualizacion. Proceda a llenar los datos del responsable de pago.");
                    Response.Redirect("../Cliente/ResponsablePago.aspx");
                }
                else
                {
                    redireccionarPaginaBien();
                }
            }

        }
        protected void btnGuardarNuevoAsegJuridico_Click(object sender, EventArgs e)
        {
            if (ViewState["ActualizacionDatosCliente"].ToString() == "False")
                guardarAseguradoJuridico();
            else
            {
                enviarCorreoElectronico("E");
                if (!chkRespPagoJuridicoExistente.Checked)                
                {
                    mostrarMensaje("Los datos del asegurado se enviaron para actualizacion. Proceda a llenar los datos del responsable de pago.");
                    Response.Redirect("../Cliente/ResponsablePago.aspx");
                }
                else
                {
                    redireccionarPaginaBien();
                }
            }
        }        
        protected void btnGuardarClienteIndividualExistente_Click(object sender, EventArgs e)
        {
            if (txtCorreoIndividualExistente.Text == string.Empty)
            {
                mostrarMensaje("El correo es un campo obligatorio. Ingrese un correo válido.");
                txtCorreoIndividualExistente.Focus();
                return;
            }
            Consultas lbConsultas = new Consultas();
            DateTime dFechaBitacora = lbConsultas.ObtenerBitacoraCliente(ViewState["CodCliOracle"].ToString());
            if (dFechaBitacora < DateTime.Now)
            {
                if (lbConsultas.ConsultaUsuarioInterno(informacionUsuario.idUsuario.ToString()))
                    mostrarMensaje("La bitácora del cliente está vencida, por favor comunicarse con el Depto. de Soluciones");
                else
                { 
                    guardarAseguradoIndividualExistente();
                    Boolean bActualizacion = true;
                    ViewState["ActualizacionDatosCliente"] = bActualizacion;
                    String[] nombreCompleto = txtNombreIndividualExistente.Text.Split(' ');
                    int iDe = 0;
                    int iNombre = 0;
                    for (int i = 0; i < nombreCompleto.Length; i++ )
                    {
                        if (nombreCompleto[i].ToString() == "DE")
                            iDe = -1;
                        else
                        {
                            if (iNombre == 0)
                                txtPrimerNombreNuevoAsegIndividual.Text = nombreCompleto[i+iDe].ToString();
                            if (iNombre == 1 )
                                txtSegundoNombreNuevoAsegIndividual.Text = nombreCompleto[i + iDe].ToString();
                            if (iNombre == 2 )
                                txtPrimerApellidoNuevoAsegIndividual.Text = nombreCompleto[i + iDe].ToString();
                            if (iNombre == 3 )
                                txtSegundoApellidoNuevoAsegIndividual.Text = nombreCompleto[i + iDe].ToString();
                           /* if (iNombre == 4 ) -- Casada
                                txtSegundoNombreNuevoAsegIndividual.Text = nombreCompleto[i + iDe].ToString();*/
                            iNombre++;
                        }
                        

                    }
                    DateTime fechaNacimientoIndividual = Convert.ToDateTime(txtFechaNacimientoIndividualExistente.Text);
                    txtFechaNacimientoNuevoAsegIndividual.Text = fechaNacimientoIndividual.ToString("dd/MM/yyyy");                    
                    SetearCombo(cmbGeneroNuevoAsegIndividual, cmbGeneroIndividualExistente.SelectedItem.Text);                    
                    SetearCombo(cmbEstadoCivilNuevoAsegIndividual, cmbEstadoCivilIndividualExistente.SelectedItem.Text);                    
                    SetearCombo(cmbProfesionNuevoAsegIndividual, cmbProfesionIndividualExistente.SelectedItem.Text);                    
                    SetearCombo(cmbNacionalidadNuevoAsegIndividual, cmbNacionalidadIndividualExistente.SelectedItem.Text);                    
                    SetearCombo(cmbTipoIdentificacionNuevoAsegIndividual, cmbTipoIdentificacionIndividualExistente.SelectedItem.Text);
                    txtNumeroIdentificacionNuevoAsegIndividual.Text = txtNumeroIdentificacionIndividualExistente.Text;                                        
                    SetearCombo(cmbPaisEmisionNuevoAsegIndividual, cmbPaisEmisionIndividualExistente.SelectedItem.Text);                    
                    SetearCombo(cmbDeptoEmisionNuevoAsegIndividual, cmbDeptoEmisionIndividualExistente.SelectedItem.Text);                    
                    SetearCombo(cmbMuniEmisionNuevoAsegIndividual, cmbMuniEmisionIndividualExistente.SelectedItem.Text);                    
                    SetearCombo(cmbPaisEmisionNuevoAsegIndividual, cmbPaisEmisionIndividualExistente.SelectedItem.Text);
                    txtNegocioPropioNuevoAsegIndividual.Text = txtNegocioPropioIndividualExistente.Text;
                    txtNumIdNuevoAsegIndividual.Text = txtNumIdIndividualExistente.Text;
                    txtDvIdNuevoAsegIndividual.Text = txtDvIdIndividualExistente.Text;
                    chkRespPagoNuevoAsegIndividual.Visible = false;
                    chkActuaNombrePropioNuevoAsegIndividual.Visible = false;
                    lbResPagoInd.Visible = false;
                    if (grvTelefonosIndividualExistente.Rows.Count > 0)
                    {
                        txtTelefonoNuevoAsegIndividual.Text = grvTelefonosIndividualExistente.Rows[0].Cells[0].Text.ToString();
                    }

                    if (grvDireccionesIndividualExistente.Rows.Count > 0)
                    {
                        //txtDireccionNuevoAsegIndividual.Text = grvDireccionesIndividualExistente.Rows[0].Cells[0].Text.ToString();
                    }

                    txtCorreoNuevoAsegIndividual.Text = txtCorreoJuridicoExistente.Text;
                    btnGuardarNuevoAsegIndividual.Text = "Enviar Datos y Continuar";
                    btnNoEnviarActualizacion.Visible = true;
                    lbTelefonoIndvidual.Visible = false;
                    //lbDireccionesIndividual.Visible = false;
                    btnAgregarTelefonoNuevoAsegIndivifual.Visible = false;
                    mostrarMensaje("Los datos del cliente están vencidos, por favor revisarlos y enviarlos para actualización.");
                    pnlDatosClienteExistente.Visible = false;
                    pnlNuevoAseguradoJuridico.Visible = false;
                    pnlDatosClienteNuevo.Visible = true;
                    pnlNuevoAseguradoIndividual.Visible = true;
                }
                
            }
            else
                if (!chkRespPagoIndividualExistente.Checked)
                {
                    guardarAseguradoIndividualExistente();
                    mostrarMensaje("Los datos del asegurado se guardaron correctamente. Proceda a llenar los datos del responsable de pago.");
                    Response.Redirect("../Cliente/ResponsablePago.aspx");
                }
                else
                {
                    guardarAseguradoIndividualExistente();
                    redireccionarPaginaBien();
                }
            
        }
        protected void btnGuardarJuridicoExistente_Click(object sender, EventArgs e)
        {
            if (txtCorreoJuridicoExistente.Text == string.Empty)
            {
                mostrarMensaje("El correo es un campo obligatorio. Ingrese un correo válido.");
                txtCorreoJuridicoExistente.Focus();
                return;
            }
            Consultas lbConsultas = new Consultas();
            DateTime dFechaBitacora = lbConsultas.ObtenerBitacoraCliente(ViewState["CodCliOracle"].ToString());
            if (dFechaBitacora < DateTime.Now)
            {
                if (lbConsultas.ConsultaUsuarioInterno(informacionUsuario.idUsuario.ToString()))
                    mostrarMensaje("La bitácora del cliente está vencida, por favor comunicarse con el Depto. de Soluciones");
                else
                {
                    guardarAseguradoJuridicoExistente();
                    Boolean bActualizacion = true;
                    ViewState["ActualizacionDatosCliente"] = bActualizacion;
                    txtNombreNuevoAsegJuridico.Text = txtNombrePersonaJuridicaJuridicoExistente.Text;
                    txtNumIdNuevoAsegJuridico.Text = txtNumIdPersonaJuridicaJuridicoExistente.Text;
                    txtDvIdNuevoAsegJuridico.Text = txtDvIdPersonaJuridicaJuridicoExistente.Text;
                    String[] nombreCompleto = txtNombreRepLegalJuridicoExistente.Text.Split();
                    int iDe = 0;
                    int iNombre = 0;
                    for (int i = 0; i < nombreCompleto.Length; i++)
                    {
                        if (nombreCompleto[i].ToString() == "DE")
                            iDe = -1;
                        else
                        {
                            if (iNombre == 0)
                                txtPrimerNombreNuevoAsegJuridico.Text = nombreCompleto[i + iDe].ToString();
                            if (iNombre == 1)
                                txtSegundoNombreNuevoAsegJuridico.Text = nombreCompleto[i + iDe].ToString();
                            if (iNombre == 2)
                                txtPrimerApellidoNuevoAsegJuridico.Text = nombreCompleto[i + iDe].ToString();
                            if (iNombre == 3)
                                txtSegundoApellidoNuevoAsegJuridico.Text = nombreCompleto[i + iDe].ToString();
                            /* if (iNombre == 4 ) -- Casada
                                 txtSegundoNombreNuevoAsegIndividual.Text = nombreCompleto[i + iDe].ToString();*/
                            iNombre++;
                        }


                    }                    
                    DateTime fechaNacimientoIndividual = Convert.ToDateTime(txtFechaConstitucionJuridicoExistente.Text);
                    txtFechaConstitucionNuevoAsegJuridico.Text = fechaNacimientoIndividual.ToString("dd/MM/yyyy");     
                    SetearCombo(cmbOrigenEmpresaNuevoAsegJuridico, cmbOrigenEmpresaJuridicoExistente.SelectedItem.Text);
                    chkRespPagoNuevoAsegJuridico.Visible = false;
                    lbResPago.Visible = false;
                    txtActividadEconomicaNuevoAsegJuridico.Text = txtActividadEconomicaJuridicoExistente.Text;
                    if (grvDireccionesJuridicoExistente.Rows.Count > 0)
                    {
                        //txtDireccionNuevoAsegJuridico.Text = grvDireccionesJuridicoExistente.Rows[0].Cells[0].Text.ToString();
                    }
                    if (grvTelefonosJuridicoExistente.Rows.Count > 0)
                    {
                        txtTelefonoNuevoAsegJuridico.Text = grvTelefonosJuridicoExistente.Rows[0].Cells[0].Text.ToString();
                    }
                    txtCorreoNuevoAsegJuridico.Text = txtCorreoJuridicoExistente.Text;
                    chkEsPepNuevoAsegJuridico.Visible = false;
                    chkRelPepNuevoAsegJuridico.Visible = false;
                    chkAsociadoPepNuevoAsegJuridico.Visible = false;
                    btnAgregarTelefonoNuevoAsegJuridico.Visible = false;
                    btnGuardarNuevoAsegJuridico.Text = "Enviar actualizacion";
                    pnlDatosClienteExistente.Visible = false;
                    pnlDatosClienteNuevo.Visible = true;
                    pnlNuevoAseguradoJuridico.Visible = true;
                    pnlNuevoAseguradoIndividual.Visible = false;
                    mostrarMensaje("Los datos del cliente están vencidos, por favor revisarlos y enviarlos para actualización.");
                }
            }
            else
                if (!chkRespPagoJuridicoExistente.Checked)
                {
                    guardarAseguradoJuridicoExistente();
                    mostrarMensaje("Los datos del asegurado se guardaron correctamente. Proceda a llenar los datos del responsable de pago.");
                    Response.Redirect("../Cliente/ResponsablePago.aspx");
                }
                else
                {
                    guardarAseguradoJuridicoExistente();
                    redireccionarPaginaBien();
                }
        }        
        private void guardarAseguradoIndividual()
        {
            Operaciones operacionesCliente = new Operaciones();
            EstructuraClienteIndividual nuevoCliente = new EstructuraClienteIndividual();
            DataTable telefonos = new DataTable();
            telefonos.Columns.Add("Telefono", typeof(string));

            //validacion de campos obligatorios

            if (txtPrimerNombreNuevoAsegIndividual.Text == string.Empty)
            {
                mostrarMensaje("El campo Primer Nombre es obligatorio.");
                txtPrimerNombreNuevoAsegIndividual.Focus();
                return;
            }

            if (txtPrimerApellidoNuevoAsegIndividual.Text == string.Empty)
            {
                mostrarMensaje("El campo Primer Apellido es obligatorio.");
                txtPrimerApellidoNuevoAsegIndividual.Focus();
                return;
            }

            if (txtFechaNacimientoNuevoAsegIndividual.Text == string.Empty)
            {
                mostrarMensaje("El campo Fecha Nacimiento es obligatorio.");
                txtFechaNacimientoNuevoAsegIndividual.Focus();
                return;
            }

            if (txtNumeroIdentificacionNuevoAsegIndividual.Text == string.Empty)
            {
                mostrarMensaje("El campo Número Identificación es obligatorio.");
                txtNumeroIdentificacionNuevoAsegIndividual.Focus();
                return;
            }

            if (txtCorreoNuevoAsegIndividual.Text == string.Empty)
            {
                mostrarMensaje("El campo Correo Electrónico es obligatorio.");
                txtCorreoNuevoAsegIndividual.Focus();
                return;
            }

            if (grvTelefonosNuevoAsegIndividual.Rows.Count == 0)
            {
                mostrarMensaje("Debe agregar al menos un número de teléfono.");
                txtTelefonoNuevoAsegIndividual.Focus();
                return;
            }

            //if (!chkEsPepIndividualExistente.Checked && !chkRelPepIndividualExistente.Checked && !chkAsociadoPepIndividualExistente.Checked && !chkNoPEP.Checked)
            //{
            //    mostrarMensaje("Debe de seleccionar al menos una opcion para persona expuesta políticamente");
            //    return;
            //}

            SeteaNuevoClienteIndvidual(ref nuevoCliente);

            for (int i = 0; i < grvTelefonosNuevoAsegIndividual.Rows.Count; i++)
            {
                telefonos.Rows.Add(grvTelefonosNuevoAsegIndividual.Rows[i].Cells[0].Text);
            }

            operacionesCliente.guardarDatosClienteIndividual(idCotizacion, "ASEG", cmbTipoCliente.SelectedValue, nuevoCliente, telefonos);
            
            if (!chkRespPagoNuevoAsegIndividual.Checked)
            {
                mostrarMensaje("Los datos del asegurado se guardaron correctamente. Proceda a llenar los datos del responsable de pago.");
                Response.Redirect("../Cliente/ResponsablePago.aspx");                
            }
            else
            {   
                operacionesCliente.actualizarEstadoCotizacion(idCotizacion);
                redireccionarPaginaBien();
            }
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
            //    mostrarMensaje("Debe de seleccionar al menos una opcion para persona expuesta políticamente");
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
            nuevoCliente.nit_individual = txtNumIdIndividualExistente.Text.Trim() +  txtDvIdIndividualExistente.Text.Trim();

            if (grvDireccionesIndividualExistente.Rows.Count > 0)
            {
                nuevoCliente.direccion_individual = grvDireccionesIndividualExistente.Rows[0].Cells[0].Text;
            }
            else
            {
                nuevoCliente.direccion_individual = "N/A";
            }

            if (chkActuaNombrePropioIndividualExistente.Checked)
                nuevoCliente.actua_nombre_propio_individual = "S";
            else
                nuevoCliente.actua_nombre_propio_individual = "N";

            if (chkRespPagoIndividualExistente.Checked)
                nuevoCliente.responsable_pago_individual = "S";
            else
                nuevoCliente.responsable_pago_individual = "N";

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

            if (!chkRespPagoIndividualExistente.Checked)            
                operacionesCliente.guardarDatosClienteIndividual(idCotizacion, "ASEG", cmbTipoCliente.SelectedValue, nuevoCliente, telefonos);             
            else
            {
                operacionesCliente.guardarDatosClienteIndividual(idCotizacion, "ASEG", cmbTipoCliente.SelectedValue, nuevoCliente, telefonos);
                operacionesCliente.actualizarEstadoCotizacion(idCotizacion);                
            }
        }
        private void guardarAseguradoJuridico()
        {
            Operaciones operacionesCliente = new Operaciones();
            EstructuraClienteJuridico nuevoCliente = new EstructuraClienteJuridico();
            DataTable telefonos = new DataTable();
            telefonos.Columns.Add("Telefono", typeof(string));

            //validacion de campos obligatorios

            if (txtNombreNuevoAsegJuridico.Text == string.Empty)
            {
                mostrarMensaje("El campo Nombre Jurídico es obligatorio.");
                txtNombreNuevoAsegJuridico.Focus();
                return;
            }

            if (txtNumIdNuevoAsegJuridico.Text == string.Empty || txtDvIdNuevoAsegJuridico.Text == string.Empty)
            {
                mostrarMensaje("Los campos del nit son obligatorios.");
                txtNumIdNuevoAsegJuridico.Focus();
                return;
            }

            if (txtPrimerNombreNuevoAsegJuridico.Text == string.Empty)
            {
                mostrarMensaje("El campo Primer Nombre es obligatorio.");
                txtPrimerNombreNuevoAsegJuridico.Focus();
                return;
            }

            if (txtPrimerApellidoNuevoAsegJuridico.Text == string.Empty)
            {
                mostrarMensaje("El campo Primer Apellido es obligatorio.");
                txtPrimerApellidoNuevoAsegJuridico.Focus();
                return;
            }

            if (txtNumeroIdentificacionNuevoAsegJuridico.Text == string.Empty)
            {
                mostrarMensaje("El campo Numero Identificación es obligatorio.");
                txtNumeroIdentificacionNuevoAsegJuridico.Focus();
                return;
            }

            if (txtFechaConstitucionNuevoAsegJuridico.Text == string.Empty)
            {
                mostrarMensaje("El campo Fecha Constitución es obligatorio.");
                txtFechaConstitucionNuevoAsegJuridico.Focus();
                return;
            }

            if (txtActividadEconomicaNuevoAsegJuridico.Text == string.Empty)
            {
                mostrarMensaje("El campo Actvidad Económica es obligatorio.");
                txtActividadEconomicaNuevoAsegJuridico.Focus();
                return;
            }

            if (txtCorreoNuevoAsegJuridico.Text == string.Empty)
            {
                mostrarMensaje("El campo Correo Electrónico es obligatorio.");
                txtCorreoNuevoAsegJuridico.Focus();
                return;
            }

            if (grvTelefonosNuevoAsegJuridico.Rows.Count == 0)
            {
                mostrarMensaje("Debe agregar al menos un número de teléfono.");
                txtTelefonoNuevoAsegJuridico.Focus();
                return;
            }


            //if (!chkEsPepIndividualExistente.Checked && !chkRelPepIndividualExistente.Checked && !chkAsociadoPepIndividualExistente.Checked && !chkNoPEP.Checked)
            //{
            //    mostrarMensaje("Debe de seleccionar al menos una opcion para persona expuesta políticamente");
            //    return;
            //}

            nuevoCliente.nombre_persona_juridica = txtNombreNuevoAsegJuridico.Text;
            //arreglo de nit juridico nuevo con el numid y dvid - Victoria Gutierrez 17/08/16 
            nuevoCliente.nit_persona_juridica = txtNumIdNuevoAsegJuridico.Text + txtDvIdNuevoAsegJuridico.Text;
            nuevoCliente.primer_nombre_representante = txtPrimerNombreNuevoAsegJuridico.Text;
            nuevoCliente.segundo_nombre_representante = txtSegundoNombreNuevoAsegJuridico.Text;
            nuevoCliente.primer_apellido_representante = txtPrimerApellidoNuevoAsegJuridico.Text;
            nuevoCliente.segundo_apellido_representante = txtSegundoApellidoNuevoAsegJuridico.Text;
            nuevoCliente.tipo_identificacion_representante = cmbTipoIdentificacionNuevoAsegJuridico.SelectedValue;
            nuevoCliente.numero_identificacion_representante = txtNumeroIdentificacionNuevoAsegJuridico.Text;
            //formateo de fecha para insertar en mysql
            DateTime fechaNacimientoIndividual = Convert.ToDateTime(txtFechaConstitucionNuevoAsegJuridico.Text);
            nuevoCliente.fecha_constitucion_empresa = fechaNacimientoIndividual.ToString("yyyy-MM-dd");
            nuevoCliente.pais_origen_empresa = cmbOrigenEmpresaNuevoAsegJuridico.SelectedValue;
            nuevoCliente.pais_emision_representante = cmbPaisEmisionNuevoAsegJuridico.SelectedValue;
            nuevoCliente.depto_emision_representante = cmbDeptoEmisionNuevoAsegJuridico.SelectedValue;
            nuevoCliente.muni_emision_representante = cmbMuniEmisionNuevoAsegJuridico.SelectedValue;

            nuevoCliente.actividad_economica_empresa = txtActividadEconomicaNuevoAsegJuridico.Text;
            nuevoCliente.direccion_empresa = unificarDireccionJuridico();
            nuevoCliente.pais_direccion_empresa = cmbPaisDireccionNuevoAsegJuridico.SelectedValue;
            nuevoCliente.depto_direccion_empresa = cmbDeptoDireccionNuevoAsegJuridico.SelectedValue;
            nuevoCliente.muni_direccion_empresa = cmbMuniDireccionNuevoAsegJuridico.SelectedValue;
            nuevoCliente.zona_direccion_empresa = cmbZonaDireccionNuevoAsegJuridico.SelectedValue;
            nuevoCliente.calle_direccion_empresa = txtCalleDireccionNuevoAsegJuridico.Text;
            nuevoCliente.avenida_direccion_empresa = txtAvenidaDireccionNuevoAsegJuridico.Text;
            nuevoCliente.numero_casa_direccion_empresa = txtNumCasaDireccionNuevoAsegJuridico.Text;
            nuevoCliente.colonia_direccion_empresa = txtColoniaDireccionNuevoAsegJuridico.Text;
            nuevoCliente.edificio_direccion_empresa = txtEdificioDireccionNuevoAsegJuridico.Text;
            nuevoCliente.lote_direccion_empresa = txtLoteDireccionNuevoAsegJuridico.Text;
            nuevoCliente.sector_direccion_empresa = txtSectorDireccionNuevoAsegJuridico.Text;
            nuevoCliente.manzana_direccion_empresa = txtManzanaDireccionNuevoAsegJuridico.Text;

            if (chkEsPepNuevoAsegJuridico.Checked)
                nuevoCliente.esPep_juridico = "S";
            else
                nuevoCliente.esPep_juridico = "N";

            if (chkRelPepNuevoAsegJuridico.Checked)
                nuevoCliente.relacionPep_juridico = "S";
            else
                nuevoCliente.relacionPep_juridico = "N";

            if (chkAsociadoPepNuevoAsegJuridico.Checked)
                nuevoCliente.asociadoPep_juridico = "S";
            else
                nuevoCliente.asociadoPep_juridico = "N";

            nuevoCliente.correo_electronico_juridico = txtCorreoNuevoAsegJuridico.Text;

            for (int i = 0; i < grvTelefonosNuevoAsegJuridico.Rows.Count; i++)
            {
                telefonos.Rows.Add(grvTelefonosNuevoAsegJuridico.Rows[i].Cells[0].Text);
            }

            operacionesCliente.guardarDatosClienteJuridico(idCotizacion, "ASEG", cmbTipoCliente.SelectedValue, nuevoCliente, telefonos);
            
            if (!chkRespPagoNuevoAsegJuridico.Checked)
            {
                mostrarMensaje("Los datos del asegurado se guardaron correctamente. Proceda a llenar los datos del responsable de pago.");
                Response.Redirect("../Cliente/ResponsablePago.aspx");
            }
            else
            {
                operacionesCliente.actualizarEstadoCotizacion(idCotizacion);
                redireccionarPaginaBien();
            }
        }
        private void guardarAseguradoJuridicoExistente()
        {
            Operaciones operacionesCliente = new Operaciones();
            EstructuraClienteJuridico nuevoCliente = new EstructuraClienteJuridico();
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
            //    mostrarMensaje("Debe de seleccionar al menos una opcion para persona expuesta políticamente");
            //    return;
            //}

            nuevoCliente.nombre_persona_juridica = txtNombrePersonaJuridicaJuridicoExistente.Text;
            nuevoCliente.nit_persona_juridica = txtNumIdPersonaJuridicaJuridicoExistente.Text.Trim() + txtDvIdPersonaJuridicaJuridicoExistente.Text.Trim();
            nuevoCliente.primer_nombre_representante = primerNombre;
            nuevoCliente.segundo_nombre_representante = segundoNombre;
            nuevoCliente.primer_apellido_representante = primerApellido;
            nuevoCliente.segundo_apellido_representante = segundoApellido;

            //formateo de fecha para insertar en mysql
            DateTime fechaNacimientoIndividual = Convert.ToDateTime(txtFechaConstitucionJuridicoExistente.Text);
            nuevoCliente.fecha_constitucion_empresa = fechaNacimientoIndividual.ToString("yyyy-MM-dd");
            nuevoCliente.pais_origen_empresa = cmbOrigenEmpresaJuridicoExistente.SelectedValue;

            if (chkRespPagoNuevoAsegJuridico.Checked)
            {

            }

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

            for (int i = 0; i < grvTelefonosJuridicoExistente.Rows.Count; i++)
            {
                telefonos.Rows.Add(grvTelefonosJuridicoExistente.Rows[i].Cells[0].Text);
            }

            operacionesCliente.guardarDatosClienteJuridico(idCotizacion, "ASEG", cmbTipoCliente.SelectedValue, nuevoCliente, telefonos);
            
            if (chkRespPagoJuridicoExistente.Checked)
            {
                operacionesCliente.actualizarEstadoCotizacion(idCotizacion);
            }
        }
        private void SetearClienteJuridico (ref EstructuraClienteJuridico nuevoCliente)
        {
            nuevoCliente.nombre_persona_juridica = txtNombreNuevoAsegJuridico.Text;
            //arreglo de nit juridico nuevo con el numid y dvid - Victoria Gutierrez 17/08/16 
            nuevoCliente.nit_persona_juridica = txtNumIdNuevoAsegJuridico.Text + txtDvIdNuevoAsegJuridico.Text;
            nuevoCliente.primer_nombre_representante = txtPrimerNombreNuevoAsegJuridico.Text;
            nuevoCliente.segundo_nombre_representante = txtSegundoNombreNuevoAsegJuridico.Text;
            nuevoCliente.primer_apellido_representante = txtPrimerApellidoNuevoAsegJuridico.Text;
            nuevoCliente.segundo_apellido_representante = txtSegundoApellidoNuevoAsegJuridico.Text;
            nuevoCliente.tipo_identificacion_representante = cmbTipoIdentificacionNuevoAsegJuridico.SelectedValue;
            nuevoCliente.numero_identificacion_representante = txtNumeroIdentificacionNuevoAsegJuridico.Text;
            //formateo de fecha para insertar en mysql
            DateTime fechaNacimientoIndividual = Convert.ToDateTime(txtFechaConstitucionNuevoAsegJuridico.Text);
            nuevoCliente.fecha_constitucion_empresa = fechaNacimientoIndividual.ToString("yyyy-MM-dd");
            nuevoCliente.pais_origen_empresa = cmbOrigenEmpresaNuevoAsegJuridico.SelectedValue;
            nuevoCliente.pais_emision_representante = cmbPaisEmisionNuevoAsegJuridico.SelectedValue;
            nuevoCliente.depto_emision_representante = cmbDeptoEmisionNuevoAsegJuridico.SelectedValue;
            nuevoCliente.muni_emision_representante = cmbMuniEmisionNuevoAsegJuridico.SelectedValue;


            nuevoCliente.actividad_economica_empresa = txtActividadEconomicaNuevoAsegJuridico.Text;
           // nuevoCliente.direccion_empresa = txtDireccionNuevoAsegJuridico.Text;
            nuevoCliente.pais_direccion_empresa = cmbPaisDireccionNuevoAsegJuridico.SelectedValue;
            nuevoCliente.depto_direccion_empresa = cmbDeptoDireccionNuevoAsegJuridico.SelectedValue;
            nuevoCliente.muni_direccion_empresa = cmbMuniDireccionNuevoAsegJuridico.SelectedValue;

            if (chkEsPepNuevoAsegJuridico.Checked)
                nuevoCliente.esPep_juridico = "S";
            else
                nuevoCliente.esPep_juridico = "N";

            if (chkRelPepNuevoAsegJuridico.Checked)
                nuevoCliente.relacionPep_juridico = "S";
            else
                nuevoCliente.relacionPep_juridico = "N";

            if (chkAsociadoPepNuevoAsegJuridico.Checked)
                nuevoCliente.asociadoPep_juridico = "S";
            else
                nuevoCliente.asociadoPep_juridico = "N";

            nuevoCliente.correo_electronico_juridico = txtCorreoNuevoAsegJuridico.Text;
        }
        private void SeteaNuevoClienteIndvidual(ref EstructuraClienteIndividual nuevoCliente)
        {
            nuevoCliente.primer_nombre_individual = txtPrimerNombreNuevoAsegIndividual.Text;
            nuevoCliente.segundo_nombre_individual = txtSegundoNombreNuevoAsegIndividual.Text;
            nuevoCliente.primer_apellido_individual = txtPrimerApellidoNuevoAsegIndividual.Text;
            nuevoCliente.segundo_apellido_individual = txtSegundoApellidoNuevoAsegIndividual.Text;

            //formateo de fecha para insertar en mysql
            DateTime fechaNacimientoIndividual = Convert.ToDateTime(txtFechaNacimientoNuevoAsegIndividual.Text);
            nuevoCliente.fecha_nacimiento_individual = fechaNacimientoIndividual.ToString("yyyy-MM-dd");

            nuevoCliente.genero_individual = cmbGeneroNuevoAsegIndividual.SelectedValue;
            nuevoCliente.estado_civil_individual = cmbEstadoCivilNuevoAsegIndividual.SelectedValue;
            nuevoCliente.profesion_individual = cmbProfesionNuevoAsegIndividual.SelectedValue;
            nuevoCliente.nacionalidad_individual = cmbNacionalidadNuevoAsegIndividual.SelectedValue;
            nuevoCliente.tipo_identificacion_individual = cmbTipoIdentificacionNuevoAsegIndividual.SelectedValue;
            nuevoCliente.numero_identificacion_individual = txtNumeroIdentificacionNuevoAsegIndividual.Text;
            nuevoCliente.pais_emision_individual = cmbPaisEmisionNuevoAsegIndividual.SelectedValue;
            nuevoCliente.depto_emision_individual = cmbDeptoEmisionNuevoAsegIndividual.SelectedValue;
            nuevoCliente.muni_emision_individual = cmbMuniEmisionNuevoAsegIndividual.SelectedValue;
            nuevoCliente.nombre_negocio_individual = txtNegocioPropioNuevoAsegIndividual.Text;
            nuevoCliente.nit_individual = txtNumIdNuevoAsegIndividual.Text.Trim() + txtDvIdNuevoAsegIndividual.Text.Trim();

            if (chkActuaNombrePropioNuevoAsegIndividual.Checked)
                nuevoCliente.actua_nombre_propio_individual = "S";
            else
                nuevoCliente.actua_nombre_propio_individual = "N";

            if (chkRespPagoNuevoAsegIndividual.Checked)
                nuevoCliente.responsable_pago_individual = "S";
            else
                nuevoCliente.responsable_pago_individual = "N";

            nuevoCliente.direccion_individual = unificarDireccionIndividual();
            nuevoCliente.pais_direccion_individual = cmbPaisDireccionNuevoAsegIndividual.SelectedValue;
            nuevoCliente.depto_direccion_individual = cmbDeptoDireccionNuevoAsegIndividual.SelectedValue;
            nuevoCliente.muni_direccion_individual = cmbMuniDireccionNuevoAsegIndividual.SelectedValue;
            nuevoCliente.zona_direccion_individual = cmbZonaDireccionNuevoAsegIndividual.SelectedValue;
            nuevoCliente.calle_direccion_individual = txtCalleDireccionNuevoAsegIndividual.Text;
            nuevoCliente.avenida_direccion_individual = txtAvenidaDireccionNuevoAsegIndividual.Text;
            nuevoCliente.numero_casa_direccion_individual = txtNumCasaDireccionNuevoAsegIndividual.Text;
            nuevoCliente.colonia_direccion_individual = txtColoniaDireccionNuevoAsegIndividual.Text;
            nuevoCliente.edificio_direccion_individual = txtEdificioDireccionNuevoAsegIndividual.Text;
            nuevoCliente.sector_direccion_individual = txtSectorDireccionNuevoAsegIndividual.Text;
            nuevoCliente.lote_direccion_individual = txtLoteDireccionNuevoAsegIndividual.Text;
            nuevoCliente.manzana_direccion_individual = txtManzanaDireccionNuevoAsegIndividual.Text;

            if (chkEsPepNuevoAsegIndividual.Checked)
                nuevoCliente.esPep_individual = "S";
            else
                nuevoCliente.esPep_individual = "N";

            if (chkRelPepNuevoAsegIndividual.Checked)
                nuevoCliente.relacionPep_individual = "S";
            else
                nuevoCliente.relacionPep_individual = "N";

            if (chkAsociadoPepNuevoAsegIndividual.Checked)
                nuevoCliente.asociadoPep_individual = "S";
            else
                nuevoCliente.asociadoPep_individual = "N";

            nuevoCliente.correo_electronico_individual = txtCorreoNuevoAsegIndividual.Text;
        }
        private void SetearCombo(DropDownList ddCombo, string Texto)
        {
            for (int i = 0; i < ddCombo.Items.Count; i++)
            {
                ddCombo.SelectedIndex = i;
                if (ddCombo.SelectedItem.Text == Texto)
                    break;
            }
        }

        #region DIRECCIONESCLIENTE

        protected void btnAgregarDireccionNuevoAsegIndividual_Click(object sender, EventArgs e)
        {
            //datosDirecciones = ViewState["datosDirecciones"] as DataTable;

            ////if (txtDireccionNuevoAsegIndividual.Text != string.Empty)
            ////{
            //DataRow fila = datosDirecciones.NewRow();

            ////fila["Direccion"] = txtDireccionNuevoAsegIndividual.Text;
            //fila["Pais"] = cmbPaisDireccionNuevoAsegIndividual.SelectedItem.Text;
            //fila["Departamento"] = cmbDeptoDireccionNuevoAsegIndividual.SelectedItem.Text;
            //fila["Municipio"] = cmbMuniDireccionNuevoAsegIndividual.SelectedItem.Text;
            //fila["Tipo"] = cmbTipoDireccionNuevoAsegIndividual.SelectedItem.Text;
            //fila["codPais"] = cmbPaisDireccionNuevoAsegIndividual.SelectedValue;
            //fila["codEstado"] = cmbDeptoDireccionNuevoAsegIndividual.SelectedValue;
            //fila["codCiudad"] = cmbMuniDireccionNuevoAsegIndividual.SelectedValue;
            //fila["codLval"] = cmbTipoDireccionNuevoAsegIndividual.SelectedValue;

            //datosDirecciones.Rows.Add(fila);

            //grvDireccionesNuevoAsegIndividual.DataSource = datosDirecciones;
            //grvDireccionesNuevoAsegIndividual.DataBind();
            ////}
        }
        protected void grvDireccionesNuevoAsegIndividual_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            datosDirecciones = ViewState["datosDirecciones"] as DataTable;

            if (e.CommandName == "Borrar")
            {
                GridView Direcciones = (GridView)sender;
                //Direcciones.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text

                var id = Convert.ToInt32(e.CommandArgument);

                DataRow dr = datosDirecciones.Rows[id];
                dr.Delete();

                Direcciones.DataSource = datosDirecciones;
                Direcciones.DataBind();
            }
        }

        #endregion

        #region TELEFONOSCLIENTE
        protected void btnAgregarTelefonoNuevoAsegIndivifual_Click(object sender, EventArgs e)
        {
            datosTelefonos = ViewState["datosTelefonos"] as DataTable;

            if (datosTelefonos.Rows.Count < 4)
            {
                if (txtTelefonoNuevoAsegIndividual.Text != string.Empty)
                {
                    if (txtTelefonoNuevoAsegIndividual.Text.Length != 8)
                    {

                        mostrarMensajeJavaScript("Ingrese un número teléfonico valido.");
                        return;
                    }

                    datosTelefonos.Rows.Add(txtTelefonoNuevoAsegIndividual.Text);
                    grvTelefonosNuevoAsegIndividual.DataSource = datosTelefonos;
                    grvTelefonosNuevoAsegIndividual.DataBind();
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
        protected void btnAgregarTelefonoNuevoAsegJuridico_Click(object sender, EventArgs e)
        {
            datosTelefonos = ViewState["datosTelefonos"] as DataTable;

            if (datosTelefonos.Rows.Count < 4)
            {
                if (txtTelefonoNuevoAsegJuridico.Text != string.Empty)
                {
                    if (txtTelefonoNuevoAsegJuridico.Text.Length != 8)
                    {
                        mostrarMensajeJavaScript("Ingrese un número teléfonico valido.");
                        return;
                    }

                    datosTelefonos.Rows.Add(txtTelefonoNuevoAsegJuridico.Text);
                    grvTelefonosNuevoAsegJuridico.DataSource = datosTelefonos;
                    grvTelefonosNuevoAsegJuridico.DataBind();
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

                cmbDeptoEmisionIndividualExistente_SelectedIndexChanged(cmbDeptoEmisionIndividualExistente, new EventArgs());
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

            DataRow[] municipios = datosMunicipios.Select("CODPAIS = " + cmbPaisEmisionIndividualExistente.SelectedValue + "AND CODESTADO = '" + cmbDeptoEmisionIndividualExistente.SelectedValue + "'");

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
        protected void cmbPaisEmisionNuevoAsegIndividual_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosDepartamentos = ViewState["datosDepartamentos"] as DataTable;
            datosPais = ViewState["datosPais"] as DataTable;

            DataRow[] departamentos = datosDepartamentos.Select("CODPAIS = " + cmbPaisEmisionNuevoAsegIndividual.SelectedValue);

            if (departamentos.Count() > 1)
            {
                datosDepartamentos = departamentos.CopyToDataTable();

                cmbDeptoEmisionNuevoAsegIndividual.Visible = true;
                cmbMuniEmisionNuevoAsegIndividual.Visible = true;
                cmbDeptoEmisionNuevoAsegIndividual.DataSource = datosDepartamentos;
                cmbDeptoEmisionNuevoAsegIndividual.DataTextField = "DESCESTADO";
                cmbDeptoEmisionNuevoAsegIndividual.DataValueField = "CODESTADO";
                cmbDeptoEmisionNuevoAsegIndividual.DataBind();

                cmbDeptoEmisionNuevoAsegIndividual_SelectedIndexChanged(cmbDeptoEmisionNuevoAsegIndividual, new EventArgs());
            }
            else
            {
                cmbDeptoEmisionNuevoAsegIndividual.Visible = false;
                cmbMuniEmisionNuevoAsegIndividual.Visible = false;
            }
        }
        protected void cmbDeptoEmisionNuevoAsegIndividual_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosMunicipios = ViewState["datosMunicipios"] as DataTable;

            DataRow[] municipios = datosMunicipios.Select("CODPAIS = " + cmbPaisEmisionNuevoAsegIndividual.SelectedValue + "AND CODESTADO = '" + cmbDeptoEmisionNuevoAsegIndividual.SelectedValue + "'");

            if (municipios.Count() > 1)
            {
                datosMunicipios = municipios.CopyToDataTable();

                cmbMuniEmisionNuevoAsegIndividual.Visible = true;
                cmbMuniEmisionNuevoAsegIndividual.DataSource = datosMunicipios;
                cmbMuniEmisionNuevoAsegIndividual.DataTextField = "DESCCIUDAD";
                cmbMuniEmisionNuevoAsegIndividual.DataValueField = "CODCIUDAD";
                cmbMuniEmisionNuevoAsegIndividual.DataBind();
            }
            else
            {
                cmbMuniEmisionNuevoAsegIndividual.Visible = false;
            }
        }
        protected void cmbPaisDireccionNuevoAsegIndividual_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosDepartamentos = ViewState["datosDepartamentos"] as DataTable;
            datosPais = ViewState["datosPais"] as DataTable;

            DataRow[] departamentos = datosDepartamentos.Select("CODPAIS = " + cmbPaisDireccionNuevoAsegIndividual.SelectedValue);

            if (departamentos.Count() > 1)
            {
                datosDepartamentos = departamentos.CopyToDataTable();

                cmbDeptoDireccionNuevoAsegIndividual.Visible = true;
                cmbMuniDireccionNuevoAsegIndividual.Visible = true;
                cmbDeptoDireccionNuevoAsegIndividual.DataSource = datosDepartamentos;
                cmbDeptoDireccionNuevoAsegIndividual.DataTextField = "DESCESTADO";
                cmbDeptoDireccionNuevoAsegIndividual.DataValueField = "CODESTADO";
                cmbDeptoDireccionNuevoAsegIndividual.DataBind();

                cmbDeptoDireccionNuevoAsegIndividual_SelectedIndexChanged(cmbDeptoDireccionNuevoAsegIndividual, new EventArgs());
            }
            else
            {
                cmbDeptoDireccionNuevoAsegIndividual.Visible = false;
                cmbMuniDireccionNuevoAsegIndividual.Visible = false;
            }
        }
        protected void cmbDeptoDireccionNuevoAsegIndividual_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosMunicipios = ViewState["datosMunicipios"] as DataTable;

            DataRow[] municipios = datosMunicipios.Select("CODPAIS = " + cmbPaisDireccionNuevoAsegIndividual.SelectedValue + "AND CODESTADO = '" + cmbDeptoDireccionNuevoAsegIndividual.SelectedValue + "'");

            if (municipios.Count() > 1)
            {
                datosMunicipios = municipios.CopyToDataTable();

                cmbMuniDireccionNuevoAsegIndividual.Visible = true;
                cmbMuniDireccionNuevoAsegIndividual.DataSource = datosMunicipios;
                cmbMuniDireccionNuevoAsegIndividual.DataTextField = "DESCCIUDAD";
                cmbMuniDireccionNuevoAsegIndividual.DataValueField = "CODCIUDAD";
                cmbMuniDireccionNuevoAsegIndividual.DataBind();

                cmbMuniDireccionNuevoAsegIndividual_SelectedIndexChanged(cmbMuniDireccionNuevoAsegIndividual, new EventArgs());
            }
            else
            {
                cmbMuniDireccionNuevoAsegIndividual.Visible = false;
            }
        }
        protected void cmbPaisEmisionNuevoAsegJuridico_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosDepartamentos = ViewState["datosDepartamentos"] as DataTable;
            datosPais = ViewState["datosPais"] as DataTable;

            DataRow[] departamentos = datosDepartamentos.Select("CODPAIS = " + cmbPaisEmisionNuevoAsegJuridico.SelectedValue);

            if (departamentos.Count() > 1)
            {
                datosDepartamentos = departamentos.CopyToDataTable();

                cmbDeptoEmisionNuevoAsegJuridico.Visible = true;
                cmbMuniEmisionNuevoAsegJuridico.Visible = true;
                cmbDeptoEmisionNuevoAsegJuridico.DataSource = datosDepartamentos;
                cmbDeptoEmisionNuevoAsegJuridico.DataTextField = "DESCESTADO";
                cmbDeptoEmisionNuevoAsegJuridico.DataValueField = "CODESTADO";
                cmbDeptoEmisionNuevoAsegJuridico.DataBind();

                cmbDeptoEmisionNuevoAsegJuridico_SelectedIndexChanged(cmbDeptoEmisionNuevoAsegJuridico, new EventArgs());
            }
            else
            {
                cmbDeptoEmisionNuevoAsegJuridico.Visible = false;
                cmbMuniEmisionNuevoAsegJuridico.Visible = false;
            }
        }
        protected void cmbDeptoEmisionNuevoAsegJuridico_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosMunicipios = ViewState["datosMunicipios"] as DataTable;

            DataRow[] municipios = datosMunicipios.Select("CODPAIS = " + cmbPaisEmisionNuevoAsegJuridico.SelectedValue + "AND CODESTADO = '" + cmbDeptoEmisionNuevoAsegJuridico.SelectedValue + "'");

            if (municipios.Count() > 1)
            {
                datosMunicipios = municipios.CopyToDataTable();

                cmbMuniEmisionNuevoAsegJuridico.Visible = true;
                cmbMuniEmisionNuevoAsegJuridico.DataSource = datosMunicipios;
                cmbMuniEmisionNuevoAsegJuridico.DataTextField = "DESCCIUDAD";
                cmbMuniEmisionNuevoAsegJuridico.DataValueField = "CODCIUDAD";
                cmbMuniEmisionNuevoAsegJuridico.DataBind();
            }
            else
            {
                cmbMuniEmisionNuevoAsegJuridico.Visible = false;
            }
        }
        protected void cmbPaisDireccionNuevoAsegJuridico_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosDepartamentos = ViewState["datosDepartamentos"] as DataTable;
            datosPais = ViewState["datosPais"] as DataTable;

            DataRow[] departamentos = datosDepartamentos.Select("CODPAIS = " + cmbPaisDireccionNuevoAsegJuridico.SelectedValue);

            if (departamentos.Count() > 1)
            {
                datosDepartamentos = departamentos.CopyToDataTable();

                cmbDeptoDireccionNuevoAsegJuridico.Visible = true;
                cmbMuniDireccionNuevoAsegJuridico.Visible = true;
                cmbDeptoDireccionNuevoAsegJuridico.DataSource = datosDepartamentos;
                cmbDeptoDireccionNuevoAsegJuridico.DataTextField = "DESCESTADO";
                cmbDeptoDireccionNuevoAsegJuridico.DataValueField = "CODESTADO";
                cmbDeptoDireccionNuevoAsegJuridico.DataBind();

                cmbDeptoDireccionNuevoAsegJuridico_SelectedIndexChanged(cmbDeptoDireccionNuevoAsegJuridico, new EventArgs());
            }
            else
            {
                cmbDeptoDireccionNuevoAsegJuridico.Visible = false;
                cmbMuniDireccionNuevoAsegJuridico.Visible = false;
            }
        }
        protected void cmbDeptoDireccionNuevoAsegJuridico_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosMunicipios = ViewState["datosMunicipios"] as DataTable;

            DataRow[] municipios = datosMunicipios.Select("CODPAIS = " + cmbPaisDireccionNuevoAsegJuridico.SelectedValue + "AND CODESTADO = '" + cmbDeptoDireccionNuevoAsegJuridico.SelectedValue + "'");

            if (municipios.Count() > 1)
            {
                datosMunicipios = municipios.CopyToDataTable();

                cmbMuniDireccionNuevoAsegJuridico.Visible = true;
                cmbMuniDireccionNuevoAsegJuridico.DataSource = datosMunicipios;
                cmbMuniDireccionNuevoAsegJuridico.DataTextField = "DESCCIUDAD";
                cmbMuniDireccionNuevoAsegJuridico.DataValueField = "CODCIUDAD";
                cmbMuniDireccionNuevoAsegJuridico.DataBind();

                cmbMuniDireccionNuevoAsegJuridico_SelectedIndexChanged(cmbMuniDireccionNuevoAsegJuridico, new EventArgs());
            }
            else
            {
                cmbMuniDireccionNuevoAsegJuridico.Visible = false;
            }
        }
        protected void cmbMuniDireccionNuevoAsegIndividual_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosZonas = ViewState["datosZonas"] as DataTable;

            DataRow[] zonas = datosZonas.Select("CODPAIS = " + cmbPaisDireccionNuevoAsegIndividual.SelectedValue + "AND CODESTADO = '" + cmbDeptoDireccionNuevoAsegIndividual.SelectedValue + "'" + "AND CODCIUDAD = '" + cmbMuniDireccionNuevoAsegIndividual.SelectedValue + "'");

            if (zonas.Count() > 1)
            {
                datosZonas = zonas.CopyToDataTable();

                cmbZonaDireccionNuevoAsegIndividual.DataSource = null;
                cmbZonaDireccionNuevoAsegIndividual.Visible = true;
                cmbZonaDireccionNuevoAsegIndividual.DataSource = datosZonas;
                cmbZonaDireccionNuevoAsegIndividual.DataTextField = "DESCMUNICIPIO";
                cmbZonaDireccionNuevoAsegIndividual.DataValueField = "CODMUNICIPIO";
                cmbZonaDireccionNuevoAsegIndividual.DataBind();
            }
            else
            {
                cmbZonaDireccionNuevoAsegIndividual.Visible = false;
            }
        }
        protected void cmbMuniDireccionNuevoAsegJuridico_SelectedIndexChanged(object sender, EventArgs e)
        {
            datosZonas = ViewState["datosZonas"] as DataTable;

            DataRow[] zonas = datosZonas.Select("CODPAIS = " + cmbPaisDireccionNuevoAsegJuridico.SelectedValue + "AND CODESTADO = '" + cmbDeptoDireccionNuevoAsegJuridico.SelectedValue + "'" + "AND CODCIUDAD = '" + cmbMuniDireccionNuevoAsegJuridico.SelectedValue + "'");

            if (zonas.Count() > 1)
            {
                datosZonas = zonas.CopyToDataTable();

                cmbZonaDireccionNuevoAsegJuridico.DataSource = null;
                cmbZonaDireccionNuevoAsegJuridico.Visible = true;
                cmbZonaDireccionNuevoAsegJuridico.DataSource = datosZonas;
                cmbZonaDireccionNuevoAsegJuridico.DataTextField = "DESCMUNICIPIO";
                cmbZonaDireccionNuevoAsegJuridico.DataValueField = "CODMUNICIPIO";
                cmbZonaDireccionNuevoAsegJuridico.DataBind();
            }
            else
            {
                cmbZonaDireccionNuevoAsegJuridico.Visible = false;
            }
        } 
        #endregion

        #region FUNCIONALIDADPEP
        protected void chkEsPepNuevoAsegIndividual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEsPepNuevoAsegIndividual.Checked)
            {
                pnlPepNuevoIndividual.Visible = true;
            }
            else
            {
                if (!chkRelPepNuevoAsegIndividual.Checked && !chkAsociadoPepNuevoAsegIndividual.Checked)
                {
                    pnlPepNuevoIndividual.Visible = false;
                }
            }
        }
        protected void chkRelPepNuevoAsegIndividual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRelPepNuevoAsegIndividual.Checked)
            {
                pnlPepNuevoIndividual.Visible = true;
            }
            else
            {
                if (!chkAsociadoPepNuevoAsegIndividual.Checked && !chkEsPepNuevoAsegIndividual.Checked)
                {
                    pnlPepNuevoIndividual.Visible = false;
                }
            }

        }
        protected void chkAsociadoPepNuevoAsegIndividual_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAsociadoPepNuevoAsegIndividual.Checked)
            {
                pnlPepNuevoIndividual.Visible = true;
            }
            else
            {
                if (!chkEsPepNuevoAsegIndividual.Checked && !chkRelPepNuevoAsegIndividual.Checked)
                {
                    pnlPepNuevoIndividual.Visible = false;
                }
            }
        }
        protected void chkEsPepNuevoAsegJuridico_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEsPepNuevoAsegJuridico.Checked)
            {
                pnlPepNuevoJuridico.Visible = true;
            }
            else
            {
                if (!chkRelPepNuevoAsegJuridico.Checked && !chkAsociadoPepNuevoAsegJuridico.Checked)
                {
                    pnlPepNuevoJuridico.Visible = false;
                }
            }
        }
        protected void chkRelPepNuevoAsegJuridico_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRelPepNuevoAsegJuridico.Checked)
            {
                pnlPepNuevoJuridico.Visible = true;
            }
            else
            {
                if (!chkEsPepNuevoAsegJuridico.Checked && !chkAsociadoPepNuevoAsegJuridico.Checked)
                {
                    pnlPepNuevoJuridico.Visible = false;
                }
            }
        }
        protected void chkAsociadoPepNuevoAsegJuridico_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAsociadoPepNuevoAsegJuridico.Checked)
            {
                pnlPepNuevoJuridico.Visible = true;
            }
            else
            {
                if (!chkEsPepNuevoAsegJuridico.Checked && !chkRelPepNuevoAsegJuridico.Checked)
                {
                    pnlPepNuevoJuridico.Visible = false;
                }
            }
        }
        #endregion
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            redireccionarPaginaBien();
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
        private void mostrarMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", "alert('" + mensaje + "');", true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        private void mostrarMensajeJavaScript(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        protected void btnNoEnviarActualizacion_Click(object sender, EventArgs e)
        {
            if (!chkRespPagoIndividualExistente.Checked)
            {
                mostrarMensaje("Los datos del asegurado se enviaron para actualizacion. Proceda a llenar los datos del responsable de pago.");
                Response.Redirect("../Cliente/ResponsablePago.aspx");
            }
            else
            {
                redireccionarPaginaBien();
            }
        }
        protected void btbNoEnviarActualizacionJuridico_Click(object sender, EventArgs e)
        {
            if (!chkRespPagoJuridicoExistente.Checked)
            {
                mostrarMensaje("Los datos del asegurado se enviaron para actualizacion. Proceda a llenar los datos del responsable de pago.");
                Response.Redirect("../Cliente/ResponsablePago.aspx");
            }
            else
            {
                redireccionarPaginaBien();
            }
        }
        private string unificarDireccionIndividual()
        {
            string resultado = string.Empty;

            resultado = txtCalleDireccionNuevoAsegIndividual.Text + " " + txtAvenidaDireccionNuevoAsegIndividual.Text 
                + " " + txtNumCasaDireccionNuevoAsegIndividual.Text + " " + cmbZonaDireccionNuevoAsegIndividual.SelectedItem.Text + " " + txtColoniaDireccionNuevoAsegIndividual.Text + " " +
                txtEdificioDireccionNuevoAsegIndividual.Text;

            return resultado;
        }
        private string unificarDireccionJuridico()
        {
            string resultado = string.Empty;

            resultado = txtCalleDireccionNuevoAsegJuridico.Text + " " + txtAvenidaDireccionNuevoAsegJuridico.Text
               + " " + txtNumCasaDireccionNuevoAsegJuridico.Text + " " + cmbZonaDireccionNuevoAsegIndividual.SelectedItem.Text + " " + txtColoniaDireccionNuevoAsegJuridico.Text + " " +
               txtEdificioDireccionNuevoAsegJuridico.Text;

            return resultado;
        }
        private string enviarCorreoElectronico(string cTipoCliente)
        {
            string direccionSalida = ConfigurationSettings.AppSettings["direccionSalidaMensajes"];
            string passwordDireccionSalida = ConfigurationSettings.AppSettings["passwordDireccionSalidaMensajes"];
            string servidorCorreo = ConfigurationSettings.AppSettings["servidorCorreo"];
            string correoOrigen = ConfigurationSettings.AppSettings["correoOrigen"];
            string respuesta = string.Empty;
            string correosDestinatarios = string.Empty;
            Consultas clsConsulta = new Consultas();
            correosDestinatarios = clsConsulta.DatosCorreos(1);

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(correoOrigen);
            msg.To.Add(correosDestinatarios);
            //msg.CC.Add(correosDestinatarios);
            msg.Body = " Se ha enviado informacion del siguiente cliente para ser actualizada: <br> <br>";
            msg.Body = msg.Body + "Codigo de Cliente: " + ViewState["CodCliOracle"].ToString() + "<br>";
            msg.Body = msg.Body + "Tipo de Cliente: " + (cTipoCliente == "P" ? "Persona Individual" : "Persona Juridica") + "<br>";
            msg.Body = msg.Body + "Nit: " + txtNumIDCliente.Text + txtDvIdCliente.Text + "<br>";
            if (cTipoCliente == "P")
            {
                msg.Body = msg.Body + "Nombre: " + txtPrimerNombreNuevoAsegIndividual.Text + " " + txtSegundoNombreNuevoAsegIndividual.Text + " " + txtPrimerApellidoNuevoAsegIndividual.Text + " " + txtSegundoApellidoNuevoAsegIndividual.Text + "<br>";
                msg.Body = msg.Body + "Identificacion: " + cmbTipoIdentificacionNuevoAsegIndividual.SelectedItem.Text + " - " + txtNumeroIdentificacionNuevoAsegIndividual.Text + "<br>";
                msg.Body = msg.Body + "Genero: " + cmbGeneroNuevoAsegIndividual.SelectedItem.Text + "<br>";
                msg.Body = msg.Body + "Nacionalidad: " + cmbNacionalidadNuevoAsegIndividual.SelectedItem.Text + "<br>";
                msg.Body = msg.Body + "Estado Civil: " + cmbEstadoCivilNuevoAsegIndividual.SelectedItem.Text + "<br>";
                msg.Body = msg.Body + "Fecha Nacimiento: " + txtFechaNacimientoNuevoAsegIndividual.Text + "<br>";
                //msg.Body = msg.Body + "Direccion: " + txtDireccionNuevoAsegIndividual.Text + "<br>";
                msg.Body = msg.Body + "Telefono: " + txtTelefonoNuevoAsegIndividual.Text + "<br>";
                msg.Body = msg.Body + "Correo Electronico: " + txtCorreoNuevoAsegIndividual.Text + "<br>";
            }
            else
            {
                msg.Body = msg.Body + "Nombre: " + txtNombreNuevoAsegJuridico.Text + "<br>";
                msg.Body = msg.Body + "Fecha Constitucion: " + txtFechaConstitucionNuevoAsegJuridico.Text + "<br>";
                msg.Body = msg.Body + "Origen de la empresa: " + cmbOrigenEmpresaNuevoAsegJuridico.SelectedItem.Text + "<br>" ;
                msg.Body = msg.Body + "Representante Legal: " + txtPrimerNombreNuevoAsegJuridico.Text + " " + txtSegundoNombreNuevoAsegJuridico.Text + " " + txtPrimerApellidoNuevoAsegJuridico.Text + " " + txtSegundoApellidoNuevoAsegJuridico.Text + "<br>";
                msg.Body = msg.Body + "Identificacion rep. legal: " + cmbTipoIdentificacionNuevoAsegJuridico.SelectedItem.Text + " - " + txtNumeroIdentificacionNuevoAsegJuridico.Text + "<br>";
                msg.Body = msg.Body + "Actividad Econmica: " + txtActividadEconomicaNuevoAsegJuridico.Text + "<br>";
                //msg.Body = msg.Body + "Direccion: " + txtDireccionNuevoAsegJuridico.Text + "<br>";
                msg.Body = msg.Body + "Telefono: " + txtTelefonoNuevoAsegJuridico.Text + "<br>";
                msg.Body = msg.Body + "Correo Electronico: " + txtCorreoNuevoAsegJuridico.Text + "<br>";
            }

            msg.Body = msg.Body + "<br><br> ******************** POR FAVOR NO RESPONDA ESTE CORREO  ********************";
            msg.IsBodyHtml = true;
            msg.Subject = "Actualizacion Datos Cliente - " + ViewState["CodCliOracle"].ToString();
            SmtpClient smt = new SmtpClient(servidorCorreo);
            smt.Port = 25;
            smt.Credentials = new NetworkCredential(direccionSalida, passwordDireccionSalida);

            try
            {
                smt.Send(msg);
            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }

            return respuesta;
        }        
    }
}