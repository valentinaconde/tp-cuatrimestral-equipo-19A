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
    public partial class Productos : System.Web.UI.Page
    {
        private int? ProductoId
        {
            get { return ViewState["ProductoId"] as int?; }
            set { ViewState["ProductoId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarProductos();
            }

        }
        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            ProductoNegocio productonegocio = new ProductoNegocio();
            Producto nuevoProducto = new Producto
            {
                nombre = txtNombreProducto.Text,
                stockactual = int.Parse(txtStockActual.Text),
                stockminimo = int.Parse(txtStockMinimo.Text),
                ganancia = float.Parse(txtPorcentajeGanancia.Text),
                idmarca = int.Parse(txtMarcaId.Text),
                idcategoria = int.Parse(txtCategoriaId.Text)


            };

            if (ProductoId.HasValue)
            {
                nuevoProducto.id = ProductoId.Value;
                productonegocio.modificar(nuevoProducto);
                lblMessage.Text = "Producto modificado exitosamente.";
                btnAgregaProducto.Text = "Agregar Proveedor";
                ProductoId = null;
            }
            else
            {
                productonegocio.agregar(nuevoProducto);
                lblMessage.Text = "Producto agregado exitosamente.";
            }

            limpiarFormulario();
            cargarProductos();

        }
        protected void productosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            ProductoNegocio productonegocio = new ProductoNegocio();

            if (e.CommandName == "editar")
            {
                Producto producto = productonegocio.buscarProductoPorId(id);
                if (producto != null)
                {
                    txtNombreProducto.Text = producto.nombre;
                    txtStockActual.Text = producto.stockactual.ToString();
                    txtStockMinimo.Text = producto.stockminimo.ToString();
                    txtPorcentajeGanancia.Text = producto.ganancia.ToString();
                    txtMarcaId.Text = producto.idmarca.ToString();
                    txtCategoriaId.Text = producto.idcategoria.ToString();

                    ProductoId = producto.id;
                    btnAgregaProducto.Text = "Modificar Producto";
                }
                else
                {
                    lblMessage2.Text = "Error al cargar el Producto.";
                }
            }
            else if (e.CommandName == "eliminar")
            {
                productonegocio.eliminar(id);
                lblMessage.Text = "Producto eliminado exitosamente.";
                cargarProductos();
            }

        }
        private void cargarProductos()
        {
            
            ProductoNegocio productonegocio = new ProductoNegocio();
            ProductosGridView.DataSource = productonegocio.listar();
            ProductosGridView.DataBind();
        }
        private void limpiarFormulario()
        {
            txtNombreProducto.Text = string.Empty;
            txtStockActual.Text = string.Empty;
            txtStockMinimo.Text = string.Empty;
            txtPorcentajeGanancia.Text = string.Empty;
            txtMarcaId.Text = string.Empty;
            txtCategoriaId.Text = string.Empty;
            lblMessage.Text = string.Empty;
            lblMessage2.Text = string.Empty;
        }
    }
}