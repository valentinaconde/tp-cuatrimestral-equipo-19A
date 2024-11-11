<%@ Page Title="Configuración de Categorías" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ConfiguracionCategorias.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.ConfiguracionCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-5">
        <h2>Agregar Nueva Categoría</h2>
        <div class="row">
            <div class="form-group col-4">
                <label for="txtNombreCategoria">Nombre de la Categoría</label>
                <asp:TextBox ID="txtNombreCategoria" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombreCategoria" runat="server" ControlToValidate="txtNombreCategoria" ErrorMessage="El nombre de la categoría es requerido." CssClass="text-danger" Display="Dynamic" />
            </div>
        </div>

        <div class="d-flex align-items-center gap-3">
            <asp:Button ID="btnAgregarCategoria" runat="server" CssClass="btn btn-primary mt-3" Text="Agregar Categoría" OnClick="btnAgregarCategoria_Click" OnClientClick="return confirm('¿Estás seguro de que deseas agregar esta categoría?');" />
            <asp:Label ID="lblMessage2" runat="server" CssClass="mt-3 fw-medium text-danger" />
            <asp:Label ID="lblMessage" runat="server" CssClass="mt-3 fw-medium text-success" />
        </div>
    </div>

    <asp:GridView ID="CategoriasGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="categoriasGridView_RowCommand" AllowPaging="true" PageSize="5" OnPageIndexChanging="categoriasGridView_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="editar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-sm" CausesValidation="false">
                        <span class="material-symbols-outlined text-warning ">edit</span>
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
            <asp:LinkButton ID="lnkNext" runat="server" CommandName="Page" CommandArgument="Next" CausesValidation="false" CssClass="btn btn-sm  mx-1">
                <span class="material-symbols-outlined text-dark fs-2">chevron_right</span>
            </asp:LinkButton>
        </div>
    </PagerTemplate>
    </asp:GridView>
</asp:Content>
