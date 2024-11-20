<%@ Page Title="Gestión de Clientes" Language="C#" MasterPageFile="~/Sharing.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SharingMain" runat="server">
    <div class="container mt-5">
        <div class="card shadow">
            <div class="card-header bg-secondary text-white">
                <h2 class="mb-0 fst-italic">Administración de clientes</h2>
            </div>
            <div class="card-body">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row ">
                            <div class="col-md-4">
                                <label for="txtNombreCliente" class="form-label">Nombre</label>
                                <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvNombreCliente" runat="server" ControlToValidate="txtNombreCliente" ErrorMessage="El nombre del cliente es obligatorio." CssClass="text-danger small" Display="Dynamic" />
                            </div>
                            <div class="col-md-4">
                                <label for="txtDniCliente" class="form-label">DNI</label>
                                <asp:TextBox ID="txtDniCliente" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvDniCliente" runat="server" ControlToValidate="txtDniCliente" ErrorMessage="El DNI del cliente es obligatorio." CssClass="text-danger small" Display="Static" />
                                <asp:RegularExpressionValidator ID="revDniCliente" runat="server" ControlToValidate="txtDniCliente" ErrorMessage="Debe ser un número entero positivo." CssClass="text-danger small" Display="Static" ValidationExpression="^\d+$" />
                            </div>
                            <div class="col-md-4">
                                <label for="txtDireccionCliente" class="form-label">Dirección</label>
                                <asp:TextBox ID="txtDireccionCliente" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvDireccionCliente" runat="server" ControlToValidate="txtDireccionCliente" ErrorMessage="La dirección del cliente es obligatoria." CssClass="text-danger small" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="row mb-4">
                            <div class="col-md-4">
                                <label for="txtTelefonoCliente" class="form-label">Teléfono</label>
                                <asp:TextBox ID="txtTelefonoCliente" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="rfvTelefonoCliente" runat="server" ControlToValidate="txtTelefonoCliente" ErrorMessage="El teléfono del cliente es obligatorio." CssClass="text-danger small" Display="Dynamic" />
                            </div>
                            <div class="col-md-4">
                                <label for="txtEmailCliente" class="form-label">Email</label>
                                <asp:TextBox ID="txtEmailCliente" runat="server" CssClass="form-control" />
                                <asp:RegularExpressionValidator ID="revEmailCliente" runat="server" ControlToValidate="txtEmailCliente" ErrorMessage="Formato de email inválido." CssClass="text-danger small" Display="Dynamic" ValidationExpression="^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" />
                                <asp:RequiredFieldValidator ID="rfvEmailCliente" runat="server" ControlToValidate="txtEmailCliente" ErrorMessage="El email del cliente es obligatorio." CssClass="text-danger small" Display="Dynamic" />
                            </div>
                        </div>
                        <div class="d-flex align-items-center gap-3">
                            <asp:Button ID="btnAgregarCliente" runat="server" CssClass="btn btn-success" Text="Agregar Cliente" OnClick="btnAgregarCliente_Click" />
                            <asp:Label ID="lblMessage2" runat="server" CssClass="fw-medium text-danger" />
                            <asp:Label ID="lblMessage" runat="server" CssClass="fw-medium text-success" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="card shadow mt-5">
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label for="txtFiltro" class="form-label fs-4 fst-italic">Buscador</label>
                        <asp:TextBox runat="server" placeholder="Buscar cliente..." ID="txtFiltro" CssClass="form-control mt-3" AutoPostBack="true" OnTextChanged="Buscar_TextChanged" />
                    </div>
                </div>
            </div>
        </div>

        <div class="card shadow mt-5">
            <div class="card-header bg-secondary text-white">
                <h4 class="mb-0 fst-italic">Listado de Clientes</h4>
            </div>
            <div class="card-body">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="ClientesGridView" runat="server" CssClass="table table-hover table-bordered align-middle" AutoGenerateColumns="False" OnRowCommand="clientesGridView_RowCommand" AllowPaging="True" PageSize="10" OnPageIndexChanging="ClientesGridView_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="ID" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="dni" HeaderText="DNI" />
                                <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                                <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                                <asp:BoundField DataField="email" HeaderText="Email" />
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <div class="d-flex justify-content-center gap-2">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="editar" CommandArgument='<%# Eval("id") %>' CssClass="p-0 border-0 bg-transparent" CausesValidation="false">
                                                <span class="material-symbols-outlined text-warning">edit</span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="eliminar" CommandArgument='<%# Eval("id") %>' CssClass="p-0 border-0 bg-transparent" OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este cliente?');" CausesValidation="false">
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
                        <asp:Label ID="lblNoResults" runat="server" CssClass="text-danger" Visible="false" Text="No se encontraron clientes." />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
