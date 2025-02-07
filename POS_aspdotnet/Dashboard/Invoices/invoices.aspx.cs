using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POS_aspdotnet.Dashboard.Invoices
{
    public partial class invoices : System.Web.UI.Page
    {
        private const int PageSize = 5;
        protected int CurrentPage { get; private set; }
        protected int TotalPages { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null || !(bool)Session["Authenticated"])
            {
                Response.Redirect("~/Auth/Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadInvoicesData();
            }
        }

        private void LoadInvoicesData()
        {
            CurrentPage = Convert.ToInt32(Request.QueryString["page"] ?? "1");
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Get total records count
                string countQuery = "SELECT COUNT(*) FROM invoices";
                SqlCommand countCmd = new SqlCommand(countQuery, conn);
                int totalRecords = (int)countCmd.ExecuteScalar();

                // Calculate total pages
                TotalPages = (int)Math.Ceiling((double)totalRecords / PageSize);

                // Ensure current page is within valid range
                if (CurrentPage < 1) CurrentPage = 1;
                if (CurrentPage > TotalPages) CurrentPage = TotalPages;

                // Calculate the offset for pagination
                int offset = (CurrentPage - 1) * PageSize;

                // Modified query with pagination
                string query = @"
                    SELECT i.Id, e.username, e.created_at 
                    FROM invoices i 
                    JOIN employees e ON i.created_by = e.Id 
                    ORDER BY i.Id
                    OFFSET @Offset ROWS 
                    FETCH NEXT @PageSize ROWS ONLY";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Offset", offset);
                cmd.Parameters.AddWithValue("@PageSize", PageSize);

                SqlDataReader reader = cmd.ExecuteReader();

                // Bind data to the Repeater control
                rptInvoices.DataSource = reader;
                rptInvoices.DataBind();

                // Setup pagination controls
                SetupPagination();
            }
        }

        private void SetupPagination()
        {
            // Update current page indicator
            lblCurrentPage.Text = $"Page {CurrentPage} of {TotalPages}";

            // Setup Previous button
            btnPrevious.Visible = CurrentPage > 1;
            btnPrevious.NavigateUrl = $"?page={CurrentPage - 1}";

            // Setup Next button
            btnNext.Visible = CurrentPage < TotalPages;
            btnNext.NavigateUrl = $"?page={CurrentPage + 1}";
        }
    }
}