<%@ Page Title="Invoice Details" Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/Dashboard.master" CodeBehind="invoiceDetails.aspx.cs" Inherits="POS_aspdotnet.Dashboard.Invoices.invoiceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DashboardContent" runat="server">
    <div class="container">
        <h2 class="mb-3">Invoice Details</h2>
        
        <!-- Invoice Header -->
        <div class="card p-3 mb-3">
            <h4>Invoice Information</h4>
            <p><strong>Invoice ID:</strong> <asp:Label ID="lblInvoiceId" runat="server"></asp:Label></p>
            <p><strong>Employee Name:</strong> <asp:Label ID="lblEmployeeName" runat="server"></asp:Label></p>
            <p><strong>Date:</strong> <asp:Label ID="lblInvoiceDate" runat="server"></asp:Label></p>
        </div>

        <!-- Invoice Items Table -->
        <div class="table-responsive">
            <asp:Repeater ID="rptInvoiceItems" runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Item Name</th>
                                <th>Quantity</th>
                                <th>Price</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("ItemName") %></td>
                        <td><%# Eval("Quantity") %></td>
                        <td><%# Eval("Price", "{0:C}") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>

        <!-- Total Amount -->
        <div class="text-end mt-3">
            <h4><strong>Total:</strong> <asp:Label ID="lblTotalAmount" runat="server" CssClass="fw-bold"></asp:Label></h4>
        </div>

        <!-- Back Button -->
        <div class="mt-3">
            <asp:Button ID="btnBack" runat="server" Text="Back to Invoices" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
        </div>
    </div>
</asp:Content>
