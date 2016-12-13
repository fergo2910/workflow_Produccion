<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="CotizadorWebAutos.HogarSeguro.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <br />

    <div class="container">
        <%-- Informacion personal del cliente --%>
        <asp:Panel ID="pnlDatosPersonales" runat="server">
            <div class="col-md-8 col-md-offset-2" style="text-align: center; background-color: whitesmoke">

                <h3 style="text-align: center; text-shadow: 1px 1px 2px #aaa"><b>Ingresa la información para cotizar</b></h3>
                <hr style="border-color: black; border-width: 1.5px;" />

            </div>

            <br />

            <div class="col-md-8 col-md-offset-2" style="align-content: center; background-color: whitesmoke;">

                <div id="divPrimeraFilaFormulario" class="row">

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

                    <div class="form-group col-md-12">
                        <label>Dirección de la vivienda:</label>
                        <asp:TextBox ID="txtDireccionVivienda" runat="server" TextMode="MultiLine" CssClass="form-control input-sm"></asp:TextBox>
                    </div>

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <%--PAIS--%>
                            <div class="form-group col-md-4">
                                <label>País</label>
                                <asp:DropDownList ID="cmbPais" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="cmbPais_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <%--DEPARTAMENTO--%>
                            <div class="form-group col-md-4">
                                <label>Departamento</label>
                                <asp:DropDownList ID="cmbDepartamento" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="cmbDepartamento_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <%--MUNICIPIO--%>
                            <div class="form-group col-md-4">
                                <label>Municipio</label>
                                <asp:DropDownList ID="cmbMunicipio" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cmbPais" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                    <div class="form-group col-md-12" style="background-color: whitesmoke; text-align: center">
                        <hr style="border-color: black; border-width: 1.5px;" />
                        <asp:Button ID="btnMostrarPlanes" runat="server" Text="Mostrar Planes" OnClick="btnMostrarPlanes_Click" CssClass="btn btn-danger" />
                        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" CssClass="btn btn-danger" />
                        <span id="error" style="color: Red; display: none">&nbsp;* Solo se permiten números (0 - 9)</span>
                    </div>



                </div>

            </div>

        </asp:Panel>

        <div class="row"></div>
        <br />

        <%-- PLANES INQUILINOS --%>
        <asp:Panel ID="pnlPlanInquilino" runat="server" Visible="false">
            <div class="col-md-12">
                <asp:UpdatePanel ID="upnlInquilino" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <%-- Inquilino Bronce --%>
                        <div class="col-md-4 text-center">
                            <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #D3851D; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                                <div class="panel-heading" style="border-width: 2px; border-color: #D3851D;">
                                    <img src="../Recursos/imagenes/incendio-bronce.gif" height="200px" width="200px" />
                                    <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Bronce</b></h3>
                                </div>

                                <ul class="list-group text-center">
                                    <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #D3851D">RC <i class="glyphicon glyphicon-ok"></i></li>
                                </ul>
                                <div class="panel-footer">
                                    <button type="button" class="btn btn-success btn-block" data-toggle="modal" data-target="#infoInquilino1" style="font-size: large"><b>Cobertura</b></></button>
                                    <br />
                                    <asp:Label runat="server" Text="Seleccionar" Style="font-size: large" Font-Bold="true"></asp:Label>
                                    <br />
                                    <asp:RadioButton ID="rdInquilinoBronce" runat="server" OnCheckedChanged="rdInquilinoBronce_CheckedChanged" AutoPostBack="true" />                                    
                                </div>
                            </div>
                        </div>

                        <%-- Inquilino Plata --%>
                        <div class="col-md-4 text-center">
                            <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #414141; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                                <div class="panel-heading" style="border-width: 2px; border-color: #414141;">
                                    <img src="../Recursos/imagenes/incendio-plateado.gif" height="200px" width="200px" />
                                    <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Plata</b></h3>
                                </div>
                                <ul class="list-group text-center">
                                    <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #414141">RC <i class="glyphicon glyphicon-ok"></i></li>
                                </ul>
                                <div class="panel-footer">
                                    <button type="button" class="btn btn-success btn-block" data-toggle="modal" data-target="#infoInquilino2" style="font-size: large"><b>Cobertura</b></></button>
                                    <br />
                                    <asp:Label runat="server" Text="Seleccionar" Style="font-size: large" Font-Bold="true"></asp:Label>
                                    <br />
                                    <asp:RadioButton ID="rdInquilinoPlata" runat="server" OnCheckedChanged="rdInquilinoPlata_CheckedChanged" AutoPostBack="true" />                                    
                                </div>
                            </div>
                        </div>

                        <%-- Inquilino Oro --%>
                        <div class="col-md-4 text-center">
                            <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #F8DA8C; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                                <div class="panel-heading" style="border-width: 2px; border-color: #F8DA8C;">
                                    <img src="../Recursos/imagenes/incendio.gif" height="200px" width="200px" />
                                    <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Oro</b></h3>
                                </div>
                                <ul class="list-group text-center">
                                    <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #F8DA8C">RC <i class="glyphicon glyphicon-ok"></i></li>
                                </ul>
                                <div class="panel-footer">
                                    <button type="button" class="btn btn-success btn-block" data-toggle="modal" data-target="#infoInquilino3" style="font-size: large"><b>Cobertura</b></></button>
                                    <br />
                                    <asp:Label runat="server" Text="Seleccionar" Style="font-size: large" Font-Bold="true"></asp:Label>
                                    <br />
                                    <asp:RadioButton ID="rdInquilinoOro" runat="server" OnCheckedChanged="rdInquilinoOro_CheckedChanged" AutoPostBack="true" />                                    
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdPropietarioBronce" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </asp:Panel>

        <div class="row"></div>

        <%-- PLANES PROPIETARIOS --%>
        <asp:Panel ID="pnlPlanPropietario" runat="server" Visible="false">

            <div class="col-md-12">
                <asp:UpdatePanel ID="upnlPropietario" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>

                        <%-- Propietario Bronce --%>
                        <div class="col-md-4 text-center">
                            <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #D3851D; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                                <div class="panel-heading" style="border-width: 2px; border-color: #D3851D;">
                                    <img src="../Recursos/imagenes/incendio-bronce.gif" height="200px" width="200px" />
                                    <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Bronce</b></h3>
                                </div>
                                <ul class="list-group text-center">
                                    <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #D3851D">Rotura de maquinaria <i class="glyphicon glyphicon-ok"></i></li>
                                </ul>
                                <div class="panel-footer">
                                    <button type="button" class="btn btn-success btn-block" data-toggle="modal" data-target="#infoPropietario1" style="font-size: large"><b>Cobertura</b></></button>
                                    <br />
                                    <asp:Label runat="server" Text="Seleccionar" Style="font-size: large" Font-Bold="true"></asp:Label>
                                    <br />
                                    <asp:RadioButton ID="rdPropietarioBronce" runat="server" OnCheckedChanged="rdPropietarioBronce_CheckedChanged" AutoPostBack="true" />                                    
                                </div>
                            </div>
                        </div>

                        <%-- Propietario Bronce --%>
                        <div class="col-md-4 text-center">
                            <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #414141; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                                <div class="panel-heading" style="border-width: 2px; border-color: #414141;">
                                    <img src="../Recursos/imagenes/incendio-plateado.gif" height="200px" width="200px" />
                                    <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Plata</b></h3>
                                </div>
                                <ul class="list-group text-center">
                                    <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #414141">Rotura de maquinaria <i class="glyphicon glyphicon-ok"></i></li>
                                </ul>
                                <div class="panel-footer">
                                    <button type="button" class="btn btn-success btn-block" data-toggle="modal" data-target="#infoPropietario2" style="font-size: large"><b>Cobertura</b></></button>
                                    <br />
                                    <asp:Label runat="server" Text="Seleccionar" Style="font-size: large" Font-Bold="true"></asp:Label>
                                    <br />
                                    <asp:RadioButton ID="rdPropietarioPlata" runat="server" OnCheckedChanged="rdPropietarioPlata_CheckedChanged" AutoPostBack="true" />                                    
                                </div>
                            </div>
                        </div>

                        <%-- Propietario Bronce --%>
                        <div class="col-md-4 text-center">
                            <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #F8DA8C; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                                <div class="panel-heading" style="border-width: 2px; border-color: #F8DA8C;">
                                    <img src="../Recursos/imagenes/incendio.gif" height="200px" width="200px" />
                                    <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Oro</b></h3>
                                </div>
                                <ul class="list-group text-center">
                                    <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #F8DA8C">Rotura de maquinaria <i class="glyphicon glyphicon-ok"></i></li>
                                </ul>
                                <div class="panel-footer">
                                    <button type="button" class="btn btn-success btn-block" data-toggle="modal" data-target="#infoPropietario3" style="font-size: large"><b>Cobertura</b></></button>
                                    <br />
                                    <asp:Label runat="server" Text="Seleccionar" Style="font-size: large" Font-Bold="true"></asp:Label>
                                    <br />
                                    <asp:RadioButton ID="rdPropietarioOro" runat="server" OnCheckedChanged="rdPropietarioOro_CheckedChanged" AutoPostBack="true" />                                    
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdPropietarioBronce" EventName="CheckedChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

        </asp:Panel>

        <div class="row"></div>

        <%-- DETALLE PAGOS --%>
        <div class="col-md-12">
            <asp:UpdatePanel ID="upnlPagos" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnlPagos" Visible="false">

                        <div class="col-md-8 col-md-offset-2" style="text-align: center; background-color: whitesmoke">

                            <h3 style="text-align: center; text-shadow: 1px 1px 2px #aaa"><b>Información de pago:</b></h3>
                            <hr style="border-color: black; border-width: 1.5px;" />
                        </div>
                        <div class="col-md-8 col-md-offset-2" style="background-color: whitesmoke">

                            <!--DIV FORMA PAGO-->
                            <div id="divFormaPago" class="form-group col-md-6" style="background-color: whitesmoke;">
                                <asp:Label ID="lblFormaPago" runat="server" Text="Forma de pago:"></asp:Label>
                                <asp:DropDownList ID="ddlFormaPago" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <!--DIV NUMERO PAGOS-->
                            <div id="divNumeroPagos" class="form-group col-md-6" style="background-color: whitesmoke;">
                                <asp:Label ID="lblNumeroPagos" runat="server" Text="Número de pagos:"></asp:Label>
                                <asp:DropDownList ID="ddlNumeroPagos" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4 col-md-offset-4">
                                <asp:Button ID="btnCotizar" Font-Bold="True" runat="server" Text="Cotizar!" CssClass="btn btn-danger btn-block" Style="font-size: large" OnClick="btnCotizar_Click" />
                            </div>

                            <div class="col-md-4 col-md-offset-4">
                                <asp:Button ID="btnGuardarCotizacion" Visible="false" runat="server" Text="Guardar Cotizacion" CssClass="btn btn-danger btn-block"  OnClick="btnGuardarCotizacion_Click" />
                            </div>

                            <%-- Detalle de Pagos --%>
                            <div class="form-group col-md-6">
                                <h2>
                                    <asp:Label ID="lblResumenPagosVisaCuotas" runat="server" Text="Resumen Pagos Cuotas T.C." Visible="false"></asp:Label></h2>
                                <asp:GridView ID="gvInformacionPlanesCuotas" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="CUOTAS" HeaderText="# Pagos" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Monto" HeaderText="Total Cuota" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CUOTA" HeaderText="Total Cotización Completa" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle BackColor="#DF0000" ForeColor="White" />
                                </asp:GridView>
                            </div>

                            <div class="form-group col-md-6">
                                <h2>
                                    <asp:Label ID="lblResumenPagosFraccionados" runat="server" Text="Resumen Pagos Fraccionados" Visible="false"></asp:Label></h2>
                                <asp:GridView ID="gvInformacionPlanesFraccionado" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="CUOTAS" HeaderText="# Pagos" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Monto" HeaderText="Total Cuota" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CUOTA" HeaderText="Total Cotización Completa" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle BackColor="#DF0000" ForeColor="White" />
                                </asp:GridView>
                            </div>

                        </div>

                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnCotizar" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <!-- Informacion detallada de los planes Propietarios -->

        <div class="modal fade" id="infoPropietario1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content" style='width: 75%'>
                    <div class="modal-header" style="background-color: #EEBF80">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="text-align: center; color: black"><b>Plan Propietario 1</b></h4>
                    </div>
                    <div class="modal-body">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>COBERTURA</th>
                                    <th>SUMA ASEGURADA</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr style="background-color: #F8E6CD">
                                    <td>EDIFICIO</td>
                                    <td>
                                        <b>Q250,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>MENAJE DE HOGAR ( ELECTRODOMESTICOS Y EE )</td>
                                    <td>
                                        <b>Q100,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>MEJORAS LOCATIVAS</td>
                                    <td>
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>JOYAS Y OBJETOS DE ARTE</td>
                                    <td>
                                        <b>Q10,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>PRENDAS DE VESTIR</td>
                                    <td>
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>GASTOS EXTRAS</td>
                                    <td>
                                        <b>Q35,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>REMOCION DE ESCOMBROS</td>
                                    <td>
                                        <b>Q35,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>RC FAMILIAR</td>
                                    <td>
                                        <b>Q200,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>RC ARRENDATARIO</td>
                                    <td>
                                        <b>Q200,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>RC TRABAJADORES DOMESTICOS</td>
                                    <td>
                                        <b>Q10,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>ROBO DE MENAJE</td>
                                    <td>
                                        <b>Q30,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>ROBO DE JOYAS</td>
                                    <td>
                                        <b>Q5,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>DINERO Y VALORES</td>
                                    <td>
                                        <b>Q1,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>CRISTALES</td>
                                    <td>
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>INMUEBLE</td>
                                    <td>
                                        <b>Q250.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>EQUIPO ELECTRODOMESTICO</td>
                                    <td>
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>HURTO PERSONAL</td>
                                    <td>
                                        <b>Q500.00 </b>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="infoPropietario2" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content" style='width: 75%'>
                    <div class="modal-header" style="background-color: #D2D2D2;">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="text-align: center"><b>Plan Propietario 2</b></h4>
                    </div>
                    <div class="modal-body">
                        <table class='table'>
                            <thead>
                                <tr>
                                    <th>COBERTURA</th>
                                    <th>SUMA ASEGURADA</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr style="background-color: #F3F3F3;">
                                    <td>EDIFICIO</td>
                                    <td>
                                        <b>Q500,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>MENAJE DE HOGAR ( ELECTRODOMESTICOS Y EE )</td>
                                    <td>
                                        <b>Q200,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>MEJORAS LOCATIVAS</td>
                                    <td>
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>JOYAS Y OBJETOS DE ARTE</td>
                                    <td>
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>PRENDAS DE VESTIR</td>
                                    <td>
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>GASTOS EXTRAS</td>
                                    <td>
                                        <b>Q70,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>REMOCION DE ESCOMBROS</td>
                                    <td>
                                        <b>Q70,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>RC FAMILIAR</td>
                                    <td>
                                        <b>Q400,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>RC ARRENDATARIO</td>
                                    <td>
                                        <b>Q400,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>RC TRABAJADORES DOMESTICOS</td>
                                    <td>
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>ROBO DE MENAJE</td>
                                    <td>
                                        <b>Q60,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>ROBO DE JOYAS</td>
                                    <td>
                                        <b>Q10,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>DINERO Y VALORES</td>
                                    <td>
                                        <b>Q2,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>CRISTALES</td>
                                    <td>
                                        <b>Q40,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>INMUEBLE</td>
                                    <td>
                                        <b>Q500.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>EQUIPO ELECTRODOMESTICO</td>
                                    <td>
                                        <b>Q40,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>HURTO PERSONAL</td>
                                    <td>
                                        <b>Q1000.00 </b>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="infoPropietario3" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content" style='width: 75%'>
                    <div class="modal-header" style="background-color: #F8DA8C">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="text-align: center"><b>Plan Propietario 3</b></h4>
                    </div>
                    <div class="modal-body">
                        <table class='table'>
                            <thead>
                                <tr>
                                    <th>COBERTURA</th>
                                    <th>SUMA ASEGURADA</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr style="background-color: #FDF2D7">
                                    <td>EDIFICIO</td>
                                    <td>
                                        <b>Q1,000,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>MENAJE DE HOGAR ( ELECTRODOMESTICOS Y EE )</td>
                                    <td>
                                        <b>Q400,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>MEJORAS LOCATIVAS</td>
                                    <td>
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>JOYAS Y OBJETOS DE ARTE</td>
                                    <td>
                                        <b>Q40,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>PRENDAS DE VESTIR</td>
                                    <td>
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>GASTOS EXTRAS</td>
                                    <td>
                                        <b>Q140,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>REMOCION DE ESCOMBROS</td>
                                    <td>
                                        <b>Q140,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>RC FAMILIAR</td>
                                    <td>
                                        <b>Q800,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>RC ARRENDATARIO</td>
                                    <td>
                                        <b>Q800,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>RC TRABAJADORES DOMESTICOS</td>
                                    <td>
                                        <b>Q40,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>ROBO DE MENAJE</td>
                                    <td>
                                        <b>Q120,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>ROBO DE JOYAS</td>
                                    <td>
                                        <b>Q15,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>DINERO Y VALORES</td>
                                    <td>
                                        <b>Q4,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>CRISTALES</td>
                                    <td>
                                        <b>Q80,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>INMUEBLE</td>
                                    <td>
                                        <b>Q1,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>EQUIPO ELECTRODOMESTICO</td>
                                    <td>
                                        <b>Q80,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>HURTO PERSONAL</td>
                                    <td>
                                        <b>Q2,000.00 </b>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
        </div>

        <!-- Informacion detallada de los planes Inquilinos -->

        <div class="modal fade" id="infoInquilino1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content" style='width: 75%'>
                    <div class="modal-header" style="background-color: #EEBF80">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="text-align: center; color: black"><b>Plan Inquilino 1</b></h4>
                    </div>


                    <div class="modal-body">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>COBERTURA</th>
                                    <th>SUMA ASEGURADA</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr style="background-color: #F8E6CD">
                                    <td>RC FAMILIAR</td>
                                    <td>
                                        <b>Q250,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>RC ARRENDATARIO</td>
                                    <td>
                                        <b>Q250,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>RC TRABAJADORES DOMESTICOS</td>
                                    <td>
                                        <b>Q250,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>MENAJE DE HOGAR ( ELECTRODOMESTICOS Y EE )</td>
                                    <td>
                                        <b>Q100,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>MEJORAS LOCATIVAS</td>
                                    <td>
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>JOYAS Y OBJETOS DE ARTE</td>
                                    <td>
                                        <b>Q10,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>PRENDAS DE VESTIR</td>
                                    <td>
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>GASTOS EXTRAS</td>
                                    <td>
                                        <b>Q35,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>REMOCION DE ESCOMBROS</td>
                                    <td>
                                        <b>Q35,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>RC FAMILIAR</td>
                                    <td>
                                        <b>Q200,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>RC ARRENDATARIO</td>
                                    <td>
                                        <b>Q200,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>RC TRABAJADORES DOMESTICOS</td>
                                    <td>
                                        <b>Q10,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>ROBO DE MENAJE</td>
                                    <td>
                                        <b>Q30,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>ROBO DE JOYAS</td>
                                    <td>
                                        <b>Q5,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>DINERO Y VALORES</td>
                                    <td>
                                        <b>Q1,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>CRISTALES</td>
                                    <td>
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>INMUEBLE</td>
                                    <td>
                                        <b>Q250.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>EQUIPO ELECTRODOMESTICO</td>
                                    <td>
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F8E6CD">
                                    <td>HURTO PERSONAL</td>
                                    <td>
                                        <b>Q500.00 </b>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>



                </div>

            </div>
        </div>

        <div class="modal fade" id="infoInquilino2" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content" style='width: 75%'>
                    <div class="modal-header" style="background-color: #D2D2D2;">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="text-align: center"><b>Plan Inquilino 2</b></h4>
                    </div>
                    <div class="modal-body">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>COBERTURA</th>
                                    <th>SUMA ASEGURADA</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr style="background-color: #F3F3F3;">
                                    <td>RC FAMILIAR</td>
                                    <td>
                                        <b>Q250,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>RC ARRENDATARIO</td>
                                    <td>
                                        <b>Q250,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>RC TRABAJADORES DOMESTICOS</td>
                                    <td>
                                        <b>Q250,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>MENAJE DE HOGAR ( ELECTRODOMESTICOS Y EE )</td>
                                    <td>
                                        <b>Q100,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>MEJORAS LOCATIVAS</td>
                                    <td>
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>JOYAS Y OBJETOS DE ARTE</td>
                                    <td>
                                        <b>Q10,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>PRENDAS DE VESTIR</td>
                                    <td>
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>GASTOS EXTRAS</td>
                                    <td>
                                        <b>Q35,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>REMOCION DE ESCOMBROS</td>
                                    <td>
                                        <b>Q35,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>RC FAMILIAR</td>
                                    <td>
                                        <b>Q200,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>RC ARRENDATARIO</td>
                                    <td>
                                        <b>Q200,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>RC TRABAJADORES DOMESTICOS</td>
                                    <td>
                                        <b>Q10,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>ROBO DE MENAJE</td>
                                    <td>
                                        <b>Q30,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>ROBO DE JOYAS</td>
                                    <td>
                                        <b>Q5,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>DINERO Y VALORES</td>
                                    <td>
                                        <b>Q1,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>CRISTALES</td>
                                    <td>
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>INMUEBLE</td>
                                    <td>
                                        <b>Q250.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>EQUIPO ELECTRODOMESTICO</td>
                                    <td>
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #F3F3F3;">
                                    <td>HURTO PERSONAL</td>
                                    <td>
                                        <b>Q500.00 </b>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="infoInquilino3" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content" style='width: 75%'>
                    <div class="modal-header" style="background-color: #F8DA8C">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="text-align: center"><b>Plan Inquilino 3</b></h4>
                    </div>
                    <div class="modal-body">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>COBERTURA</th>
                                    <th>SUMA ASEGURADA</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr style="background-color: #FDF2D7">
                                    <td>RC FAMILIAR</td>
                                    <td>
                                        <b>Q250,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>RC ARRENDATARIO</td>
                                    <td>
                                        <b>Q250,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>RC TRABAJADORES DOMESTICOS</td>
                                    <td>
                                        <b>Q250,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>MENAJE DE HOGAR ( ELECTRODOMESTICOS Y EE )</td>
                                    <td>
                                        <b>Q100,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>MEJORAS LOCATIVAS</td>
                                    <td>
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>JOYAS Y OBJETOS DE ARTE</td>
                                    <td>
                                        <b>Q10,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>PRENDAS DE VESTIR</td>
                                    <td>
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>GASTOS EXTRAS</td>
                                    <td>
                                        <b>Q35,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>REMOCION DE ESCOMBROS</td>
                                    <td>
                                        <b>Q35,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>RC FAMILIAR</td>
                                    <td>
                                        <b>Q200,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>RC ARRENDATARIO</td>
                                    <td>
                                        <b>Q200,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>RC TRABAJADORES DOMESTICOS</td>
                                    <td>
                                        <b>Q10,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>ROBO DE MENAJE</td>
                                    <td>
                                        <b>Q30,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>ROBO DE JOYAS</td>
                                    <td>
                                        <b>Q5,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>DINERO Y VALORES</td>
                                    <td>
                                        <b>Q1,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>CRISTALES</td>
                                    <td>
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>INMUEBLE</td>
                                    <td>
                                        <b>Q250.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>EQUIPO ELECTRODOMESTICO</td>
                                    <td>
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #FDF2D7">
                                    <td>HURTO PERSONAL</td>
                                    <td>
                                        <b>Q500.00 </b>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
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

</asp:Content>

