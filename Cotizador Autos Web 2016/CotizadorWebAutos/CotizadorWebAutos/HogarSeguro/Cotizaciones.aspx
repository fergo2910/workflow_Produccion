<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Cotizaciones.aspx.cs" Inherits="CotizadorWebAutos.HogarSeguro.Cotizaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="col-md-4 col-md-offset-4" style="text-align: center">
            <h1>Mis Cotizaciones</h1>
        </div>

        <div class="row"></div>

        <div class="col-md-8 col-md-offset-2" style="text-align: center">
            <asp:Label runat="server" ID="lblError" Visible="false" Font-Size="Medium" ForeColor="Red" CssClass="alert-danger"></asp:Label>
        </div>

        <div class="row"></div>

        <br />
        <div class="col-md-12">
            <div class="col-md-12" style="align-content: center; height: 400px; overflow: auto">
                <asp:GridView ID="grvCotizacionesIntermediario" runat="server" CssClass="panel-danger" DataKeyNames="id_cotizacion" AutoGenerateColumns="False" AllowSorting="True" HorizontalAlign="Center" Font-Size="Small" OnRowDataBound="grvCotizacionesIntermediario_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="id_cotizacion" HeaderText="Cotización">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="nombre_solicitante" HeaderText="Cliente">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="id_plan_cotizado" HeaderText="IdPlan">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="nombre_plan_web" HeaderText="Plan Cotizado">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="nombre_estado_cotizacion" HeaderText="Estado Cotizacion">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:BoundField DataField="fecha_cotizacion" HeaderText="Fecha Cotización">
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Cliente">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnINC" Text="Ingresar" Visible="false" OnCommand="ingresarCliente" runat="server" CommandArgument='<%# Eval("id_cotizacion") + ";" +Eval("id_plan_cotizado")%>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Póliza">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEMI" Visible="false" Text="Emitir" OnCommand="ingresarEmision" CommandArgument='<%# Eval("id_cotizacion") + ";" +Eval("id_plan_cotizado")%>' runat="server"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cotización">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnIMP" Text="Imprimir" Visible="true" OnCommand="imprimirCotizacion" CommandArgument='<%# Eval("id_cotizacion") + ";" +Eval("id_plan_cotizado")%>' runat="server"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnELI" Text="Eliminar" Visible="true" OnCommand="eliminarCotizacion" CommandArgument='<%# Eval("id_cotizacion") + ";" +Eval("id_plan_cotizado")%>' runat="server"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                    </Columns>
                    <HeaderStyle BackColor="#DF0000" ForeColor="White" />
                </asp:GridView>
            </div>
            <div class="form-group col-md-4 col-md-offset-4">
                <br />
                <asp:LinkButton ID="btnCotizacionNueva" runat="server" CssClass="btn btn-danger btn-block"
                    OnClick="btnCotizacionNueva_Click" Font-Bold="True"><i class="glyphicon glyphicon glyphicon-plus-sign"></i> Nueva Cotización</asp:LinkButton>
            </div>
            <div class="form-group col-md-4 col-md-offset-4" style="border: none; text-align: center">
                <asp:LinkButton ID="btnRegresar" runat="server" CssClass="btn btn-danger" OnClick="btnRegresar_Click">
                        <i class="glyphicon glyphicon glyphicon-arrow-left"></i> Regresar
                </asp:LinkButton>
            </div>
        </div>

    </div>
</asp:Content>
