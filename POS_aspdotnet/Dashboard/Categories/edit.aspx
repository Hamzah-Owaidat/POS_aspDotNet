<%@ Page Title="Edit Category" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Dashboard.master" CodeBehind="edit.aspx.cs" Inherits="POS_aspdotnet.Dashboard.Categories.edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DashboardContent" runat="server">
    <div class="container mt-4">
            <h2>Edit Stock</h2>
            <hr />
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />

            <div class="mb-3">
                <label for="txtCategoryName" class="form-label">Category Name</label>
                <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-success" OnClick="btnSave_Click" />
                <a href="categories.aspx" class="btn btn-secondary">Cancel</a>
            </div>
        </div>
</asp:Content>
