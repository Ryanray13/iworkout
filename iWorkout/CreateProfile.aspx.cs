using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using DataLayer;
using BusinessLayer;
using Glb = iWorkout.GlobalConfig;
using HMembership = iWorkout.HelperMembership;
using HWebForm = iWorkout.HelperWebForm;

// Local using
using VsDay = System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int, string>>;
using VsCheckBox = System.Collections.Generic.List<bool>;

namespace iWorkout
{
    public partial class CreateProfile : System.Web.UI.Page
    {
        private Guid userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            userId = HMembership.getUserId(HMembership.getCurrentUserName());

            ActivityConfirmLabel.Text = "";
            ProfileDetailPanel.EnableViewState = false;

            if (ViewState[Glb.PROFILE_DETAIL_VIEWSTATE_KEY] == null)
            { ViewState[Glb.PROFILE_DETAIL_VIEWSTATE_KEY] = new List<VsDay>(); }


            if (ViewState[Glb.PREVIOUS_URL_VIEWSTATE_KEY] == null)
            {
                try
                {
                    ViewState[Glb.PREVIOUS_URL_VIEWSTATE_KEY] = Request.UrlReferrer.ToString();
                }
                catch (NullReferenceException)
                {
                    ViewState[Glb.PREVIOUS_URL_VIEWSTATE_KEY] = Glb.DEFAULT_PAGE;
                }
                catch (Exception)
                {
                    ViewState[Glb.PREVIOUS_URL_VIEWSTATE_KEY] = Glb.DEFAULT_PAGE;
                }
            }

            if (IsPostBack == false)
            {
                ActivityDetailPanel.Visible = false;
            }
            else
            {
                var viewState_profileDetails = HWebForm.getFromViewstate<List<VsDay>>(ViewState, Glb.PROFILE_DETAIL_VIEWSTATE_KEY);
                int dayIndex = 0;//
                foreach (VsDay dayCheckBox in viewState_profileDetails)
                {
                    Panel newDayPanel = new Panel()
                    {
                        BorderStyle = BorderStyle.Dashed,
                        Height = 200,
                        Width = 200,
                        ScrollBars = ScrollBars.Both,
                        CssClass = "dayPanel"
                    };

                    CheckBoxList newDayActivities = new CheckBoxList() { Width = 500}; 
                    newDayPanel.Controls.Add(new Label { Text = "Day " + (dayIndex + 1)  });
                    dayIndex++;
                    newDayPanel.Controls.Add(newDayActivities);
                    ProfileDetailPanel.Controls.Add(newDayPanel);
                    foreach (KeyValuePair<int, String> activity in dayCheckBox)
                    {
                        ListItem restoreActivity = new ListItem(activity.Value, activity.Key.ToString());
                        newDayActivities.Items.Add(restoreActivity);
                        restoreActivity.Attributes.Add("class", "checkBoxListItem");
                    } 
                    // We would have liked this to happen after viewstate is restored
                }
            } // end IsPostBack == false
        }

        protected void ShowActivityDetailButton_Click(object sender, EventArgs e)
        {
            loadCorrectCheckBoxListViewState();
            ActivityDetailPanel.Visible = true;
        }

        protected void SubmitActivityButton_Click(object sender, EventArgs e)
        {
            loadCorrectCheckBoxListViewState();
            if (IsValid == false)
            {
                return; // do nothing.
            }
            string description = ActDescripTextBox.Text;
            string parseNum = AvailableActivityType.SelectedValue;
            int activityType;
            if (Int32.TryParse(parseNum, out activityType)
                && Model.createActivity(userId, description, activityType))
            {
                ActivityDetailPanel.Visible = false;
                ActivityConfirmLabel.Text = "Your activity was successfully created!";
                AvailableActivities.DataBind(); // Consider dynamically loading.
            }
            else
            {
                ActivityDetailPanel.Visible = true;
                ActivityConfirmLabel.Text = "Failed to create activity.";
            }
        }

