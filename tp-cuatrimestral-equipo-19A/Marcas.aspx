<%@ Page Title="Configuración de Marcas" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ConfiguracionMarcas.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.ConfiguracionMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="mb-5">
        <h2>Agregar Nueva Marca</h2>
        <div class="row">
            <div class="form-group col-4">
                <label for="txtNombreMarca">Nombre de la Marca</label>
                <asp:TextBox ID="txtNombreMarca" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvNombreMarca" runat="server" ControlToValidate="txtNombreMarca" ErrorMessage="El nombre de la marca es requerido." CssClass="text-danger" Display="Dynamic" />
            </div>
        </div>

        <div class="d-flex align-items-center gap-3">
            <asp:Button ID="btnAgregarMarca" runat="server" CssClass="btn btn-primary mt-3" Text="Agregar Marca" OnClick="btnAgregarMarca_Click" />
            <asp:Label ID="lblMessage2" runat="server" CssClass="mt-3 fw-medium text-danger" />
            <asp:Label ID="lblMessage" runat="server" CssClass="mt-3 fw-medium text-success" />
        </div>
    </div>

    <asp:GridView ID="MarcasGridView" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnRowCommand="marcasGridView_RowCommand">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="ID" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="editar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-sm" CausesValidation="false">
                            <span class="material-symbols-outlined text-warning ">edit</span>
                       </asp:LinkButton>
                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="eliminar" CommandArgument='<%# Eval("id") %>' CssClass="btn btn-sm"
                        OnClientClick="return confirm('¿Estás seguro de que deseas eliminar esta marca?');" CausesValidation="false">
                         <span class="material-symbols-outlined text-danger">delete</span>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
