using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using Glb = iWorkout.GlobalConfig;

// local using
using sysout = System.Diagnostics.Debug;

namespace iWorkout
{
    public static class HelperMembership
    {
        public static string getCurrentUserName()
        {
            MembershipUser currentUser = Membership.GetUser();
            try
            {
                return currentUser.UserName;
            }
            catch (NullReferenceException nullPointer)
            {
                if (Glb.DEBUG_MODE == true) { return Glb.DEBUG_USER; }
                else { throw nullPointer; }
            }
        }

        public static Guid getUserId(string userName)
        {
            if (Glb.DEBUG_MODE == true) { sysout.WriteLine("current user: " + userName); }
            try
            {
                return (Guid)Membership.GetUser(userName).ProviderUserKey;
            }
            catch (NullReferenceException nullPointer)
            {
                throw nullPointer;
            }
            catch (InvalidCastException badCast)
            {
                throw badCast;
            }
        }
    }

    public static class HelperWebForm
    {
        public static CheckBoxList getFirstCheckBoxList(Control parentContainer)
        {
            foreach (Control childControl in parentContainer.Controls)
            {
                if (childControl.GetType() == typeof(CheckBoxList))
                {
                    return (CheckBoxList)childControl;
                }
            }
            return null;
        }

        public static T getFirstControl<T>(Control parentContainer) where T : System.Web.UI.Control
        {
            foreach (Control childControl in parentContainer.Controls)
            {
                if (childControl.GetType() == typeof(T))
                {
                    return (T)childControl;
                }
            }
            return null;
        }

        public static T getFromViewstate<T>(StateBag viewstate, string vsKey)
        {
            return (T)viewstate[vsKey];
        }

    }
}