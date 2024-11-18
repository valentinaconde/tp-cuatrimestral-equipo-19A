<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Configuracion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-secondary text-white">
                <h2 class="mb-0 fst-italic">Administración de usuarios</h2>
            </div>
            <div class="card-body">
                <div class="row mb-4">
                    <div class="col-md-4">
                        <label for="txtNombre" class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="El nombre es requerido." CssClass="text-danger small" Display="Static" />
                    </div>
                    <div class="col-md-4">
                        <label for="txtApellido" class="form-label">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvApellido" runat="server" ControlToValidate="txtApellido" ErrorMessage="El apellido es requerido." CssClass="text-danger small" Display="Static" />
                    </div>
                </div>
                <div class="row mb-4">
                    <div class="col-md-4">
                        <label for="txtEmail" class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="El email es requerido." CssClass="text-danger small" Display="Static" />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Formato de email inválido." CssClass="text-danger small" Display="Dynamic" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
                        <label class="text-info">La contraseña por defecto es el email de la persona.</label>
                    </div>
                    <div class="col-md-4">
                        <label for="ddlRol" class="form-label">Rol</label>
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1">Administrador</asp:ListItem>
                            <asp:ListItem Value="2">Vendedor</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvRol" runat="server" ControlToValidate="ddlRol" InitialValue="" ErrorMessage="El rol es requerido." CssClass="text-danger small" Display="Static" />
                    </div>
                </div>

                <div class="d-flex align-items-center gap-3">
                    <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-secondary mt-3" Text="Agregar Usuario" OnClick="btnAgregar_Click" />
                    <asp:Label ID="lblMessage2" runat="server" CssClass="mt-3 fw-medium text-danger" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="mt-3 fw-medium text-success" />
                </div>
            </div>
        </div>

        <h4 class="fst-italic fw-medium mt-5">Listado de Usuarios</h4>
        <asp:GridView ID="UsuariosGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="UsuariosGridView_RowCommand" AllowPaging="true" PageSize="3" OnPageIndexChanging="UsuariosGridView_PageIndexChanging">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="apellido" HeaderText="Apellido" />
                <asp:BoundField DataField="email" HeaderText="Email" />
                <asp:TemplateField HeaderText="Rol">
                    <ItemTemplate>
                        <%# Eval("rol_id").ToString() == "1" ? "Administrador" : "Vendedor" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="editar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-sm" CausesValidation="false">
                            <span class="material-symbols-outlined text-warning">edit</span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnEliminar" runat="server" CommandName="eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-sm" OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este usuario?');" CausesValidation="false">
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
    </div>
</asp:Content>
