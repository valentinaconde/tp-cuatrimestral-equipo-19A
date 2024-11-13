<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Reportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.13/js/select2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%= ddlProveedor.ClientID %>').select2();
            $('#<%= ddlCategoria.ClientID %>').select2();
            $('#<%= ddlMarca.ClientID %>').select2();
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

    <div class="container mt-4">
        <h2>Reportes</h2>
        <div class="d-flex gap-2">
            <div class="form-group">
                <label for="txtFechaDesde">Fecha Desde</label>
                <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control" onkeyup="formatDateString(this)"></asp:TextBox>
                <asp:CustomValidator ID="cvFechaDesde" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Formato de fecha inválido." CssClass="text-danger" Display="Dynamic" ClientValidationFunction="validateDateFormat" />
            </div>
            <div class="form-group">
                <label for="txtFechaHasta">Fecha Hasta</label>
                <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control" onkeyup="formatDateString(this)"></asp:TextBox>
                <asp:CustomValidator ID="cvFechaHasta" runat="server" ControlToValidate="txtFechaHasta" ErrorMessage="Formato de fecha inválido." CssClass="text-danger" Display="Dynamic" ClientValidationFunction="validateDateFormat" />
            </div>
        </div>

        <div class="d-flex gap-2">
            <div class="form-group">
                <label for="ddlProveedor">Proveedor</label>
                <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-control">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="ddlCategoria">Categoría</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <label for="ddlMarca">Marca</label>
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control">
                    <asp:ListItem Text="" Value="" />
                </asp:DropDownList>
            </div>
        </div>

        <asp:Button ID="btnGenerarReporte" runat="server" CssClass="btn btn-primary mt-3" Text="Generar Reporte" OnClick="btnGenerarReporte_Click" />

        <asp:GridView ID="gvReportes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered mt-3">
            <Columns>
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" />
                <asp:BoundField DataField="Producto" HeaderText="Producto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" />
                <asp:BoundField DataField="Total" HeaderText="Total" />
            </Columns>
        </asp:GridView>

        <div class="d-flex gap-2">
            <asp:Button ID="btnExportarPDF" runat="server" CssClass="btn btn-secondary mt-3" Text="Exportar a PDF" OnClick="btnExportarPDF_Click" />
            <asp:Button ID="btnExportarExcel" runat="server" CssClass="btn btn-secondary mt-3" Text="Exportar a Excel" OnClick="btnExportarExcel_Click" />
        </div>
    </div>
</asp:Content>
