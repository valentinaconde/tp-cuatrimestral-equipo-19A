﻿<%@ Page Title="Gestión de Clientes" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-3">
        <h2>Agregar Nuevo Cliente</h2>
        <div class="row">
            <div class="form-group col-4">
                <label for="txtNombreCliente">Nombre</label>
                <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombreCliente" runat="server" ControlToValidate="txtNombreCliente" ErrorMessage="El nombre del cliente es obligatorio." CssClass="text-danger" Display="Static" />
            </div>
            <div class="form-group col-4">
                <label for="txtDireccionCliente">Dirección</label>
                <asp:TextBox ID="txtDireccionCliente" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvDireccionCliente" runat="server" ControlToValidate="txtDireccionCliente" ErrorMessage="La dirección del cliente es obligatoria." CssClass="text-danger" Display="Static" />
            </div>
        </div>
        <div class="row">
            <div class="form-group col-4">
                <label for="txtTelefonoCliente">Teléfono</label>
                <asp:TextBox ID="txtTelefonoCliente" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvTelefonoCliente" runat="server" ControlToValidate="txtTelefonoCliente" ErrorMessage="El teléfono del cliente es obligatorio." CssClass="text-danger" Display="Static" />
            </div>
            <div class="form-group col-4">
                <label for="txtEmailCliente">Email</label>
                <asp:TextBox ID="txtEmailCliente" runat="server" CssClass="form-control" />
                <asp:RegularExpressionValidator ID="revEmailCliente" runat="server" ControlToValidate="txtEmailCliente" ErrorMessage="Formato de email inválido." CssClass="text-danger" Display="Dynamic" ValidationExpression="^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" />
                <asp:RequiredFieldValidator ID="rfvEmailCliente" runat="server" ControlToValidate="txtEmailCliente" ErrorMessage="El email del cliente es obligatorio." CssClass="text-danger" Display="Static" />
            </div>
        </div>
    </div>

    <div class="d-flex align-items-center gap-3">
        <asp:Button ID="btnAgregarCliente" runat="server" CssClass="btn btn-primary mb-5" Text="Agregar Cliente" OnClick="btnAgregarCliente_Click" OnClientClick="return confirm('¿Estás seguro de que deseas agregar este cliente?');" />
        <asp:Label ID="lblMessage2" runat="server" CssClass="mt-3 fw-medium text-danger" />
        <asp:Label ID="lblMessage" runat="server" CssClass="mt-3 fw-medium text-success" />
    </div>

   

    <asp:GridView ID="ClientesGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="clientesGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="direccion" HeaderText="Dirección" />
            <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
            <asp:BoundField DataField="email" HeaderText="Email" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="editar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-warning btn-sm" CausesValidation="false" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este cliente?');" CausesValidation="false" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
