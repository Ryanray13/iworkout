using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using BusinessLayer;
using Glb = iWorkout.GlobalConfig;
using HMembership = iWorkout.HelperMembership;

//local using
using VsDay = System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, string>>;

namespace iWorkout
{
    public partial class CreateProgram : System.Web.UI.Page
    {
        private Guid userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            userId = HMembership.getUserId(HMembership.getCurrentUserName());
            if (!IsPostBack)
            {
                this.ProfileDetailPanel.Visible = false;
                IEnumerable<ProfileDetail> availableProfiles = Model.loadUserProfiles(userId);
                foreach (var prof in availableProfiles)
                {
                    ListItem newProf = new ListItem("profile " + prof.Id.ToString(), prof.Id.ToString());
                    this.AvailableProfiles.Items.Add(newProf);
                }
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            foreach (ListItem item in ChosenProfiles.Items) 
            {
                item.Attributes.Add("class", "checkBoxListItem");
            }
        }

        protected void AddProfiles_Click(object sender, EventArgs e)
        {
            int[] chosenIndices = AvailableProfiles.GetSelectedIndices();
            for (int i = 0; i < chosenIndices.Count(); i++)
            {
                int currentIndex = chosenIndices[i];
                ListItem currentChosenItem = AvailableProfiles.Items[currentIndex];
                ChosenProfiles.Items.Add(currentChosenItem);
                currentChosenItem.Selected = false;
            }
        }

        protected void RemoveProfiles_Click(object sender, EventArgs e)
        {           
            List<ListItem> selectedProfiles = ChosenProfiles.Items.Cast<ListItem>().ToList();  
            int count = selectedProfiles.Count;
            for (int i = count-1 ; i >= 0; i-- )
            {
                if (selectedProfiles[i].Selected == true)
                {
                    ChosenProfiles.Items.RemoveAt(i);
                }
            }
         }

        protected void SubmitProgram_Click(object sender, EventArgs e)
        {
            if (!IsValid) { return; }
            string name = CreateProgramNameTextBox.Text;
            List<int> profiles = new List<int>();
            foreach (ListItem item in ChosenProfiles.Items)
	        {
                int val = -1;
                Int32.TryParse(item.Value, out val);
                profiles.Add(val);
	        }
            bool createProg = Model.createProgram(userId, name, profiles);
            if (createProg == false)
            {
                this.Response.Write(@"<script>alert("" Creating Program failed ! "")</script>");
            }
            else
            {
                // TODO: REDIRECT TO CLIENT OR COACH
                Response.Redirect(Glb.PORTAL);
            }
        }

        protected void CreateProfileButton_Click(object sender, EventArgs e)
        {
            //Server.Transfer(Glb.CREATE_PROFILE); // This doesn't update the URL string
            Response.Redirect(Glb.CREATE_PROFILE);
        }

        protected void ShowProfileDetailButton_Click(object sender, EventArgs e)
        {
            this.ProfileDetailPanel.Visible = true;
            int[] chosenIndices = AvailableProfiles.GetSelectedIndices();
            for (int i = 0; i < chosenIndices.Count(); i++)
            {
                int currentIndex = chosenIndices[i];
                int profileId =Int32.Parse( AvailableProfiles.Items[currentIndex].Value);

                List<VsDay> profileDetail = Model.getProfileDetail(profileId);
                if (profileDetail == null) { return; }

                var newProfilePanel = new Panel();
                newProfilePanel.BorderStyle = BorderStyle.Dashed;
                var profileLabel = new Label();
                profileLabel.Text=AvailableProfiles.Items[currentIndex].Text;
                newProfilePanel.Controls.Add(profileLabel);
                
                int dayIndex = 0;
                foreach (VsDay dayCheckBox in profileDetail)
                {
                    Panel newDayPanel = new Panel()
                    {
                        BorderStyle = BorderStyle.Dashed,
                        Height = 200,
                        Width = 200,
                        ScrollBars = ScrollBars.Both,
                        CssClass = "dayPanel"
                    };
                    CheckBoxList newDayActivities = new CheckBoxList();
                    newDayPanel.Controls.Add(new Label { Text = "Day" + (dayIndex + 1) });
                    dayIndex++;
                    newDayPanel.Controls.Add(newDayActivities);
                    
                    foreach (KeyValuePair<int, String> activity in dayCheckBox)
                    {
                        ListItem restoreActivity =
                            new ListItem(activity.Value, activity.Key.ToString());
                        restoreActivity.Attributes.Add("class", "checkBoxListItem");
                        newDayActivities.Items.Add(restoreActivity);
                        restoreActivity.Selected = false;
                    } 

                    newProfilePanel.Controls.Add(newDayPanel);
                    
                }
                ProfileDetailPanel.Controls.Add(newProfilePanel);
            }
        }

        protected void CancelProgram_Click(object sender, EventArgs e)
        {
            Response.Redirect(Glb.PORTAL);
        }
            
    }
}
