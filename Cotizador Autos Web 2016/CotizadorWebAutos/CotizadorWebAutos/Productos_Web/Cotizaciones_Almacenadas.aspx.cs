using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using Lbl_Cotizado_Autos_Web.CotizacionesMysql;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Updates;
using Lbl_Cotizado_Autos_Web.Acceso;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Lbl_Cotizado_Autos_Web.ProcesoEmision;
using Lbl_Cotizado_Autos_Web.Comunes;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Inserts;
using Lbl_Cotizador_Autos_Web.Acceso;

namespace CotizadorWebAutos.Productos_Web
{
    public partial class Cotizaciones_Almacenadas : System.Web.UI.Page
    {        
        int idPlan = 0;        
        IngresoSistema.informacionUsuario informacionUsuario;
        DataTable permisosUsuarioLogueado = new DataTable();
        UpdatesBD objetoUpdate = new UpdatesBD();       
        ReportDocument objetoReporte = new ReportDocument();
        DataTable InformacionCotizacionMYSQL = new DataTable("DT_Prime_Dolares_MYSQL");
        DataTable InformacionCotizacionORACLE = new DataTable("DT_Prime_Dolares_ORACLE");        
        DataTable InformacionPlanesWeb = new DataTable("DT_Planes_Web");
        DataSet ds = new DataSet("DS_Prime_Dolares");
        DataTable descripcionNombresPlanes = new DataTable();
        string nombrePlan = string.Empty;        
        ConsultasBD objetoConsultas = new ConsultasBD();       
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {

                idPlan = int.Parse(Request.QueryString["plan"]);
                crearVariablesSession(0, idPlan);

                informacionUsuario = new IngresoSistema.informacionUsuario();
                AccesoUsuario clAccesoUsuario = new AccesoUsuario();

                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];

                permisosUsuarioLogueado = clAccesoUsuario.obtenerPermisosUsuario(informacionUsuario.idUsuario);

