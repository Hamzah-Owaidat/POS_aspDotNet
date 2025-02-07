<%@ Page Title="Invoices" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Dashboard.master" CodeBehind="invoices.aspx.cs" Inherits="POS_aspdotnet.Dashboard.Invoices.invoices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DashboardContent" runat="server">
    <div class="d-flex justify-content-between align-items-center">
        <h1>Invoices Page</h1>
    </div>
    <div class="table-responsive mt-3">
        <asp:Repeater ID="rptInvoices" runat="server">
            <HeaderTemplate>
                <table class="table table-striped table-bordered">
                    <thead>
                        <tr>
                            <th>ID</th>
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
                    <td><%# Eval("created_at", "{0:yyyy-MM-dd}") %></td>
                    <td>
                        <a href="invoiceDetails.aspx?id=<%# Eval("Id") %>" class="btn btn-warning btn-sm">
                            <i class="fa fa-eye"></i>
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