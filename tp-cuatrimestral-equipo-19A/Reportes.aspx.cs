using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = new Usuario();
                usuario = (Usuario)Session["UsuarioActual"];
                if (usuario == null)
                {
                    Response.Redirect("Default.aspx");

                }
            }

        }
        protected void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            // Lógica para obtener los datos filtrados y llenar el GridView.


        }

        protected void btnExportarPDF_Click(object sender, EventArgs e)
        {

        }

        protected void btnExportarExcel_Click(object sender, EventArgs e)
        {

        }
    }
}