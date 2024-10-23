<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
</asp:Content>
