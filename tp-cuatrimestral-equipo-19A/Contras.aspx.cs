using Dominio;
using Negocio;
using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class Contras : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnCambiarContrasena_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            usuario = (Usuario)Session["UsuarioActual"];

            string contrasenaActual = txtContrasenaActual.Text;
            string nuevaContrasena = txtNuevaContrasena.Text;

            lblMessage.Text = usuario.password;
            if (contrasenaActual != usuario.password)
            {
                lblMessage.Text = "Contraseña no coincide con la actual.";
            }
            else
            {
                usuario.password = nuevaContrasena;
            }
            if(contrasenaActual == nuevaContrasena)
            {
                lblMessage.Text = "Las contraseñas son iguales.";
                return;
            }
            if(nuevaContrasena.Length < 6)
            {
                lblMessage.Text = "La contraseña debe tener minimo 6 caracteres.";
                return;
            }

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            usuarioNegocio.modificar(usuario);
         
        }
    }
}
