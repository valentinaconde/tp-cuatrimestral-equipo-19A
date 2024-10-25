using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class ConfiguracionCategorias : Page
    {
        private int? CategoriaId
        {
            get { return ViewState["CategoriaId"] as int?; }
            set { ViewState["CategoriaId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarCategorias();
            }
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            Categoria nuevaCategoria = new Categoria
            {
                nombre = txtNombreCategoria.Text
            };

            if (CategoriaId.HasValue)
            {
                nuevaCategoria.id = CategoriaId.Value;
                categoriaNegocio.modificar(nuevaCategoria.id, nuevaCategoria.nombre);
                lblMessage.Text = "Categoría modificada exitosamente.";
                btnAgregarCategoria.Text = "Agregar Categoría";
                CategoriaId = null;
            }
            else
            {
                categoriaNegocio.agregar(nuevaCategoria.nombre);
                lblMessage.Text = "Categoría agregada exitosamente.";
            }

            limpiarFormulario();
            cargarCategorias();
        }

        protected void categoriasGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            if (e.CommandName == "editar")
            {
                Categoria categoria = categoriaNegocio.buscarCategoriaPorId(id);
                if (categoria.nombre != null)
                {
                    txtNombreCategoria.Text = categoria.nombre;
                    CategoriaId = categoria.id;
                    btnAgregarCategoria.Text = "Modificar Categoría";
                }
            }
            else if (e.CommandName == "eliminar")
            {
                categoriaNegocio.eliminar(id);
                lblMessage.Text = "Categoría eliminada exitosamente.";
                cargarCategorias();
            }
        }

        private void cargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            CategoriasGridView.DataSource = categoriaNegocio.listar();
            CategoriasGridView.DataBind();
        }

        private void limpiarFormulario()
        {
            txtNombreCategoria.Text = string.Empty;
            lblMessage.Text = string.Empty;
            lblMessage2.Text = string.Empty;
        }
    }
}
