<%@ Page Title="Add Stock" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Dashboard.master" CodeBehind="add.aspx.cs" Inherits="POS_aspdotnet.Dashboard.Stocks.add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DashboardContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">Add New Stock</h2>

        <div class="card p-4 shadow-sm">
            <asp:Literal ID="ltMessage" runat="server"></asp:Literal>

            <div class="mb-3">
                <label class="form-label">Item Name</label>
                <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" placeholder="Enter item name" required></asp:TextBox>
            </div>

            <div class="mb-3">
                <label class="form-label">Quantity</label>
                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" TextMode="Number" placeholder="Enter quantity" required></asp:TextBox>
            </div>

            <div class="mb-3">
                <label class="form-label">Price</label>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" placeholder="Enter price" required></asp:TextBox>
            </div>

            <div class="mb-3">
                <label class="form-label">PON (Purchase Order Number)</label>
                <asp:TextBox ID="txtPON" runat="server" CssClass="form-control" placeholder="Enter PON" required></asp:TextBox>
            </div>

            <div class="mb-3">
                <label class="form-label">Category</label>
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" required>
                    <asp:ListItem Value="" Text="Select Category" />
                </asp:DropDownList>
            </div>

            <div class="mb-3">
                <label class="form-label">Upload Image</label>
                <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control" />
            </div>

            <div class="d-flex gap-2">
                <asp:Button ID="btnSaveStock" runat="server" CssClass="btn btn-success" Text="Save Stock" OnClick="btnSaveStock_Click" />
                <a href="stocks.aspx" class="btn btn-secondary">Back to Stocks</a>
            </div>
        </div>
    </div>
</asp:Content>
