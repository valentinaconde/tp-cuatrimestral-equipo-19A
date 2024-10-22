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
                CargarProductos();
            }

        }
        private void CargarProductos()
        {
            ProductoNegocio productonegocio = new ProductoNegocio();
            List<Producto> listaProducto = productonegocio.listar();

            ProductosGridView.DataSource = listaProducto;
            ProductosGridView.DataBind();
        }
    }
}