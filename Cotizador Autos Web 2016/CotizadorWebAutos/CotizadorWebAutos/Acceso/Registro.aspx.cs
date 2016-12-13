using Lbl_Cotizado_Autos_Web.Acceso;
using Lbl_Cotizado_Autos_Web.Comunes;
using Lbl_Cotizado_Autos_Web.Seguridad;
using Lbl_Cotizador_Autos_Web.Acceso;
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

namespace CotizadorWebAutos.Acceso
{
    public partial class Registro : System.Web.UI.Page
    {        
        IngresoSistema.informacionUsuario informacionUsuario; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();

                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];

                if (!IsPostBack)
                {
                    llenarDropDownIntermediarios();
                }
            }
        }
        protected void llenarDropDownIntermediarios()
        {
            DataTable dtIntermediario = null;
            IngresoSistema clMantUsuario = new IngresoSistema();
            dropCodigoIntermediario.DataSource = null;
            dtIntermediario = clMantUsuario.ObtenerIntermediarios();
            dropCodigoIntermediario.DataSource = dtIntermediario;
            dropCodigoIntermediario.DataTextField = "cod_nom";
            dropCodigoIntermediario.DataValueField = "codinter";
            dropCodigoIntermediario.DataBind();
        }
        protected void btnGuardarUsuarioNuevo_Click(object sender, EventArgs e)
        {
            MantenimientoUsuarios clMantenimiento = new MantenimientoUsuarios();
            AccesoUsuario clAcceso = new AccesoUsuario();

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

            if (txtDireccion.Text == string.Empty)
            {
                mostrarMensaje("Debe ingresar una direccíon.");
                txtCorreoElectronico.Focus();
                return;
            }

            if (txtNombreUsuario.Text == string.Empty)
            {
                mostrarMensaje("Debe ingresar un nombre de usuario.");
                txtNombreUsuario.Focus();
                return;
            }

            //se agrega condicion para saber si es usuario médico. Si lo es, no necesita un código de intermediario.
            if (!chkUsuarioMedico.Checked && dropCodigoIntermediario.SelectedItem.Value.ToString().Equals("000000"))
            {
                if (txtCodigoIntermediario.Text == string.Empty)
                {
                    mostrarMensaje("Debe ingresar un codigo intermediario.");
                    txtCodigoIntermediario.Focus();
                    return;
                }
            }

            //Validaciones de usuarios existentes y NIT Invalido....
            if (!clAcceso.nitValidoCliente(int.Parse(txtNumID.Text), txtDvId.Text))
            {
                mostrarMensaje("El NIT que ingresó no es valido.");
                txtNumID.Focus();
                return;
            }

            if (clAcceso.existeUsuarioXNit(int.Parse(txtNumID.Text), txtDvId.Text))
            {
                mostrarMensaje("El NIT que ingresó ya tiene un usuario creado.");
                txtNumID.Focus();
                return;
            }

            if (clAcceso.existeUsuarioXCorreo(txtCorreoElectronico.Text.Trim()))
            {
                mostrarMensaje("El correo ingresado ya tiene asignado un usuario.");
                return;
            }

            if (clAcceso.existeUsuarioXNombreUsuario(txtNombreUsuario.Text.Trim()))
            {
                mostrarMensaje("El nombre de usuario ingresado ya existe.");
                return; 
            }

            DataTable datosUsuarioExistente = new DataTable();
            datosUsuarioExistente = clAcceso.existeUsuarioRemotoOracle(txtNombreUsuario.Text.Trim(), txtNumID.Text.Trim(), txtDvId.Text.Trim());

            if (datosUsuarioExistente.Rows.Count == 1)
            {
                CrearUsuarioExistente(datosUsuarioExistente);
            }
            else
            {
                CrearUsuarioNuevo();
            }
        }
        private void CrearUsuarioNuevo()
        {
            string respuesta = string.Empty;
            AccesoUsuario clAcceso = new AccesoUsuario();
            NuevoUsuario usuario = new NuevoUsuario();
            DataTable datosIntermediario = new DataTable();
            Varias clVarias = new Varias();
            string nuevoPass = clVarias.crearPasswordNuevo(8);
            string codigoUsuarioRemoto = string.Empty;
            codigoUsuarioRemoto = clAcceso.generarCodigoUsuarioRemoto();
            int codint = 0;
            string codigoIntermediario;

            usuario.NUMID = txtNumID.Text;
            usuario.DVID = txtDvId.Text;
            usuario.NOMBRES = txtNombres.Text;
            usuario.APELLIDOS = txtApellidos.Text;
            usuario.TELEFONO = txtTelefono.Text;
            usuario.CORREO_ELECTRONICO = txtCorreoElectronico.Text;
            usuario.DIRECCION = txtDireccion.Text;
            usuario.NOMBRE_UNICO_USUARIO = txtNombreUsuario.Text;
            usuario.ID_USUARIO_CREADOR = informacionUsuario.idUsuario.ToString();

            if (!chkUsuarioMedico.Checked)
            {
                try
                {
                    if (dropCodigoIntermediario.SelectedItem.Value.ToString().Equals("000000"))
                        codint = int.Parse(txtCodigoIntermediario.Text.ToString());
                    else
                        codint = int.Parse(dropCodigoIntermediario.SelectedItem.Value.ToString());
                }
                catch (Exception ex)
                {
                    mostrarMensaje("El codigo de intermediario debe ser un numero");
                    return;
                }

                codigoIntermediario = completarCodigoAgente(codint);

                datosIntermediario = clAcceso.obtenerIntermediario(codigoIntermediario);

                if (datosIntermediario.Rows.Count == 1)
                {
                    clAcceso.ingresarIntermediarioWeb(datosIntermediario);

                    usuario.CODIGO_INTERMEDIARIO = codigoIntermediario;
                    usuario.PASSWORD = nuevoPass;

                    if (chkUsuarioInterno.Checked)
                    {
                        usuario.USUARIO_INTERNO = "S";
                    }
                    else
                    {
                        usuario.USUARIO_INTERNO = "N";
                    }

                    // Se guarda el nuevo usuario en la base de datos de Oracle
                    usuario.CODIGO_USUARIO_REMOTO = codigoUsuarioRemoto;
                    clAcceso.agregarUsuarioRemotoOracle(usuario, "web");

                    if (chkUsuarioInterno.Checked)
                    {
                        usuario.USUARIO_INTERNO = "TRUE";
                    }
                    else
                    {
                        usuario.USUARIO_INTERNO = "FALSE";
                    }

                    // Se guarda el nuevo en la base de datos de Mysql
                    int idUsuarioCreado = clAcceso.agregarNuevoUsuario(usuario,"web");
                    //agregando rol de cotizador e inspector automaticamente

                    if (chkUsuarioInterno.Checked)
                    {
                        clAcceso.agregarRolNuevoUsuario(idUsuarioCreado, "interno");
                    }
                    else
                    {
                        clAcceso.agregarRolNuevoUsuario(idUsuarioCreado, "externo");
                    }

                    string linkCreaUsuario = System.Configuration.ConfigurationManager.AppSettings["LinkCreacionUsuario"];

                    string urlNuevoUsuario = linkCreaUsuario + idUsuarioCreado + "&tokenId=" + nuevoPass;

                    respuesta = enviarCorreoElectronico(usuario, urlNuevoUsuario);

                    if (respuesta != string.Empty)
                    {
                        mostrarMensaje("El correo no fue enviado. Serás direccionado a otra página para ingresar tu nueva contraseña.",
                           urlNuevoUsuario);
                    }
                    else
                    {
                        mostrarMensaje("El usuario ha sido creado exitosamente. Se envió un correo con las credenciales.", "../Acceso/Ingreso.aspx");
                    }
                }
            }
            else
            { 
                ///ACLARACIÓN: PARA LA CREACIÓN DE MEDICOS, SE UTILIZA EL CODIGO INTERMEDIARIO PARA ALMACENAR
                ///EL IDE PROVEEDOR. SE ACORDO CON EXZQUIEL
                usuario.CODIGO_INTERMEDIARIO = string.Empty;
                usuario.IDE_PROVEEDOR = clAcceso.obtenerIdeProveedor(usuario.NUMID, usuario.DVID);
                if(usuario.IDE_PROVEEDOR.Equals(string.Empty))
                {
                    mostrarMensaje("El número de nit no está afiliado a la aseguradora");
                    return;
                }
                usuario.PASSWORD = nuevoPass;

                if (chkUsuarioInterno.Checked)
                {
                    usuario.USUARIO_INTERNO = "S";
                }
                else
                {
                    usuario.USUARIO_INTERNO = "N";
                }

                // Se guarda el nuevo usuario en la base de datos de Oracle
                usuario.CODIGO_USUARIO_REMOTO = codigoUsuarioRemoto;
                clAcceso.agregarUsuarioRemotoOracle(usuario, "medico");

                if (chkUsuarioInterno.Checked)
                {
                    usuario.USUARIO_INTERNO = "TRUE";
                }
                else
                {
                    usuario.USUARIO_INTERNO = "FALSE";
                }

                // Se guarda el nuevo en la base de datos de Mysql
                int idUsuarioCreado = clAcceso.agregarNuevoUsuario(usuario,"medico");
                //agregando rol de cotizador e inspector automaticamente

                clAcceso.agregarRolNuevoUsuario(idUsuarioCreado, "medico");

                string linkCreaUsuario = System.Configuration.ConfigurationManager.AppSettings["LinkCreacionUsuario"];

                string urlNuevoUsuario = linkCreaUsuario + idUsuarioCreado + "&tokenId=" + nuevoPass;

                respuesta = enviarCorreoElectronico(usuario, urlNuevoUsuario);

                if (respuesta != string.Empty)
                {
                    mostrarMensaje("El correo no fue enviado. Serás direccionado a otra página para ingresar tu nueva contraseña.",
                        urlNuevoUsuario);
                }
                else
                {
                    mostrarMensaje("El usuario ha sido creado exitosamente. Se envió un correo con las credenciales.", "../Acceso/Ingreso.aspx");
                }
            }            
        }
        private void CrearUsuarioExistente(DataTable datosUsuario)
        {
            string respuesta = string.Empty;
            AccesoUsuario clAcceso = new AccesoUsuario();
            NuevoUsuario usuario = new NuevoUsuario();
            DataTable datosIntermediario = new DataTable();
            Varias clVarias = new Varias();
            string nuevoPass = clVarias.crearPasswordNuevo(8);

            string NUMID = datosUsuario.Rows[0]["NUMID"].ToString();
            string DVID = datosUsuario.Rows[0]["DVID"].ToString();
            string NOMBRE = datosUsuario.Rows[0]["NOMBRE"].ToString();
            string DIRECCION = datosUsuario.Rows[0]["DIRECCION"].ToString();
            string IDUSUARIO = datosUsuario.Rows[0]["IDUSUARIO"].ToString();
            string EMAIL = datosUsuario.Rows[0]["EMAIL"].ToString();
            string CODUSUARIOREMO = datosUsuario.Rows[0]["CODUSUARIOREMO"].ToString();
            string CODINTER = datosUsuario.Rows[0]["CODINTER"].ToString();
            string INDINTERNO = datosUsuario.Rows[0]["INDINTERNO"].ToString();

            usuario.NUMID = NUMID;
            usuario.DVID = DVID;
            usuario.NOMBRES = NOMBRE;
            usuario.TELEFONO = "";
            usuario.CORREO_ELECTRONICO = EMAIL;
            usuario.DIRECCION = DIRECCION;
            usuario.NOMBRE_UNICO_USUARIO = IDUSUARIO;
            usuario.PASSWORD = nuevoPass;
            usuario.CODIGO_USUARIO_REMOTO = CODUSUARIOREMO;
            usuario.ID_USUARIO_CREADOR = informacionUsuario.idUsuario.ToString();

            if (!chkUsuarioMedico.Checked)
            {

                datosIntermediario = clAcceso.obtenerIntermediario(CODINTER);

                if (datosIntermediario.Rows.Count == 1)
                {
                    clAcceso.ingresarIntermediarioWeb(datosIntermediario);

                    usuario.CODIGO_INTERMEDIARIO = CODINTER;

                    switch (INDINTERNO)
                    {
                        case "S":
                            usuario.USUARIO_INTERNO = "TRUE";
                            break;
                        case "N":
                            usuario.USUARIO_INTERNO = "FALSE";
                            break;
                        default:
                            usuario.USUARIO_INTERNO = "TRUE";
                            break;
                    }

                    // Se guarda el nuevo en la base de datos de Mysql
                    int idUsuarioCreado = clAcceso.agregarNuevoUsuario(usuario, "web");
                    //agregando rol de cotizador e inspector automaticamente
                    switch (INDINTERNO)
                    {
                        case "S":
                            clAcceso.agregarRolNuevoUsuario(idUsuarioCreado, "interno");
                            break;
                        case "N":
                            clAcceso.agregarRolNuevoUsuario(idUsuarioCreado, "externo");
                            break;
                        default:
                            clAcceso.agregarRolNuevoUsuario(idUsuarioCreado, "interno");
                            break;
                    }

                    string linkCreaUsuario = System.Configuration.ConfigurationManager.AppSettings["LinkCreacionUsuario"];

                    string urlNuevoUsuario = linkCreaUsuario + idUsuarioCreado + "&tokenId=" + nuevoPass;

                    respuesta = enviarCorreoElectronico(usuario, urlNuevoUsuario);

                    if (respuesta != string.Empty)
                    {
                        mostrarMensaje("El correo no fue enviado. Serás direccionado a otra página para ingresar tu nueva contraseña.",
                            urlNuevoUsuario);
                    }
                    else
                    {
                        mostrarMensaje("El usuario ha sido creado exitosamente. Se envió un correo con las credenciales.", "../Acceso/Ingreso.aspx");
                    }
                }
            }
            else
            {
                ///SE ACORDÓ CON EXZEQUIEL DE UTILIZAR EL CAMPO CODINTER DE LA TABLA USUARIO_REMOTO EN ORACLE
                ///PARA ALMACENAR EL IDEPROVEEDOR DE UN USUARIO MEDICO
                usuario.IDE_PROVEEDOR = CODINTER;

                switch (INDINTERNO)
                {
                    case "S":
                        usuario.USUARIO_INTERNO = "TRUE";
                        break;
                    case "N":
                        usuario.USUARIO_INTERNO = "FALSE";
                        break;
                    default:
                        usuario.USUARIO_INTERNO = "TRUE";
                        break;
                }

                // Se guarda el nuevo en la base de datos de Mysql
                int idUsuarioCreado = clAcceso.agregarNuevoUsuario(usuario, "medico");
                //agregando rol de cotizador e inspector automaticamente
                clAcceso.agregarRolNuevoUsuario(idUsuarioCreado, "medico");

                string linkCreaUsuario = System.Configuration.ConfigurationManager.AppSettings["LinkCreacionUsuario"];

                string urlNuevoUsuario = linkCreaUsuario + idUsuarioCreado + "&tokenId=" + nuevoPass;

                respuesta = enviarCorreoElectronico(usuario, urlNuevoUsuario);

                if (respuesta != string.Empty)
                {
                    mostrarMensaje("El correo no fue enviado. Serás direccionado a otra página para ingresar tu nueva contraseña.",
                        urlNuevoUsuario);
                }
                else
                {
                    mostrarMensaje("El usuario ha sido creado exitosamente. Se envió un correo con las credenciales.", "../Acceso/Ingreso.aspx");
                }
            }
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
        private string enviarCorreoElectronico(NuevoUsuario usuario, string url)
        {
            string direccionSalida = ConfigurationSettings.AppSettings["direccionSalidaMensajes"];
            string passwordDireccionSalida = ConfigurationSettings.AppSettings["passwordDireccionSalidaMensajes"];
            string servidorCorreo = ConfigurationSettings.AppSettings["servidorCorreo"];
            string correoOrigen =  ConfigurationSettings.AppSettings["correoOrigen"];
            string respuesta = string.Empty;

            AccesoUsuario clAcceso = new AccesoUsuario();
            DataTable correosEncargadosWeb = new DataTable();

            correosEncargadosWeb = clAcceso.obtenerCorreosEncargadosWeb();

            string correosDestinatarios = string.Empty;

            int contadorCorreos = correosEncargadosWeb.Rows.Count -1;

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
            //msg.CC.Add(correosDestinatarios);

            msg.Body = "Estimado(a) " + usuario.NOMBRES + " " +usuario.APELLIDOS + "," + Environment.NewLine + Environment.NewLine + Environment.NewLine
            + "Su usuario se creo correctamente.  El usuario es: '" + usuario.NOMBRE_UNICO_USUARIO + "'. Entra al siguiente link para activar tu cuenta:" + Environment.NewLine + Environment.NewLine
            + url + Environment.NewLine + Environment.NewLine
            + "Atentamente," + Environment.NewLine + Environment.NewLine
            + "Aseguradora MAPFRE Guatemala";

            msg.IsBodyHtml = true;
            msg.Subject = "Nuevo Usuario";
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
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Principal/Menu_Principal.aspx");  
        }
        protected void dropCodigoIntermediario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropCodigoIntermediario.SelectedValue.ToString().Equals("000000") )
            {
                lblCodigoIntermediario.Visible = true;
                dropCodigoIntermediario.Visible = true;
                txtCodigoIntermediario.Visible = true;
            }
            else if(chkUsuarioMedico.Checked)
            {
                lblCodigoIntermediario.Visible = false;
                dropCodigoIntermediario.Visible = false;
                txtCodigoIntermediario.Visible = false;
                txtCodigoIntermediario.Text = string.Empty;
            }
            else
            {
                txtCodigoIntermediario.Visible = false;
                txtCodigoIntermediario.Text = string.Empty;
            }
        }
        protected void chkUsuarioMedico_CheckedChanged(object sender, EventArgs e)
        {
            if(chkUsuarioMedico.Checked)
            { 
                lblCodigoIntermediario.Visible = false;
                dropCodigoIntermediario.Visible = false;
                txtCodigoIntermediario.Visible = false;
            }
            else
            {
                lblCodigoIntermediario.Visible = true;
                dropCodigoIntermediario.Visible = true;
                txtCodigoIntermediario.Visible = true;
                dropCodigoIntermediario_SelectedIndexChanged(null, null);
            }
        }       
    }
}