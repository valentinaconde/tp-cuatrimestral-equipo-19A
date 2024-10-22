using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

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
                password = txtEmail.Text, // La contraseña por defecto es el email
                rol_id = int.Parse(ddlRol.SelectedValue)
            };

            if (UsuarioId.HasValue)
            {
                // Editar usuario existente
                nuevoUsuario.id = UsuarioId.Value;
                usuarioNegocio.modificar(nuevoUsuario.id, nuevoUsuario.nombre, nuevoUsuario.apellido, nuevoUsuario.email, nuevoUsuario.password, nuevoUsuario.rol_id);
                lblMessage.Text = "Usuario modificado exitosamente.";
                btnAgregar.Text = "Agregar Usuario";
                UsuarioId = null;
            }
            else
            {
                // Agregar nuevo usuario
                usuarioNegocio.agregar(nuevoUsuario.nombre, nuevoUsuario.apellido, nuevoUsuario.email, nuevoUsuario.password, nuevoUsuario.rol_id);
                lblMessage.Text = "Usuario agregado exitosamente.";
            }

            limpiarFormulario();
            cargarUsuarios();
        }

        protected void UsuariosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();

            if (e.CommandName == "editar")
            {
                Usuario usuario = usuarioNegocio.listar().Find(u => u.id == id);
                if (usuario != null)
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
                cargarUsuarios();
            }
        }

        private void cargarUsuarios()
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            UsuariosGridView.DataSource = usuarioNegocio.listar();
            UsuariosGridView.DataBind();
        }

        private void limpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlRol.SelectedIndex = 0;
            lblMessage.Text = string.Empty;
            lblMessage2.Text = string.Empty;
        }
    }
}
