<%@ Page Title="" Language="C#" MasterPageFile="~/Vendedor.Master" AutoEventWireup="true" CodeBehind="HomeVendedorPage.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.HomeVendedorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <main aria-labelledby="title">
        <h1 id="title" class="fst-italic fw-light">Bienvenido, <%# usuario.nombre%></h1>
        <div class="d-flex mt-4">
            <a href="Ventas.aspx" class="btn btn-secondary mt-2 w-25 btn-lg mx-2 d-flex align-items-center gap-2 justify-content-center">
                Generar nueva venta <span class="material-symbols-outlined">attach_money</span>
            </a>
            <a href="Facturas.aspx" class="btn btn-secondary mt-2 w-25 btn-lg mx-2 d-flex align-items-center gap-2 justify-content-center">
                Facturacion <span class="material-symbols-outlined">summarize</span>
            </a>
        </div>
    </main>
</asp:Content>
