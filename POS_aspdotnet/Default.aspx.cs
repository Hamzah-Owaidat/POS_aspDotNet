using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POS_aspdotnet
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null || !(bool)Session["Authenticated"])
            {
                Response.Redirect("~/Auth/Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadStockData();
                LoadInvoiceList();
            }
        }

        private void LoadStockData()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Id, image, item_name, quantity, price FROM stocks";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                rptStocks.DataSource = reader;
                rptStocks.DataBind();
            }
        }

        protected void rptStocks_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AddToInvoice")
            {
                int itemId = Convert.ToInt32(e.CommandArgument);
                AddItemToInvoice(itemId);
            }
        }

        private void AddItemToInvoice(int itemId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT item_name, price FROM stocks WHERE Id = @ItemId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemId", itemId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string itemName = reader["item_name"].ToString();
                    decimal originalPrice = Convert.ToDecimal(reader["price"]);
                    decimal increasedPrice = originalPrice * 1.1M; // Apply 10% increase

                    Dictionary<int, Tuple<string, int, decimal>> invoiceItems = Session["InvoiceItems"] as Dictionary<int, Tuple<string, int, decimal>> ?? new Dictionary<int, Tuple<string, int, decimal>>();

                    if (invoiceItems.ContainsKey(itemId))
                    {
                        var existingItem = invoiceItems[itemId];
                        invoiceItems[itemId] = new Tuple<string, int, decimal>(existingItem.Item1, existingItem.Item2 + 1, existingItem.Item3 + increasedPrice);
                    }
                    else
                    {
                        invoiceItems[itemId] = new Tuple<string, int, decimal>(itemName, 1, increasedPrice);
                    }

                    Session["InvoiceItems"] = invoiceItems;
                    LoadInvoiceList();
                }
            }
        }


        private void LoadInvoiceList()
        {
            invoiceLb.Items.Clear();
            Dictionary<int, Tuple<string, int, decimal>> invoiceItems = Session["InvoiceItems"] as Dictionary<int, Tuple<string, int, decimal>>;

            decimal totalAmount = 0;

            if (invoiceItems != null)
            {
                foreach (var item in invoiceItems)
                {
                    invoiceLb.Items.Add($"{item.Value.Item1} - Qty: {item.Value.Item2}, Price: {(item.Value.Item3):C}");
                    totalAmount += item.Value.Item3; // Sum total price
                }
            }

            lblTotalInvoiceAmount.Text = totalAmount.ToString("C"); // Display total
        }


        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Dictionary<int, Tuple<string, int, decimal>> invoiceItems = Session["InvoiceItems"] as Dictionary<int, Tuple<string, int, decimal>>;

            if (invoiceItems == null || invoiceItems.Count == 0)
            {
                Response.Write("<script>alert('No items selected for the invoice.');</script>");
                return;
            }

            int? createdBy = Session["UserID"] as int?;
            if (createdBy == null)
            {
                Response.Write("<script>alert('Error: User not authenticated.');</script>");
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Insert into Invoices table
                    string insertInvoiceQuery = @"
                INSERT INTO invoices (created_by, created_at) 
                VALUES (@CreatedBy, GETDATE()); 
                SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdInvoice = new SqlCommand(insertInvoiceQuery, conn, transaction);
                    cmdInvoice.Parameters.AddWithValue("@CreatedBy", createdBy);
                    int invoiceId = Convert.ToInt32(cmdInvoice.ExecuteScalar());

                    // Insert items into Invoice_Details with 10% increased price
                    foreach (var item in invoiceItems)
                    {
                        int itemId = item.Key;
                        string itemName = item.Value.Item1;
                        int quantity = item.Value.Item2;
                        decimal priceWithIncrease = item.Value.Item3 / quantity; // This price already includes the 10% increase

                        string insertDetailQuery = @"
                    INSERT INTO invoice_details (invoice_id, item_name, quantity, price) 
                    VALUES (@InvoiceId, @ItemName, @Quantity, @Price);";

                        SqlCommand cmdDetail = new SqlCommand(insertDetailQuery, conn, transaction);
                        cmdDetail.Parameters.AddWithValue("@InvoiceId", invoiceId);
                        cmdDetail.Parameters.AddWithValue("@ItemName", itemName);
                        cmdDetail.Parameters.AddWithValue("@Quantity", quantity);
                        cmdDetail.Parameters.AddWithValue("@Price", priceWithIncrease);
                        cmdDetail.ExecuteNonQuery();
                    }


                    transaction.Commit();
                    Session["InvoiceItems"] = null;
                    LoadInvoiceList();

                    Response.Write("<script>alert('Invoice created successfully!');</script>");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                }
            }
        }




        protected void btnClear_Click(object sender, EventArgs e)
        {
            Session["InvoiceItems"] = null;
            LoadInvoiceList();
        }
    }
}
