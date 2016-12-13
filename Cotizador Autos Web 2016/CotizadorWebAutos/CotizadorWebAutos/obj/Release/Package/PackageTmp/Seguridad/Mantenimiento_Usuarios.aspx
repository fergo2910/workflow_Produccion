<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Mantenimiento_Usuarios.aspx.cs" Inherits="CotizadorWebAutos.Seguridad.Mantenimiento_Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <br />
        <br />
        <div class="col-md-12" style="height: 75px; text-align: center">
            <h1 style="background-color: white">Usuarios del sistema</h1>
        </div>
        <div class="row">
         <div class="col-md-10 col-md-offset-1  ">
                 <div class="form-inline">
                   <span class="input-group-addon"><span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                   <asp:TextBox ID="txtbusquedaNombre" runat="server" CssClass="form-control input-sm"  Style="text-transform: uppercase" ToolTip="Ingrese" OnTextChanged="txtbusquedaNombre_TextChanged" AutoPostBack="true" placeholder="Usuario" ></asp:TextBox>
                   <asp:DropDownList ID="ddlcodinter" runat="server" class="form-control" ToolTip="Seleccione un intermediario" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlcodinter_SelectedIndexChanged" AutoPostBack="true" Width="700">
                   </asp:DropDownList>  
                 </div>
          <br />
        </div>
            </div>
        <div class="col-md-10 col-md-offset-1" style="height: 350px; overflow: auto; align-content: center">
            <br />
            <br />
            <asp:GridView CssClass="panel-danger" DataKeyNames="id_usuario" ID="grvUsuariosSistema" runat="server" AutoGenerateColumns="False" OnRowCommand="grvUsuariosSistema_RowCommand" GridLines="Both" OnRowDataBound="grvUsuariosSistema_RowDataBound">
                <Columns>
                    <asp:ButtonField ButtonType="Link" CommandName="Configurar" Text="Configurar">
                        <ControlStyle CssClass="btn btn-danger" />
                    </asp:ButtonField>
                    <asp:BoundField DataField="id_usuario" HeaderText="id_usuario" />
                    <asp:BoundField DataField="nombre_unico_usuario" HeaderText="Usuario" />
                    <asp:BoundField DataField="correo_electronico" HeaderText="Correo" />
                    <asp:BoundField DataField="codigo_intermediario" HeaderText="Código Intermediario">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="codigo_usuario_remoto" HeaderText="Código Usuario Remoto">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="estado" HeaderText="Estado" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnBorrar" Text="Borrar" OnCommand="borrarUsuario" runat="server" CommandArgument='<%# Eval("id_usuario") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnHabilitar" Text="Habilitar" OnCommand="habilitarUsuario" CommandArgument='<%# Eval("id_usuario") %>' runat="server"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-3 col-md-offset-4" style="height: 350px; overflow: auto; align-content: center">
          <br />
           <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-block btn-danger" OnClick="btnRegresar_Click" />
        </div>
    </div>
</asp:Content>
