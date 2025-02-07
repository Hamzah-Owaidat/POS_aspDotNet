<%@ Page Title="Edit Stock" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Dashboard.master" CodeBehind="edit.aspx.cs" Inherits="POS_aspdotnet.Dashboard.Stocks.edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DashboardContent" runat="server">
    <div class="container mt-4">
            <h2>Edit Stock</h2>
            <hr />
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" />

            <div class="mb-3">
                <label for="txtItemName" class="form-label">Item Name</label>
                <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="txtQuantity" class="form-label">Quantity</label>
                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="txtPrice" class="form-label">Price</label>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="txtPON" class="form-label">PON</label>
                <asp:TextBox ID="txtPON" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="ddlCategory" class="form-label">Category</label>
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" required>
                    <asp:ListItem Value="" Text="Select Category" />
                </asp:DropDownList>
            </div>

            <div class="mb-3">
                <label for="currentImage" class="form-label">Current Image</label><br />
                <asp:Image ID="imgCurrent" runat="server" CssClass="img-fluid" style="width: 100px; height: 100px" />
            </div>

            <div class="mb-3">
                <label for="fileImage" class="form-label">Item Image</label>
                <asp:FileUpload ID="fileImage" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn btn-success" OnClick="btnSave_Click" />
                <a href="stocks.aspx" class="btn btn-secondary">Cancel</a>
            </div>
        </div>
</asp:Content>