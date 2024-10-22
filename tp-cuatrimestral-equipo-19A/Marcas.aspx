<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Marcas" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

    <title>Marcas</title>

<link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1 class="mt-5">Listado de Marcas</h1>
            <asp:GridView ID="MarcasGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>

</html>
