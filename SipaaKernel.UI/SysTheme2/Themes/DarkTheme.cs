using PrismGL2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernel.UI.SysTheme2.Themes
{
    public class DarkTheme : Theme
    {
        /** original version
         
        protected override ColorDictionnary InitColorDictionnary()
        {
            ColorDictionnary cd = new ColorDictionnary();
            cd.AccentIdle = Color.FromARGB(255, 73, 9, 158);
            cd.AccentHover = Color.FromARGB(255, 57, 7, 122);
            cd.AccentClicked = Color.FromARGB(255, 39, 6, 82);
            cd.AccentForeground = Color.White;

            cd.WidgetIdle = Color.FromARGB(255, 33,33,33);
            cd.WidgetHover = Color.FromARGB(255, 26, 26, 26);
            cd.WidgetClicked = Color.FromARGB(255, 20, 20, 20);

            cd.WindowBackground = Color.FromARGB(255, 12, 12, 12);
            cd.Component = Color.FromARGB(255, 32, 32, 32);

            cd.Foreground = Color.FromARGB(255, 255, 255, 255);

            this.BorderRadius = 0;
            this.CenterWindowTitle = false;

            return cd;
        }

         **/

        protected override ColorDictionnary InitColorDictionnary()
        {
            ColorDictionnary cd = new ColorDictionnary();
            cd.AccentIdle = Color.FromARGB(128, 73, 9, 158);
            cd.AccentHover = Color.FromARGB(128, 57, 7, 122);
            cd.AccentClicked = Color.FromARGB(128, 39, 6, 82);
            cd.AccentForeground = Color.White;

            cd.WidgetIdle = Color.FromARGB(128, 33,33,33);
            cd.WidgetHover = Color.FromARGB(128, 26, 26, 26);
            cd.WidgetClicked = Color.FromARGB(128, 20, 20, 20);

            cd.WindowBackground = Color.FromARGB(128, 12, 12, 12);
            cd.Component = Color.FromARGB(128, 32, 32, 32);

            cd.Foreground = Color.White;

            this.BorderRadius = 0;
            this.CenterWindowTitle = false;

            return cd;
        }
    }
}
