<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="CotizadorWebAutos.Seguridad.Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <div class="container">
        <br />
        <br />
        <br />
        <br />
        <asp:UpdatePanel ID="upnlRoles" runat="server">
            <ContentTemplate>
                <div class="col-md-12">
                    <p></p>
                    <asp:Button ID="Button1" runat="server" Text="Regresar" CssClass="btn btn-danger btn-block" OnClick="btnRegresar_Click" />
                    <div class="col-md-12" style="text-align: center">
                        <h1>Administración de Roles</h1>
                    </div>
                    <div class="row"></div>
                    <br />
                    <%-- Roles del sistema--%>
                    <div class="col-md-6" style="align-content: center; border: none">
                        <div class="panel panel-danger ">
                            <div class="panel-heading">
                                <h4>Roles del Sistema</h4>
                            </div>
                            <div class="panel-body">
                                <asp:ListBox CssClass="form-control" ID="lstRolesSistema" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstRolesSistema_SelectedIndexChanged" Height="250px"></asp:ListBox>
                            </div>
                        </div>
                    </div>
                    <%-- Acciones por rol--%>
                    <div class="col-md-6" style="align-content: center; border: none">
                        <div class="panel panel-danger ">
                            <div class="panel-heading">
                                <h4>Acciones por Rol</h4>
                            </div>
                            <div class="panel-body" style="overflow-y: auto; height: 280px">
                                <asp:CheckBoxList ID="chkAccionesRol" AutoPostBack="true" runat="server" TextAlign="Right" OnSelectedIndexChanged="chkAccionesRol_SelectedIndexChanged">
                                </asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-12" style="text-align: center">
                    <div class="panel panel-danger ">
                        <div class="panel-heading">
                            <h4>Manejo de roles</h4>
                        </div>
                        <div class="panel-body">
                            <div class="form-group col-md-3">
                                <h5>Ingrese el nombre de nuevo rol</h5>
                                <asp:TextBox ID="txtNombreRol" CssClass="form-control" runat="server" placeHolder="Nombre de Rol"></asp:TextBox>
                                <p></p>
                                <asp:Button ID="btnAgregarRol" runat="server" Text="Agregar Rol" CssClass="btn btn-danger center-block" OnClick="btnAgregarRol_Click" />
                            </div>
                            <div class="form-group col-md-6">
                                <asp:GridView ID="grvListaRoles" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estado" HeaderText="Estado">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Eliminar Rol">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEliminar" Text="Eliminar" Visible="true" OnCommand="btnEliminar_Command" CommandArgument='<%# Eval("id_rol") %>' runat="server"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modificar Rol">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnModificar" Text="Modificar" Visible="true" OnCommand="btnModificar_Command" CommandArgument='<%# Eval("id_rol") %>' runat="server"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#DF0000" ForeColor="White" />
                                </asp:GridView>
                            </div>
                            <div class="form-group col-md-3">
                                <asp:Panel ID="pnlModificar" runat="server" Visible="false">
                                    <h5>Ingrese el nuevo nombre</h5>
                                    <asp:TextBox ID="txtNuevoNombreRol" CssClass="form-control" runat="server" placeHolder="Nombre de Rol"></asp:TextBox>
                                    <p></p>
                                    <asp:Label ID="lblHiddenIdRol" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Button ID="btnModificarRol" runat="server" Text="Modificar Rol" CssClass="btn btn-danger center-block" OnClick="btnActualizar_Click" />
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <h4>
                                <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="label-info"></asp:Label></h4>
                        </div>
                    </div>
                </div>
                <div class="col-md-12" style="text-align: center">
                    <div class="panel panel-danger ">
                        <div class="panel-heading">
                            <h4>Manejo de Acciones</h4>
                        </div>
                        <div class="panel-body">
                            <div class="form-group col-md-3">
                                <h5>Ingrese el nombre de la nueva accion</h5>
                                <asp:TextBox ID="txtNombreAccion" CssClass="form-control" runat="server" placeHolder="Nombre del Accion"></asp:TextBox>
                                <p></p>
                                <asp:TextBox ID="txtDeetalleAccion" CssClass="form-control" runat="server" placeHolder="Detalle de Accion"></asp:TextBox>
                                <p></p>
                                <asp:Button ID="btnAgregarAccion" runat="server" Text="Agregar Accion" CssClass="btn btn-danger center-block" OnClick="btnAgregarAccion_Click" />
                            </div>
                            <div class="form-group col-md-6">
                                <asp:GridView ID="grvListaAccions" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="nombre_accion" HeaderText="Nombre">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="estado" HeaderText="Estado">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="detalle_accion" HeaderText="Detalle">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Eliminar Accion">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEliminarAccion" Text="Eliminar" Visible="true" OnCommand="btnEliminarAccion_Command" CommandArgument='<%# Eval("id_accion") %>' runat="server"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modificar Accion">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnModificarAccion" Text="Modificar" Visible="true" OnCommand="btnModificarAccion_Command" CommandArgument='<%# Eval("id_accion") %>' runat="server"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#DF0000" ForeColor="White" />
                                </asp:GridView>
                            </div>
                            <div class="form-group col-md-3">
                                <asp:Panel ID="pnlModificarAccion" runat="server" Visible="false">
                                    <h5>Ingrese el nuevo nombre</h5>
                                    <asp:TextBox ID="txtNuevoNombreAccion" CssClass="form-control" runat="server" placeHolder="Nombre de Accion"></asp:TextBox>
                                    <p></p>
                                    <asp:TextBox ID="txtNuevoDetalleAccion" CssClass="form-control" runat="server" placeHolder="Detalle de Accion"></asp:TextBox>
                                    <p></p>
                                    <asp:Label ID="lblHiddenIdAccion" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Button ID="btnModificarAccion" runat="server" Text="Modificar Accion" CssClass="btn btn-danger center-block" OnClick="btnModificarAccion_Click" />
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="panel-footer">
                            <h4>
                                <asp:Label ID="lblMensajeAccion" runat="server" Text="" CssClass="label-info"></asp:Label></h4>
                        </div>
                    </div>
                </div>
                <p></p>
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-danger btn-block" OnClick="btnRegresar_Click" />
            </ContentTemplate>
            <Triggers></Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
