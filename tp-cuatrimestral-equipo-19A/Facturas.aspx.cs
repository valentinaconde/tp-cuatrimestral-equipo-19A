using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

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

                List <DetalleVenta> detalleVentas = ventaNegocio.listarDetallesFactura(facturaId.ToString());
                Cliente cliente = new Cliente();
                ClienteNegocio clienteNegocio = new ClienteNegocio();
                cliente =  clienteNegocio.buscarClientePorId(factura.cliente_id);

                Usuario usuario = new Usuario();
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                usuario = usuarioNegocio.buscarUsuarioPorId(factura.usuario_id);




                Document pdfDoc = new Document(PageSize.A4);
                MemoryStream ms = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                pdfDoc.Open();

                pdfDoc.Add(new Paragraph($"Factura N°: {factura.numero_factura}"));
                pdfDoc.Add(new Paragraph($"Fecha: {factura.fecha:dd/MM/yyyy}"));
                pdfDoc.Add(new Paragraph($"DNI Cliente: {cliente.dni}"));
                pdfDoc.Add(new Paragraph($"Nombre Cliente: {cliente.nombre}"));
                pdfDoc.Add(new Paragraph($"Vendedor ID: {usuario.id}"));
                pdfDoc.Add(new Paragraph($"Nombre vendedor: {usuario.nombre} {usuario.apellido}"));
                pdfDoc.Add(new Paragraph(" "));

                PdfPTable table = new PdfPTable(3);

                PdfPCell headerCell = new PdfPCell();
                headerCell.Padding = 5;

                headerCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;

                headerCell.Phrase = new Phrase("Producto");
                table.AddCell(headerCell);

                headerCell.Phrase = new Phrase("Cantidad");
                table.AddCell(headerCell);

                headerCell.Phrase = new Phrase("Precio Unitario");
                table.AddCell(headerCell);

                PdfPCell dataCell = new PdfPCell();
                dataCell.Padding = 5;

                foreach (var detalle in detalleVentas)
                {
                    dataCell.Phrase = new Phrase(detalle.Producto.nombre);
                    table.AddCell(dataCell);

                    dataCell.Phrase = new Phrase(detalle.Cantidad.ToString());
                    table.AddCell(dataCell);

                    dataCell.Phrase = new Phrase(detalle.PrecioUnitario.ToString("C"));
                    table.AddCell(dataCell);
                }

                pdfDoc.Add(table);

                pdfDoc.Add(new Paragraph(" "));
                pdfDoc.Add(new Paragraph($"Total: ${factura.total}"));
                pdfDoc.Add(new Paragraph("Gracias por su compra."));

                pdfDoc.Close();
                writer.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", $"attachment; filename=Factura_{factura.numero_factura}.pdf");
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
            else
            { 
                Response.Write("Factura no encontrada.");
            }

        }
        private string GenerarFacturaHtml(Venta factura, Cliente cliente, Usuario usuario, List<DetalleVenta> detalleVentas)
        {
            string detallesHtml = "";
            foreach (var detalle in detalleVentas)
            {
                detallesHtml += $@"
                <tr>
                    <td>{detalle.Producto.nombre}</td>
                    <td>{detalle.Cantidad}</td>
                    <td>{detalle.PrecioUnitario:C}</td>
                </tr>";
            }

            return $@"
     <!DOCTYPE html>
     <html lang='es'>
     <head>
         <meta charset='UTF-8'>
         <meta name='viewport' content='width=device-width, initial-scale=1.0'>
         <title>Factura N° {factura.numero_factura}</title>
         <style>
            body {{ font-family: Arial, sans-serif; margin: 20px; line-height: 1.6; }}
            header {{ text-align: center; margin-bottom: 20px; }}
            .detalles {{ margin-top: 20px; }}
            .detalles p {{ margin: 5px 0; }}
            hr {{ border: 1px solid #ddd; margin: 20px 0; }}
            .footer {{ text-align: center; font-weight: bold; margin-top: 20px; }}
            table {{ width: 100%; border-collapse: collapse; margin-top: 20px; }}
            table, th, td {{ border: 1px solid black; }}
            th, td {{ padding: 10px; text-align: left; }}
         </style>
     </head>
     <body>
         <header>
             <h2>Factura N° {factura.numero_factura}</h2>
         </header>
         <section class='detalles'>
             <p><strong>Fecha:</strong> {factura.fecha:dd/MM/yyyy}</p>
             <p><strong>DNI Cliente:</strong> {cliente.dni}</p>
             <p><strong>Nombre Cliente:</strong> {cliente.nombre}</p>
             <p><strong>Vendedor ID:</strong> {usuario.id}</p>
             <p><strong>Nombre Vendedor:</strong> {usuario.nombre} {usuario.apellido}</p>
         </section>
         <hr />
         <table>
             <thead>
                 <tr>
                     <th>Producto</th>
                     <th>Cantidad</th>
                     <th>Precio Unitario</th>
                 </tr>
             </thead>
             <tbody>
                 {detallesHtml}
             </tbody>
         </table>
         <hr />
         <p><strong>Total:</strong> ${factura.total}</p>
         <div class='footer'>
             <p>Gracias por su compra</p>
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
                List<DetalleVenta> detalleVentas = ventaNegocio.listarDetallesFactura(facturaId.ToString());
                Cliente cliente = new Cliente();
                ClienteNegocio clienteNegocio = new ClienteNegocio();
                cliente = clienteNegocio.buscarClientePorId(factura.cliente_id);

                Usuario usuario = new Usuario();
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                usuario = usuarioNegocio.buscarUsuarioPorId(factura.usuario_id);

                string facturaHtml = GenerarFacturaHtml(factura, cliente, usuario, detalleVentas).Replace("'", "\\'").Replace("\n", "").Replace("\r", "");




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