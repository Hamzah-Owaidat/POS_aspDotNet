<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="POS_aspdotnet.Auth.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - POS System</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.2/css/all.min.css" integrity="sha512-Evv84Mr4kqVGRNSgIGL/F/aIDqQb7xQ2vcrdIwxfjThSH8CSR7PBEakCr51Ck+w+/U6swU2Im1vVX0SVk9ABhg==" crossorigin="anonymous" referrerpolicy="no-referrer" />
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <div class="container d-flex justify-content-center align-items-center vh-100">
            <div class="card shadow-lg p-4 rounded-3" style="width: 350px;">
                <div class="text-center mb-3">
                    <h4 class="text-success fw-bold">POS System</h4>
                    <p class="text-muted">Login to continue</p>
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger fw-bold"></asp:Label>
                </div>

                <div class="mb-3">
                    <label class="form-label">Username</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Enter username"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Username is required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>

                <div class="mb-3">
                    <label class="form-label">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" CssClass="text-danger"></asp:RequiredFieldValidator>
                </div>

                <div class="d-grid">
                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-success fw-bold" Text="Login" OnClick="btnLogin_Click" />
                </div>

                <div class="text-center mt-3">
                    <a href="#" class="text-decoration-none text-success">Forgot Password?</a>
                </div>
            </div>
        </div>
    </form>

    <style>
        .card {
            border: none;
            border-radius: 12px;
        }

        .btn-success {
            background-color: #28a745;
            border: none;
        }

        .btn-success:hover {
            background-color: #218838;
        }
    </style>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/Scripts/bootstrap.js") %>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.7.2/js/all.min.js" integrity="sha512-b+nQTCdtTBIRIbraqNEwsjB6UvL3UEMkXnhzd8awtCYh0Kcsjl9uEgwVFVbhoj3uu1DO1ZMacNvLoyJJiNfcvg==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    </asp:PlaceHolder>
</body>
</html>
