using Lbl_Cotizado_Autos_Web.Acceso;
using Lbl_Cotizado_Autos_Web.Comunes;
using Lbl_Cotizado_Autos_Web.Seguridad;
using Lbl_Cotizador_Autos_Web.RecursosFactura;
using CotizadorWebAutos.Acceso;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lbl_Cotizado_Autos_Web.PagoEnLinea;
using System.Xml;
using CrystalDecisions.CrystalReports.Engine;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using System.IO;
using Lbl_Cotizador_Autos_Web.RecursosFactura;

namespace CotizadorWebAutos.FacturaElectronica
{
    public partial class CrearUsuarioParaFacturas : System.Web.UI.Page
    {
        public string datoUR = string.Empty;
        RequerimientosFact clRequerimientos = new RequerimientosFact();
        DataTable Productos = new DataTable();
        public string datoTelefono = string.Empty;
        DataTable datosClienteNuevoPolizas = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {   
            //LLENANDO COMBOBOX CON PRODUCTOS CODPOL
            Productos = clRequerimientos.Productos();
            txtCodPol2.DataSource = Productos;
            txtCodPol2.DataTextField = "CODPOL";
            txtCodPol2.DataValueField = "CODPOL";
            txtCodPol2.DataBind();
            txtCodPol2.Items.Insert(0, "Todos");

            Regresar.Attributes.Add("onclick", "history.back(); return false;");
        }
        protected void btnGuardarUsuarioNuevo_Click(object sender, EventArgs e)
        {
            MantenimientoUsuarios clMantenimiento = new MantenimientoUsuarios();
            AccesoUsuario clAcceso = new AccesoUsuario();
            RequerimientosFact clRequerimientos = new RequerimientosFact();
            //VALIDANDO QUE NO ESTEN VACIOS LOS REQUISITOS
            if (txtNumID.Text == string.Empty || txtDvId.Text == string.Empty)
            {
                mostrarMensaje("Debe ingresar los datos del NIT.");
                txtNumID.Focus();
                return;
            }

            if (txtNombres.Text == string.Empty)
            {
                mostrarMensaje("Debe ingresar los nombres del usuario.");
                txtNombres.Focus();
                return;
            }

            if (txtApellidos.Text == string.Empty)
            {
                mostrarMensaje("Debe ingresar los apellidos del usuario.");
                txtApellidos.Focus();
                return;
            }

            if (txtTelefono.Text == string.Empty)
            {
                mostrarMensaje("Debe un número de teléfono para el usuario.");
                txtTelefono.Focus();
                return;
            }
            else if (txtTelefono.Text.Length < 8)
            {
                mostrarMensaje("Ingrese un número de telefóno valido.");
                txtTelefono.Focus();
                return;
            }

            if (txtCorreoElectronico.Text == string.Empty)
            {
                mostrarMensaje("Ingrese un correo electrónico.");
                txtCorreoElectronico.Focus();
                return;
            }


            if (txtNombreUsuario.Text == string.Empty)
            {
                mostrarMensaje("Debe ingresar un nombre de usuario.");
                txtNombreUsuario.Focus();
                return;
            }


            if (clAcceso.existeUsuarioXCorreo(txtCorreoElectronico.Text.Trim()))
            {
                mostrarMensaje("El correo ingresado ya tiene asignado un usuario.");
                return;
            }

            if (!clAcceso.nitValidoCliente(int.Parse(txtNumID.Text), txtDvId.Text))
            {
                mostrarMensaje("El NIT que ingreso no es valido.");
                txtNumID.Focus();
                return;
            }

            try
            {
                NuevoUsuario usuario = new NuevoUsuario();
                DataTable datosIntermediario = new DataTable();
                DataTable ConsultasiExisteOracle = new DataTable();
                Varias clVarias = new Varias();

                string nuevoPass = clVarias.crearPasswordNuevo(8);
                string codigoUsuarioRemoto = string.Empty;


                //PROCESO PARA USUARIOS FINALES QUE SOLO CONSULTAN SUS FACTURAS


                usuario.NUMID = txtNumID.Text;
                usuario.DVID = txtDvId.Text;
                usuario.NOMBRES = txtNombres.Text.ToLower();
                usuario.APELLIDOS = txtApellidos.Text.ToLower();
                usuario.TELEFONO = txtTelefono.Text;
                usuario.CORREO_ELECTRONICO = txtCorreoElectronico.Text.ToLower();
                usuario.NOMBRE_UNICO_USUARIO = txtNombreUsuario.Text.ToLower();
                usuario.PASSWORD = nuevoPass;
                usuario.CODIGO_INTERMEDIARIO = "NULL";
                usuario.CODIGO_USUARIO_REMOTO = "NULL";
                usuario.USUARIO_INTERNO = "FALSE";
                usuario.DIRECCION = txtDireccion.Text;


                //se omite proceso de creación de usuario remoto por no ser usuario interno

                // Se guarda el nuevo en la base de datos de Mysql
                int idUsuarioCreado = clRequerimientos.agregarNuevoUsuario2(usuario);

                clRequerimientos.agregarRolAccesoFACTURA(idUsuarioCreado);

                string direccionNuevoUsuario = ConfigurationSettings.AppSettings["direccionSalidaMensajes"];


                string linkCreaUsuario = System.Configuration.ConfigurationManager.AppSettings["LinkCreacionUsuario"];

                string urlNuevoUsuario = linkCreaUsuario + idUsuarioCreado + "&tokenId=" + nuevoPass;

                pnlConfirma.Visible = true;
                lblverificar.Visible = true;

                enviarCorreoElectronico(usuario, urlNuevoUsuario);

            }
            catch (Exception ex)
            {
                //en caso no se cree el usuario
                pnlConfirma.Visible = true;
                Lblsincrear.Visible = true;
                lblverificar.Visible = false;
                throw ex;
            }
        }
        private void enviarCorreoElectronico(NuevoUsuario usuario, string url)
        {
            string direccionSalida = ConfigurationSettings.AppSettings["direccionSalidaMensajes"];
            string passwordDireccionSalida = ConfigurationSettings.AppSettings["passwordDireccionSalidaMensajes"];
            string servidorCorreo = ConfigurationSettings.AppSettings["servidorCorreo"];
            string correoOrigen = ConfigurationSettings.AppSettings["correoOrigen"];

            AccesoUsuario clAcceso = new AccesoUsuario();
            DataTable correosEncargadosWeb = new DataTable();

            correosEncargadosWeb = clAcceso.obtenerCorreosEncargadosWeb();

            string correosDestinatarios = string.Empty;

            int contadorCorreos = correosEncargadosWeb.Rows.Count - 1;

           

            for (int i = 0; i < correosEncargadosWeb.Rows.Count; i++)
            {
                if (correosEncargadosWeb.Rows.Count == 1)
                {
                    correosDestinatarios += correosEncargadosWeb.Rows[i]["correo_electronico"];
                }
                else
                {
                    if (contadorCorreos != i)
                    {
                        correosDestinatarios += correosEncargadosWeb.Rows[i]["correo_electronico"] + "; ";
                    }
                    else
                    {
                       correosDestinatarios += correosEncargadosWeb.Rows[i]["correo_electronico"];
                    }
                }
            }

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(correoOrigen);
            msg.To.Add(usuario.CORREO_ELECTRONICO);
            msg.CC.Add(correosDestinatarios);

            msg.Body = "Estimado(a) " + usuario.NOMBRES + " " + usuario.APELLIDOS + "," + Environment.NewLine + Environment.NewLine + Environment.NewLine
            + "Su usuario se creo correctamente. Entra al siguiente link para activar tu cuenta:" + Environment.NewLine + Environment.NewLine
            + url + Environment.NewLine + Environment.NewLine
            + "Atentamente," + Environment.NewLine + Environment.NewLine
            + "Aseguradora MAPFRE Guatemala";

            msg.IsBodyHtml = true;
            msg.Subject = "Nuevo Usuario";
            SmtpClient smt = new SmtpClient(servidorCorreo);
            smt.Port = 25;
            smt.Credentials = new NetworkCredential(direccionSalida, passwordDireccionSalida);

            smt.Send(msg);
        }
        private void mostrarMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", "alert('" + mensaje + "');", true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        private void mostrarMensaje(string mensaje, string url)
        {
            string script = "window.onload = function(){ alert('";
            script += mensaje;
            script += "');";
            script += "window.location = '";
            script += url;
            script += "'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }
        protected void BuscarDatos_Click(object sender, EventArgs e)
        {
            RequerimientosFact clRequerimientos = new RequerimientosFact();
            DataTable BuscarDF = new DataTable();
            //Asigno valores nulos al buscar datos
            string NUMID = txtNumID.Text;
            string DVID = txtDvId.Text;
            string CODPOL = "";
            string NUMPOL = txtNumPol.Text;
            string CORREO = txtCorreo.Text;
            string NOMTER1 = txtNombre1.Text.ToUpper();
            string APTER1 = txtApellidos1.Text.ToUpper();
            //en caso el combobox CODPOL ESTE VACIO
            if (txtCodPol2.SelectedValue.Equals("Todos"))
            {
                CODPOL = "";
            }
            else { CODPOL = txtCodPol2.SelectedItem.Text; }

            //Busca los datos
            BuscarDF = clRequerimientos.ValidarDatoNU(CODPOL, NUMPOL, NUMID, DVID, CORREO, NOMTER1, APTER1);

            if (BuscarDF.Rows.Count > 0)
            {
                DataRow Consulta = BuscarDF.Rows[0];
                pnlCorrobora.Visible = true;
                txtNumID.Text = Consulta["NUMID"].ToString();
                txtNumID.Enabled = false;
                txtDvId.Text = Consulta["DVID"].ToString();
                txtDvId.Enabled = false;
                txtNombres.Text = Consulta["NOMTER"].ToString();
                txtNombres.Enabled = false;
                txtApellidos.Text = Consulta["APETER"].ToString();
                txtApellidos.Enabled = false;
                //---------------------------telefono------------------
                //Como es requisito en MySQL muestro el recuadro de telefono si no se encuentra registrado en oracle
                txtTelefono.Text = Consulta["TELEF1"].ToString();
                datoTelefono = Consulta["TELEF1"].ToString();

                if (txtTelefono.Text.Length == 0)
                {
                    txtTelefono.Enabled = true;
                    lblTelefono.Visible = true;
                    txtTelefono.Visible = true;

                }
                else
                {
                    txtTelefono.Enabled = false;
                    lblTelefono.Visible = false;
                    txtTelefono.Visible = false;
                }

                //---------------------------------------------------
                txtCorreoElectronico.Text = Consulta["TELEX"].ToString();
                txtCorreoElectronico.Enabled = true;
                txtDireccion.Text = Consulta["DIR_COBRO"].ToString();
                txtDireccion.Enabled = false;
                lblDireccion.Visible = false;
                txtDireccion.Visible = false;
                //txtNombreUsuario ES INGRESADO POR EL INTERESADO
                txtCodigoIntermediario.Text = Consulta["CODINTER"].ToString();
                txtCodigoIntermediario.Enabled = false;
                lblIntermediario.Visible = false;
                txtCodigoIntermediario.Visible = false;
                Alerta_y_datos.Visible = false;
                //llenando combo de polizas del cliente
                datosClienteNuevoPolizas = clRequerimientos.PolizaDatoNU(CODPOL, NUMPOL, NUMID, DVID, CORREO, NOMTER1, APTER1);
                Poliza.DataSource = datosClienteNuevoPolizas;
                Poliza.DataTextField = ("POLIZA");
                Poliza.DataValueField = ("POLIZA");
                Poliza.DataBind();
                Poliza.Items.Insert(0, "");
                txtCorreoElectronico.Enabled = false;

            }
            else
            {

                Alerta_y_datos.Visible = true;
                pnlCorrobora.Visible = false;
                txtNumID.Text = "";
                txtNumID.Enabled = true;
                txtDvId.Text = "";
                txtDvId.Enabled = true;

                //lblFact.Visible = true;
                //pnlImprime.Visible = false;
                // mostrarMensaje("No se han econtrado facturas emitidas");
            }
        }
    }
}