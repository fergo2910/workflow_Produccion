<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Cotizador_Web_Autos.Master" AutoEventWireup="true" CodeBehind="Ingreso.aspx.cs" Inherits="CotizadorWebAutos.Acceso.Ingreso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Recursos/Diseno_Login/css/form-elements.css" rel="stylesheet" />
    <link href="../Recursos/Diseno_Login/css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />


    <script type="text/javascript">
        function MostrandoMensajeaUsuario() {
            $("#btnShowPopup").click();
        }
    </script>


    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container">


        <%--CUADRO DE DIALOGO - VICTORIA GUTIERREZ--%>
         <button type="button" style="display: none;" id="btnShowPopup"
                    data-toggle="modal" data-target="#MostrarMensajeGeneral">
                </button>

         <div class="modal fade" id="MostrarMensajeGeneral" role="dialog">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content">

                            <div class="modal-header" style="background-color: #DF0000">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title" style="text-align: center; color: white"><b>Mensaje</b></h4>
                               
                            </div>

                            <div class="modal-body">
                                <asp:Label ID="MostrarMensaje" runat="server" Visible="true"  ForeColor="#000000" ></asp:Label>
                            </div>

                            <div class="modal-footer">
                        <asp:LinkButton ID="btnAceptar" runat="server" CssClass="btn btn-danger"
                                 class="close" Font-Bold="True"><i class="glyphicon glyphicon-ok"></i> Aceptar
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
         </div>

      <%------------------------------------------%>

        <div class="row"></div>

        <div class="col-sm-6 col-sm-offset-3 form-box">

            <div class="form-top">
                <div class="form-top-left">
                    <h3>Accede al sitio</h3>
                    <p>Ingresa tu usuario y contraseña:</p>
                </div>
                <div class="form-top-right">
                    <img src="../Recursos/imagenes/logoMap2.png" />
                </div>
            </div>

            <div class="form-bottom">
                <div class="form-group">
                    <asp:TextBox ID="txtNombreUsuario" runat="server" placeholder="Usuario..." CssClass="form-username form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtPasswordUsuario" runat="server" placeholder="Contraseña..." CssClass="form-password form-control" TextMode="Password"></asp:TextBox>
                </div>

                <asp:Button ID="btnLoginIngresar" runat="server" Text="Iniciar sesión" CssClass="center-block btn-danger btn" Font-Bold="True" OnClick="btnLoginIngresar_Click" />

                <asp:HyperLink runat="server" CssClass="center-block  btn" ForeColor="White" NavigateUrl="~/Acceso/RecuperarClave.aspx" Text="Olvide mi contraseña"></asp:HyperLink>
                <asp:HyperLink runat="server" CssClass="center-block  btn" ForeColor="White" NavigateUrl="~/FacturaElectronica/Registro.aspx" Text="Crear usuario para consulta de facturas electrónicas" Visible="true"></asp:HyperLink>
              
            </div>
        </div>

    </div>
</asp:Content>
