<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Configuracion_Plan_Intermediario.aspx.cs" Inherits="CotizadorWebAutos.Seguridad.Configuracion_Plan_Intermediario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
        <br />
        <br />
        <br />
        <div class="panel panel-default">

            <div class="panel-heading">
                <h4>Asignación De Planes Por Intermediario </h4>
            </div>

            <div class="panel-body" style="overflow-y: auto">

                <div class="form-group col-md-8">
                    <asp:Label runat="server" Text="Catálogo Intermediarios Activos:"></asp:Label>
                    <asp:DropDownList ID="cmbIntermediarios" runat="server" CssClass="form-control" OnSelectedIndexChanged="cmbIntermediarios_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>

                <div class="form-group col-md-8">
                    <asp:Label runat="server" Text="Catálogo Planes Disponibles:"></asp:Label>
                    <asp:CheckBoxList runat="server" ID="chkPlanesDisponibles" AutoPostBack="true" OnSelectedIndexChanged="chkPlanesDisponibles_SelectedIndexChanged" TextAlign="Right"></asp:CheckBoxList>
                </div>

            </div>
        </div>

        <div class="col-md-4 col-md-offset-4">
            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-block btn-danger" OnClick="btnRegresar_Click" />
        </div>
    </div>

</asp:Content>
