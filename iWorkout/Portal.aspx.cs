using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using System.Web.Security;
using Glb = iWorkout.GlobalConfig;
using HMembership = iWorkout.HelperMembership;
using HWebForm = iWorkout.HelperWebForm;


//local 
using ProfileActivityId = System.Int32;
using SubscriptionId = System.Int32;
using ActivityLogViewStateIndex = System.Int32;

namespace iWorkout
{
    public partial class Portal : System.Web.UI.Page
    {
        private Guid userId;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            userId = HMembership.getUserId(HMembership.getCurrentUserName());
            string type =  Model.getUserType(userId);
            TodayPanel.EnableViewState = false;
            SubscriptionPanel.EnableViewState = false;
            AllProgramPanel.EnableViewState = false;
            if (type == "Client")
            {
                ClientPortalPanel.Visible = true;
                CoachPortalPanel.Visible = false;
                if (IsPostBack == false)
                {
                  loadAllClientPanel();
                }
                loadAllProgramPanel();
            }
            else if (type == "Coach")
            {
                EngageClientStatus.Visible = false;
                ClientPortalPanel.Visible = false;
                CoachPortalPanel.Visible = true;
                loadActivityLogPanel();
            }
            else
            {
                Response.Redirect(Glb.DEFAULT_PAGE);
            }         
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (IsPostBack == true)
            {
               loadAllClientPanel();
            }
        }
        private void loadTodayPanel()
        {
            IEnumerable<TodayActivity> todayActivities = Model.loadClientTodayData(userId);
         
            BulletedList todayBullets = new BulletedList();
            TodayPanel.Controls.Add(todayBullets);
            foreach (TodayActivity act in todayActivities)
            {
                string resultText = act.Program.Name +
                    " started on " + act.Subscription.StartDate.ToString("R") +
                    " :    " + act.Activity.Description;
                todayBullets.Items.Add(new ListItem(resultText, act.Subscription.Id.ToString()));
            }
        }

        private void loadAllClientPanel() 
        {
            loadTodayPanel();            
            loadLogPanel(); // This creates a viewstate entry
            loadSubscriptionPanel();
        }

        private void loadLogPanel()
        {
            var todayActivities = Model.loadClientTodayData(userId);
          //  int 
            ViewState[Glb.ACTIVITY_LOG_VS_KEY] = new List<ActivityLogForeignKey>();
            var vsActivityLogItems = HWebForm.getFromViewstate<List<ActivityLogForeignKey>>(ViewState, Glb.ACTIVITY_LOG_VS_KEY);

            LogActivityDropDownList.Items.Clear();
            for (int i = 0; i < todayActivities.Count(); i++ )
            {
                var act = todayActivities.ElementAt(i);
                vsActivityLogItems.Add(
                    new ActivityLogForeignKey()
                    {
                        ProfileActivityID = act.ProfileActivity.Id,
                        SubscriptionID = act.Subscription.Id
                    });
                string resultText = act.Program.Name +
                    " started on " + act.Subscription.StartDate.ToString("r") +
                    ":" + act.Activity.Description;
                var elem = new ListItem(resultText, i.ToString());
                LogActivityDropDownList.Items.Add(elem);
            }
        }

        private void loadSubscriptionPanel()
        {
            IEnumerable<UserSubscription> clientSubscriptions = Model.loadClientSubscriptions(userId);
            BulletedList subscriptionBullets = new BulletedList();
            SubscriptionPanel.Controls.Add(subscriptionBullets);
            AllSubscriptionDropDownList.Items.Clear();
            foreach (var sub in clientSubscriptions)
            {
                string result = sub.program.Name + " started on " +
                      sub.subscription.StartDate.ToString("r");
                subscriptionBullets.Items.Add(new ListItem(result));
                ListItem item = new ListItem(result, sub.subscription.Id.ToString());
                AllSubscriptionDropDownList.Items.Add(item);
            }
        }
 
        private void loadAllProgramPanel() 
        {
            List<ProgramUserAttribute> allProgams = Model.loadUserAllPrograms(userId);
            AllProgramTable.Rows.Clear();
            TableRow headRow = new TableRow();
            headRow.Cells.Add(new TableCell() {Text="Program"});
            headRow.Cells.Add(new TableCell() { Text = "Owned" });
            headRow.Cells.Add(new TableCell() { Text = "Subscribed" });
            AllProgramTable.Rows.Add(headRow);
        
            foreach (var item in allProgams)
            {
                TableRow newRow = new TableRow();
                
                newRow.Cells.Add(new TableCell() { Text = item.Program.Name });
                newRow.Cells.Add(new TableCell() { Text = item.isOwned ? "X" : "" });
                newRow.Cells.Add(new TableCell() { Text = item.isSubscribed ? "X" : "" });
                TableCell buttonCell = new TableCell();
                if(item.isOwned == true )
                {
                    Button subscribeButton = new Button();
                    subscribeButton.Text = "Subscribe";
                    subscribeButton.ID = item.Program.Id.ToString();
                    subscribeButton.Click +=subscribeButton_Click;
                    buttonCell.Controls.Add(subscribeButton);
                }
                newRow.Cells.Add(buttonCell);
                this.AllProgramTable.Rows.Add(newRow);
                
            }
        }

