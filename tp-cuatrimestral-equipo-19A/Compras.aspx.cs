using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using Dominio;
using Negocio;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class Compras : Page
    {
        private DataTable dtProductos;

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

                dtProductos = new DataTable();
                dtProductos.Columns.Add("Producto");
                dtProductos.Columns.Add("Cantidad");
                dtProductos.Columns.Add("Porcentaje");
                dtProductos.Columns.Add("Precio");
                dtProductos.Columns.Add("Categoria");
                dtProductos.Columns.Add("Marca");
                ViewState["dtProductos"] = dtProductos;
                listarDropdowns();
            }
            else
            {
                dtProductos = (DataTable)ViewState["dtProductos"];
            }
            
        }

        protected void listarDropdowns()
        {
            
            ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
            ddlProveedor.DataSource = proveedorNegocio.listar();
            ddlProveedor.DataTextField = "Nombre";
            ddlProveedor.DataValueField = "Id";
            ddlProveedor.DataBind();

            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            ddlCategoria.DataSource = categoriaNegocio.listar();
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();

            MarcaNegocio marcaNegocio = new MarcaNegocio();
            ddlMarca.DataSource = marcaNegocio.listar();
            ddlMarca.DataTextField = "Nombre";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();

        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {

            if(txtProducto.Text == string.Empty || txtPorcentaje.Text == string.Empty || txtCantidad.Text == string.Empty || txtPrecio.Text == string.Empty || txtFecha.Text == string.Empty)
            {
                txtErrorCompras.Text = "Debe completar todos los campos.";
                txtErrorCompras.CssClass = "text-danger";
                return;
            }
            DataRow dr = dtProductos.NewRow();
            dr["Producto"] = txtProducto.Text;
            dr["Cantidad"] = txtCantidad.Text;
            dr["Precio"] = txtPrecio.Text;
            dr["Porcentaje"] = txtPorcentaje.Text;
            dr["Categoria"] = ddlCategoria.SelectedValue;
            dr["Marca"] = ddlMarca.SelectedValue;
            dtProductos.Rows.Add(dr);

            ViewState["dtProductos"] = dtProductos;
            gvProductos.DataSource = dtProductos;
            gvProductos.DataBind();

            
            txtProducto.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtPorcentaje.Text = string.Empty;
            txtErrorCompras.Text = "";
            ddlMarca.SelectedIndex = 0;
            ddlCategoria.SelectedIndex = 0;


        }

        protected void btnRegistrarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtProductos.Rows.Count == 0)
                {
                    txtErrorCompras.Text = "Debe agregar al menos un producto.";
                    return;
                }

                CompraNegocio compraNegocio = new CompraNegocio();
                DateTime fecha;
                if (!DateTime.TryParseExact(txtFecha.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out fecha))
                {
                    txtErrorCompras.Text = "Formato de fecha inválido. Use el formato dd/MM/yyyy.";
                    return;
                }
                float total = 0;
                int proveedorID = int.Parse(ddlProveedor.SelectedValue);

                List<DetalleCompra> detalles = new List<DetalleCompra>();
                foreach (DataRow row in dtProductos.Rows)
                {
                    ProductoNegocio productoNegocio = new ProductoNegocio();
                    Producto producto = new Producto();
                    producto.nombre = row["Producto"].ToString();
                    producto.idmarca = Convert.ToInt32(row["Marca"]);

                    producto = productoNegocio.buscarProductoPorNombreYMarca(producto.nombre, producto.idmarca);
                    if (producto.id > 0 && producto.activo == true)
                    {
                        producto.stockactual += int.Parse(row["Cantidad"].ToString());
                        producto.precio_unitario = int.Parse(row["Precio"].ToString());
                        producto.ganancia = float.Parse(row["Porcentaje"].ToString());
                        producto.nombre = row["Producto"].ToString();

                        productoNegocio.modificar(producto);

                    }
                    else
                    {
                        producto.stockactual = int.Parse(row["Cantidad"].ToString());
                        producto.precio_unitario = int.Parse(row["Precio"].ToString());
                        producto.ganancia = float.Parse(row["Porcentaje"].ToString());
                        producto.nombre = row["Producto"].ToString();
                        producto.idcategoria = Convert.ToInt32(row["Categoria"]);
                        producto.idmarca = Convert.ToInt32(row["Marca"]);
                        producto.activo = true;
                        productoNegocio.agregar(producto);
                        producto = productoNegocio.buscarProductoPorNombreYMarca(producto.nombre, producto.idmarca);


                    }

                    DetalleCompra detalle = new DetalleCompra
                    {
                        Cantidad = int.Parse(row["Cantidad"].ToString()),
                        PrecioUnitario = float.Parse(row["Precio"].ToString()),
                        ProductoId = producto.id,
                    };
                    detalles.Add(detalle);
                    total += detalle.Cantidad * detalle.PrecioUnitario;
                }

                compraNegocio.agregar(fecha, total, proveedorID, detalles);


                dtProductos.Clear();
                ViewState["dtProductos"] = dtProductos;

                gvProductos.DataSource = dtProductos;
                gvProductos.DataBind();

                txtErrorCompras.Text = "Agregado con exito";
                txtErrorCompras.CssClass = "text-success";
            }
            catch (Exception ex)
            {
                txtErrorCompras.Text = "Error al registrar la compra" + ex;
                txtErrorCompras.CssClass = "text-danger";
            }
            
        }
    }
}
