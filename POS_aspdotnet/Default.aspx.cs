using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace POS_aspdotnet
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Authenticated"] != null && !(bool)Session["Authenticated"])
            {
                // If user is authenticated, redirect to the default page (e.g., Dashboard or home page)
                Response.Redirect("~/Auth/Login.aspx");
            }
        }
    }
}