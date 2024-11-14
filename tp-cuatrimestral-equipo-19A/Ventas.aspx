﻿<%@ Page Title="Ventas" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Ventas.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Ventas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= ddlCliente.ClientID %>').select2();
            $('#<%= ddlProducto.ClientID %>').select2(); // Added this line
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
        .height {
            height: 27px;
            border: 1px solid #AAAAAA;
            width: 240px;
        }
    </style>
    <div class="container mt-4">
        <h2>Registrar Nueva Venta</h2>
        <label class="text-warning">Para registrar una venta el cliente debe estar registrado en sistema.</label>
        <div class="d-flex gap-2">
            <div class="form-group d-flex flex-column">
                <label for="ddlCliente">Cliente</label>
                <asp:DropDownList ID="ddlCliente" runat="server" CssClass="form-control height">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCliente" runat="server" ControlToValidate="ddlCliente" InitialValue="" ErrorMessage="Seleccione un cliente." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div class="form-group">
                <label for="txtFecha">Fecha</label>
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control height" Text='<%# DateTime.Now.ToString("dd/MM/yyyy") %>' onkeyup="formatDateString(this)"></asp:TextBox>
                <asp:CustomValidator ID="cvFecha" runat="server" ControlToValidate="txtFecha" ErrorMessage="Formato de fecha inválido." CssClass="text-danger" Display="Dynamic" ClientValidationFunction="validateDateFormat" />
            </div>
        </div>
        <div class="d-flex gap-2">
            <div class="form-group d-flex flex-column">
                <label for="ddlProducto">Producto</label>
                <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control height" AutoPostBack="true" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>

                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCliente" InitialValue="" ErrorMessage="Seleccione un cliente." CssClass="text-danger" Display="Dynamic" />
            </div>
            <div>
                <div class="form-group">
                    <label for="txtCantidad">Cantidad</label>
                    <asp:TextBox ID="txtCantidad" CssClass="form-control height" Text="1" runat="server" AutoPostBack="true" OnTextChanged="txtCantidad_TextChanged"></asp:TextBox>

                    <asp:RegularExpressionValidator ID="revCantidad" runat="server" ControlToValidate="txtCantidad" ErrorMessage="Ingrese una cantidad válida." CssClass="text-danger" Display="Dynamic" ValidationExpression="^\d+$" />
                </div>


                <div>
                    <label for="txtPrecio">Precio del producto: $ </label>
                    <asp:Label ID="txtPrecio" runat="server" CssClass=""></asp:Label>
                </div>
            </div>


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
    <div class="d-flex align-items-center gap-2">
        <asp:Button ID="btnRegistrarVenta" runat="server" CssClass="btn btn-primary mt-3" Text="Registrar Venta" OnClick="btnRegistrarVenta_Click" />
        <asp:Label ID="txtErrorVentas" runat="server" CssClass="w-100 border-0 text-danger mt-2" />

    </div>
</asp:Content>
