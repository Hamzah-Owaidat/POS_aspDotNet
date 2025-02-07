<%@ Page Title="Add Employee" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Dashboard.master" CodeBehind="add.aspx.cs" Inherits="POS_aspdotnet.Dashboard.Employees.add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DashboardContent" runat="server">
        <div class="container form-container">
            <h2>Add Employee</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <div class="mb-3">
                <label for="txtUsername" class="form-label">Username</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername"
                    ErrorMessage="Username is required" CssClass="text-danger" Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label for="txtPassword" class="form-label">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="Password is required" CssClass="text-danger" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="Password must be at least 8 characters long" CssClass="text-danger" Display="Dynamic"
                    ValidationExpression=".{8,}" />
            </div>

            <div class="mb-3">
                <label for="ddlRole" class="form-label">Role</label>
                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Role" Value="" />
                    <asp:ListItem Text="Admin" Value="1" />
                    <asp:ListItem Text="Cashier" Value="2" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="ddlRole"
                    InitialValue="" ErrorMessage="Role is required" CssClass="text-danger" Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label for="txtSalary" class="form-label">Salary</label>
                <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSalary" runat="server" ControlToValidate="txtSalary"
                    ErrorMessage="Salary is required" CssClass="text-danger" Display="Dynamic" />
            </div>

            <div class="d-flex justify-content-between">
                <asp:Button ID="btnAdd" runat="server" Text="Add Employee" CssClass="btn btn-success" OnClick="btnAdd_Click" />
                <a href="/Dashboard/Employees/employees" class="btn btn-secondary">Cancel</a>
            </div>
        </div>
</asp:Content>
