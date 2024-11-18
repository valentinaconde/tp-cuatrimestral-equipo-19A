<%@ Page Title="Home" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="HomeAdminPage.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h1 id="title" class="fst-italic fw-light">Bienvenido, <%# usuario.nombre%></h1>
        <div class="d-flex mt-4">
            <a href="Compras.aspx" class="btn btn-secondary mt-2 w-25 btn-lg mx-2 d-flex align-items-center gap-2 justify-content-center">
                Generar nueva compra <span class="material-symbols-outlined">shopping_cart</span>
            </a>
            <a href="Ventas.aspx" class="btn btn-secondary mt-2 w-25 btn-lg mx-2 d-flex align-items-center gap-2 justify-content-center">
                Generar nueva venta <span class="material-symbols-outlined">attach_money</span>
            </a>
            <a href="Facturas.aspx" class="btn btn-secondary mt-2 w-25 btn-lg mx-2 d-flex align-items-center gap-2 justify-content-center">
                Facturacion <span class="material-symbols-outlined">summarize</span>
            </a>
        </div>
    </main>
</asp:Content>