        private void subscribeButton_Click(object sender, EventArgs e)
        {
             int proId = Int32.Parse(((Button)sender).ID);
             if (Model.createSubscription(userId, proId))
             {
                 Response.Write(@"<script>alert("" Subscribe Succeed ! "")</script>");
             }
             else
             {
                 Response.Write(@"<script>alert("" Subscribe Failed ! "")</script>");
             }
             loadAllProgramPanel();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(Glb.CREATE_PROGRAM);
        }


        protected void UnsubscribeButton_Click(object sender, EventArgs e)
        {
            if (AllSubscriptionDropDownList.Items.Count <= 0) { return; }
            SubscriptionId subId = Int32.Parse(AllSubscriptionDropDownList.SelectedValue);
            if (Model.deactivateSubscription(subId))
            {
                Response.Write(@"<script>alert("" Unsubscribe Succeed ! "")</script>");
            }
            else
            {
                Response.Write(@"<script>alert("" Unsubscribe Failed ! "")</script>");
            }
            loadAllProgramPanel();
        }

        protected void SubscribeNewProgramButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Glb.MARKET_PLACE_PAGE);
        }

        [Serializable]
        protected class ActivityLogForeignKey
        {
            public ProfileActivityId ProfileActivityID { get; set; }
            public SubscriptionId SubscriptionID { get; set; }
        }

        protected void SubmitLogButton_Click(object sender, EventArgs e)
        {   
            
            if (LogActivityDropDownList.Items.Count <= 0)
            {
                return; // simple postback
            }
            else
            {
                var vsActivityLogItems = HWebForm.getFromViewstate<List<ActivityLogForeignKey>>(ViewState, Glb.ACTIVITY_LOG_VS_KEY);
                int logIndex = Int32.Parse(LogActivityDropDownList.SelectedValue);
                ActivityLogForeignKey foreignKey = vsActivityLogItems[logIndex];
                string logMemo = MemoTextBox.Text;
                bool logAccomplished = AccomplishedCheckBox.Checked;
                if (Model.createActivityLog(foreignKey.SubscriptionID, foreignKey.ProfileActivityID, logAccomplished, logMemo))
                {
                    Response.Write(@"<script>alert("" Log Succeed ! "")</script>");
                    return; // TODO: 
                }
                else
                {
                    Response.Write(@"<script>alert("" Log Failed ! "")</script>");
                    return; // postback on failure
                }            
            }
        }

        protected void MakeRelationshipButton_Click(object sender, EventArgs e)
        {
            Label statusLabel = EngageClientStatus;
            statusLabel.Visible = true;
            if (ClientNameTextBox.Text.Equals(""))
            {
                statusLabel.Text = "You didn't provide a client username.";
                return;
            }
            MembershipUser client = Membership.GetUser(ClientNameTextBox.Text);
            if (client == null)
            {
                statusLabel.Text = "There is no client by that username.";
                return;
            }
            IEnumerable<User> currentCustomers = Model.loadCoachCustomers(userId);
            foreach (var user in currentCustomers)
            {
                if (user.Id == ((Guid)client.ProviderUserKey))
                {
                    statusLabel.Text = "This is already your client.";
                    return;
                }
            }
            if (Model.createCustomerRelationship(userId, (Guid)client.ProviderUserKey))
            {
                statusLabel.Text = "You have engaged the client.";
                return;
            }
            else
            {
                statusLabel.Text = "Couldn't engage the client right now. Try again later.";
                return; // postback on database fail
            }
            
        }

        protected void CoachSubscribeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Glb.MARKET_PLACE_PAGE);
        }


        private void loadActivityLogPanel()
        {
            IEnumerable<ClientActivityLog> logs = Model.loadActivityLog(userId);

            ActivityLogTable.Rows.Clear();
            TableRow headRow = new TableRow();
            headRow.Cells.Add(new TableCell() { Text = "User Name" });
            headRow.Cells.Add(new TableCell() { Text = "Activity" });
            headRow.Cells.Add(new TableCell() { Text = "Memo" });
            headRow.Cells.Add(new TableCell() { Text = "Date" });
            headRow.Cells.Add(new TableCell() { Text = "Accomplished" });
            ActivityLogTable.Rows.Add(headRow);

            foreach (var item in logs)
            {
                TableRow newRow = new TableRow();

                newRow.Cells.Add(new TableCell() { Text = item.UserName });
                newRow.Cells.Add(new TableCell() { Text = item.ActivityDescription });
                newRow.Cells.Add(new TableCell() { Text = item.ActivityLog.Memo });
                newRow.Cells.Add(new TableCell() { Text = item.ActivityLog.Date.ToString("r") });
                newRow.Cells.Add(new TableCell() { Text = item.ActivityLog.Accomplished ? "YES" : "" });
                ActivityLogTable.Rows.Add(newRow);
            }
        }
    }
}