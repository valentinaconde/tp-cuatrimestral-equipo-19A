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
            <asp:Button ID="btnAgregarCategoria" runat="server" CssClass="btn btn-primary mt-3" Text="Agregar Categoría" OnClick="btnAgregarCategoria_Click" OnClientClick="return confirm('¿Estás seguro de que deseas agregar esta categoría?');"/>
            <asp:Label ID="lblMessage2" runat="server" CssClass="mt-3 fw-medium text-danger" />
            <asp:Label ID="lblMessage" runat="server" CssClass="mt-3 fw-medium text-success" />
        </div>
    </div>

    <asp:GridView ID="CategoriasGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="categoriasGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="editar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-warning btn-sm" CausesValidation="false" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Estás seguro de que deseas eliminar esta categoría?');" CausesValidation="false" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
