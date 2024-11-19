using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.Collections.Generic;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class Configuracion : Page
    {
        private int? UsuarioId
        {
            get { return ViewState["UsuarioId"] as int?; }
            set { ViewState["UsuarioId"] = value; }
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
                cargarUsuarios();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Usuario nuevoUsuario = new Usuario
            {
                nombre = txtNombre.Text,
                apellido = txtApellido.Text,
                email = txtEmail.Text,
                password = txtEmail.Text,
                rol_id = int.Parse(ddlRol.SelectedValue),
                activo = true
            };

            if (UsuarioId.HasValue)
            {
                nuevoUsuario.id = UsuarioId.Value;
                usuarioNegocio.modificar(nuevoUsuario);
                lblMessage.Text = "Usuario modificado exitosamente.";
                lblMessage.CssClass = "text-success";

                btnAgregar.Text = "Agregar Usuario";
                UsuarioId = null;
            }
            else
            {

                Usuario usuarioActual = new Usuario();
                usuarioActual = usuarioNegocio.buscarUsuarioPorEmail(nuevoUsuario.email);

                if (usuarioActual.nombre != null && usuarioActual.activo == true)
                {

                    lblMessage.Text = "El usuario ya existe.";
                    lblMessage.CssClass = "text-danger";

                }
                else if (usuarioActual.nombre != null && usuarioActual.activo == false)
                {
                    usuarioNegocio.activarUsuario(usuarioActual.email);
                    lblMessage.Text = "Usuario agregado exitosamente.";
                    lblMessage.CssClass = "text-success";
                }
                else
                {
                    usuarioNegocio.agregar(nuevoUsuario.nombre, nuevoUsuario.apellido, nuevoUsuario.email, nuevoUsuario.password, nuevoUsuario.rol_id);
                    lblMessage.Text = "Usuario agregado exitosamente.";
                    lblMessage.CssClass = "text-success";


                }
            }

            limpiarFormulario();
            cargarUsuarios();
        }

        protected void UsuariosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page") return;
            int id = Convert.ToInt32(e.CommandArgument);
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            if (e.CommandName == "editar")
            {
                Usuario usuario = usuarioNegocio.buscarUsuarioPorId(id);
                if (usuario.email != null)
                {
                    txtNombre.Text = usuario.nombre;
                    txtApellido.Text = usuario.apellido;
                    txtEmail.Text = usuario.email;
                    ddlRol.SelectedValue = usuario.rol_id.ToString();
                    UsuarioId = usuario.id;
                    btnAgregar.Text = "Modificar Usuario";
                }
            }
            else if (e.CommandName == "eliminar")
            {
                usuarioNegocio.eliminar(id);
                lblMessage.Text = "Usuario eliminado exitosamente.";
                lblMessage.CssClass = "text-success";

                cargarUsuarios();
            }
        }

        private void cargarUsuarios()
        {
            UsuarioNegocio usuarionegocio = new UsuarioNegocio();
            List<Usuario> usuarios = usuarionegocio.listar();

            Session["listaUsuarios"] = usuarios;

            if (usuarios.Count == 0)
            {
                lblNoResults.Text = "No se encontraron usuarios.";
                lblNoResults.CssClass = "text-dark";

                lblNoResults.Visible = true;
            }
            else
            {
                lblNoResults.Visible = false;
            }

            UsuariosGridView.DataSource = usuarios;
            UsuariosGridView.DataBind();
            UpdatePagerInfo();
        }

        private void limpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlRol.SelectedIndex = 0;
        }

        protected void UsuariosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            UsuariosGridView.PageIndex = e.NewPageIndex;
            cargarUsuarios();
            UpdatePagerInfo();
        }

        private void UpdatePagerInfo()
        {
            GridViewRow pagerRow = UsuariosGridView.BottomPagerRow;
            if (pagerRow != null)
            {
                Label lblPageInfo = (Label)pagerRow.FindControl("lblPageInfo");
                if (lblPageInfo != null)
                {
                    lblPageInfo.Text = $"Página {UsuariosGridView.PageIndex + 1} de {UsuariosGridView.PageCount}";
                }
            }
        }

        protected void Buscar_TextChanged(object sender, EventArgs e)
        {
            List<Usuario> listaUsuarios = (List<Usuario>)Session["listaUsuarios"];
            List<Usuario> listaFiltrada = listaUsuarios.FindAll(x => x.nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));

            if (listaFiltrada.Count > 0)
            {
                UsuariosGridView.DataSource = listaFiltrada;
                UsuariosGridView.DataBind();
                lblNoResults.Visible = false;
            }
            else
            {
                UsuariosGridView.DataSource = null;
                UsuariosGridView.DataBind();
                lblNoResults.Text = "No se encontraron usuarios.";
                lblNoResults.CssClass = "text-dark";

                lblNoResults.Visible = true;
            }

            if (string.IsNullOrEmpty(txtFiltro.Text))
            {
                cargarUsuarios();
                lblNoResults.Visible = false;
            }
        }
    }
}
