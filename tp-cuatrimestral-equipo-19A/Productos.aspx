﻿<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Productos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-3">
        <h2>Administración de productos</h2>
        <div class="row">
            <div class="form-group col-4">
                <label for="txtNombreProducto">Nombre</label>
                <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombreProducto" runat="server" ControlToValidate="txtNombreProducto" ErrorMessage="El nombre del Producto es obligatorio." CssClass="text-danger" Display="Static" />
            </div>
            <div class="form-group col-4">
                <label for="txtStockActual">StockActual</label>
                <asp:TextBox ID="txtStockActual" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvStockProducto" runat="server" ControlToValidate="txtStockActual" ErrorMessage="El stock del producto es obligatorio." CssClass="text-danger" Display="Static" />
                <asp:RegularExpressionValidator ID="revStockActual" runat="server" ControlToValidate="txtStockActual" ErrorMessage="El stock actual debe ser un número entero positivo." CssClass="text-danger" Display="Dynamic" ValidationExpression="^\d+$" />
            </div>
            <div class="form-group col-4">
                <label for="txtPrecioUnitario">Precio Unitario</label>
                <asp:TextBox ID="txtPrecioUnitario" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvPrecioUnitario" runat="server" ControlToValidate="txtPrecioUnitario" ErrorMessage="El precio unitario del producto es obligatorio." CssClass="text-danger" Display="Static" />
                <asp:RegularExpressionValidator ID="revPrecioUnitario" runat="server" ControlToValidate="txtPrecioUnitario" ErrorMessage="El precio unitario debe ser un número entero positivo." CssClass="text-danger" Display="Dynamic" ValidationExpression="^\d+$" />
            </div>
        </div>
        <div class="row">
            <div class="form-group col-4">
                <label for="txtPorcentajeGanancia">PorcentajeGanancia</label>
                <asp:TextBox ID="txtPorcentajeGanancia" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvPorcentajeGanancia" runat="server" ControlToValidate="txtPorcentajeGanancia" ErrorMessage="El Porcentaje de ganacia del producto es obligatorio." CssClass="text-danger" Display="Static" />
                <asp:RangeValidator ID="rvPorcentajeGanancia" runat="server" ControlToValidate="txtPorcentajeGanancia" MinimumValue="0" MaximumValue="100" Type="Double" ErrorMessage="El porcentaje de ganancia debe estar entre 0 y 100." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="form-group col-4">
                <label for="txtMarcaId">IDMarca</label>
                <asp:TextBox ID="txtMarcaId" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvMarcaId" runat="server" ControlToValidate="txtMarcaId" ErrorMessage="El IDMarca del producto es obligatorio." CssClass="text-danger" Display="Static" />
            </div>
            <div class="form-group col-4">
                <label for="txtCategoriaId">IDCategoria</label>
                <asp:TextBox ID="txtCategoriaId" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvCategoriaId" runat="server" ControlToValidate="txtCategoriaId" ErrorMessage="El IDCategoria del producto es obligatorio." CssClass="text-danger" Display="Static" />
            </div>
        </div>
    </div>

    <div class="d-flex align-items-center gap-3">
        <asp:Button ID="btnAgregaProducto" runat="server" CssClass="btn btn-primary mb-5" Text="Agregar Producto" OnClick="btnAgregarProducto_Click" />
        <asp:Label ID="lblMessage2" runat="server" CssClass="mt-3 fw-medium text-danger" />
        <asp:Label ID="lblMessage" runat="server" CssClass="mt-3 fw-medium text-success" />
    </div>



    <asp:GridView ID="ProductosGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="productosGridView_RowCommand" AllowPaging="true" PageSize="10" OnPageIndexChanging="ProductosGridView_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="stockactual" HeaderText="StockActual" />
            <asp:BoundField DataField="precio_unitario" HeaderText="Precio Unitario" />
            <asp:BoundField DataField="ganancia" HeaderText="Porcentaje de Ganancia" />
            <asp:BoundField DataField="idmarca" HeaderText="Idmarca" />
            <asp:BoundField DataField="idcategoria" HeaderText="Idcategoria" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="editar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-sm" CausesValidation="false">
                        <span class="material-symbols-outlined text-warning ">edit</span>
                     </asp:LinkButton>
                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-sm"
                        OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este producto?');" CausesValidation="false">
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

