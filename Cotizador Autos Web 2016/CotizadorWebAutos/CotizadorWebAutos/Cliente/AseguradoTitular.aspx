<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="AseguradoTitular.aspx.cs" Inherits="CotizadorWebAutos.Cliente.AseguradoTitular" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <br />
    <br />

    <div class="col-md-8 col-md-offset-2" style="text-align: center">
        <h2>Busqueda Clientes</h2>
    </div>

    <br />

    <%--BUSQUEDA DEL CLIENTE--%>
    <div class="col-md-4 col-md-offset-4" style="text-align: center">
        <div class="form-inline">
            <div class="col-xs-12">
                <label>Tipo Cliente</label>
                <asp:DropDownList ID="cmbTipoCliente" runat="server" CssClass="form-control">
                    <asp:ListItem Value="P">Individual</asp:ListItem>
                    <asp:ListItem Value="E">Jurídico</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
            </div>
        </div>
    </div>

    <%-- BUSQUEDA DEL NIT --%>
    <div class="col-md-12" style="text-align: center">
        <div class="form-inline">
            <label>NIT :</label>
            <div class="form-group">
                <asp:TextBox ID="txtNumIDCliente" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="110px" onkeypress="return IsNumeric(event);"></asp:TextBox>
                -
                <asp:TextBox ID="txtDvIdCliente" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px" Style="text-transform: uppercase"></asp:TextBox>
                <asp:Button ID="btnBuscarNitCliente" runat="server" Text="Buscar" CssClass="btn-danger" OnClick="btnBuscarNitCliente_Click" />
            </div>
            <hr />
        </div>
    </div>

    <div class="row"></div>

    <%-- ERROR VALIDACION DE INGRESO DE DATOS NUMERICOS --%>
    <div class="col-md-8 col-md-offset-2" style="text-align: center">
        <div class="form-group col-md-12">
            <span id="error" style="color: Red; display: none">* Solo se permiten números (0 - 9)</span>
        </div>
    </div>

    <div class="row"></div>

    <asp:Panel ID="pnlDatosClienteExistente" runat="server" Visible="false">

        <div class="col-md-12" style="text-align: center">
            <h2>Datos del asegurado</h2>
        </div>

        <asp:Panel ID="pnlClienteIndividualExistente" runat="server" Visible="false">

            <div class="col-md-8 col-md-offset-2" style="text-align: left">

                <%----------------DATOS GENERALES DEL Cliente-----------------------%>

                <div class="form-group col-md-12">
                    <label>Nombre</label>
                    <asp:TextBox ID="txtNombreIndividualExistente" runat="server" CssClass="form-control input-sm" Enabled="False"></asp:TextBox>
                </div>

                <%--FECHA NACIMIENTO--%>
                <div class="form-group col-md-3">
                    <label>Fecha Nacimiento</label>
                    <asp:TextBox ID="txtFechaNacimientoIndividualExistente" runat="server" CssClass="form-control input-sm" Enabled="False"></asp:TextBox>
                </div>

                <%--GENERO--%>
                <div class="form-group col-md-3">
                    <label>Género</label>
                    <asp:DropDownList ID="cmbGeneroIndividualExistente" runat="server" CssClass="form-control input-sm" Enabled="False"></asp:DropDownList>
                </div>

                <%--ESTADO CIVIL--%>
                <div class="form-group col-md-3">
                    <label>Estado Civil</label>
                    <asp:DropDownList ID="cmbEstadoCivilIndividualExistente" runat="server" CssClass="form-control input-sm" Enabled="False"></asp:DropDownList>
                </div>

                <%--PROFESION--%>
                <div class="form-group col-md-3">
                    <label>Profesión</label>
                    <asp:DropDownList ID="cmbProfesionIndividualExistente" runat="server" CssClass="form-control input-sm" Enabled="False"></asp:DropDownList>
                </div>

                <%--NACIONALIDAD--%>
                <div class="form-group col-md-3">
                    <label>Nacionalidad</label>
                    <asp:DropDownList ID="cmbNacionalidadIndividualExistente" runat="server" CssClass="form-control input-sm" Enabled="False"></asp:DropDownList>
                </div>

                <%--TIPO IDENTIFICACION--%>
                <div class="form-group col-md-3">
                    <label>Tipo Identificación</label>
                    <asp:DropDownList ID="cmbTipoIdentificacionIndividualExistente" runat="server" CssClass="form-control input-sm" Enabled="False">
                        <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="D">DPI</asp:ListItem>
                        <asp:ListItem Value="P">Pasaporte</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <%--No. IDENTIFICACION--%>
                <div class="form-group col-md-3">
                    <label>No. Identificación</label>
                    <asp:TextBox ID="txtNumeroIdentificacionIndividualExistente" runat="server" Enabled="false" CssClass="form-control input-sm" onkeypress="return IsNumeric(event);"></asp:TextBox>
                </div>

                <%--PAIS EMISION--%>
                <div class="form-group col-md-3">
                    <label>País Emisión</label>
                    <asp:DropDownList ID="cmbPaisEmisionIndividualExistente" runat="server" CssClass="form-control input-sm" AutoPostBack="true"
                        OnSelectedIndexChanged="cmbPaisEmisionIndividualExistente_SelectedIndexChanged" Enabled="False">
                    </asp:DropDownList>
                </div>

                <%--DEPTO EMISION--%>
                <div class="form-group col-md-3">
                    <label>Depto Emisión</label>
                    <asp:DropDownList ID="cmbDeptoEmisionIndividualExistente" runat="server" CssClass="form-control input-sm" AutoPostBack="true"
                        OnSelectedIndexChanged="cmbDeptoEmisionIndividualExistente_SelectedIndexChanged" Enabled="False">
                    </asp:DropDownList>
                </div>

                <%--MUNI EMISION--%>
                <div class="form-group col-md-3">
                    <label>Muni Emisión</label>
                    <asp:DropDownList ID="cmbMuniEmisionIndividualExistente" runat="server" CssClass="form-control input-sm" Enabled="False"></asp:DropDownList>
                </div>

                <%--NEGOCIO PROPIO--%>
                <div class="form-group col-md-6">
                    <label>Nombre Negocio Propio</label>
                    <asp:TextBox ID="txtNegocioPropioIndividualExistente" CssClass="form-control input-sm" runat="server" Enabled="False"></asp:TextBox>
                </div>

                <%--NIT--%>
                <div class="form-group col-md-5">
                    <label>NIT :</label>
                    <div class="form-inline">

                        <div class="form-group">
                            <asp:TextBox ID="txtNumIdIndividualExistente" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="110px" Enabled="False"></asp:TextBox>
                            -
                                <asp:TextBox ID="txtDvIdIndividualExistente" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px" Enabled="False"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <%--ACTUA NOMBRE PROPIO--%>
                <div class="form-group col-md-4">
                    <asp:Label runat="server" Visible="false">Actua en nombre propio?</asp:Label>
                    <asp:CheckBox ID="chkActuaNombrePropioIndividualExistente" Checked="true" runat="server" Enabled="False" Visible="false" />
                </div>

                <%--RESPONSABLE DE PAGO--%>
                <div class="form-group col-md-3">
                    <asp:Label runat="server">Responsable Pago</asp:Label>
                    <%--<label>Responsable Pago</label>--%>
                    <asp:CheckBox ID="chkRespPagoIndividualExistente" runat="server" />
                </div>

                <div class="row"></div>
                <br />

                <div class="form-group col-md-12" style="background-color: whitesmoke">
                    <div class="form-group col-md-12">
                        <asp:Label runat="server" Font-Bold="true"> Información Persona Expuesta Poltíticamente</asp:Label>
                    </div>


                    <%--ES PEP--%>
                    <div class="form-group col-md-6">
                        <br />
                        <asp:CheckBox ID="chkEsPepIndividualExistente" runat="server"
                            Text="Es Persona Expuesta Políticamente (PEP)" Font-Size="8" Enabled="False" />
                    </div>

                    <%--REL CON PEP--%>
                    <div class="form-group col-md-6">
                        <br />

                        <asp:CheckBox ID="chkRelPepIndividualExistente" runat="server"
                            Text="Tiene relación con Persona Expuesta Políticamente (PEP)" Font-Size="8" Enabled="False" />
                    </div>


                    <div class="row"></div>

                    <%--ASOCIADO PEP--%>
                    <div class="form-group col-md-6">
                        <asp:CheckBox ID="chkAsociadoPepIndividualExistente" runat="server"
                            Text="Esta asociado con Persona Expuesta Políticamente (PEP)" Font-Size="8" Enabled="False" />
                    </div>

                    <div class="row"></div>
                    <br />

                </div>

                <%--TELEFONO--%>
                <div class="form-group col-md-2" style="height: 150px; width: 200px; overflow: auto;">
                    <label>Teléfonos</label>
                    <asp:GridView ID="grvTelefonosIndividualExistente" runat="server" AutoGenerateColumns="False" Enabled="False">
                        <Columns>
                            <asp:BoundField DataField="TELEFONO" HeaderText="Número" ReadOnly="True" />
                            <asp:BoundField DataField="TIPO" HeaderText="Tipo" ReadOnly="True" />
                        </Columns>
                    </asp:GridView>
                </div>

                <%--DIRECCIONES--%>
                <div class="form-group col-md-9" style="height: 150px; width: 500px; overflow: auto;">
                    <label>Direcciones</label>
                    <asp:GridView ID="grvDireccionesIndividualExistente" runat="server" AutoGenerateColumns="False" Enabled="False">
                        <Columns>
                            <asp:BoundField DataField="DIRECCION" HeaderText="Dirección" ReadOnly="True" />
                            <asp:BoundField DataField="TIPO" HeaderText="Tipo" ReadOnly="True" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="ROW"></div>

                <%--CORREO ELECTRONICO--%>
                <div class="form-group col-md-5">
                    <label>Correo Electrónico</label>
                    <asp:TextBox ID="txtCorreoIndividualExistente" runat="server" CssClass="form-control input-sm" Enabled="False" TextMode="Email" placeholder="Ingresar correo"></asp:TextBox>
                </div>

                <%--BOTON GUARDAR NUEVO CLIENTE INDIVIDUAL--%>
                <div class="form-group col-md-12">
                    <asp:Button ID="btnGuardarClienteIndividualExistente" runat="server" Text="Guardar Cliente" CssClass="btn btn-danger pull-right"
                        OnClick="btnGuardarClienteIndividualExistente_Click" />
                </div>
            </div>

        </asp:Panel>

        <asp:Panel ID="pnlClienteJuridicoExistente" runat="server" Visible="false">

            <div class="col-md-8 col-md-offset-2" style="text-align: left">

                <%--DATOS GENERALES DEL CLIENTE JURIDICO--%>

                <%--NOMBRE PERSONA JURIDICA--%>
                <div class="form-group col-md-7">
                    <label>Nombre</label>
                    <asp:TextBox ID="txtNombrePersonaJuridicaJuridicoExistente" Enabled="false" placeholder="de la persona jurídica" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>

                <%--NIT PERSONA JURIDICA--%>
                <div class="form-group col-md-5">
                    <label>NIT :</label>
                    <div class="form-inline">

                        <div class="form-group">
                            <asp:TextBox ID="txtNumIdPersonaJuridicaJuridicoExistente" Enabled="false" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="110px"></asp:TextBox>
                            -
                                <asp:TextBox ID="txtDvIdPersonaJuridicaJuridicoExistente" Enabled="false" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <%--NOMBRE REPRESENTANTE LEGAL--%>
                <div class="form-group col-md-7">
                    <label>Nombre representante legal</label>
                    <asp:TextBox ID="txtNombreRepLegalJuridicoExistente" Enabled="false" runat="server" placeholder="del representante legal" CssClass="form-control input-sm"></asp:TextBox>
                </div>

                <%--NIT REPRESENTANTE LEGAL--%>
                <div class="form-group col-md-5">
                    <label>NIT :</label>
                    <div class="form-inline">

                        <div class="form-group">
                            <asp:TextBox ID="txtNumIdRepLegalJuridicoExistente" Enabled="false" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="110px"></asp:TextBox>
                            -
                                <asp:TextBox ID="txtDvIdRepLegalJuridicoExistente" Enabled="false" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <%--FECHA CONSTITUCION--%>
                <div class="form-group col-md-3">
                    <label>Fecha Constitución</label>
                    <asp:TextBox ID="txtFechaConstitucionJuridicoExistente" Enabled="false" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>

                <%--ORIGEN DE LA EMPRESA--%>
                <div class="form-group col-md-3">
                    <label>Origen Empresa</label>
                    <asp:DropDownList ID="cmbOrigenEmpresaJuridicoExistente" Enabled="false" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                </div>

                <%--RESPONSABLE DE PAGO--%>
                <div class="form-group col-md-3 col-md-offset-1">
                    <asp:Label runat="server">Responsable Pago</asp:Label>
                    <%--<label>Responsable Pago</label>--%>
                    <asp:CheckBox ID="chkRespPagoJuridicoExistente" runat="server" />
                </div>

                <%--ACTIVIDAD ECONOMICA--%>
                <div class="form-group col-md-11">
                    <label>Actividad Económica</label>
                    <asp:TextBox ID="txtActividadEconomicaJuridicoExistente" Enabled="false" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>

                <%--ES PEP--%>
                <div class="form-group col-md-3 col-md-offset-2">
                    <asp:CheckBox ID="chkEsPepJuridicoExistente" runat="server"
                        Text="Es Persona Expuesta Politicamente (PEP)" Enabled="False" Font-Size="8pt" />
                </div>

                <%--REL CON PEP--%>
                <div class="form-group col-md-3">
                    <asp:CheckBox ID="chkRelPepJuridicoExistente" runat="server"
                        Text="Tiene relación con una Persona Expuesta Politicamente (PEP)" Enabled="False" Font-Size="8pt" />
                </div>

                <%--ASOCIADO PEP--%>
                <div class="form-group col-md-3">
                    <asp:CheckBox ID="chkAsociadoPepJuridicoExistente" runat="server"
                        Text="Está asociado con una Persona Expuesta Politicamente (PEP)" Enabled="False" Font-Size="8pt" />
                </div>

                <%--TELEFONO--%>
                <div class="form-group col-md-2" style="height: 150px; width: 200px; overflow: auto;">
                    <label>Teléfonos</label>
                    <asp:GridView ID="grvTelefonosJuridicoExistente" runat="server" AutoGenerateColumns="False" Enabled="False">
                        <Columns>
                            <asp:BoundField DataField="TELEFONO" HeaderText="Número" ReadOnly="True" />
                            <asp:BoundField DataField="TIPO" HeaderText="Tipo" ReadOnly="True" />
                        </Columns>
                    </asp:GridView>
                </div>

                <%--DIRECCION--%>
                <div class="form-group col-md-9" style="height: 150px; width: 500px; overflow: auto;">
                    <label>Direcciones</label>
                    <asp:GridView ID="grvDireccionesJuridicoExistente" runat="server" AutoGenerateColumns="False" Enabled="False">
                        <Columns>
                            <asp:BoundField DataField="DIRECCION" HeaderText="Dirección" ReadOnly="True" />
                            <asp:BoundField DataField="TIPO" HeaderText="Tipo" ReadOnly="True" />
                        </Columns>
                    </asp:GridView>
                </div>

                <%--CORREO ELECTRONICO--%>
                <div class="form-group col-md-5">
                    <label>Correo Electrónico</label>
                    <asp:TextBox ID="txtCorreoJuridicoExistente" Enabled="false" runat="server" CssClass="form-control input-sm" TextMode="Email" placeholder="Ingresar correo"></asp:TextBox>
                </div>

                <%--BOTON GUARDAR NUEVO CLIENTE JURIDICO--%>
                <div class="form-group col-md-12">
                    <asp:Button ID="btnGuardarJuridicoExistente" runat="server" Text="Guardar Cliente" CssClass="btn btn-danger pull-right"
                        OnClick="btnGuardarJuridicoExistente_Click" />
                </div>

            </div>

        </asp:Panel>

    </asp:Panel>

    <asp:Panel ID="pnlDatosClienteNuevo" runat="server" Visible="false">

        <div class="col-md-12" style="text-align: center">
            <h2>Datos del asegurado</h2>
        </div>

        <asp:Panel ID="pnlNuevoAseguradoIndividual" runat="server" Visible="false">

            <div class="col-md-8 col-md-offset-2" style="text-align: left">

                <%----------------DATOS GENERALES DEL Cliente-----------------------%>
                <asp:Panel runat="server" CssClass="" GroupingText="Información General" Style="border: none">
                    <%--PRIMER NOMBRE--%>
                    <div class="form-group col-md-6">
                        <label>Primer Nombre</label>
                        <asp:TextBox ID="txtPrimerNombreNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <%--SEGUNDO NOMBRE--%>
                    <div class="form-group col-md-6">
                        <label>Segundo Nombre</label>
                        <asp:TextBox ID="txtSegundoNombreNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <%--PRIMER APELLIDO--%>
                    <div class="form-group col-md-6">
                        <label>Primer Apellido</label>
                        <asp:TextBox ID="txtPrimerApellidoNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <%--SEGUNDO APELLIDO--%>
                    <div class="form-group col-md-6">
                        <label>Segundo Apellido</label>
                        <asp:TextBox ID="txtSegundoApellidoNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <%--FECHA NACIMIENTO--%>
                    <div class="form-group col-md-3">
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                            TargetControlID="txtFechaNacimientoNuevoAsegIndividual"
                            Mask="99/99/9999"
                            MaskType="Date" />
                        <label>Fecha Nacimiento</label>
                        <asp:TextBox ID="txtFechaNacimientoNuevoAsegIndividual" placeholder="dd/MM/yyyy" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <%--GENERO--%>
                    <div class="form-group col-md-3">
                        <label>Género</label>
                        <asp:DropDownList ID="cmbGeneroNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>

                    <%--ESTADO CIVIL--%>
                    <div class="form-group col-md-3">
                        <label>Estado Civil</label>
                        <asp:DropDownList ID="cmbEstadoCivilNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>

                    <%--PROFESION--%>
                    <div class="form-group col-md-3">
                        <label>Profesión</label>
                        <asp:DropDownList ID="cmbProfesionNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>

                    <%--NACIONALIDAD--%>
                    <div class="form-group col-md-3">
                        <label>Nacionalidad</label>
                        <asp:DropDownList ID="cmbNacionalidadNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>

                    <%--NIT--%>
                    <div class="form-group col-md-3">
                        <label>NIT :</label>
                        <div class="form-inline">

                            <div class="form-group">
                                <asp:TextBox ID="txtNumIdNuevoAsegIndividual" Enabled="false" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="110px" onkeypress="return IsNumeric(event);"></asp:TextBox>
                                -
                                <asp:TextBox ID="txtDvIdNuevoAsegIndividual" Enabled="false" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <%--CORREO ELECTRONICO--%>
                    <div class="form-group col-md-3">
                        <label>Correo Electrónico</label>
                        <asp:TextBox ID="txtCorreoNuevoAsegIndividual" TextMode="Email" runat="server" CssClass="form-control input-sm" placeholder="Ingresar correo"></asp:TextBox>
                    </div>

                    <%--TELEFONO--%>
                    <div class="form-group col-md-3">
                        <label>Teléfono</label>
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:TextBox ID="txtTelefonoNuevoAsegIndividual" runat="server" MaxLength="8" CssClass="form-control input-sm" onkeypress="return IsNumeric(event);"></asp:TextBox>
                                <asp:Label runat="server" Visible="false">Tipo</asp:Label>
                                <asp:DropDownList ID="cmbTipoTelefonoNuevoAsegIndividual" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>

                                <asp:LinkButton ID="btnAgregarTelefonoNuevoAsegIndivifual" runat="server" CssClass="btn btn-success btn-sm" ToolTip="Agregar Teléfono" OnClick="btnAgregarTelefonoNuevoAsegIndivifual_Click"><i class="glyphicon glyphicon-earphone"></i></asp:LinkButton>
                            </div>
                        </div>
                    </div>

                    <%--NEGOCIO PROPIO--%>
                    <div class="form-group col-md-8">
                        <label>Nombre Negocio Propio</label>
                        <asp:TextBox ID="txtNegocioPropioNuevoAsegIndividual" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                    </div>

                    <%--TELEFONOS--%>
                    <div class="form-group col-md-4">
                        <asp:Label runat="server" ID="lbTelefonoIndvidual">Teléfonos</asp:Label>
                        <asp:GridView ID="grvTelefonosNuevoAsegIndividual" runat="server"></asp:GridView>
                    </div>
                </asp:Panel>

                <div class="row"></div>

                <%----------------IDENTIFICACION Cliente-----------------------%>
                <asp:Panel runat="server" CssClass="" GroupingText="Identificación" Style="border: none">
                    <%--TIPO IDENTIFICACION--%>
                    <div class="form-group col-md-3">
                        <label>Tipo Identificación</label>
                        <asp:DropDownList ID="cmbTipoIdentificacionNuevoAsegIndividual" runat="server" CssClass="form-control input-sm">
                            <asp:ListItem Value="D">DPI</asp:ListItem>
                            <asp:ListItem Value="P">Pasaporte</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <%--# IDENTIFICACION--%>
                    <div class="form-group col-md-2">
                        <label>No. Identificación</label>
                        <asp:TextBox ID="txtNumeroIdentificacionNuevoAsegIndividual" runat="server" CssClass="form-control input-sm" onkeypress="return IsNumeric(event);"></asp:TextBox>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%--PAIS EMISION--%>
                            <div class="form-group col-md-2">
                                <label>País Emisión</label>
                                <asp:DropDownList ID="cmbPaisEmisionNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="cmbPaisEmisionNuevoAsegIndividual_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <%--DEPTO EMISION--%>
                            <div class="form-group col-md-2">
                                <label>Depto Emisión</label>
                                <asp:DropDownList ID="cmbDeptoEmisionNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="cmbDeptoEmisionNuevoAsegIndividual_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <%--MUNI EMISION--%>
                            <div class="form-group col-md-2">
                                <label>Muni Emisión</label>
                                <asp:DropDownList ID="cmbMuniEmisionNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cmbPaisEmisionNuevoAsegIndividual" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </asp:Panel>

                <div class="row"></div>

                <%-- DIRECCION DEL CLIENTE --%>
                <asp:Panel runat="server" CssClass="" GroupingText="Dirección" Style="border: none">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <%--PAIS--%>
                            <div class="form-group col-md-3">
                                <label>País</label>
                                <asp:DropDownList ID="cmbPaisDireccionNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="cmbPaisDireccionNuevoAsegIndividual_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <%--DEPARTAMENTO--%>
                            <div class="form-group col-md-3">
                                <label>Departamento</label>
                                <asp:DropDownList ID="cmbDeptoDireccionNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="cmbDeptoDireccionNuevoAsegIndividual_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <%--MUNICIPIO--%>
                            <div class="form-group col-md-3">
                                <label>Municipio</label>
                                <asp:DropDownList ID="cmbMuniDireccionNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="cmbMuniDireccionNuevoAsegIndividual_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <%--ZONA--%>
                            <div class="form-group col-md-3">
                                <label>Zona</label>
                                <asp:DropDownList ID="cmbZonaDireccionNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </div>

                            <div class="row"></div>

                            <%--CALLE--%>
                            <div class="form-group col-md-2">
                                <label>Calle</label>
                                <asp:TextBox ID="txtCalleDireccionNuevoAsegIndividual" CssClass="form-control input-sm" runat="server" MaxLength="3"></asp:TextBox>
                            </div>

                            <%--AVENIDA--%>
                            <div class="form-group col-md-1">
                                <label>Avenida</label>
                                <asp:TextBox ID="txtAvenidaDireccionNuevoAsegIndividual" CssClass="form-control input-sm" runat="server" MaxLength="3"></asp:TextBox>
                            </div>

                            <%--NUMERO CASA/APARTAMENTO--%>
                            <div class="form-group col-md-3">
                                <label>No. Casa/Apartamento</label>
                                <asp:TextBox ID="txtNumCasaDireccionNuevoAsegIndividual" CssClass="form-control input-sm" runat="server" MaxLength="10"></asp:TextBox>
                            </div>

                            <%--COLONIA--%>
                            <div class="form-group col-md-3">
                                <label>Colonia</label>
                                <asp:TextBox ID="txtColoniaDireccionNuevoAsegIndividual" CssClass="form-control input-sm" runat="server" MaxLength="50"></asp:TextBox>
                            </div>

                            <%--EDIFICIO O COMPLEMENTO--%>
                            <div class="form-group col-md-3">
                                <label>Edificio ó Complemento</label>
                                <asp:TextBox ID="txtEdificioDireccionNuevoAsegIndividual" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>

                            <%--LOTE--%>
                            <div class="form-group col-md-3">
                                <label>Lote</label>
                                <asp:TextBox ID="txtLoteDireccionNuevoAsegIndividual" CssClass="form-control input-sm" runat="server" MaxLength="3" TextMode="Number"></asp:TextBox>
                            </div>

                            <%--Manzana--%>
                            <div class="form-group col-md-3">
                                <label>Manzana</label>
                                <asp:TextBox ID="txtManzanaDireccionNuevoAsegIndividual" CssClass="form-control input-sm" runat="server" MaxLength="5"></asp:TextBox>
                            </div>

                            <%--Sector--%>
                            <div class="form-group col-md-3">
                                <label>Sector</label>
                                <asp:TextBox ID="txtSectorDireccionNuevoAsegIndividual" CssClass="form-control input-sm" runat="server" MaxLength="40"></asp:TextBox>
                            </div>

                            <%--TIPO DIRECCION--%>
                            <%--<div class="form-group col-md-4">
                            <label>Tipo</label>
                            <div class="form-inline">
                                <asp:DropDownList ID="cmbTipoDireccionNuevoAsegIndividual" runat="server" CssClass="form-control input-sm"></asp:DropDownList>

                                <asp:LinkButton ID="btnAgregarDireccionNuevoAsegIndividual" runat="server" CssClass="btn btn-success btn-sm" ToolTip="Agregar Dirección" OnClick="btnAgregarDireccionNuevoAsegIndividual_Click" Visible="false"><i class="glyphicon glyphicon-file"></i></asp:LinkButton>
                            </div>

                        </div>--%>



                            <%--DIRECCIONES--%>
                            <%-- <div class="form-group col-md-7">

                            <asp:Label runat="server" ID="lbDireccionesIndividual">Direcciones</asp:Label>
                            <asp:GridView ID="grvDireccionesNuevoAsegIndividual" runat="server" AutoGenerateColumns="False" OnRowCommand="grvDireccionesNuevoAsegIndividual_RowCommand">
                                <Columns>
                                    <asp:ButtonField CommandName="Borrar" Text="Borrar" />
                                    <asp:BoundField DataField="Direccion" HeaderText="Dirección">
                                        <ItemStyle Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pais" HeaderText="País" />
                                    <asp:BoundField DataField="Departamento" HeaderText="Departamento" />
                                    <asp:BoundField DataField="Municipio" HeaderText="Municipio" />
                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                                    <asp:BoundField DataField="codPais" HeaderText="codPais" Visible="False" />
                                    <asp:BoundField DataField="codEstado" HeaderText="codDepto" Visible="False" />
                                    <asp:BoundField DataField="codMuni" HeaderText="codMuni" Visible="False" />
                                    <asp:BoundField DataField="codLval" HeaderText="codTipo" Visible="False" />
                                </Columns>
                            </asp:GridView>
                        </div>--%>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cmbPaisDireccionNuevoAsegIndividual" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>

                <%--RESPONSABLE DE PAGO--%>
                <div class="form-group col-md-4 col-lg-offset-8 text-right">
                    <asp:Label runat="server" Font-Bold="true" ID="lbResPagoInd">Responsable Pago</asp:Label>
                    <asp:CheckBox ID="chkRespPagoNuevoAsegIndividual" runat="server" />
                </div>

                <div class="row"></div>

                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <%--ES PEP--%>
                        <div class="form-group col-md-3 col-md-offset-1">
                            <asp:CheckBox ID="chkEsPepNuevoAsegIndividual" AutoPostBack="true" runat="server"
                                Text="Es Persona Expuesta Politicamente (PEP)?" OnCheckedChanged="chkEsPepNuevoAsegIndividual_CheckedChanged" Font-Size="8pt" />
                        </div>

                        <%--REL CON PEP--%>
                        <div class="form-group col-md-3">
                            <asp:CheckBox ID="chkRelPepNuevoAsegIndividual" AutoPostBack="true" runat="server"
                                Text="Tiene relación con una Persona Expuesta Politicamente (PEP)?" OnCheckedChanged="chkRelPepNuevoAsegIndividual_CheckedChanged" Font-Size="8pt" />
                        </div>

                        <%--ASOCIADO PEP--%>
                        <div class="form-group col-md-3">
                            <asp:CheckBox ID="chkAsociadoPepNuevoAsegIndividual" AutoPostBack="true" runat="server"
                                Text="Está asociado con una Persona Expuesta Politicamente (PEP)?" OnCheckedChanged="chkAsociadoPepNuevoAsegIndividual_CheckedChanged" Font-Size="8pt" />
                        </div>

                        <div class="row"></div>

                        <asp:Panel ID="pnlPepNuevoIndividual" runat="server" Visible="false">

                            <ul class="nav nav-tabs col-md-12 col-md-offset-1">
                                <li id="menuEsPepNuevoIndividual" class="active"><a data-toggle="tab" href="#divEsPepNuevoIndividual">Persona Expuesta Politicamente</a></li>
                                <li id="menuRelPepNuevoIndividual"><a data-toggle="tab" href="#divRelPepNuevoIndividual">Parentezco PEP</a></li>
                                <li id="menuAsociadoPepNuevoIndividual"><a data-toggle="tab" href="#divAsociadoPepNuevoIndividual">Asociado PEP</a></li>
                            </ul>

                            <div class="tab-content">
                                <div id="divEsPepNuevoIndividual" class="tab-pane fade in active">

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Solicitante</label>
                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Representante Legal</label>
                                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Beneficiario</label>
                                        <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-7" style="border: none">
                                        <label>Otros firmantes y/o Tarjetahabientes adicionales</label>
                                        <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-5" style="border: none">
                                        <label>Fecha Vencimiento Estado PEP</label>
                                        <asp:TextBox ID="TextBox3" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-8" style="border: none">
                                        <label>Nombre Institución o Donde Trabaja</label>
                                        <asp:TextBox ID="TextBox1" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Condición</label>
                                        <asp:DropDownList ID="DropDownList6" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-8" style="border: none">
                                        <label>Puesto que desempeña</label>
                                        <asp:TextBox ID="TextBox2" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>País de la institución o Ente</label>
                                        <asp:DropDownList ID="DropDownList9" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                </div>

                                <div id="divRelPepNuevoIndividual" class="tab-pane fade">

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Solicitante</label>
                                        <asp:DropDownList ID="DropDownList5" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Representante Legal</label>
                                        <asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Beneficiario</label>
                                        <asp:DropDownList ID="DropDownList8" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-7" style="border: none">
                                        <label>Otros firmantes y/o Tarjetahabientes adicionales</label>
                                        <asp:DropDownList ID="DropDownList10" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-5" style="border: none">
                                        <label>Condición</label>
                                        <asp:DropDownList ID="DropDownList11" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-3" style="border: none">
                                        <label>Parentesco</label>
                                        <asp:DropDownList ID="DropDownList13" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-6" style="border: none">
                                        <label>Especifique</label>
                                        <asp:TextBox ID="TextBox5" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-3" style="border: none">
                                        <label>Género</label>
                                        <asp:DropDownList ID="DropDownList14" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Primer Nombre</label>
                                        <asp:TextBox ID="TextBox4" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Segundo Nombre</label>
                                        <asp:TextBox ID="TextBox6" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Otros Nombres</label>
                                        <asp:TextBox ID="TextBox7" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Primer Apellido</label>
                                        <asp:TextBox ID="TextBox8" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Segundo Apellido</label>
                                        <asp:TextBox ID="TextBox9" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Apellido Casada</label>
                                        <asp:TextBox ID="TextBox10" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-12" style="border: none">
                                        <label>Nombre Institución o Donde Trabaja</label>
                                        <asp:TextBox ID="TextBox11" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-8" style="border: none">
                                        <label>Puesto que desempeña</label>
                                        <asp:TextBox ID="TextBox12" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>País de la institución o Ente</label>
                                        <asp:DropDownList ID="DropDownList12" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                </div>

                                <div id="divAsociadoPepNuevoIndividual" class="tab-pane fade">

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Solicitante</label>
                                        <asp:DropDownList ID="DropDownList15" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Representante Legal</label>
                                        <asp:DropDownList ID="DropDownList16" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Beneficiario</label>
                                        <asp:DropDownList ID="DropDownList17" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-7" style="border: none">
                                        <label>Otros firmantes y/o Tarjetahabientes adicionales</label>
                                        <asp:DropDownList ID="DropDownList18" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-5" style="border: none">
                                        <label>Condición</label>
                                        <asp:DropDownList ID="DropDownList19" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-3" style="border: none">
                                        <label>Motivo</label>
                                        <asp:DropDownList ID="DropDownList20" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-6" style="border: none">
                                        <label>Especifique</label>
                                        <asp:TextBox ID="TextBox13" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-3" style="border: none">
                                        <label>Género</label>
                                        <asp:DropDownList ID="DropDownList21" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Primer Nombre</label>
                                        <asp:TextBox ID="TextBox14" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Segundo Nombre</label>
                                        <asp:TextBox ID="TextBox15" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Otros Nombres</label>
                                        <asp:TextBox ID="TextBox16" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Primer Apellido</label>
                                        <asp:TextBox ID="TextBox17" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Segundo Apellido</label>
                                        <asp:TextBox ID="TextBox18" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Apellido Casada</label>
                                        <asp:TextBox ID="TextBox19" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-12" style="border: none">
                                        <label>Nombre Institución o Donde Trabaja</label>
                                        <asp:TextBox ID="TextBox20" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-8" style="border: none">
                                        <label>Puesto que desempeña</label>
                                        <asp:TextBox ID="TextBox21" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>País de la institución o Ente</label>
                                        <asp:DropDownList ID="DropDownList22" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                </div>
                            </div>

                        </asp:Panel>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkEsPepNuevoAsegIndividual" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>

                <%--ACTUA NOMBRE PROPIO--%>
                <div class="form-group col-md-4">
                    <asp:Label runat="server" Visible="false">Actua en nombre propio?</asp:Label>
                    <asp:CheckBox ID="chkActuaNombrePropioNuevoAsegIndividual" runat="server" Checked="true" Visible="false" />
                </div>

                <%--BOTON GUARDAR NUEVO CLIENTE INDIVIDUAL--%>
                <div class="form-group col-md-12">
                    <asp:Button ID="btnNoEnviarActualizacion" runat="server" Text="Continuar" CssClass="btn btn-danger pull-right" Visible="false" OnClick="btnNoEnviarActualizacion_Click" />
                    <asp:Button ID="btnGuardarNuevoAsegIndividual" runat="server" Text="Guardar Cliente" CssClass="btn btn-danger pull-right"
                        OnClick="btnGuardarNuevoAsegIndividual_Click" />
                </div>
            </div>

        </asp:Panel>

        <asp:Panel ID="pnlNuevoAseguradoJuridico" runat="server" Visible="false">

            <div class="col-md-8 col-md-offset-2" style="text-align: left">

                <%--DATOS GENERALES DEL CLIENTE JURIDICO--%>

                <%--NOMBRE PERSONA JURIDICA--%>
                <div class="form-group col-md-7">
                    <label>Nombre</label>
                    <asp:TextBox ID="txtNombreNuevoAsegJuridico" placeholder="Nombre de la persona jurídica" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>

                <%--NIT--%>
                <div class="form-group col-md-5">
                    <label>NIT :</label>
                    <div class="form-inline">

                        <div class="form-group">
                            <asp:TextBox ID="txtNumIdNuevoAsegJuridico" runat="server" CssClass="form-control input-sm" onkeypress="return IsNumeric(event);" MaxLength="7" Width="110px"></asp:TextBox>
                            -
                                <asp:TextBox ID="txtDvIdNuevoAsegJuridico" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <%--PRIMER NOMBRE--%>
                <div class="form-group col-md-6">
                    <label>Primer Nombre</label>
                    <asp:TextBox ID="txtPrimerNombreNuevoAsegJuridico" runat="server" placeholder="del representante legal" CssClass="form-control input-sm"></asp:TextBox>
                </div>

                <%--SEGUNDO NOMBRE--%>
                <div class="form-group col-md-6">
                    <label>Segundo Nombre</label>
                    <asp:TextBox ID="txtSegundoNombreNuevoAsegJuridico" runat="server" placeholder="del representante legal" CssClass="form-control input-sm"></asp:TextBox>
                </div>

                <%--PRIMER APELLIDO--%>
                <div class="form-group col-md-6">
                    <label>Primer Apellido</label>
                    <asp:TextBox ID="txtPrimerApellidoNuevoAsegJuridico" runat="server" placeholder="del representante legal" CssClass="form-control input-sm"></asp:TextBox>
                </div>

                <%--SEGUNDO APELLIDO--%>
                <div class="form-group col-md-6">
                    <label>Segundo Apellido</label>
                    <asp:TextBox ID="txtSegundoApellidoNuevoAsegJuridico" runat="server" placeholder="del representante legal" CssClass="form-control input-sm"></asp:TextBox>
                </div>

                <%--TIPO IDENTIFICACION--%>
                <div class="form-group col-md-3">
                    <label>Tipo Identificación</label>
                    <asp:DropDownList ID="cmbTipoIdentificacionNuevoAsegJuridico" runat="server" CssClass="form-control input-sm">
                        <asp:ListItem Value="D">DPI</asp:ListItem>
                        <asp:ListItem Value="P">Pasaporte</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <%--# IDENTIFICACION--%>
                <div class="form-group col-md-3">
                    <label>No. Identificación</label>
                    <asp:TextBox ID="txtNumeroIdentificacionNuevoAsegJuridico" placeholder="del representante legal" runat="server" onkeypress="return IsNumeric(event);" CssClass="form-control input-sm"></asp:TextBox>
                </div>

                <%--FECHA CONSTITUCION--%>
                <div class="form-group col-md-3">
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                        TargetControlID="txtFechaConstitucionNuevoAsegJuridico"
                        Mask="99/99/9999"
                        MaskType="Date" />
                    <label>Fecha Constitución</label>
                    <asp:TextBox ID="txtFechaConstitucionNuevoAsegJuridico" runat="server" placeholder="de la empresa" CssClass="form-control input-sm"></asp:TextBox>
                </div>

                <%--ORIGEN DE LA EMPRESA--%>
                <div class="form-group col-md-3">
                    <label>Origen Empresa</label>
                    <asp:DropDownList ID="cmbOrigenEmpresaNuevoAsegJuridico" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                </div>

                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <%--PAIS EMISION--%>
                        <div class="form-group col-md-3">
                            <label>Pais Emisión</label>
                            <asp:DropDownList ID="cmbPaisEmisionNuevoAsegJuridico" runat="server" CssClass="form-control input-sm" AutoPostBack="true"
                                OnSelectedIndexChanged="cmbPaisEmisionNuevoAsegJuridico_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <%--DEPTO EMISION--%>
                        <div class="form-group col-md-3">
                            <label>Depto Emisión</label>
                            <asp:DropDownList ID="cmbDeptoEmisionNuevoAsegJuridico" runat="server" CssClass="form-control input-sm" AutoPostBack="true"
                                OnSelectedIndexChanged="cmbDeptoEmisionNuevoAsegJuridico_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <%--MUNI EMISION--%>
                        <div class="form-group col-md-3">
                            <label>Muni Emisión</label>
                            <asp:DropDownList ID="cmbMuniEmisionNuevoAsegJuridico" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmbPaisEmisionNuevoAsegJuridico" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

                <%--RESPONSABLE DE PAGO--%>
                <div class="form-group col-md-3">
                    <asp:Label runat="server" ID="lbResPago">Responsable Pago</asp:Label>
                    <%--<label>Responsable Pago</label>--%>
                    <asp:CheckBox ID="chkRespPagoNuevoAsegJuridico" runat="server" />
                </div>

                <%--ACTIVIDAD ECONOMICA--%>
                <div class="form-group col-md-12">
                    <label>Actividad Económica</label>
                    <asp:TextBox ID="txtActividadEconomicaNuevoAsegJuridico" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>

                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <%--PAIS--%>
                        <div class="form-group col-md-3">
                            <label>País</label>
                            <asp:DropDownList ID="cmbPaisDireccionNuevoAsegJuridico" runat="server" CssClass="form-control input-sm"
                                OnSelectedIndexChanged="cmbPaisDireccionNuevoAsegJuridico_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <%--DEPARTAMENTO--%>
                        <div class="form-group col-md-3">
                            <label>Departamento</label>
                            <asp:DropDownList ID="cmbDeptoDireccionNuevoAsegJuridico" runat="server" CssClass="form-control input-sm"
                                OnSelectedIndexChanged="cmbDeptoDireccionNuevoAsegJuridico_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <%--MUNICIPIO--%>
                        <div class="form-group col-md-3">
                            <label>Municipio</label>
                            <asp:DropDownList ID="cmbMuniDireccionNuevoAsegJuridico" OnSelectedIndexChanged="cmbMuniDireccionNuevoAsegJuridico_SelectedIndexChanged" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                        </div>

                        <%--ZONA--%>
                        <div class="form-group col-md-3">
                            <label>Zona</label>
                            <asp:DropDownList ID="cmbZonaDireccionNuevoAsegJuridico" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                        </div>

                        <%--CALLE--%>
                        <div class="form-group col-md-2">
                            <label>Calle</label>
                            <asp:TextBox ID="txtCalleDireccionNuevoAsegJuridico" CssClass="form-control input-sm" runat="server" MaxLength="3"></asp:TextBox>
                        </div>

                        <%--AVENIDA--%>
                        <div class="form-group col-md-1">
                            <label>Avenida</label>
                            <asp:TextBox ID="txtAvenidaDireccionNuevoAsegJuridico" CssClass="form-control input-sm" runat="server" MaxLength="3"></asp:TextBox>
                        </div>

                        <%--NUMERO CASA/APARTAMENTO--%>
                        <div class="form-group col-md-3">
                            <label>No. Casa/Apartamento</label>
                            <asp:TextBox ID="txtNumCasaDireccionNuevoAsegJuridico" CssClass="form-control input-sm" runat="server" MaxLength="10"></asp:TextBox>
                        </div>

                        <%--COLONIA--%>
                        <div class="form-group col-md-3">
                            <label>Colonia</label>
                            <asp:TextBox ID="txtColoniaDireccionNuevoAsegJuridico" CssClass="form-control input-sm" runat="server" MaxLength="50"></asp:TextBox>
                        </div>

                        <%--EDIFICIO O COMPLEMENTO--%>
                        <div class="form-group col-md-3">
                            <label>Edificio ó Complemento</label>
                            <asp:TextBox ID="txtEdificioDireccionNuevoAsegJuridico" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>

                        <%--LOTE--%>
                        <div class="form-group col-md-3">
                            <label>Lote</label>
                            <asp:TextBox ID="txtLoteDireccionNuevoAsegJuridico" CssClass="form-control input-sm" runat="server" MaxLength="3" TextMode="Number"></asp:TextBox>
                        </div>

                        <%--Manzana--%>
                        <div class="form-group col-md-3">
                            <label>Manzana</label>
                            <asp:TextBox ID="txtManzanaDireccionNuevoAsegJuridico" CssClass="form-control input-sm" runat="server" MaxLength="5"></asp:TextBox>
                        </div>

                        <%--Sector--%>
                        <div class="form-group col-md-3">
                            <label>Sector</label>
                            <asp:TextBox ID="txtSectorDireccionNuevoAsegJuridico" CssClass="form-control input-sm" runat="server" MaxLength="40"></asp:TextBox>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmbPaisDireccionNuevoAsegJuridico" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

                <%--TELEFONO--%>
                <div class="form-group col-md-5 col-md-offset-1">
                    <label>Teléfono</label>
                    <div class="form-inline">
                        <div class="form-group">
                            <asp:TextBox ID="txtTelefonoNuevoAsegJuridico" runat="server" MaxLength="8" CssClass="form-control input-sm" onkeypress="return IsNumeric(event);"></asp:TextBox>
                            <asp:Button ID="btnAgregarTelefonoNuevoAsegJuridico" runat="server" Text="+" CssClass="btn btn-success btn-sm " OnClick="btnAgregarTelefonoNuevoAsegJuridico_Click" />
                        </div>
                    </div>
                </div>

                <%--CORREO ELECTRONICO--%>
                <div class="form-group col-md-5">
                    <label>Correo Electrónico</label>
                    <asp:TextBox ID="txtCorreoNuevoAsegJuridico" runat="server" CssClass="form-control input-sm" TextMode="Email" placeholder="Ingresar correo"></asp:TextBox>
                </div>

                <%--TELEFONOS--%>
                <div class="form-group col-md-5 col-md-offset-1">
                    <label>Teléfonos</label>
                    <asp:GridView ID="grvTelefonosNuevoAsegJuridico" runat="server"></asp:GridView>
                </div>
                <div class="row"></div>

                <br />
                <br />

                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <%--ES PEP--%>
                        <div class="form-group col-md-3 col-md-offset-2">
                            <asp:CheckBox ID="chkEsPepNuevoAsegJuridico" runat="server" AutoPostBack="true"
                                Text="Es Persona Expuesta Políticamente (PEP)?" OnCheckedChanged="chkEsPepNuevoAsegJuridico_CheckedChanged" Font-Size="8pt" />
                        </div>

                        <%--REL CON PEP--%>
                        <div class="form-group col-md-3">
                            <asp:CheckBox ID="chkRelPepNuevoAsegJuridico" OnCheckedChanged="chkRelPepNuevoAsegJuridico_CheckedChanged" runat="server" AutoPostBack="true"
                                Text="Tiene relación con una Persona Expuesta Políticamente (PEP)?" Font-Size="8pt" />
                        </div>

                        <%--ASOCIADO PEP--%>
                        <div class="form-group col-md-3">
                            <asp:CheckBox ID="chkAsociadoPepNuevoAsegJuridico" OnCheckedChanged="chkAsociadoPepNuevoAsegJuridico_CheckedChanged" runat="server" AutoPostBack="true  "
                                Text="Está asociado con una Persona Expuesta Políticamente (PEP)?" Font-Size="8pt" />
                        </div>

                        <%--BOTON GUARDAR NUEVO CLIENTE JURIDICO--%>
                        <div class="form-group col-md-12">
                            <asp:Button ID="btnGuardarNuevoAsegJuridico" runat="server" Text="Guardar Cliente" CssClass="btn btn-danger pull-right"
                                OnClick="btnGuardarNuevoAsegJuridico_Click" />
                            <asp:Button ID="btbNoEnviarActualizacionJuridico" runat="server" Text="Continuar" CssClass="btn btn-danger pull-right" OnClick="btbNoEnviarActualizacionJuridico_Click" />
                        </div>
                        <asp:Panel ID="pnlPepNuevoJuridico" runat="server" Visible="false">

                            <ul class="nav nav-tabs col-md-12 col-md-offset-1">
                                <li id="menuEsPepNuevoJuridico" class="active"><a data-toggle="tab" href="#divEsPepNuevoJuridico">Persona Expuesta Politicamente</a></li>
                                <li id="menuRelPepNuevoJuridico"><a data-toggle="tab" href="#divRelPepNuevoJuridico">Parentezco PEP</a></li>
                                <li id="menuAsociadoPepNuevoJuridico"><a data-toggle="tab" href="#divAsociadoPepNuevoJuridico">Asociado PEP</a></li>
                            </ul>



                            <div class="tab-content">
                                <div id="divEsPepNuevoJuridico" class="tab-pane fade in active">

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Solicitante</label>
                                        <asp:DropDownList ID="DropDownList23" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Representante Legal</label>
                                        <asp:DropDownList ID="DropDownList24" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Beneficiario</label>
                                        <asp:DropDownList ID="DropDownList25" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-7" style="border: none">
                                        <label>Otros firmantes y/o Tarjetahabientes adicionales</label>
                                        <asp:DropDownList ID="DropDownList26" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-5" style="border: none">
                                        <label>Fecha Vencimiento Estado PEP</label>
                                        <asp:TextBox ID="TextBox22" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-8" style="border: none">
                                        <label>Nombre Institución o Donde Trabaja</label>
                                        <asp:TextBox ID="TextBox23" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Condición</label>
                                        <asp:DropDownList ID="DropDownList27" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-8" style="border: none">
                                        <label>Puesto que desempeña</label>
                                        <asp:TextBox ID="TextBox24" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>País de la institución o Ente</label>
                                        <asp:DropDownList ID="DropDownList28" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                </div>

                                <div id="divRelPepNuevoJuridico" class="tab-pane fade">

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Solicitante</label>
                                        <asp:DropDownList ID="DropDownList29" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Representante Legal</label>
                                        <asp:DropDownList ID="DropDownList30" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Beneficiario</label>
                                        <asp:DropDownList ID="DropDownList31" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-7" style="border: none">
                                        <label>Otros firmantes y/o Tarjetahabientes adicionales</label>
                                        <asp:DropDownList ID="DropDownList32" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-5" style="border: none">
                                        <label>Condición</label>
                                        <asp:DropDownList ID="DropDownList33" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-3" style="border: none">
                                        <label>Parentesco</label>
                                        <asp:DropDownList ID="DropDownList34" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-6" style="border: none">
                                        <label>Especifique</label>
                                        <asp:TextBox ID="TextBox25" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-3" style="border: none">
                                        <label>Género</label>
                                        <asp:DropDownList ID="DropDownList35" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Primer Nombre</label>
                                        <asp:TextBox ID="TextBox26" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Segundo Nombre</label>
                                        <asp:TextBox ID="TextBox27" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Otros Nombres</label>
                                        <asp:TextBox ID="TextBox28" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Primer Apellido</label>
                                        <asp:TextBox ID="TextBox29" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Segundo Apellido</label>
                                        <asp:TextBox ID="TextBox30" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Apellido Casada</label>
                                        <asp:TextBox ID="TextBox31" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-12" style="border: none">
                                        <label>Nombre Institución o Donde Trabaja</label>
                                        <asp:TextBox ID="TextBox32" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-8" style="border: none">
                                        <label>Puesto que desempeña</label>
                                        <asp:TextBox ID="TextBox33" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>País de la institución o Ente</label>
                                        <asp:DropDownList ID="DropDownList36" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                </div>

                                <div id="divAsociadoPepNuevoJuridico" class="tab-pane fade">

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Solicitante</label>
                                        <asp:DropDownList ID="DropDownList37" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Representante Legal</label>
                                        <asp:DropDownList ID="DropDownList38" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Beneficiario</label>
                                        <asp:DropDownList ID="DropDownList39" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-7" style="border: none">
                                        <label>Otros firmantes y/o Tarjetahabientes adicionales</label>
                                        <asp:DropDownList ID="DropDownList40" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-5" style="border: none">
                                        <label>Condición</label>
                                        <asp:DropDownList ID="DropDownList41" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-3" style="border: none">
                                        <label>Motivo</label>
                                        <asp:DropDownList ID="DropDownList42" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-6" style="border: none">
                                        <label>Especifique</label>
                                        <asp:TextBox ID="TextBox34" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-3" style="border: none">
                                        <label>Género</label>
                                        <asp:DropDownList ID="DropDownList43" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Primer Nombre</label>
                                        <asp:TextBox ID="TextBox35" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Segundo Nombre</label>
                                        <asp:TextBox ID="TextBox36" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Otros Nombres</label>
                                        <asp:TextBox ID="TextBox37" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Primer Apellido</label>
                                        <asp:TextBox ID="TextBox38" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Segundo Apellido</label>
                                        <asp:TextBox ID="TextBox39" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>Apellido Casada</label>
                                        <asp:TextBox ID="TextBox40" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-12" style="border: none">
                                        <label>Nombre Institución o Donde Trabaja</label>
                                        <asp:TextBox ID="TextBox41" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-8" style="border: none">
                                        <label>Puesto que desempeña</label>
                                        <asp:TextBox ID="TextBox42" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group col-md-4" style="border: none">
                                        <label>País de la institución o Ente</label>
                                        <asp:DropDownList ID="DropDownList44" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>

                                </div>
                            </div>


                        </asp:Panel>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkEsPepNuevoAsegJuridico" EventName="CheckedChanged" />
                    </Triggers>

                </asp:UpdatePanel>

                <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                    <ProgressTemplate>
                        <%-- <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>--%>
                        <div class="overlay" />
                        <div class="overlayContent">
                            <h2>Realizando Busqueda...</h2>
                            <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <br />
                <br />



            </div>



        </asp:Panel>

    </asp:Panel>

    <br />
    <br />

    <div class="col-md-8 col-md-offset-2" style="text-align: center">
        <div class="form-group col-md-4 col-md-offset-4">
            <%--CANCELAR--%>
            <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click"><i class="glyphicon glyphicon glyphicon-arrow-left"></i> Regresar</asp:LinkButton>
        </div>
    </div>

    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8);
        specialKeys.push(9);//Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
            document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }
    </script>

    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>

</asp:Content>