        protected void AddDayButton_Click(object sender, EventArgs e)
        {
            loadCorrectCheckBoxListViewState();
            if (ViewState[Glb.PROFILE_DETAIL_VIEWSTATE_KEY] == null)
            {
                ViewState[Glb.PROFILE_DETAIL_VIEWSTATE_KEY] = new List<VsDay>();
            }
            var loadMe = (List<VsDay>)ViewState[Glb.PROFILE_DETAIL_VIEWSTATE_KEY];
            loadMe.Add(new VsDay()); // adding an empty day to viewstate

            Panel newDayPanel = new Panel()
            {
                BorderStyle = BorderStyle.Dashed,
                Height = 200,
                Width = 200,
                ScrollBars = ScrollBars.Both,
                CssClass = "dayPanel"
            };  // #
            CheckBoxList newDayActivities = new CheckBoxList();

            //            newDayActivities.EnableViewState = false; //
            int currentDayCount = SelectedAddDay.Items.Count;//
            newDayPanel.Controls.Add(new Label { Text = "Day " + (currentDayCount + 1) });

            newDayPanel.Controls.Add(newDayActivities);
            ProfileDetailPanel.Controls.Add(newDayPanel);


            SelectedAddDay.Items.Add(new ListItem(
                "Day " + (currentDayCount + 1), currentDayCount.ToString()));
            SelectedRemoveDay.Items.Add(new ListItem(
                "Day " + (currentDayCount + 1), currentDayCount.ToString()));
            int updatedDayCount = SelectedAddDay.Items.Count;
            SelectedAddDay.SelectedValue = "" + (updatedDayCount - 1);
        }

        protected void RemoveDayButton_Click(object sender, EventArgs e)
        {
       
            loadCorrectCheckBoxListViewState();
            if (SelectedRemoveDay.Items.Count <= 0)
            {
                return; // viewstate is implied empty
            }
            int selectedDayIndex = Int32.Parse(SelectedRemoveDay.SelectedValue);
            var loadMe = (List<VsDay>)ViewState[Glb.PROFILE_DETAIL_VIEWSTATE_KEY];
            loadMe.RemoveAt(selectedDayIndex);

            List<VsCheckBox> changedCheckBox = new List<VsCheckBox>();

            ProfileDetailPanel.Controls.RemoveAt(selectedDayIndex);
             for (int dayIndex = selectedDayIndex; dayIndex < ProfileDetailPanel.Controls.Count; dayIndex++)
             {
                 Panel dayPanel = (Panel)ProfileDetailPanel.Controls[dayIndex];
                 Label dayLabel = HWebForm.getFirstControl<Label>(dayPanel);
                 dayLabel.Text = "Day " + (dayIndex + 1);
             }

            for(int dayIndex = 0; dayIndex < ProfileDetailPanel.Controls.Count; dayIndex ++)
            {
                Panel dayPanel = (Panel)ProfileDetailPanel.Controls[dayIndex];
                CheckBoxList dayCheckBox = HWebForm.getFirstControl<CheckBoxList>(dayPanel);
                var checkBox = new VsCheckBox();
                foreach(ListItem item in dayCheckBox.Items)
                {
                    if(item.Selected == true)
                    {
                        checkBox.Add(true);
                    }
                    else
                    {
                        checkBox.Add(false);
                    }
                }
                changedCheckBox.Add(checkBox);
            }

            if(ViewState[Glb.PROFILE_DETAIL_CHECKBOXLIST_KEY]==null)
            {
                ViewState[Glb.PROFILE_DETAIL_CHECKBOXLIST_KEY] = changedCheckBox;
            }


            int currentDayCount = SelectedRemoveDay.Items.Count;
            SelectedAddDay.Items.RemoveAt(currentDayCount - 1);
            SelectedRemoveDay.Items.RemoveAt(currentDayCount - 1);
            int updatedDayCount = SelectedRemoveDay.Items.Count;
            SelectedRemoveDay.SelectedValue = "" + (updatedDayCount - 1);
            if (ViewState[Glb.PROFILE_DETAIL_CHECKBOXLIST_KEY] == null)
            {

            }
        }

        protected void ActivityAddButton_Click(object sender, EventArgs e)
        {
            loadCorrectCheckBoxListViewState();
            if (SelectedAddDay.Items.Count <= 0
                || AvailableActivities.Items.Count <= 0)
            {
                return;
            }

            int selectedDayIndex = Int32.Parse(SelectedAddDay.SelectedValue);
            ListItem selectedActivity = AvailableActivities.SelectedItem;
            int selectedActivityId = Int32.Parse(selectedActivity.Value);

            var loadMe = (List<VsDay>)ViewState[Glb.PROFILE_DETAIL_VIEWSTATE_KEY];
            VsDay selectedVsDay = loadMe[selectedDayIndex];
            selectedVsDay.Add(new KeyValuePair<int, string>(selectedActivityId, selectedActivity.Text));

            var dayPanel = (Panel)ProfileDetailPanel.Controls[selectedDayIndex];

            //Might return null
            var selectedDayCheckBox = HWebForm.getFirstCheckBoxList(dayPanel);
            clearAllSelection();

            selectedDayCheckBox.Items.Add(selectedActivity);
            selectedActivity.Attributes.Add("class", "checkBoxListItem");

            selectedActivity.Selected = true;
        }

