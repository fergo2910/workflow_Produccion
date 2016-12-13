using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using Lbl_Cotizador_Autos_Web.Acceso;
using Lbl_Cotizado_Autos_Web.Acceso;
using Lbl_Cotizado_Autos_Web.Seguridad;

namespace CotizadorWebAutos.Principal
{
    public partial class Menu_Principal : System.Web.UI.Page
    {        
        string planes = string.Empty;
        string nombrePlan = string.Empty;
        DataTable descripcionNombresPlanes = new DataTable();
        IngresoSistema.informacionUsuario informacionUsuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();

                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];

                ConsultasBD objetoConsultas = new ConsultasBD();
                MantenimientoUsuarios clMantenimientoUsuarios = new MantenimientoUsuarios();

                DataTable codigosPlanes = new DataTable();
                DataTable planesIntermediario = new DataTable();
                DataTable categoriasProducto = new DataTable();
                DataTable permisosRol = new DataTable();
                DataTable accionesSistema = new DataTable();

                string encabezadoDivProductos = string.Empty;
                string finDivProductos = string.Empty;
                string contenidoDivProductos = string.Empty;
                string completo = string.Empty;

                Literal divProductosMAPFRE;

                //Se llena legenda con el nombre del usuario logueado y su intermediario.
                legenda.InnerText = "Usuario: " + informacionUsuario.nombreUsuarioUnico + " - " + informacionUsuario.codIntermediario;



                //Se verifican los tabs permitidos para el usuario
                categoriasProducto = objetoConsultas.obtenerCategoriasDisponiblesXMostrar(informacionUsuario.codIntermediario);



                //Se habilita el menu de seguridad para el rol administrador
                if (informacionUsuario.accionesPermitidas.Contains("Ver_Menu_Seguridad"))
                {
                    pnlMenuSeguridad.Visible = true;
                    if (informacionUsuario.accionesPermitidas.Contains("Ver_Submenu_Roles"))
                    {
                        linkRoles.Visible = true;
                    }
                }
                else
                {
                    pnlMenuSeguridad.Visible = false;
                }

                //Se asignan las url para cada link del menu

                //Modulo Seguridad
                linkConfPlan.Attributes["href"] = "../Seguridad/Configuracion_Plan_Intermediario.aspx";
                linkMantUsuario.Attributes["href"] = "../Seguridad/Mantenimiento_Usuarios.aspx";
                linkRoles.Attributes["href"] = "../Seguridad/Roles.aspx";
                linkCrearUsuario.Attributes["href"] = "../Acceso/Registro.aspx";

                //Modulo Cotizador
                linkCotizacionesAlmacenadas.Attributes["href"] = "../Principal/MisCotizaciones.aspx";
                linkReimpresion.Attributes["href"] = "../Reimpresion/Reimpresion.aspx";

                //Modulo Consulta                    
                linkFacturaE.Attributes["href"] = "../FacturaElectronica/FacturaElectronica.aspx";
                linkEstadoCuenta.Attributes["href"] = "../Oficina_Virtual/Consulta_Estado_Cuenta.aspx";
                linkPrimasPagadas.Attributes["href"] = "../Oficina_Virtual/Consulta_Primas_Pagadas.aspx";
                linkPrimasPendientes.Attributes["href"] = "../Oficina_Virtual/Consulta_Primas_Pendientes.aspx";

                //Modulo Pago En linea
                //linkPagos.Attributes["href"] = "../Pago_En_Linea/Pago_Requerimientos.aspx";

