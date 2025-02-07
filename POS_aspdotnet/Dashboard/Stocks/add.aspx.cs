using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POS_aspdotnet.Dashboard.Stocks
{
    public partial class add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] == null || !(bool)Session["Authenticated"])
            {
                Response.Redirect("~/Auth/Login.aspx");
            }

            // Bind categories to dropdown on first load
            if (!IsPostBack)
            {
                BindCategories();
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
                ddlCategory.DataTextField = "name"; // Text to display
                ddlCategory.DataValueField = "Id"; // Value to store
                ddlCategory.DataBind();
                reader.Close();
            }
        }

        protected void btnSaveStock_Click(object sender, EventArgs e)
        {
            string itemName = txtItemName.Text.Trim();
            int quantity = int.Parse(txtQuantity.Text);
            decimal price = decimal.Parse(txtPrice.Text);
            string pon = txtPON.Text.Trim();
            string createdBy = Session["UserID"]?.ToString();
            string imageFileName = "";
            int categoryId = int.Parse(ddlCategory.SelectedValue); // Get selected category Id

            // Handle Image Upload
            if (fuImage.HasFile)
            {
                string fileExt = Path.GetExtension(fuImage.FileName);
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".jfif" };

                if (Array.Exists(allowedExtensions, ext => ext.Equals(fileExt, StringComparison.OrdinalIgnoreCase)))
                {
                    imageFileName = Guid.NewGuid() + fileExt; // Unique filename
                    string filePath = Server.MapPath("~/Images/") + imageFileName;
                    fuImage.SaveAs(filePath);
                }
                else
                {
                    ltMessage.Text = "<div class='alert alert-danger'>Invalid file type! Only JPG, PNG, GIF, JFIF allowed.</div>";
                    return;
                }
            }

            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "INSERT INTO stocks (item_code, item_name, quantity, price, PON, created_by, image, category, created_at) " +
                           "VALUES (@ItemCode, @ItemName, @Quantity, @Price, @PON, @CreatedBy, @Image, @CategoryId, GETDATE())";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ItemCode", GenerateItemCode());
                cmd.Parameters.AddWithValue("@ItemName", itemName);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@PON", pon);
                cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                cmd.Parameters.AddWithValue("@Image", imageFileName);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId); // Insert the category Id

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Response.Redirect("stocks.aspx");
                }
                else
                {
                    ltMessage.Text = "<div class='alert alert-danger'>Error adding stock.</div>";
                }
            }
        }

        public static string GenerateItemCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefjhijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 9)
                                        .Select(s => s[random.Next(s.Length)])
                                        .ToArray());
        }
    }
}
