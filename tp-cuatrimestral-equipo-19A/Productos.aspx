<%@ Page Title="Productos" Language="C#" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Productos" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Productos</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1 class="mt-5">Productos Disponibles</h1>
            <asp:GridView ID="ProductosGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="stock" HeaderText="Stock" />
                    <asp:BoundField DataField="ganancia" HeaderText="Ganancia" />
                    <asp:BoundField DataField="idmarca" HeaderText="Idmarca" />
                    <asp:BoundField DataField="idcategoria" HeaderText="Idcategoria" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>

