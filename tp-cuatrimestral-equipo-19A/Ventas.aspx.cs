using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class Ventas : Page
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

                dtProductos = new DataTable();
                dtProductos.Columns.Add("ProductoID");
                dtProductos.Columns.Add("Producto");
                dtProductos.Columns.Add("Cantidad");
                dtProductos.Columns.Add("Precio");
                dtProductos.Columns.Add("Categoria");
                ViewState["dtProductos"] = dtProductos;
                listarDropdowns();
                CalculatePrice();
            }
            else
            {
                dtProductos = ViewState["dtProductos"] as DataTable;
                if (dtProductos == null)
                {
                    dtProductos = new DataTable();
                    dtProductos.Columns.Add("ProductoID");
                    dtProductos.Columns.Add("Producto");
                    dtProductos.Columns.Add("Cantidad");
                    dtProductos.Columns.Add("Precio");
                    dtProductos.Columns.Add("Categoria");
                    ViewState["dtProductos"] = dtProductos;
                }
            }
        }

        protected void listarDropdowns()
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            ddlCliente.DataSource = clienteNegocio.listar();
            ddlCliente.DataTextField = "Nombre";
            ddlCliente.DataValueField = "Id";
            ddlCliente.DataBind();

            ProductoNegocio productoNegocio = new ProductoNegocio();
            List<Producto> productos = productoNegocio.listar();

            List<ListItem> items = new List<ListItem>();

            MarcaNegocio marcaNegocio = new MarcaNegocio();

            foreach (var producto in productos)
            {
                Marca marca = marcaNegocio.buscarMarcaPorId(producto.idmarca);
                string text = $"{producto.nombre} - {marca.nombre}";
                items.Add(new ListItem(text, producto.id.ToString()));
            }

            ddlProducto.DataSource = items;
            ddlProducto.DataTextField = "Text";
            ddlProducto.DataValueField = "Value";
            ddlProducto.DataBind();

        }
        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculatePrice();
            string productoSeleccionado = ddlProducto.SelectedValue;
            Producto producto = new Producto();
            ProductoNegocio productoNegocio = new ProductoNegocio();
            producto = productoNegocio.buscarProductoPorId(int.Parse(ddlProducto.SelectedValue));

            Marca marca = new Marca();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            marca = marcaNegocio.buscarMarcaPorId(producto.idmarca);
            
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            CalculatePrice();
        }
        private void CalculatePrice()
        {

            if (ddlProducto.SelectedItem != null)
            {
                ProductoNegocio productoNegocio = new ProductoNegocio();
                Producto producto = productoNegocio.buscarProductoPorId(int.Parse(ddlProducto.SelectedValue));
                if (producto != null)
                {
                    float precio_unitario = producto.precio_unitario;
                    float porcentaje = producto.ganancia / 100;
                    float precio_final = (precio_unitario + (precio_unitario * porcentaje))  ;
                    txtPrecio.Text = precio_final.ToString();
                }
            }
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (ddlProducto.SelectedItem == null || txtCantidad.Text == string.Empty || txtPrecio.Text == string.Empty || txtFecha.Text == string.Empty)
            {
                txtErrorVentas.Text = "Debe completar todos los campos.";
                return;
            }

            DataRow dr = dtProductos.NewRow();
            dr["ProductoID"] = ddlProducto.SelectedValue;
            dr["Producto"] = ddlProducto.SelectedItem.Text;
            dr["Cantidad"] = txtCantidad.Text;
            dr["Precio"] = txtPrecio.Text;
            dtProductos.Rows.Add(dr);

            ViewState["dtProductos"] = dtProductos;
            gvProductos.DataSource = dtProductos;
            gvProductos.DataBind();

            ddlProducto.ClearSelection();
            txtCantidad.Text = "1";
            txtErrorVentas.Text = "";
        }

        protected void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtProductos.Rows.Count == 0)
                {
                    txtErrorVentas.Text = "Debe agregar al menos un producto.";
                    return;
                }

                VentaNegocio ventaNegocio = new VentaNegocio();
                DateTime fecha;
                if (!DateTime.TryParseExact(txtFecha.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out fecha))
                {
                    txtErrorVentas.Text = "Formato de fecha inválido. Use el formato dd/MM/yyyy.";
                    return;
                }

                float total = 0;
                int clienteID = int.Parse(ddlCliente.SelectedValue);

                List<DetalleVenta> detalles = new List<DetalleVenta>();
                foreach (DataRow row in dtProductos.Rows)
                {
                    ProductoNegocio productoNegocio = new ProductoNegocio();
                    Producto producto = productoNegocio.buscarProductoPorId(int.Parse(row["ProductoID"].ToString())); 

                    if (producto.id > 0 && producto.activo == true)
                    {
                        if (producto.stockactual < int.Parse(row["Cantidad"].ToString()))
                        {
                            txtErrorVentas.Text = "No hay suficiente stock para el producto " + producto.nombre;
                            return;
                        }

                        producto.stockactual -= int.Parse(row["Cantidad"].ToString());
                        productoNegocio.modificar(producto);

                        DetalleVenta detalle = new DetalleVenta
                        {
                            Cantidad = int.Parse(row["Cantidad"].ToString()),
                            PrecioUnitario = float.Parse(row["Precio"].ToString()),
                            Producto = producto,
                        };
                        detalles.Add(detalle);
                        total += detalle.Cantidad * detalle.PrecioUnitario;
                    }
                    else
                    {
                        txtErrorVentas.Text = "Producto no encontrado: " + row["Producto"].ToString();
                        return;
                    }
                }
                Usuario usuario = new Usuario();
                usuario = (Usuario)Session["UsuarioActual"];
                if (usuario == null)
                {
                    usuario = new Usuario();
                    usuario.nombre = "";
                }

                ventaNegocio.agregar(fecha, total, clienteID, usuario.id, detalles);

                dtProductos.Clear();
                ViewState["dtProductos"] = dtProductos;

                gvProductos.DataSource = dtProductos;
                gvProductos.DataBind();

                txtErrorVentas.Text = "Venta registrada con éxito";
                limpiarFormulario();
            }
            catch (Exception ex)
            {
                txtErrorVentas.Text = "Error al registrar la venta: " + ex;
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            DataTable dtProductos = ViewState["dtProductos"] as DataTable;

            if (dtProductos != null)
            {
                Button btnEliminar = (Button)sender;
                GridViewRow row = (GridViewRow)btnEliminar.NamingContainer;
                int Index = row.RowIndex;
                dtProductos.Rows.RemoveAt(Index);

                ViewState["dtProductos"] = dtProductos;
                gvProductos.DataSource = dtProductos;
                gvProductos.DataBind();
            }
        }
        private void limpiarFormulario()
        {
            txtFecha.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            ddlCliente.SelectedIndex = 0;
            ddlProducto.SelectedIndex = 0;
        }
        protected void gvProductos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
