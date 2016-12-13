<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Menu_Principal.aspx.cs" Inherits="CotizadorWebAutos.Principal.Menu_Principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid " style="border: none">

        <div class="col-md-3" style="border: none">

            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />

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
                <asp:Panel ID="PanelFactura" Visible="true" runat="server">
                    <div class="panel-group" id="accordionF" role="tablist">
                        <div class="panel panel-default">
                            <div class="panel-heading" role="tab" id="headingOneF">
                                <h4 class="panel-title">
                                    <a role="button" data-toggle="collapse" data-parent="#accordionF" href="#collapseOneF" aria-expanded="false" aria-controls="collapseOne">Documentos Electrónicos
                                    </a>
                                </h4>
                            </div>
                            <div id="collapseOneF" class="panel-collapse collapse out" role="tabpanel" aria-labelledby="headingOne">
                                <div class="panel-body">
                                    <ul class="nav nav-pills nav-stacked">
                                        <li><a runat="server" class="alert-danger" id="linkFacturaE" href="">Factura Electrónica</a></li>
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
        <div class="col-md-8 col-md-offset-1 " style="border: none">
            <br />
            <br />
            <br />
            <br />
            <asp:Panel runat="server" ID="pnlProductosPrincipal" Visible="true" GroupingText="Seleccione un producto para cotizar">
                <div id="divPrincipal" class="col-md-12" style="text-align: center" runat="server">

                    <%--<h3>
                        <asp:Label ID="Label1" runat="server" Text="Seleccione el producto a cotizar" Font-Bold="true"></asp:Label>
                    </h3>--%>
                </div>
            </asp:Panel>
        </div>       

    </div>
</asp:Content>
