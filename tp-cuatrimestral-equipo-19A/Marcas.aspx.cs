using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class ConfiguracionMarcas : Page
    {
        private int? MarcaId
        {
            get { return ViewState["MarcaId"] as int?; }
            set { ViewState["MarcaId"] = value; }
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
            }
        }

        protected void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

            MarcaNegocio marcaNegocio = new MarcaNegocio();
            Marca nuevaMarca = new Marca
            {
                nombre = txtNombreMarca.Text
            };

            if (MarcaId.HasValue)
            {
                nuevaMarca.id = MarcaId.Value;
                marcaNegocio.modificar(nuevaMarca.id, nuevaMarca.nombre);
                lblMessage.Text = "Marca modificada exitosamente.";
                btnAgregarMarca.Text = "Agregar Marca";
                MarcaId = null;
            }
            else
            {
                marcaNegocio.agregar(nuevaMarca.nombre);
                lblMessage.Text = "Marca agregada exitosamente.";
            }

            limpiarFormulario();
            cargarMarcas();

            }
        }

        protected void marcasGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page") return;
            int id = Convert.ToInt32(e.CommandArgument);
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            if (e.CommandName == "editar")
            {
                Marca marca = marcaNegocio.buscarMarcaPorId(id);
                if (marca.nombre != null && marca.activo == true)
                {
                    txtNombreMarca.Text = marca.nombre;
                    MarcaId = marca.id;
                    btnAgregarMarca.Text = "Modificar Marca";
                }
                else txtNombreMarca.Text = "ERROR";
            }
            else if (e.CommandName == "eliminar")
            {
                marcaNegocio.eliminar(id);
                lblMessage.Text = "Marca eliminada exitosamente.";
                cargarMarcas();
            }
        }

        protected void MarcasGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            MarcasGridView.PageIndex = e.NewPageIndex;
            cargarMarcas();
            UpdatePagerInfo();
        }

        private void cargarMarcas()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            MarcasGridView.DataSource = marcaNegocio.listar();
            MarcasGridView.DataBind();
            UpdatePagerInfo();
        }

        private void limpiarFormulario()
        {
            txtNombreMarca.Text = string.Empty;
            lblMessage.Text = string.Empty;
            lblMessage2.Text = string.Empty;
        }

        private void UpdatePagerInfo()
        {
            GridViewRow pagerRow = MarcasGridView.BottomPagerRow;
            if (pagerRow != null)
            {
                Label lblPageInfo = (Label)pagerRow.FindControl("lblPageInfo");
                if (lblPageInfo != null)
                {
                    lblPageInfo.Text = $"Página {MarcasGridView.PageIndex + 1} de {MarcasGridView.PageCount}";
                }
            }
        }
    }
}
