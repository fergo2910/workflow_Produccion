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
using Lbl_Cotizado_Autos_Web.ConexionesBD.Inserts;
using Lbl_Cotizado_Autos_Web.ProcesoEmision;
using Lbl_Cotizado_Autos_Web.Comunes;
using Lbl_Cotizador_Autos_Web.Acceso;

namespace CotizadorWebAutos.Principal
{
    public partial class MisCotizaciones : System.Web.UI.Page
    {        
        string nombrePlan = string.Empty;       

        IngresoSistema.informacionUsuario informacionUsuario;

        UpdatesBD objetoUpdate = new UpdatesBD();
        ConsultasBD objetoConsultas = new ConsultasBD();
        ReportDocument objetoReporte = new ReportDocument();        
        InsertsBD objetoInserts = new InsertsBD();        
        DataTable permisosUsuarioLogueado = new DataTable();
        DataTable InformacionCotizacionMYSQL = new DataTable("DT_Prime_Dolares_MYSQL");
        DataTable InformacionCotizacionORACLE = new DataTable("DT_Prime_Dolares_ORACLE");        
        DataTable InformacionPlanesWeb = new DataTable("DT_Planes_Web");
        DataTable descripcionNombresPlanes = new DataTable();
        DataSet ds = new DataSet("DS_Prime_Dolares");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();
                AccesoUsuario clAccesoUsuario = new AccesoUsuario();

                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];

                permisosUsuarioLogueado = clAccesoUsuario.obtenerPermisosUsuario(informacionUsuario.idUsuario);

