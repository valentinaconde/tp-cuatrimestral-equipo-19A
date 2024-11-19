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
                activo = true 
            };

            if (ProductoId.HasValue)
            {
                nuevoProducto.id = ProductoId.Value;
                productonegocio.modificar(nuevoProducto);
                lblMessage.Text = "Producto modificado exitosamente.";
                lblMessage.CssClass = "text-success";
                btnAgregaProducto.Text = "Agregar Producto";
                ProductoId = null;
            }
            else
            {
                Producto productoActual = new Producto();
                productoActual = productonegocio.buscarProductoPorNombreYMarca(nuevoProducto.nombre, nuevoProducto.idmarca);

                if (productoActual.nombre != null && productoActual.activo == true)
                {

                    lblMessage.Text = "El producto ya existe.";
                    lblMessage.CssClass = "text-danger";

                }
                else if (productoActual.nombre != null && productoActual.activo == false)
                {
                    productonegocio.activarProducto(productoActual.nombre, productoActual.idmarca);
                    lblMessage.Text = "Producto agregado exitosamente.";
                    lblMessage.CssClass = "text-success";
                }
                else
                {
                    productonegocio.agregar(nuevoProducto);
                    lblMessage.Text = "Producto agregado exitosamente.";
                    lblMessage.CssClass = "text-success";


                }
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
                if (producto.activo == true)
                {
                    txtNombreProducto.Text = producto.nombre;
                    txtStockActual.Text = producto.stockactual.ToString();
                    txtPrecioUnitario.Text = producto.precio_unitario.ToString();
                    txtPorcentajeGanancia.Text = producto.ganancia.ToString();


                    if (ddlMarca.Items.FindByValue(producto.idmarca.ToString()) == null)
                    {
                        MarcaNegocio marcaNegocio = new MarcaNegocio();
                        Marca marca = marcaNegocio.buscarMarcaPorId(producto.idmarca);
                        if (marca != null)
                        {
                            ddlMarca.Items.Add(new ListItem(marca.nombre, marca.id.ToString()));
                        }
                    }
                    ddlMarca.SelectedValue = producto.idmarca.ToString();

                    if (ddlCategoria.Items.FindByValue(producto.idcategoria.ToString()) == null)
                    {
                        CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                        Categoria categoria = categoriaNegocio.buscarCategoriaPorId(producto.idcategoria);
                        if (categoria != null)
                        {
                            ddlCategoria.Items.Add(new ListItem(categoria.nombre, categoria.id.ToString()));
                        }
                    }
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
                lblMessage.CssClass = "text-success";

                cargarProductos();
            }

        }
        private void cargarProductos()
        {
            ProductoNegocio productonegocio = new ProductoNegocio();
            List<Producto> productos = productonegocio.listar();

            Session["listaProductos"] = productos;

            if (productos.Count == 0)
            {
                lblNoResults.Text = "No se encontraron productos.";
                lblNoResults.CssClass = "text-dark";

                lblNoResults.Visible = true;
            }
            else
            {
                lblNoResults.Visible = false;
            }

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

        protected void Buscar_TextChanged(object sender, EventArgs e)
        {
            List<Producto> listaProductos = (List<Producto>)Session["listaProductos"];
            List<Producto> listaFiltrada = listaProductos.FindAll(x => x.nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));

            if (listaFiltrada.Count > 0)
            {
                ProductosGridView.DataSource = listaFiltrada;
                ProductosGridView.DataBind();
                lblNoResults.Visible = false;
            }
            else
            {
                ProductosGridView.DataSource = null;
                ProductosGridView.DataBind();
                lblNoResults.Text = "No se encontraron Productos.";
                lblNoResults.CssClass = "text-dark";

                lblNoResults.Visible = true;
            }

            if (string.IsNullOrEmpty(txtFiltro.Text))
            {
                cargarCategorias();
                lblNoResults.Visible = false;
            }
        }
    }
}
