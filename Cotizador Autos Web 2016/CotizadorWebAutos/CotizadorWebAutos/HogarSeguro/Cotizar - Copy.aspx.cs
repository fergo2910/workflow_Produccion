using Lbl_Cotizado_Autos_Web.HogarSeguro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CotizadorWebAutos.HogarSeguro
{
    public partial class Cotizar : System.Web.UI.Page
    {
        //codigo viejo
        /*
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnPersonalesSiguiente_Click(object sender, EventArgs e)
        {
            if (txtCorreoCliente.Text == string.Empty || txtNombreCliente.Text == string.Empty || txtTelefonoCliente.Text == string.Empty)
            {
                mostrarMensaje("Debe ingresar los datos personales para poder continuar.");
                return;
            }

            if (txtTelefonoCliente.Text.Length < 8)
            {
                mostrarMensaje("Debe ingresar un número teléfonico valido.");
                return;
            }

            if (rbtnTipoBien.SelectedValue == string.Empty)
            {
                mostrarMensaje("Seleccione si es inquilino o propietario.");
                return;
            }

            if (chkAgente.Checked && (txtCodigoAgente.Text == string.Empty))
            {
                mostrarMensaje("Debe ingresar un codigo de agente.");
                return;
            }

            if (rbtnTipoBien.SelectedValue == "Inquilino")
            {
                pnlPlanInquilino.Visible = true;
                pnlPlanPropietario.Visible = false;
                cotizarPropietario1.Visible = false;
                cotizarPropietario2.Visible = false;
                cotizarPropietario3.Visible = false;
                cotizarInquilino1.Visible = false;
                cotizarInquilino2.Visible = false;
                cotizarInquilino3.Visible = false;

            }
            else
            {
                pnlPlanPropietario.Visible = true;
                pnlPlanInquilino.Visible = false;
                cotizarPropietario1.Visible = false;
                cotizarPropietario2.Visible = false;
                cotizarPropietario3.Visible = false;
                cotizarInquilino1.Visible = false;
                cotizarInquilino2.Visible = false;
                cotizarInquilino3.Visible = false;
            }
        }
        protected void btnCotizarPropietarioUno_Click(object sender, EventArgs e)
        {
            guardarCotizacion("HOGARTOT-1", "COMBO01");
        }
        protected void btnCotizarPropietarioDos_Click(object sender, EventArgs e)
        {
            guardarCotizacion("HOGARTOT-1", "COMBO02");
        }
        protected void btnCotizarPropietarioTres_Click(object sender, EventArgs e)
        {
            guardarCotizacion("HOGARTOT-1", "COMBO03");
        }
        protected void btnCotizarInquilinoUno_Click(object sender, EventArgs e)
        {
            guardarCotizacion("", "");

            pnlPlanPropietario.Visible = false;
            pnlPlanInquilino.Visible = false;
            cotizarPropietario1.Visible = false;
            cotizarPropietario2.Visible = false;
            cotizarPropietario3.Visible = false;
            cotizarInquilino1.Visible = true;
            cotizarInquilino2.Visible = false;
            cotizarInquilino3.Visible = false;
        }
        protected void btnCotizarInquilinoDos_Click(object sender, EventArgs e)
        {

            guardarCotizacion("", "");

            pnlPlanPropietario.Visible = false;
            pnlPlanInquilino.Visible = false;
            cotizarPropietario1.Visible = false;
            cotizarPropietario2.Visible = false;
            cotizarPropietario3.Visible = false;
            cotizarInquilino1.Visible = false;
            cotizarInquilino2.Visible = true;
            cotizarInquilino3.Visible = false;
        }
        protected void btnCotizarInquilinoTres_Click(object sender, EventArgs e)
        {

            guardarCotizacion("", "");

            pnlPlanPropietario.Visible = false;
            pnlPlanInquilino.Visible = false;
            cotizarPropietario1.Visible = false;
            cotizarPropietario2.Visible = false;
            cotizarPropietario3.Visible = false;
            cotizarInquilino1.Visible = false;
            cotizarInquilino2.Visible = false;
            cotizarInquilino3.Visible = true;
        }
        protected void chkAgente_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAgente.Checked)
            {
                txtCodigoAgente.Enabled = true;
            }
            else
            {
                txtCodigoAgente.Text = string.Empty;
                txtCodigoAgente.Enabled = false;
            }
        }
        private void guardarCotizacion(string id_plan_pol, string plan_pol)
        {
            Proceso_Cotizacion clCotizacion = new Proceso_Cotizacion();
            bool resultadoAgente = false;

            int planElegido = clCotizacion.obtenerIdPlanWeb(id_plan_pol, plan_pol);

            if (txtCodigoAgente.Text == string.Empty)
            {
                int numeroCotizacion = Convert.ToInt32(clCotizacion.guardarCotizacion(txtNombreCliente.Text, txtCorreoCliente.Text, Convert.ToInt32(txtTelefonoCliente.Text), DateTime.Now, planElegido, id_plan_pol, plan_pol));

                switch (id_plan_pol + plan_pol)
                {
                    case "HOGARTOT-1COMBO01":
                        mostrarPropietario1();
                        break;
                    case "HOGARTOT-1COMBO02":
                        mostrarPropietario2();
                        break;
                    case "HOGARTOT-1COMBO03":
                        mostrarPropietario3();
                        break;
                }
            }
            else
            {
                resultadoAgente = clCotizacion.ObtenerAgente(txtCodigoAgente.Text);

                if (resultadoAgente == true)
                {
                    int numeroCotizacion = Convert.ToInt32(clCotizacion.guardarCotizacion(txtNombreCliente.Text, txtCorreoCliente.Text, Convert.ToInt32(txtTelefonoCliente.Text), DateTime.Now, planElegido, id_plan_pol, plan_pol, Convert.ToInt32(txtCodigoAgente.Text)));

                    switch (id_plan_pol + plan_pol)
                    {
                        case "HOGARTOT-1COMBO01":
                            mostrarPropietario1();
                            break;
                        case "HOGARTOT-1COMBO02":
                            mostrarPropietario2();
                            break;
                        case "HOGARTOT-1COMBO03":
                            mostrarPropietario3();
                            break;
                    }
                }
                else
                {
                    mostrarMensaje("El codigo de agente no es valido.");
                }
            }
        }
        protected void regresarPropietarios()
        {
            pnlPlanPropietario.Visible = true;
            pnlPlanInquilino.Visible = false;
            cotizarPropietario1.Visible = false;
            cotizarPropietario2.Visible = false;
            cotizarPropietario3.Visible = false;
            cotizarInquilino1.Visible = false;
            cotizarInquilino2.Visible = false;
            cotizarInquilino3.Visible = false;
        }
        protected void regresarInqulinos()
        {
            pnlPlanPropietario.Visible = false;
            pnlPlanInquilino.Visible = true;
            cotizarPropietario1.Visible = false;
            cotizarPropietario2.Visible = false;
            cotizarPropietario3.Visible = false;
            cotizarInquilino1.Visible = false;
            cotizarInquilino2.Visible = false;
            cotizarInquilino3.Visible = false;
        }
        protected void mostrarPropietario1()
        {
            pnlPlanPropietario.Visible = false;
            pnlPlanInquilino.Visible = false;
            cotizarPropietario1.Visible = true;
            cotizarPropietario2.Visible = false;
            cotizarPropietario3.Visible = false;
            cotizarInquilino1.Visible = false;
            cotizarInquilino2.Visible = false;
            cotizarInquilino3.Visible = false;
        }
        protected void mostrarPropietario2()
        {
            pnlPlanPropietario.Visible = false;
            pnlPlanInquilino.Visible = false;
            cotizarPropietario1.Visible = false;
            cotizarPropietario2.Visible = true;
            cotizarPropietario3.Visible = false;
            cotizarInquilino1.Visible = false;
            cotizarInquilino2.Visible = false;
            cotizarInquilino3.Visible = false;
        }
        protected void mostrarPropietario3()
        {
            pnlPlanPropietario.Visible = false;
            pnlPlanInquilino.Visible = false;
            cotizarPropietario1.Visible = false;
            cotizarPropietario2.Visible = false;
            cotizarPropietario3.Visible = true;
            cotizarInquilino1.Visible = false;
            cotizarInquilino2.Visible = false;
            cotizarInquilino3.Visible = false;
        }
        //retorna a mostrar los planes de propietarios despues de haber seleccionado uno
        protected void regresar_Click1(object sender, EventArgs e)
        {
            regresarPropietarios();
        }

        //retorna a mostrar los planes de inquilinos despues de haber seleccionado uno
        protected void regresar_ClickInquilino(object sender, EventArgs e)
        {
            regresarInqulinos();
        }
        private void mostrarMensaje(string mensaje)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "mensaje", "alert('" + mensaje + "');", true);
        }
    */
    }

}