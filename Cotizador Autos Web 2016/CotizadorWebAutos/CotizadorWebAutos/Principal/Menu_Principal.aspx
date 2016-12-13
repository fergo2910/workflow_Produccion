<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Menu_Principal.aspx.cs" Inherits="CotizadorWebAutos.Principal.Menu_Principal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid " style="border: none">

        <br />
        <div class="col-md-3" style="border: none; background-color: lightgray">



            <%-- MENU OPCIONES COTIZADOR --%>
            <div class="col-md-12">
                <asp:Panel ID="panelCotizaciones" runat="server" Visible="true">

                    <legend style="text-align: center" runat="server" id="legenda"><span></span></legend>

                    <div class="panel-group" id="accordion3" role="tablist" aria-multiselectable="true">
                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="headingThree">
                                <h4 class="panel-title">
                                    <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion3" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">Cotizador
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseThree" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                <div class="panel-body">
                                    <ul class="nav nav-pills nav-stacked">
                                        <li><a runat="server" id="linkCotizacionesAlmacenadas" class="alert-danger" href="">Mis Cotizaciones</a></li>
                                        <%--<li><a runat="server" id="linkPagos" class="alert-danger" href="">Pagos en Línea</a></li>--%>
                                        <li><a runat="server" id="linkReimpresion" class="alert-danger" href="">Reimpresión de Pólizas</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="row"></div>

            <%-- MENU FACTURA ELECTRONICA --%>
            <div class="col-md-12">
                <asp:Panel ID="PanelConsultas" Visible="true" runat="server">
                    <div class="panel-group" id="accordionC" role="tablist">
                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="headingOneC">
                                <h4 class="panel-title">
                                    <a role="button" data-toggle="collapse" data-parent="#accordionC" href="#collapseOneC" aria-expanded="false" aria-controls="collapseOne">Consultas Electrónicas
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseOneC" class="panel-collapse collapse out" role="tabpanel" aria-labelledby="headingOne">
                                <div class="panel-body">
                                    <ul class="nav nav-pills nav-stacked">
                                        <li><a class="alert-danger" href="" id="linkFacturaE" runat="server">Factura Electrónica</a></li>
                                        <li><a runat="server" id="linkEstadoCuenta" class="alert-danger" href="" visible="true">Estado de Cuenta</a></li>
                                        <li><a runat="server" id="linkPrimasPagadas" class="alert-danger" href="" visible="true">Primas Pagadas</a></li>
                                        <li><a runat="server" id="linkPrimasPendientes" class="alert-danger" href="" visible="true">Primas Pendientes</a></li>
                                        <%--<li><a runat="server" id="A3" class="alert-danger" href="">Mantenimiento de usuarios</a></li>--%>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="row"></div>

            <%-- MENU SEGURIDAD --%>
            <div class="col-md-12">
                <asp:Panel ID="pnlMenuSeguridad" Visible="false" runat="server">
                    <div class="panel-group" id="accordion" role="tablist">
                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="headingOne">
                                <h4 class="panel-title">
                                    <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="false" aria-controls="collapseOne">Seguridad
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseOne" class="panel-collapse collapse out" role="tabpanel" aria-labelledby="headingOne">
                                <div class="panel-body">
                                    <ul class="nav nav-pills nav-stacked">
                                        <li>
                                            <asp:Panel ID="pnlMenuCliente" Visible="true" runat="server">
                                                <div class="nav nav-pills nav-stacked" id="accordionCliente" role="tablist">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading" role="tab" id="headingOne2">
                                                            <h4 class="panel-title">
                                                                <a role="button" data-toggle="collapse" data-parent="#accordionCliente" href="#collapseOne2" aria-expanded="false" aria-controls="collapseOne2">Creación de usuarios
                                                                </a>
                                                            </h4>
                                                        </div>
                                                        <div id="collapseOne2" class="panel-collapse collapse out" role="tabpanel" aria-labelledby="headingOne2">
                                                            <div class="panel-body">
                                                                <ul class="nav nav-pills nav-stacked">
                                                                    <li><a class="alert-danger" href="" id="linkCrearUsuario" runat="server">Ingresar Nuevo Usuario Cotizador</a></li>
                                                                    <%--<li><a runat="server" id="linkCrearUsuarioMedico" class="alert-danger" href="">Creación de usuario médico</a></li>--%>
                                                                </ul>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </li>
                                        <li><a runat="server" id="linkRoles" class="alert-danger" href="" visible="false">Roles</a></li>
                                        <li><a runat="server" id="linkMantUsuario" class="alert-danger" href="">Mantenimiento de usuarios</a></li>
                                        <li><a runat="server" id="linkConfPlan" class="alert-danger" href="">Asignacion Planes Intermediarios</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="row"></div>

        </div>

        <%-- PANEL PRINCIPAL DE PRODUCTOS PARA COTIZAR --%>
        <div class="col-md-9 form-horizontal" style="border: none; text-align: center">

            <ajaxToolkit:TabContainer ID="tabContenedorCategorias" runat="server">
            </ajaxToolkit:TabContainer>
        </div>

    </div>
</asp:Content>
