<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Pagos.aspx.cs" Inherits="CotizadorWebAutos.Pagos.Pagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="col-md-8 col-md-offset-2" style="text-align: left">
        <div class="form-group col-md-12">
            <h1><span style="color: black">ESTADO DE CUENTA</span></h1>
        </div>    
        <div class="form-group col-md-4">
            <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click"><i class="glyphicon glyphicon glyphicon-arrow-left"></i> Regresar</asp:LinkButton>
        </div>
    </div>
    </div>
</asp:Content>
