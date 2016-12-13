<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Consulta_Estado_Cuenta.aspx.cs" Inherits="CotizadorWebAutos.Oficina_Virtual.Consulta_Estado_Cuenta" %>

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
                <h2>Consulta de estado de cuenta</h2>
            </div>
            <div class="panel-body" style="overflow-y: auto; overflow-x: hidden; overflow-y: inherit">
                <div class="col-md-12">
                    <asp:Panel ID="PanelConsulta" runat="server" CssClass="" GroupingText="Parámetros de Consulta:" Style="border: none">
                        <div class="col-md-2" style="align-content: center">
                            <img id="img1" src="../Recursos/imagenes/Uno.jpg" class="img-rounded" width="95" height="95" runat="server" />
                        </div>
                        <asp:Label runat="server" Text="• Debe ingresar al menos un dato para realizar la busqueda, si su nit ya aparecen solo haga clic en 'Buscar'."></asp:Label>
                        <br />
                        <hr />
                        <div class="form-group col-md-4" style="border: none">
                            <div class="form-inline" style="align-content: center">
                                <asp:Label runat="server" Text="NIT:"></asp:Label>
                                <asp:TextBox ID="txtNumid" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="90px" onkeypress="return IsNumeric(event);" ToolTip="Ingrese su nit con el ultimo digito en el cuadro pequeño"></asp:TextBox>
                                <asp:TextBox ID="txtDvid" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px" Style="text-transform: uppercase" ToolTip="Ingrese aquí el ultimo digito de su nit"></asp:TextBox>
                                <br />
                                <br />
                                <asp:Label runat="server" Text="Ramo y número de póliza"></asp:Label>
                                <br />
                                <asp:DropDownList ID="comboRamoPolizas" runat="server" class="form-control" ToolTip="Seleccione un ramo de poliza" AppendDataBoundItems="true">
                                </asp:DropDownList>
                                <asp:TextBox runat="server" ID="txtNumPol" CssClass="form-control input-sm" ToolTip="Número de su poliza" TextMode="Number"></asp:TextBox>
                                <br />
                                <br />
                            </div>
                        </div>
                        <div class="form-group col-md-4">
                            <asp:Panel ID="pnlNombres" runat="server">
                                <asp:Label runat="server" Text="Nombres y Apellidos:"></asp:Label>
                                <asp:TextBox runat="server" ID="txtNombres" CssClass="form-control input-sm" placeHolder="Ingrese nombres" Style="text-transform: uppercase"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txtApellidos" CssClass="form-control input-sm" placeHolder="Ingrese apellidos" Style="text-transform: uppercase"></asp:TextBox>
                            </asp:Panel>
                            <br />
                        </div>
                        <div class="row"></div>
                    </asp:Panel>
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanelBotones" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class=" col-md-4">
                                    <br />
                                    <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" CssClass="btn btn-danger btn-block" runat="server" Text="Buscar" />
                                </div>
                                <div class=" col-md-4">
                                    <br />
                                    <asp:Button ID="btnRegresar" OnClick="btnRegresar_Click" CssClass="btn btn-danger btn-block" runat="server" Text="Regresar" />
                                </div>
                                <div class=" col-md-4">
                                    <br />
                                    <asp:Button ID="btnLimpiar" OnClick="btnLimpiar_Click" CssClass="btn btn-danger btn-block" runat="server" Text="Nueva Busqueda" />
                                </div>
                                <div class=" col-md-5 col-md-offset-4  ">
                                    <br />
                                    <asp:Label ID="lblErrorGeneral" runat="server" Text="" Visible="false"></asp:Label>
                                </div>
                                <div class="form-group col-md-12">
                                    <br />
                                    <asp:Panel ID="pnlResultadoPolizas" Visible="false" runat="server" GroupingText="Resultado Búsqueda" Style="border: none">
                                        <div class="col-md-2" style="align-content: center">
                                            <img id="img2" src="../Recursos/imagenes/dos.jpg" class="img-rounded" width="95" height="95" runat="server" />
                                        </div>
                                        <asp:Label runat="server" Text="• Debe hacer clic en 'Detalle de Póliza' para visualizar la información de la misma"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblInfoPoliza" runat="server" Text=" "></asp:Label>
                                        <hr />
                                        <br />
                                        <asp:Panel ID="pnlPolizas" Visible="true" runat="server" Style="border: none; align-content: center" ScrollBars="Auto" Height="200" Width="100%">
                                            <div style="align-content: center; overflow: auto">
                                                <asp:GridView ID="grvPolizas" CssClass="panel-danger" DataKeyNames="CODPOL" runat="server" AutoGenerateColumns="False"
                                                     AllowSorting="True" HorizontalAlign="Center" Font-Size="Small">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDetallePolizas" Visible="true" Text="&nbsp;&nbsp;&nbsp;Detalle de póliza&nbsp;&nbsp;&nbsp;"
                                                                    OnCommand="btnDetallePolizas_Command" CommandArgument='<%# Eval("CODPOL") + ";" + Eval("NUMPOL") %>' runat="server" ForeColor="Red"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CODPOL" HeaderText="Código de póliza">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NUMPOL" HeaderText="Número de póliza">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NOMBRE" HeaderText="Número de póliza">
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
                                    <asp:Panel ID="pnlResultadoVigencias" Visible="false" runat="server" GroupingText="Resultado de vigencias" Style="border: none">
                                        <div class="col-md-2" style="align-content: center">
                                            <img id="img3" src="../Recursos/imagenes/tres.jpg" class="img-rounded" width="95" height="95" runat="server" />
                                        </div>
                                        <asp:Label runat="server" Text="• Debe hacer clic en 'Detalle de Vigencia' para visualizar la información de la misma"></asp:Label>
                                        <br />
                                        <asp:Label ID="lblInfoVigencia" runat="server" Text=" "></asp:Label>
                                        <hr />
                                        <br />
                                        <asp:Panel ID="pnlVigencias" Visible="true" runat="server" Style="border: none; align-content: center" ScrollBars="Auto" Height="200" Width="100%">
                                            <div style="align-content: center; overflow: auto">
                                                <asp:GridView ID="grvVigencias" CssClass="panel-danger" DataKeyNames="" runat="server" AutoGenerateColumns="False"
                                                    AllowSorting="True" HorizontalAlign="Center" Font-Size="Small">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDetalleVigencias" Visible="true" Text="&nbsp;&nbsp;&nbsp;Detalle de vigencia&nbsp;&nbsp;&nbsp;"
                                                                    OnCommand="btnDetalleVigencias_Command" CommandArgument='<%# Eval("CODPOL") + ";" + Eval("NUMPOL") + ";" + Eval("IDEPOL") %>' runat="server" ForeColor="Red"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CODPOL" HeaderText="Código de póliza">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NUMPOL" HeaderText="Número de póliza">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FECINIVIG" HeaderText="Inicio">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FECFINVIG" HeaderText="Final">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="STSPOL" HeaderText="ESTADO">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FECANUL" HeaderText="Fecha anulada">
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
                                    <asp:Panel ID="pnlResultadoEstadoCuenta" Visible="false" runat="server" GroupingText="Estado de cuenta" Style="border: none">
                                        <asp:Label runat="server" Text="• Seleccione los estados de cuenta que desee descargar y haga clic en 'Generar PDF'"></asp:Label><br />
                                        <hr />
                                        <br />
                                        <asp:Panel ID="pnlEstadoCuenta" Visible="true" runat="server" Style="border: none; align-content: center" ScrollBars="None" Height="500" Width="100%">
                                            <div class="col-md-10 col-md-offset-1" style="align-content: center">
                                                <asp:Button ID="btnImprimirEstadoCuenta" OnClick="btnImprimirEstadoCuenta_Click" CssClass="btn btn-danger pull-right" runat="server" Text="Generar PDF" />
                                                <br />
                                                <br />
                                                <asp:GridView ID="grvEstadoCuenta" CssClass="panel-danger" runat="server" AutoGenerateColumns="False" 
                                                     CellPadding="5" RowStyle-HorizontalAlign="center" ClientIDMode="AutoID" 
                                                    AllowSorting="True" HorizontalAlign="Center" Font-Size="Small">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <HeaderTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text="Seleccion" Visible="true"></asp:Label>
                                                                <asp:CheckBox ID="chkAllSelect" runat="server" onclick="CheckAll(this);" />/ 
                                                                <asp:Label ID="Label2" runat="server" Text="Generar" Visible="true"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkEstadoCuenta" CssClass="nombreclass" Checked='false'></asp:CheckBox>
                                                                <asp:LinkButton ID="btnDescargarEstadoCuenta" Visible="true" Text="Imprimir Estado de cuenta" OnCommand="btnDescargarEstadoCuenta_Command" CommandArgument='<%# Eval("IDEPOL") %>' runat="server" ForeColor="Red"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="chkEstadoCuenta" CssClass="nombreclass" Checked='false'></asp:CheckBox>
                                                                <asp:LinkButton ID="btnDescargarEstadoCuenta" Visible="true" Text="Descargar factura individual" OnCommand="btnDescargarEstadoCuenta_Command" CommandArgument='<%# Eval("IDEPOL") %>' runat="server" ForeColor="Red"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CODPOL" HeaderText="Código de póliza">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NUMPOL" HeaderText="Número de póliza">
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="IDEPOL" HeaderText="Nombre">
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
                                <asp:UpdateProgress ID="UpdateProgress1" DynamicLayout="true" runat="server" AssociatedUpdatePanelID="UpdatePanelBotones">
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
                                <asp:PostBackTrigger ControlID="btnImprimirEstadoCuenta" />
                                <asp:PostBackTrigger ControlID="grvEstadoCuenta" />
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
            return ret;
        }
        function CheckAll(Checkbox) {
            var GridVwHeaderCheckbox = document.getElementById("<%=grvEstadoCuenta.ClientID %>");
            for (i = 1; i < GridVwHeaderCheckbox.rows.length; i++) {
                GridVwHeaderCheckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
        $(function () {
            $('#datetimepicker1').datetimepicker();
        });
    </script>
</asp:Content>
