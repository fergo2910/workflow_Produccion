<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Cotizaciones_Almacenadas.aspx.cs" Inherits="CotizadorWebAutos.Productos_Web.Cotizaciones_Almacenadas" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../crystalreportviewers13/js/crviewer/crv.js"></script>

    <script type="text/javascript">
        function PedirFechaVigencia() {
            $("#btnShowPopup").click();
        }

        function abrirResultado() {
            $("#btnResultado").click();
        }

        function aceptacionEliminar() {
            $("#btnEliminacion").click();
        }

        function detallePlan() {
            $("#btnDetallePlan").click();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- MODAL PARA INGRESAR EL INICIO DE VIGENCIA DE LA POLIZA --%>
    <div class="modal fade" id="VigenciaPoliza" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header" style="background-color: #DF0000">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="text-align: center; color: white"><b>Fecha Inicio Vigencia Póliza</b></h4>
                </div>

                <div class="modal-body">
                    <asp:Label ID="lblIdCotizacionSeleccionada" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblIdPlanSeleccionado" runat="server" Visible="false"></asp:Label>

                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        TargetControlID="txtFechaInicioVigenciaPoliza"
                        Mask="99/99/9999"
                        MaskType="Date" />

                    <asp:Label ID="Label1" runat="server">Fecha</asp:Label>
                    <asp:TextBox ID="txtFechaInicioVigenciaPoliza" runat="server" CssClass="form-control input-sm" placeholder="DD/MM/YYYY"></asp:TextBox>
                </div>

                <div class="modal-footer">
                    <asp:LinkButton ID="btnEmitirDialog" runat="server" CssClass="btn btn-danger"
                        OnClick="btnEmitirDialog_Click" Font-Bold="True"><i class="glyphicon glyphicon-th-large"></i> Emitir
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <%-- MODAL PARA MOSTRAR RESULTADO DE ERROR DE EMISION--%>
    <div class="modal fade" id="modalERROR" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">

                <div class="modal-header" style="background-color: #DF0000">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="text-align: center; color: white"><b>Respuesta</b></h4>
                </div>

                <div class="modal-body">
                    <asp:Label ID="lblRespuesta" runat="server" Text="" Visible="false" CssClass="alert-default"></asp:Label>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                </div>

            </div>
        </div>
    </div>

    <%-- MODAL PARA APROBAR ELIMINACION DE COTIZACION--%>
    <div class="modal fade" id="modalEliminacion" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header" style="background-color: #DF0000">
                    <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                    <h4 class="modal-title" style="text-align: center; color: white"><b>Eliminar</b></h4>
                </div>

                <div class="modal-body">
                    <asp:Label ID="MensajeModalEliminacion" runat="server" CssClass="alert-default">Está seguro que desea eliminar la cotizacion No.: </asp:Label>
                    <asp:Label ID="lblIdCotizacionEliminar" runat="server" Text="" Visible="false"></asp:Label>
                </div>

                <div class="modal-footer">
                    <asp:LinkButton ID="btnSi" runat="server" CssClass="btn btn-danger"
                        OnClick="btnSi_Click" Font-Bold="True"> Si
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnNo" runat="server" CssClass="btn"
                        Font-Bold="True"> No
                    </asp:LinkButton>
                </div>

            </div>
        </div>
    </div>

    <%-- MODAL PARA MOSTRAR DETALLE DE COTIZACION--%>
    <div class="modal fade" id="modalDetallePlan" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header" style="background-color: #DF0000">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="text-align: center; color: white"><b>Emitir</b></h4>
                </div>

                <div class="modal-body">
                    <asp:Label ID="MensajeModalDetallePlan" runat="server" CssClass="alert-default">La póliza se emitirá con el siguiente plan de pago:</asp:Label>
                    <p></p><asp:Label ID="txtForma" runat="server" CssClass="label-danger" Text="Label"></asp:Label>
                    <p></p><asp:Label ID="txtNumero" runat="server" CssClass="label-danger" Text="Label"></asp:Label>
                    <asp:Label ID="lblIdCotizacionDetalle" runat="server" Text="" Visible="false"></asp:Label>
                </div>

                <div class="modal-footer">
                    <asp:LinkButton ID="Emitir" runat="server" CssClass="btn btn-danger"
                        OnClick="Emitir_Click" Font-Bold="True"> Emitir
                    </asp:LinkButton>
                    <asp:LinkButton ID="Cancelar" runat="server" CssClass="btn"
                        Font-Bold="True"> Cancelar
                    </asp:LinkButton>
                </div>

            </div>
        </div>
    </div>

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

    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container">

                <%-- FUNCIONALIDAD PARA SOLICITAR FECHA DE VIGENCIA DE LA POLIZA. --%>
                <button type="button" style="display: none;" id="btnShowPopup"
                    data-toggle="modal" data-target="#VigenciaPoliza">
                </button>

                <button type="button" class="close" style="display: none;" id="btnResultado"
                    data-toggle="modal" data-target="#modalERROR">
                </button>

                <button type="button" class="close" style="display: none;" id="btnEliminacion"
                    data-toggle="modal" data-target="#modalEliminacion">
                </button>

                <button type="button" class="close" style="display: none;" id="btnDetallePlan"
                    data-toggle="modal" data-target="#modalDetallePlan">
                </button>
                <%-- ******************************************************************************************************** --%>

                <div class="col-md-12" style="text-align: center">
                    <h3>
                        <asp:Label ID="lblNombreCotizacion" runat="server" Text="" Font-Bold="true"></asp:Label></h3>
                </div>

                <div class="row"></div>

                <div style="align-content: center; height: 400px; overflow: auto">
                    <asp:GridView ID="grvCotizacionesIntermediario" CssClass="panel-danger" DataKeyNames="id_cotizacion" runat="server" AutoGenerateColumns="False" AllowSorting="True" HorizontalAlign="Center" Font-Size="Small" OnRowDataBound="grvCotizacionesIntermediario_RowDataBound">
                        <Columns>

                            <asp:BoundField DataField="id_cotizacion" HeaderText="Cotización">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="nombre_solicitante" HeaderText="Cliente">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="fecha_cotizacion" HeaderText="Fecha Cotización">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:BoundField DataField="estado_coti" HeaderText="Estado Cotización">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>

                            <asp:TemplateField HeaderText="Cliente">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnINC" Text="Ingresar" Visible="false" OnCommand="ingresarCliente" runat="server" CommandArgument='<%# Eval("id_cotizacion") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Datos del vehículo">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnINS" Text="Ingresar" Visible="false" OnCommand="ingresarInspeccion" CommandArgument='<%# Eval("id_cotizacion") %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Autorización">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnAUT" Visible="false" Text="Ingresar" OnCommand="ingresarAutorizacion" CommandArgument='<%# Eval("id_cotizacion") %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Emitir Poliza">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEMI" Visible="false" Text="Emitir" OnCommand="ingresarEmision" CommandArgument='<%# Eval("id_cotizacion") %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Imprimir Cotización">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnIMP" Text="Imprimir" Visible="true" OnCommand="ingresarImpresion" CommandArgument='<%# Eval("id_cotizacion") %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Eliminar Cotización">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnELI" Text="Eliminar" Visible="true" OnCommand="ingresarEliminar" CommandArgument='<%# Eval("id_cotizacion") %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>

                        </Columns>
                        <HeaderStyle BackColor="#DF0000" ForeColor="White" />
                    </asp:GridView>
                </div>

                <div class="col-md-4 col-md-offset-4" style="text-align: center">
                    <asp:LinkButton ID="btnCotizacionNueva" runat="server" CssClass="btn btn-danger"
                        OnClick="btnCotizacionNueva_Click" Font-Bold="True"><i class="glyphicon glyphicon glyphicon-plus-sign"></i> Nueva Cotización
                    </asp:LinkButton>
                </div>

                <div class="col-md-4 col-md-offset-4" style="text-align: center">
                    <br />
                    <asp:LinkButton ID="btnRegresar" runat="server" CssClass="btn btn-danger"
                        OnClick="btnRegresar_Click" Font-Bold="True"><i class="glyphicon glyphicon glyphicon-arrow-left"></i> Regresar
                    </asp:LinkButton>
                </div>
            </div>
            <div class="col-md-12" style="align-content: center; overflow: auto; height: auto">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" />
            </div>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="grvCotizacionesIntermediario" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <%-- <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>--%>
            <div class="overlay" />
            <div class="overlayContent">
                <h2>Un momento porfavor</h2>
                <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>




</asp:Content>
