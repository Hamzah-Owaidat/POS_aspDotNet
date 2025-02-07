using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace POS_aspdotnet
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            routes.MapPageRoute("Employees", "Dashboard/Employees", "~/Dashboard/Employees/employees.aspx");
            routes.MapPageRoute("EmployeesAdd", "Dashboard/Employees/Add", "~/Dashboard/Employees/add.aspx");
            routes.MapPageRoute("EmployeesEdit", "Dashboard/Employees/Edit", "~/Dashboard/Employees/edit.aspx");
            routes.MapPageRoute("EmployeesDelete", "Dashboard/Employees/Delete", "~/Dashboard/Employees/delete.aspx");

            routes.MapPageRoute("Stocks", "Dashboard/Stocks", "~/Dashboard/Stocks/stocks.aspx");
            routes.MapPageRoute("StocksAdd", "Dashboard/Stocks/Add", "~/Dashboard/Stocks/add.aspx");
            routes.MapPageRoute("StocksEdit", "Dashboard/Stocks/Edit", "~/Dashboard/Stocks/edit.aspx");
            routes.MapPageRoute("StocksDelete", "Dashboard/Stocks/Delete", "~/Dashboard/Stocks/delete.aspx");

            routes.MapPageRoute("Categories", "Dashboard/Categories", "~/Dashboard/Categories/categories.aspx");
            routes.MapPageRoute("CategoriesAdd", "Dashboard/Categories/Add", "~/Dashboard/Categories/add.aspx");
            routes.MapPageRoute("CategoriesEdit", "Dashboard/Categories/Edit", "~/Dashboard/Categories/edit.aspx");
            routes.MapPageRoute("CategoriesDelete", "Dashboard/Categories/Delete", "~/Dashboard/Categories/delete.aspx");

            routes.MapPageRoute("Invoices", "Dashboard/Invoices", "~/Dashboard/Invoices/invoices.aspx");
            routes.MapPageRoute("InvoiceDetails", "Dashboard/Invoices/InvoiceDetails", "~/Dashboard/Invoices/invoiceDetails.aspx");
            routes.MapPageRoute("InvoicesDelete", "Dashboard/Invoices/Delete", "~/Dashboard/Invoices/delete.aspx");
        }
    }
}
