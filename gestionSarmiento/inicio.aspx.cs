using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gestionSarmiento
{
    public partial class inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (helper.validaInt(Convert.ToString(HttpContext.Current.Session["idusuario"])) == 0)
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
}