                //Se agregan los planes que puede cotizar el intermediario logueado.
                AjaxControlToolkit.TabPanel tabPanel;
                for (int i = 0; i < categoriasProducto.Rows.Count; i++)
                {
                    tabPanel = new AjaxControlToolkit.TabPanel();
                    tabPanel.ID = categoriasProducto.Rows[i]["nombre"].ToString();
                    tabPanel.HeaderText = categoriasProducto.Rows[i]["nombre"].ToString();

                    //tabPanel.Attributes.Add("runat", "server");                    

                    tabContenedorCategorias.Tabs.Add(tabPanel);

                    divProductosMAPFRE = new Literal();

                    completo = string.Empty;

                    //Se cargan los planes que tiene asignado el intermediario del usuario logueado.
                    planesIntermediario = objetoConsultas.ObtenerPlanesDeIntermediario(informacionUsuario.codIntermediario, categoriasProducto.Rows[i]["nombre"].ToString());

                    for (int j = 0; j < planesIntermediario.Rows.Count; j++)
                    {
                        string nombrePlan = planesIntermediario.Rows[j]["nombre"].ToString();
                        int idPlan = int.Parse(planesIntermediario.Rows[j]["id_plan_web"].ToString());

                        encabezadoDivProductos = "<div class='col-md-3'>";
                        switch (nombrePlan)
                        {
                            case "AUTO PRIME $.":
                                contenidoDivProductos = "<a href='../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan + "'><img src='../Recursos/imagenes/Imagenes 400x208/PRIME.jpg' alt='MAPFRE' style='width:200px;height:150px;' class='img-rounded'></a><p><strong>" + nombrePlan + "</strong></p>";
                                break;
                            case "AUTO PRIME Q.":
                                contenidoDivProductos = "<a href='../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan + "'><img src='../Recursos/imagenes/Imagenes 400x208/PRIME.jpg' alt='MAPFRE' style='width:200px;height:150px;' class='img-rounded'></a><p><strong>" + nombrePlan + "</strong></p>";
                                break;
                            case "AUTO SEGURA Q.":
                                contenidoDivProductos = "<a href='../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan + "'><img src='../Recursos/imagenes/Imagenes 400x208/AUTOSEGURA.jpg' alt='MAPFRE' style='width:200px;height:150px;' class='img-rounded'></a><p><strong>" + nombrePlan + "</strong></p>";
                                break;
                            case "AUTO SEGURA $.":
                                contenidoDivProductos = "<a href='../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan + "'><img src='../Recursos/imagenes/Imagenes 400x208/AUTOSEGURA.jpg' alt='MAPFRE' style='width:200px;height:150px;' class='img-rounded'></a><p><strong>" + nombrePlan + "</strong></p>";
                                break;
                            case "R.C. 99":
                                contenidoDivProductos = "<a href='../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan + "'><img src='../Recursos/imagenes/Imagenes 400x208/RC99.jpg' alt='MAPFRE' style='width:200px;height:150px;' class='img-rounded'></a><p><strong>" + nombrePlan + "</strong></p>";
                                break;
                            case "PROMOCION DIEZ (Q)":
                                contenidoDivProductos = "<a href='../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan + "'><img src='../Recursos/imagenes/Imagenes 400x208/AUTO10.jpg' alt='MAPFRE' style='width:200px;height:150px;' class='img-rounded'></a><p><strong>" + nombrePlan + "</strong></p>";
                                break;
                            case "PROMOCION DIEZ ($)":
                                contenidoDivProductos = "<a href='../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan + "'><img src='../Recursos/imagenes/Imagenes 400x208/AUTO10.jpg' alt='MAPFRE' style='width:200px;height:150px;' class='img-rounded'></a><p><strong>" + nombrePlan + "</strong></p>";
                                break;
                            case "SEGURO COMPLETO":
                                contenidoDivProductos = "<a href='../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan + "'><img src='../Recursos/imagenes/Imagenes 400x208/COMPLETO.jpg' alt='MAPFRE' style='width:200px;height:150px;' class='img-rounded'></a><p><strong>" + nombrePlan + "</strong></p>";
                                break;
                            case "SEGURO RC DE AUTOMOVIL":
                                contenidoDivProductos = "<a href='../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan + "'><img src='../Recursos/imagenes/Imagenes 400x208/DEFAULT.jpg' alt='MAPFRE' style='width:200px;height:150px;' class='img-rounded'></a><p><strong>" + nombrePlan + "</strong></p>";
                                break;
                            case "HOGAR SEGURO":
                                contenidoDivProductos = "<a href='../HogarSeguro/Cotizaciones.aspx?plan=" + idPlan + "'><img src='../Recursos/imagenes/Imagenes 400x208/LOGO HS, 400.jpg' alt='MAPFRE' style='width:200px;height:150px;' class='img-rounded'></a><p><strong>" + nombrePlan + "</strong></p>";
                                break;
                            case "AUTO DIEZ (Q)":
                                contenidoDivProductos = "<a href='../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan + "'><img src='../Recursos/imagenes/Imagenes 400x208/AUTO10.jpg' alt='MAPFRE' style='width:200px;height:150px;' class='img-rounded'></a><p><strong>" + nombrePlan + "</strong></p>";
                                break;
                            case "AUTO DIEZ ($)":
                                contenidoDivProductos = "<a href='../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan + "'><img src='../Recursos/imagenes/Imagenes 400x208/AUTO10.jpg' alt='MAPFRE' style='width:200px;height:150px;' class='img-rounded'></a><p><strong>" + nombrePlan + "</strong></p>";
                                break;
                            default:
                                contenidoDivProductos = "<a href='../Productos_Web/Cotizaciones_Almacenadas.aspx?plan=" + idPlan + "'><img src='../Recursos/imagenes/Imagenes 400x208/DEFAULT.jpg' alt='MAPFRE' style='width:200px;height:150px;' class='img-rounded'></a><p><strong>" + nombrePlan + "</strong></p>";
                                break;
                        }
                        finDivProductos = "</div>";
                        completo = completo + encabezadoDivProductos + contenidoDivProductos + finDivProductos;
                    }

                    divProductosMAPFRE.Text = completo;

                    tabPanel.Controls.Add(divProductosMAPFRE);
                }
            }
        }       
    }
}