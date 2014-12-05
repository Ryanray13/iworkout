using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iWorkout
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        
        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static AjaxControlToolkit.Slide[] GetSlides()
        {
            AjaxControlToolkit.Slide[] imgSlide = new AjaxControlToolkit.Slide[4];
            imgSlide[0] = new AjaxControlToolkit.Slide("Images/cc.jpg", "workout", "workout");
            imgSlide[1] = new AjaxControlToolkit.Slide("Images/gym.jpg", "Gym", "Gym");
            imgSlide[2] = new AjaxControlToolkit.Slide("Images/gym2.JPG", "gym view", "gym view");
            imgSlide[3] = new AjaxControlToolkit.Slide("Images/workout1.jpg", "workout", "workout");

      //      imgSlide[3] = new AjaxControlToolkit.Slide("images/Dock.jpg", "Dock", "Dock");
            return (imgSlide);
        }
         
    }
}