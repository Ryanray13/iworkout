using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using BusinessLayer;
using Glb = iWorkout.GlobalConfig;
using HMembership = iWorkout.HelperMembership;
using HWebForm = iWorkout.HelperWebForm;
using sysout = System.Diagnostics.Debug;

namespace iWorkout
{
    public partial class MarketPlace : System.Web.UI.Page
    {
        private Guid userId;
        private string userType;
        private const int MAX_ROWS = 50;

        protected void Page_Load(object sender, EventArgs e)
        {
            userId = HMembership.getUserId(HMembership.getCurrentUserName());
            userType = Model.getUserType(userId);
            sysout.WriteLine("The current user type: " + userType);

            if (userType == "Client")
            {
                PanelCoachAccess.Visible = false;
            }
            else if (userType == "Coach")
            {
                PanelClientAccess.Visible = false;
                if (IsPostBack == false)
                {
                    loadClientCustomers(DropDownListCurrentClients);
                }
            }
            else
            {
                return;// do something bad usually from database.
            }

            if (IsPostBack == false) {
                loadPrograms(ListBoxAvailablePrograms);
            }
        }

        private void loadPrograms(ListBox container)
        {
            Func<ListBox, int> displayRowCount = delegate(ListBox lbox)
                { return Math.Min(lbox.Items.Count + 1, MAX_ROWS); };
            IEnumerable<Program> pubPrograms = Model.loadProgramsWithPublicity(true);
            foreach (Program prog in pubPrograms)
            {
                container.Items.Add(new ListItem(prog.Name, prog.Id.ToString()));
            }
            container.Rows = displayRowCount(container);
        }

        private void loadClientCustomers(DropDownList container)
        {
            var customers = Model.loadCoachCustomers(userId);
            foreach (var customer in customers) {
                container.Items.Add(new ListItem(customer.Handle, customer.Id.ToString()));
            }
        }

        protected void ButtonCoachSubscribe_Click(object sender, EventArgs e)
        {
            if (DropDownListCurrentClients.Items.Count <= 0 
                || ListBoxAvailablePrograms.Items.Count <= 0)
            {
                return; // empty activities postback to current page
            }
            else
            {
                int programId;
                Guid clientId;
                if (Int32.TryParse(ListBoxAvailablePrograms.SelectedValue, out programId) 
                    && Guid.TryParse(DropDownListCurrentClients.SelectedValue, out clientId)
                    && Model.createSubscription(clientId, programId))
                {
                    // it was successful
                    Response.Redirect(Glb.PORTAL);
                }
                else
                {
                    return; // failed to parse
                }
            }
        }

        protected void ButtonClientSubscribe_Click(object sender, EventArgs e)
        {
            int programId;
            if (ListBoxAvailablePrograms.Items.Count <= 0)
            {
                // empty list. postback to current page
                return;
            }
            else
            {
                if (Int32.TryParse(ListBoxAvailablePrograms.SelectedValue, out programId)
                    && Model.createSubscription(userId, programId))
                {
                    //success
                    Response.Redirect(Glb.PORTAL);
                }
                else
                {
                    // failed to parse. postback to current page
                    return;
                }
            }
        }
    }
}