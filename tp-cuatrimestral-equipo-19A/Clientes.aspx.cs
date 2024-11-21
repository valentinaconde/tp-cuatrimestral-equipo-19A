using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.Collections.Generic;

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
                Usuario usuario = new Usuario();
                usuario = (Usuario)Session["UsuarioActual"];
                if (usuario == null)
                {
                    Response.Redirect("Default.aspx");

                }
                cargarClientes();
            }
        }

        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ClienteNegocio clienteNegocio = new ClienteNegocio();
                Cliente nuevoCliente = new Cliente
                {
                    nombre = txtNombreCliente.Text,
                    direccion = txtDireccionCliente.Text,
                    telefono = txtTelefonoCliente.Text,
                    email = txtEmailCliente.Text,
                    dni = txtDniCliente.Text,
                    activo = true
                };

              
                if (ClienteId.HasValue)
                {
                    nuevoCliente.id = ClienteId.Value;
                    clienteNegocio.modificar(nuevoCliente);
                    lblMessage.Text = "Cliente modificado exitosamente.";
                    lblMessage.CssClass = "text-success";

                    btnAgregarCliente.Text = "Agregar Cliente";
                    ClienteId = null;
                }
                else
                {
                    Cliente clienteActual = new Cliente();
                    clienteActual = clienteNegocio.buscarClientePorDni(nuevoCliente.dni);

                    if (clienteActual.nombre != null && clienteActual.activo == true)
                    {

                        lblMessage.Text = "El cliente ya existe.";
                        lblMessage.CssClass = "text-danger";

                    }
                    else if (clienteActual.nombre != null && clienteActual.activo == false)
                    {
                        clienteNegocio.activarCliente(clienteActual.dni, nuevoCliente.direccion, nuevoCliente.telefono, nuevoCliente.email);
                        lblMessage.Text = "Cliente agregado exitosamente.";
                        lblMessage.CssClass = "text-success";
                    }
                    else
                    {
                        clienteNegocio.agregar(nuevoCliente);
                        lblMessage.Text = "Cliente agregado exitosamente.";
                        lblMessage.CssClass = "text-success";


                    }

                }

                limpiarFormulario();
                cargarClientes();
                limpiarLabelsEn3segundos();



            }
        }


       

        protected void clientesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            lblMessage2.Text = "";
            lblMessage.Text = "";
            if (e.CommandName == "Page") return;
            int id = Convert.ToInt32(e.CommandArgument);
            ClienteNegocio clienteNegocio = new ClienteNegocio();

            if (e.CommandName == "editar")
            {
                txtNombreCliente.Enabled = false;
                txtDniCliente.Enabled = false;

                Cliente cliente = clienteNegocio.buscarClientePorId(id);
                if (cliente != null && cliente.activo)
                {
                    txtNombreCliente.Text = cliente.nombre;
                    txtDireccionCliente.Text = cliente.direccion;
                    txtTelefonoCliente.Text = cliente.telefono;
                    txtEmailCliente.Text = cliente.email;
                    txtDniCliente.Text = cliente.dni; 

                    ClienteId = cliente.id;
                    ClienteId = cliente.id;
                    btnAgregarCliente.Text = "Modificar Cliente";
                }
                else
                {
                    lblMessage2.Text = "Error al cargar el cliente.";
                    lblMessage.CssClass = "text-danger";

                }
            }
            else if (e.CommandName == "eliminar")
            {
                clienteNegocio.eliminar(id);
                lblMessage.Text = "Cliente eliminado exitosamente.";
                lblMessage.CssClass = "text-success";

                cargarClientes();
            }
            limpiarLabelsEn3segundos();
        }

        protected void ClientesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ClientesGridView.PageIndex = e.NewPageIndex;
            cargarClientes();
        }

        private void cargarClientes()
        {
            ClienteNegocio clientenegocio = new ClienteNegocio();
            List<Cliente> clientes = clientenegocio.listar();

            Session["listaClientes"] = clientes;

            if (clientes.Count == 0)
            {
                lblNoResults.Text = "No se encontraron clientes.";
                lblNoResults.CssClass = "text-dark";

                lblNoResults.Visible = true;
            }
            else
            {
                lblNoResults.Visible = false;
            }

            ClientesGridView.DataSource = clientes;
            ClientesGridView.DataBind();
            UpdatePagerInfo();
        }

        private void limpiarFormulario()
        {
            txtNombreCliente.Text = string.Empty;
            txtDireccionCliente.Text = string.Empty;
            txtTelefonoCliente.Text = string.Empty;
            txtEmailCliente.Text = string.Empty;
            txtDniCliente.Text = string.Empty;
            txtNombreCliente.Enabled = true;
            txtDniCliente.Enabled = true;
        }

        private void UpdatePagerInfo()
        {
            GridViewRow pagerRow = ClientesGridView.BottomPagerRow;
            if (pagerRow != null)
            {
                Label lblPageInfo = (Label)pagerRow.FindControl("lblPageInfo");
                if (lblPageInfo != null)
                {
                    lblPageInfo.Text = $"Página {ClientesGridView.PageIndex + 1} de {ClientesGridView.PageCount}";
                }
            }
        }

        protected void Buscar_TextChanged(object sender, EventArgs e)
        {
            List<Cliente> listaClientes = (List<Cliente>)Session["listaClientes"];
            List<Cliente> listaFiltrada = listaClientes.FindAll(x => x.nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));

            if (listaFiltrada.Count > 0)
            {
                ClientesGridView.DataSource = listaFiltrada;
                ClientesGridView.DataBind();
                lblNoResults.Visible = false;
            }
            else
            {
                ClientesGridView.DataSource = null;
                ClientesGridView.DataBind();
                lblNoResults.Text = "No se encontraron clientes.";
                lblNoResults.CssClass = "text-dark";

                lblNoResults.Visible = true;
            }

            if (string.IsNullOrEmpty(txtFiltro.Text))
            {
                cargarClientes();
                lblNoResults.Visible = false;
            }
        }

        private void limpiarLabelsEn3segundos()
        {
            string script = @"
            setTimeout(function() {
                document.getElementById('" + lblMessage.ClientID + @"').innerText = '';
                document.getElementById('" + lblMessage2.ClientID + @"').innerText = '';
            }, 3000);";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "limpiarLabels", script, true);
        }
    }
}
