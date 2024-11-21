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

        

        function validateDateFormat(sender, args) {
            var datePattern = /^(0[1-9]|1[0-9]|2[0-9]|3[01])\/(0[1-9]|1[0-2])\/\d{4}$/;
            args.IsValid = datePattern.test(args.Value);
        }
    </script>
    <style>
        .height{
            height: 27px;
            border: 1px solid #AAAAAA;
            width: 200px;
        }
    </style>
    <div class="container mt-4">
        <h2 class="fst-italic bg-secondary text-white p-2">Registrar Nueva Compra</h2>
        <label class="text-danger mb-2">Para registrar una compra el proveedor debe estar registrado en sistema.</label>
        <div class="d-flex gap-2">
            <div class="form-group d-flex flex-column">
                <label for="ddlProveedor">Proveedor</label>
                <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-control height">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvProveedor" runat="server" ControlToValidate="ddlProveedor" InitialValue="" ErrorMessage="Seleccione un proveedor." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label for="txtFecha">Fecha</label>
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control height" Text='<%# DateTime.Now.ToString("dd/MM/yyyy") %>' onkeyup="formatDateString(this)"></asp:TextBox>
                <asp:CustomValidator ID="cvFecha" runat="server" ControlToValidate="txtFecha" ErrorMessage="Formato de fecha inválido." CssClass="text-danger" Display="Dynamic" ClientValidationFunction="validateDateFormat" />

            </div>
        </div>
        <div class="d-flex gap-2 mt-3">
            <div class="form-group">
                <label for="txtProducto">Producto</label>
                <asp:TextBox ID="txtProducto" runat="server" CssClass="form-control height"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtCantidad">Cantidad</label>
                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control height"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revCantidad" runat="server" ControlToValidate="txtCantidad" ErrorMessage="Ingrese una cantidad válida." CssClass="text-danger" Display="Dynamic" ValidationExpression="^\d+$" />
            </div>
        
        </div>
           <div class="d-flex gap-2 mt-3">
      <div class="form-group">
        <label for="txtPrecio">Precio unitario</label>
        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control height"></asp:TextBox>
        <asp:RegularExpressionValidator ID="revPrecio" runat="server" ControlToValidate="txtPrecio" ErrorMessage="Ingrese un precio válido." CssClass="text-danger" Display="Dynamic" ValidationExpression="^\d+(\.\d{1,2})?$" />
    </div>
                  <div class="form-group">
     <label for="txtPrecio">Porcentaje de ganancia</label>
     <asp:TextBox ID="txtPorcentaje" runat="server" CssClass="form-control height"></asp:TextBox>
     <asp:RegularExpressionValidator ID="revPorcentaje" runat="server" ControlToValidate="txtPorcentaje" ErrorMessage="Ingrese un porcentaje válido." CssClass="text-danger" Display="Dynamic" ValidationExpression="^\d+(\.\d{1,2})?$" />
 </div>
   </div>
        <div class="d-flex justify-content-start gap-2 mt-3">
            <div class="form-group d-flex flex-column">
                <label for="ddlCategoria">Categoria</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control height">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
            </div>
            <div class="form-group d-flex flex-column">
                <label for="ddlMarca">Marca</label>
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control height">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
            </div>
        </div>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
        <asp:Button ID="btnAgregarProducto" runat="server" CssClass="btn btn-secondary mt-3" Text="Agregar Producto" OnClick="btnAgregarProducto_Click" />
        <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered mt-3">
            <Columns>
                <asp:BoundField DataField="Producto" HeaderText="Producto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="Precio" HeaderText="Precio unitario" />
                <asp:BoundField DataField="Porcentaje" HeaderText="Porcentaje de ganancia" />
                <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
                <asp:BoundField DataField="Marca" HeaderText="Marca" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm" Text="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' OnClick="btnEliminar_Click">
                            <span class="material-symbols-outlined text-danger">delete</span>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="d-flex align-items-center gap-2">
            <asp:Button ID="btnRegistrarCompra" runat="server" CssClass="btn btn-primary mt-3" Text="Registrar Compra" OnClick="btnRegistrarCompra_Click" />
            <asp:Label ID="txtErrorCompras" runat="server" CssClass="w-100 border-0 text-danger mt-3" />
        </div>
             </ContentTemplate>
</asp:UpdatePanel>

    </div>
</asp:Content>
