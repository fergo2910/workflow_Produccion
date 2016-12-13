<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Cotizar.aspx.cs" Inherits="CotizadorWebAutos.HogarSeguro.Cotizar1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <br />
     <style type="text/css">
         .overlay {
             position: fixed;
             z-index: 98;
             top: 0px;
             left: 0px;
             right: 0px;
             bottom: 0px;
             background-color: #ffcccc;
             filter: alpha(opacity=80);
             opacity: 0.8;
         }

         .overlayContent {
             z-index: 99;
             margin: 250px auto;
             width: 200px;
             height: 200px;
         }

             .overlayContent h2 {
                 font-size: 35px;
                 font-weight: bold;
                 color: #000000;
             }

             .overlayContent img {
                 width: 80px;
                 height: 80px;
             }
     </style>
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


                            <div class="form-group col-md-3">
                                <label>País</label>
                                <asp:DropDownList ID="cmbPais" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="cmbPais_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-md-3">
                                <label>Departamento</label>
                                <asp:DropDownList ID="cmbDepartamento" runat="server" CssClass="form-control input-sm"
                                    OnSelectedIndexChanged="cmbDepartamento_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-md-3">
                                <label>Municipio</label>
                                <asp:DropDownList ID="cmbMunicipio" runat="server" CssClass="form-control input-sm"
                                     OnSelectedIndexChanged="cmbMunicipio_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                            <div class="form-group col-md-3">
                                <label>Zona</label>
                                <asp:DropDownList ID="cmbZona" runat="server" CssClass="form-control input-sm" ></asp:DropDownList>
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cmbPais" EventName="SelectedIndexChanged" />
                            
                        </Triggers>
                    </asp:UpdatePanel>

                    <div class="form-group col-md-12" style="background-color: whitesmoke; text-align: center">
                        <hr style="border-color: black; border-width: 1.5px;" />
                        <asp:Button ID="btnMostrarPlanes" runat="server" Text="Mostrar Planes" OnClick="btnMostrarPlanes_Click" CssClass="btn btn-danger" />
                        <asp:LinkButton ID="btnRegresar" runat="server" CssClass="btn btn-danger" OnClick="btnRegresar_Click">
                        <i class="glyphicon glyphicon glyphicon-arrow-left"></i> Regresar
                        </asp:LinkButton>
                        <%--<asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" CssClass="btn btn-danger" />--%>
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

                        <%-- Inquilino ESMERALDA --%>
                        <div class="col-md-4 text-center">
                            <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #287233; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                                <div class="panel-heading" style="border-width: 2px; border-color: #287233;">
                                    <img src="../Recursos/imagenes/Esmeralda.gif" height="200px" width="200px" />
                                    <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Esmeralda</b></h3>
                                </div>

                                <ul class="list-group text-center">
                                    <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #287233">RC <i class="glyphicon glyphicon-ok"></i></li>
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

                        <%-- Inquilino RUBI --%>
                        <div class="col-md-4 text-center">
                            <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #9b111e; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                                <div class="panel-heading" style="border-width: 2px; border-color: #9b111e;">
                                    <img src="../Recursos/imagenes/Ruby.gif" height="200px" width="200px" />
                                    <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Rubí</b></h3>
                                </div>
                                <ul class="list-group text-center">
                                    <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #9b111e">RC <i class="glyphicon glyphicon-ok"></i></li>
                                </ul>
                                <div class="panel-footer">
                                    <button type="button" class="btn btn-danger btn-block" data-toggle="modal" data-target="#infoInquilino2" style="font-size: large"><b>Cobertura</b></></button>
                                    <br />
                                    <asp:Label runat="server" Text="Seleccionar" Style="font-size: large" Font-Bold="true"></asp:Label>
                                    <br />
                                    <asp:RadioButton ID="rdInquilinoPlata" runat="server" OnCheckedChanged="rdInquilinoPlata_CheckedChanged" AutoPostBack="true" />
                                </div>
                            </div>
                        </div>

                        <%-- Inquilino DIAMANTE --%>
                        <div class="col-md-4 text-center">
                            <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #337ab7; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                                <div class="panel-heading" style="border-width: 2px; border-color: #337ab7;">
                                    <img src="../Recursos/imagenes/Diamante.gif" height="200px" width="200px" />
                                    <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Diamante</b></h3>
                                </div>
                                <ul class="list-group text-center">
                                    <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #337ab7">RC <i class="glyphicon glyphicon-ok"></i></li>
                                </ul>
                                <div class="panel-footer">
                                    <button type="button" class="btn btn-primary btn-block" data-toggle="modal" data-target="#infoInquilino3" style="font-size: large"><b>Cobertura</b></></button>
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

                        <%-- Propietario ESMERALDA --%>
                        <div class="col-md-4 text-center">
                            <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #287233; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                                <div class="panel-heading" style="border-width: 2px; border-color: #287233;">
                                    <img src="../Recursos/imagenes/Esmeralda.gif" height="200px" width="200px" />
                                    <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Esmeralda</b></h3>
                                </div>
                                <ul class="list-group text-center">
                                    <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #287233">Rotura de maquinaria <i class="glyphicon glyphicon-ok"></i></li>
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

                        <%-- Propietario RUBI --%>
                        <div class="col-md-4 text-center">
                            <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #9b111e; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                                <div class="panel-heading" style="border-width: 2px; border-color: #9b111e;">
                                    <img src="../Recursos/imagenes/Ruby.gif" height="200px" width="200px" />
                                    <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Rubí</b></h3>
                                </div>
                                <ul class="list-group text-center">
                                    <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #9b111e">Rotura de maquinaria <i class="glyphicon glyphicon-ok"></i></li>
                                </ul>
                                <div class="panel-footer">
                                    <button type="button" class="btn btn-danger btn-block" data-toggle="modal" data-target="#infoPropietario2" style="font-size: large"><b>Cobertura</b></></button>
                                    <br />
                                    <asp:Label runat="server" Text="Seleccionar" Style="font-size: large" Font-Bold="true"></asp:Label>
                                    <br />
                                    <asp:RadioButton ID="rdPropietarioPlata" runat="server" OnCheckedChanged="rdPropietarioPlata_CheckedChanged" AutoPostBack="true" />
                                </div>
                            </div>
                        </div>

                        <%-- Propietario DIAMANTE --%>
                        <div class="col-md-4 text-center">
                            <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #337ab7; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                                <div class="panel-heading" style="border-width: 2px; border-color: #337ab7;">
                                    <img src="../Recursos/imagenes/Diamante.gif" height="200px" width="200px" />
                                    <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Diamante</b></h3>
                                </div>
                                <ul class="list-group text-center">
                                    <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                                    <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #337ab7">Rotura de maquinaria <i class="glyphicon glyphicon-ok"></i></li>
                                </ul>
                                <div class="panel-footer">
                                    <button type="button" class="btn btn-primary btn-block" data-toggle="modal" data-target="#infoPropietario3" style="font-size: large"><b>Cobertura</b></></button>
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
                                
                                 <asp:Button ID="btnGuardarCotizacion" Visible="false" runat="server" Text="Guardar Cotizacion" CssClass="btn btn-danger btn-block" OnClick="btnGuardarCotizacion_Click" />
                            </div>

                            <div class="row"></div>

                            <div class="col-md-4 col-md-offset-4" style="border:none; text-align:center">
                                <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="upnlPagos">
                                    <ProgressTemplate>                                           
                                        <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>

                             <div class="row"></div>

                            <div class="col-md-8 col-md-offset-2" style="text-align: center">
                                <asp:Label runat="server" ID="lblError" Visible="false" Font-Size="Medium" ForeColor="Red" CssClass="alert-danger"></asp:Label>
                            </div>

                            <div class="row"></div>

                            <%-- Detalle de Pagos --%>
                            <div class="form-group col-md-12" style="border: none">
                                <h3>
                                    <asp:Label ID="lblResumenPagosVisaCuotas" runat="server" Text="Resumen Pagos Cuotas T.C." Visible="false"></asp:Label></h3>

                                <div class="form-group col-md-8 col-md-offset-3"">
                                    <h4><asp:Label ID="lblResumenPagosFraccionados" runat="server" Text="Resumen Pagos Fraccionados" Visible="false"></asp:Label></h4>

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
                        </div>
                    </asp:Panel>

                    <%--<asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="upnlPagos">
                        <ProgressTemplate>                                       
                            <div class="overlay" >
                                <div class="overlayContent">
                                    <h2>Cotizando...</h2>
                                    <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>
                                </div>
                            </div>                                            
                        </ProgressTemplate>
                    </asp:UpdateProgress>--%>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnCotizar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <%-- DETALLE COTIZACION EXTERNA --%>
        <div class="col-md-12">
            <asp:UpdatePanel ID="upnlDetalleCotExterna" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:Panel runat="server" ID="pnlDetalleCotExterna" Visible="false">

                        <div class="col-md-8 col-md-offset-2" style="text-align: center; background-color: whitesmoke">
                            <h3 style="text-align: center; text-shadow: 1px 1px 2px #aaa"><b>Información de pago:</b></h3>
                            <hr style="border-color: black; border-width: 1.5px;" />
                        </div>

                        <div class="col-md-8 col-md-offset-2" style="background-color: whitesmoke">

                            <!--DIV FORMA PAGO-->
                            <div class="form-group col-md-6" style="background-color: whitesmoke;">
                                <asp:Label ID="Label1" runat="server" Text="Forma de pago:"></asp:Label>
                                <asp:DropDownList ID="ddlFormaPagoExterno" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFormaPagoExterno_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <!--DIV NUMERO PAGOS-->
                            <div class="form-group col-md-6" style="background-color: whitesmoke;">
                                <asp:Label ID="Label2" runat="server" Text="Número de pagos:"></asp:Label>
                                <asp:DropDownList ID="ddlNumeroPagosExterno" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>

                            <div class="col-md-4 col-md-offset-4">
                                 <asp:LinkButton ID="btnCotizarExterno" runat="server" CssClass="btn btn-danger btn-block" OnClick="btnCotizar_Click">
                                        <i class="glyphicon glyphicon-certificate"></i> Cotizar!
                                </asp:LinkButton>                                
                            </div>

                            <%-- Detalle de Pagos --%>
                            <div class="form-group col-md-12" style="border: none">
                                <h3>
                                    <asp:Label ID="Label3" runat="server" Text="Resumen Pagos Cuotas T.C." Visible="false"></asp:Label></h3>

                                <div class="form-group col-md-8 col-md-offset-3"">
                                    <h4><asp:Label ID="Label4" runat="server" Text="Resumen Pagos Fraccionados" Visible="false"></asp:Label></h4>

                                    <asp:GridView ID="gvInformacionPlanesFraccionadoExterno" runat="server" AutoGenerateColumns="False">
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

                                <div class="form-group col-md-8 col-md-offset-3">
                                     <asp:GridView ID="gvInformacionPlanesCuotasExterno" runat="server" AutoGenerateColumns="False">
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

                            <div class="form-group col-md-8 col-md-offset-2" style="background-color: whitesmoke">
                                <asp:Label ID="lblNumeroCotizacion" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:LinkButton ID="btnImprimirCotizacion" Visible="false" runat="server" CssClass="btn btn-danger btn-block" OnClick="btnImprimirCotizacion_Click">
                                        <i class="glyphicon glyphicon-print"></i> Descargar Cotización
                                </asp:LinkButton>
                            </div>

                        </div>

                    </asp:Panel>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnCotizarExterno" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <!-- Informacion detallada de los planes Propietarios -->

        <div class="modal fade" id="infoPropietario1" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content" style='width: 75%'>
                    <div class="modal-header" style="background-color: #287233">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="text-align: center; color: white"><b>Plan Esmeralda</b></h4>
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
                                <tr style="background-color: #287233">
                                    <td style="color: white;">EDIFICIO</td>
                                    <td style="color: white;">
                                        <b>Q250,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">MENAJE DE HOGAR ( ELECTRODOMESTICOS Y EE )</td>
                                    <td style="color: white;">
                                        <b>Q100,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">MEJORAS LOCATIVAS</td>
                                    <td style="color: white;">
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>                               
                                <tr style="background-color: #287233">
                                    <td style="color: white;">PRENDAS DE VESTIR</td>
                                    <td style="color: white;">
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">GASTOS EXTRAS</td>
                                    <td style="color: white;">
                                        <b>Q35,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">REMOCION DE ESCOMBROS</td>
                                    <td style="color: white;">
                                        <b>Q35,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">RC FAMILIAR</td>
                                    <td style="color: white;">
                                        <b>Q200,000.00 </b>
                                    </td>
                                </tr>                               
                                <tr style="background-color: #287233">
                                    <td style="color: white;">RC TRABAJADORES DOMESTICOS</td>
                                    <td style="color: white;">
                                        <b>Q10,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">ROBO Y/O ATRACO DE MENAJE</td>
                                    <td style="color: white;">
                                        <b>Q30,000.00 </b>
                                    </td>
                                </tr>                               
                                <tr style="background-color: #287233">
                                    <td style="color: white;">DINERO Y VALORES DENTRO</td>
                                    <td style="color: white;">
                                        <b>Q1,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">ROTURA DE CRISTALES</td>
                                    <td style="color: white;">
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>                                
                                <tr style="background-color: #287233">
                                    <td style="color: white;">EQUIPO ELECTRODOMESTICO</td>
                                    <td style="color: white;">
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">HURTO PERSONAL</td>
                                    <td style="color: white;">
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
                    <div class="modal-header" style="background-color: #9b111e;">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="text-align: center"><b><span style="color: white;">Plan Rubí</span></b></h4>
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
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">EDIFICIO</span></td>
                                    <td>
                                        <b><span style="color: white;">Q500,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">MENAJE DE HOGAR ( ELECTRODOMESTICOS Y EE )</span></td>
                                    <td>
                                        <b><span style="color: white;">Q200,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">MEJORAS LOCATIVAS</span></td>
                                    <td>
                                        <b><span style="color: white;">INCLUIDO </span></b>
                                    </td>
                                </tr>
                                <%--<tr style="background-color: #9b111e;">
                                    <td>JOYAS Y OBJETOS DE ARTE</td>
                                    <td>
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>--%>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">PRENDAS DE VESTIR</span></td>
                                    <td>
                                        <b><span style="color: white;">INCLUIDO</span> </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">GASTOS EXTRAS</span></td>
                                    <td>
                                        <b><span style="color: white;">Q70,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">REMOCION DE ESCOMBROS</span></td>
                                    <td>
                                        <b><span style="color: white;">Q70,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">RC FAMILIAR</span></td>
                                    <td>
                                        <b><span style="color: white;">Q400,000.00 </span></b>
                                    </td>
                                </tr>
                                <%--<tr style="background-color: #9b111e;">
                                    <td>RC ARRENDATARIO</td>
                                    <td>
                                        <b>Q400,000.00 </b>
                                    </td>
                                </tr>--%>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">RC TRABAJADORES DOMESTICOS</span></td>
                                    <td>
                                        <b><span style="color: white;">Q20,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">ROBO Y/O ATRACO DE MENAJE</span></td>
                                    <td>
                                        <b><span style="color: white;">Q60,000.00 </span></b>
                                    </td>
                                </tr>
                                <%--<tr style="background-color: #9b111e;">
                                    <td>ROBO DE JOYAS</td>
                                    <td>
                                        <b>Q10,000.00 </b>
                                    </td>
                                </tr>--%>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">DINERO Y VALORES DENTRO</span></td>
                                    <td>
                                        <b><span style="color: white;">Q2,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">ROTURA DE CRISTALES</span></td>
                                    <td>
                                        <b><span style="color: white;">Q40,000.00 </span></b>
                                    </td>
                                </tr>
                                <%--<tr style="background-color: #9b111e;">
                                    <td>INMUEBLE</td>
                                    <td>
                                        <b>Q500.00 </b>
                                    </td>
                                </tr>--%>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">EQUIPO ELECTRODOMESTICO</span></td>
                                    <td>
                                        <b><span style="color: white;">Q40,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">HURTO PERSONAL</span></td>
                                    <td>
                                        <b><span style="color: white;">Q1,000.00 </span></b>
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
                    <div class="modal-header" style="background-color: #337ab7">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="text-align: center"><b><span style="color: white;">Plan Diamante</span></b></h4>
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
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">EDIFICIO</span></td>
                                    <td>
                                        <b><span style="color: white;">Q1,000,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">MENAJE DE HOGAR ( ELECTRODOMESTICOS Y EE )</span></td>
                                    <td>
                                        <b><span style="color: white;">Q400,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">MEJORAS LOCATIVAS</span></td>
                                    <td>
                                        <b><span style="color: white;">INCLUIDO </span></b>
                                    </td>
                                </tr>
                                <%--<tr style="background-color: #287233">
                                    <td>JOYAS Y OBJETOS DE ARTE</td>
                                    <td>
                                        <b>Q40,000.00 </b>
                                    </td>
                                </tr>--%>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">PRENDAS DE VESTIR</span></td>
                                    <td>
                                        <b><span style="color: white;">INCLUIDO </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">GASTOS EXTRAS</span></td>
                                    <td>
                                        <b><span style="color: white;">Q140,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">REMOCION DE ESCOMBROS</span></td>
                                    <td>
                                        <b><span style="color: white;">Q140,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">RC FAMILIAR</span></td>
                                    <td>
                                        <b><span style="color: white;">Q800,000.00 </span></b>
                                    </td>
                                </tr>
                                <%--<tr style="background-color: #287233">
                                    <td>RC ARRENDATARIO</td>
                                    <td>
                                        <b>Q800,000.00 </b>
                                    </td>
                                </tr>--%>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">RC TRABAJADORES DOMESTICOS</span></td>
                                    <td>
                                        <b><span style="color: white;">Q40,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">ROBO Y/O ATRACO DE MENAJE</span></td>
                                    <td>
                                        <b><span style="color: white;">Q120,000.00 </span></b>
                                    </td>
                                </tr>
                                <%--<tr style="background-color: #287233">
                                    <td>ROBO DE JOYAS</td>
                                    <td>
                                        <b>Q15,000.00 </b>
                                    </td>
                                </tr>--%>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">DINERO Y VALORES DENTRO</span></td>
                                    <td>
                                        <b><span style="color: white;">Q4,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">ROTURA DE CRISTALES</span></td>
                                    <td>
                                        <b><span style="color: white;">Q80,000.00 </span></b>
                                    </td>
                                </tr>
                                <%--<tr style="background-color: #287233">
                                    <td>INMUEBLE</td>
                                    <td>
                                        <b>Q1,000.00 </b>
                                    </td>
                                </tr>--%>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">EQUIPO ELECTRODOMESTICO</span></td>
                                    <td>
                                        <b><span style="color: white;">Q80,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">HURTO PERSONAL</span></td>
                                    <td>
                                        <b><span style="color: white;">Q2,000.00 </span></b>
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
                    <div class="modal-header" style="background-color: #287233">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="text-align: center; color: white"><b>Plan Esmeralda</b></h4>
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
                                <tr style="background-color: #287233">
                                    <td style="color: white;">MENAJE DE HOGAR ( ELECTRODOMESTICOS Y EE )</td>
                                    <td style="color: white;">
                                        <b>Q100,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">MEJORAS LOCATIVAS</td>
                                    <td style="color: white;">
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                
                                <tr style="background-color: #287233">
                                    <td style="color: white;">PRENDAS DE VESTIR</td>
                                    <td style="color: white;">
                                        <b>INCLUIDO </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">GASTOS EXTRAS</td>
                                    <td style="color: white;">
                                        <b>Q10,000.00 </b>
                                    </td>
                                </tr>                                
                                <tr style="background-color: #287233">
                                    <td style="color: white;">RC FAMILIAR</td>
                                    <td style="color: white;">
                                        <b>Q200,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">RC ARRENDATARIO</td>
                                    <td style="color: white;">
                                        <b>Q200,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">RC TRABAJADORES DOMESTICOS</td>
                                    <td style="color: white;">
                                        <b>Q10,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">ROBO Y/O ATRACO DE MENAJE</td>
                                    <td style="color: white;">
                                        <b>Q30,000.00 </b>
                                    </td>
                                </tr>                                
                                <tr style="background-color: #287233">
                                    <td style="color: white;">DINERO Y VALORES DENTRO</td>
                                    <td style="color: white;">
                                        <b>Q1,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">ROTURA DE CRISTALES</td>
                                    <td style="color: white;">
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>                                
                                <tr style="background-color: #287233">
                                    <td style="color: white;">EQUIPO ELECTRODOMESTICO</td>
                                    <td style="color: white;">
                                        <b>Q20,000.00 </b>
                                    </td>
                                </tr>
                                <tr style="background-color: #287233">
                                    <td style="color: white;">HURTO PERSONAL</td>
                                    <td style="color: white;">
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
                    <div class="modal-header" style="background-color: #9b111e;">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="text-align: center"><b><span style="color: white;">Plan Rubí</span></b></h4>
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
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">MENAJE DE HOGAR ( ELECTRODOMESTICOS Y EE )</span></td>
                                    <td>
                                        <b><span style="color: white;">Q200,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">MEJORAS LOCATIVAS</span></td>
                                    <td>
                                        <b><span style="color: white;">INCLUIDO </span></b>
                                    </td>
                                </tr>
                               
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">PRENDAS DE VESTIR</span></td>
                                    <td>
                                        <b><span style="color: white;">INCLUIDO </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">GASTOS EXTRAS</span></td>
                                    <td>
                                        <b><span style="color: white;">Q20,000.00 </span></b>
                                    </td>
                                </tr>                                
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">RC FAMILIAR</span></td>
                                    <td>
                                        <b><span style="color: white;">Q400,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">RC ARRENDATARIO</span></td>
                                    <td>
                                        <b><span style="color: white;">Q400,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">RC TRABAJADORES DOMESTICOS</span></td>
                                    <td>
                                        <b><span style="color: white;">Q20,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">ROBO Y/O ATRACO DE MENAJE</span></td>
                                    <td>
                                        <b><span style="color: white;">Q60,000.00 </span></b>
                                    </td>
                                </tr>                                
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">DINERO Y VALORES DENTRO</span></td>
                                    <td>
                                        <b><span style="color: white;">Q2,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">ROTURA DE CRISTALES</span></td>
                                    <td>
                                        <b><span style="color: white;">Q40,000.00 </span></b>
                                    </td>
                                </tr>                                
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">EQUIPO ELECTRODOMESTICO</span></td>
                                    <td>
                                        <b><span style="color: white;">Q40,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #9b111e;">
                                    <td><span style="color: white;">HURTO PERSONAL</span></td>
                                    <td>
                                        <b><span style="color: white;">Q1,000.00 </span></b>
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
                    <div class="modal-header" style="background-color: #337ab7">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" style="text-align: center"><b><span style="color: white;">Plan Diamante</span></b></h4>
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
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">MENAJE DE HOGAR ( ELECTRODOMESTICOS Y EE )</span></td>
                                    <td>
                                        <b><span style="color: white;">Q400,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">MEJORAS LOCATIVAS</span></td>
                                    <td>
                                        <b><span style="color: white;">INCLUIDO </span></b>
                                    </td>
                                </tr>                                
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">PRENDAS DE VESTIR</span></td>
                                    <td>
                                        <b><span style="color: white;">INCLUIDO </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">GASTOS EXTRAS</span></td>
                                    <td>
                                        <b><span style="color: white;">Q40,000.00 </span></b>
                                    </td>
                                </tr>                                
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">RC FAMILIAR</span></td>
                                    <td>
                                        <b><span style="color: white;">Q800,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">RC ARRENDATARIO</span></td>
                                    <td>
                                        <b><span style="color: white;">Q800,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">RC TRABAJADORES DOMESTICOS</span></td>
                                    <td>
                                        <b><span style="color: white;">Q40,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">ROBO Y/O ATRACO DE MENAJE</span></td>
                                    <td>
                                        <b><span style="color: white;">Q120,000.00 </span></b>
                                    </td>
                                </tr>                               
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">DINERO Y VALORES DENTRO</span></td>
                                    <td>
                                        <b><span style="color: white;">Q4,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">ROTURA DE CRISTALES</span></td>
                                    <td>
                                        <b><span style="color: white;">Q80,000.00 </span></b>
                                    </td>
                                </tr>                               
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">EQUIPO ELECTRODOMESTICO</span></td>
                                    <td>
                                        <b><span style="color: white;">Q80,000.00 </span></b>
                                    </td>
                                </tr>
                                <tr style="background-color: #337ab7">
                                    <td><span style="color: white;">HURTO PERSONAL</span></td>
                                    <td>
                                        <b><span style="color: white;">Q2,000.00</span> </b>
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
