using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class Marcas : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaMarcas();
            }
        }

        private void CargaMarcas()
        {
            MarcaNegocio marcaNeg = new MarcaNegocio();
            List<Marca> listaMarcas = marcaNeg.listar();
            MarcasGridView.DataSource = listaMarcas;
            MarcasGridView.DataBind();
        }

    }
}