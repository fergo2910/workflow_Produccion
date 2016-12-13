<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Configuracion_Usuario.aspx.cs" Inherits="CotizadorWebAutos.Seguridad.Configuracion_Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function detalleAccion() {
            $("#btnDetalleAccion").click();
        }
        function MostrarAccesos() {
            $("#btnMostrarAccesos").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <%-- MODAL PARA MOSTRAR ACCIONES POR ROL--%>
    <div class="modal fade" id="modalDetalleAcciones" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header" style="background-color: #DF0000">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="text-align: center; color: white"><b>Acciones Permitidas</b></h4>
                </div>

                <div class="modal-body" style="text-align: center">
                    <asp:ListBox ID="lstDetalleAcciones" runat="server" Enabled="false" Rows="10"></asp:ListBox>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                </div>

            </div>
        </div>
    </div>
    <%-- MODAL PARA MOSTRAR LISTADO DETALLE ACCIONES --%>
    <div class="modal fade" id="modalMostrarAccesos" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header" style="background-color: #DF0000">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="text-align: center; color: white"><b>Listado de Acciones Permitidas</b></h4>
                </div>

                <div class="modal-body" style="text-align: center; height: 200px; overflow: auto">
                    <asp:GridView ID="grvAccesoDetalle" runat="server" AutoGenerateColumns="false" >
                        <Columns>
                            <asp:BoundField DataField="nombre_accion" HeaderText="Acción">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                            <asp:BoundField DataField="detalle_accion" HeaderText="Detalle">
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#DF0000" ForeColor="White" />
                    </asp:GridView>
                </div>

            </div>
        </div>
    </div>

    <div class="container-fluid">

        <%-- BOTON ESCONDIDO PARA ACCIONAR MODAL DETALLE ACCION DE ROL --%>
        <button type="button" class="close" style="display: none;" id="btnDetalleAccion"
            data-toggle="modal" data-target="#modalDetalleAcciones">
        </button>
        <%-- BOTON ESCONDIDO PARA ACCIONAR MODAL LISTA DE DETALLES DE ACCIONES --%>
        <button type="button" class="close" style="display: none;" id="btnMostrarAccesos"
            data-toggle="modal" data-target="#modalMostrarAccesos">
        </button>

        <br />
        <br />
        <br />

        <div class="col-md-12" style="text-align: center">
            <h2>Configuración de usuario</h2>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />

        <div class="col-md-5" style="align-content: center; border: none">

            <div class="panel panel-danger ">

                <div class="panel-heading">
                    <h4>Edición De Usuario</h4>
                </div>

                <div class="panel-body" style="overflow-y: auto">

                    <%-- NOMBRES --%>
                    <div class="form-group col-md-6">
                        <asp:Label runat="server">Nombres</asp:Label>
                        <asp:TextBox runat="server" ID="txtNombres" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <%-- APELLIDOS --%>
                    <div class="form-group col-md-6">
                        <asp:Label runat="server">Apellidos</asp:Label>
                        <asp:TextBox runat="server" ID="txtApellidos" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>

                    <%-- FECHA CREACION USUARIO --%>
                    <div class="form-group col-md-8">
                        <asp:Label runat="server">Fecha Creación Usuario</asp:Label>
                        <asp:TextBox runat="server" ID="txtFechaCreacion" Enabled="false" CssClass="form-control"></asp:TextBox>
                    </div>

                    <%-- USUARIO INTERNO --%>
                    <div class="form-group col-md-4">
                        <asp:CheckBox CssClass="chkUsuarioInterno" Enabled="false" Checked="true" runat="server" ID="chkUsuarioInterno" Text="Usuario Interno" TextAlign="right" />
                    </div>

                    <div class="row"></div>

                    <%-- TELEFONO --%>
                    <div class="form-group col-md-4">
                        <asp:Label runat="server">Teléfono</asp:Label>
                        <asp:TextBox runat="server" ID="txtTelefono" MaxLength="8" CssClass="form-control"></asp:TextBox>
                    </div>

                    <%-- CORREO ELECTRONICO --%>
                    <div class="form-group col-md-8">
                        <asp:Label runat="server">Correo Electrónico</asp:Label>
                        <asp:TextBox runat="server" ID="txtCorreoElectronico" TextMode="Email" CssClass="form-control"></asp:TextBox>
                    </div>

                    <%-- ACTUALIZAR INFORMACION --%>
                    <div class="form-group col-md-4 col-lg-offset-4">
                        <asp:LinkButton ID="btnActualizar" OnClick="btnActualizar_Click" runat="server" CssClass="btn btn-danger btn-block"><i class="glyphicon glyphicon-pencil"></i> Actualizar</asp:LinkButton>
                    </div>

                </div>
            </div>
        </div>

        <div class="col-md-4" style="border-style: none; border-color: inherit; border-width: medium; align-content: center; top: 0px; left: 0px;">
            <%-- Panel de roles de acceso--%>
            <div class="panel panel-danger ">
                <div class="panel-heading">
                    <h4>Roles de acceso asignados</h4>
                </div>

                <div class="panel-body" style="overflow-y: auto">
                    <asp:GridView runat="server" ID="grvRolesAsignados" AutoGenerateColumns="false" DataKeyNames="id_rol" OnRowDataBound="grvRolesAsignados_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Seleccionar">
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkRol" AutoPostBack="true" OnCheckedChanged="chkRol_CheckedChanged"></asp:CheckBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="id_rol" HeaderText="id_rol" />
                            <asp:BoundField DataField="nombre" HeaderText="Rol" />
                            <asp:TemplateField HeaderText="Acciones permitidas">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDetalleRol" Text="Ver" runat="server" OnCommand="verDetalleAccion" CommandArgument='<%# Eval("id_rol") %>'></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <asp:Button ID="btnVerAcciones" CssClass="btn btn-danger center-block" runat="server" Text="Ver lista de acciones" OnClick="btnVerAcciones_Click" />

            </div>
        </div>

        <div class="col-md-3" style="align-content: center; border: none">
            <div class="panel panel-danger ">
                <div class="panel-heading">
                    <h4>Permisos especiales</h4>
                </div>

                <div class="panel-body" style="overflow-y: auto">
                    <asp:CheckBoxList ID="chkPermisosUsuario" AutoPostBack="true" runat="server" TextAlign="Right" OnSelectedIndexChanged="chkPermisosUsuario_SelectedIndexChanged">
                    </asp:CheckBoxList>
                </div>

            </div>
        </div>

        <div class="row"></div>

        <div class="form-group col-md-6 " style="align-content: center; border: none">

            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h4>Asignación de Intermediarios para cotizaciones</h4>
                    <asp:Label runat="server" Text="Intermediarios:"></asp:Label>
                    <asp:DropDownList ID="ddlIntermediario" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="panel-body" style="overflow-y: auto">

                    <asp:GridView ID="gvIntermediarios" runat="server" AutoGenerateColumns="False" OnRowCommand="gvIntermediarios_RowCommand" AllowPaging="True" OnPageIndexChanging="gvIntermediarios_PageIndexChanging">
                        <Columns>
                            <asp:ButtonField ButtonType="Button" CommandName="EliminaInter" Text="Eliminar" />
                            <asp:BoundField DataField="codigo_intermediario" HeaderText="Código" />
                            <asp:BoundField DataField="nombre" HeaderText="Intermediario" />
                        </Columns>
                    </asp:GridView>

                </div>

                <div class="panel-footer">
                    <table style="width: 100%">
                        <tr>
                            <th>

                                <asp:Button ID="btNuevoIntermediario" CssClass="btn btn-danger center-block" runat="server" Text="Agregar Intermediario" OnClick="btNuevoIntermediario_Click" />

                            </th>
                            <th>

                                <asp:Button ID="btTodosIntermediarios" CssClass="btn btn-danger center-block" runat="server" Text="Todos los Intermediario" OnClick="btTodosIntermediarios_Click" />

                            </th>
                        </tr>
                    </table>
                </div>
            </div>

        </div>

        <%-- Panel de descueto asignado--%>
        <asp:Panel ID="pnlDescuento" runat="server" Visible="false">
            <div class="form-group col-md-6" style="align-content: center; border: none">
                <div class="panel panel-danger">

                    <div class="panel-heading">
                        <h4>Roles de descuento</h4>

                        <asp:Label runat="server" Text="Roles de acceso disponibles:"></asp:Label>
                        <asp:DropDownList ID="cmbRolesDescuento" runat="server" CssClass="form-control"></asp:DropDownList>



                        <asp:Label runat="server" Text="Planes con descuento disponibles:"></asp:Label>
                        <asp:DropDownList ID="cmbPlanesDescuento" runat="server" CssClass="form-control"></asp:DropDownList>

                    </div>

                    <div class="panel-body" style="overflow-y: auto">

                        <h5>Roles de descuento asignados</h5>
                        <asp:GridView ID="grvRolesDescuento" runat="server" AutoGenerateColumns="False" OnRowCommand="grvRolesDescuento_RowCommand" OnRowDataBound="grvRolesDescuento_RowDataBound">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="eliminar" Text="Eliminar" />
                                <asp:BoundField DataField="id_rol" HeaderText="id_rol" />
                                <asp:BoundField DataField="nombre_rol" HeaderText="Rol" />
                                <asp:BoundField DataField="porcentaje_descuento_maximo" HeaderText="% Descuento" />
                                <asp:BoundField DataField="id_plan_web" HeaderText="id_plan_web" />
                                <asp:BoundField DataField="nombre_plan" HeaderText="Plan" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="panel-footer">
                        <asp:Button ID="btnGuardarRoles" CssClass="btn btn-danger center-block" runat="server" Text="Agregar Rol" OnClick="btnGuardarRoles_Click" />
                    </div>

                </div>
            </div>
        </asp:Panel>

        <div class="col-md-4 col-md-offset-4" style="height: 350px; overflow: auto; align-content: center">
            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-block btn-danger" OnClick="btnRegresar_Click" />
        </div>
    </div>
</asp:Content>
