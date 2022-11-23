using SipaaKernel.Core;
using SipaaKernel.UI;
using PrismGL2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SipaaKernel.UI.SysTheme2;

namespace SipaaKernel
{
    public class TestApp : Application
    {
        public override void AppMain()
        {
            var w = new Window();
            var s =
                $"Application Name : {this.ApplicationName}\n" +
                $"Application Display Name : {this.ApplicationDisplayName}\n" +
                $"Application Package Name : {this.ApplicationPackageName}";

            w.OnDraw = (g) => 
            {
                if (w.EnableWindowFrame)
                {
                    g.DrawString(w.X + 10, w.Y + (int)Window.TitleBarHeight + 10, s, Font.Fallback, ThemeManager.GetCurrentTheme().GetForegroundColor());
                }
                else
                {
                    g.DrawString(w.X + 10, w.Y + 10, s, Font.Fallback, ThemeManager.GetCurrentTheme().GetForegroundColor());
                }
            };
        }

        public override void InitAppInfo()
        {
            ApplicationDisplayName = "Test Application";
            ApplicationIcon = null;
            ApplicationName = "TestApp";
            ApplicationPackageName = "fr.raphmar2019.testapp";
        }
    }
}
