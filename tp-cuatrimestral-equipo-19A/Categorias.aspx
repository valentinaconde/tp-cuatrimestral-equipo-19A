<%@ Page Title="Categorías" Language="C#" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Categorias" MasterPageFile="~/Admin.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<body>
        <div class="container">
            <h1 class="mt-5">Listado de Categorías</h1>
            <asp:GridView ID="CategoriasGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                </Columns>
            </asp:GridView>
        </div>
</body>
    </asp:Content>