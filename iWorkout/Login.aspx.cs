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
    public partial class Login : System.Web.UI.Page
    {
        private DropDownList UserTypeDropDL;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Default text
            CreateUserWizard.CompleteSuccessText = "Your account has been successfully created.";
            //Try to dynamically add DropDownList.
            UserTypeDropDL = (DropDownList)CreateUserWizard.CreateUserStep.ContentTemplateContainer.FindControl("UserTypeDropDownList");
            CreateUserWizard.ContinueDestinationPageUrl = Glb.LOGIN_PAGE;
           
            if (!IsPostBack)
            {
                UserTypeDropDL.SelectedValue = Glb.USER_TYPE_DEFAULT;
                this.RegisterDiv.Visible = false;
                this.ButtonRegister.Visible = true;
            }          
        }

        protected void CreateUserWizard_CreatedUser(object sender, EventArgs e)
        {
            string newHandle = CreateUserWizard.UserName;
            int newType;
            Guid newId = (Guid)Membership.GetUser(CreateUserWizard.UserName).ProviderUserKey;

            if (!(Int32.TryParse(UserTypeDropDL.SelectedValue, out newType) && Model.addMember(newId, newHandle, newType)))
            {
                Membership.DeleteUser(CreateUserWizard.UserName, true);
                this.Response.Write(@"<script>alert("" Creating Member failed ! "")</script>");
                CreateUserWizard.CompleteSuccessText = "You fail to create an account!!!";
            }
            // this.Response.Redirect(DB_REGISTRATION_REDIRECT);
        }

        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            this.RegisterDiv.Visible = true;
        }  

    }
}