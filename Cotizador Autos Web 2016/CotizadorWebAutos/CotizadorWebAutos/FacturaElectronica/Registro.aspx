<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="CrearUsuarioParaFacturas.aspx.cs" Inherits="CotizadorWebAutos.FacturaElectronica.CrearUsuarioParaFacturas" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="col-md-10 col-lg-offset-1" style="align-content: center; background-color: whitesmoke">
            <h3 style="text-align: center"><b>Creación de usuario para consulta de facturas</b></h3>
                    </div>
        <br />
         <br />
        
        <div class="col-md-10 col-lg-offset-1" style="align-content: center">
            <br />
            <asp:Label runat="server" Text="• Debe ingresar al menos un dato para realizar la busqueda, es recomendable utilizar el nit"></asp:Label>
            <br />
            <asp:Label runat="server" Text="• Puede ingresar su poliza pero esta debe ser acompañada por lo menos con un nombre y un apellido para realizar la busqueda"></asp:Label>
            <br />
            <asp:Label runat="server" Text="• En caso desee ubicar la información con el correo electrónico es necesario que solo ingrese el mismo"></asp:Label>
            <br /><br />

           <%--  PASO 1 - Panel de ingreso de datos para busqueda de información--%>
            <asp:Panel ID="IngresoDatos" runat="server" CssClass="" GroupingText="Ingreso de datos:" Style="border: none">
                   <div class=" form-group col-md-12" style="text-align: center">
                    <div class="col-md-2" style="align-content:center">
                    <img id="Img1"  src="../FacturaElectronica/Uno.jpg" class="img-rounded" width="95" height="95"  runat="server" />
                    </div>
                     <%--  txt de numid, dvid, combo de productos--%>
                    <div class="col-md-4" style="align-content:center">
                       <div class="form-inline">
                       <div class="form-group">
                        <asp:Label ID="Label10" runat="server" Text="NIT: "></asp:Label>
                        <asp:TextBox ID="txtNumID" runat="server" CssClass="form-control input-sm" MaxLength="7" Width="110px"></asp:TextBox>
                        -
                        <asp:TextBox ID="txtDvId" runat="server" CssClass="form-control input-sm" MaxLength="1" Width="60px" Style="text-transform: uppercase"></asp:TextBox>
                        </div>
                        <br />  
                        <br />
                        <br />
                        <asp:Label runat="server" Text="Ramo y número de póliza"></asp:Label>
                        <br />
                        <asp:DropDownList ID="txtCodPol2" runat="server" class="form-control" ToolTip="Seleccione un ramo de poliza" AppendDataBoundItems="true">
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtNumPol" CssClass="form-control input-sm" ToolTip="Número de su poliza"></asp:TextBox>
                        </div>  
                        <asp:Label ID="Alerta_y_datos" runat="server" Text="Corrobore los datos ingresados en caso no exista debe comunicarse con soluciones para actualizar la información a soluciones@mapfre.com " Visible="false"></asp:Label>
                     </div>  
                       
                    <div class="form-group col-md-4">
                        <%--Nombres, Apellidos, Correo--%>
                        <asp:Label ID="Label3" runat="server" Text="Nombre: "></asp:Label>
                        <asp:TextBox runat="server" ID="txtNombre1" CssClass="form-control input-sm" ToolTip="Nombre"></asp:TextBox>
                        <asp:Label ID="Label8" runat="server" Text="Apellidos: "></asp:Label>
                        <asp:TextBox runat="server" ID="txtApellidos1" CssClass="form-control input-sm" ToolTip="Apellidos"></asp:TextBox>
                        <asp:Label ID="Label9" runat="server" Text="Correo electronico: "></asp:Label>
                        <asp:TextBox runat="server" ID="txtCorreo" CssClass="form-control input-sm" ToolTip="Correo Electronico"></asp:TextBox>
                    </div> 

                   <div class="form-group col-md-2">
                       <br />
                       <br />
                       <asp:Button ID="BuscarDatos"  OnClick="BuscarDatos_Click" CssClass="btn btn-block btn-danger" runat="server" Text="Buscar" />
                       
                   </div> 
                    
                </div>
             </asp:Panel>

            <%--PASO 2 - Panel de corroboracion de datos--%>
            <asp:Panel ID="pnlCorrobora" runat="server" CssClass="" GroupingText="Confirmación de datos:" Style="border: none" Visible="false">
                <div class=" form-group col-md-12" style="text-align: center">
                    <div class="col-md-2" style="align-content:center">
                        <img id="Img2"  src="../FacturaElectronica/dos.jpg" class="img-rounded" width="95" height="95"  runat="server" />
                    </div>
                   <%-- cajas de texto nombres, apellidos, nombre de usuario, correo electronico, direccion, telefono, poliza y codigo de intermediario--%>
                    <div class="col-md-10" style="align-content:center">
                        <div class="form-group col-md-6">
                            <asp:Label ID="Label1" runat="server" Text="Nombres:"></asp:Label>
                            <asp:TextBox ID="txtNombres" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-6">
                            <asp:Label ID="Label5" runat="server" Text="Apellidos: "></asp:Label>
                            <asp:TextBox ID="txtApellidos" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="row"></div>
                        <div class="form-group col-md-6">
                            <asp:Label ID="Label7" runat="server" Text="Nombre de usuario"></asp:Label>
                            <asp:TextBox ID="txtNombreUsuario" CssClass="form-control" runat="server" ToolTip="Ingrese el usuario con el que ingresará a la pagina web"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-6">
                            <asp:Label ID="Label4" runat="server" Text="Correo Electrónico"></asp:Label>
                            <asp:TextBox ID="txtCorreoElectronico" CssClass="form-control" runat="server" TextMode="Email"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-12">
                            <asp:Label ID="lblDireccion" runat="server" Text="Dirección" ></asp:Label>
                            <asp:TextBox ID="txtDireccion" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-6">
                            <asp:Label ID="lblTelefono" runat="server" Text="Teléfono"></asp:Label>
                            <asp:TextBox ID="txtTelefono" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-4">
                            <asp:Label ID="Label2" runat="server" Text="Poliza"></asp:Label>
                            <asp:DropDownList ID="Poliza" runat="server" class="form-control" ToolTip="Seleccione un ramo de poliza" AppendDataBoundItems="true">
                       </asp:DropDownList>
                        </div>
                        <div class="form-group col-md-4">
                            <asp:Label ID="lblIntermediario" runat="server" Text="Código Intermediario"></asp:Label>
                            <asp:TextBox ID="txtCodigoIntermediario" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="row"></div>
                        <div class="col-md-4 col-lg-offset-4">
                        <asp:Button ID="btnGuardarUsuarioNuevo" OnClick="btnGuardarUsuarioNuevo_Click" CssClass="btn btn-block btn-danger" runat="server" Text="Confirmar" />
                        <asp:Label ID="Label6" runat="server" Text="En caso desee actualizar la información debe comunicarse con soluciones a soluciones@mapfre.com "></asp:Label>
                        </div>
                        </div>
                     </div>
            </asp:Panel>

             <%--PASO 3 - Panel de verificación de correo--%>
            <asp:Panel ID="pnlConfirma" Visible="false" runat="server" GroupingText="Verificación" Style="border: none">
                 <div class="col-md-2" style="align-content:center">
                        <img id="Img3"  src="../FacturaElectronica/tres.jpg" class="img-rounded" width="95" height="95"  runat="server" />
                 </div>
                <div class="col-md-10" style="align-content:center">
                    <div class="form-group col-md-4"  id="Div1" runat="server" style="align-content:center"> 
                        <br />
                        <asp:Label ID="lblverificar" runat="server" Text="Porfavor verifique su correo electrónico para finalizar la creación de usuario." Visible="false"></asp:Label>
                        <asp:Label ID="Lblsincrear" runat="server" Text="No ha sido posible crear el usuario, comunicarse con informática." Visible="false"></asp:Label>
                    </div>
                    <div class="form-group col-md-4"  id="pnlimagenrelleno" runat="server">
                       <br /><img id="Img4"  src="../Recursos/imagenes/mapfre.png" class="img-rounded" alt="Cinque Terre" width="350" height="75"  runat="server" />
                    </div>
                    <div class="col-md-4 col-lg-offset-4">
                    <br />
                        <asp:Button ID="Regresar"  CssClass="btn btn-block btn-danger" runat="server" Text="Regresar" />
                    </div>
                </div>

            </asp:Panel>
        </div>
    </div>
</asp:Content>
