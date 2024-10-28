using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarProductos();
            }

        }
        protected void productosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        private void cargarProductos()
        {
            /*
            ProductoNegocio productonegocio = new ProductoNegocio();
            ProductosGridView.DataSource = productonegocio.listar();
            ProductosGridView.DataBind();*/
        }

        protected void ProductosGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {

        }
    }
}