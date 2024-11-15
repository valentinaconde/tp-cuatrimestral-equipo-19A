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
            //if (e.CommandName == "Page") return;
            //int id = Convert.ToInt32(e.CommandArgument);
            //VentaNegocio ventanegocio = new VentaNegocio();

            //if (e.CommandName == "generar")
            //{

            //}

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

        protected void FacturasGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnGenerarFactura_Click(object sender, EventArgs e)
        {

            Button btnGenerar = (Button)sender;
            GridViewRow fila = (GridViewRow)btnGenerar.NamingContainer;
            int facturaId = Convert.ToInt32(FacturasGridView.DataKeys[fila.RowIndex].Value);

            VentaNegocio ventaNegocio = new VentaNegocio();
            Venta factura = ventaNegocio.BuscarPorId(facturaId);

            if (factura != null)
            {
                string contenido = $"Factura N°: {factura.numero_factura}\n";
                contenido += $"Fecha: {factura.fecha:dd/MM/yyyy}\n";
                contenido += $"Cliente ID: {factura.cliente_id}\n";
                contenido += $"Usuario ID: {factura.usuario_id}\n";
                contenido += $"Total: ${factura.total}\n";
                contenido += "-------------------------------------\n";
                contenido += "Gracias por su compra.";

                Response.Clear();
                Response.ContentType = "text/plain";
                Response.AddHeader("Content-Disposition", $"attachment; filename=Factura_{factura.numero_factura}.txt");
                Response.Write(contenido);
                Response.End();
            }
            else
            { 
                Response.Write("Factura no encontrada.");
            }

        }
    }
}