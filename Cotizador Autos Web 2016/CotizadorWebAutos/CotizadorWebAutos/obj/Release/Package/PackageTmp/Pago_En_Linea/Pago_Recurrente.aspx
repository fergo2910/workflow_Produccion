<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Pago_Recurrente.aspx.cs" Inherits="CotizadorWebAutos.Pago_En_Linea.Pago_Recurrente" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="container">

        <div class="panel panel-default">

            <div class="panel-heading" style="text-align: center">
                <h4>Busqueda de pólizas</h4>
            </div>

            <div class="panel-body" style="overflow-y: auto">

                <div class="col-md-9 col-md-offset-2">

                    <%-- DATOS DEL CLIENTE --%>

                    <asp:Panel runat="server" CssClass="" GroupingText="Cliente" Style="border: none">

                        <%-- NIT --%>
                        <div class="form-inline col-md-3" style="border: none">
                            <div class="form-inline">
                                <asp:Label runat="server" Text="NIT:"></asp:Label>
                                <div class="form-group">
                                    <asp:TextBox ID="txtNumIDCliente" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="90px" onkeypress="return IsNumeric(event);"></asp:TextBox>
                                    -
                                <asp:TextBox ID="txtDvIdCliente" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px" Style="text-transform: uppercase"></asp:TextBox>

                                </div>
                            </div>
                        </div>

                        <%-- NOMBRES --%>
                        <div class="form-group col-md-4">
                            <asp:Label runat="server" Text="Nombres:"></asp:Label>
                            <asp:TextBox runat="server" ID="txtNombres" CssClass="form-control input-sm"></asp:TextBox>
                        </div>

                        <%-- APELLIDOS --%>
                        <div class="form-group col-md-4">
                            <asp:Label runat="server" Text="Apellidos:"></asp:Label>
                            <asp:TextBox runat="server" ID="txtApellidos" CssClass="form-control input-sm"></asp:TextBox>
                        </div>

                        <div class="row"></div>

                    </asp:Panel>

                    <%-- DATOS DE LA POLIZA --%>

                    <asp:Panel runat="server" GroupingText="Póliza" Style="border: none">

                        <%-- CODPOL --%>
                        <div class="form-group col-md-2">
                            <asp:Label runat="server" Text="CODPOL:"></asp:Label>
                            <asp:TextBox runat="server" ID="txtCodPol" CssClass="form-control input-sm"></asp:TextBox>
                        </div>

                        <%-- NUMPOL --%>
                        <div class="form-group col-md-2">
                            <asp:Label runat="server" Text="NUMPOL:"></asp:Label>
                            <asp:TextBox runat="server" ID="txtNumPol" CssClass="form-control input-sm"></asp:TextBox>
                        </div>

                    </asp:Panel>

                </div>

                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <%-- BUSCAR LA POLIZA --%>
                            <div class="col-md-4 col-md-offset-4 ">
                                <asp:Button ID="btnBuscarPolizas" OnClick="btnBuscarPolizas_Click" CssClass="btn btn-danger btn-block" runat="server" Text="Buscar" />
                            </div>

                            <br />

                            <%-- RESULTADO POLIZAS --%>
                            <div class="form-group col-md-10 col-md-offset-1" style="height: 200px; overflow: auto">
                                <br />
                                <asp:Panel ID="pnlResultadoBusqueda" Visible="false" runat="server" GroupingText="Resultado Búsqueda" Style="border: none">
                                    <asp:GridView ID="grvPolizas" DataKeyNames="IDEPOL" runat="server" OnRowDataBound="grvPolizas_RowDataBound" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDetallePoliza" Visible="true" Text="Detalle" OnCommand="detallePoliza" CommandArgument='<%# Eval("IDEPOL") %>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="IDEPOL" HeaderText="IDEPOL" />
                                            <asp:BoundField DataField="CODPOL" HeaderText="CODPOL" />
                                            <asp:BoundField DataField="NUMPOL" HeaderText="NUMPOL" />
                                            <asp:BoundField DataField="STSPOL" HeaderText="Estado Póliza" />
                                            <asp:BoundField DataField="FECINIVIG" HeaderText="Inicio Vigencia" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="FECFINVIG" HeaderText="Fin Vigencia" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Contratante" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>

                            <%-- RESULTADO OPERACIONES --%>
                            <div style="height: 150px; overflow: auto" class="form-group col-md-10 col-md-offset-1">
                                <asp:Panel ID="pnlOperaciones" Visible="false" runat="server" GroupingText="Operaciones Póliza" Style="border: none">
                                    <asp:GridView ID="grvOperaciones" runat="server" OnRowDataBound="grvOperaciones_RowDataBound" AutoGenerateColumns="False" OnSelectedIndexChanged="grvOperaciones_SelectedIndexChanged">

                                    </asp:GridView>

                                </asp:Panel>
                            </div>

                            <%-- RESULTADO REQUERIMIENTOS --%>
                            <div class="form-group col-md-10 col-md-offset-1" style="height: 200px; overflow: auto">
                                <br />
                                <asp:Panel ID="pnlRequerimientos" Visible="false" runat="server" GroupingText="Requerimientos" Style="border: none">

                                    <asp:GridView ID="grvRequerimientos" runat="server" OnRowDataBound="grvRequerimientos_RowDataBound" AutoGenerateColumns="False" OnSelectedIndexChanged="grvRequerimientos_SelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField DataField="Requerimiento" HeaderText="Requerimiento" />
                                            <asp:BoundField DataField="CODFACT" HeaderText="CODFACT" />
                                            <asp:BoundField DataField="NUMFACT" HeaderText="NUMFACT" />
                                            <asp:BoundField DataField="MONEDA" HeaderText="Moneda" />
                                            <asp:BoundField DataField="MONTO" HeaderText="Monto" />
                                            <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
                                            <asp:BoundField DataField="FECHA_COBRO" HeaderText="Fecha Cobro" />
                                            <asp:BoundField DataField="RespPago" HeaderText="Resp Pago" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>

                            <%-- TOTAL A COBRAR--%>
                            <div class="col-md-4 col-md-offset-8">
                                <div class="form-inline">
                                    <asp:Label ID="lblTotal" runat="server" Text="Total a Pagar:" Visible="false"></asp:Label>
                                    <asp:Label ID="lblTotalRequerimientos" runat="server" Text=""></asp:Label>
                                </div>

                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnBuscarPolizas" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

            </div>

            <div class="panel-footer">

                <asp:UpdatePanel ID="upnlFooter" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <h4 style="text-align: center">Datos del cobro</h4>

                        <%-- TIPO DE CUENTA --%>
                        <div class="form-group col-md-4">
                            <asp:Label ID="lblTipoCuenta" runat="server" Text="Tipo de pago"></asp:Label>
                            <asp:DropDownList ID="ddlTipoCuenta" runat="server" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>

                        <%-- BANCOS DE TARJETA --%>
                        <div class="form-group col-md-4">
                            <asp:Label ID="lblBancoTarjeta" runat="server" Text="Entidad financiera"></asp:Label>
                            <asp:DropDownList ID="ddlBancoTarjeta" runat="server" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>

                        <%-- TIPO DE TARJETA --%>
                        <div class="form-group col-md-4">
                            <asp:Label ID="lblTipoTarjeta" runat="server" Text="Tipo tarjeta"></asp:Label>
                            <asp:DropDownList ID="ddlTipoTarjeta" runat="server" CssClass="form-control dropdown"></asp:DropDownList>
                        </div>

                        <%-- NUMERO DE TARJETA --%>
                        <div class="form-group col-md-4">
                            <asp:Label ID="lblNumeroTarjeta" runat="server" Text="Número de tarjeta"></asp:Label>
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:TextBox onkeypress="return IsNumeric(event);" ID="txtNumeroTarjeta" runat="server" CssClass="form-control input-sm" TextMode="Password"></asp:TextBox>
                                    <asp:CheckBox ID="chkMostrarPass" runat="server" OnCheckedChanged="chkMostrarPass_CheckedChanged" Text="Mostrar" AutoPostBack="true" />
                                </div>
                            </div>
                        </div>

                        <%-- VENCIMIENTO DE TARJETA --%>
                        <div class="form-group col-md-4">
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                                TargetControlID="txtFechaVencimientoTarjeta"
                                Mask="99/9999"
                                MaskType="Date" />
                            <asp:Label ID="lblFechaVencimientoTarjeta" runat="server" Text="Fecha Vencimiento"></asp:Label>
                            <asp:TextBox ID="txtFechaVencimientoTarjeta" placeholder="MM/YYYY" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        </div>

                        <%-- MONTO A CANCELAR --%>
                        <div class="form-group col-md-4">
                            <asp:Label ID="lblMontoCancelar" runat="server" Text="Monto a cancelar"></asp:Label>
                            <asp:TextBox ID="txtMontoCancelar" runat="server" Enabled="false" CssClass="form-control input-sm"></asp:TextBox>
                        </div>

                        <div class="row"></div>

                        <%-- COBRAR REQUERIMIENTOS SELECCIONADOS --%>
                        <div class="form-group col-md-4 col-md-offset-4">
                            <asp:Button ID="btnCobrar" OnClick="btnCobrar_Click" CssClass="btn btn-danger btn-block" runat="server" Text="Cobrar" />
                            <asp:Label ID="lblErrorCobro" Visible="false" ForeColor="Red" Font-Bold="true" Font-Size="Large" runat="server" Text=""></asp:Label>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnCobrar" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>
</asp:Content>
