<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Cotizador_Web_Autos.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="CotizadorWebAutos.Acceso.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="col-md-8 col-lg-offset-2" style="align-content: center; background-color: whitesmoke">
            <h3 style="text-align: center"><b>Información del nuevo usuario</b></h3>
        </div>

        <div class="row"></div>

        <br />
        <br />

        <div class="col-md-10 col-lg-offset-1" style="border: none">

            <div class=" form-group col-md-12" style="text-align: center">
                <asp:Label ID="Label10" runat="server" Text="NIT"></asp:Label>
                <div class="form-inline">
                    <div class="form-group">
                        <asp:TextBox ID="txtNumID" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="110px"></asp:TextBox>
                        -
                        <asp:TextBox ID="txtDvId" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px" Style="text-transform: uppercase"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row"></div>

            <div class="form-group col-md-6">
                <asp:Label ID="Label1" runat="server" Text="Nombres"></asp:Label>
                <asp:TextBox ID="txtNombres" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group col-md-6">
                <asp:Label ID="Label2" runat="server" Text="Apellidos"></asp:Label>
                <asp:TextBox ID="txtApellidos" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="row"></div>

            <div class="form-group col-md-6">
                <asp:Label ID="Label3" runat="server" Text="Teléfono"></asp:Label>
                <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server" MaxLength="8"></asp:TextBox>
            </div>

            <div class="form-group col-md-6">
                <asp:Label ID="Label4" runat="server" Text="Correo Electrónico"></asp:Label>
                <asp:TextBox ID="txtCorreoElectronico" CssClass="form-control" runat="server" TextMode="Email"></asp:TextBox>
            </div>

            <div class="form-group col-md-12">
                <asp:Label ID="Label8" runat="server" Text="Dirección"></asp:Label>
                <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="Label5" runat="server" Text="Nombre de usuario"></asp:Label>
                <asp:TextBox ID="txtNombreUsuario" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class=" form-group col-md-4 bottom-right">
                <asp:Label ID="Label11" runat="server" Text="Usuario Interno?"></asp:Label>
                <asp:CheckBox ID="chkUsuarioInterno" runat="server" />
                <br />
                <asp:Label ID="Label6" runat="server" Text="Usuario Médico?"></asp:Label>
                <asp:CheckBox ID="chkUsuarioMedico" runat="server" OnCheckedChanged="chkUsuarioMedico_CheckedChanged" AutoPostBack="True"/>
            </div>

            <div class="form-group col-md-4">
                <asp:Label ID="lblCodigoIntermediario" runat="server" Text="Código Intermediario"></asp:Label>
                <asp:DropDownList ID="dropCodigoIntermediario" CssClass="form-control" AutoPostBack="true" runat="server" OnLoad="dropCodigoIntermediario_SelectedIndexChanged" OnSelectedIndexChanged="dropCodigoIntermediario_SelectedIndexChanged"></asp:DropDownList>
                <br />
                <asp:TextBox ID="txtCodigoIntermediario" Visible="false" runat="server" CssClass="form-control" placeholder="Escribe el código de intermediario aquí"></asp:TextBox>
            </div>

            <div class="row"></div>

            <div class="col-md-4 col-lg-offset-4">
                <asp:Button ID="btnGuardarUsuarioNuevo" OnClick="btnGuardarUsuarioNuevo_Click" CssClass="btn btn-block btn-danger" runat="server" Text="Guardar Usuario" />
            </div>

            <div class="row"></div>

            <br />

            <div class="col-md-4 col-md-offset-4" style="height: 350px; overflow: auto; align-content: center">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-block btn-danger" OnClick="btnRegresar_Click" />
            </div>

        </div>
    </div>
</asp:Content>
