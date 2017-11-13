using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheDriver
{
    public partial class Master : System.Web.UI.MasterPage
    {
        internal bool _access = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserSettings"] != null)
            {
                greet.Visible = true;
                account.Visible = false;
                greetingText.InnerText = "Hello " + Server.HtmlEncode(Request.Cookies["UserSettings"]["FirstName"]);
            }
            else
            {
                greet.Visible = false;
                account.Visible = true;
            }
        }
    }
}