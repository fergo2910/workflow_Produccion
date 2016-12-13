﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Control_Cupones.aspx.cs" Inherits="CotizadorWebAutos.GM_Control_Cupones.Control_Cupones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../crystalreportviewers13/js/crviewer/crv.js"></script>
    <script type="text/javascript">
        function informacionCupon() {
            $("#btninformacionCupon").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- MODAL PARA MOSTRAR INFORMACION DE CUPON--%>
	<div class="modal fade" id="modalinformacionCupon" role="dialog">
		<div class="modal-dialog modal-sm">
			<div class="modal-content">

				<div class="modal-header" style="background-color: #DF0000">
					<button type="button" class="close" data-dismiss="modal">&times;</button>
					<h4 class="modal-title" style="text-align: center; color: white"><b>Información</b></h4>
				</div>

				<div class="modal-body">
					<asp:Label ID="lblinformacionCupon" runat="server" CssClass="alert-default"></asp:Label>
				</div>

				<div class="modal-footer">
				</div>

			</div>
		</div>
	</div>

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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
                <button type="button" class="close" style="display: none;" id="btninformacionCupon"
			        data-toggle="modal" data-target="#modalinformacionCupon">
		        </button>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="row">
        <!-- Columna izquierda, area de busqueda e informacion -->
        <div class="col-sm-3 col-lg-offset-0">
            <br />
            <br />
            <h1 class="modal-title">Médico Virtual</h1>
            <!-- BUSQUEDA -->
            <div class="well">
                <h4>Búsqueda de pólizas</h4>
                <div class="btn-group">
                    <div class="input-group">
                        <span class="input-group-addon" id="poliza">Póliza</span>
                        <asp:TextBox ID="txtcodpol" runat="server" class="form-control" placeholder="Código" Style="text-transform: uppercase"></asp:TextBox>
                        <asp:TextBox ID="txtnumpol" runat="server" class="form-control" placeholder="Número" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="input-group">
                        <span class="input-group-addon" id="cert-addon">Certificado</span>
                        <asp:TextBox ID="txtcertificado" runat="server" class="form-control" aria-describedby="cert-addon" placeholder="Número" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="col-lg-8 col-lg-offset-2">
                        <asp:Button ID="btnBusqueda" runat="server" Text="Busqueda" CssClass="btn btn-block text-center" OnClick="btnBusqueda_Click" />
                    </div>
                </div>
                <div>
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Fecha: " Font-Bold="True" Font-Size="Medium"></asp:Label><asp:Label ID="txtfecha" runat="server" Text="" Font-Bold="True" Font-Size="Medium"></asp:Label>
                </div>
                <br />

                <div id="AlertaMsg" runat="server">
                </div>
            </div>
            <%--boton de historial medico--%>
            <div class="well">
                <asp:Button ID="historialMedico" CssClass="btn btn-block btn-danger" runat="server" Text="Mostrar Historial" OnClick="historialMedico_Click" />
            </div>
        </div>
        <!-- Columna derecha, todo lo que el usuario puede manejar -->
        <div class="col-md-9">
            <h1 class="page-header">Búsqueda de observaciones</h1>
            <%-- columna 1 --%>
            <div class="col-md-6">
                <asp:Panel ID="pnlBeneficiarios" runat="server" Visible="false">
                    <!-- First Blog Post -->
                    <h3>Asegurados</h3>
                    <asp:GridView ID="TablaAsociados" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Seleccion">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnSeleccion" runat="server" ImageUrl="~/Recursos/imagenes/busqueda.png" CssClass="img-responsive" Height="30" Width="30" OnClick="seleccion_Click" />
                                    <asp:HiddenField ID="valorIDEASEG" runat="server" Value='<%#Eval("IDEASEG")%>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="IDEASEG" HeaderText="IDEASEG" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NOMTER" HeaderText="Nombre">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="APETER" HeaderText="Apellido">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="APECAS" HeaderText="APECAS" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STSASEG" HeaderText="STSASEG" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CODPARENT" HeaderText="CODPARENT" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DESCPARENT" HeaderText="Parentesco">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="IDEPOL" HeaderText="IDEPOL" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STSPOL" HeaderText="STSPOL" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#DF0000" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </asp:Panel>

                <asp:Panel ID="pnlHistorial" runat="server" Visible="false" DataKeyNames="NumCupon">
                    <h3><asp:Label ID="lblTitulo" runat="server" Text="Historial de cupones. "></asp:Label></h3>
                    <div style="align-content: center; height: 250px; overflow: auto">
                        <asp:GridView ID="historialCupones" runat="server" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="NumCupon" HeaderText="No. Cupón">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="FecSts" HeaderText="Fecha de emisión">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Ideproveedor" HeaderText="Id proveedor" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="IdeAseg" HeaderText="Código de paciente" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nomter" HeaderText="Nombre">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Apeter" HeaderText="Apellido">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CodParent" HeaderText="Codigo de parentesco" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DescParent" HeaderText="Parentesco">
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle BackColor="#DF0000" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </div>
                </asp:Panel>

            </div>
            <%-- columna 2 --%>
            <div class="col-md-6">
                <asp:Panel ID="pnlObservaciones" runat="server" Visible="false">
                    <!-- Second Blog Post -->
                    <h3>Observacion de asegurado:</h3>
                    <asp:Button ID="btnCobrarCupon" runat="server" CssClass="btn btn-danger btn-block" Text="Guardar Cupón" OnClick="btnCobrarCupon_Click" />
                    <asp:GridView ID="observaciones" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ORDEN" HeaderText="Orden">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STSBIT" HeaderText="Estado">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#DF0000" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                    <div id="alertaObservaciones" runat="server">
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <!-- /.row -->
</asp:Content>
