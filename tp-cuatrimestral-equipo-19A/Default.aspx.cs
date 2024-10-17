using System;
using System.Web.UI;

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


                bool isAuthenticated = AuthenticateUser(email, password);
                if (isAuthenticated)
                {
                    Response.Redirect("HomePage.aspx");
                }
        }

        private bool AuthenticateUser(string email, string password)
        {
            
            return email == "test@prueba.com" && password == "password123";
        }
    }
}
