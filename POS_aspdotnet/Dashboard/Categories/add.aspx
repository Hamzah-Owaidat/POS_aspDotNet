<%@ Page Title="Add Category" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Dashboard.master" CodeBehind="add.aspx.cs" Inherits="POS_aspdotnet.Dashboard.Categories.add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DashboardContent" runat="server">
    <div class="container mt-4">
            <h2>Add Category</h2>
            <hr />
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />

            <div class="mb-3">
                <label for="txtCategoryName" class="form-label">Category Name</label>
                <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" />
            </div>

            <div class="d-flex gap-2">
                <asp:Button ID="btnSaveCategory" runat="server" CssClass="btn btn-success" Text="Save Category" OnClick="btnSaveCategory_Click" />
                <a href="categories.aspx" class="btn btn-secondary">Back to Categories</a>
            </div>
        </div>
</asp:Content>
