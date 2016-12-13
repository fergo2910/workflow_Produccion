<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Historial_Cupones.aspx.cs" Inherits="CotizadorWebAutos.GM_Control_Cupones.Historial_Cupones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div style="align-content: center; height: 400px; overflow: auto">
        <asp:GridView ID="grvHistorialCupones" CssClass="panel-danger" runat="server" AutoGenerateColumns="False" AllowSorting="True" HorizontalAlign="Center" Font-Size="Small">
            <Columns>

                <asp:BoundField DataField="NumCupon" HeaderText="No. Cupón">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

                <asp:BoundField DataField="FecSts" HeaderText="Fecha de emisión">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

                <asp:BoundField DataField="Ideproveedor" HeaderText="Id proveedor" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

                <asp:BoundField DataField="IdeAseg" HeaderText="Código de paciente" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

                <asp:BoundField DataField="Nomter" HeaderText="Nombre">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

                <asp:BoundField DataField="Apeter" HeaderText="Apellido">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

                <asp:BoundField DataField="CodParent" HeaderText="Codigo de parentesco" Visible="false">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

                <asp:BoundField DataField="DescParent" HeaderText="Parentesco">
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>

            </Columns>
            <HeaderStyle BackColor="#DF0000" ForeColor="White" />
        </asp:GridView>
    </div>
    <div class="col-md-offset-4 col-md-4">
        <br />
        <br />
        <asp:Button ID="regresar" runat="server" CssClass="btn btn-block btn-danger col-lg-6" Text="Regresar" OnClick="regresar_Click"/>
    </div>
</asp:Content>
