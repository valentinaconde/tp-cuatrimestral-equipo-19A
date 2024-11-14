<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Reportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-3">
        <h2>Facturacion</h2>
    </div>

    <asp:GridView ID="FacturasGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="facturasGridView_RowCommand" AllowPaging="true" PageSize="10" OnPageIndexChanging="FacturasGridView_PageIndexChanging" OnSelectedIndexChanged="FacturassGridView_SelectedIndexChanged">
    <Columns>
        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
        <asp:BoundField DataField="total" HeaderText="Total" />
        <asp:BoundField DataField="numero_factura" HeaderText="Factura" />
        <asp:BoundField DataField="cliente_id" HeaderText="Cliente" />
        <asp:BoundField DataField="usuario_id" HeaderText="Usuario" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="editar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-sm" CausesValidation="false">
                    <span class="material-symbols-outlined text-warning ">edit</span>
                 </asp:LinkButton>
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
</asp:Content>
