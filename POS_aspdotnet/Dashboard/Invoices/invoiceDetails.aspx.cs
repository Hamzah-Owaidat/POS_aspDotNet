using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace POS_aspdotnet.Dashboard.Invoices
{
    public partial class invoiceDetails : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null || !(bool)Session["Authenticated"])
            {
                Response.Redirect("~/Auth/Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadInvoiceDetails();
            }
        }

        private void LoadInvoiceDetails()
        {
            // Get Invoice ID from Query String
            string invoiceId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(invoiceId))
            {
                Response.Redirect("invoices.aspx"); // Redirect if no invoice ID is provided
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Fetch Invoice Header Details
                    string queryHeader = @"SELECT i.Id, i.created_at, e.username AS EmployeeName 
                                           FROM invoices i
                                           JOIN employees e ON i.created_by = e.Id
                                           WHERE i.Id = @InvoiceId";
                using (SqlCommand cmd = new SqlCommand(queryHeader, conn))
                {
                    cmd.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblInvoiceId.Text = reader["Id"].ToString();
                        lblEmployeeName.Text = reader["EmployeeName"].ToString();
                        lblInvoiceDate.Text = Convert.ToDateTime(reader["created_at"]).ToString("yyyy-MM-dd");
                    }
                    reader.Close();
                }

                // Fetch Invoice Items
                string queryItems = @"SELECT i.item_name AS ItemName, i.quantity AS Quantity, i.price AS Price
                                      FROM invoice_details i
                                      WHERE i.invoice_id = @InvoiceId";

                using (SqlDataAdapter da = new SqlDataAdapter(queryItems, conn))
                {
                    da.SelectCommand.Parameters.AddWithValue("@InvoiceId", invoiceId);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Bind to Repeater
                    rptInvoiceItems.DataSource = dt;
                    rptInvoiceItems.DataBind();

                    // Calculate Total Amount
                    decimal totalAmount = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        totalAmount += Convert.ToDecimal(row["Price"]);
                    }
                    lblTotalAmount.Text = totalAmount.ToString("C");
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("invoices.aspx");
        }
    }
}
