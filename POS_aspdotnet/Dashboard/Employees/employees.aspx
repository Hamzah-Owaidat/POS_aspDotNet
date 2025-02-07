<%@ Page Title="Employees" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Dashboard.master" CodeBehind="employees.aspx.cs" Inherits="POS_aspdotnet.Dashboard.Employees.employees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DashboardContent" runat="server">
    <div class="d-flex justify-content-between align-items-center">
        <h1>Employees Page</h1>
        <asp:Button ID="btnAddEmployee" runat="server" Text="➕" CssClass="btn btn-primary mb-3 btn-sm fw-bold text-white" OnClick="btnAddEmployee_Click" />
    </div>

    <div class="table-responsive mt-3">
        <!-- Repeater to display dynamic employee data -->
        <asp:Repeater ID="rptEmployees" runat="server">
            <HeaderTemplate>
                <table class="table table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Username</th>
                            <th>Role</th>
                            <th>Salary</th>
                            <th>Created By</th>
                            <th>Created At</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>

            <ItemTemplate>
                <tr>
                    <td><%# Eval("Id") %></td>
                    <td><%# Eval("username") %></td>
                    <td><%# Eval("role_name") %></td>
                    <td><%# Eval("salary") %></td>
                    <td><%# Eval("created_by") %></td>
                    <td><%# Eval("created_at", "{0:yyyy-MM-dd}") %></td>
                    <td>
                        <a href="edit.aspx?id=<%# Eval("Id") %>" class="btn btn-warning btn-sm">
                            <i class="fa fa-edit"></i>
                        </a>
                        <a href="delete.aspx?id=<%# Eval("Id") %>" class="btn btn-danger btn-sm">
                            <i class="fa fa-trash"></i>
                        </a>
                    </td>
                </tr>
            </ItemTemplate>

            <FooterTemplate>
                    </tbody>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
