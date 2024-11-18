﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.Collections.Generic;
using System.Linq;

namespace tp_cuatrimestral_equipo_19A
{
    public partial class ConfiguracionCategorias : Page
    {
        private int? CategoriaId
        {
            get { return ViewState["CategoriaId"] as int?; }
            set { ViewState["CategoriaId"] = value; }
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
                if(usuario.rol_id == 2)
                {
                    Response.Redirect("HomeVendedorPage.aspx");
                }
                cargarCategorias();
            }
        }

        protected void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

           
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                Categoria nuevaCategoria = new Categoria
                {
                    nombre = txtNombreCategoria.Text,
                    activo = true
                };

            if (CategoriaId.HasValue)
            {
                nuevaCategoria.id = CategoriaId.Value;
                categoriaNegocio.modificar(nuevaCategoria.id, nuevaCategoria.nombre);
                lblMessage.Text = "Categoría modificada exitosamente.";
                btnAgregarCategoria.Text = "Agregar Categoría";
                CategoriaId = null;
            }
            else
            {
                categoriaNegocio.agregar(nuevaCategoria.nombre);
                lblMessage.Text = "Categoría agregada exitosamente.";
            }

            limpiarFormulario();
            cargarCategorias();
            Response.Redirect(Request.RawUrl);
            }
        }

        protected void categoriasGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Page") return;
            int id = Convert.ToInt32(e.CommandArgument);
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            if (e.CommandName == "editar")
            {
                Categoria categoria = categoriaNegocio.buscarCategoriaPorId(id);
                if (categoria.nombre != null && categoria.activo == true)
                {
                    txtNombreCategoria.Text = categoria.nombre;
                    CategoriaId = categoria.id;
                    btnAgregarCategoria.Text = "Modificar Categoría";
                }
            }
            else if (e.CommandName == "eliminar")
            {
                categoriaNegocio.eliminar(id);
                lblMessage.Text = "Categoría eliminada exitosamente.";
                cargarCategorias();
                
            }
        }

        private void cargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Categoria> listaCategorias = categoriaNegocio.listar();
            
            Session["listaCategorias"] = listaCategorias;


            CategoriasGridView.DataSource = listaCategorias;
            CategoriasGridView.DataBind();
            UpdatePagerInfo();
        }

        private void limpiarFormulario()
        {
            txtNombreCategoria.Text = string.Empty;
            lblMessage.Text = string.Empty;
            lblMessage2.Text = string.Empty;
        }

        protected void categoriasGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CategoriasGridView.PageIndex = e.NewPageIndex;
            cargarCategorias();
            UpdatePagerInfo();
        }

        private void UpdatePagerInfo()
        {
            GridViewRow pagerRow = CategoriasGridView.BottomPagerRow;
            if (pagerRow != null)
            {
                Label lblPageInfo = (Label)pagerRow.FindControl("lblPageInfo");
                if (lblPageInfo != null)
                {
                    lblPageInfo.Text = $"Página {CategoriasGridView.PageIndex + 1} de {CategoriasGridView.PageCount}";
                }
            }
        }
        protected void Buscar_TextChanged(object sender, EventArgs e)
        {

            List<Categoria> listaCategorias = (List<Categoria>)Session["listaCategorias"];
            List<Categoria> listaFiltrada = listaCategorias.FindAll(x => x.nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));

            if (listaFiltrada.Count > 0)
            {
                CategoriasGridView.DataSource = listaFiltrada;
                CategoriasGridView.DataBind();
                lblNoResults.Visible = false;
            }
            else
            {
                CategoriasGridView.DataSource = null;
                CategoriasGridView.DataBind();
                lblNoResults.Text = "No se encontraron categorías.";
                lblNoResults.Visible = true;
            }

            if (string.IsNullOrEmpty(txtFiltro.Text))
            {
                cargarCategorias();
                lblNoResults.Visible = false;
            }
        }

    }

}