                if (!IsPostBack)
                { 
                    permisosUsuarioLogueado = clAccesoUsuario.obtenerPermisosUsuario(informacionUsuario.idUsuario);
                    mostrarCotizaciones();
                }
            }
        }
        private void mostrarCotizaciones()
        {
            if (informacionUsuario.accionesPermitidas.Contains("Ver_Todas_Cotizaciones"))
            {
                grvCotizacionesIntermediario.DataSource = objetoConsultas.ObtenerTodasLasCotizaciones(informacionUsuario.idUsuario.ToString());
                grvCotizacionesIntermediario.DataBind();
                return;
            }
            else if (informacionUsuario.accionesPermitidas.Contains("Ver_Cotizaciones_Usuario"))
            {
                grvCotizacionesIntermediario.DataSource = objetoConsultas.ObtenerCotizacionesAlmacenadasXPlan(informacionUsuario.idUsuario.ToString());
                grvCotizacionesIntermediario.DataBind();
                return;
            }
            else if (informacionUsuario.accionesPermitidas.Contains("Ver_Cotizaciones_Autorizar"))
            {
                grvCotizacionesIntermediario.DataSource = objetoConsultas.obtenerCotizacionesXAutorizar();
                grvCotizacionesIntermediario.DataBind();
                return;
            }
        }
        protected void ingresarCliente(Object sender, CommandEventArgs e)
        {            
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int cotId = int.Parse(arg[0]);
            int idPlan = int.Parse(arg[1]);

            crearVariablesSession(cotId, idPlan);
            Response.Redirect("../Cliente/AseguradoTitular.aspx");
        }
        protected void ingresarInspeccion(Object sender, CommandEventArgs e)
        {            
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int cotId = int.Parse(arg[0]);
            int idPlan = int.Parse(arg[1]);

            crearVariablesSession(cotId, idPlan);
            Response.Redirect("../Productos_Web/Inspeccion.aspx");
        }
        protected void ingresarAutorizacion(Object sender, CommandEventArgs e)
        {            
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            int cotId = int.Parse(arg[0]);
            int idPlan = int.Parse(arg[1]);

            crearVariablesSession(cotId, idPlan);
            Response.Redirect("../Productos_Web/Autorizacion.aspx");
        }
        protected void ingresarEmision(Object sender, CommandEventArgs e)
        {
            string[] words = e.CommandArgument.ToString().Split(';');
            lblIdCotizacionDetalle.Text = words[0];
            lblIdPlanDetalle.Text = words[1];
            DataTable detalle = objetoConsultas.obtenerDetalleCotizacion(lblIdCotizacionDetalle.Text);
            if (detalle != null)
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
            //int idCotizacionSeleccionada = int.Parse(e.CommandArgument.ToString());
            string ubicacionPDF = Server.MapPath(("~/Reportes/Generados/"));
            string correoUsuario = string.Empty;
            string idPlan = string.Empty;
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');

            InformacionCotizacionMYSQL = objetoConsultas.ObtenerInformacionCotizacionMYSQL(arg[0].ToString());
            InformacionCotizacionORACLE = objetoConsultas.ObtenerInformacionCotizacionORACLE(arg[0].ToString());
            //InformacionBeneficios = objectoConsultas.ObtenerBeneficios(idPlan);
            InformacionPlanesWeb = objetoConsultas.ObtenerNombrePlan(arg[1]);
            ds.Tables.Add(InformacionCotizacionMYSQL);
            //ds.Tables.Add(InformacionCotizacionORACLE);
            //objetoReporte.Database.Tables[0].SetDataSource(InformacionCotizacionMYSQL);
            //objetoReporte.Database.Tables[1].SetDataSource(InformacionCotizacionMYSQL);
            descripcionNombresPlanes = objetoConsultas.ObtenerNombrePlan(arg[1]);
            nombrePlan = descripcionNombresPlanes.Rows[0]["nombre"].ToString();

            string nombrePDF = informacionUsuario.idUsuario + DateTime.Now.Date.ToString("ddMMyyyy") + arg[0] + nombrePlan + ".pdf";
            //string ubicacionPDF = "../Cotizador Web/Cotizador_Web/Cotizador_Web/Reportes/PDFs/";
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
                case "AUTO DIEZ (Q)":
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
                default:
                    objetoReporte.Load(Server.MapPath("../Reportes/RPT/CotizacionAutomovil.rpt"));
                    break;
            }
            #region comentarios
            //if (arg[1] == "11")
            //{
            //    objetoReporte.Load(Server.MapPath("../Reportes/RPT/PrimeDolares.rpt"));
            //}
            //if (arg[1] == "12")
            //{
            //    objetoReporte.Load(Server.MapPath("../Reportes/RPT/PrimeQuetzales.rpt"));
            //}
            //if (arg[1] == "13")
            //{
            //    objetoReporte.Load(Server.MapPath("../Reportes/RPT/MujerDolares.rpt"));
            //}
            //if (arg[1] == "14")
            //{
            //    objetoReporte.Load(Server.MapPath("../Reportes/RPT/MujerQuetzales.rpt"));
            //}
            //if (arg[1] == "15")
            //{
            //    objetoReporte.Load(Server.MapPath("../Reportes/RPT/RCNoventayNueve.rpt"));
            //}
            //if (arg[1] == "16")
            //{
            //    objetoReporte.Load(Server.MapPath("../Reportes/RPT/PromoQuetzales.rpt"));
            //}
            //if (arg[1] == "17")
            //{
            //    objetoReporte.Load(Server.MapPath("../Reportes/RPT/PromoDolares.rpt"));
            //}
            //if (arg[1] == "19")
            //{
            //    objetoReporte.Load(Server.MapPath("../Reportes/RPT/SeguroCompleto.rpt"));
            //}
            //if (arg[1] == "20")
            //{
            //    objetoReporte.Load(Server.MapPath("../Reportes/RPT/RCCompleto.rpt"));
            //}
            #endregion
            objetoReporte.Database.Tables[0].SetDataSource(InformacionCotizacionMYSQL);
            objetoReporte.Database.Tables[1].SetDataSource(InformacionCotizacionORACLE);

            objetoReporte.ExportToHttpResponse
                (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, nombrePDF);
        }
        protected void ingresarEliminar(Object sender, CommandEventArgs e)
        {
            string[] arg = new string[2];
            arg = e.CommandArgument.ToString().Split(';');
            lblIdCotizacionEliminar.Text = arg[0].ToString();
            MensajeModalEliminacion.Text = "Está seguro que desea eliminar la cotizacion No: " + arg[0].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { aceptacionEliminar(); });", true);
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
                
                if (e.Row.Cells[2].Text.Contains("HOGAR"))
                {                    

                    if (informacionUsuario.accionesPermitidas.Contains("Ingresar_Cliente")|| informacionUsuario.accionesPermitidas.Contains("Emitir") )
                    {
                        if (e.Row.Cells[4].Text == "VAL")
                        {
                            btnINC.Visible = true;
                            btnINS.Visible = false;
                            btnAUT.Visible = false;
                        }

                        if (e.Row.Cells[4].Text == "INC")
                        {
                            btnINC.Visible = false;
                            btnINS.Visible = false;
                            btnAUT.Visible = false;
                            btnEMI.Visible = true;
                        }
                    }                    
                }
                else
                {
                    if (e.Row.Cells[4].Text == "VAL")
                    {
                        if (informacionUsuario.accionesPermitidas.Contains("Ingresar_Cliente"))
                        {
                            btnINC.Visible = true;
                            btnINS.Visible = false;
                            btnAUT.Visible = false;
                        }
                    }

                    if (e.Row.Cells[4].Text == "INC")
                    {
                        if (informacionUsuario.accionesPermitidas.Contains("Ingresar_Vehiculo"))
                        {
                            btnINC.Visible = false;
                            btnINS.Visible = true;
                            btnAUT.Visible = false;
                        }                        
                    }

                    if (e.Row.Cells[4].Text == "INS")
                    { 
                        if (informacionUsuario.accionesPermitidas.Contains("Autorizar"))
                        {
                            btnINC.Visible = false;
                            btnINS.Visible = false;
                            btnAUT.Visible = true;
                        }
                    }

                    if (e.Row.Cells[4].Text == "AUT")
                    {
                        if (informacionUsuario.accionesPermitidas.Contains("Emitir"))
                        {
                            btnINC.Visible = false;
                            btnINS.Visible = false;
                            btnEMI.Visible = true;
                        }                        
                    }
                }                
            }

            e.Row.Cells[4].Visible = false;
            e.Row.Cells[11].Visible = false;
        }
        private void mostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Principal/Menu_Principal.aspx");
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
        protected void btnEmitirDialog_Click(object sender, EventArgs e)
        {
            EmitirPoliza clEmision = new EmitirPoliza();
            Varias clVarias = new Varias();
            DataTable datosUsuarioLogueado = new DataTable();
            DataTable datosPlanCotizado = new DataTable();
            DataTable datosCotizacionSeleccionada = new DataTable();

            datosUsuarioLogueado = clVarias.obtenerInformacionUsuarioLogueado(informacionUsuario.idUsuario);
            datosPlanCotizado = clVarias.obtenerInformacionPlan(int.Parse(lblIdPlanSeleccionado.Text));
            datosCotizacionSeleccionada = clVarias.obtenerInformacionCotizacion(int.Parse(lblIdCotizacionSeleccionada.Text));

            string respuesta = string.Empty;

            if (datosPlanCotizado.Rows[0]["nombre"].ToString().Contains("HOGAR"))
            {
                respuesta = clEmision.EmitirPolizaHogarSeguro(int.Parse(lblIdCotizacionSeleccionada.Text), int.Parse(lblIdPlanSeleccionado.Text), informacionUsuario);
            }
            else
            {
                DateTime fecha = DateTime.Parse(txtFechaInicioVigenciaPoliza.Text);

                //if (fecha.Date < DateTime.Parse(datosCotizacionSeleccionada.Rows[0]["fecha_cotizacion"].ToString()).Date)
                //{
                //    lblRespuesta.Text = "La fecha de inicio de vigencia de la poliza no puede ser menor a la fecha de la cotización.";
                //    lblRespuesta.Visible = true;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { abrirResultado(); });", true);
                //}
                //else
                //{
                    respuesta = clEmision.Emitir(lblIdCotizacionSeleccionada.Text, datosUsuarioLogueado, int.Parse(lblIdPlanSeleccionado.Text), informacionUsuario, fecha.ToString("dd/MM/yyyy"));

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
        }
        protected void btnSi_Click(object sender, EventArgs e)
        {
            string idCotizacion = lblIdCotizacionEliminar.Text;
            bool resultadoEliminar = false;

            resultadoEliminar = objetoUpdate.DeleteInformacionCotizacionOracle(idCotizacion);

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

            string IdCotizacionParaEmitir = lblIdCotizacionDetalle.Text;
            string idPlanCotizadoParaEmitir = lblIdPlanDetalle.Text;

            if (permisoVigenciaPoliza.Count() == 1)
            {
                lblIdCotizacionSeleccionada.Text = IdCotizacionParaEmitir;
                lblIdPlanSeleccionado.Text = idPlanCotizadoParaEmitir;
                txtFechaInicioVigenciaPoliza.Text = DateTime.Today.ToString("dd/MM/yyyy");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Mensaje", "$(function() { PedirFechaVigencia(); });", true);
            }
            else
            {
                EmitirPoliza clEmision = new EmitirPoliza();
                Varias clVarias = new Varias();
                DataTable datosUsuarioLogueado = new DataTable();
                DataTable datosPlanCotizado = new DataTable();

                datosPlanCotizado = clVarias.obtenerInformacionPlan(int.Parse(idPlanCotizadoParaEmitir));
                datosUsuarioLogueado = clVarias.obtenerInformacionUsuarioLogueado(informacionUsuario.idUsuario);

                string respuesta = string.Empty;

                if (datosPlanCotizado.Rows[0]["nombre"].ToString().Contains("HOGAR"))
                {
                    respuesta = clEmision.EmitirPolizaHogarSeguro(int.Parse(IdCotizacionParaEmitir), int.Parse(idPlanCotizadoParaEmitir), informacionUsuario);
                }
                else
                {
                    respuesta = clEmision.Emitir(IdCotizacionParaEmitir, datosUsuarioLogueado, int.Parse(idPlanCotizadoParaEmitir), informacionUsuario);
                }

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
        private void crearVariablesSession(int idCotizacion, int idPlan)
        {
            Session["cotId"] = idCotizacion;
            Session["idPlan"] = idPlan;
        }
    }
}