using System;
using System.Web.UI;
using Negocio;
using Dominio;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class Configuracion : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarUsuarios();
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario nuevoUsuario = new Usuario
            {
                nombre = txtNombre.Text,
                apellido = txtApellido.Text,
                email = txtEmail.Text,
                password = txtEmail.Text,
                rol_id = int.Parse(ddlRol.SelectedValue)
            };

            bool existeUsuario = validarUsuarioInexistente();

            if (!existeUsuario) return;
            negocio.agregar(nuevoUsuario.nombre, nuevoUsuario.apellido, nuevoUsuario.email, nuevoUsuario.password, nuevoUsuario.rol_id);
            lblMessage.Text = "Usuario agregado exitosamente.";
            lblMessage2.Text = "";
            limpiarFormulario();
            cargarUsuarios();
        }

        private bool validarUsuarioInexistente()
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Usuario usuario = new Usuario();

            usuario = usuarioNegocio.buscarUsuario(txtEmail.Text);
            if (usuario.email == null) return true;
            lblMessage2.Text = "El usuario ya existe.";
            lblMessage.Text = "";



            return false;
        }
            private void cargarUsuarios()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            UsuariosGridView.DataSource = negocio.listar();
            UsuariosGridView.DataBind();
        }

        private void limpiarFormulario()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            ddlRol.SelectedIndex = 0;
        }
    }
}