        protected void SubmitProfileButton_Click(object sender, EventArgs e)
        {
            loadCorrectCheckBoxListViewState();
            if (ViewState[Glb.PROFILE_DETAIL_VIEWSTATE_KEY] == null) { return; }
            var loadMe = (List<VsDay>)ViewState[Glb.PROFILE_DETAIL_VIEWSTATE_KEY];
            var newProfileActivities = new List<List<int>>();
            foreach (VsDay day in loadMe)
            {
                var persistDay = new List<int>();
                foreach (var activityPair in day) // we want the pair.key
                {
                    persistDay.Add(activityPair.Key);
                }
                newProfileActivities.Add(persistDay);
            }
            // TODO possibly handle cyclerepeatcount. currently default to 1.
            if (Model.createProfile(userId, newProfileActivities))
            {
                // Successfully created a profile
                Response.Write(@"<script>alert("" Your profile was successfully created ! "")</script>");
                var reqPrevious = (String)ViewState[Glb.PREVIOUS_URL_VIEWSTATE_KEY];
                Response.Redirect(reqPrevious.ToString());
                // TODO ensure that user is logged in!
            }
            else
            {
                // Failed to create profile
                Response.Write(@"<script>alert("" Creating Profile failed ! "")</script>");
            }
        }

        protected void CancelActivityButton_Click(object sender, EventArgs e)
        {
            loadCorrectCheckBoxListViewState();
            ActivityDetailPanel.Visible = false;
        }

        protected void RemoveActivitiesButton_Click(object sender, EventArgs e)
        {
            loadCorrectCheckBoxListViewState();
            if (ViewState[Glb.PROFILE_DETAIL_VIEWSTATE_KEY] == null) { return; }
            var loadMe = (List<VsDay>)ViewState[Glb.PROFILE_DETAIL_VIEWSTATE_KEY];

            for (int vsDayIndex = loadMe.Count - 1; vsDayIndex >= 0; vsDayIndex--)
            {

                var dayPanel = (Panel)ProfileDetailPanel.Controls[vsDayIndex];
                var dayCheckBox = HWebForm.getFirstCheckBoxList(dayPanel);
                for (int dayActIndex = dayCheckBox.Items.Count - 1; dayActIndex >= 0; dayActIndex--)
                {
                    if (dayCheckBox.Items[dayActIndex].Selected == true)
                    {
                        loadMe[vsDayIndex].RemoveAt(dayActIndex);
                        dayCheckBox.Items.RemoveAt(dayActIndex);
                    }
                }
            }
        }

        protected void CancelProfileButton_Click(object sender, EventArgs e)
        {
            var reqPrevious = (String)ViewState[Glb.PREVIOUS_URL_VIEWSTATE_KEY];
            Response.Redirect(reqPrevious.ToString());
        }

        protected void DeselectButton_Click(object sender, EventArgs e)
        {
            clearAllSelection();
        }

        private void clearAllSelection()
        {
            foreach (Panel dayActivities in ProfileDetailPanel.Controls)
            {
                var dayCheckBox = HWebForm.getFirstCheckBoxList(dayActivities);
                foreach (ListItem dayActivity in dayCheckBox.Items)
                {
                    dayActivity.Selected = false;
                }
            }
        }

        private void loadCorrectCheckBoxListViewState()
        {
            if(ViewState[Glb.PROFILE_DETAIL_CHECKBOXLIST_KEY]==null)
            {
                return;
            }
            else
            {
                var checkBoxListViewState = HWebForm.getFromViewstate<List<VsCheckBox>>(ViewState,Glb.PROFILE_DETAIL_CHECKBOXLIST_KEY);
                for (int dayIndex = 0; dayIndex < ProfileDetailPanel.Controls.Count; dayIndex++)
                {
                    Panel dayPanel = (Panel)ProfileDetailPanel.Controls[dayIndex];
                    CheckBoxList dayCheckBox = HWebForm.getFirstControl<CheckBoxList>(dayPanel);
                    var checkBox = new VsCheckBox();
                    for (int index = 0; index < dayCheckBox.Items.Count; index++ )
                    {
                        if (checkBoxListViewState[dayIndex][index]==true)
                        {
                            dayCheckBox.Items[index].Selected = true;
                        }
                        else
                        {
                            dayCheckBox.Items[index].Selected = false;
                        }
                    }
                }
                ViewState[Glb.PROFILE_DETAIL_CHECKBOXLIST_KEY] = null;
            }
        }
    }
}