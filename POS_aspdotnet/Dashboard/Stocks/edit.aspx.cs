using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace POS_aspdotnet.Dashboard.Stocks
{
    public partial class edit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null || !(bool)Session["Authenticated"])
            {
                // Redirect to login if not authenticated
                Response.Redirect("~/Auth/Login.aspx");
            }

            if (!IsPostBack)
            {
                // Bind categories to dropdown list on first load
                BindCategories();

                // Load stock data
                int stockId = Convert.ToInt32(Request.QueryString["id"]);
                LoadStockData(stockId);
            }
        }

        private void BindCategories()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT Id, name FROM categories";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlCategory.DataSource = reader;
                ddlCategory.DataTextField = "name";  // Text to display
                ddlCategory.DataValueField = "Id";  // Value to store
                ddlCategory.DataBind();
                reader.Close();
            }
        }

        private void LoadStockData(int stockId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "SELECT Id, item_name, quantity, price, PON, image, created_by, category FROM stocks WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", stockId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtItemName.Text = reader["item_name"].ToString();
                    txtQuantity.Text = reader["quantity"].ToString();
                    txtPrice.Text = reader["price"].ToString();
                    txtPON.Text = reader["PON"].ToString();

                    // Load current image
                    string currentImage = reader["image"].ToString();
                    if (!string.IsNullOrEmpty(currentImage))
                    {
                        imgCurrent.ImageUrl = "~/Images/" + currentImage;  // Set the image URL
                    }

                    // Set selected category in dropdown
                    ddlCategory.SelectedValue = reader["category"].ToString();  // Set the category ID
                }
                conn.Close();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int stockId = Convert.ToInt32(Request.QueryString["id"]);
            string itemName = txtItemName.Text;
            int quantity = Convert.ToInt32(txtQuantity.Text);
            decimal price = Convert.ToDecimal(txtPrice.Text);
            string pon = txtPON.Text;
            string imageFileName = fileImage.HasFile ? fileImage.FileName : string.Empty;
            int categoryId = Convert.ToInt32(ddlCategory.SelectedValue);  // Get selected category ID

            // Logic to handle image upload (optional)
            if (fileImage.HasFile)
            {
                // Save new image
                string imagePath = Server.MapPath("~/Images/") + imageFileName;
                fileImage.SaveAs(imagePath);
            }
            else
            {
                // If no new image is uploaded, retain the old image
                string currentImage = imgCurrent.ImageUrl.Replace("~/Images/", ""); // Extract current image filename
                imageFileName = currentImage; // Retain old image
            }

            // Save updated stock data to the database
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "UPDATE stocks SET item_name = @ItemName, quantity = @Quantity, price = @Price, PON = @PON, image = @Image, category = @CategoryId, updated_by = @UpdatedBy, updated_at = GETDATE() WHERE Id = @Id";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemName", itemName);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@PON", pon);
                cmd.Parameters.AddWithValue("@Image", imageFileName);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);  // Insert selected category ID
                cmd.Parameters.AddWithValue("@UpdatedBy", Session["UserID"]);
                cmd.Parameters.AddWithValue("@Id", stockId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            Response.Redirect("stocks.aspx");
        }
    }
}
