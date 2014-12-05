using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iWorkout
{
    public partial class Slideshow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static AjaxControlToolkit.Slide[] GetSlides()
        {
            AjaxControlToolkit.Slide[] imgSlide = new AjaxControlToolkit.Slide[4];

            imgSlide[0] = new AjaxControlToolkit.Slide("Images/workout1.jpg", "Autumn", "Autumn Leaves");
            imgSlide[1] = new AjaxControlToolkit.Slide("Images/gym.jpg", "Creek", "Creek");
            imgSlide[2] = new AjaxControlToolkit.Slide("Images/Desert Landscape.jpg", "Landscape", "Landscape");
            imgSlide[3] = new AjaxControlToolkit.Slide("Images/Dock.jpg", "Dock", "Dock");

            return (imgSlide);
        }
    }
}