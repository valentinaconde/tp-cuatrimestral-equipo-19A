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
            List<Venta> ventas = ventanegocio.listar();

            Session["listaVentas"] = ventas;

            if (ventas.Count == 0)
            {
                lblNoResults.Text = "No se encontraron Facturas.";
                lblNoResults.CssClass = "text-dark";

                lblNoResults.Visible = true;
            }
            else
            {
                lblNoResults.Visible = false;
            }

            FacturasGridView.DataSource = ventas;
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
        private string GenerarFacturaHtml(Venta factura)
        {

            return $@"
     <!DOCTYPE html>
     <html lang='es'>
     <head>
         <meta charset='UTF-8'>
         <meta name='viewport' content='width=device-width, initial-scale=1.0'>
         <title>Factura N° {factura.numero_factura}</title>
         <style>
            body {{ font-family: Arial, sans-serif; margin: 20px; line-height: 1.6; }}
           header {{text-align: center; margin-bottom: 20px; }}
         .detalles {{ margin-top: 20px;}}
          .detalles p {{ margin: 5px 0;}}
         hr {{ border: 1px solid #ddd; margin: 20px 0; }}
         .footer {{text-align: center; font-weight: bold; margin-top: 20px; }}
         </style>
     </head>
     <body>
         <header>
         <h2>Factura N° {factura.numero_factura}</h2>
         </header>
         <section class='detalles'>
         <p><strong>Fecha:</strong> {factura.fecha:dd/MM/yyyy}</p>
         <p><strong>Cliente:</strong> {factura.cliente_id}</p>
         <p><strong>Usuario:</strong> {factura.usuario_id}</p>
         <p><strong>Total:</strong> ${factura.total}</p>
         </section>
         <hr />
         <div class='footer'>
         <p>Gracias por su compra<p>
         </div>
     </body>
     </html>";
        }
        protected void btnImprimirFactura_Click(object sender, EventArgs e)
        {
            Button btnImprimir = (Button)sender;
            GridViewRow fila = (GridViewRow)btnImprimir.NamingContainer;
            int facturaId = Convert.ToInt32(FacturasGridView.DataKeys[fila.RowIndex].Value);

            VentaNegocio ventaNegocio = new VentaNegocio();
            Venta factura = ventaNegocio.BuscarPorId(facturaId);

            if (factura != null)
            {

                string facturaHtml = GenerarFacturaHtml(factura).Replace("'", "\\'").Replace("\n", "").Replace("\r", "");


                string script = $@"
         var printWindow = window.open('', '', 'height=650,width=900');
         printWindow.document.write('<html><head><title>Factura</title></head><body>');
         printWindow.document.write('{facturaHtml}');
         printWindow.document.write('</body></html>');
         printWindow.document.close();
         printWindow.print();";


                ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenFacturaPrintWindow", script, true);
            }
            else
            {
                Response.Write("Factura no encontrada.");
            }
        }
    }
}