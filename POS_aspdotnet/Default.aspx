<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="POS_aspdotnet._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-3">
        <div class="row">
            <!-- Invoice List Box -->
            <div class="col-4 bg-success p-3">
                <h4 class="text-white">Invoice List</h4>
                <asp:ListBox ID="invoiceLb" runat="server" CssClass="form-control w-full" Height="300px"></asp:ListBox>
                <br />
                <p>Total: <asp:Label ID="lblTotalInvoiceAmount" runat="server" CssClass="text-white"></asp:Label></p>
                <asp:Button ID="btnCreate" runat="server" Text="Create Invoive" CssClass="btn btn-danger mt-2" OnClick="btnCreate_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear List" CssClass="btn btn-danger mt-2" OnClick="btnClear_Click" />
            </div>

            <!-- Stocks List -->
            <div class="col-8 main-content overflow-auto ps-5" style="max-height: 80vh;">
                <div class="d-flex flex-wrap gap-3">
                    <asp:Repeater ID="rptStocks" runat="server" OnItemCommand="rptStocks_ItemCommand">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnAddItem" runat="server" CommandName="AddToInvoice" CommandArgument='<%# Eval("Id") %>' CssClass="text-decoration-none">
                                <div class="card p-2 text-center text-dark" style="width: 200px; cursor: pointer;">
                                    <img src='<%# ResolveUrl("~/Images/") + Eval("image") %>' alt="Stock Image" class="img-thumbnail mx-auto" style="width: 100px; height: 100px;" />
                                    <h5><%# Eval("item_name") %></h5>
                                    <p>Quantity: <%# Eval("quantity") %></p>
                                    <p>Price: <%# (Convert.ToDecimal(Eval("price")) * 1.1M).ToString("C") %> $</p>
                                </div>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
