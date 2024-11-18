<%@ Page Title="Configuración de Marcas" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ConfiguracionMarcas.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.ConfiguracionMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="card shadow">
            <div class="card-header bg-secondary text-white">
                <h2 class="mb-0 fst-italic">Administración de marcas</h2>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label for="txtNombreMarca" class="form-label">Nombre de la Marca</label>
                        <asp:TextBox ID="txtNombreMarca" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvNombreMarca" runat="server" ControlToValidate="txtNombreMarca" ErrorMessage="El nombre de la marca es requerido." CssClass="text-danger small" Display="Dynamic" />
                    </div>
                </div>
                <div class="d-flex align-items-center gap-3">
                    <asp:Button ID="btnAgregarMarca" runat="server" CssClass="btn btn-success" Text="Agregar Marca" OnClick="btnAgregarMarca_Click" />
                    <asp:Label ID="lblMessage2" runat="server" CssClass="fw-medium text-danger" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="fw-medium text-success" />
                </div>
            </div>
        </div>

        <div class="card shadow mt-5">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6 mb-3">
                         <label for="txtFiltroMarca" class="form-label fs-4 fst-italic">Buscador</label>
                        <asp:TextBox runat="server" ID="txtFiltroMarca" CssClass="form-control mt-3" AutoPostBack="true" OnTextChanged="BuscarMarca_TextChanged" placeholder="Buscar marca..." />
                    </div>
                </div>
            </div>
        </div>

        <div class="card shadow mt-5">
            <div class="card-header bg-secondary text-white">
    <h4 class="mb-0 fst-italic">Listado de Marcas</h4>
</div>
            <div class="card-body">
     <asp:GridView ID="MarcasGridView" runat="server" CssClass="table table-hover table-bordered align-middle" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" OnPageIndexChanging="MarcasGridView_PageIndexChanging" OnRowCommand="marcasGridView_RowCommand">
    <Columns>
        <asp:BoundField DataField="id" HeaderText="ID" HeaderStyle-CssClass="bg-light text-dark fw-bold" ItemStyle-CssClass="text-center" />
        <asp:BoundField DataField="nombre" HeaderText="Nombre" HeaderStyle-CssClass="bg-light text-dark fw-bold" />
        <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-light text-dark fw-bold text-center">
            <ItemTemplate>
                <div class="d-flex justify-content-center gap-2">
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="editar" CommandArgument='<%# Eval("id") %>' CausesValidation="false" CssClass="p-0 border-0 bg-transparent">
                        <span class="material-symbols-outlined text-warning">edit</span>
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="eliminar" CommandArgument='<%# Eval("id") %>' CausesValidation="false" CssClass="p-0 border-0 bg-transparent" OnClientClick="return confirm('¿Estás seguro de que deseas eliminar esta marca?');">
                        <span class="material-symbols-outlined text-danger">delete</span>
                    </asp:LinkButton>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <PagerTemplate>
        <div class="d-flex justify-content-center align-items-center mt-3">
            <asp:LinkButton ID="lnkPrev" runat="server" CommandName="Page" CommandArgument="Prev" CausesValidation="false" CssClass="btn btn-sm mx-1">
                <span class="material-symbols-outlined text-dark">chevron_left</span>
            </asp:LinkButton>
            <asp:Label ID="lblPageInfo" runat="server" CssClass="mx-2 fw-medium"></asp:Label>
            <asp:LinkButton ID="lnkNext" runat="server" CommandName="Page" CommandArgument="Next" CausesValidation="false" CssClass="btn btn-sm mx-1">
                <span class="material-symbols-outlined text-dark">chevron_right</span>
            </asp:LinkButton>
        </div>
    </PagerTemplate>
</asp:GridView>

                <asp:Label ID="lblNoResultsMarca" runat="server" CssClass="text-danger mt-3 fw-medium d-block text-center" Visible="false" Text="No se encontraron marcas." />
            </div>
        </div>
    </div>
</asp:Content>
