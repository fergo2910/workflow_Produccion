<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Cotizar_Auto.aspx.cs" Inherits="CotizadorWebAutos.Productos_Web.Cotizar_Auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <!--DIV PRINCIPAL-->
        <div class="col-md-8 col-md-offset-2" style="text-align: left">
            <!--DIV ENCABEZADO-->
            <div class="form-group col-md-12">
                <h3 runat="server">
                    <asp:Label ID="lblNombreProducto" runat="server" Text="" Font-Bold="true"></asp:Label></h3>
                 <h6 runat="server"><asp:Label ID="Label2" runat="server" Text="Todos los campos con * son obligatorios" ForeColor="#ff5050"></asp:Label></h6>
            </div>
            <!--DIV PLANES-->
            <%--<div id="divPlanesAuto" class="form-group col-md-6">
            <asp:Label ID="lblPlanesAuto" runat="server" Text="Plan:"></asp:Label>
            <asp:DropDownList ID="ddlPlanesAuto" runat="server" class="form-control" AutoPostBack="true">
            <%--<asp:DropDownList ID="ddlPlanesAuto" runat="server" class="form-control" OnSelectedIndexChanged="ddlPlanesAuto_SelectedIndexChanged" AutoPostBack="true">--%>
            <%--<asp:ListItem Value="0">Seleccione el plan para poder cotizar</asp:ListItem>
            </asp:DropDownList>--%>
            <%-- </div>--%>
            <!--DIV FORMA PAGO-->
            <div id="divFormaPago" class="form-group col-md-6">
                <asp:Label ID="lblFormaPago" runat="server" Text="Forma de pago:"></asp:Label>
                <asp:DropDownList ID="ddlFormaPago" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged">
                </asp:DropDownList>
            </div>

            <!--DIV PLANES-->
            <div id="divNumeroPagos" class="form-group col-md-6">
                <asp:Label ID="lblNumeroPagos" runat="server" Text="Número de pagos:"></asp:Label>
                <asp:DropDownList ID="ddlNumeroPagos" runat="server" class="form-control">
                </asp:DropDownList>
            </div>
            <div class="row"></div>
            <!--DIV MOMBRE CLIENTE-->
            <div id="divNombreClienteCotizacion" class="form-group col-md-6">
                <asp:Label ID="lblNombresCliente" runat="server" Text="Nombres: *"></asp:Label>
                <asp:TextBox ID="txtNombresCliente" runat="server" class="form-control" placeholder="Nombres Cliente" required="true"></asp:TextBox>
            </div>
            <!--DIV APELLIDO CLIENTE-->
            <div id="divApellidosClienteCotizacion" class="form-group col-md-6">
                <asp:Label ID="lblApellidosCliente" runat="server" Text="Apellidos: *"></asp:Label>
                <asp:TextBox ID="txtApellidosCliente" runat="server" class="form-control" placeholder="Apellidos Cliente" required="true"></asp:TextBox>
            </div>
            <!--DIV TIPO VEHICULO-->
            <div id="divTipoVehiculo" class="form-group col-md-4">
                <asp:Label ID="lvlTipoVehiculo" runat="server" Text="Tipo de vehículo:"></asp:Label>
                <asp:DropDownList ID="ddlTipoVehiculo" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTipoVehiculo_SelectedIndexChanged">
                    <%--<asp:ListItem Value="0">Seleccione un tipo</asp:ListItem>--%>
                </asp:DropDownList>
            </div>
            <!--DIV MARCA VEHICULO-->
            <div id="divMarcaVehiculo" class="form-group col-md-4">
                <asp:Label ID="lblMarcaVehiculo" runat="server" Text="Marca vehículo:"></asp:Label>
                <asp:DropDownList ID="ddlMarcaVehiculo" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMarcaVehiculo_SelectedIndexChanged">
                    <%--<asp:DropDownList ID="ddlMarcaVehiculo" runat="server" class="form-control" OnSelectedIndexChanged="ddlMarcaVehiculo_SelectedIndexChanged" AutoPostBack="true">--%>
                    <%--<asp:ListItem Value="0">Seleccione una marca</asp:ListItem>--%>
                </asp:DropDownList>
            </div>
            <!--DIV LINEA VEHICULO-->
            <div id="divLineaVehiculo" class="form-group col-md-4">
                <asp:Label ID="lblLineaVehiculo" runat="server" Text="Linea vehículo:"></asp:Label>
                <asp:DropDownList ID="ddlLineaVehiculo" runat="server" class="form-control" OnSelectedIndexChanged="ddlLineaVehiculo_SelectedIndexChanged" AutoPostBack="true">
                    <%--<asp:ListItem Value="0">Seleccione una linea</asp:ListItem>--%>
                </asp:DropDownList>
            </div>
            <!--DIV ANIO VEHICULO-->
            <div id="divAnioVehiculo" class="form-group col-md-3">
                <asp:Label ID="lblAnioVehiculo" runat="server" Text="Año vehículo:"></asp:Label>
                <asp:DropDownList ID="ddlAnioVehiculo" runat="server" class="form-control">
                </asp:DropDownList>
            </div>
            <!--DIV ASIENTOS VEHICULO-->

            <div id="divAsientosVehiculo" class="form-group col-md-2">
                <asp:Label ID="lblAsientosVehiculo" runat="server" Text="Asientos:"></asp:Label>
                <asp:TextBox ID="txtAsientosVehiculo" runat="server" class="form-control" placeholder="5" onkeypress="return IsNumeric(event);"></asp:TextBox>
                <%--<asp:TextBox ID="txtAsientosVehiculo" runat="server" class="form-control" placeholder="5" onkeypress="return IsNumeric(event);"></asp:TextBox>--%>
            </div>
            <!--DIV DANOS POR OCUPANTES-->
            <div id="divDanosOcupantes" class="form-group col-md-3">
                <asp:Label ID="lblDanosOcupantes" runat="server" Text="Lesiones a ocupantes:"></asp:Label>
                <asp:DropDownList ID="ddlDanosOcupantes" runat="server" class="form-control">
                </asp:DropDownList>
                <%--<asp:TextBox ID="txtAsientosVehiculo" runat="server" class="form-control" placeholder="5" onkeypress="return IsNumeric(event);"></asp:TextBox>--%>
            </div>
            <!--DIV MONTO ASEGURADO-->
            <asp:Panel ID="Panel1" runat="server">
                <div id="divValorMontoAsegurado" class="form-group col-md-4" runat="server">
                    <asp:Label ID="lblMontoAsegurado" runat="server" Text="Valor del vehiculo: *"></asp:Label>
                    <div class="input-group">
                        <div class="input-group-addon">
                            <asp:Label ID="lblValorVehiculoMoneda" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:TextBox ID="txtValorVehiculo" runat="server" class="form-control" onkeypress="return IsNumeric(event);" required="true"></asp:TextBox>
                        <%--<asp:TextBox ID="txtValorVehiculo" runat="server" class="form-control" onkeypress="return IsNumeric(event);"></asp:TextBox>--%>
                        <%--<div class="input-group-addon" visible="true">.00</div>--%>
                    </div>
                </div>
            </asp:Panel>
            <!--DIV RESPONSIBILIDAD CIVIL-->
            <div id="divRC" class="form-group col-md-4">
                <asp:Label ID="lblRC" runat="server" Text="Límite de Responsabilidad Civil:"></asp:Label>
                <asp:DropDownList ID="ddlRC" runat="server" class="form-control">
                </asp:DropDownList>
            </div>
            <!--DIV MENORES-->
            <%--<div id="divMenores" class="form-group col-md-4">
            <asp:Label ID="lblMenores" runat="server" Text="Menores:"></asp:Label>
            <asp:DropDownList ID="ddlMenores" runat="server" class="form-control">
                <asp:ListItem Value="0">Seleccione "Menores hasta"</asp:ListItem>
            </asp:DropDownList>
        </div>--%>
            <%--<!--DIV CRISTALES-->
        <div id="divCristales" class="form-group col-md-4">
            <asp:Label ID="lblCristales" runat="server" Text="Cristales, Hasta un máximo de:"></asp:Label>
            <asp:DropDownList ID="ddlCristales" runat="server" class="form-control" AutoPostBack="true">
            <%--<asp:DropDownList ID="ddlMarcaVehiculo" runat="server" class="form-control" OnSelectedIndexChanged="ddlMarcaVehiculo_SelectedIndexChanged" AutoPostBack="true">--%>
            <%--<asp:ListItem Value="0">Seleccione maximo de cristales</asp:ListItem>
            </asp:DropDownList>--%>
            <%--</div>--%>
            <!--DIV MUERTE-->
            <%--<div id="divMuerte" class="form-group col-md-4">
            <asp:Label ID="lblMuerte" runat="server" Text="Muerte accidental:"></asp:Label>
            <asp:DropDownList ID="ddlMuerte" runat="server" class="form-control">
                <asp:ListItem Value="0">Seleccione maximo de muerte acc.</asp:ListItem>
            </asp:DropDownList>
        </div>--%>
            <!--DIV TELEFONO PRINCIPAL-->
            <div class="form-group col-md-4">
                <asp:Label ID="lblTelefonoPrincipal" runat="server" Text="Teléfono de Contacto: *"></asp:Label>
                <asp:TextBox ID="txtTelefonoPrincipal" runat="server" class="form-control" MaxLength="8" onkeypress="return IsNumeric(event);" required="true"></asp:TextBox>
                <%--<asp:TextBox ID="txtTelefonoPrincipal" runat="server" class="form-control" MaxLength="8" onkeypress="return IsNumeric(event);"></asp:TextBox>--%>
            </div>

            <!--DIV TELEFONO SECUNDARIO-->
            <div class="form-group col-md-4">
                <asp:Label ID="lblTelefonoSecundario" runat="server" Text="Teléfono Adicional:"></asp:Label>
                <asp:TextBox ID="txtTelefonoSecundario" runat="server" class="form-control" MaxLength="8" onkeypress="return IsNumeric(event);"></asp:TextBox>
                <%--<asp:TextBox ID="txtTelefonoSecundario" runat="server" class="form-control" MaxLength="8" onkeypress="return IsNumeric(event);"></asp:TextBox>--%>
            </div>
            <!--DIV CORREO ELECTRONICO-->
            <div class="form-group col-md-4">
                <asp:Label ID="lblCorreoElectronico" runat="server" Text="Correo: *"></asp:Label>
                <asp:TextBox ID="txtCorreoElectronico" runat="server" class="form-control" placeholder="ejemplo@ejemplo.com" type="email" required="true"></asp:TextBox>
            </div>

            <!--DIV EQUIPO ESPECIAL-->
            <asp:Panel ID="Panel2" runat="server">
                <div id="divEquipoEspecial" class="form-group col-md-4">
                    <asp:Label ID="lblEquipoEspecial" runat="server" Text="Valor de Equipo Especial:"></asp:Label>
                    <div class="input-group">
                        <div class="input-group-addon">
                            <asp:Label ID="lblEquipoEspecialMoneda" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:TextBox ID="txtEquipoEspecial" runat="server" class="form-control" onkeypress="return IsNumeric(event);"></asp:TextBox>
                        <%--<asp:TextBox ID="txtValorVehiculo" runat="server" class="form-control" onkeypress="return IsNumeric(event);"></asp:TextBox>--%>
                        <%--<div class="input-group-addon">.00</div>--%>
                    </div>
                </div>            
            </asp:Panel>
           
                <!--DIV INTERMEDIARIO-->
                <div id="divIntermediario" class="form-group col-md-8">
                    <asp:Label ID="lblIntermediario" runat="server" Text="Intermediario:"></asp:Label>
                    <asp:DropDownList ID="ddlIntermediario" runat="server" class="form-control" AutoPostBack="true" >                       
                    </asp:DropDownList>
                </div>
            
            <!--DIV DESCUENTOS-->
            <div class="form-group col-md-4">
                <asp:Label ID="lblDescuentos" runat="server" Text="Descuento: (%)" Visible="false"></asp:Label>
                <asp:TextBox ID="txtDescuento" runat="server" class="form-control" MaxLength="2" Visible="false" onkeypress="return IsNumeric(event);"></asp:TextBox>
                <%--<asp:TextBox ID="txtTelefonoSecundario" runat="server" class="form-control" MaxLength="8" onkeypress="return IsNumeric(event);"></asp:TextBox>--%>
            </div>
           
            <!--DIV COBERTURAS ADICIONALES-->
            <div class="form-group col-md-5">
                <h3>
                    <asp:Label ID="lblCoberturasAdicionales" runat="server" Text="Coberturas Adicionales:"></asp:Label></h3>
                <asp:GridView ID="gvCoberturasAdicionales" runat="server" CellPadding="4"
                    CssClass="grdData" ForeColor="#333333" Width="500px" AutoGenerateColumns="False" OnRowDataBound="gvCoberturasAdicionales_RowDataBound">
                    <AlternatingRowStyle BackColor="White"
                        ForeColor="#333333"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Elegir">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbSelect"
                                    CssClass="gridCB" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="DESCRIP"
                        HeaderText="DESCRIPCION"></asp:BoundField>
                    <asp:BoundField DataField="SUMAASEG"
                        HeaderText="SUMA"></asp:BoundField>
                    <asp:BoundField DataField="PRIMA"
                        HeaderText="PRIMA"></asp:BoundField>
                    <asp:BoundField DataField="CODRAMO" HeaderText="CODIGO RAMO" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>--%>
                        <asp:BoundField DataField="CODCOBERT" HeaderText="CODIGO COBERTURA" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <%--<asp:BoundField DataField="SUMAASEG" HeaderText="Suma Asegurada" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N2}">
                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TASA" HeaderText="TASA" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRIMA" HeaderText="Prima" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:N2}">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MAXPORCSUM" HeaderText="MAXPORCSUM" HeaderStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>--%>
                        <asp:BoundField DataField="NOMBRE" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Suma Asegurada">
                            <ItemTemplate>
                                <%--<asp:Label ID="lblValor" runat="server" Text='<%# Eval("SUMAASEG") %>' Visible="false" />--%>
                                <asp:DropDownList ID="ddlMontos" runat="server" DataTextFormatString="{0:N2}" Width="100px">
                                </asp:DropDownList>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            <ItemStyle Width="100px" Wrap="True" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                    <FooterStyle BackColor="White" Font-Bold="True"
                        ForeColor="White"></FooterStyle>
                    <HeaderStyle BackColor="#DF0000" Font-Bold="True"
                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                    <PagerStyle BackColor="#DF0000" ForeColor="White"
                        HorizontalAlign="Center"></PagerStyle>
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"
                        ForeColor="#333333"></SelectedRowStyle>
                    <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                    <SortedAscendingHeaderStyle BackColor="#DF0000"></SortedAscendingHeaderStyle>
                    <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                    <SortedDescendingHeaderStyle BackColor="#DF0000"></SortedDescendingHeaderStyle>
                </asp:GridView>
            </div>
            <asp:Panel ID="Panel3" runat="server">
                <!--DIV ALARMA-->
                <div class="form-group col-md-4 col-lg-offset-3">
                    <h2>
                        <asp:Label ID="lblAlarma" runat="server" Text="Alarma:"></asp:Label></h2>
                    <asp:RadioButtonList ID="rbAlarma" runat="server">
                        <asp:ListItem Value="0" Selected="True">SIN ALARMA</asp:ListItem>
                        <asp:ListItem Value="1">GPS</asp:ListItem>
                        <%--<asp:ListItem Value="S">INMOBILIZADORA</asp:ListItem>--%>
                        <asp:ListItem Value="2">LOJACK</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </asp:Panel>
            <div class="row"></div>
            <!--DIV RECARGOS ADICIONALES-->
            <div class="form-group col-md-5">
                <h3>
                    <asp:Label ID="lblRecargos" runat="server" Text="Anexos Adicionales:"></asp:Label></h3>
                <asp:GridView ID="gvRecargosAdicionales" runat="server" CellPadding="4"
                    CssClass="grdData" ForeColor="#333333" Width="500px" OnRowDataBound="gvRecargosAdicionales_RowDataBound" AutoGenerateColumns="False" GridLines="Both">
                    <AlternatingRowStyle BackColor="White"
                        ForeColor="#333333"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Elegir">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbSelect"
                                    CssClass="gridCB" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="DESCRIP"
                        HeaderText="DESCRIPCION"></asp:BoundField>
                    <asp:BoundField DataField="SUMAASEG"
                        HeaderText="SUMA"></asp:BoundField>
                    <asp:BoundField DataField="PRIMA"
                        HeaderText="PRIMA"></asp:BoundField>--%>
                        <asp:BoundField DataField="CODRECADCTO" HeaderText="CODIGO RECARGO" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DESCRECADCTO" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MTOLIMRECADCTO" HeaderText="LÍMITE RECARGO" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PORCRECADCTO" HeaderText="PORCENTAJE RECARGO" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="INDOBLIG" HeaderText="OBLIGATORIO" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                    <FooterStyle BackColor="#DF0000" Font-Bold="True"
                        ForeColor="White"></FooterStyle>
                    <HeaderStyle BackColor="#DF0000" Font-Bold="True"
                        ForeColor="White"></HeaderStyle>
                    <PagerStyle BackColor="#DF0000" ForeColor="White"
                        HorizontalAlign="Center"></PagerStyle>
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True"
                        ForeColor="#333333"></SelectedRowStyle>
                    <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                    <SortedAscendingHeaderStyle BackColor="#DF0000"></SortedAscendingHeaderStyle>
                    <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                    <SortedDescendingHeaderStyle BackColor="#DF0000"></SortedDescendingHeaderStyle>
                </asp:GridView>
            </div>
            <div class="form-group col-md-4 col-lg-offset-3">
                <asp:Image ID="imagenProducto" runat="server" ImageUrl="" Width="500px" Height="177px" class="img-responsive img-rounded" />
            </div>
            <div class="row"></div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--DIV BOTON COTIZAR-->
                    <div class="form-group col-md-3" style="align-content: center">
                        <asp:Button ID="btnCotizar" runat="server" Text="Cotizar" class="btn btn-danger btn-block" OnClick="btnCotizar_Click" />
                        <%--<asp:Button ID="btnGuardarCotizacion" runat="server" Text="Cotizar" class="btn btn-danger btn-block" OnClick="btnGuardarCotizacion_Click" />--%>
                    </div>
                    <!--DIV BOTON GUARDAR-->
                    <div class="form-group col-md-3" style="align-content: center">
                        <asp:LinkButton ID="btnGuardarCotizacionFinal" runat="server" CssClass="btn btn-danger btn-block" Visible="false" OnClick="btnGuardarCotizacionFinal_Click"><i class="glyphicon glyphicon-save"></i> Guardar Cotización</asp:LinkButton>
                        <%--<asp:LinkButton ID="btnGuardarCotizacionFinal" runat="server" CssClass="btn btn-danger btn-block" OnClick="btnGuardarCotizacionFinal_Click" Visible="false"><i class="glyphicon glyphicon-save"></i> Guardar Cotización</asp:LinkButton>--%>
                    </div>
                    <div class="col-md-3">
                        <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                            <ProgressTemplate>
                                <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>

                    <div class="row"></div>
                    <div class="col-md-3">
                        <asp:Label ID="Label1" runat="server" Text="Label" Visible="false"></asp:Label>
                    </div>
                    <div class="row"></div>
                    <div class="form-group col-md-5">
                        <span id="error" style="color: Red; display: none">* Solo se permiten números (0 - 9)</span>
                    </div>

                    <div class="row"></div>


                    <!--DIV CAMBIO DE LINEA-->
                    <div class="row"></div>
                    <!--DIV GRIDVIEW-->
                    <div class="form-group col-md-12">
                        <asp:GridView ID="gvInformacionPoliza" runat="server"></asp:GridView>
                    </div>

                    <div class="form-group col-md-6">
                        <h2>
                            <asp:Label ID="lblResumenPagosVisaCuotas" runat="server" Text="Pagos Cuotas T.C." Visible="false"></asp:Label></h2>
                        <asp:GridView ID="gvInformacionPlanesCuotas" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="CUOTAS" HeaderText="No. de cuotas" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Monto" HeaderText="Total cuota" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CUOTA" HeaderText="Pago Total" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle BackColor="#DF0000" ForeColor="White" />
                        </asp:GridView>
                    </div>
                    <div class="form-group col-md-6">
                        <h2>
                            <asp:Label ID="lblResumenPagosFraccionados" runat="server" Text="Pagos Fraccionados" Visible="false"></asp:Label></h2>
                        <asp:GridView ID="gvInformacionPlanesFraccionado" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="CUOTAS" HeaderText="No. de cuotas" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MONEDA" HeaderText="Moneda" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Monto" HeaderText="Total cuota" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CUOTA" HeaderText="Pago Total" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle BackColor="#DF0000" ForeColor="White" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnCotizar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div class="row"></div>

            <div class="col-md-8 col-md-offset-2" style="text-align: center">
                <div class="form-group col-md-4 col-md-offset-4">
                    <%--CANCELAR--%>
                    <%--<asp:Button ID="btnRegresarCotizacionesAlmacenadas" runat="server" Text="Regresar" OnClick="btnRegresarCotizacionesAlmacenadas_Click" class="btn btn-danger btn-block" />--%>
                    <asp:LinkButton ID="btnRegresar" runat="server" CssClass="btn btn-danger btn-block" OnClick="btnCancelarCotizacion_Click"><i class="glyphicon glyphicon glyphicon-arrow-left"></i> Regresar</asp:LinkButton>
                    <%--<asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger btn-block" OnClick="btnCancelarCotizacion_Click"><i class="glyphicon glyphicon glyphicon-arrow-left"></i> Regresar</asp:LinkButton>--%>
                </div>
            </div>

        </div>
    </div>

    <script type="text/javascript">
        var specialKeys = new Array();
        specialKeys.push(8);
        specialKeys.push(9);//Backspace
        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var ret = ((keyCode >= 48 && keyCode <= 57) || (specialKeys.indexOf(keyCode) != -1) || (keyCode == 46));
            document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }
    </script>

    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-75674732-1', 'auto');
        ga('send', 'pageview');

    </script>
</asp:Content>

