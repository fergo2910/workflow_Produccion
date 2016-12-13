<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Inspeccion.aspx.cs" Inherits="CotizadorWebAutos.Productos_Web.Inspeccion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-8 col-md-offset-2" style="text-align: left">
        <div class="form-group col-md-12">
            <h1><span style="color: black">INFORMACION VEHÍCULO</span></h1>
        </div>

        <div class="form-group col-md-6">
            <asp:Label ID="lblTipoVehiculo" runat="server" Text="Tipo vehículo"></asp:Label>
            <asp:TextBox ID="txtTipoVehiculo" runat="server" CssClass="form-control input-sm" Enabled="False"></asp:TextBox>
        </div>
        <div class="form-group col-md-6">
            <asp:Label ID="lblMarcaVehiculo" runat="server" Text="Marca vehículo"></asp:Label>
            <asp:TextBox ID="txtMarcaVehiculo" runat="server" CssClass="form-control input-sm" Enabled="False"></asp:TextBox>
        </div>
        <div class="form-group col-md-6">
            <asp:Label ID="lblLineaVehiculo" runat="server" Text="Linea Vehículo"></asp:Label>
            <asp:TextBox ID="txtLineaVehiculo" runat="server" CssClass="form-control input-sm" Enabled="False"></asp:TextBox>
        </div>
        <div class="form-group col-md-6">
            <asp:Label ID="lblAnioVehiculo" runat="server" Text="Año Vehículo"></asp:Label>
            <asp:TextBox ID="txtAnioVehiculo" runat="server" CssClass="form-control input-sm" Enabled="False"></asp:TextBox>
        </div>
        <div class="form-group col-md-6">
            <asp:Label ID="lblTarjetaCirculacion" runat="server" Text="No. Tarjeta Circulación"></asp:Label>
            <asp:TextBox ID="txtNumeroTarjetaCirculacion" runat="server" class="form-control" MaxLength="10"></asp:TextBox>
        </div>
        <div class="form-group col-md-6">
            <asp:Label ID="lblNumeroChasis" runat="server" Text="No. de chasis"></asp:Label>
            <asp:TextBox ID="txtNumeroChasis" runat="server" class="form-control" MaxLength="20" Style="text-transform: uppercase"></asp:TextBox>
        </div>
        <div class="form-group col-md-6">
            <label>Placa :</label>
            <div class="form-inline">
                <div class="form-group">
                    <asp:DropDownList ID="ddlTipoPlaca" runat="server" class="form-control">
                        <asp:ListItem Value="P">P</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtNumeroPlaca" runat="server" Width="60px" MaxLength="3" class="form-control" onkeypress="return IsNumeric(event);"></asp:TextBox>
                    <asp:TextBox ID="txtCorrelativoPlaca" runat="server" Width="60px" MaxLength="3" class="form-control" Style="text-transform: uppercase"></asp:TextBox>
                </div>
            </div>
        </div>
        <div class="form-group col-md-6">
            <asp:Label ID="lblCilindraje" runat="server" Text="Cilindraje"></asp:Label>
            <asp:TextBox ID="txtCilindraje" runat="server" class="form-control" MaxLength="25"></asp:TextBox>
        </div>
        <div class="row"></div>
        <div class="form-group col-md-6">
            <asp:Label ID="lblTonelaje" runat="server" Text="Tonelaje"></asp:Label>
            <asp:TextBox ID="txtTonelaje" runat="server" onkeypress="return IsNumeric(event);" class="form-control" MaxLength="4"></asp:TextBox>
        </div>
        <div class="form-group col-md-6">
            <asp:Label ID="lblcolor" runat="server" Text="Color"></asp:Label>
            <asp:TextBox ID="txtColor" runat="server" class="form-control" MaxLength="30"></asp:TextBox>
        </div>
        <div class="form-group col-md-6">
            <asp:Label ID="lblMotor" runat="server" Text="No. de motor"></asp:Label>
            <asp:TextBox ID="txtNumeroMotor" runat="server" class="form-control" MaxLength="25" Style="text-transform: uppercase"></asp:TextBox>
        </div>
        <div class="form-group col-md-6">
            <asp:Label ID="lblNumeroInspeccion" runat="server" Text="No. de inspección" Visible="true"></asp:Label>
            <asp:TextBox ID="txtNumeroInspeccion" onkeypress="return IsNumeric(event);" runat="server" class="form-control" MaxLength="10" Visible="true" Text="0"></asp:TextBox>
        </div>
        <div class="form-group col-md-12">
            <asp:Label ID="lblCoenentariosInspeccion" runat="server" Text="Comentarios inspección"></asp:Label>
            <asp:TextBox ID="txtComentariosInspeccion" runat="server" class="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="form-group col-md-3">
            <asp:Button ID="btnGuardarDatosVehiculo" runat="server" Text="Guardar Información" class="btn btn-danger" OnClick="btnGuardarDatosVehiculo_Click1" />
        </div>

        <div class="form-group col-md-3">
            <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger btn-block" OnClick="btnCancelar_Click"><i class="glyphicon glyphicon glyphicon-arrow-left"></i> Regresar</asp:LinkButton>
        </div>

        <div class="row"></div>
        <div class="col-md-8 col-md-offset-2" style="text-align: center">
            <div class="form-group col-md-12">
                <span id="error" style="color: Red; display: none">* Solo se permiten números (0 - 9)</span>
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
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-75674732-1', 'auto');
        ga('send', 'pageview');

    </script>
</asp:Content>
