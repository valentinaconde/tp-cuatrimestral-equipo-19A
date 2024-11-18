<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <div class="card shadow">
            <div class="card-header bg-secondary text-white">
                <h2 class="mb-0 fst-italic">Administración de productos</h2>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <label for="txtNombreProducto" class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvNombreProducto" runat="server" ControlToValidate="txtNombreProducto" ErrorMessage="El nombre del Producto es obligatorio." CssClass="text-danger small" Display="Static" />
                    </div>
                    <div class="col-md-4 mb-3">
                        <label for="txtStockActual" class="form-label">Stock Actual</label>
                        <asp:TextBox ID="txtStockActual" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvStockProducto" runat="server" ControlToValidate="txtStockActual" ErrorMessage="El stock del producto es obligatorio." CssClass="text-danger small" Display="Static" />
                        <asp:RegularExpressionValidator ID="revStockActual" runat="server" ControlToValidate="txtStockActual" ErrorMessage="Debe ser un número entero positivo." CssClass="text-danger small" Display="Dynamic" ValidationExpression="^\d+$" />
                    </div>
                    <div class="col-md-4 mb-3">
                        <label for="txtPrecioUnitario" class="form-label">Precio Unitario</label>
                        <asp:TextBox ID="txtPrecioUnitario" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvPrecioUnitario" runat="server" ControlToValidate="txtPrecioUnitario" ErrorMessage="El precio unitario es obligatorio." CssClass="text-danger small" Display="Static" />
                        <asp:RegularExpressionValidator ID="revPrecioUnitario" runat="server" ControlToValidate="txtPrecioUnitario" ErrorMessage="Debe ser un número entero positivo." CssClass="text-danger small" Display="Dynamic" ValidationExpression="^\d+$" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4 mb-3">
                        <label for="txtPorcentajeGanancia" class="form-label">Porcentaje de Ganancia</label>
                        <asp:TextBox ID="txtPorcentajeGanancia" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvPorcentajeGanancia" runat="server" ControlToValidate="txtPorcentajeGanancia" ErrorMessage="Este campo es obligatorio." CssClass="text-danger small" Display="Static" />
                    </div>
                    <div class="col-md-4 mb-3">
                        <label for="ddlMarca" class="form-label">Marca</label>
                        <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvMarca" runat="server" ControlToValidate="ddlMarca" InitialValue="" ErrorMessage="La marca es requerida." CssClass="text-danger small" Display="Static" />
                    </div>
                    <div class="col-md-4 mb-3">
                        <label for="ddlCategoria" class="form-label">Categoría</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" ControlToValidate="ddlCategoria" InitialValue="" ErrorMessage="La categoría es requerida." CssClass="text-danger small" Display="Static" />
                    </div>
                </div>
                <div class="d-flex justify-content-start gap-3">
                    <asp:Button ID="btnAgregaProducto" runat="server" CssClass="btn btn-success" Text="Agregar Producto" OnClick="btnAgregarProducto_Click" />
                    <asp:Label ID="lblMessage2" runat="server" CssClass="fw-medium text-danger" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="fw-medium text-success" />
                </div>
            </div>
        </div>

        <div class="card shadow mt-5">
            <div class="card-header bg-secondary text-white">
                <h4 class="mb-0 fst-italic">Listado de Productos</h4>
            </div>
            <div class="card-body">
                <asp:GridView ID="ProductosGridView" runat="server" CssClass="table table-hover table-bordered align-middle" AutoGenerateColumns="False" OnRowCommand="productosGridView_RowCommand" AllowPaging="true" PageSize="10" OnPageIndexChanging="ProductosGridView_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" />
                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="stockactual" HeaderText="Stock" />
                        <asp:BoundField DataField="precio_unitario" HeaderText="Precio" />
                        <asp:BoundField DataField="ganancia" HeaderText="Ganancia (%)" />
                        <asp:TemplateField HeaderText="Marca">
                            <ItemTemplate>
                                <%# Eval("idmarca") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Categoría">
                            <ItemTemplate>
                                <%# Eval("idcategoria") %>
                            </ItemTemplate>
                        </asp:TemplateField>
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
            </div>
        </div>
    </div>
</asp:Content>
