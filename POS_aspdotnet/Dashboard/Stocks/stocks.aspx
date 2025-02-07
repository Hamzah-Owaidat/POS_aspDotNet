<%@ Page Title="Stocks" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Dashboard.master" CodeBehind="stocks.aspx.cs" Inherits="POS_aspdotnet.Dashboard.Stocks.stocks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DashboardContent" runat="server">
    <div class="d-flex justify-content-between align-items-center">
        <h1>Stocks Page</h1>
        <asp:Button ID="btnAddStock" runat="server" Text="➕" CssClass="btn btn-primary mb-3 btn-sm fw-bold text-white" OnClick="btnAddStock_Click" />
    </div>
    <div class="table-responsive mt-3">
        <asp:Repeater ID="rptStocks" runat="server">
            <HeaderTemplate>
                <table class="table table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Image</th>
                            <th>Item Name</th>
                            <th>Quantity</th>
                            <th>Price</th>
                            <th>PON</th>
                            <th>Category</th>
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
                    <td>
                        <img src='<%# ResolveUrl("~/Images/") + Eval("image") %>' alt="Stock Image" class="img-thumbnail" style="width: 50px; height: 50px;" />
                    </td>
                    <td><%# Eval("item_name") %></td>
                    <td><%# Eval("quantity") %></td>
                    <td><%# Eval("price") %></td>
                    <td><%# Eval("PON") %></td>
                    <td><%# Eval("username") %></td>
                    <td><%# Eval("name") %></td>
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

        <!-- Pagination -->
        <div class="d-flex justify-content-center align-items-center mt-3">
            <nav aria-label="Page navigation" class="d-flex align-items-center">
                <asp:HyperLink ID="btnPrevious" runat="server" CssClass="btn btn-outline-success me-3" NavigateUrl='<%# "?page=" + (CurrentPage - 1) %>'>
                    <
                </asp:HyperLink>
                
                <asp:Label ID="lblCurrentPage" runat="server" CssClass="mx-3"></asp:Label>
                
                <asp:HyperLink ID="btnNext" runat="server" CssClass="btn btn-outline-success ms-3" NavigateUrl='<%# "?page=" + (CurrentPage + 1) %>'>
                    >
                </asp:HyperLink>
            </nav>
        </div>
    </div>
</asp:Content>