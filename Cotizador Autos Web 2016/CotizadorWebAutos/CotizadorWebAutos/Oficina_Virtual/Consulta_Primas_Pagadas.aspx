<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Consulta_Primas_Pagadas.aspx.cs" Inherits="CotizadorWebAutos.Oficina_Virtual.Consulta_Primas_Pagadas" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <style type="text/css">
        .overlay {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #F8F8FF;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }

        .overlayContent {
            z-index: 99;
            margin: 250px auto;
            width: 80px;
            height: 80px;
        }

            .overlayContent h2 {
                font-size: 18px;
                font-weight: bold;
                color: #000;
            }

            .overlayContent img {
                width: 80px;
                height: 80px;
            }
    </style>

    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading" style="text-align: center">
                <h2>Consulta de primas pagadas</h2>
            </div>
            <div class="panel-body" style="overflow-y: auto; overflow-x: hidden; overflow-y: inherit">
                <div class="col-md-12">
                    <asp:Panel ID="PanelConsulta" runat="server" CssClass="" GroupingText="Parámetros de Consulta:" Style="border: none">
                        <div class="col-md-2" style="align-content: center">
                            <img id="img1" src="../Recursos/imagenes/Uno.jpg" class="img-rounded" width="95" height="95" runat="server" />
                        </div>
                        • Debe seleccionar la fecha de inicio y fin de su búsqueda. 
                        <br />
                        • Su búsqueda no debe sobrepasar un rango de 2 meses entre la fecha inicial y la fecha final
                        <br />
                        <hr />
                        <div class="form-group col-md-4" style="border: none">
                            <div class="form-inline" style="align-content: center">
                                <h3>Fecha inicial</h3>
                                <br />
                                <asp:Calendar ID="fecIni" runat="server"></asp:Calendar>
                                <br />
                                <br />
                            </div>
                        </div>
                        <div class="form-group col-md-4" style="border: none">
                            <div class="form-inline" style="align-content: center">
                                <h3>Fecha final</h3>
                                <br />
                                <asp:Calendar ID="fecFin" runat="server"></asp:Calendar>
                                <br />
                                <br />
                            </div>
                        </div>
                        <div class="row"></div>
                    </asp:Panel>
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanelBotones" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class=" col-md-4">
                                    <br />
                                    <asp:Button ID="btnBuscar" CssClass="btn btn-danger btn-block" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                </div>
                                <div class=" col-md-4">
                                    <br />
                                    <asp:Button ID="btnRegresar" CssClass="btn btn-danger btn-block" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
                                </div>
                                <div class=" col-md-4">
                                    <br />
                                    <asp:Button ID="btnLimpiar" CssClass="btn btn-danger btn-block" runat="server" Text="Nueva Busqueda" OnClick="btnLimpiar_Click" />
                                </div>
                                <div class=" col-md-12">
                                    <br />
                                    <h3>
                                        <asp:Label ID="lblErrorGeneral" runat="server" Visible="False" CssClass="alert" Font-Bold="True"></asp:Label></h3>
                                </div>
                                <div class="form-group col-md-12">
                                    <br />
                                    <asp:Panel ID="pnlResultadoPrimas" Visible="false" runat="server" GroupingText="Resultado de Búsqueda" Style="border: none">
                                        <div class="col-md-2" style="align-content: center">
                                            <img id="img2" src="../Recursos/imagenes/dos.jpg" class="img-rounded" width="95" height="95" runat="server" />
                                        </div>
                                        <div class="form-group col-md-4 col-lg-offset-2" style="text-align: center">
                                            <h3>Seleccione su tipo de descarga</h3>
                                            Archivo Excel o Archivo PDF
                                            <div class="form-inline">
                                                <asp:ImageButton ID="btnExportarPrimasPagadasExcel" Visible="true" ToolTip="Exportar datos a Excel" runat="server" OnClick="btnExportarPrimasPagadasExcel_Click" Height="50px" Width="50px" ImageUrl="~/Recursos/imagenes/excel-xls-icon.png" Enabled="true" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:ImageButton ID="btnExportarPrimasPagadas" Visible="true" ToolTip="Exportar datos a PDF" runat="server" OnClick="btnExportarPrimasPagadas_Click" Height="50px" ImageUrl="~/Recursos/imagenes/PDF-download.png" Width="50px" />
                                            </div>
                                            <h4><asp:Label ID="lblError" runat="server" Text="" Visible="false"></asp:Label></h4>
                                        </div>
                                        <div class="row"></div>
                                        <asp:Label ID="lblInfoPrimas" runat="server" Text=" "></asp:Label>
                                        <div style="align-content: center; overflow: auto">
                                            <asp:GridView ID="grvResultadoPrimas" CssClass="panel-danger" runat="server"
                                                AllowSorting="True" HorizontalAlign="Center" Font-Size="Small" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="poliza" HeaderText="Póliza">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nombre" HeaderText="Asegurado">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="giros" HeaderText="Pago">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="idefact" HeaderText="Requerimiento">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="codfact" HeaderText="Serie">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="numfact" HeaderText="Factura">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecvencfact" DataFormatString="{0:d/MM/yyyy}" HeaderText="Fecha de Venc">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fecstsreling" DataFormatString="{0:d/MM/yyyy}" HeaderText="Fecha Pago">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="codmoneda" HeaderText="Moneda">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="mtofactlocal" HeaderText="Monto">
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle BackColor="#DF0000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" />
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </div>
                                <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="UpdatePanelBotones">
                                    <ProgressTemplate>
                                        <div class="overlay" />
                                        <div class="overlayContent">
                                            <h2>Realizando Busqueda...</h2>
                                            <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExportarPrimasPagadas" />
                                <asp:PostBackTrigger ControlID="grvResultadoPrimas" />
                                <asp:PostBackTrigger ControlID="btnExportarPrimasPagadasExcel" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
