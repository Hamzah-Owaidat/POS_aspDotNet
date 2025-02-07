<%@ Page Title="Categories" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Dashboard.master" CodeBehind="categories.aspx.cs" Inherits="POS_aspdotnet.Dashboard.Categories.categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DashboardContent" runat="server">
    <div class="d-flex justify-content-between align-items-center">
    <h1>Categories Page</h1>
    <asp:Button ID="btnAddCategories" runat="server" Text="➕" CssClass="btn btn-primary mb-3 btn-sm fw-bold text-white" OnClick="btnAddCategories_Click" />
</div>

<div class="table-responsive mt-3">
    <!-- Repeater to display dynamic employee data -->
    <asp:Repeater ID="rptCategories" runat="server">
        <HeaderTemplate>
            <table class="table table-striped table-bordered">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Created By</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
        </HeaderTemplate>

        <ItemTemplate>
            <tr>
                <td><%# Eval("Id") %></td>
                <td><%# Eval("name") %></td>
                <td><%# Eval("username") %></td>
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