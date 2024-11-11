<%@ Page Title="Gestión de Proveedores" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-3">
        <h2>Agregar Nuevo Proveedor</h2>
        <div class="row">
            <div class="form-group col-4">
                <label for="txtNombreProveedor">Nombre</label>
                <asp:TextBox ID="txtNombreProveedor" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombreProveedor" runat="server" ControlToValidate="txtNombreProveedor" ErrorMessage="El nombre del proveedor es obligatorio." CssClass="text-danger" Display="Static" />
            </div>
            <div class="form-group col-4">
                <label for="txtDireccionProveedor">Dirección</label>
                <asp:TextBox ID="txtDireccionProveedor" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvDireccionProveedor" runat="server" ControlToValidate="txtDireccionProveedor" ErrorMessage="La dirección del proveedor es obligatoria." CssClass="text-danger" Display="Static" />
            </div>
        </div>
        <div class="row">
            <div class="form-group col-4">
                <label for="txtTelefonoProveedor">Teléfono</label>
                <asp:TextBox ID="txtTelefonoProveedor" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvTelefonoProveedor" runat="server" ControlToValidate="txtTelefonoProveedor" ErrorMessage="El teléfono del proveedor es obligatorio." CssClass="text-danger" Display="Static" />
            </div>
            <div class="form-group col-4">
                <label for="txtEmailProveedor">Email</label>
                <asp:TextBox ID="txtEmailProveedor" runat="server" CssClass="form-control" />
                <asp:RegularExpressionValidator ID="revEmailProveedor" runat="server" ControlToValidate="txtEmailProveedor" ErrorMessage="Formato de email inválido." CssClass="text-danger" Display="Dynamic" ValidationExpression="^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" />
                <asp:RequiredFieldValidator ID="rfvEmailProveedor" runat="server" ControlToValidate="txtEmailProveedor" ErrorMessage="El email del proveedor es obligatorio." CssClass="text-danger" Display="Static" />
            </div>
        </div>
    </div>

    <div class="d-flex align-items-center gap-3">
        <asp:Button ID="btnAgregarProveedor" runat="server" CssClass="btn btn-primary mb-5" Text="Agregar Proveedor" OnClick="btnAgregarProveedor_Click" />
        <asp:Label ID="lblMessage2" runat="server" CssClass="mt-3 fw-medium text-danger" />
        <asp:Label ID="lblMessage" runat="server" CssClass="mt-3 fw-medium text-success" />
    </div>

    <asp:GridView ID="ProveedoresGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="proveedoresGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="direccion" HeaderText="Dirección" />
            <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
            <asp:BoundField DataField="email" HeaderText="Email" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="editar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-sm" CausesValidation="false">
                <span class="material-symbols-outlined text-warning ">edit</span>
            </asp:LinkButton>
                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-sm"
                        OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este proveedor?');" CausesValidation="false">
                <span class="material-symbols-outlined text-danger">delete</span>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