                if (!IsPostBack)
                { 
                    permisosUsuarioLogueado = clAccesoUsuario.obtenerPermisosUsuario(informacionUsuario.idUsuario);

                    lblNombreCotizacion.Text = "COTIZACIONES " + objetoConsultas.ObtenerNombrePlan(idPlan.ToString()).Rows[0]["nombre"].ToString();
                    mostrarCotizaciones();
                }
            }
        }
        private void mostrarCotizaciones()
        {
            if (informacionUsuario.accionesPermitidas.Contains("Ver_Todas_Cotizaciones"))
            {
                grvCotizacionesIntermediario.DataSource = objetoConsultas.ObtenerTodasLasCotizaciones(informacionUsuario.idUsuario.ToString(), idPlan.ToString());
                grvCotizacionesIntermediario.DataBind();
                return;
            }
            else if (informacionUsuario.accionesPermitidas.Contains("Ver_Cotizaciones_Usuario"))
            {
                grvCotizacionesIntermediario.DataSource = objetoConsultas.ObtenerCotizacionesAlmacenadasXPlan(informacionUsuario.idUsuario.ToString(), idPlan.ToString());
                grvCotizacionesIntermediario.DataBind();
                return;
            }
            else if (informacionUsuario.accionesPermitidas.Contains("Ver_Cotizaciones_Autorizar"))
            {
                grvCotizacionesIntermediario.DataSource = objetoConsultas.obtenerCotizacionesXAutorizar(idPlan.ToString());
                grvCotizacionesIntermediario.DataBind();
                return;
            }            
        }
        protected void ingresarCliente(Object sender, CommandEventArgs e)
        {
            int idCotizacionSeleccionada = int.Parse(e.CommandArgument.ToString());

            crearVariablesSession(idCotizacionSeleccionada, idPlan);
            Response.Redirect("../Cliente/AseguradoTitular.aspx");
        }
        protected void ingresarInspeccion(Object sender, CommandEventArgs e)
        {
            int idCotizacionSeleccionada = int.Parse(e.CommandArgument.ToString());

            crearVariablesSession(idCotizacionSeleccionada, idPlan);
            Response.Redirect("../Productos_Web/Inspeccion.aspx");
        }
        private void crearVariablesSession(int idCotizacion, int idPlan)
        {
            Session["cotId"] = idCotizacion;
            Session["idPlan"] = idPlan;
        }
        protected void ingresarAutorizacion(Object sender, CommandEventArgs e)
        {
            int idCotizacionSeleccionada = int.Parse(e.CommandArgument.ToString());

            crearVariablesSession(idCotizacionSeleccionada, idPlan);
            Response.Redirect("../Productos_Web/Autorizacion.aspx");
        }
        protected void ingresarEmision(Object sender, CommandEventArgs e)
        {
            //pedir información
            DataTable detalle = objetoConsultas.obtenerDetalleCotizacion(e.CommandArgument.ToString());
            lblIdCotizacionDetalle.Text = e.CommandArgument.ToString();
            if(detalle!=null)
            {
                txtForma.Text = "• " + detalle.Rows[0]["desc_forma_pago_cotizado"].ToString();
                txtNumero.Text = "• " + detalle.Rows[0]["desc_numero_pagos_cotizado"].ToString();
            }
            else
                MensajeModalDetallePlan.Text = "No existe información de esta póliza. Se emitió antes de agregar funcionalidad";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { detallePlan(); });", true);
        }
        protected void ingresarImpresion(Object sender, CommandEventArgs e)
        {
            int idCotizacionSeleccionada = int.Parse(e.CommandArgument.ToString());
            string ubicacionPDF = Server.MapPath(("~/Reportes/Generados/"));
            string correoUsuario = string.Empty;

            descripcionNombresPlanes = objetoConsultas.ObtenerNombrePlan(idPlan.ToString());
            nombrePlan = descripcionNombresPlanes.Rows[0]["nombre"].ToString();
            InformacionCotizacionMYSQL = objetoConsultas.ObtenerInformacionCotizacionMYSQL(idCotizacionSeleccionada.ToString());
            InformacionCotizacionORACLE = objetoConsultas.ObtenerInformacionCotizacionORACLE(idCotizacionSeleccionada.ToString());
            //InformacionBeneficios = objectoConsultas.ObtenerBeneficios(idPlan);
            InformacionPlanesWeb = objetoConsultas.ObtenerNombrePlan(idPlan.ToString());
            ds.Tables.Add(InformacionCotizacionMYSQL);
            //ds.Tables.Add(InformacionCotizacionORACLE);
            //objetoReporte.Database.Tables[0].SetDataSource(InformacionCotizacionMYSQL);
            //objetoReporte.Database.Tables[1].SetDataSource(InformacionCotizacionMYSQL);
            string nombrePDF = informacionUsuario.idUsuario + "_" + DateTime.Now.Date.ToString("ddMMyyyy") + idCotizacionSeleccionada + nombrePlan ;
            string archivoPDF = ubicacionPDF + nombrePDF;

            switch (nombrePlan)
            {
                case "AUTO PRIME $.":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/CotizacionAutomovil.rpt"));
                    break;
                case "AUTO PRIME Q.":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/CotizacionAutomovil.rpt"));
                    break;
                case "AUTO SEGURA Q.":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/CotizacionAutomovil.rpt"));
                    break;
                case "AUTO SEGURA $.":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/CotizacionAutomovil.rpt"));
                    break;
                case "R.C. 99":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/CotizacionAutomovil.rpt"));
                    break;
                case "PROMOCION DIEZ (Q)":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/CotizacionAutomovil.rpt"));
                    break;
                case "PROMOCION DIEZ ($)":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/CotizacionAutomovil.rpt"));
                    break;
                case "SEGURO COMPLETO":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/CotizacionAutomovil.rpt"));
                    break;
                case "SEGURO RC DE AUTOMOVIL":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/CotizacionAutomovil.rpt"));
                    break;
                case  "AUTO DIEZ (Q)":
                    //SE DEBE CREAR EL REPORTE PARA ESTE NUEVO PLAN 29/06/2016
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/CotizacionAutomovil.rpt"));
                    break;
                case "AUTO DIEZ ($)":
                    //SE DEBE CREAR EL REPORTE PARA ESTE NUEVO PLAN 29/06/2016
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/CotizacionAutomovil.rpt"));
                    break;
                case "HOGAR SEGURO DIAMANTE/PROPIETARIO":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/HogarSeguro.rpt"));
                    break;
                case "HOGAR SEGURO RUBI/PROPIETARIO":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/HogarSeguroRubiPropi.rpt"));
                    break;
                case "HOGAR SEGURO ESMERALDA/PROPIETARIO":
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/HogarSeguroEsmeralPropi.rpt"));
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
                default:
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/CotizacionAutomovil.rpt"));
                    break;
            }
           
            objetoReporte.Database.Tables[0].SetDataSource(InformacionCotizacionMYSQL);
            objetoReporte.Database.Tables[1].SetDataSource(InformacionCotizacionORACLE);
           
            objetoReporte.ExportToHttpResponse
                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, nombrePDF);           
        }
        protected void ingresarEliminar(Object sender, CommandEventArgs e)
        {
            lblIdCotizacionEliminar.Text = e.CommandArgument.ToString();
            MensajeModalEliminacion.Text = "Está seguro que desea eliminar la cotizacion No: " + e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { aceptacionEliminar(); });", true);
        }
        protected void btnCotizacionNueva_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Productos_Web/Cotizar_Auto.aspx?plan=" + idPlan);            
        }
        protected void grvCotizacionesIntermediario_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnINC = (LinkButton)e.Row.FindControl("btnINC");
                LinkButton btnINS = (LinkButton)e.Row.FindControl("btnINS");
                LinkButton btnAUT = (LinkButton)e.Row.FindControl("btnAUT");
                LinkButton btnIMP = (LinkButton)e.Row.FindControl("btnAUT");
                LinkButton btnELI = (LinkButton)e.Row.FindControl("btnAUT");
                LinkButton btnEMI = (LinkButton)e.Row.FindControl("btnEMI");

                if (e.Row.Cells[3].Text == "VAL")
                    {
                        if (informacionUsuario.accionesPermitidas.Contains("Ingresar_Cliente"))
                        {
                            btnINC.Visible = true;
                            btnINS.Visible = false;
                            btnAUT.Visible = false;
                        }
                    }

                if (e.Row.Cells[3].Text == "INC")
                {
                    if (informacionUsuario.accionesPermitidas.Contains("Ingresar_Vehiculo"))
                    {
                        btnINC.Visible = false;
                        btnINS.Visible = true;
                        btnAUT.Visible = false;
                    }                        
                }

                if (e.Row.Cells[3].Text == "INS")
                { 
                    if (informacionUsuario.accionesPermitidas.Contains("Autorizar"))
                    {
                        btnINC.Visible = false;
                        btnINS.Visible = false;
                        btnAUT.Visible = true;
                    }
                }

                if (e.Row.Cells[3].Text == "AUT")
                {
                    if (informacionUsuario.accionesPermitidas.Contains("Emitir"))
                    {
                        btnINC.Visible = false;
                        btnINS.Visible = false;
                        btnEMI.Visible = true;
                    }                        
                }                
            }

            e.Row.Cells[3].Visible = false;
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
        private void mostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Principal/Menu_Principal.aspx");
        }
        protected void btnEmitirDialog_Click(object sender, EventArgs e)
        {
            EmitirPoliza clEmision = new EmitirPoliza();
            Varias clVarias = new Varias();
            DataTable datosUsuarioLogueado = new DataTable();
            DataTable datosCotizacionSeleccionada = new DataTable();

            datosUsuarioLogueado = clVarias.obtenerInformacionUsuarioLogueado(informacionUsuario.idUsuario);
            datosCotizacionSeleccionada = clVarias.obtenerInformacionCotizacion(int.Parse(lblIdCotizacionSeleccionada.Text));

            DateTime fecha = DateTime.Parse(txtFechaInicioVigenciaPoliza.Text);


            //if (fecha.Date < DateTime.Parse(datosCotizacionSeleccionada.Rows[0]["fecha_cotizacion"].ToString()).Date)
            //{
            //    lblRespuesta.Text = "La fecha de inicio de vigencia de la poliza no puede ser menor a la fecha de la cotización.";
            //    lblRespuesta.Visible = true;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { abrirResultado(); });", true);
            //}
            //else
            //{
                string respuesta = string.Empty;

                respuesta = clEmision.Emitir(lblIdCotizacionSeleccionada.Text, datosUsuarioLogueado, idPlan, informacionUsuario, fecha.ToString("dd/MM/yyyy"));

                if (!respuesta.Contains("ORA"))
                {
                    lblRespuesta.Text = respuesta;
                    lblRespuesta.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { abrirResultado(); });", true);
                }
                else
                {
                    lblRespuesta.Text = respuesta;
                    lblRespuesta.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { abrirResultado(); });", true);
                }
            //}
        }
        protected void btnSi_Click(object sender, EventArgs e)
        {
            int idCotizacionSeleccionada = int.Parse(lblIdCotizacionEliminar.Text);
            bool resultadoEliminar = false;

            resultadoEliminar = objetoUpdate.DeleteInformacionCotizacionOracle(idCotizacionSeleccionada.ToString());
            if (resultadoEliminar)
            {
                mostrarCotizaciones();
            }
            else
            {
                mostrarMensaje("ERROR AL ELIMINAR LA POLIZA");
            }            
        }
        protected void Emitir_Click(object sender, EventArgs e)
        {
            DataRow[] permisoVigenciaPoliza = permisosUsuarioLogueado.Select("nombre = 'INGRESO FECHA VIGENCIA EMISION'");
            string IdCotizacion = lblIdCotizacionDetalle.Text;

            if (permisoVigenciaPoliza.Count() == 1)
            {
                lblIdCotizacionSeleccionada.Text = IdCotizacion;
                lblIdPlanSeleccionado.Text = idPlan.ToString();
                txtFechaInicioVigenciaPoliza.Text = DateTime.Today.ToString("dd/MM/yyyy");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { PedirFechaVigencia(); });", true);
            }
            else
            {
                EmitirPoliza clEmision = new EmitirPoliza();
                Varias clVarias = new Varias();
                DataTable datosUsuarioLogueado = new DataTable();
                datosUsuarioLogueado = clVarias.obtenerInformacionUsuarioLogueado(informacionUsuario.idUsuario);

                string respuesta = string.Empty;

                respuesta = clEmision.Emitir(IdCotizacion, datosUsuarioLogueado, idPlan, informacionUsuario);

                if (!respuesta.Contains("ORA"))
                {
                    lblRespuesta.Text = respuesta;
                    lblRespuesta.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { abrirResultado(); });", true);
                }
                else
                {
                    lblRespuesta.Text = respuesta;
                    lblRespuesta.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { abrirResultado(); });", true);
                }
            }
        }
    }
}