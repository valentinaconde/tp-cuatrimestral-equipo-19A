﻿<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A._Default" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Home Page</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .text-danger {
            color: red;
        }

        .mt-5 {
            margin-top: 3rem;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <main>

                <div class="d-flex flex-column align-items-center mt-5">
                    <!-- TO-DO: ELIMINAR TEXTO INFORMATIVO    -->
                    <p class="text-danger ">
                        TEXTO INFORMATIVO PARA EL GRUPO(luego sera borrado)<br/>
                        para iniciar sesion como "admin" poner email == "test@prueba.com" && password == "password123"<br/>
                        para iniciar sesion como vendedor, poner cualquier otra cosa
                    </p>

                    <div class="w-25 mt-5">
                        <asp:Label ID="EmailLabel" runat="server" Text="Email:" AssociatedControlID="EmailTextBox" />
                        <asp:TextBox ID="EmailTextBox" runat="server" CssClass="form-control" />
                        <asp:RegularExpressionValidator ID="EmailValidator" runat="server"
                            ControlToValidate="EmailTextBox"
                            ErrorMessage="Por favor ingrese un email valido."
                            ValidationExpression="^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$"
                            CssClass="text-danger"
                            ValidationGroup="LoginGroup" />
                    </div>

                    <div class="w-25">
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
                </div>

            </main>
        </div>
    </form>
</body>
</html>
