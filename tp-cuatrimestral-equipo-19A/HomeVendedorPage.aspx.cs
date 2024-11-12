using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class HomeVendedorPage : System.Web.UI.Page
    {
        public Usuario usuario = new Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                usuario = (Usuario)Session["UsuarioActual"];
                if (usuario == null)
                {
                    usuario = new Usuario();
                    usuario.nombre = "";
                }
                DataBind();
            }
        }
    }
}