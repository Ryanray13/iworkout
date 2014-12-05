using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;


// local using
using sysout = System.Diagnostics.Debug;

namespace iWorkout
{
    // TODO ensure that user is logged in, otherwise we redirect to some page
    public static class GlobalConfig
    {
        public static bool DEBUG_MODE = false;
        public const string DEBUG_USER = "Ray";

        public const string PROFILE_DETAIL_VIEWSTATE_KEY = "ProfileDetailViewStateKey";
        public const string PREVIOUS_URL_VIEWSTATE_KEY = "PreviousUrlViewStateKey";
        public const string PROFILE_DETAIL_CHECKBOXLIST_KEY = "CheckBoxlistViewStateKey";
        public const string ACTIVITY_LOG_VS_KEY = "activityLogViewStateKey";

        public const string USER_TYPE_DEFAULT = "1"; 
        public const string LOGIN_PAGE = "~/Login.aspx";
        public const string DEFAULT_PAGE = "~/";
        public const string PORTAL = "~/Portal.aspx";
        public const string CREATE_PROGRAM = "~/CreateProgram.aspx";
        public const string CREATE_PROFILE = "~/CreateProfile.aspx";
        public const string MARKET_PLACE_PAGE = "~/MarketPlace.aspx";

    }

}