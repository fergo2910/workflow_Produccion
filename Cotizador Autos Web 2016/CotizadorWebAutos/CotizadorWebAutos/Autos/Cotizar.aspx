<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Cotizar.aspx.cs" Inherits="CotizadorWebAutos.Autos.Cotizar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <div class="col-lg-12" style="border: none">

            <%-- DATOS DEL CLIENTE --%>
            <div class="col-lg-6" style="border: none">

                <asp:Panel runat="server" ID="pnlDatoscliente" GroupingText="Cliente">
                    <form>
                        <div class="form-group-sm col-lg-12">
                            <label for="txtNombreCliente">Nombre</label>
                            <asp:TextBox runat="server" ID="txtNombreCliente" CssClass="form-control">
                            </asp:TextBox>
                        </div>
                        <div class="form-group-sm col-lg-6">
                            <label for="txtTelefonoCliente">Teléfono</label>
                            <asp:TextBox runat="server" ID="txtTelefonoCliente" CssClass="form-control">
                            </asp:TextBox>
                        </div>
                        <div class="form-group-sm col-lg-6">
                            <label for="txtCorreoCliente">Correo electrónico</label>
                            <asp:TextBox runat="server" ID="txtCorreoCliente" CssClass="form-control">
                            </asp:TextBox>
                        </div>
                    </form>
                </asp:Panel>

            </div>

            <%-- DATOS GENERALES DEL VEHICULO --%>
            <div class="col-lg-6" style="border: none">
                <asp:Panel runat="server" ID="pnlDatosVehiculo" GroupingText="Vehiculo">
                    <form>

                        <!--TIPO VEHICULO-->
                        <div class="form-group-sm col-lg-4">
                            <label for="cmbTipoVehiculo">Tipo vehículo</label>
                            <asp:DropDownList ID="cmbTipoVehiculo" runat="server" class="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <!--MARCA VEHICULO-->
                        <div class="form-group-sm col-lg-4">
                            <label for="cmbMarcaVehiculo">Marca</label>
                            <asp:DropDownList ID="cmbMarcaVehiculo" runat="server" class="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <!--LINEA VEHICULO-->
                        <div class="form-group-sm col-lg-4">
                            <label for="cmbLineaVehiculo">Linea</label>
                            <asp:DropDownList ID="cmbLineaVehiculo" runat="server" class="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <!--AÑO VEHICULO-->
                        <div class="form-group-sm col-lg-6">
                            <label for="cmbAnioVehiculo">Año</label>
                            <asp:DropDownList ID="cmbAnioVehiculo" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>

                        <!--DIV ASIENTOS VEHICULO-->
                        <div class="form-group-sm col-lg-6">
                            <label for="txtAsientosVehiculo">Asientos</label>
                            <asp:TextBox ID="txtAsientosVehiculo" runat="server" class="form-control" onkeypress="return IsNumeric(event);"></asp:TextBox>
                        </div>

                    </form>
                </asp:Panel>
            </div>

            <div class="col-lg-6" style="border: none">
                <asp:Panel runat="server" ID="pnlDatosConfiguracion" GroupingText="Configuración">
                    <form>
                       <!--LESIONES A OCUPANTES-->
                        <div class="form-group-sm col-lg-6">
                            <label for="cmbLesionesOcupantes">Lesiones a ocupantes</label>
                            <asp:DropDownList ID="cmbLesionesOcupantes" runat="server" class="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <!--LIMITE RC-->
                        <div class="form-group-sm col-lg-6">
                            <label for="cmbLimiteRC">Límite RC</label>
                            <asp:DropDownList ID="cmbLimiteRC" runat="server" class="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        
                    </form>
                </asp:Panel>
            </div>

            <div class="col-lg-6" style="border: none">
                <asp:Panel runat="server" ID="pnlDatosPago" GroupingText="Pago">
                    <form>
                        <!--TIPO VEHICULO-->
                        <div class="form-group-sm col-lg-6">
                            <label for="cmbFormaPago">Forma de pago</label>
                            <asp:DropDownList ID="cmbFormaPago" runat="server" class="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <!--MARCA VEHICULO-->
                        <div class="form-group-sm col-lg-6">
                            <label for="cmbNumeroPagos">Número pagos</label>
                            <asp:DropDownList ID="cmbNumeroPagos" runat="server" class="form-control" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </form>
                </asp:Panel>
            </div>

        </div>

        <div class="row"></div>

        <div class="col-lg-12">
        </div>



    </div>

</asp:Content>
