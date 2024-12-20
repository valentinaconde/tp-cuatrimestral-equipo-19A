﻿<%@ Page Title="Facturas" Language="C#" MasterPageFile="~/Sharing.Master" AutoEventWireup="true" CodeBehind="Facturas.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Reportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SharingMain" runat="server">
    <div class="container mt-3">
    <div class="mb-3 ">
        <h2 class="fst-italic fw-medium">Facturacion</h2>
    </div>

    <asp:GridView ID="FacturasGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="facturasGridView_RowCommand" AllowPaging="true" PageSize="10" OnPageIndexChanging="FacturasGridView_PageIndexChanging" OnSelectedIndexChanged="FacturasGridView_SelectedIndexChanged" DataKeyNames="id">
    <Columns>
        <asp:BoundField DataField="id" HeaderText="ID" />
        <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
        <asp:BoundField DataField="total" HeaderText="Total" />
        <asp:BoundField DataField="numero_factura" HeaderText="Numero factura" />
        <asp:TemplateField HeaderText="Cliente">
            <ItemTemplate>
                <%# Eval("cliente_dni") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Vendedor">
            <ItemTemplate>
                <%# Eval("usuario_nombre") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnGenerarFactura" runat="server" CssClass="btn btn-primary mb-5" Text="Generar" OnClick="btnGenerarFactura_Click" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
    <ItemTemplate>
        <asp:Button ID="btnImprimirFactura" runat="server" CssClass="btn btn-secondary mb-5" Text="Imprimir" OnClick="btnImprimirFactura_Click"  AutoPostBack="false" />
    </ItemTemplate>
</asp:TemplateField>
    </Columns>
        <PagerTemplate>
    <div class="d-flex justify-content-center align-items-center">
        <asp:LinkButton ID="lnkPrev" runat="server" CommandName="Page" CommandArgument="Prev" CausesValidation="false" CssClass="btn btn-sm mx-1">
            <span class="material-symbols-outlined text-dark fs-2">chevron_left</span>
        </asp:LinkButton>
        <asp:Label ID="lblPageInfo" runat="server" CssClass="mx-2 mb-1"></asp:Label>
        <asp:LinkButton ID="lnkNext" runat="server" CommandName="Page" CommandArgument="Next" CausesValidation="false" CssClass="btn btn-sm  mx-1">
            <span class="material-symbols-outlined text-dark fs-2">chevron_right</span>
        </asp:LinkButton>
    </div>
</PagerTemplate>
</asp:GridView>
        <asp:Label ID="lblNoResults" runat="server" CssClass="text-danger" Visible="false" Text="No se encontraron Facturas." />
        </div>
</asp:Content>
