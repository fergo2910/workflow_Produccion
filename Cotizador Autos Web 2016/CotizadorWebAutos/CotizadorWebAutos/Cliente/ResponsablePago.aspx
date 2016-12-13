<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="ResponsablePago.aspx.cs" Inherits="CotizadorWebAutos.Cliente.ResponsablePago" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col-md-12" style="text-align: center">
        <h1>Datos del responsable de pago</h1>
    </div>

    <br />
    <br />

    <%--Busqueda Cliente--%>

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

    <br />
    <br />

    <%--Busqueda Cliente--%>
    <div class="col-md-12" style="text-align: center">
        <div class="form-inline">
            <label>NIT :</label>
            <div class="form-group">
                <asp:TextBox ID="txtNumIDCliente" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="110px"></asp:TextBox>
                -
                <asp:TextBox ID="txtDvIdCliente" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px" Style="text-transform: uppercase"></asp:TextBox>
                <asp:Button ID="btnBuscarNitCliente" runat="server" Text="Buscar" CssClass="btn-danger" OnClick="btnBuscarNitCliente_Click" />
            </div>
            <hr />
        </div>
    </div>

    <asp:Panel ID="pnlDatosClienteExistente" runat="server" Visible="false">

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
                    <label>Genero</label>
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

                <%--# IDENTIFICACION--%>
                <div class="form-group col-md-3">
                    <label>No. Identificación</label>
                    <asp:TextBox ID="txtNumeroIdentificacionIndividualExistente" runat="server" CssClass="form-control input-sm"></asp:TextBox>
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
                    <asp:DropDownList ID="cmbMuniEmisionIndividualExistente" runat="server" CssClass="form-control input-sm" AutoPostBack="true" Enabled="False"></asp:DropDownList>
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
                    <asp:CheckBox ID="chkActuaNombrePropioIndividualExistente" runat="server" Enabled="False" Visible="false" Checked="true" />
                </div>

                <div class="row"></div>
                <br />
                <asp:Label runat="server" Font-Bold="true"> Información Persona Expuesta Poltíticamente</asp:Label>
                <div class="form-group col-md-12 text-center" style="background-color: whitesmoke">
                    <%--ES PEP--%>
                    <div class="form-group col-md-6">
                        <br />
                        <asp:CheckBox ID="chkEsPepIndividualExistente" runat="server"
                            Text="¿Es Persona Expuesta Políticamente?" Font-Size="8" />
                    </div>

                    <%--REL CON PEP--%>
                    <div class="form-group col-md-6">
                        <br />
                        <asp:CheckBox ID="chkRelPepIndividualExistente" runat="server"
                            Text="¿Relación con Persona Expuesta Políticamente?" Font-Size="8" />
                    </div>

                    <%--ASOCIADO PEP--%>
                    <div class="form-group col-md-6">
                        <br />
                        <asp:CheckBox ID="chkAsociadoPepIndividualExistente" runat="server"
                            Text="¿Asociado con Persona Expuesta Políticamente?" Font-Size="8" />
                    </div>

                </div>

                <div class="row"></div>
                <br />

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
                    <asp:TextBox ID="txtCorreoIndividualExistente" runat="server" CssClass="form-control input-sm" Enabled="False" TextMode="Email"></asp:TextBox>
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
                    <asp:TextBox ID="txtNombrePersonaJuridicaJuridicoExistente" placeholder="de la persona jurídica" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                </div>

                <%--NIT PERSONA JURIDICA--%>
                <div class="form-group col-md-5">
                    <label>NIT :</label>
                    <div class="form-inline">

                        <div class="form-group">
                            <asp:TextBox ID="txtNumIdPersonaJuridicaJuridicoExistente" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="110px" Enabled="false"></asp:TextBox>
                            -
                                <asp:TextBox ID="txtDvIdPersonaJuridicaJuridicoExistente" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <%--NOMBRE REPRESENTANTE LEGAL--%>
                <%--<div class="form-group col-md-7">
                    <label>Nombre representante legal</label>
                    <asp:TextBox ID="txtNombreRepLegalJuridicoExistente" runat="server" placeholder="del representante legal" CssClass="form-control input-sm"></asp:TextBox>
                </div>--%>

                <%--NIT REPRESENTANTE LEGAL--%>
                <%--<div class="form-group col-md-5">
                    <label>NIT :</label>
                    <div class="form-inline">

                        <div class="form-group">
                            <asp:TextBox ID="txtNumIdRepLegalJuridicoExistente" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="110px"></asp:TextBox>
                            -
                                <asp:TextBox ID="txtDvIdRepLegalJuridicoExistente" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px"></asp:TextBox>
                        </div>
                    </div>
                </div>--%>

                <%--FECHA CONSTITUCION--%>
                <div class="form-group col-md-3">
                    <label>Fecha Constitución</label>
                    <asp:TextBox ID="txtFechaConstitucionJuridicoExistente" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                </div>

                <%--ORIGEN DE LA EMPRESA--%>
                <div class="form-group col-md-3">
                    <label>Origen Empresa</label>
                    <asp:DropDownList ID="cmbOrigenEmpresaJuridicoExistente" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:DropDownList>
                </div>

                <%--ACTIVIDAD ECONOMICA--%>
                <div class="form-group col-md-11">
                    <label>Actividad Económica</label>
                    <asp:TextBox ID="txtActividadEconomicaJuridicoExistente" CssClass="form-control input-sm" runat="server" Enabled="false"></asp:TextBox>
                </div>

                <%--ES PEP--%>
                <div class="form-group col-md-3 col-md-offset-2">
                    <asp:CheckBox ID="chkEsPepJuridicoExistente" runat="server"
                        Text="Es Persona Expuesta Politicamente (PEP)?" Font-Size="8pt" Enabled="false" />
                </div>

                <%--REL CON PEP--%>
                <div class="form-group col-md-3">
                    <asp:CheckBox ID="chkRelPepJuridicoExistente" runat="server"
                        Text="Tiene relación con una Persona Expuesta Políticamente (PEP)?" Font-Size="8pt" Enabled="false" />
                </div>

                <%--ASOCIADO PEP--%>
                <div class="form-group col-md-3">
                    <asp:CheckBox ID="chkAsociadoPepJuridicoExistente" runat="server"
                        Text="Está asociado con una Persona Expuesta Politicamente (PEP)?" Font-Size="8pt" Enabled="false" />
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
                    <asp:TextBox ID="txtCorreoJuridicoExistente" runat="server" CssClass="form-control input-sm" TextMode="Email"></asp:TextBox>
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

        <%--DATOS GENERALES DEL CLIENTE INDIVICUAL--%>

        <asp:Panel ID="pnlNuevoRespPagoIndividual" runat="server" Visible="false">

            <div class="col-md-8 col-md-offset-2" style="text-align: left">

                <%----------------DATOS GENERALES DEL Cliente-----------------------%>
                <asp:Panel runat="server" CssClass="" GroupingText="Información General" Style="border: none">
                    <%--PRIMER NOMBRE--%>
                    <div class="form-group col-md-6">
                        <label>Primer Nombre</label>
                        <asp:TextBox ID="txtPrimerNombreNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <%--SEGUNDO NOMBRE--%>
                    <div class="form-group col-md-6">
                        <label>Segundo Nombre</label>
                        <asp:TextBox ID="txtSegundoNombreNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <%--PRIMER APELLIDO--%>
                    <div class="form-group col-md-6">
                        <label>Primer Apellido</label>
                        <asp:TextBox ID="txtPrimerApellidoNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <%--SEGUNDO APELLIDO--%>
                    <div class="form-group col-md-6">
                        <label>Segundo Apellido</label>
                        <asp:TextBox ID="txtSegundoApellidoNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <%--FECHA NACIMIENTO--%>
                    <div class="form-group col-md-3">
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                            TargetControlID="txtFechaNacimientoNuevoResPagoIndividual"
                            Mask="99/99/9999"
                            MaskType="Date" />
                        <label>Fecha Nacimiento</label>
                        <asp:TextBox ID="txtFechaNacimientoNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <%--GENERO--%>
                    <div class="form-group col-md-3">
                        <label>Genero</label>
                        <asp:DropDownList ID="cmbGeneroNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>

                    <%--ESTADO CIVIL--%>
                    <div class="form-group col-md-3">
                        <label>Estado Civil</label>
                        <asp:DropDownList ID="cmbEstadoCivilNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>

                    <%--PROFESION--%>
                    <div class="form-group col-md-3">
                        <label>Profesión</label>
                        <asp:DropDownList ID="cmbProfesionNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>

                    <%--NACIONALIDAD--%>
                    <div class="form-group col-md-3">
                        <label>Nacionalidad</label>
                        <asp:DropDownList ID="cmbNacionalidadNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                    </div>

                    <%--NIT--%>
                    <div class="form-group col-md-3">
                        <label>NIT :</label>
                        <div class="form-inline">

                            <div class="form-group">
                                <asp:TextBox ID="txtNumIdNuevoResPagoIndividual" Enabled="false" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="110px"></asp:TextBox>
                                -
                                <asp:TextBox ID="txtDvIdNuevoResPagoIndividual" Enabled="false" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <%--CORREO ELECTRONICO--%>
                    <div class="form-group col-md-3">
                        <label>Correo Electrónico</label>
                        <asp:TextBox ID="txtCorreoNuevoResPagoIndividual" TextMode="Email" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <%--TELEFONO--%>
                    <div class="form-group col-md-3">
                        <label>Teléfono</label>
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:TextBox ID="txtTelefonoNuevoResPagoIndividual" runat="server" MaxLength="8" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:Button ID="btnAgregarTelefonoNuevoResPagoIndividual" runat="server" Text="+" CssClass="btn btn-success btn-sm " OnClick="btnAgregarTelefonoNuevoResPagoIndividual_Click" />
                            </div>
                        </div>
                    </div>

                    <%--NEGOCIO PROPIO--%>
                    <div class="form-group col-md-8">
                        <label>Nombre Negocio Propio</label>
                        <asp:TextBox ID="txtNegocioPropioNuevoResPagoIndividual" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                    </div>

                    <%--TELEFONOS--%>
                    <div class="form-group col-md-3">
                        <label>Teléfonos</label>
                        <asp:GridView ID="grvTelefonosNuevoResPagoIndividual" runat="server"></asp:GridView>
                    </div>
                </asp:Panel>

                <div class="row"></div>
                <%----------------IDENTIFICACION DEL CLIENTE-----------------------%>
                <asp:Panel runat="server" CssClass="" GroupingText="Identificación" Style="border: none">
                    <%--TIPO IDENTIFICACION--%>
                    <div class="form-group col-md-3">
                        <label>Tipo Identificación</label>
                        <asp:DropDownList ID="cmbTipoIdentificacionNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm">
                            <asp:ListItem Value="D">DPI</asp:ListItem>
                            <asp:ListItem Value="P">Pasaporte</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <%--# IDENTIFICACION--%>
                    <div class="form-group col-md-2">
                        <label>No. Identificación</label>
                        <asp:TextBox ID="txtNumeroIdentificacionNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%--PAIS EMISION--%>
                            <div class="form-group col-md-2">
                                <label>País Emisión</label>
                                <asp:DropDownList ID="cmbPaisEmisionNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm" AutoPostBack="true"
                                    OnSelectedIndexChanged="cmbPaisEmisionNuevoResPagoIndividual_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <%--DEPTO EMISION--%>
                            <div class="form-group col-md-2">
                                <label>Depto Emisión</label>
                                <asp:DropDownList ID="cmbDeptoEmisionNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm" AutoPostBack="true"
                                    OnSelectedIndexChanged="cmbDeptoEmisionNuevoResPagoIndividual_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <%--MUNI EMISION--%>
                            <div class="form-group col-md-2">
                                <label>Muni Emisión</label>
                                <asp:DropDownList ID="cmbMuniEmisionNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cmbPaisEmisionNuevoResPagoIndividual" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>


                </asp:Panel>

                <div class="row"></div>
                <%----------------DIRECCION DEL CLIENTE-----------------------%>
                <asp:Panel runat="server" CssClass="" GroupingText="Dirección" Style="border: none">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%--PAIS--%>
                            <div class="form-group col-md-3">
                                <label>País</label>
                                <asp:DropDownList ID="cmbPaisDireccionNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="cmbPaisDireccionNuevoResPagoIndividual_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <%--DEPARTAMENTO--%>
                            <div class="form-group col-md-3">
                                <label>Departamento</label>
                                <asp:DropDownList ID="cmbDeptoDireccionNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="cmbDeptoDireccionNuevoResPagoIndividual_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <%--MUNICIPIO--%>
                            <div class="form-group col-md-3">
                                <label>Municipio</label>
                                <asp:DropDownList ID="cmbMuniDireccionNuevoResPagoIndividual" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="cmbMuniDireccionNuevoResPagoIndividual_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <%--ZONA--%>
                            <div class="form-group col-md-3">
                                <label>Zona</label>
                                <asp:DropDownList ID="cmbZonaDireccionNuevoRespPagoIndividual" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </div>

                            <div class="row"></div>

                            <%--CALLE--%>
                            <div class="form-group col-md-2">
                                <label>Calle</label>
                                <asp:TextBox ID="txtCalleDireccionNuevoRespPagoIndividual" CssClass="form-control input-sm" runat="server" MaxLength="3"></asp:TextBox>
                            </div>

                            <%--AVENIDA--%>
                            <div class="form-group col-md-1">
                                <label>Avenida</label>
                                <asp:TextBox ID="txtAvenidaDireccionNuevoRespPagoIndividual" CssClass="form-control input-sm" runat="server" MaxLength="3"></asp:TextBox>
                            </div>

                            <%--NUMERO CASA/APARTAMENTO--%>
                            <div class="form-group col-md-3">
                                <label>No. Casa/Apartamento</label>
                                <asp:TextBox ID="txtNumCasaDireccionNuevoRespPagoIndividual" CssClass="form-control input-sm" runat="server" MaxLength="10"></asp:TextBox>
                            </div>

                            <%--COLONIA--%>
                            <div class="form-group col-md-3">
                                <label>Colonia</label>
                                <asp:TextBox ID="txtColoniaDireccionNuevoRespPagoIndividual" CssClass="form-control input-sm" runat="server" MaxLength="50"></asp:TextBox>
                            </div>

                            <%--EDIFICIO O COMPLEMENTO--%>
                            <div class="form-group col-md-3">
                                <label>Edificio ó Complemento</label>
                                <asp:TextBox ID="txtEdificioDireccionNuevoRespPagoIndividual" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </div>

                            <%--LOTE--%>
                            <div class="form-group col-md-3">
                                <label>Lote</label>
                                <asp:TextBox ID="txtLoteDireccionNuevoRespPagoIndividual" CssClass="form-control input-sm" runat="server" MaxLength="3" TextMode="Number"></asp:TextBox>
                            </div>

                            <%--Manzana--%>
                            <div class="form-group col-md-3">
                                <label>Manzana</label>
                                <asp:TextBox ID="txtManzanaDireccionNuevoRespPagoIndividual" CssClass="form-control input-sm" runat="server" MaxLength="5"></asp:TextBox>
                            </div>

                            <%--Sector--%>
                            <div class="form-group col-md-3">
                                <label>Sector</label>
                                <asp:TextBox ID="txtSectorDireccionNuevoRespPagoIndividual" CssClass="form-control input-sm" runat="server" MaxLength="40"></asp:TextBox>
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cmbPaisDireccionNuevoResPagoIndividual" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </asp:Panel>

                <div class="row"></div>

                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <%--ES PEP--%>
                        <div class="form-group col-md-3 col-md-offset-2">
                            <asp:CheckBox ID="chkEsPepNuevoResPagoIndividual" runat="server" AutoPostBack="true"
                                Text="Es Persona Expuesta Politicamente (PEP)?" Font-Size="8pt" OnCheckedChanged="chkEsPepNuevoResPagoIndividual_CheckedChanged" />
                        </div>

                        <%--REL CON PEP--%>
                        <div class="form-group col-md-3">
                            <asp:CheckBox ID="chkRelPepNuevoResPagoIndividual" runat="server" AutoPostBack="true"
                                Text="Tiene relación con una Persona Expuesta Politicamente (PEP)?" Font-Size="8pt" OnCheckedChanged="chkRelPepNuevoResPagoIndividual_CheckedChanged" />
                        </div>

                        <%--ASOCIADO PEP--%>
                        <div class="form-group col-md-3">
                            <asp:CheckBox ID="chkAsociadoPepNuevoResPagoIndividual" AutoPostBack="true" runat="server"
                                Text="Está asociado con una Persona Expuesta Politicamente (PEP)?" Font-Size="8pt" OnCheckedChanged="chkAsociadoPepNuevoResPagoIndividual_CheckedChanged" />
                        </div>

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

                                    <%--<div class="form-group col-md-4" style="border: none">
                                        <label>Representante Legal</label>
                                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>--%>

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

                                    <%--<div class="form-group col-md-4" style="border: none">
                                        <label>Representante Legal</label>
                                        <asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>--%>

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

                                    <%--<div class="form-group col-md-4" style="border: none">
                                        <label>Representante Legal</label>
                                        <asp:DropDownList ID="DropDownList16" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>--%>

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
                        <asp:AsyncPostBackTrigger ControlID="chkEsPepNuevoResPagoIndividual" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>

                <%--ACTUA NOMBRE PROPIO--%>
                <div class="form-group col-md-4">
                    <asp:Label runat="server" Visible="false">Actua en nombre propio?</asp:Label>
                    <asp:CheckBox ID="chkActuaNombrePropioNuevoResPagoIndividual" runat="server" Checked="true" Visible="false" />
                </div>

                <%--BOTON GUARDAR NUEVO CLIENTE INDIVIDUAL--%>
                <div class="form-group col-md-12">
                    <asp:Button ID="btnGuardarNuevoResPagoIndividual" runat="server" Text="Guardar Cliente" CssClass="btn btn-danger pull-right"
                        OnClick="btnGuardarNuevoResPagoIndividual_Click" />
                </div>
            </div>

        </asp:Panel>

        <asp:Panel ID="pnlNuevoRespPagoJuridico" runat="server" Visible="false">

            <div class="col-md-8 col-md-offset-2" style="text-align: left">

                <%--DATOS GENERALES DEL CLIENTE JURIDICO--%>

                <%--NOMBRE PERSONA JURIDICA--%>
                <div class="form-group col-md-7">
                    <label>Nombre</label>
                    <asp:TextBox ID="txtNombreNuevoRespPagoJuridico" placeholder="Nombre de la persona jurídica" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>

                <%--NIT--%>
                <div class="form-group col-md-5">
                    <label>NIT :</label>
                    <div class="form-inline">

                        <div class="form-group">
                            <asp:TextBox ID="txtNumIdNuevoRespPagoJuridico" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="110px"></asp:TextBox>
                            -
                                <asp:TextBox ID="txtDvIdNuevoRespPagoJuridico" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <%--PRIMER NOMBRE--%>
                <%--<div class="form-group col-md-6">
                    <label>Primer Nombre</label>
                    <asp:TextBox ID="txtPrimerNombreNuevoRespPagoJuridico" runat="server" placeholder="del representante legal" CssClass="form-control input-sm"></asp:TextBox>
                </div>--%>

                <%--SEGUNDO NOMBRE--%>
                <%--<div class="form-group col-md-6">
                    <label>Segundo Nombre</label>
                    <asp:TextBox ID="txtSegundoNombreNuevoRespPagoJuridico" runat="server" placeholder="del representante legal" CssClass="form-control input-sm"></asp:TextBox>
                </div>--%>

                <%--PRIMER APELLIDO--%>
                <%--<div class="form-group col-md-6">
                    <label>Primer Apellido</label>
                    <asp:TextBox ID="txtPrimerApellidoNuevoRespPagoJuridico" runat="server" placeholder="del representante legal" CssClass="form-control input-sm"></asp:TextBox>
                </div>--%>

                <%--SEGUNDO APELLIDO--%>
                <%--<div class="form-group col-md-6">
                    <label>Segundo Apellido</label>
                    <asp:TextBox ID="txtSegundoApellidoNuevoRespPagoJuridico" runat="server" placeholder="del representante legal" CssClass="form-control input-sm"></asp:TextBox>
                </div>--%>

                <%--TIPO IDENTIFICACION--%>
                <div class="form-group col-md-3">
                    <label>Tipo Identificación</label>
                    <asp:DropDownList ID="cmbTipoIdentificacionNuevoRespPagoJuridico" runat="server" CssClass="form-control input-sm">
                        <asp:ListItem Value="D">DPI</asp:ListItem>
                        <asp:ListItem Value="P">Pasaporte</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <%--# IDENTIFICACION--%>
                <%--<div class="form-group col-md-3">
                    <label>No. Identificación</label>
                    <asp:TextBox ID="txtNumeroIdentificacionNuevoRespPagoJuridico" placeholder="del representante legal" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>--%>

                <%--FECHA CONSTITUCION--%>
                <div class="form-group col-md-3">
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                        TargetControlID="txtFechaConstitucionNuevoRespPagoJuridico"
                        Mask="99/99/9999"
                        MaskType="Date" />
                    <label>Fecha Constitución</label>
                    <asp:TextBox ID="txtFechaConstitucionNuevoRespPagoJuridico" runat="server" placeholder="de la empresa" CssClass="form-control input-sm"></asp:TextBox>
                </div>

                <%--ORIGEN DE LA EMPRESA--%>
                <div class="form-group col-md-3">
                    <label>Origen Empresa</label>
                    <asp:DropDownList ID="cmbOrigenEmpresaNuevoRespPagoJuridico" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                </div>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <%--PAIS EMISION--%>
                        <div class="form-group col-md-3">
                            <label>Pais Emisión</label>
                            <asp:DropDownList ID="cmbPaisEmisionNuevoRespPagoJuridico" runat="server" CssClass="form-control input-sm" AutoPostBack="true"
                                OnSelectedIndexChanged="cmbPaisEmisionNuevoResPagoJuridico_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <%--DEPTO EMISION--%>
                        <div class="form-group col-md-3">
                            <label>Depto Emisión</label>
                            <asp:DropDownList ID="cmbDeptoEmisionNuevoRespPagoJuridico" runat="server" CssClass="form-control input-sm" AutoPostBack="true"
                                OnSelectedIndexChanged="cmbDeptoEmisionNuevoResPagoJuridico_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <%--MUNI EMISION--%>
                        <div class="form-group col-md-3">
                            <label>Muni Emisión</label>
                            <asp:DropDownList ID="cmbMuniEmisionNuevoRespPagoJuridico" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmbPaisEmisionNuevoRespPagoJuridico" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>


                <%--ACTIVIDAD ECONOMICA--%>
                <div class="form-group col-md-12">
                    <label>Actividad Económica</label>
                    <asp:TextBox ID="txtActividadEconomicaNuevoRespPagoJuridico" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                </div>

                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <%--PAIS--%>
                        <div class="form-group col-md-3">
                            <label>País</label>
                            <asp:DropDownList ID="cmbPaisDireccionNuevoRespPagoJuridico" runat="server" CssClass="form-control input-sm"
                                OnSelectedIndexChanged="cmbPaisDireccionNuevoResPagoJuridico_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <%--DEPARTAMENTO--%>
                        <div class="form-group col-md-3">
                            <label>Departamento</label>
                            <asp:DropDownList ID="cmbDeptoDireccionNuevoRespPagoJuridico" runat="server" CssClass="form-control input-sm"
                                OnSelectedIndexChanged="cmbDeptoDireccionNuevoResPagoJuridico_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <%--MUNICIPIO--%>
                        <div class="form-group col-md-3">
                            <label>Municipio</label>
                            <asp:DropDownList ID="cmbMuniDireccionNuevoRespPagoJuridico" runat="server" CssClass="form-control input-sm"
                                OnSelectedIndexChanged="cmbMuniDireccionNuevoRespPagoJuridico_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <%--ZONA--%>
                        <div class="form-group col-md-3">
                            <label>Zona</label>
                            <asp:DropDownList ID="cmbZonaDireccionNuevoRespPagoJuridico" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                        </div>

                        <%--CALLE--%>
                        <div class="form-group col-md-2">
                            <label>Calle</label>
                            <asp:TextBox ID="txtCalleDireccionNuevoRespPagoJuridico" CssClass="form-control input-sm" runat="server" MaxLength="3"></asp:TextBox>
                        </div>

                        <%--AVENIDA--%>
                        <div class="form-group col-md-2">
                            <label>Avenida</label>
                            <asp:TextBox ID="txtAvenidaDireccionNuevoRespPagoJuridico" CssClass="form-control input-sm" runat="server" MaxLength="3"></asp:TextBox>
                        </div>

                        <%--NUMERO CASA/APARTAMENTO--%>
                        <div class="form-group col-md-2">
                            <label>No. Casa/Apartamento</label>
                            <asp:TextBox ID="txtNumCasaDireccionNuevoRespPagoJuridico" CssClass="form-control input-sm" runat="server" MaxLength="10"></asp:TextBox>
                        </div>

                        <%--COLONIA--%>
                        <div class="form-group col-md-3">
                            <label>Colonia</label>
                            <asp:TextBox ID="txtColoniaDireccionNuevoRespPagoJuridico" CssClass="form-control input-sm" runat="server" MaxLength="50"></asp:TextBox>
                        </div>

                        <%--EDIFICIO O COMPLEMENTO--%>
                        <div class="form-group col-md-3">
                            <label>Edificio ó Complemento</label>
                            <asp:TextBox ID="txtEdificioDireccionNuevoRespPagoJuridico" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </div>

                        <%--LOTE--%>
                        <div class="form-group col-md-3">
                            <label>Lote</label>
                            <asp:TextBox ID="txtLoteDireccionNuevoRespPagoJuridico" CssClass="form-control input-sm" runat="server" MaxLength="3" TextMode="Number"></asp:TextBox>
                        </div>

                        <%--Manzana--%>
                        <div class="form-group col-md-3">
                            <label>Manzana</label>
                            <asp:TextBox ID="txtManzanaDireccionNuevoRespPagoJuridico" CssClass="form-control input-sm" runat="server" MaxLength="5"></asp:TextBox>
                        </div>

                        <%--Sector--%>
                        <div class="form-group col-md-3">
                            <label>Sector</label>
                            <asp:TextBox ID="txtSectorDireccionNuevoRespPagoJuridico" CssClass="form-control input-sm" runat="server" MaxLength="40"></asp:TextBox>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cmbPaisDireccionNuevoRespPagoJuridico" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>


                <%--TELEFONO--%>
                <div class="form-group col-md-5 col-md-offset-1">
                    <label>Teléfono</label>
                    <div class="form-inline">
                        <div class="form-group">
                            <asp:TextBox ID="txtTelefonoNuevoRespPagoJuridico" runat="server" MaxLength="8" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:Button ID="btnAgregarTelefonoNuevoRespPagoJuridico" runat="server" Text="+" CssClass="btn btn-success btn-sm " OnClick="btnAgregarTelefonoNuevoRespPagoJuridico_Click" />
                        </div>
                    </div>
                </div>

                <%--CORREO ELECTRONICO--%>
                <div class="form-group col-md-5">
                    <label>Correo Electrónico</label>
                    <asp:TextBox ID="txtCorreoNuevoRespPagoJuridico" TextMode="Email" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>

                <%--TELEFONOS--%>
                <div class="form-group col-md-5 col-md-offset-1">
                    <label>Teléfonos</label>
                    <asp:GridView ID="grvTelefonosNuevoResPagoJuridico" runat="server"></asp:GridView>
                </div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <%--ES PEP--%>
                        <div class="form-group col-md-3 col-md-offset-2">
                            <asp:CheckBox ID="chkEsPepNuevoRespPagoJuridico" AutoPostBack="true" runat="server"
                                Text="Es PEP?" OnCheckedChanged="chkEsPepNuevoRespPagoJuridico_CheckedChanged" />
                        </div>

                        <%--REL CON PEP--%>
                        <div class="form-group col-md-3">
                            <asp:CheckBox ID="chkRelPepNuevoRespPagoJuridico" AutoPostBack="true" runat="server"
                                Text="Rel con PEP?" OnCheckedChanged="chkRelPepNuevoRespPagoJuridico_CheckedChanged" />
                        </div>

                        <%--ASOCIADO PEP--%>
                        <div class="form-group col-md-3">
                            <asp:CheckBox ID="chkAsociadoPepNuevoRespPagoJuridico" AutoPostBack="true" runat="server"
                                Text="Asociado PEP?" OnCheckedChanged="chkAsociadoPepNuevoRespPagoJuridico_CheckedChanged" />
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

                                    <%--<div class="form-group col-md-4" style="border: none">
                                        <label>Representante Legal</label>
                                        <asp:DropDownList ID="DropDownList24" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>--%>

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

                                    <%--<div class="form-group col-md-4" style="border: none">
                                        <label>Representante Legal</label>
                                        <asp:DropDownList ID="DropDownList30" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>--%>

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

                                    <%--<div class="form-group col-md-4" style="border: none">
                                        <label>Representante Legal</label>
                                        <asp:DropDownList ID="DropDownList38" runat="server" CssClass="form-control input-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>--%>

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
                        <asp:AsyncPostBackTrigger ControlID="chkEsPepNuevoResPagoIndividual" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>

                <%--BOTON GUARDAR NUEVO CLIENTE JURIDICO--%>
                <div class="form-group col-md-12">
                    <asp:Button ID="btnGuardarNuevoRespPagoJuridico" runat="server" Text="Guardar Cliente" CssClass="btn btn-danger pull-right"
                        OnClick="btnGuardarNuevoRespPagoJuridico_Click" />
                </div>

            </div>

        </asp:Panel>

    </asp:Panel>

    <div class="col-md-8 col-md-offset-2" style="text-align: center">
        <div class="form-group col-md-4 col-md-offset-4">
            <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger btn-block"
                OnClick="btnCancelar_Click"><i class="glyphicon glyphicon glyphicon-remove"></i>Cancelar</asp:LinkButton>
        </div>
    </div>
</asp:Content>

