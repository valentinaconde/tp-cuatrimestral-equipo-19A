using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.Collections.Generic;
using System.Linq;


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
                nombre = txtNombreMarca.Text,
                activo = true
            };

          
                    Marca marcaActual = new Marca();
                    marcaActual = marcaNegocio.buscarMarcaPorNombre(nuevaMarca.nombre);

                    if (marcaActual.nombre != null && marcaActual.activo == true)
                    {

                        lblMessage.Text = "La marca ya existe.";
                        lblMessage.CssClass = "text-danger";

                    }
                    else if (marcaActual.nombre != null && marcaActual.activo == false)
                    {
                        if (MarcaId.HasValue)
                        {
                            nuevaMarca.id = MarcaId.Value;
                            marcaNegocio.modificar(nuevaMarca.id, nuevaMarca.nombre);
                            lblMessage.Text = "Marca modificada exitosamente.";
                            lblMessage.CssClass = "text-success";

                            btnAgregarMarca.Text = "Agregar Marca";
                            MarcaId = null;
                        }
                    else
                    {
                        marcaNegocio.activarMarca(marcaActual.nombre);
                        lblMessage.Text = "Marca agregada exitosamente.";
                        lblMessage.CssClass = "text-success";
                    }

                       
                    }
                    else
                    {
                        if (MarcaId.HasValue)
                        {
                            nuevaMarca.id = MarcaId.Value;
                            marcaNegocio.modificar(nuevaMarca.id, nuevaMarca.nombre);
                            lblMessage.Text = "Marca modificada exitosamente.";
                            lblMessage.CssClass = "text-success";

                            btnAgregarMarca.Text = "Agregar Marca";
                            MarcaId = null;
                        }
                        else
                        {
                            marcaNegocio.agregar(nuevaMarca.nombre);
                            lblMessage.Text = "Marca agregada exitosamente.";
                            lblMessage.CssClass = "text-success";
                        }
                      


                    }

            limpiarFormulario();
            cargarMarcas();
            limpiarLabelsEn3segundos();



            }
        }

        protected void marcasGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblMessage.Text = "";
            if (e.CommandName == "Page") return;
            int id = Convert.ToInt32(e.CommandArgument);
            MarcaNegocio marcaNegocio = new MarcaNegocio();

            if (e.CommandName == "editar")
            {
                Marca marca = marcaNegocio.buscarMarcaPorId(id);
                if (marca.activo == true)
                {
                    txtNombreMarca.Text = marca.nombre;
                    MarcaId = marca.id;
                    btnAgregarMarca.Text = "Modificar Marca";
                }
                else txtNombreMarca.Text = marca.nombre;
            }
            else if (e.CommandName == "eliminar")
            {
                marcaNegocio.eliminar(id);
                lblMessage.Text = "Marca eliminada exitosamente.";
                lblMessage.CssClass = "text-success";
                btnAgregarMarca.Text = "Agregar Marca";

                cargarMarcas();
            }

            limpiarLabelsEn3segundos();
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
            List<Marca> listaMarcas = marcaNegocio.listar();

            Session["listaMarcas"] = listaMarcas;

            MarcasGridView.DataSource = listaMarcas;
            MarcasGridView.DataBind();
            UpdatePagerInfo();
        }

        private void limpiarFormulario()
        {
            txtNombreMarca.Text = string.Empty;
            btnAgregarMarca.Text = "Agregar Marca";

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
        protected void BuscarMarca_TextChanged(object sender, EventArgs e)
        {
            List<Marca> listaMarcas = (List<Marca>)Session["listaMarcas"];

            if (listaMarcas != null)
            {
                List<Marca> listaFiltrada = listaMarcas.FindAll(x => x.nombre.ToUpper().Contains(txtFiltroMarca.Text.ToUpper()))
                    .ToList();


                if (listaFiltrada.Count > 0)
                {
                    MarcasGridView.DataSource = listaFiltrada;
                    MarcasGridView.DataBind();
                    lblNoResultsMarca.Visible = false;
                }
                else
                {
                    MarcasGridView.DataSource = null;
                    MarcasGridView.DataBind();
                    lblNoResultsMarca.Text = "No se encontraron marcas.";
                    lblNoResultsMarca.CssClass = "text-dark";

                    lblNoResultsMarca.Visible = true;

                }
            }
            if (string.IsNullOrEmpty(txtFiltroMarca.Text))
            {
                cargarMarcas();
                lblNoResultsMarca.Visible = false;
            }
        }

        private void limpiarLabelsEn3segundos()
        {
            string script = @"
            setTimeout(function() {
                document.getElementById('" + lblMessage.ClientID + @"').innerText = '';
                document.getElementById('" + lblMessage2.ClientID + @"').innerText = '';
            }, 3000);";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "limpiarLabels", script, true);
        }
    }
}
