<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Cotizador_Web_Autos.Master" AutoEventWireup="true" CodeBehind="RecuperarClave.aspx.cs" Inherits="CotizadorWebAutos.Acceso.RecuperarClave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container" style="align-content: center;">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

        <div class="col-md-4 center-block" style="align-content: center; margin-left: auto">
            <img src="../Recursos/imagenes/llaves.png" class="img-rounded" alt="Cinque Terre" width="200" height="200" />
        </div>

        <div class="container">
            <div class="col-md-8" style="align-content: center; background-color: whitesmoke">
                <h3 style="text-align: center"><b>Solicitud de nueva contraseña</b></h3>
            </div>

            <div class="col-md-8" style="text-align: center">
                <br />
                <h5 style="text-align: center"><b>Ingrese el correo electronico con el que se registró. </b></h5>
                <br />

                <div class="col-md-8 col-md-offset-2" style="align-content: center">
                    <div class="input-group">
                        <span class="input-group-addon" style="align-content: center"><span class="glyphicon glyphicon-envelope" aria-hidden="true"></span></span>
                        <asp:TextBox ID="txtCorreoElectronico" runat="server" CssClass="form-control" placeholder="Correo electrónico *">     
                        </asp:TextBox>

                    </div>
                    <br />
                </div>
                <br />
                <div class="col-md-4 col-md-offset-4" style="align-content: center">
                    <asp:Button ID="btnSolicitar" runat="server" Text="Solicitar nueva contraseña" CssClass="btn btn-danger btn-sm" OnClick="btnSolicitar_Click" />
                    <br />
                    <br />
                    <br />
                    <br />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
