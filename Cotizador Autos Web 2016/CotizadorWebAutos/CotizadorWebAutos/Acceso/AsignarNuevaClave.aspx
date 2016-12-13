<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="AsignarNuevaClave.aspx.cs" Inherits="CotizadorWebAutos.Acceso.AsignarNuevaClave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">

        <div class="col-md-8 col-lg-offset-2" style="align-content: center; background-color: whitesmoke">            
            <h3 style="text-align: center"><b>Configurar nueva clave de acceso</b></h3>
        </div>

        <div class="col-md-4 col-md-offset-4">
            <br />
            <asp:TextBox ID="txtClave" runat="server" CssClass="form-control" placeholder="Escribe tu contraseña *" TextMode="Password"></asp:TextBox>
        </div>

        <div class="col-md-4 col-md-offset-4" style="align-content: center">
            <asp:TextBox ID="txtRepetirClave" runat="server" CssClass="form-control" placeholder="Confirma tu contraseña *" TextMode="Password"></asp:TextBox>
        </div>

        <div class="col-md-4 col-md-offset-5" style="align-content: center">
            <br />
            <asp:Button ID="btnGuardarContraseña" runat="server" Text="Guardar contraseña" CssClass="btn btn-danger btn-sm" OnClick="btnGuardarContraseña_Click" />
        </div>

    </div>
</asp:Content>
