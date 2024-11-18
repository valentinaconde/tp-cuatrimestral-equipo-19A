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
                Usuario usuario = new Usuario();
                usuario = (Usuario)Session["UsuarioActual"];
                if (usuario == null)
                {
                    Response.Redirect("Default.aspx");

                }
                if (usuario.rol_id == 2)
                {
                    Response.Redirect("HomeVendedorPage.aspx");
                }
                cargarMarcas();
                cargarCategorias();
                cargarProductos();
            }

        }
        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (ddlMarca.SelectedValue == "0")
            {
                lblMessage.Text = "Por favor, seleccione una marca.";
                return;
            }

            if (ddlCategoria.SelectedValue == "0")
            {
                lblMessage.Text = "Por favor, seleccione una categoria.";
                return;
            }

            ProductoNegocio productonegocio = new ProductoNegocio();
            Producto nuevoProducto = new Producto
            {
                nombre = txtNombreProducto.Text,
                stockactual = int.Parse(txtStockActual.Text),
                precio_unitario = int.Parse(txtPrecioUnitario.Text),
                ganancia = float.Parse(txtPorcentajeGanancia.Text),
                idmarca = int.Parse(ddlMarca.SelectedValue),
                idcategoria = int.Parse(ddlCategoria.SelectedValue),
                activo = true // Set the product as active by default
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
            if (e.CommandName == "Page") return;
            int id = Convert.ToInt32(e.CommandArgument);
            ProductoNegocio productonegocio = new ProductoNegocio();

            if (e.CommandName == "editar")
            {
                Producto producto = productonegocio.buscarProductoPorId(id);
                if (producto != null && producto.activo)
                {
                    txtNombreProducto.Text = producto.nombre;
                    txtStockActual.Text = producto.stockactual.ToString();
                    txtPrecioUnitario.Text = producto.precio_unitario.ToString();
                    txtPorcentajeGanancia.Text = producto.ganancia.ToString();
                    ddlMarca.SelectedValue = producto.idmarca.ToString();
                    ddlCategoria.SelectedValue = producto.idcategoria.ToString();

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
            List<Producto> productos = productonegocio.listar().Where(p => p.activo).ToList();
            ProductosGridView.DataSource = productos;
            ProductosGridView.DataBind();
            UpdatePagerInfo();
        }
        private void cargarMarcas()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            List<Marca> marcas = marcaNegocio.listar();

            ddlMarca.DataSource = marcas;
            ddlMarca.DataTextField = "Nombre";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem(string.Empty, "0"));
        }

        private void cargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            ddlCategoria.DataSource = categoriaNegocio.listar();
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem(string.Empty, "0"));
        }
        private void limpiarFormulario()
        {
            txtNombreProducto.Text = string.Empty;
            txtStockActual.Text = string.Empty;
            txtPrecioUnitario.Text = string.Empty;
            txtPorcentajeGanancia.Text = string.Empty;
            ddlMarca.SelectedValue = "0";
            ddlCategoria.SelectedValue = "0";
            lblMessage.Text = string.Empty;
            lblMessage2.Text = string.Empty;
        }

        protected void ProductosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProductosGridView.PageIndex = e.NewPageIndex;
            cargarProductos();
            UpdatePagerInfo();
        }

        private void UpdatePagerInfo()
        {
            GridViewRow pagerRow = ProductosGridView.BottomPagerRow;
            if (pagerRow != null)
            {
                Label lblPageInfo = (Label)pagerRow.FindControl("lblPageInfo");
                if (lblPageInfo != null)
                {
                    lblPageInfo.Text = $"Página {ProductosGridView.PageIndex + 1} de {ProductosGridView.PageCount}";
                }
            }
        }
    }
}
