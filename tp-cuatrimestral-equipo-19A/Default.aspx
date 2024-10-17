<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div>
            <asp:Label ID="EmailLabel" runat="server" Text="Email:" AssociatedControlID="EmailTextBox" />
            <asp:TextBox ID="EmailTextBox" runat="server" CssClass="form-control" />
            <asp:RegularExpressionValidator ID="EmailValidator" runat="server"
                ControlToValidate="EmailTextBox"
                ErrorMessage="Por favor ingrese un email valido."
                ValidationExpression="^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$"
                CssClass="text-danger"
                ValidationGroup="LoginGroup" />
        </div>

        <div>
            <asp:Label ID="PasswordLabel" runat="server" Text="Password:" AssociatedControlID="PasswordTextBox" />
            <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" CssClass="form-control" />
            <div>
                <asp:RequiredFieldValidator ID="PasswordRequiredValidator" runat="server"
                    ControlToValidate="PasswordTextBox"
                    ErrorMessage="Password is required."
                    CssClass="text-danger"
                    Display="Dynamic"
                    ValidationGroup="LoginGroup" />

                <asp:RegularExpressionValidator ID="PasswordValidator" runat="server"
                    ControlToValidate="PasswordTextBox"
                    ErrorMessage="La contraseña debe tener minimo 8 caracteres."
                    ValidationExpression=".{8,}"
                    CssClass="text-danger"
                    Display="Dynamic"
                    ValidationGroup="LoginGroup" />
            </div>
        </div>

        <asp:Button ID="LoginButton" runat="server" Text="Iniciar sesion" CssClass="btn btn-primary mt-5" OnClick="LoginButton_Click" ValidationGroup="LoginGroup" />
    </main>

</asp:Content>
