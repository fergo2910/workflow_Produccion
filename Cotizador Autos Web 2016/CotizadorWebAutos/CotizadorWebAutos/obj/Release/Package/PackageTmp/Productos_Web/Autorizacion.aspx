<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Autorizacion.aspx.cs" Inherits="CotizadorWebAutos.Productos_Web.Autorizacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <style type="text/css">
        .overlay {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #F8F8FF;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }

        .overlayContent {
            z-index: 99;
            margin: 250px auto;
            width: 80px;
            height: 80px;
        }

            .overlayContent h2 {
                font-size: 18px;
                font-weight: bold;
                color: #000;
            }

            .overlayContent img {
                width: 80px;
                height: 80px;
            }
    </style>




    <div class="col-md-8 col-md-offset-2" style="text-align: left">
        <div class="form-group col-md-12">
            <h1><span style="color: black">AUTORIZACIÓN INSPECCIÓN</span></h1>
        </div>

        <div class="form-group col-md-12">
            <asp:Label ID="lblComentarioInspeccion" runat="server" Text="Comentario Inspección:"></asp:Label>
            <asp:TextBox ID="txtComentariosInspeccion" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Enabled="False"></asp:TextBox>
        </div>

        <div class="form-group col-md-12">
            <asp:Label ID="lblCotizacion" runat="server" Text="Numero Cotización:"></asp:Label>
            <asp:TextBox ID="txtCotizacion" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Enabled="False"></asp:TextBox>
        </div>

        <div class="form-group col-md-12">
            <asp:Label ID="lblInspeccion" runat="server" Text="Numero Inspección:"></asp:Label>
            <asp:TextBox ID="txtNumeroInspeccion" runat="server" TextMode="MultiLine" CssClass="form-control input-sm" Enabled="False"></asp:TextBox>
        </div>

        <div class="form-group col-md-6">
            <label>Autorización: </label>
            <div class="form-inline">
                <div class="form-group">
                    <asp:DropDownList ID="ddlAutorizacionMAPFRE" runat="server" class="form-control">
                        <asp:ListItem Value="0">Seleccione tipo de autorización</asp:ListItem>
                        <asp:ListItem Value="AUT">AUTORIZADO</asp:ListItem>
                        <asp:ListItem Value="DEN">DENEGADO</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="row"></div>
        <div class="form-group col-md-12">
            <asp:Label ID="lblComentarioAutorizacion" runat="server" Text="Comentario:"></asp:Label>
            <asp:TextBox ID="txtComentarioAutorizacion" runat="server" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox>
        </div>
        <div class="form-group col-md-12">
            <asp:Label ID="lblRespuesta" runat="server" Text="" Visible="false" Font-Size="Medium" ForeColor="Red" CssClass="alert-danger"></asp:Label>
            </div>
       <div class="row"></div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

        <div class="form-group col-md-4">
            <asp:Button ID="btnGuardarAutorizacion" runat="server" Text="Guardar Autorización" class="btn btn-danger btn-block" OnClick="btnGuardarAutorizacion_Click" />
        </div>
        <div class="form-group col-md-4">
            <asp:Button ID="btnEmision" runat="server" Text="Emitir Poliza" class="btn btn-danger btn-block" Visible="false" OnClick="btnEmision_Click" />
        </div>
        <div class="form-group col-md-4">
            <asp:LinkButton ID="Regresar" runat="server" CssClass="btn btn-danger btn-block" Visible="true" ><i class="glyphicon glyphicon glyphicon-arrow-left"></i> Regresar</asp:LinkButton>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>


   <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
       <ProgressTemplate>
       <%-- <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>--%>
       <div class="overlay" />
       <div class="overlayContent">
       <h2>Un momento porfavor</h2>
       <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>
       </div>
       </ProgressTemplate>
  </asp:UpdateProgress>



</asp:Content>
