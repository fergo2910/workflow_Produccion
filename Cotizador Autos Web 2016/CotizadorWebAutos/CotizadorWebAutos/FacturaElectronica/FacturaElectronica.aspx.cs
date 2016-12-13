using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lbl_Cotizado_Autos_Web.PagoEnLinea;
using System.Data;
using System.Xml;
using CrystalDecisions.CrystalReports.Engine;
using Lbl_Cotizador_Autos_Web.RecursosFactura;
using Lbl_Cotizado_Autos_Web.Acceso;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas;
using System.IO;
using Lbl_Cotizador_Autos_Web.Acceso;




namespace CotizadorWebAutos.FacturaElectronica
{
    public partial class FacturaElectronica : System.Web.UI.Page
    {

        IngresoSistema.informacionUsuario informacionUsuario;

        DataTable codigoIntermediario = new DataTable();
        AccesoUsuario clAccesoUsuario = new AccesoUsuario();
        ConsultasBD objetoConsultasBD = new ConsultasBD();
        RequerimientosFact clRequerimientos = new RequerimientosFact();
        DataTable datosUsuarioRemo = new DataTable();
        DataTable DatoPolizaSC = new DataTable();
        DataTable datosCliente = new DataTable();
        DataTable datosClienteNITparaCOMBO = new DataTable();
        DataTable Productos = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["informacionUsuario"] != null)
            {
                informacionUsuario = new IngresoSistema.informacionUsuario();
                informacionUsuario = (IngresoSistema.informacionUsuario)Session["informacionUsuario"];

                if (!IsPostBack)
                {
                    #region codigo viejo
                    ////1. Consulta el tipo de usuario y sus respectivos permisos
                    //if (rolesUsuario.Rows.Count > 0)
                    //{
                    //    //Esta persona solo puede consultar facturas que tengan su nit, es necesario el nit para esta busqueda la cual se asigna al crear el usuario
                    //    DataRow[] rol = rolesUsuario.Select("nombre = 'CONSULTA_FACTURA'");

                    //    if (rol.Count() == 1)
                    //    {
                    //        datosUsuarioRemo = clRequerimientos.sacandoUsuarioNit(idUsuario);

                    //        DataRow row = datosUsuarioRemo.Rows[0];
                    //        //datoC = row["correo_electronico"].ToString();


                    //        if (datosUsuarioRemo.Rows.Count > 0)
                    //        {
                    //            //COLOCAMOS DATOS DEL CLIENTE POR DEFECTO
                    //             txtNumIDCliente.Text = row["NUMID"].ToString();
                    //            txtDvIdCliente.Text = row["DVID"].ToString();

                    //            txtNumIDCliente.Enabled = false;
                    //            txtDvIdCliente.Enabled = false;

                    //            //llenando combos con vigencias para cliente final (solo en este caso)
                    //            datosClienteNITparaCOMBO = clRequerimientos.comboañosCONSULTA_FACTURA(row["NUMID"].ToString() + row["DVID"].ToString());
                    //            ddlaños.DataSource = datosClienteNITparaCOMBO;
                    //            ddlaños.DataTextField = "FECHA";
                    //            ddlaños.DataValueField = "FECHA";
                    //            ddlaños.DataBind();
                    //            ddlaños.Items.Insert(0, "Todos");

                    //        }
                    //        else
                    //        {
                    //            mostrarMensaje("Error al enviar datos consulte con informática");
                    //        }                       
                    //    }

                    //    DataRow[] rolSC = rolesUsuario.Select("nombre = 'CONSULTA_FACTURA_COLECTIVA'");

                    //    if (rolSC.Count() == 1)
                    //    {
                    //        datosUsuarioRemo = clRequerimientos.sacandoUsuarioNit(idUsuario);
                    //        DataRow rowsc2 = datosUsuarioRemo.Rows[0];

                    //        //datoC = row["correo_electronico"].ToString();
                    //        if (datosUsuarioRemo.Rows.Count > 0)
                    //        {
                    //            //COLOCAMOS DATOS DEL CLIENTE POR DEFECTO
                    //            string NUMIDSC = rowsc2["NUMID"].ToString();
                    //            string DVIDSC = rowsc2["DVID"].ToString();

                    //            DatoPolizaSC = clRequerimientos.BuscandopolizaSeguroColectivo(NUMIDSC, DVIDSC);
                    //            DataRow ROWSC = DatoPolizaSC.Rows[0];

                    //            txtCodPol2.Text = ROWSC["CODPOL"].ToString();
                    //            txtNumPol.Text = ROWSC["NUMPOL"].ToString();
                    //            txtCodPol2.Enabled = false;
                    //            txtNumPol.Enabled = false;

                    //            //llenando combo de años general
                    //            for (int i = (DateTime.Now.Year - 10); i <= DateTime.Now.Year; i++)
                    //            {
                    //                ListItem item = new ListItem(i.ToString(), i.ToString());
                    //                ddlaños.Items.Add(item);
                    //                SortListControl(ddlaños, false);
                    //            }
                    //            ddlaños.Items.Insert(0, "Todos");
                    //        }
                    //        else
                    //        {
                    //            mostrarMensaje("Error al enviar datos consulte con informática");
                    //        }
                    //    }    

                    //    else
                    //    { //pnlimagenrelleno.Visible = false;
                    //        //pnldesdehasta.Visible = true;

                    //        //llenando combo de años general
                    //        for (int i = (DateTime.Now.Year - 10); i <= DateTime.Now.Year; i++)
                    //        {
                    //            ListItem item = new ListItem(i.ToString(), i.ToString());
                    //            ddlaños.Items.Add(item);
                    //            SortListControl(ddlaños, false);
                    //        }
                    //        ddlaños.Items.Insert(0, "Todos");
                    //    }
                    //}
                    #endregion

                    //1.Permiso 1 Todos las facturas
                    //2.Permiso 2 Solo las facturas de intermediario
                    //3.Permiso 3 Solo las facturas de usuario (nit o poliza)

                    // Permiso 1 precedencia 1. Permiso administrador de facturas. Puede ver toda factura del sistema. Permiso: CONSULTA_GENERAL_FACTURA
                    if (!informacionUsuario.accionesPermitidas.Contains(ObtenerPermisoPorPrecedencia(1)))
                    {
                        // Permiso 2 precedencia 2. Este rol consulta toda factura que tenga asociado su intermediario. Permiso: CONSULTA_FACTURA_COLECTIVA
                        if (informacionUsuario.accionesPermitidas.Contains(ObtenerPermisoPorPrecedencia(2)))
                        {
                            //todos los productos por el momento, pero se debe mostrar solo productos del intermediario
                        }

                        // Permiso 3 precedencia 3 la más baja, donde un usuario consulta sus propias facturas. Permiso: CONSULTA_FACTURA
                        else if (informacionUsuario.accionesPermitidas.Contains(ObtenerPermisoPorPrecedencia(3)))
                        {
                            DataTable UsuarioActual = clRequerimientos.sacandoUsuarioNit(informacionUsuario.idUsuario.ToString());
                            DataRow DatosUsuario = UsuarioActual.Rows[0];
                            txtNumIDCliente.Text = DatosUsuario["NUMID"].ToString();
                            txtDvIdCliente.Text = DatosUsuario["DVID"].ToString();
                            txtNumIDCliente.Enabled = false;
                            txtDvIdCliente.Enabled = false;
                            pnlRequerimiento.Visible = false;
                            pnlNombres.Visible = false;
                            //todos los productos por el momento, pero se debe mostrar solo productos del usuario
                        }

                        //NINGUN PERMISO, NO DEBE TENER ACCESO 
                        else
                        {
                            PanelInicio.Visible = false;
                            btnBuscarPolizas.Visible = false;
                            btnLimpiar.Visible = false;
                            NoPermitido.Visible = true;
                        }
                    }
                    //todos los productos por el momento
                    Productos = clRequerimientos.Productos();
                    txtCodPol2.DataSource = Productos;
                    txtCodPol2.DataTextField = "CODPOL";
                    txtCodPol2.DataValueField = "CODPOL";
                    txtCodPol2.DataBind();
                    txtCodPol2.Items.Insert(0, "        ");
                    //Todos los años por el momento
                    for (int i = (DateTime.Now.Year - 10); i <= DateTime.Now.Year; i++)
                    {
                        ListItem item = new ListItem(i.ToString(), i.ToString());
                        ddlaños.Items.Add(item);
                        SortListControl(ddlaños, false);
                    }
                    ddlaños.Items.Insert(0, "Todos");
                    //para regresar a pagina anterior
                    Regresar.Attributes.Add("onclick", "history.back(); return false;");
                }
            }
        }
        protected void btnBuscarPolizasF_Click(object sender, EventArgs e)
        {           
            RequerimientosFact clFactura = new RequerimientosFact();
            DataTable datosReq = null;
            DataTable datos = new DataTable();
            pnlResultadoBusqueda.Visible = false;
            pnlFacturas.Visible = false;
            lblFact.Visible = false;
            LnlSinRegistro.Visible = false;
            pnlImprime.Visible = false;
            lblFact.Visible = false;
            LabelDesdeHasta.Visible = false;
            lblFact2.Visible = false;
            LnlNingunCampo.Visible = false;
            LnlErrNUMPOL.Visible = false;
            string CODPOL = string.Empty, NUMPOL = string.Empty, NOMTER = string.Empty, APETER = string.Empty, NUMID = string.Empty, DVID = string.Empty, Periodo = string.Empty;

            if (informacionUsuario.accionesPermitidas.Contains(ObtenerPermisoPorPrecedencia(3)) &&
                !informacionUsuario.accionesPermitidas.Contains(ObtenerPermisoPorPrecedencia(2)) &&
                !informacionUsuario.accionesPermitidas.Contains(ObtenerPermisoPorPrecedencia(1)))
            {
                string anio = string.Empty;
                if (!ddlaños.SelectedIndex.ToString().Equals("0"))
                    anio = ddlaños.SelectedItem.Text;

                if (!(txtNumPol.Text.Equals(string.Empty)))
                    NUMPOL = txtNumPol.Text;

                if(!txtCodPol2.SelectedIndex.ToString().Equals("0"))
                    CODPOL = txtCodPol2.SelectedItem.Text;

                datos = clFactura.buscarPolizaRolConsultaFactura(txtNumIDCliente.Text, txtDvIdCliente.Text, anio, CODPOL, NUMID);
                if (datos.Rows.Count > 0)
                {
                    grvPolizas.DataSource = datos;
                    grvPolizas.DataBind();
                    pnlResultadoBusqueda.Visible = true;
                    pnlResultadoBusqueda.Focus();
                }
                else
                {
                    LnlSinRegistro.Visible = true;
                    pnlResultadoBusqueda.Visible = false;
                }
                return;
            }

            if(txtNumIDCliente.Text.Equals(string.Empty) && txtDvIdCliente.Text.Equals(string.Empty) && txtCodPol2.SelectedIndex.ToString().Equals("0") && 
                txtNumPol.Text.Equals(string.Empty) && txtNombre.Text.Equals(string.Empty) && txtApellido.Text.Equals(string.Empty) && 
                txtBusquedaReq.Text.Equals(string.Empty) && ddlaños.SelectedIndex.ToString().Equals("0"))
            {
                LnlNingunCampo.Text = " Ningun campo con datos válidos, por favor ingrese los datos necesarios para buscar las facturas. ";
                LnlNingunCampo.Visible = true;
                return;
            }
            lblInfo.Text = "Factura buscada por medio de: ";
            
            if (!txtBusquedaReq.Text.Equals(string.Empty))
            {
                //busqueda por requerimiento
                string periodoReq = string.Empty;
                 if (!ddlaños.SelectedIndex.ToString().Equals("0"))
                     periodoReq = ddlaños.SelectedItem.Text;
                 if (informacionUsuario.accionesPermitidas.Contains(ObtenerPermisoPorPrecedencia(2)) &&
                     !informacionUsuario.accionesPermitidas.Contains(ObtenerPermisoPorPrecedencia(1))) //intermediario
                     datosReq = clFactura.buscarPorReq(txtBusquedaReq.Text, periodoReq, informacionUsuario.codIntermediario);
                 else
                     datosReq = clFactura.buscarPorReq(txtBusquedaReq.Text, periodoReq);
                pnlImprime.Visible = true;
                grvFacturas.DataSource = datosReq;
                grvFacturas.DataBind();
                pnlFacturas.Visible = true;
                Imprime.Focus();
                return;
            }
            
            if (!(txtNumIDCliente.Text.Equals(string.Empty) || txtDvIdCliente.Text.Equals(string.Empty)))
            {
                lblInfo.Text += "<br/>&nbsp;&nbsp;&nbsp;• NIT";
                NUMID = txtNumIDCliente.Text;
                DVID = txtDvIdCliente.Text;
            }
            if (!(txtNumPol.Text.Equals(string.Empty)))
            {
                lblInfo.Text += "<br/>&nbsp;&nbsp;&nbsp;• Numero de póliza";
                NUMPOL = txtNumPol.Text;
            }

            if (!txtCodPol2.SelectedIndex.ToString().Equals("0"))
            {
                lblInfo.Text += "<br/>&nbsp;&nbsp;&nbsp;• Código de póliza";
                CODPOL = txtCodPol2.SelectedItem.Text; 
            }

            if (!(txtNombre.Text.Equals(string.Empty) && txtApellido.Text.Equals(string.Empty)))
            {
                lblInfo.Text += "<br/>&nbsp;&nbsp;&nbsp;• Nombre";
                NOMTER = "%" + txtNombre.Text.ToUpper() + "%";
                APETER = "%" + txtApellido.Text.ToUpper() + "%";
            }
            if (!ddlaños.SelectedIndex.ToString().Equals("0") && !ddlaños.SelectedIndex.ToString().Equals("-1"))
            {
                Periodo = ddlaños.SelectedItem.Text;
                lblInfo.Text += "<br/>&nbsp;&nbsp;&nbsp;• Periodo: " + Periodo;
            }
            else
                lblInfo.Text += "<br/>&nbsp;&nbsp;&nbsp;• Periodo: Todos";

            int varNumpol = 0;
            if(!NUMPOL.Equals(string.Empty))
                varNumpol = int.Parse(NUMPOL);


            if (informacionUsuario.accionesPermitidas.Contains(ObtenerPermisoPorPrecedencia(2)) &&
                     !informacionUsuario.accionesPermitidas.Contains(ObtenerPermisoPorPrecedencia(1))) //intermediario
                datos = clFactura.buscarPolizaPorIntermediario(cCODPOL: CODPOL, nNUMPOL: varNumpol, cNOMTER: NOMTER, cAPETER: APETER, nNUMID: NUMID, cDVID: DVID, cCodInter: informacionUsuario.codIntermediario, cPeriodo: Periodo);
            else
                datos = clFactura.buscarPoliza(cCODPOL: CODPOL, nNUMPOL: varNumpol, cNOMTER: NOMTER, cAPETER: APETER, nNUMID: NUMID, cDVID: DVID, cPeriodo: Periodo);
            if (datos.Rows.Count > 0)
            {
                grvPolizas.DataSource = datos;
                grvPolizas.DataBind();
                pnlResultadoBusqueda.Visible = true;
                pnlResultadoBusqueda.Focus();
            }
            else
            {
                LnlSinRegistro.Visible = true;
                pnlResultadoBusqueda.Visible = false;
            }
        }
        protected void grvPolizas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[1].Visible = false;
        }
        protected void detallefactura(Object sender, CommandEventArgs e)
        {
            pnlFacturas.Visible = false;
            pnlImprime.Visible = false;

            RequerimientosFact clRequerimientos = new RequerimientosFact();
            DataTable datosfac = new DataTable();
            string IDEPOL = e.CommandArgument.ToString();
           
            datosfac = clRequerimientos.buscarRequerimientosPoliza(int.Parse(IDEPOL),txtNumIDCliente.Text, txtDvIdCliente.Text);

            if (datosfac.Rows.Count > 0)
            {
                pnlImprime.Visible = true;
                grvFacturas.DataSource = datosfac;
                grvFacturas.DataBind();
                pnlFacturas.Visible = true;
                Imprime.Focus();
            }
        }
        protected void FacturaIndividual(Object sender, CommandEventArgs e)
        {
            RequerimientosFact clRequerimientos = new RequerimientosFact();
            DataTable datosfacI = new DataTable();
            ReportDocument objetoReporteI = new ReportDocument();

            string s = e.CommandArgument.ToString();
            string[] words = s.Split(';');
            string IDEFACT = words[0];
            string poliza = words[1];
            string Nombre = words[2];
            string idepol = words[3];

            string nombrePDF = Nombre + "_" + poliza +  "_" + DateTime.Now.Date.ToString("ddMMyyyy") + ".pdf";
            objetoReporteI.Load(Server.MapPath("../Reportes/RPT/FACTURA.rpt"));
            objetoReporteI.SetDataSource(datosfacI = clRequerimientos.ImprimirFacturaIndividual(IDEFACT));

            objetoReporteI.ExportToHttpResponse
                  (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "'"+nombrePDF+"'");

        }
        protected void grvRequerimientos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void grvRequerimientos_SelectedIndexChanged(object sender, EventArgs e)
        { }
        protected void chkFactura_CheckedChanged(object sender, EventArgs e) 
        { }
        protected void ImprimeFact_Click(object sender, EventArgs e)
        {
            CheckBox chkSeleccionado = new CheckBox();

            RequerimientosFact clRequerimientos = new RequerimientosFact();
            DataTable datos = new DataTable();
            ReportDocument objetoReporte2 = new ReportDocument();
            List<string> listadoFacturas = new List<string>();
            Factura fact = new Factura();

            string poliza = string.Empty;
            string idepol = string.Empty;
            string NombreCliente = string.Empty;

            foreach (GridViewRow item in grvFacturas.Rows)
            {
                chkSeleccionado = item.Cells[0].Controls[1] as CheckBox;
                CheckBox chk = (CheckBox)item.FindControl("chkFactura");

                if (chk != null & chk.Checked)
                {
                    listadoFacturas.Add(item.Cells[8].Text);
                    poliza = item.Cells[1].Text;
                    idepol = item.Cells[9].Text;
                    NombreCliente = item.Cells[4].Text;
                }
            }
            if(listadoFacturas.Count<1)
            {
                mostrarMensaje("Seleccione la factura a imprimir");
                return;
            }

            fact.POLIZA = poliza;
            fact.CODCLI = NombreCliente;

            string nombrePDF = fact.CODCLI + "_" + fact.POLIZA + "_" + DateTime.Now.Date.ToString("ddMMyyyy") + ".pdf";



            objetoReporte2.Load(Server.MapPath("../Reportes/RPT/FACTURA.rpt"));

            objetoReporte2.SetDataSource(clRequerimientos.ImprimirFacturas(listadoFacturas));

            objetoReporte2.ExportToHttpResponse
                    (CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "'" + nombrePDF + "'");
            
           

        }
        private void mostrarMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", "alert('" + mensaje + "');", true);
        }
        public static void SortListControl(ListControl control, bool isAscending)
        {
            List<ListItem> collection;

            if (isAscending)
                collection = control.Items.Cast<ListItem>()
                    .Select(x => x)
                    .OrderBy(x => x.Text)
                    .ToList();
            else
                collection = control.Items.Cast<ListItem>()
                    .Select(x => x)
                    .OrderByDescending(x => x.Text)
                    .ToList();

            control.Items.Clear();

            foreach (ListItem item in collection)
                control.Items.Add(item);
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("../FacturaElectronica/FacturaElectronica.aspx");
        }        
        protected void OrderGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbUnidad = (LinkButton)e.Row.FindControl("btnFacIndividual");
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lbUnidad);
            }
        }
        protected void ImprimeMasivo_Click(object sender, EventArgs e)
        {
            LinkButton boton = new LinkButton();
            foreach (GridViewRow item in grvFacturas.Rows)
            {
                boton = item.Cells[0].Controls[1] as LinkButton;
                LinkButton bton = (LinkButton)item.FindControl("btnFacIndividual");              
            }
        }
        protected void grvFacturas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
        }
        public string ObtenerPermisoPorPrecedencia(int rol)
        {
            if (rol == 1)
                return "Descargar_Facturas";
            else if (rol == 2)
                return "Descargar_Facturas_X_Intermediario";
            else if (rol == 3)
                return "Descargar_Factura_Individual";
            else
                return "";
        }
       
    }
}