<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="cotizar.aspx.cs" Inherits="CotizadorWebAutos.Personas.GastosMedicosIndividual.cotizar" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="precios.css" rel="stylesheet" />

    <style>
        div > img {
            width: auto;
            height: initial;
            max-height: 100%;
            max-width: 100%;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid" style="background-color:white">
        <br />

        <div class="col-lg-12" style="border: none">

            <div class="col-lg-8 col-lg-offset-2" style="border: none">
                <asp:Image ID="imagenProducto" runat="server" ImageUrl="~/Recursos/imagenes/animacion_medical.gif" />
                <br />
                <br />
            </div>

            <%-- ASEGURADO TITULAR --%>
            <div class="col-lg-8 col-lg-offset-2" style="background-color: whitesmoke">
                <asp:Panel runat="server" ID="pnlAseguradoTitular" GroupingText="Asegurado Titular">
                    <form>
                        <%-- NOMBRES Y APELLIDOS --%>
                        <div class="form-group-sm col-lg-12">
                            <label for="txtNombresTitular">Nombres y Apellidos</label>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:TextBox runat="server" ID="txtNombresTitular" CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                        <%-- GENERO --%>
                        <div class="form-group-sm col-lg-6">
                            <label for="cmbGeneroTitular">Género</label>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="fa fa-genderless"></i></span>
                                <asp:DropDownList ID="cmbGeneroTitular" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <%-- FECHA NACIMIENTO --%>
                        <div class="form-group-sm col-lg-6">
                            <label for="txtFechaNacTitular">Fecha Nacimiento</label>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                <asp:TextBox CssClass="form-control" ID="txtFechaNacTitular" runat="server"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calendarFecNacTitular" runat="server"
                                    TargetControlID="txtFechaNacTitular"
                                    Format="dd/MM/yyyy" />
                            </div>
                        </div>
                        <%-- ESTADO CIVIL --%>
                        <div class="form-group-sm col-lg-6">
                            <label for="cmbEstadoCivilTitular">Estado Civil</label>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-list-alt"></i></span>
                                <asp:DropDownList ID="cmbEstadoCivilTitular" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <%-- TELEFONO --%>
                        <div class="form-group-sm col-lg-6">
                            <label for="txtTelefonoTitular">Teléfono</label>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-earphone"></i></span>
                                <asp:TextBox runat="server" MaxLength="8" ID="txtTelefonoTitular" CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                        <%-- CORREO ELECTRONICO --%>
                        <div class="form-group-sm col-lg-6">
                            <label for="txtCorreoTitular">Correo electrónico</label>
                            <div class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
                                <asp:TextBox runat="server" TextMode="Email" ID="txtCorreoTitular" CssClass="form-control">
                                </asp:TextBox>
                            </div>
                        </div>
                        <%-- AGREGAR CONYUGE --%>
                        <div class="form-group-sm col-lg-6" style="border: none">
                            <label for="chkConyuge">Agregar cónyuge?</label>
                            <asp:CheckBox ID="chkConyuge" runat="server" AutoPostBack="true" OnCheckedChanged="chkConyuge_CheckedChanged" />
                        </div>
                        <%-- AGREGAR HIJOS --%>
                        <div class="form-group-sm col-lg-6" style="border: none">
                            <label for="btnAgregarHijos">Agregar hijo/a</label>
                            <asp:Button ToolTip="Hijos dependientes menores de 25 años." CssClass="btn btn-success btn-sm" ID="btnAgregarHijos" runat="server" Text="+" OnClick="btnAgregarHijos_Click" />
                        </div>

                    </form>
                </asp:Panel>
                <br />
            </div>

            <div class="row"></div>
            <br />

            <%-- DATOS CONYUGE --%>
            <div class="col-lg-8 col-lg-offset-2" style="background-color: whitesmoke">
                <asp:UpdatePanel ID="upnlll" runat="server">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="pnlDatosConyuge" Visible="false" GroupingText="Cónyuge">
                            <form>
                                <%-- NOMBRES Y APELLIDOS --%>
                                <div class="form-group-sm col-lg-12">
                                    <label for="txtNombresConyuge">Nombres y Apellidos</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                        <asp:TextBox runat="server" ID="txtNombresConyuge" CssClass="form-control">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <%-- GENERO --%>
                                <div class="form-group-sm col-lg-6">
                                    <label for="cmbGeneroConyuge">Género</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="fa fa-genderless"></i></span>
                                        <asp:DropDownList ID="cmbGeneroConyuge" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%-- FECHA NACIMIENTO --%>
                                <div class="form-group-sm col-lg-6">
                                    <label for="txtFechaNacConyuge">Fecha Nacimiento</label>
                                    <div class="input-group">
                                        <span class="input-group-addon"><i class="glyphicon glyphicon-calendar"></i></span>
                                        <asp:TextBox CssClass="form-control" ID="txtFechaNacConyuge" runat="server"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calendarFecNacConyuge" runat="server"
                                            TargetControlID="txtFechaNacConyuge"
                                            Format="dd/MM/yyyy" />
                                    </div>
                                    <br />
                                </div>

                            </form>

                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkConyuge" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>

            <%-- DATOS HIJOS --%>
            <div class="col-lg-8 col-lg-offset-2" style="background-color: whitesmoke">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="pnlHIjos" Visible="false" GroupingText="Hijos/as">
                            <form>
                                <%--HIJOS --%>
                                <div class="form-group col-md-12">
                                    <asp:DataList runat="server" DataKeyField="id_hijo" ID="dtlHijos" RepeatDirection="Horizontal" OnDeleteCommand="dtlHijos_DeleteCommand">
                                        <ItemTemplate>
                                            <asp:LinkButton BackColor="WhiteSmoke" ID="btnRegresar" CommandName="delete" runat="server" CssClass="btn btn-link"><i class="glyphicon glyphicon-remove"></i>
                                            </asp:LinkButton>
                                            <asp:Image runat="server" Height="55px" Width="55px" ImageUrl="~/Recursos/imagenes/icono_multisexo.png" />
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </form>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnAgregarHijos" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>

            <%-- SEGURO DE VIDA --%>
            <div class="col-lg-4 col-lg-offset-2" style="border: none">
                <asp:Panel runat="server" ID="pnlDatosConfiguracion" GroupingText="Seguro de vida">
                    <!--SEGURO VIDA-->
                    <div class="form-group" style="border: none">
                        <label for="cmbSumaAseguradaVida">Suma Asegurada</label>
                        <asp:DropDownList ID="cmbSumaAseguradaVida" runat="server" class="form-control">
                            <asp:ListItem>50000</asp:ListItem>
                            <asp:ListItem>100000</asp:ListItem>
                            <asp:ListItem>150000</asp:ListItem>
                            <asp:ListItem>200000</asp:ListItem>
                            <asp:ListItem>250000</asp:ListItem>
                            <asp:ListItem>300000</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
            </div>

            <%-- SEGURO DENTAL --%>
            <div class="col-lg-4" style="border: none">
                <asp:Panel runat="server" ID="pnlDatosPago" GroupingText="Coberturas Adicionales">
                    <form>
                        <div class="form-group-sm col-lg-6" style="border: none">
                            <label for="CheckBox1">Seguro dental?</label>
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                        </div>
                        <div class="form-group-sm col-lg-6" style="border: none">
                            <label for="CheckBox2">Maternidad?</label>
                            <asp:CheckBox ID="CheckBox2" runat="server" />
                        </div>
                    </form>
                </asp:Panel>
            </div>

            <div class="col-lg-4 col-lg-offset-4">
                <asp:Button CssClass="btn-block btn-danger" ID="btnCalcularPlanes" OnClick="btnCalcularPlanes_Click" runat="server" Text="Calcular Planes" />
                <br />
            </div>

            <div class="row"></div>

            <%-- LISTADO PLANES --%>
            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlPlanes" runat="server" Visible="false">
                            <div class="col-lg-2 col-lg-offset-1">
                                <div class="pricingTable">
                                    <div class="pricingTable-header">
                                        <h3 class="heading">Básico</h3>
                                        <span class="price-value">
                                            <span class="currency">Q</span> 10
                        <span class="month">mensual</span>
                                        </span>
                                    </div>
                                    <div class="pricing-content">
                                        <ul>
                                            <li>Territorio Cobertura Centro América</li>
                                            <li>Máximo Vitalicio  1,000,000</li>
                                            <li>Deducible en CA 750</li>
                                            <li>Trasplante de Órganos 500,000</li>
                                            <li>Reembolso en C.A 80%</li>
                                            <li>Servicio de Asistencia Si aplica</li>
                                        </ul>
                                        <asp:Button CssClass="read btn-block btn-danger" ID="Button5" runat="server" Text="Seleccionar" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-2">
                                <div class="pricingTable">
                                    <div class="pricingTable-header">
                                        <h3 class="heading">Vital</h3>
                                        <span class="price-value">
                                            <span class="currency">Q</span> 999.00
                        <span class="month">mensual</span>
                                        </span>
                                    </div>
                                    <div class="pricing-content">
                                        <ul>
                                            <li>Territorio Cobertura Centro América</li>
                                            <li>Máximo Vitalicio  2,000,000</li>
                                            <li>Deducible en CA 1,000</li>
                                            <li>Trasplante de Órganos 500,000</li>
                                            <li>Reembolso en C.A 80%</li>
                                            <li>Servicio de Asistencia Si aplica</li>
                                        </ul>
                                        <asp:Button CssClass="read btn-block btn-danger" ID="Button4" runat="server" Text="Seleccionar" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-2">
                                <div class="pricingTable">
                                    <div class="pricingTable-header">
                                        <h3 class="heading">Plus</h3>
                                        <span class="price-value">
                                            <span class="currency">Q</span> 3500.00
                        <span class="month">mensual</span>
                                        </span>
                                    </div>
                                    <div class="pricing-content">
                                        <ul>
                                            <li>Territorio Cobertura Mundial</li>
                                            <li>Máximo Vitalicio  3,000,000</li>
                                            <li>Deducible en CA 1,500</li>
                                            <li>Trasplante de Órganos 1,000,000</li>
                                            <li>Reembolso en C.A 80%</li>
                                            <li>Servicio de Asistencia Si aplica</li>
                                        </ul>
                                        <asp:Button CssClass="read btn-block btn-danger" ID="Button3" runat="server" Text="Seleccionar" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-2">
                                <div class="pricingTable">
                                    <div class="pricingTable-header">
                                        <h3 class="heading">Platino</h3>
                                        <span class="price-value">
                                            <span class="currency">Q</span> 4000.00
                        <span class="month">mensual</span>
                                        </span>
                                    </div>
                                    <div class="pricing-content">
                                        <ul>
                                            <li>Territorio Cobertura Mundial</li>
                                            <li>Máximo Vitalicio  6,000,000</li>
                                            <li>Deducible en CA 2,000</li>
                                            <li>Trasplante de Órganos 1,000,000</li>
                                            <li>Reembolso en C.A 80%</li>
                                            <li>Servicio de Asistencia Si aplica</li>
                                        </ul>
                                        <asp:Button CssClass="read btn-block btn-danger" ID="Button2" runat="server" Text="Seleccionar" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-lg-2">
                                <div class="pricingTable">
                                    <div class="pricingTable-header">
                                        <h3 class="heading">Diamante</h3>
                                        <span class="price-value">
                                            <span class="currency">Q</span>5000.00
                        <span class="month">mensual</span>
                                        </span>
                                    </div>
                                    <div class="pricing-content">
                                        <ul>
                                            <li>Territorio Cobertura Mundial</li>
                                            <li>Máximo Vitalicio  8,000,000</li>
                                            <li>Deducible en CA 2,000</li>
                                            <li>Trasplante de Órganos 1,000,000</li>
                                            <li>Reembolso en C.A 80%</li>
                                            <li>Servicio de Asistencia Si aplica</li>
                                        </ul>
                                        <asp:Button CssClass="read btn-block btn-danger" ID="btnSeleccionarDiamante" runat="server" Text="Seleccionar" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnCalcularPlanes" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>




            </div>

        </div>
    </div>
</asp:Content>
