using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class Clientes : Page
    {
        private int? ClienteId
        {
            get { return ViewState["ClienteId"] as int?; }
            set { ViewState["ClienteId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarClientes();
            }
        }

        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            Cliente nuevoCliente = new Cliente
            {
                nombre = txtNombreCliente.Text,
                direccion = txtDireccionCliente.Text,
                telefono = txtTelefonoCliente.Text,
                email = txtEmailCliente.Text



            };

            if (ClienteId.HasValue)
            {
                nuevoCliente.id = ClienteId.Value;
                clienteNegocio.modificar(nuevoCliente);
                lblMessage.Text = "Cliente modificado exitosamente.";
                btnAgregarCliente.Text = "Agregar Cliente";
                ClienteId = null;
            }
            else
            {
                clienteNegocio.agregar(nuevoCliente);
                lblMessage.Text = "Cliente agregado exitosamente.";
            }

            limpiarFormulario();
            cargarClientes();
        }

        protected void clientesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            ClienteNegocio clienteNegocio = new ClienteNegocio();

            if (e.CommandName == "editar")
            {
                Cliente cliente = clienteNegocio.buscarClientePorId(id);
                if (cliente != null)
                {
                    txtNombreCliente.Text = cliente.nombre;
                    txtDireccionCliente.Text = cliente.direccion;
                    txtTelefonoCliente.Text = cliente.telefono;
                    txtEmailCliente.Text = cliente.email;


                    ClienteId = cliente.id;
                    btnAgregarCliente.Text = "Modificar Cliente";
                }
                else
                {
                    lblMessage2.Text = "Error al cargar el cliente.";
                }
            }
            else if (e.CommandName == "eliminar")
            {
                clienteNegocio.eliminar(id);
                lblMessage.Text = "Cliente eliminado exitosamente.";
                cargarClientes();
            }
        }

        private void cargarClientes()
        {
            ClienteNegocio clienteNegocio = new ClienteNegocio();
            ClientesGridView.DataSource = clienteNegocio.listar();
            ClientesGridView.DataBind();
        }

        private void limpiarFormulario()
        {
            txtNombreCliente.Text = string.Empty;
            lblMessage.Text = string.Empty;
            lblMessage2.Text = string.Empty;
            txtDireccionCliente.Text = string.Empty;
            txtTelefonoCliente.Text = string.Empty;
            txtEmailCliente.Text = string.Empty;


        }
    }
}
