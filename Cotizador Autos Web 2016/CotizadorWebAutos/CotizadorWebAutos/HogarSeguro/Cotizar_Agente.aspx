<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Cotizar_Agente.aspx.cs" Inherits="CotizadorWebAutos.HogarSeguro.Cotizar_Agente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <%-- Informacion personal del cliente --%>
    <asp:Panel ID="pnlDatosPersonales" runat="server">
        <div class="container">

            <div class="col-md-8" style="align-content: center; background-color: whitesmoke;">

                <div id="divPrimeraFilaFormulario" class="row">
                    <!--DIV FORMA PAGO-->
                    <div id="divFormaPago" class="form-group col-md-6" style="background-color: whitesmoke;">
                        <asp:Label ID="lblFormaPago" runat="server" Text="Forma de pago:"></asp:Label>
                        <asp:DropDownList ID="ddlFormaPago" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <!--DIV PLANES-->
                    <div id="divNumeroPagos" class="form-group col-md-6" style="background-color: whitesmoke;">
                        <asp:Label ID="lblNumeroPagos" runat="server" Text="Numero de pagos:"></asp:Label>
                        <asp:DropDownList ID="ddlNumeroPagos" runat="server" class="form-control">
                        </asp:DropDownList>
                    </div>

                    <div class="form-group col-md-6" style="background-color: whitesmoke;">
                        <div class="input-group input-group-lg">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-user" aria-hidden="true"></span></span>
                            <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control text-danger" placeholder="Nombre y Apellido *">     
                            </asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group col-md-6" style="background-color: whitesmoke">
                        <div class="input-group input-group-lg">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-envelope" aria-hidden="true"></span></span>
                            <asp:TextBox ID="txtCorreoCliente" runat="server" CssClass="form-control" placeholder="Correo Electrónico *" TextMode="Email"></asp:TextBox>
                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="form-group col-xs- 12 col-sm-8 col-md-6" style="background-color: whitesmoke">
                        <div class="input-group input-group-lg">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-earphone" aria-hidden="true"></span></span>
                            <asp:TextBox ID="txtTelefonoCliente" runat="server" CssClass="form-control" MaxLength="8" placeholder="Teléfono" onkeypress="return IsNumeric(event);"></asp:TextBox>
                        </div>


                    </div>



                    <div class="form-group col-xs- 12 col-sm-4 col-md-5;" style="border: 0px; background-color: whitesmoke">
                        <asp:RadioButtonList ID="rbtnTipoBien" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>Propietario &nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem>Inquilino</asp:ListItem>
                        </asp:RadioButtonList>

                    </div>

                    <div class="form-group col-xs- 12 col-sm-8 col-md-5" style="background-color: whitesmoke">
                        <asp:CheckBox ID="chkAgente" runat="server" Checked="true" Enabled="false" AutoPostBack="true" Text="&nbsp; Es Agente?" />
                        <asp:TextBox ID="txtCodigoAgente" runat="server" Enabled="false" MaxLength="6" placeholder="Código Agente" onkeypress="return IsNumeric(event);"> </asp:TextBox>




                    </div>

                    <div class="form-group  col-xs- 12 col-sm-12 col-md-8" style="background-color: whitesmoke">
                        <asp:Button ID="btnPersonalesSiguiente" runat="server" Text="Mostrar Planes" OnClick="btnPersonalesSiguiente_Click" CssClass="btn btn-danger" />
                        <asp:Button ID="btnRegresarCotizacion" runat="server" Text="Regresar" OnClick="btnRegresarCotizacion_Click" CssClass="btn btn-danger" />
                        <span id="error" style="color: Red; display: none">&nbsp;* Solo se permiten números (0 - 9)</span>
                    </div>



                </div>

            </div>

        </div>


    </asp:Panel>

</asp:Content>
