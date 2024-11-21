using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.Collections.Generic;

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
                    cuit = txtCuitProveedor.Text,
                    activo = true
                };

                if (ProveedorId.HasValue)
                {
                    nuevoProveedor.id = ProveedorId.Value;
                    proveedorNegocio.modificar(nuevoProveedor);
                    lblMessage.Text = "Proveedor modificado exitosamente.";
                    lblMessage.CssClass = "text-success";
                    btnAgregarProveedor.Text = "Agregar Proveedor";
                    ProveedorId = null;
                }
                else
                {
                    Proveedor proveedorActual = new Proveedor();
                    proveedorActual = proveedorNegocio.buscarProveedorPorCuit(nuevoProveedor.cuit);

                    if (proveedorActual.nombre != null && proveedorActual.activo == true)
                    {

                        lblMessage.Text = "El proveedor ya existe.";
                        lblMessage.CssClass = "text-danger";

                    }
                    else if (proveedorActual.nombre != null && proveedorActual.activo == false)
                    {
                        proveedorNegocio.activarProveedor(proveedorActual.cuit, nuevoProveedor.direccion, nuevoProveedor.telefono, nuevoProveedor.email);
                        lblMessage.Text = "Proveedor agregado exitosamente.";
                        lblMessage.CssClass = "text-success";
                    }
                    else
                    {
                        proveedorNegocio.agregar(nuevoProveedor);
                        lblMessage.Text = "Proveedor agregado exitosamente.";
                        lblMessage.CssClass = "text-success";



                    }
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
                txtNombreProveedor.Enabled = false;
                txtCuitProveedor.Enabled = false;
                Proveedor proveedor = proveedorNegocio.buscarProveedorPorId(id);
                if (proveedor != null && proveedor.activo == true)
                {
                    txtNombreProveedor.Text = proveedor.nombre;
                    txtDireccionProveedor.Text = proveedor.direccion;
                    txtTelefonoProveedor.Text = proveedor.telefono;
                    txtEmailProveedor.Text = proveedor.email;
                    txtCuitProveedor.Text = proveedor.cuit;

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
                lblMessage.CssClass = "text-success";

                cargarProveedores();
            }
        }

        private void cargarProveedores()
        {
            ProveedorNegocio proveedornegocio = new ProveedorNegocio();
            List<Proveedor> proveedores = proveedornegocio.listar();

            Session["listaProveedores"] = proveedores;

            if (proveedores.Count == 0)
            {
                lblNoResults.Text = "No se encontraron proveedores.";
                lblNoResults.CssClass = "text-dark";

                lblNoResults.Visible = true;
            }
            else
            {
                lblNoResults.Visible = false;
            }

            ProveedoresGridView.DataSource = proveedores;
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
            txtNombreProveedor.Enabled = true;
            txtCuitProveedor.Enabled = true;
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

        protected void Buscar_TextChanged(object sender, EventArgs e)
        {
            List<Proveedor> listaProveedores = (List<Proveedor>)Session["listaProveedores"];
            List<Proveedor> listaFiltrada = listaProveedores.FindAll(x => x.nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));

            if (listaFiltrada.Count > 0)
            {
                ProveedoresGridView.DataSource = listaFiltrada;
                ProveedoresGridView.DataBind();
                lblNoResults.Visible = false;
            }
            else
            {
                ProveedoresGridView.DataSource = null;
                ProveedoresGridView.DataBind();
                lblNoResults.CssClass = "text-dark";
                lblNoResults.Text = "No se encontraron proveedores.";
                lblNoResults.CssClass = "text-dark";
                lblNoResults.Visible = true;
            }

            if (string.IsNullOrEmpty(txtFiltro.Text))
            {
                cargarProveedores();
                lblNoResults.Visible = false;
            }
        }
    }
}
