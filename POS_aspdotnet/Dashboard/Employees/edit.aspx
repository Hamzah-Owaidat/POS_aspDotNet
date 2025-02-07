<%@ Page Title="Edit Employee" Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/Dashboard.master" CodeBehind="edit.aspx.cs" Inherits="POS_aspdotnet.Dashboard.Employees.edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DashboardContent" runat="server">
        <div class="container form-container">
            <h2>Edit Employee</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <div class="mb-3">
                <label for="txtUsername" class="form-label">Username</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtPassword" class="form-label">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="ddlRole" class="form-label">Role</label>
                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Select Role" Value="" />
                    <asp:ListItem Text="Admin" Value="1" />
                    <asp:ListItem Text="Cashier" Value="2" />
                </asp:DropDownList>
            </div>

            <div class="mb-3">
                <label for="txtSalary" class="form-label">Salary</label>
                <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="d-flex justify-content-between">
                <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-success" OnClick="btnSave_Click" />
                <a href="/Dashboard/Employees/employees" class="btn btn-secondary">Cancel</a>
            </div>
        </div>
</asp:Content>
