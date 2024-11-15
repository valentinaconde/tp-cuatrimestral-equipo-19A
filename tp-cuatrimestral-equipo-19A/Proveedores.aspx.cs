using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class Proveedores : Page
    {
        private int? ProveedorId
        {
            get { return ViewState["ProveedorId"] as int?; }
            set { ViewState["ProveedorId"] = value; }
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
                cargarProveedores();
            }
        }

        protected void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
                Proveedor nuevoProveedor = new Proveedor
                {
                    nombre = txtNombreProveedor.Text,
                    direccion = txtDireccionProveedor.Text,
                    telefono = txtTelefonoProveedor.Text,
                    email = txtEmailProveedor.Text,
                    cuit = txtCuitProveedor.Text // Nuevo campo
                };

                if (ProveedorId.HasValue)
                {
                    nuevoProveedor.id = ProveedorId.Value;
                    proveedorNegocio.modificar(nuevoProveedor);
                    lblMessage.Text = "Proveedor modificado exitosamente.";
                    btnAgregarProveedor.Text = "Agregar Proveedor";
                    ProveedorId = null;
                }
                else
                {
                    proveedorNegocio.agregar(nuevoProveedor);
                    lblMessage.Text = "Proveedor agregado exitosamente.";
                }

                limpiarFormulario();
                cargarProveedores();
            }
        }

        protected void ProveedoresGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblMessage.Text = string.Empty;
            lblMessage2.Text = string.Empty;
            if (e.CommandName == "Page") return;
            int id = Convert.ToInt32(e.CommandArgument);
            ProveedorNegocio proveedorNegocio = new ProveedorNegocio();

            if (e.CommandName == "editar")
            {
                Proveedor proveedor = proveedorNegocio.buscarProveedorPorId(id);
                if (proveedor != null)
                {
                    txtNombreProveedor.Text = proveedor.nombre;
                    txtDireccionProveedor.Text = proveedor.direccion;
                    txtTelefonoProveedor.Text = proveedor.telefono;
                    txtEmailProveedor.Text = proveedor.email;
                    txtCuitProveedor.Text = proveedor.cuit; // Nuevo campo

                    ProveedorId = proveedor.id;
                    btnAgregarProveedor.Text = "Modificar Proveedor";
                }
                else
                {
                    lblMessage2.Text = "Error al cargar el proveedor.";
                }
            }

            else if (e.CommandName == "eliminar")
            {
                proveedorNegocio.eliminar(id);
                lblMessage.Text = "Proveedor eliminado exitosamente.";
                cargarProveedores();
            }
        }

        private void cargarProveedores()
        {
            ProveedorNegocio proveedorNegocio = new ProveedorNegocio();
            ProveedoresGridView.DataSource = proveedorNegocio.listar();
            ProveedoresGridView.DataBind();
            UpdatePagerInfo();
        }

        private void limpiarFormulario()
        {
            txtNombreProveedor.Text = string.Empty;
            txtDireccionProveedor.Text = string.Empty;
            txtTelefonoProveedor.Text = string.Empty;
            txtEmailProveedor.Text = string.Empty;
            txtCuitProveedor.Text = string.Empty;
        }

        protected void ProveedoresGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ProveedoresGridView.PageIndex = e.NewPageIndex;
            cargarProveedores();
            UpdatePagerInfo();
        }

        private void UpdatePagerInfo()
        {
            GridViewRow pagerRow = ProveedoresGridView.BottomPagerRow;
            if (pagerRow != null)
            {
                Label lblPageInfo = (Label)pagerRow.FindControl("lblPageInfo");
                if (lblPageInfo != null)
                {
                    lblPageInfo.Text = $"Página {ProveedoresGridView.PageIndex + 1} de {ProveedoresGridView.PageCount}";
                }
            }
        }
    }
}
