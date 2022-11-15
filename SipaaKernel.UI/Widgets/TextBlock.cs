using PrismGL2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SipaaKernel.UI.SysTheme2.ThemeManager;
namespace SipaaKernel.UI.Widgets
{
    // a text block is literally nothing lol
    public class TextBlock : Widget
    {
        public override void OnDraw(Graphics g)
        {
            g.DrawString(X, Y, Text, Font.Fallback, GetCurrentTheme().GetForegroundColor());
        }
    }
}
