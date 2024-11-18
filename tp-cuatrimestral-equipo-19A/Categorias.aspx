<%@ Page Title="Configuración de Categorías" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ConfiguracionCategorias.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.ConfiguracionCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="card shadow">
            <div class="card-header bg-secondary text-white">
                <h2 class="mb-0 fst-italic">Administración de categorías</h2>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label for="txtNombreCategoria" class="form-label">Nombre de la Categoría</label>
                        <asp:TextBox ID="txtNombreCategoria" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvNombreCategoria" runat="server" ControlToValidate="txtNombreCategoria" ErrorMessage="El nombre de la categoría es requerido." CssClass="text-danger small" Display="Dynamic" />
                    </div>
                </div>
                <div class="d-flex align-items-center gap-3">
                    <asp:Button ID="btnAgregarCategoria" runat="server" CssClass="btn btn-success" Text="Agregar Categoría" OnClick="btnAgregarCategoria_Click" />
                    <asp:Label ID="lblMessage2" runat="server" CssClass="fw-medium text-danger" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="fw-medium text-success" />
                </div>
            </div>
        </div>

        <div class="card shadow mt-5">
       
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label for="txtFiltro" class="form-label fs-4 fst-italic">Buscador</label>
                        <asp:TextBox runat="server"  placeholder="Buscar categoria..." ID="txtFiltro" CssClass="form-control mt-3" AutoPostBack="true" OnTextChanged="Buscar_TextChanged" />
                    </div>
                </div>
            </div>
        </div>

        <div class="card shadow mt-5">
                 <div class="card-header bg-secondary text-white">
         <h4 class="mb-0 fst-italic">Listado de Categorias</h4>
     </div>
            <div class="card-body">
                <asp:GridView ID="CategoriasGridView" runat="server" CssClass="table table-hover table-bordered align-middle" AutoGenerateColumns="False" OnRowCommand="categoriasGridView_RowCommand" AllowPaging="true" PageSize="10" OnPageIndexChanging="categoriasGridView_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="editar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-sm" CausesValidation="false">
                                    <span class="material-symbols-outlined text-warning">edit</span>
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server" CommandName="eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-sm"
                                    OnClientClick="return confirm('¿Estás seguro de que deseas eliminar esta categoría?');" CausesValidation="false">
                                    <span class="material-symbols-outlined text-danger">delete</span>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                        <div class="d-flex justify-content-center align-items-center">
                            <asp:LinkButton ID="lnkPrev" runat="server" CommandName="Page" CommandArgument="Prev" CausesValidation="false" CssClass="btn btn-sm mx-1">
                                <span class="material-symbols-outlined text-dark fs-2">chevron_left</span>
                            </asp:LinkButton>
                            <asp:Label ID="lblPageInfo" runat="server" CssClass="mx-2 mb-1"></asp:Label>
                            <asp:LinkButton ID="lnkNext" runat="server" CommandName="Page" CommandArgument="Next" CausesValidation="false" CssClass="btn btn-sm mx-1">
                                <span class="material-symbols-outlined text-dark fs-2">chevron_right</span>
                            </asp:LinkButton>
                        </div>
                    </PagerTemplate>
                </asp:GridView>
                <asp:Label ID="lblNoResults" runat="server" CssClass="text-dark fst-italic" Visible="false" Text="No se encontraron categorías." />
            </div>
        </div>
    </div>
</asp:Content>
