<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OlvideContra.aspx.cs" Inherits="tp_cuatrimestral_equipo_19A.OlvideContra" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Olvidé mi Contraseña</title>
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
                    <div class="w-25 mt-5">
                        <asp:Label ID="EmailLabel" runat="server" Text="Email:" AssociatedControlID="EmailTextBox" />
                        <asp:TextBox ID="EmailTextBox" runat="server" CssClass="form-control" placeholder="example@example.com" />
                        <asp:RegularExpressionValidator ID="EmailValidator" runat="server"
                            ControlToValidate="EmailTextBox"
                            ErrorMessage="Por favor ingrese un email valido."
                            ValidationExpression="^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$"
                            CssClass="text-danger"
                            ValidationGroup="RecoverGroup" />
                    </div>
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />
                    <asp:Button ID="btnRecuperar" runat="server" Text="Recuperar Contraseña" CssClass="btn btn-primary mt-5" OnClick="btnRecuperar_Click" ValidationGroup="RecoverGroup" />
                    <asp:HyperLink ID="volverInicio" runat="server" NavigateUrl="~/Default.aspx" CssClass="btn btn-link mt-3">
                        Volver a inicio
                    </asp:HyperLink>
                </div>
            </main>
        </div>
    </form>
</body>
</html>

