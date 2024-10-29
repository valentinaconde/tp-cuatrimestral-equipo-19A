<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Configuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-5">
        <h2>Agregar Nuevo Usuario</h2>
        <div class="row">
            <div class="form-group col-4">
                <label for="txtNombre">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre es requerido." CssClass="text-danger" Display="Static" />
            </div>
            <div class="form-group col-4">
                <label for="txtApellido">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido" ErrorMessage="El apellido es requerido." CssClass="text-danger" Display="Static" />
            </div>
        </div>

        <div class="row">
            <div class="form-group col-4">
                <label for="txtEmail">Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="El email es requerido." CssClass="text-danger" Display="Static" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Formato de email inválido." CssClass="text-danger" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
                <label class="text-info">La contraseña por defecto es el email de la persona.</label>
            </div>
            <div class="form-group col-4">
                <label for="ddlRol">Rol</label>
                <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                    <asp:ListItem Value="1">Admin</asp:ListItem>
                    <asp:ListItem Value="2">User</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvRol" runat="server" ControlToValidate="ddlRol" InitialValue="" ErrorMessage="El rol es requerido." CssClass="text-danger" Display="Static" />
            </div>
        </div>

        <div class="d-flex align-items-center gap-3">
            <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-primary mt-3" Text="Agregar Usuario" OnClick="btnAgregar_Click" />
            <asp:Label ID="lblMessage2" runat="server" CssClass="mt-3 fw-medium text-danger" />
            <asp:Label ID="lblMessage" runat="server" CssClass="mt-3 fw-medium text-success" />
        </div>
    </div>

    <asp:GridView ID="UsuariosGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="UsuariosGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="email" HeaderText="Email" />
            <asp:BoundField DataField="rol_id" HeaderText="Rol" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="editar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-warning btn-sm" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este usuario?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
