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
               

                dtProductos = new DataTable();
                dtProductos.Columns.Add("Producto");
                dtProductos.Columns.Add("Cantidad");
                dtProductos.Columns.Add("Precio");
                ViewState["dtProductos"] = dtProductos;
            }
            else
            {
                dtProductos = (DataTable)ViewState["dtProductos"];
            }
            listarDropdowns();
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

            if(txtProducto.Text == string.Empty || txtCantidad.Text == string.Empty || txtPrecio.Text == string.Empty || txtFecha.Text == string.Empty)
            {
                txtErrorCompras.Text = "Debe completar todos los campos.";
                return;
            }
            DataRow dr = dtProductos.NewRow();
            dr["Producto"] = txtProducto.Text;
            dr["Cantidad"] = txtCantidad.Text;
            dr["Precio"] = txtPrecio.Text;
            dtProductos.Rows.Add(dr);

            ViewState["dtProductos"] = dtProductos;
            gvProductos.DataSource = dtProductos;
            gvProductos.DataBind();

            
            txtProducto.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtErrorCompras.Text = "";
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
                    producto.idmarca = int.Parse(ddlMarca.SelectedValue);

                    producto = productoNegocio.buscarProductoPorNombreYMarca(producto.nombre, producto.idmarca);
                    if (producto.id > 0)
                    {
                        producto.stockactual += int.Parse(row["Cantidad"].ToString());
                        producto.precio_unitario = int.Parse(row["Precio"].ToString());
                        producto.nombre = row["Producto"].ToString();

                        productoNegocio.modificar(producto);

                    }
                    else
                    {
                        producto.stockactual = int.Parse(row["Cantidad"].ToString());
                        producto.precio_unitario = int.Parse(row["Precio"].ToString());
                        producto.nombre = row["Producto"].ToString();
                        producto.idcategoria = int.Parse(ddlCategoria.SelectedValue);
                        producto.idmarca = int.Parse(ddlMarca.SelectedValue);
                        productoNegocio.agregar(producto);

                    }

                    DetalleCompra detalle = new DetalleCompra
                    {
                        Cantidad = int.Parse(row["Cantidad"].ToString()),
                        PrecioUnitario = float.Parse(row["Precio"].ToString()),
                        ProductoId = obtenerProductoId(row["Producto"].ToString()),
                    };
                    detalles.Add(detalle);
                    total += detalle.Cantidad * detalle.PrecioUnitario;
                }

                compraNegocio.agregar(fecha, total, proveedorID, detalles);


                dtProductos.Clear();
                ViewState["dtProductos"] = dtProductos;

                gvProductos.DataSource = dtProductos;
                gvProductos.DataBind();

                //txtErrorCompras.Text = "Agregado con exito";
            }
            catch (Exception ex)
            {
                txtErrorCompras.Text = "Error al registrar la compra" + ex;
            }
            
        }

        private int obtenerProductoId(string nombreProducto)
        {
            // Implementa la lógica para obtener el ID del producto basado en su nombre
            // Esto puede implicar una consulta a la base de datos
            return 1; // Placeholder
        }
    }
}
