<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Master_Cotizador.Master" AutoEventWireup="true" CodeBehind="LogOut.aspx.cs" Inherits="CotizadorWebAutos.LogIn_Out.LogOut" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
               <div class="col-md-4 col-md-offset-4" style="align-content:center; align-items:center; align-self:center">
                   <br />
                   <br />
                   <br />
                   <br />
                   <br />
                   <br />
                   <br />
                   <br />
                <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick">
                </asp:Timer>
                   <br />
                   <br />
                    <asp:Label ID="Label1" Text="Cerrando Sesión." runat="server"  Font-Bold="true" Font-Size="Large"/>
                <img src="../Recursos/imagenes/MapfreTeCuida.jpg" width="400" height="85"/>
               </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>