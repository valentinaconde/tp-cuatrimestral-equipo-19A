<%@ Page Title="Categorías" Language="C#"  AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Categorias" %>


<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Categorías</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1 class="mt-5">Listado de Categorías</h1>
            <asp:GridView ID="CategoriasGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
