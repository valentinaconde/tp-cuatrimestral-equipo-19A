using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using Negocio;
using Dominio;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class OlvideContra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            EmailService emailService = new EmailService();
            string email = EmailTextBox.Text;

            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Usuario usuario = new Usuario();
            usuario = usuarioNegocio.buscarUsuarioPorEmail(email);
            if(usuario.email == null)
            {
                lblMessage.Text = "El email ingresado no existe";
                return;
            }

            emailService.armarCorreo(email, "Recuperar contraseña", "Tu contraseña es: " + usuario.password);
            emailService.enviarEmail();
            lblMessage.Text = "Email enviado exitosamente";
            
            //Response.Redirect("Login.aspx");

        }
    }
}