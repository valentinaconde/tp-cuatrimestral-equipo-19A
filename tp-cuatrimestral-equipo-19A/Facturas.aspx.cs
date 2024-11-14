using Dominio;
using Negocio;
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
        private int? ProductoId
        {
            get { return ViewState["FacturaId"] as int?; }
            set { ViewState["FacturaId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarFacturas();
            }

        }
        protected void facturasGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page") return;
            int id = Convert.ToInt32(e.CommandArgument);
            VentaNegocio ventanegocio = new VentaNegocio();

            if (e.CommandName == "generar")
            {
               
            }

        }
        private void cargarFacturas()
        {

            VentaNegocio ventanegocio = new VentaNegocio();
            FacturasGridView.DataSource = ventanegocio.listar();
            FacturasGridView.DataBind();
            UpdatePagerInfo();
        }
        protected void FacturasGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FacturasGridView.PageIndex = e.NewPageIndex;
            cargarFacturas();
            UpdatePagerInfo();
        }

        private void UpdatePagerInfo()
        {
            GridViewRow pagerRow = FacturasGridView.BottomPagerRow;
            if (pagerRow != null)
            {
                Label lblPageInfo = (Label)pagerRow.FindControl("lblPageInfo");
                if (lblPageInfo != null)
                {
                    lblPageInfo.Text = $"Página {FacturasGridView.PageIndex + 1} de {FacturasGridView.PageCount}";
                }
            }
        }
    }
}