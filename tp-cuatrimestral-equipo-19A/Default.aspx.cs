using Negocio;
using System;
using System.Web.UI;
using Dominio;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
                string email = EmailTextBox.Text;
                string password = PasswordTextBox.Text;


                bool isError = AuthenticateUser(email, password);
                if (isError)
                {
                errorLabel.Text = "Usuario o contraseña incorrectos";
                   
                }



        }

        private bool AuthenticateUser(string email, string password)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Usuario usuario = new Usuario();
            usuario = usuarioNegocio.buscarUsuarioPorEmail(email);
            if (usuario.email == null) return true;
            if (usuario.password != password) return true;



            if (usuario.rol_id == 1) Response.Redirect("HomeAdminPage.aspx");
            else Response.Redirect("HomeVendedorPage.aspx");
            return false;


        }
    }
}
