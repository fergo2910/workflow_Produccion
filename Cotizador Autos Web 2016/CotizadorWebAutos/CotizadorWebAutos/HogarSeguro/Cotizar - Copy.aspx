<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="Cotizar.aspx.cs" Inherits="CotizadorWebAutos.HogarSeguro.Cotizar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    
    <%-- Informacion personal del cliente --%>
    <asp:Panel ID="pnlDatosPersonales" runat="server">
        <div class="container">

            <div class="col-md-4">
                <img src="../Recursos/imagenes/logomapvg.png" class="img-rounded" alt="Cinque Terre" width="250" height="180" />
            </div>

            <div class="col-md-8" style="align-content: center; background-color: whitesmoke">
                <div id="divtituloformulario" class="row">
                    <h3 style="text-align: center; text-shadow: 1px 1px 2px #aaa"><b>Realiza la cotización de tu seguro en 3 simples pasos:</b></h3>
                    <hr style="border-color: black; border-width: 1.5px;" />
                    <ol>
                        <li>Ingresa tus datos</li>
                        <li>Haz clic en "Mostrar Planes"</li>
                        <li>selecciona el plan que más te interesa</li>
                    </ol>
                </div>
            </div>
            <br />

            <div class="col-md-8" style="align-content: center; background-color: whitesmoke;">

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

                    <div class="form-group col-xs- 12 col-sm-8 col-md-5" style="background-color: whitesmoke">
                        <asp:CheckBox ID="chkAgente" runat="server" OnCheckedChanged="chkAgente_CheckedChanged" AutoPostBack="true" Text="&nbsp; Es Agente?"/>
                        <asp:TextBox ID="txtCodigoAgente" runat="server" Enabled="false" MaxLength="6" placeholder="Código Agente" onkeypress="return IsNumeric(event);"> </asp:TextBox>
                                          
                         
                        
                        
                         </div>

                    <div class="form-group  col-xs- 12 col-sm-12 col-md-6" style="background-color: whitesmoke">
                        <asp:Button ID="btnPersonalesSiguiente" runat="server" Text="Mostrar Planes" OnClick="btnPersonalesSiguiente_Click" CssClass="btn btn-danger" />
                              <span id="error" style="color: Red; display: none">&nbsp;* Solo se permiten números (0 - 9)</span>                    
                    </div>



                </div>

            </div>

        </div>


    </asp:Panel>

    <%-- Productos planes Inquilinos --%>

    <br />

    <asp:Panel ID="pnlPlanInquilino" runat="server" Visible="false">
        <div class="container">
            <div class="row">

                <div class="col-md-4 text-center">
                    <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #D3851D; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                        <div class="panel-heading" style="border-width: 2px; border-color: #D3851D;">
                            <img src="../Recursos/imagenes/incendio-bronce.gif" height="200px" width="200px" />
                            <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Inquilino 1</b></h3>
                        </div>

                        <ul class="list-group text-center">
                            <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #D3851D">RC <i class="glyphicon glyphicon-ok"></i></li>
                        </ul>
                        <div class="panel-footer">
                            <button type="button" class="btn btn-success btn-block" data-toggle="modal" data-target="#infoInquilino1" style="font-size: large"><b>Cobertura</b></></button>
                            <asp:Button ID="btnCotizarInquilinoUno" runat="server" Text="Cotizar!" CssClass="btn btn-danger btn-block" Style="font-size: large" Font-Bold="True" OnClick="btnCotizarInquilinoUno_Click" />

                        </div>
                    </div>
                </div>

                <div class="col-md-4 text-center">
                    <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #414141; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                        <div class="panel-heading" style="border-width: 2px; border-color: #414141;">
                            <img src="../Recursos/imagenes/incendio-plateado.gif" height="200px" width="200px" />
                            <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Inquilino 2</b></h3>
                        </div>
                        <ul class="list-group text-center">
                            <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #414141">RC <i class="glyphicon glyphicon-ok"></i></li>
                        </ul>
                        <div class="panel-footer">
                            <button type="button" class="btn btn-success btn-block" data-toggle="modal" data-target="#infoInquilino2" style="font-size: large"><b>Cobertura</b></></button>
                            <asp:Button ID="btnCotizarInquilinoDos" runat="server" Text="Cotizar!" CssClass="btn btn-danger btn-block" Style="font-size: large" Font-Bold="True" OnClick="btnCotizarInquilinoDos_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-4 text-center">
                    <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #F8DA8C; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                        <div class="panel-heading" style="border-width: 2px; border-color: #F8DA8C;">
                            <img src="../Recursos/imagenes/incendio.gif" height="200px" width="200px" />
                            <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Inquilino 3</b></h3>
                        </div>
                        <ul class="list-group text-center">
                            <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #F8DA8C">RC <i class="glyphicon glyphicon-ok"></i></li>
                        </ul>
                        <div class="panel-footer">
                            <button type="button" class="btn btn-success btn-block" data-toggle="modal" data-target="#infoInquilino3" style="font-size: large"><b>Cobertura</b></></button>
                            <asp:Button ID="btnCotizarInquilinoTres" runat="server" Text="Cotizar!" CssClass="btn btn-danger btn-block" Style="font-size: large" Font-Bold="True" OnClick="btnCotizarInquilinoTres_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </asp:Panel>

    <%-- Productos planes Propietarios --%>

    <br />

    <asp:Panel ID="pnlPlanPropietario" runat="server" Visible="false">

        <div class="container">
            <div class="row">

                <div class="col-md-4 text-center">
                    <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #D3851D; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                        <div class="panel-heading" style="border-width: 2px; border-color: #D3851D;">
                            <img src="../Recursos/imagenes/incendio-bronce.gif" height="200px" width="200px" />
                            <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Propietario 1</b></h3>
                        </div>
                        <ul class="list-group text-center">
                            <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #D3851D">Rotura de maquinaria <i class="glyphicon glyphicon-ok"></i></li>
                        </ul>
                        <div class="panel-footer">
                            <button type="button" class="btn btn-success btn-block" data-toggle="modal" data-target="#infoPropietario1" style="font-size: large"><b>Cobertura</b></></button>
                            <asp:Button ID="btnCotizarPropietarioUno" runat="server" Text="Cotizar!" CssClass="btn btn-danger btn-block" Style="font-size: large" Font-Bold="True" OnClick="btnCotizarPropietarioUno_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-4 text-center">
                    <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #414141; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                        <div class="panel-heading" style="border-width: 2px; border-color: #414141;">
                            <img src="../Recursos/imagenes/incendio-plateado.gif" height="200px" width="200px" />
                            <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Propietario 2</b></h3>
                        </div>
                        <ul class="list-group text-center">
                            <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #414141">Rotura de maquinaria <i class="glyphicon glyphicon-ok"></i></li>
                        </ul>
                        <div class="panel-footer">
                            <button type="button" class="btn btn-success btn-block" data-toggle="modal" data-target="#infoPropietario2" style="font-size: large"><b>Cobertura</b></></button>
                            <asp:Button ID="btnCotizarPropietarioDos" Font-Bold="True" runat="server" Text="Cotizar!" CssClass="btn btn-danger btn-block" Style="font-size: large" OnClick="btnCotizarPropietarioDos_Click" />
                        </div>
                    </div>
                </div>

                <div class="col-md-4 text-center">
                    <div class="panel panel-default panel-pricing" style="border-width: 2px; border-color: #F8DA8C; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75);">
                        <div class="panel-heading" style="border-width: 2px; border-color: #F8DA8C;">
                            <img src="../Recursos/imagenes/incendio.gif" height="200px" width="200px" />
                            <h3 style="color: black; text-shadow: 2px 2px 2px #aaa"><b>Propietario 3</b></h3>
                        </div>
                        <ul class="list-group text-center">
                            <li class="list-group-item">Incendio <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item">Robo <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item">Cristales <i class="glyphicon glyphicon-ok"></i></li>
                            <li class="list-group-item" style="border-bottom-width: 2px; border-bottom-color: #F8DA8C">Rotura de maquinaria <i class="glyphicon glyphicon-ok"></i></li>
                        </ul>
                        <div class="panel-footer">
                            <button type="button" class="btn btn-success btn-block" data-toggle="modal" data-target="#infoPropietario3" style="font-size: large"><b>Cobertura</b></></button>
                            <asp:Button ID="btnCotizarPropietarioTres" Font-Bold="True" runat="server" Text="Cotizar!" CssClass="btn btn-danger btn-block" Style="font-size: large" OnClick="btnCotizarPropietarioTres_Click" />
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </asp:Panel>

    <%-- --------------------------------------------------------------------------------------------------------------------------- --%>

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

    <%----------------------------------------------------------------------------------------------------------%>

    <%-- Detalles de cotizaciones de propietarios--%>


    <asp:Panel ID="cotizarPropietario1" runat="server" Visible="false">

        <div class="container">
            <div class="row">

                <div class="col-md-8; panel panel-default; center-block" style='width: 90%; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75); border-color: #EEBF80; border-width: 2px'>




                    <div class=" panel-heading" style="background-color: #EEBF80">
                        <h3 style="text-align: left; text-shadow: 1px 1px 2px #aaa; color: black"><b>Cotización Propietario 1</b></h3>
                    </div>

                    <div class="modal-body">

                        <div class="col-md-5 center-block" style="vertical-align: central">

                            <div class="col-md-5  text-center">
                                <img src="../Recursos/imagenes/incendio-bronce.gif" height="150px" width="150px" style="align-self: center" />
                            </div>

                            <div class="col-md-7  text-center">
                                <div class="btn-group-vertical" role="group">
                                    <br />


                                    <asp:Button ID="imprimirCotizacion" runat="server" Text="Imprimir cotización" CssClass="btn btn-danger" BackColor="#AE6F17" BorderColor="WhiteSmoke" />

                                    <asp:Button ID="enviarCotizacion" runat="server" Text="Enviar cotización" CssClass="btn btn-danger" BackColor="#AE6F17" BorderColor="WhiteSmoke" />

                                    <asp:Button ID="regresar" runat="server" Text="Regresar" CssClass="btn btn-danger" OnClick="regresar_Click1" BackColor="#AE6F17" BorderColor="WhiteSmoke" />

                                </div>

                            </div>


                        </div>


                        <div class="col-md-7 ; panel; center-block">



                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>No. de pagos</th>
                                        <th>Total de la cuota</th>
                                        <th>Total cotización completa</th>
                                    </tr>

                                </thead>
                                <tbody>

                                    <tr>
                                        <td>4</td>
                                        <td><b>Q696.08</b></td>
                                        <td><b>Q2784.32</b></td>
                                    </tr>

                                    <tr>
                                        <td>6</td>
                                        <td><b>Q472.27</b></td>
                                        <td><b>Q2833.62</b></td>
                                    </tr>

                                    <tr>
                                        <td>12</td>
                                        <td><b>Q243.26</b></td>
                                        <td><b>Q2919.12</b></td>
                                    </tr>


                                </tbody>
                            </table>
                        </div>


                        <h6 style="color: whitesmoke;">Hola</h6>



                    </div>
                </div>





            </div>
        </div>


    </asp:Panel>

    <asp:Panel ID="cotizarPropietario2" runat="server" Visible="false">

        <div class="container">
            <div class="row">

                <div class="col-md-8; panel panel-default; center-block" style='width: 90%; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75); border-color: #404040; border-width: 2px'>




                    <div class=" panel-heading" style="background-color: #D2D2D2">
                        <h3 style="text-align: left; text-shadow: 1px 1px 2px #aaa; color: black"><b>Cotización Propietario 2</b></h3>
                    </div>

                    <div class="modal-body">

                        <div class="col-md-5 center-block" style="vertical-align: central">

                            <div class="col-md-5  text-center">
                                <img src="../Recursos/imagenes/incendio-plateado.gif" height="150px" width="150px" style="align-self: center" />
                            </div>

                            <div class="col-md-7  text-center">
                                <div class="btn-group-vertical" role="group">
                                    <br />


                                    <asp:Button ID="imprimirPropietario2" runat="server" Text="Imprimir cotización" CssClass="btn btn-danger" BackColor="#404040" BorderColor="WhiteSmoke" />

                                    <asp:Button ID="enviarPropietario2" runat="server" Text="Enviar cotización" CssClass="btn btn-danger" BackColor="#404040" BorderColor="WhiteSmoke" />

                                    <asp:Button ID="regresarpropietario2" runat="server" Text="Regresar" CssClass="btn btn-danger" OnClick="regresar_Click1" BackColor="#404040" BorderColor="WhiteSmoke" />

                                </div>

                            </div>


                        </div>


                        <div class="col-md-7 ; panel; center-block">



                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>No. de pagos</th>
                                        <th>Total de la cuota</th>
                                        <th>Total cotización completa</th>
                                    </tr>

                                </thead>
                                <tbody>

                                    <tr>
                                        <td>4</td>
                                        <td><b>Q696.08</b></td>
                                        <td><b>Q2784.32</b></td>
                                    </tr>

                                    <tr>
                                        <td>6</td>
                                        <td><b>Q472.27</b></td>
                                        <td><b>Q2833.62</b></td>
                                    </tr>

                                    <tr>
                                        <td>12</td>
                                        <td><b>Q243.26</b></td>
                                        <td><b>Q2919.12</b></td>
                                    </tr>


                                </tbody>
                            </table>
                        </div>


                        <h6 style="color: whitesmoke;">Hola</h6>



                    </div>
                </div>





            </div>
        </div>


    </asp:Panel>

    <asp:Panel ID="cotizarPropietario3" runat="server" Visible="false">

        <div class="container">
            <div class="row">

                <div class="col-md-8; panel panel-default; center-block" style='width: 90%; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75); border-color: #F8DA8C; border-width: 2px'>




                    <div class=" panel-heading" style="background-color: #F8DA8C">
                        <h3 style="text-align: left; text-shadow: 1px 1px 2px #aaa; color: black"><b>Cotización Propietario 3</b></h3>
                    </div>

                    <div class="modal-body">

                        <div class="col-md-5 center-block" style="vertical-align: central">

                            <div class="col-md-5  text-center">
                                <img src="../Recursos/imagenes/incendio.gif" height="150px" width="150px" style="align-self: center" />
                            </div>

                            <div class="col-md-7  text-center">
                                <div class="btn-group-vertical" role="group">
                                    <br />
                                    <asp:Button ID="imprimirPropietario3" runat="server" Text="Imprimir cotización" CssClass="btn btn-danger" BackColor="#D29C0D" BorderColor="WhiteSmoke" />
                                    <asp:Button ID="eviarPropietario3" runat="server" Text="Enviar cotización" CssClass="btn btn-danger" BackColor="#D29C0D" BorderColor="WhiteSmoke" />
                                    <asp:Button ID="regresarPropietario3" runat="server" Text="Regresar" CssClass="btn btn-danger" OnClick="regresar_Click1" BackColor="#D29C0D" BorderColor="WhiteSmoke" />

                                </div>

                            </div>


                        </div>


                        <div class="col-md-7 ; panel; center-block">



                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>No. de pagos</th>
                                        <th>Total de la cuota</th>
                                        <th>Total cotización completa</th>
                                    </tr>

                                </thead>
                                <tbody>

                                    <tr>
                                        <td>4</td>
                                        <td><b>Q696.08</b></td>
                                        <td><b>Q2784.32</b></td>
                                    </tr>

                                    <tr>
                                        <td>6</td>
                                        <td><b>Q472.27</b></td>
                                        <td><b>Q2833.62</b></td>
                                    </tr>

                                    <tr>
                                        <td>12</td>
                                        <td><b>Q243.26</b></td>
                                        <td><b>Q2919.12</b></td>
                                    </tr>


                                </tbody>
                            </table>
                        </div>


                        <h6 style="color: whitesmoke;">Hola</h6>



                    </div>
                </div>





            </div>
        </div>


    </asp:Panel>


    <%--Detalles de cotizaciones inquilinos--%>

    <asp:Panel ID="cotizarInquilino1" runat="server" Visible="false">

        <div class="container">
            <div class="row">

                <div class="col-md-8; panel panel-default; center-block" style='width: 90%; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75); border-color: #EEBF80; border-width: 2px'>




                    <div class=" panel-heading" style="background-color: #EEBF80">
                        <h3 style="text-align: left; text-shadow: 1px 1px 2px #aaa; color: black"><b>Cotización Inquilino 1</b></h3>
                    </div>

                    <div class="modal-body">

                        <div class="col-md-5 center-block" style="vertical-align: central">

                            <div class="col-md-5  text-center">
                                <img src="../Recursos/imagenes/incendio-bronce.gif" height="150px" width="150px" style="align-self: center" />
                            </div>

                            <div class="col-md-7  text-center">
                                <div class="btn-group-vertical" role="group">
                                    <br />


                                    <asp:Button ID="imprimirInquilino1" runat="server" Text="Imprimir cotización" CssClass="btn btn-danger" BackColor="#AE6F17" BorderColor="WhiteSmoke" />

                                    <asp:Button ID="enviarInquilino1" runat="server" Text="Enviar cotización" CssClass="btn btn-danger" BackColor="#AE6F17" BorderColor="WhiteSmoke" />

                                    <asp:Button ID="regresarInqulino1" runat="server" Text="Regresar" CssClass="btn btn-danger" OnClick="regresar_ClickInquilino" BackColor="#AE6F17" BorderColor="WhiteSmoke" />

                                </div>

                            </div>


                        </div>


                        <div class="col-md-7 ; panel; center-block">



                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>No. de pagos</th>
                                        <th>Total de la cuota</th>
                                        <th>Total cotización completa</th>
                                    </tr>

                                </thead>
                                <tbody>

                                    <tr>
                                        <td>4</td>
                                        <td><b>Q696.08</b></td>
                                        <td><b>Q2784.32</b></td>
                                    </tr>

                                    <tr>
                                        <td>6</td>
                                        <td><b>Q472.27</b></td>
                                        <td><b>Q2833.62</b></td>
                                    </tr>

                                    <tr>
                                        <td>12</td>
                                        <td><b>Q243.26</b></td>
                                        <td><b>Q2919.12</b></td>
                                    </tr>


                                </tbody>
                            </table>
                        </div>

                        <%--No quitar el siguiente codigo es para ajustar correctamente el espacio--%>
                        <h6 style="color: whitesmoke;">Hola</h6>



                    </div>
                </div>





            </div>
        </div>


    </asp:Panel>

    <asp:Panel ID="cotizarInquilino2" runat="server" Visible="false">

        <div class="container">
            <div class="row">

                <div class="col-md-8; panel panel-default; center-block" style='width: 90%; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75); border-color: #404040; border-width: 2px'>




                    <div class=" panel-heading" style="background-color: #D2D2D2">
                        <h3 style="text-align: left; text-shadow: 1px 1px 2px #aaa; color: black"><b>Cotización Inquilino 2</b></h3>
                    </div>

                    <div class="modal-body">

                        <div class="col-md-5 center-block" style="vertical-align: central">

                            <div class="col-md-5  text-center">
                                <img src="../Recursos/imagenes/incendio-plateado.gif" height="150px" width="150px" style="align-self: center" />
                            </div>

                            <div class="col-md-7  text-center">
                                <div class="btn-group-vertical" role="group">
                                    <br />


                                    <asp:Button ID="imprimirInquilino2" runat="server" Text="Imprimir cotización" CssClass="btn btn-danger" BackColor="#404040" BorderColor="WhiteSmoke" />

                                    <asp:Button ID="enviarInquilino2" runat="server" Text="Enviar cotización" CssClass="btn btn-danger" BackColor="#404040" BorderColor="WhiteSmoke" />

                                    <asp:Button ID="regresarInquilino2" runat="server" Text="Regresar" CssClass="btn btn-danger" OnClick="regresar_ClickInquilino" BackColor="#404040" BorderColor="WhiteSmoke" />

                                </div>

                            </div>


                        </div>


                        <div class="col-md-7 ; panel; center-block">



                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>No. de pagos</th>
                                        <th>Total de la cuota</th>
                                        <th>Total cotización completa</th>
                                    </tr>

                                </thead>
                                <tbody>

                                    <tr>
                                        <td>4</td>
                                        <td><b>Q696.08</b></td>
                                        <td><b>Q2784.32</b></td>
                                    </tr>

                                    <tr>
                                        <td>6</td>
                                        <td><b>Q472.27</b></td>
                                        <td><b>Q2833.62</b></td>
                                    </tr>

                                    <tr>
                                        <td>12</td>
                                        <td><b>Q243.26</b></td>
                                        <td><b>Q2919.12</b></td>
                                    </tr>


                                </tbody>
                            </table>
                        </div>


                        <h6 style="color: whitesmoke;">Hola</h6>



                    </div>
                </div>





            </div>
        </div>


    </asp:Panel>

    <asp:Panel ID="cotizarInquilino3" runat="server" Visible="false">

        <div class="container">
            <div class="row">

                <div class="col-md-8; panel panel-default; center-block" style='width: 90%; box-shadow: 6px 6px 4px 0px rgba(0,0,0,0.75); border-color: #F8DA8C; border-width: 2px'>




                    <div class=" panel-heading" style="background-color: #F8DA8C">
                        <h3 style="text-align: left; text-shadow: 1px 1px 2px #aaa; color: black"><b>Cotización Inquilino 3</b></h3>
                    </div>

                    <div class="modal-body">

                        <div class="col-md-5 center-block" style="vertical-align: central">

                            <div class="col-md-5  text-center">
                                <img src="../Recursos/imagenes/incendio.gif" height="150px" width="150px" style="align-self: center" />
                            </div>

                            <div class="col-md-7  text-center">
                                <div class="btn-group-vertical" role="group">
                                    <br />
                                    <asp:Button ID="imprimirInquilino3" runat="server" Text="Imprimir cotización" CssClass="btn btn-danger" BackColor="#D29C0D" BorderColor="WhiteSmoke" />
                                    <asp:Button ID="enviarInquilino3" runat="server" Text="Enviar cotización" CssClass="btn btn-danger" BackColor="#D29C0D" BorderColor="WhiteSmoke" />
                                    <asp:Button ID="regresarInquilino3" runat="server" Text="Regresar" CssClass="btn btn-danger" OnClick="regresar_ClickInquilino" BackColor="#D29C0D" BorderColor="WhiteSmoke" />

                                </div>

                            </div>


                        </div>


                        <div class="col-md-7 ; panel; center-block">



                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>No. de pagos</th>
                                        <th>Total de la cuota</th>
                                        <th>Total cotización completa</th>
                                    </tr>

                                </thead>
                                <tbody>

                                    <tr>
                                        <td>4</td>
                                        <td><b>Q696.08</b></td>
                                        <td><b>Q2784.32</b></td>
                                    </tr>

                                    <tr>
                                        <td>6</td>
                                        <td><b>Q472.27</b></td>
                                        <td><b>Q2833.62</b></td>
                                    </tr>

                                    <tr>
                                        <td>12</td>
                                        <td><b>Q243.26</b></td>
                                        <td><b>Q2919.12</b></td>
                                    </tr>


                                </tbody>
                            </table>
                        </div>


                        <h6 style="color: whitesmoke;">Hola</h6>



                    </div>
                </div>





            </div>
        </div>


    </asp:Panel>

    <%--hola--%>
</asp:Content>