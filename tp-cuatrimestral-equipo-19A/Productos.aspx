<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Productos" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-3">
        <h2>Agregar Nuevo Producto</h2>
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
                <asp:RegularExpressionValidator ID="revStockActual" runat="server" ControlToValidate="txtStockActual" ErrorMessage="El stock actual debe ser un número entero positivo." CssClass="text-danger" Display="Dynamic"  ValidationExpression="^\d+$" />
            </div>
            <div class="form-group col-4">
                <label for="txtStockMinimo">StockMinimo</label>
                <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvStockMinimo" runat="server" ControlToValidate="txtStockMinimo" ErrorMessage="El stock minimo del producto es obligatorio." CssClass="text-danger" Display="Static" />
                <asp:RegularExpressionValidator ID="revStockMinimo" runat="server" ControlToValidate="txtStockMinimo" ErrorMessage="El stock minimo debe ser un número entero positivo." CssClass="text-danger" Display="Dynamic"  ValidationExpression="^\d+$" />
            </div>
        </div>
        <div class="row">
            <div class="form-group col-4">
                <label for="txtPorcentajeGanancia">PorcentajeGanancia</label>
                <asp:TextBox ID="txtPorcentajeGanancia" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvPorcentajeGanancia" runat="server" ControlToValidate="txtPorcentajeGanancia" ErrorMessage="El Porcentaje de ganacia del producto es obligatorio." CssClass="text-danger" Display="Static" />
                <asp:RangeValidator ID="rvPorcentajeGanancia" runat="server"  ControlToValidate="txtPorcentajeGanancia" MinimumValue="0" MaximumValue="100" Type="Double" ErrorMessage="El porcentaje de ganancia debe estar entre 0 y 100." CssClass="text-danger" Display="Dynamic" />
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

   

        <asp:GridView ID="ProductosGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="productosGridView_RowCommand">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="stockactual" HeaderText="StockActual" />
                <asp:BoundField DataField="stockminimo" HeaderText="StockMinimo" />
                <asp:BoundField DataField="ganancia" HeaderText="Porcentaje de Ganancia" />
                <asp:BoundField DataField="idmarca" HeaderText="Idmarca" />
                <asp:BoundField DataField="idcategoria" HeaderText="Idcategoria" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="editar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-warning btn-sm" CausesValidation="false" />
                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Estás seguro de que deseas eliminar el producto?');" CausesValidation="false" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
</asp:Content>

