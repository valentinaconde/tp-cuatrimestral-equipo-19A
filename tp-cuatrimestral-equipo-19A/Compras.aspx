<%@ Page Title="Compras" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= ddlMarca.ClientID %>').select2();
            $('#<%= ddlCategoria.ClientID %>').select2();
            $('#<%= ddlProveedor.ClientID %>').select2();
        });

        function formatDateString(input) {
            var value = input.value.replace(/\D/g, '');
            var formattedValue = '';
            if (value.length > 2) {
                formattedValue += value.substring(0, 2) + '/';
                if (value.length > 4) {
                    formattedValue += value.substring(2, 4) + '/';
                    formattedValue += value.substring(4, 8);
                } else {
                    formattedValue += value.substring(2, 4);
                }
            } else {
                formattedValue = value;
            }
            input.value = formattedValue;
        }
    </script>
    <div class="container mt-4">
        <h2>Registrar Nueva Compra</h2>
        <label class="text-warning">Para registrar una compra el proveedor debe estar registrado en sistema.</label>
        <div class="d-flex gap-2">
            <div class="form-group d-flex flex-column">
                <label for="ddlProveedor">Proveedor</label>
                <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-control">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvProveedor" runat="server" ControlToValidate="ddlProveedor" InitialValue="" ErrorMessage="Seleccione un proveedor." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label for="txtFecha">Fecha</label>
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" Text='<%# DateTime.Now.ToString("dd/MM/yyyy") %>' onkeyup="formatDateString(this)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFecha" runat="server" ControlToValidate="txtFecha" ErrorMessage="La fecha es obligatoria." CssClass="text-danger" Display="Dynamic" />
            </div>
        </div>
        <div class="d-flex gap-2">
            <div class="form-group">
                <label for="txtProducto">Producto</label>
                <asp:TextBox ID="txtProducto" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvProducto" runat="server" ControlToValidate="txtProducto" ErrorMessage="El campo es obligatorio." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label for="txtCantidad">Cantidad</label>
                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ControlToValidate="txtCantidad" ErrorMessage="El campo es obligatorio." CssClass="text-danger" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revCantidad" runat="server" ControlToValidate="txtCantidad" ErrorMessage="Ingrese una cantidad válida." CssClass="text-danger" Display="Dynamic" ValidationExpression="^\d+$" />
            </div>
            <div class="form-group">
                <label for="txtPrecio">Precio</label>
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPrecio" runat="server" ControlToValidate="txtPrecio" ErrorMessage="El campo es obligatorio." CssClass="text-danger" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revPrecio" runat="server" ControlToValidate="txtPrecio" ErrorMessage="Ingrese un precio válido." CssClass="text-danger" Display="Dynamic" ValidationExpression="^\d+(\.\d{1,2})?$" />
            </div>
        </div>
        <div class="row w-50">
            <div class="form-group d-flex flex-column col-6">
                <label for="ddlCategoria">Categoria</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCategoria" InitialValue="" ErrorMessage="Seleccione una categoria." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="form-group d-flex flex-column col-6">
                <label for="ddlMarca">Marca</label>
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMarca" InitialValue="" ErrorMessage="Seleccione una categoria." CssClass="text-danger" Display="Dynamic" />
            </div>
        </div>
        <asp:Button ID="btnAgregarProducto" runat="server" CssClass="btn btn-secondary mt-3" Text="Agregar Producto" OnClick="btnAgregarProducto_Click" />
        <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered mt-3">
            <Columns>
                <asp:BoundField DataField="Producto" HeaderText="Producto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnRegistrarCompra" runat="server" CssClass="btn btn-primary mt-3" Text="Registrar Compra" OnClick="btnRegistrarCompra_Click" />
    </div>
        <asp:TextBox ID="txtErrorCompras" runat="server" CssClass="form-control" />

</asp:Content>
