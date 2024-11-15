<%@ Page Title="" Language="C#" MasterPageFile="~/Sharing.Master" AutoEventWireup="true" CodeBehind="Contras.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.Contras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SharingMain" runat="server">
    <div class="container mt-3">
        <h2 class="fst-italic fw-medium">Cambiar Contraseña</h2>
        <div class="row">
            <div class="form-group col-4">
                <label for="txtContrasenaActual">Contraseña Actual</label>
                <asp:TextBox ID="txtContrasenaActual" runat="server" CssClass="form-control" TextMode="Password" />
                <asp:RequiredFieldValidator ID="rfvContrasenaActual" runat="server" ControlToValidate="txtContrasenaActual" ErrorMessage="La contraseña actual es obligatoria." CssClass="text-danger" Display="Static" />
            </div>
        </div>
        <div class="row">
            <div class="form-group col-4">
                <label for="txtNuevaContrasena">Nueva Contraseña</label>
                <asp:TextBox ID="txtNuevaContrasena" runat="server" CssClass="form-control" TextMode="Password" />
                <asp:RequiredFieldValidator ID="rfvNuevaContrasena" runat="server" ControlToValidate="txtNuevaContrasena" ErrorMessage="La nueva contraseña es obligatoria." CssClass="text-danger" Display="Static" />
            </div>
        </div>
        <div class="row mt-3">
            <div class="form-group col-4">
                <asp:Button ID="btnCambiarContrasena" runat="server" CssClass="btn btn-primary" Text="Aceptar" OnClick="btnCambiarContrasena_Click" />
                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />
            </div>
        </div>
    </div>
</asp:Content>
