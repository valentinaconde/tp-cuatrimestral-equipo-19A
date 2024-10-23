<%@ Page Title="Marcas" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Marcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <h1 class="mt-5">Listado de Marcas</h1>
        <asp:GridView ID="MarcasGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" />
                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
