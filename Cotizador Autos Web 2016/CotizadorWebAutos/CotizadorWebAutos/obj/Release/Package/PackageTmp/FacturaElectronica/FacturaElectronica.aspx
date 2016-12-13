<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="FacturaElectronica.aspx.cs" Inherits="CotizadorWebAutos.FacturaElectronica.FacturaElectronica" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>




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

    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading" style="text-align: center">
                <h4>Busqueda de facturas</h4>
            </div>
            <div class="panel-body" style="overflow-y: auto; overflow-x: hidden; overflow-y: inherit">
                <div class="col-md-12">
                    <asp:Panel ID="PanelInicio" runat="server" CssClass="" GroupingText="Ingreso de datos:" Style="border: none">
                        <div class="col-md-2" style="align-content: center">
                            <img id="Img2" src="../FacturaElectronica/Uno.jpg" class="img-rounded" width="95" height="95" runat="server" />
                        </div>
                        <asp:Label runat="server" Text="• Debe ingresar al menos un dato para realizar la busqueda, si su nit ya aparecen solo haga clic en 'Buscar'."></asp:Label>
                        <br />
                        <asp:Label runat="server" ID="parareq" Text="• Si realizará la busqueda por requerimientos recuerde llenar solo este campo en caso de ser varias recuerde separarlas por una coma."></asp:Label>
                        <br />
                        <hr />
                        <div class="form-group col-md-4" style="border: none">
                            <div class="form-inline" style="align-content: center">
                                <asp:Label runat="server" Text="NIT:"></asp:Label>
                                <asp:TextBox ID="txtNumIDCliente" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="90px" onkeypress="return IsNumeric(event);" ToolTip="Ingrese su nit con el ultimo digito en el cuadro pequeño"></asp:TextBox>
                                <asp:TextBox ID="txtDvIdCliente" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px" Style="text-transform: uppercase" ToolTip="Ingrese aquí el ultimo digito de su nit"></asp:TextBox>
                                <br />
                                <br />
                                <asp:Label runat="server" Text="Ramo y número de póliza"></asp:Label>
                                <br />
                                <asp:DropDownList ID="txtCodPol2" runat="server" class="form-control" ToolTip="Seleccione un ramo de poliza" AppendDataBoundItems="true">
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="txtNumPol" CssClass="form-control input-sm" ToolTip="Número de su poliza" TextMode="Number"></asp:TextBox>
                                <br />
                                <br />
                                <asp:Panel ID="pnlRequerimiento" runat="server">
                                    <asp:Label ID="lblBusquedaReq" runat="server" Text="Busqueda por requerimiento" Visible="true"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtBusquedaReq" CssClass="form-control input-sm" ToolTip="Para ingresar varios requerimientos debe ingresarlos separados por ," TextMode="SingleLine"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="BUSQUEDAreq" runat="server" Text="Ej: 12345678, 12345678, 12345678" Visible="true"></asp:Label>
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="form-group col-md-4">
                            <asp:Label ID="Label1" runat="server" Text="Seleccione el periodo: " Visible="true"></asp:Label>
                            <asp:DropDownList ID="ddlaños" runat="server" class="form-control" ToolTip="Seleccione el año con el que filtrará la busqueda de las facturas">
                            </asp:DropDownList>
                            <br />
                            <asp:Panel ID="pnlNombres" runat="server">
                                <asp:Label runat="server" Text="Nombres y Apellidos:"></asp:Label>
                                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control input-sm" placeHolder="Ingrese nombres" Style="text-transform: uppercase"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control input-sm" placeHolder="Ingrese apellidos" Style="text-transform: uppercase"></asp:TextBox>
                            </asp:Panel>
                            <br />
                        </div>
                        <%--                        <div class="form-group col-md-3" style="border:double; border-color:#DF0000;" id="pnldesdehasta" runat="server">
                            <br />
                            <asp:Label ID="lblFecha1" runat="server" Text="Desde: " Visible="true"></asp:Label>
                            <asp:TextBox ID="txtDesde" placeholder="DD/MM/YY" runat="server" CssClass="form-control input-sm" TextMode="Date"></asp:TextBox>
                            <br />
                            <asp:Label ID="lblFecha2" runat="server" Text="Hasta: " Visible="true"></asp:Label>
                            <asp:TextBox ID="txtHasta" runat="server" CssClass="form-control input-sm" placeholder="DD/MM/YY" TextMode="Date"></asp:TextBox>
                            <br />
                        </div>--%>
                        <%--    Seguir investigando sobre  DATEPICKER v3--%>
                        <div class="row"></div>
                    </asp:Panel>
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class=" col-md-4">
                                    <br />
                                    <asp:Button ID="btnBuscarPolizas" OnClick="btnBuscarPolizasF_Click" CssClass="btn btn-danger btn-block" runat="server" Text="Buscar" />
                                </div>
                                <div class=" col-md-4">
                                    <br />
                                    <asp:Button ID="Regresar" CssClass="btn btn-danger btn-block" runat="server" Text="Regresar" />
                                </div>
                                <div class=" col-md-4">
                                    <br />
                                    <asp:Button ID="btnLimpiar" OnClick="btnLimpiar_Click" CssClass="btn btn-danger btn-block" runat="server" Text="Nueva Busqueda" />
                                </div>
                                <div class=" col-md-5 col-md-offset-4  ">
                                    <br />
                                    <asp:Label ID="LnlSinRegistro" runat="server" Text=" No se han econtrado registros con los datos ingresados, corriga los datos y ejecute la busqueda nuevamente" Visible="false"></asp:Label>
                                    <asp:Label ID="LnlErrNUMPOL" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="LnlNingunCampo" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblFact" runat="server" Text=" No se han econtrado facturas emitidas para las fechas seleccionadas" Visible="false"></asp:Label>
                                    <asp:Label ID="LabelDesdeHasta" runat="server" Text=" No se han encontrado facturas, recuerde ingresar correctamente los datos" Visible="false"></asp:Label>
                                    <asp:Label ID="lblFact2" runat="server" Text=" No se han econtrado facturas para los requerimientos seleccionados" Visible="false"></asp:Label>
                                    <asp:Label ID="NoPermitido" runat="server" Text="El usuario no posee privilegios suficientes para poder buscar facturas. Por favor comuniquese con su administrador si desea habilitar esta opción." Visible="false"></asp:Label>                                
                                </div>
                                <div class="form-group col-md-12">
                                    <br />
                                    <asp:Panel ID="pnlResultadoBusqueda" Visible="false" runat="server" GroupingText="Resultado Búsqueda" Style="border: none">
                                        <div class="col-md-2" style="align-content: center">
                                            <img id="Img1" src="../FacturaElectronica/dos.jpg" class="img-rounded" width="95" height="95" runat="server" />
                                        </div>
                                        <asp:Label runat="server" Text="• Debe hacer clic en 'Detalle de factura' para visualizar la información de la misma"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblInfo" runat="server" Text=" "></asp:Label>
                                        <hr />
                                        <br />
                                        <asp:Panel ID="Panelinterno" Visible="true" runat="server" Style="border: none; align-content: center" ScrollBars="Auto" Height="200" Width="100%">
                                            <div style="align-content: center; overflow: auto">
                                                <asp:GridView ID="grvPolizas" CssClass="panel-danger" DataKeyNames="IDEPOL" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvRequerimientos_RowDataBound" AllowSorting="True" HorizontalAlign="Center" Font-Size="Small">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDetalleFactura" Visible="true" Text="&nbsp;&nbsp;&nbsp;Detalle de factura&nbsp;&nbsp;&nbsp;" OnCommand="detallefactura" CommandArgument='<%# Eval("IDEPOL") %>' runat="server" ForeColor="Red"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CODPOL" HeaderText="Ramo de póliza">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NUMPOL" HeaderText="Número de póliza">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FECINIVIG" HeaderText="Fecha de Inicio de póliza">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#DF0000" ForeColor="White" />
                                                </asp:GridView>
                                            </div>
                                        </asp:Panel>
                                    </asp:Panel>
                                </div>
                                <div class="form-group col-md-12">
                                    <br />
                                    <asp:Panel ID="pnlFacturas" Visible="false" runat="server" GroupingText="Facturas" Style="border: none">
                                        <div class="col-md-2" style="align-content: center">
                                            <img id="Img3" src="../FacturaElectronica/tres.jpg" class="img-rounded" width="95" height="95" runat="server" />
                                        </div>
                                        <asp:Label runat="server" Text="• Seleccione las facturas que desee descargar y haga clic en 'Generar PDF'"></asp:Label><br />
                                        <hr />
                                        <br />
                                        <div></div>
                                        <asp:Panel ID="Panel1" Visible="true" runat="server" Style="border: none; align-content: center" ScrollBars="None" Height="500" Width="100%">
                                            <div class="col-md-10 col-md-offset-1" style="align-content: center">
                                                <asp:Button ID="Imprime" OnClick="ImprimeFact_Click" CssClass="btn btn-danger pull-right" runat="server" Text="Generar PDF" />
                                                <br />
                                                <br />
                                                <asp:GridView ID="grvFacturas" CssClass="panel-danger" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvFacturas_RowDataBound" OnSelectedIndexChanged="grvRequerimientos_SelectedIndexChanged" CellPadding="5" RowStyle-HorizontalAlign="center" ClientIDMode="AutoID" AllowSorting="True" HorizontalAlign="Center" Font-Size="Small">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <HeaderTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text="Seleccion" Visible="true"></asp:Label>
                                                                <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAll(this);" />/ 
                                                                <asp:Label ID="Label2" runat="server" Text="Generar" Visible="true"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkFactura" CssClass="nombreclass" Checked='false'></asp:CheckBox>
                                                                <asp:LinkButton ID="btnFacIndividual" Visible="true" Text="Imprimir factura individual" OnCommand="FacturaIndividual" CommandArgument='<%# Eval("IDEFACT") + ";" + Eval("POLIZA") + ";" + Eval("NOMBRE") + ";" + Eval("IDEPOL") %>' runat="server" ForeColor="Red"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkFactura" CssClass="nombreclass" Checked='false'></asp:CheckBox>
                                                                <asp:LinkButton ID="btnFacIndividual" Visible="true" Text="Descargar factura individual" OnCommand="FacturaIndividual" CommandArgument='<%# Eval("IDEFACT") + ";" + Eval("POLIZA") + ";"+ Eval("NOMBRE") + ";" + Eval("IDEPOL") %>' runat="server" ForeColor="Red"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="POLIZA" HeaderText="Póliza">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SERIE" HeaderText="Serie">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FACTURA" HeaderText="Número de factura">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FECHA_COBRO" HeaderText="Fecha emisión">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CODMONEDA" HeaderText="Moneda">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TOTALPRIMA" HeaderText="Total">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="IDEFACT" HeaderText="NUMERO_REQUERIMIENTO">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="IDEPOL" HeaderText="IDEPOL">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#DF0000" ForeColor="White" />
                                                </asp:GridView>
                                            </div>
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlImprime" runat="server" Visible="false">
                                    </asp:Panel>
                                </div>
                                <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <div class="overlay" />
                                        <div class="overlayContent">
                                            <h2>Realizando Busqueda...</h2>
                                            <i class="fa fa-spinner fa-spin fa-3x fa-fw"></i>
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="Imprime" />
                                <asp:PostBackTrigger ControlID="grvFacturas" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="panel-footer">
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
            // document.getElementById("error").style.display = ret ? "none" : "inline";
            return ret;
        }
        // for check all checkbox  
        function CheckAll(Checkbox) {
            var GridVwHeaderCheckbox = document.getElementById("<%=grvFacturas.ClientID %>");
            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
        $(function () {
            $('#datetimepicker1').datetimepicker();
        });
    </script>
</asp:Content>
