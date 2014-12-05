using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using BusinessLayer;
using Glb = iWorkout.GlobalConfig;

namespace iWorkout
{
    public partial class Navigation : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            FormsAuthentication.SignOut();
        }

        protected void PortalLink_Click(object sender, EventArgs e)
        {
            Response.Redirect(Glb.PORTAL);
        }
    }
}