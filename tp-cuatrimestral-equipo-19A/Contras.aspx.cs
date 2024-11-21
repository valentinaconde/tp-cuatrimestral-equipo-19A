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

            if (contrasenaActual != usuario.password)
            {
                lblMessage.Text = "La contraseña ingresada no coincide con la actual.";
                lblMessage.CssClass = "text-danger";
            }
            else
            {
                usuario.password = nuevaContrasena;
                lblMessage.Text = "La contraseña fue cambiada con exito.";
                lblMessage.CssClass = "text-success";
            }
            if(contrasenaActual == nuevaContrasena)
            {
                lblMessage.Text = "Las contraseñas son iguales.";
                lblMessage.CssClass = "text-danger";
                return;
            }
            if(nuevaContrasena.Length < 6)
            {
                lblMessage.Text = "La contraseña debe tener minimo 6 caracteres.";
                lblMessage.CssClass = "text-danger";
                return;
            }

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            usuarioNegocio.modificar(usuario);
         
        }
    }
}
