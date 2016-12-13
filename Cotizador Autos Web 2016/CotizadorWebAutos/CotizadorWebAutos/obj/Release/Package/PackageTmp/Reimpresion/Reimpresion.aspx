<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Reimpresion.aspx.cs" Inherits="CotizadorWebAutos.Reimpresion.Reimpresion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">


        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <br />
        <br />

        <div class="col-md-8 col-md-offset-2" style="text-align: center">
            <h2>Busqueda de Polizas</h2>
        </div>

        <br />

        <%--Busqueda Cliente--%>
        <div class="col-md-4 col-md-offset-4" style="text-align: center">
            <div class="form-inline">
                <div class="col-xs-12">
                    <label>Ingrese los datos de la poliza</label>
                    <%--<asp:DropDownList ID="cmbTipoCliente" runat="server" CssClass="form-control">
                    <asp:ListItem Value="P">Individual</asp:ListItem>
                    <asp:ListItem Value="E">Jurídico</asp:ListItem>
                </asp:DropDownList>--%>
                </div>
            </div>
        </div>

        <br />
        <br />

        <div class="col-md-12" style="text-align: center">
            <div class="form-inline">
                <div class="form-group">
                    <asp:TextBox ID="txtCodPol" runat="server" CssClass="form-control input-sm" MaxLength="4" Width="110px" Style="text-transform: uppercase"></asp:TextBox>
                    -
                                <asp:TextBox ID="txtNumpol" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="110px" onkeypress="return IsNumeric(event);"></asp:TextBox>
                    <asp:Button ID="btnBuscarPolizaEmitida" runat="server" Text="Buscar" CssClass="btn-danger" OnClick="btnBuscarPolizaEmitida_Click" />
                </div>
            </div>
        </div>
        <div class="row"></div>
        <br />
        <br />
        <div class="col-md-12" style="text-align: center">
            <asp:Label ID="lblErrores" runat="server" Text="" CssClass="alert-danger"></asp:Label>
        </div>
        <div class="row"></div>
        <div class="col-md-10 col-md-offset-2">
            <div class="form-group">
                <div class="form-inline">
                    <asp:GridView ID="gvInformacionPoliza" runat="server" OnRowDataBound="gvInformacionPoliza_RowDataBound" CssClass="panel-danger" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White"
                            ForeColor="#333333"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Elegir">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDetallePoliza" Visible="True" Text="Detalle" OnCommand="detallePoliza" CommandArgument='<%# Eval("idepol_emi") %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="inicio" HeaderText="Inicio Vigencia" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fin" HeaderText="Fin Vigencia" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:d}">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codpol_emi" HeaderText="Codigo Poliza" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numpol_emi" HeaderText="Numero Poliza" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="producto" HeaderText="Producto Poliza" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cliente" HeaderText="Nombre Cliente" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
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
            </div>
        </div>
        <div class="row"></div>
        <div class="col-md-4 col-md-offset-5">
            <div class="form-group">
                <div class="form-inline">
                    <asp:GridView ID="gvDetallePoliza" runat="server" OnRowDataBound="gvDetallePoliza_RowDataBound" CssClass="panel-danger">
                        <Columns>
                            <asp:TemplateField HeaderText="Elegir">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnImprimir" Visible="True" Text="Imprimir" OnCommand="imprimirdocumentos" CommandArgument='<%# Eval("NOMBRE") %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre Acrhivo" HeaderStyle-HorizontalAlign="Center">
                                <HeaderStyle HorizontalAlign="Right" VerticalAlign="Middle"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
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
            </div>
        </div>
        <br />
        <br />
        <div class="row"></div>

        <div class="col-md-8 col-md-offset-2" style="text-align: center">
            <div class="form-group col-md-4 col-md-offset-4">
                <%--CANCELAR--%>
                <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click"><i class="glyphicon glyphicon glyphicon-arrow-left"></i> Regresar</asp:LinkButton>
            </div>
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
